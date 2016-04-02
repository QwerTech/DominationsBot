using System.Drawing.Imaging;
using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Tests
{
    [TestClass]
    public class ScreenCaptureTests
    {
        readonly IContainer _container = new Container(new RootRegistry());
        [TestMethod]
        public void TestScreenDimentions()
        {
            var blueStackController = _container.GetInstance<EmulatorWindowController>();
            blueStackController.Activate();
            var screenCapture = _container.GetInstance<ScreenCapture>();

            var snapShot = screenCapture.SnapShot();

            snapShot.Save("snapshot.png");
            Assert.AreEqual(blueStackController.GetArea().Size, snapShot.Size);
        }
        [TestMethod]
        public void TestScreenPixelFormat()
        {
            var blueStackController = _container.GetInstance<EmulatorWindowController>();
            blueStackController.Activate();
            var screenCapture = _container.GetInstance<ScreenCapture>();

            var snapShot = screenCapture.SnapShot();

            snapShot.Save("snapshot.png");
            Assert.AreEqual(PixelFormat.Format24bppRgb, snapShot.PixelFormat);
        }
    }
}