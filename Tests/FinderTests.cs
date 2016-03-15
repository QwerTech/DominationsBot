using DominationsBot;
using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace Tests
{
    [TestClass]
    public class FinderTests
    {
        private readonly IContainer _container = new Container(new RootRegistry());

        [TestMethod]
        public void TestFindCoins()
        {
            //var resizeTemplateFinder = _container.GetInstance<SaeedTemplateFinder>();
            //var templateMatches = resizeTemplateFinder.FindTemplate(TestScreens.TestScreen, Screens.Coin).ToList();
            var subPositions = SearchImage.GetSubPositions(TestScreens.TestScreen, new Bitmap(@"d:/TestScreenSub.png"));


            //TestScreens.TestScreen.ViewContains(templateMatches).Save("result.png");
            //Assert.AreEqual(3, templateMatches.Count());
        }

        [TestMethod]
        public void TestResizeEpsilonTemplateFinderOnCoins()
        {
            var finder = _container.GetInstance<ResizeEpsilonTemplateFinder>();
            var templateMatches = finder.FindTemplate(TestScreens.TestScreen, Screens.Coin).ToList();


            //TestScreens.TestScreen.ViewContains(templateMatches).Save("result.png");
            Assert.AreEqual(3, templateMatches.Count());
        }

        [TestMethod]
        public void TestAllTemplateFinderOnCoins()
        {
            var finders = _container.GetAllInstances<ITemplateFinder>();
            foreach (var templateFinder in finders)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                var templateMatches = templateFinder.FindTemplate(TestScreens.TestScreen, Screens.Coin).ToList();   
                stopwatch.Stop();
                Trace.TraceInformation(
                    $"\r\n{templateFinder.GetType().Name} нашел {templateMatches.Count} монетки за {stopwatch.ElapsedMilliseconds}ms. \r\n" +
                    $"Координаты: {string.Join(",", templateMatches.OrderBy(t => t.Rectangle.X).ThenBy(t => t.Rectangle.Y).Select(t => t.Rectangle))}");
            }
        }
    }
}