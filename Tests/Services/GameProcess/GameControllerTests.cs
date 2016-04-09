using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using NUnit.Framework;
using StructureMap;

namespace Tests.Services.GameProcess
{
    [TestFixture]
    public class GameControllerTests
    {
        private readonly GameController _gameController;
        readonly IContainer _container = new Container(new TestRootRegistry());

        
        public GameControllerTests()
        {
            _gameController = _container.GetInstance<GameController>();
        }

        [Test,Explicit]
        public void ReadFoodAndGoldTest()
        {
            var readFood = _gameController.ReadFood();

            var readGold = _gameController.ReadGold();

            Assert.Pass($"Gold:{readGold} Food:{readFood}");
        }


        [Test]
        public void UnzoomTest()
        {
            _gameController.Unzoom();
        }
    }
}