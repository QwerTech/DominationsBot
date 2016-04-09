using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using NUnit.Framework;
using StructureMap;

namespace Tests.Services.GameProcess
{
    [TestFixture()]
    public class AntiSleepGameTests
    {
        readonly IContainer _container = new Container(new RootRegistry());
        [Test(),Explicit]
        public void IsGameSleepsTest()
        {
var controller = _container.GetInstance<AntiSleepGame>();
            var isGameSleeps = controller.IsGameSleeps();
            Assert.IsTrue(isGameSleeps);
        }

        [Test()]
        public void DoWorkTest()
        {
            var controller = _container.GetInstance<AntiSleepGame>();
            controller.DoWork();
        }
    }
}