using DominationsBot.DI;
using StructureMap;

namespace Tests
{
    public class TestRootRegistry : Registry
    {
        public TestRootRegistry()
        {
            IncludeRegistry<RootRegistry>();
            //For<EmulatorWindowController>()
            //    .Use(() => Mock.Of<EmulatorWindowController>(controller => controller.Handle == IntPtr.Zero &&
            //                                                               controller.IsForeground &&
            //                                                               controller.IsRunning && controller.IsVisible &&
            //                                                               controller.GetArea() ==
            //                                                               new Rectangle(new Point(0, 0),
            //                                                                   TestScreens.NormalScreen.Size)));
            //var screenCaptureMock
            //    = new Mock<IScreenCapture>();
            //screenCaptureMock.Setup(capture => capture.SnapShot()).Returns(TestScreens.NormalScreen);
            //screenCaptureMock.Setup(capture => capture.SnapShot(It.IsAny<Rectangle>())).Returns(
            //    (Rectangle rect) =>
            //    {
            //        var snapShot = screenCaptureMock.Object.SnapShot();
            //        return snapShot.Clone(rect, snapShot.PixelFormat);
            //    });

            //For<IScreenCapture>().Use(screenCaptureMock.Object);
        }
    }
}       