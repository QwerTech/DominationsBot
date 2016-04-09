using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using NUnit.Framework;
using StructureMap;

namespace Tests.Services.GameProcess
{
    [TestFixture()]
    public class CollectFoodTests
    {
        readonly IContainer _container = new Container(new RootRegistry());
        [Test()]
        public void DoWorkTest()
        {
            var controller = _container.GetInstance<CollectFood>();
            controller.DoWork();
        }
    }
}