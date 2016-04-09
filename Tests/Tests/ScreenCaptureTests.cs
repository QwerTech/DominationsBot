using System.Drawing.Imaging;
using DominationsBot.DI;
using DominationsBot.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using StructureMap;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Tests.Tests
{
    [TestFixture]
    public class ScreenCaptureTests
    {
        readonly IContainer _container = new Container(new TestRootRegistry());
        [Test]
        public void TestScreenDimentions()
        {
            var blueStackController = _container.GetInstance<EmulatorWindowController>();
            var screenCapture = _container.GetInstance<ScreenCapture>();

            var snapShot = screenCapture.SnapShot();

            snapShot.Save("c:\\temp\\snapshot.png");
            Assert.AreEqual(blueStackController.GetArea().Size, snapShot.Size);
        }
        [Test, Explicit]
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