using System.Windows.Forms;
using DominationsBot.DI;
using DominationsBot.Extensions;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests.Services
{
    [TestClass()]
    public class EmulatorWindowControllerTests
    {
        [TestMethod()]
        public void MouseCenterTest()
        {
            var emulatorWindowController = _container.GetInstance<EmulatorWindowController>();
            emulatorWindowController.MouseCenter();
            Assert.AreEqual(emulatorWindowController.GetLocation().Middle(), Cursor.Position);
        }
        IContainer _container = new Container(new RootRegistry());
        [TestMethod()]
        public void GetAreaTest()
        {

        }

        [TestMethod()]
        public void GetLocationTest()
        {

        }

        [TestMethod()]
        public void SetDimensionsIntoRegistryTest()
        {

        }

        [TestMethod()]
        public void ClickTest()
        {

        }

        [TestMethod()]
        public void ClickTest1()
        {

        }

        [TestMethod()]
        public void SendVirtualKeyTest()
        {

        }

        [TestMethod()]
        public void SendTest()
        {

        }

        [TestMethod()]
        public void ClickOnPoint2Test()
        {

        }

        [TestMethod()]
        public void ActivateBlueStackTest()
        {
            _container.GetInstance<EmulatorWindowController>().Activate();
        }
    }
}