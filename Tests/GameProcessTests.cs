using System;
using DominationsBot.DI;
using DominationsBot.Services;
using DominationsBot.Services.GameProcess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests
{
    [TestClass]
    public class GameProcessTests
    {
        IContainer Container = new Container(new RootRegistry());
        [TestMethod]
        public void TestUnzoom()
        {
            var gameController = Container.GetInstance<GameController>();
            gameController.Unzoom();
            
        }
    }
}
