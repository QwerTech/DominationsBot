using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using NUnit.Framework;
using StructureMap;

namespace Tests.Services.GameProcess
{
    [TestFixture()]
    public class TrainTroopsTests
    {
        readonly IContainer _container = new Container(new TestRootRegistry());

        [Test()]
        public void DoWorkTest()
        {
var controller = _container.GetInstance<TrainTroops>();
            controller.DoWork();
        }
    }
}