using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using StructureMap;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.Tests
{
    [TestFixture]
    public class GameProcessTests
    {
        IContainer Container = new Container(new RootRegistry());
        [TestMethod]
        public void TestUnzoom()
        {
            var gameController = Container.GetInstance<GameController>();
            //for (int i = 0; i < 10; i++)
            {
                gameController.Unzoom();
            }
            

        }
        [TestMethod]
        public void TestCollectGold()
        {
            var controller = Container.GetInstance<CollectGold>();
            controller.DoWork();

        }
        [TestMethod]
        public void TestCollectFood()
        {
            var controller = Container.GetInstance<CollectFood>();
            controller.DoWork();

        }
        [TestMethod]
        public void TestTrainTroops()
        {
            var controller = Container.GetInstance<TrainTroops>();
            controller.DoWork();
        }
        [TestMethod]
        public void TestAntiSleep()
        {
            var controller = Container.GetInstance<AntiSleepGame>();
            controller.DoWork();

        }
        [Test]
        public void TestGameIsSleep()
        {
            var controller = Container.GetInstance<AntiSleepGame>();
            var isGameSleeps = controller.IsGameSleeps();
            Assert.IsTrue(isGameSleeps);
        }
    }
}
