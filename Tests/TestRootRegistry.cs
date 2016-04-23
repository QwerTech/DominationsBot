using System;
using System.Drawing;
using DominationsBot.DI;
using DominationsBot.Services;
using Moq;
using StructureMap;

namespace Tests
{
    public class TestRootRegistry : Registry
    {
        public TestRootRegistry()
        {
            IncludeRegistry<RootRegistry>();
            For<EmulatorWindowController>()
                .Use(() => Mock.Of<EmulatorWindowController>(controller => controller.Handle == IntPtr.Zero &&
                                                                           controller.IsForeground &&
                                                                           controller.IsRunning && controller.IsVisible &&
                                                                           controller.GetArea() ==
                                                                           new Rectangle(new Point(0, 0),
                                                                               TestScreens.NormalScreen.Size)));

            ScreenCaptureMock = new Mock<IScreenCapture>();
            ScreenCaptureMock.Setup(capture => capture.SnapShot()).Returns(TestScreens.NormalScreen);
            ScreenCaptureMock.Setup(capture => capture.SnapShot(It.IsAny<Rectangle>())).Returns(
                (Rectangle rect) =>
                {
                    var snapShot = ScreenCaptureMock.Object.SnapShot();
                    return snapShot.Clone(rect, snapShot.PixelFormat);
                });

            For<IScreenCapture>().Use(ScreenCaptureMock.Object);
        }

        public Mock<IScreenCapture> ScreenCaptureMock { get; set; }
    }
}