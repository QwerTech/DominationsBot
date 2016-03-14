using DominationsBot;
using DominationsBot.DI;
using DominationsBot.Extensions;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class FinderTests
    {
        readonly IContainer _container = new Container(new RootRegistry());
        [TestMethod]
        public void TestFindCoins()
        {
            var resizeTemplateFinder = _container.GetInstance<ResizeTemplateFinder>();
            var templateMatches = resizeTemplateFinder.FindTemplate(TestScreens.TestScreen, Screens.Coin).ToList();
            TestScreens.TestScreen.ViewContains(templateMatches).Save("result.png");
            Assert.AreEqual(2, templateMatches.Count());
        }
    }
}
