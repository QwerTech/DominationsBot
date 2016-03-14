using System.Drawing;
using DominationsBot;
using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests
{
    [TestClass]
    public class FinderTests
    {
        readonly IContainer _container = new Container(new RootRegistry());
        [TestMethod]
        public void TestFindCoins()
        {
            //var resizeTemplateFinder = _container.GetInstance<SaeedTemplateFinder>();
            //var templateMatches = resizeTemplateFinder.FindTemplate(TestScreens.TestScreen, Screens.Coin).ToList();
            for (int i = 0; i < 100; i++)
            {
                var subPositions = SearchImage.GetSubPositions(TestScreens.TestScreen, new Bitmap(@"d:/TestScreenSub.png"));
            }
            
            //TestScreens.TestScreen.ViewContains(templateMatches).Save("result.png");
            //Assert.AreEqual(2, templateMatches.Count());
        }
    }
}
