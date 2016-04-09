using DominationsBot.Services.GameProcess;
using NUnit.Framework;
using StructureMap;

namespace Tests.Services.GameProcess
{
    [TestFixture]
    public class TrainTroopsTests
    {
        private readonly IContainer _container = new Container(new TestRootRegistry());

        [Test, Explicit]
        public void DoWorkTest()
        {
            var controller = _container.GetInstance<TrainTroops>();
            controller.DoWork();
        }
    }
}