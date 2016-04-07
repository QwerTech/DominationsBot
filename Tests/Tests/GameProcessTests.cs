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
        [Test,Explicit]
        public void TestUnzoom()
        {
            var gameController = Container.GetInstance<GameController>();
            //for (int i = 0; i < 10; i++)
            {
                gameController.Unzoom();
            }
            

        }
        [Test,Explicit]
        public void TestCollectGold()
        {
            var controller = Container.GetInstance<CollectGold>();
            controller.DoWork();

        }
        [Test,Explicit]
        public void TestCollectFood()
        {
            var controller = Container.GetInstance<CollectFood>();
            controller.DoWork();

        }
        [Test,Explicit]
        public void TestTrainTroops()
        {
            var controller = Container.GetInstance<TrainTroops>();
            controller.DoWork();
        }
        [Test, Explicit]
        public void TestAntiSleep()
        {
            var controller = Container.GetInstance<AntiSleepGame>();
            controller.DoWork();

        }
        [Test,Explicit]
        public void TestGameIsSleep()
        {
            var controller = Container.GetInstance<AntiSleepGame>();
            var isGameSleeps = controller.IsGameSleeps();
            Assert.IsTrue(isGameSleeps);
        }
    }
}
