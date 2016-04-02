using Microsoft.VisualStudio.TestTools.UnitTesting;
using DominationsBot.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using DominationsBot.DI;
using DominationsBot.Extensions;
using StructureMap;

namespace DominationsBot.Services.Tests
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