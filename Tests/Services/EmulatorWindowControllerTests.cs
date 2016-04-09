using System.Windows.Forms;
using DominationsBot.DI;
using DominationsBot.Extensions;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests.Services
{
    [TestClass]
    public class EmulatorWindowControllerTests
    {
        [TestMethod, Ignore]
        public void MouseCenterTest()
        {
            var emulatorWindowController = _container.GetInstance<EmulatorWindowController>();
            emulatorWindowController.MouseCenter();
            Assert.AreEqual(emulatorWindowController.GetLocation().Middle(), Cursor.Position);
        }

        readonly IContainer _container = new Container(new TestRootRegistry());
        

        [TestMethod, Ignore]
        public void ActivateBlueStackTest()
        {
            _container.GetInstance<EmulatorWindowController>().Activate();
        }
    }
}