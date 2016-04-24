using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DominationsBot;
using DominationsBot.DI;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests.Tests
{
    [TestClass]
    public class FinderTests
    {
        private readonly IContainer _container = new Container(new TestRootRegistry());


        public static void Init(TestContext testContext)
        {

        }

        //[TestMethod]
        //public void TestAllTemplateFinderOnCoins()
        //{
        //    var finders = _container.GetAllInstances<ITemplateFinder>();
        //    foreach (var templateFinder in finders)
        //    {
        //        var stopwatch = new Stopwatch();
        //        stopwatch.Start();
        //        var templateMatches = templateFinder.FindTemplate(TestScreens.TestScreen, Screens.Coin).ToList();
        //        stopwatch.Stop();
        //        Trace.TraceInformation(
        //            $"\r\n{templateFinder.GetType().Name} нашел {templateMatches.Count} монетки за {stopwatch.ElapsedMilliseconds}ms. \r\n" +
        //            $"Координаты: {string.Join(",", templateMatches.OrderBy(t => t.Rectangle.X).ThenBy(t => t.Rectangle.Y).Select(t => t.Rectangle))}");
        //    }
        //}

        [TestMethod, Ignore]
        public void TestAllTemplateFinderOnSimple()
        {
            var finders =
                _container.GetAllInstances<ITemplateFinder>()
                    .Where(
                        t =>
                            t.GetType() != typeof(ResizeTemplateFinder) &&
                            t.GetType() != typeof(ResizeEpsilonTemplateFinder));
            foreach (var templateFinder in finders)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var templateMatches =
                    templateFinder.FindTemplate(TestScreens.SimpleFindBig, TestScreens.SimpleFindSmall1).ToList();
                stopwatch.Stop();
                Assert.AreEqual(1, templateMatches.Count, $"Поисковик {templateFinder.GetType().Name}");
            }
        }

        [TestMethod, Ignore]
        public void TestAllTemplateFinderOnSleepScreen()
        {
            var finders =
                _container.GetAllInstances<ITemplateFinder>().Where(t => t.GetType() != typeof(ResizeTemplateFinder)
                                                                         &&
                                                                         t.GetType() !=
                                                                         typeof(ResizeEpsilonTemplateFinder)
                                                                         && t.GetType() != typeof(EqualTemplateFinder));
            Assert.IsTrue(finders.Any());
            foreach (var templateFinder in finders)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var templateMatches = templateFinder.FindTemplate(TestScreens.SleepScreen, BotResources.BmpSleepDialog).ToList();
                stopwatch.Stop();

                Assert.AreEqual(1, templateMatches.Count, $"Поисковик {templateFinder.GetType().Name}");
                Trace.TraceInformation(
                    $"Поисковик {templateFinder.GetType().Name} нашел за {stopwatch.ElapsedMilliseconds}ms");
            }
        }

        [TestMethod, Ignore]
        public void TestTemplateFinderOnSleepScreen()
        {
            var finder = _container.GetInstance<Func<double, SaeedTemplateFinder>>()(0.2);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Assert.AreEqual(1, finder.FindTemplate(TestScreens.SleepScreen, BotResources.BmpSleepDialog).Count());
            stopwatch.Stop();
            Trace.TraceInformation($"Нашел за {stopwatch.ElapsedMilliseconds}ms");
        }
        [TestMethod, Ignore]
        public void TestSaeedTemplateFinderOnBattleAndStoreButtons()
        {
            var finder = _container.GetInstance<Func<double, SaeedTemplateFinder>>()(0.3);

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            Assert.AreEqual(1, finder.FindTemplate(TestScreens.NormalScreen, BotResources.BmpStoreButton).Count());
            stopwatch.Stop();
            Assert.AreEqual(1, finder.FindTemplate(TestScreens.NormalScreen, BotResources.BmpBattleButton).Count());

            Assert.AreEqual(1, finder.FindTemplate(TestScreens.NormalScreen2, BotResources.BmpStoreButton).Count());
            Assert.AreEqual(1, finder.FindTemplate(TestScreens.NormalScreen2, BotResources.BmpBattleButton).Count());
            Trace.TraceInformation($"Нашел за {stopwatch.ElapsedMilliseconds}ms");
        }


        [TestMethod, Ignore]
        public void TestAllTemplateFinderOnBattleAndStoreButtons()
        {

            List<ITemplateFinder> finders =
                _container.GetAllInstances<ITemplateFinder>()
                    .Where(t => t.GetType() != typeof(ResizeTemplateFinder)
                                //&& t.GetType() != typeof(ResizeEpsilonTemplateFinder)
                                && t.GetType() != typeof(EqualTemplateFinder)).ToList();
            finders.Add(new SaeedTemplateFinder(0.3));
            Assert.IsTrue(finders.Any());
            foreach (var templateFinder in finders)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var templateMatches =
                    templateFinder.FindTemplate(TestScreens.NormalScreen, BotResources.BmpBattleButton).ToList();
                stopwatch.Stop();
                var finderName = templateFinder.GetType().Name;
                TestScreens.NormalScreen.ViewContains(templateMatches)
                    .Save($"{finderName}.{nameof(BotResources.BmpBattleButton)}.png");
                //Assert.AreEqual(1, templateMatches.Count,$"{nameof(Screens.BattleButton)} Поисковик {finderName}");
                Trace.TraceInformation(
                    $"{nameof(BotResources.BmpBattleButton)} Поисковик {finderName} нашел за {stopwatch.ElapsedMilliseconds}ms");

                templateMatches = templateFinder.FindTemplate(TestScreens.NormalScreen, BotResources.BmpStoreButton).ToList();
                TestScreens.NormalScreen.ViewContains(templateMatches)
                    .Save($"{finderName}.{nameof(BotResources.BmpStoreButton)}.png");
                //Assert.AreEqual(1, templateMatches.Count,$"{nameof(Screens.StoreButton)} Поисковик {finderName} ");

                Trace.TraceInformation($"{nameof(BotResources.BmpStoreButton)} Поисковик {finderName}");
            }
        }
    }
}