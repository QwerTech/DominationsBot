using System;
using System.Drawing;
using DominationsBot.DI;
using DominationsBot.Services;
using Moq;
using StructureMap;

namespace Tests
{
    public class TestRootRegistry:Registry
    {
        public TestRootRegistry()
        {
            IncludeRegistry<RootRegistry>();
            For<EmulatorWindowController>()
                .Use(() => Mock.Of<EmulatorWindowController>(controller => controller.Handle == IntPtr.Zero &&
                controller.IsForeground && controller.IsRunning && controller.IsVisible && controller.GetLocation()==new Rectangle(0,0,100,100)));
        }
    }
}