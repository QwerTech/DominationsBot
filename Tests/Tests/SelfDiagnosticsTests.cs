using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests
{
    [TestClass]
    public class SelfDiagnosticsTests
    {
        readonly IContainer _container = new Container(new RootRegistry());
        [TestMethod]
        public void TestCheckDimensions()
        {
            var selfDiagnostics = _container.GetInstance<SelfDiagnostics>();
            _container.GetInstance<EmulatorWindowController>().Activate();
            selfDiagnostics.Check();
        }
    }
}
