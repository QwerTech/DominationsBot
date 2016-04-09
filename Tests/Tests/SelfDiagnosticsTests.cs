using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests.Tests
{
    [TestClass]
    public class SelfDiagnosticsTests
    {
        readonly IContainer _container = new Container(new TestRootRegistry());
        [TestMethod, Ignore]
        public void TestCheckDimensions()
        {
            var selfDiagnostics = _container.GetInstance<SelfDiagnostics>();
            _container.GetInstance<EmulatorWindowController>().Activate();
            selfDiagnostics.Check();
        }
    }
}
