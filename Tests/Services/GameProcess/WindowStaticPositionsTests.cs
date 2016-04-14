using NUnit.Framework;
using DominationsBot.Services.GameProcess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominationsBot.DI;
using DominationsBot.Services.Logging;
using StructureMap;
using Tests;

namespace DominationsBot.Services.GameProcess.Tests
{
    [TestFixture()]
    public class WindowStaticPositionsTests
    {
        [Test()]
        public void ViewTest()
        {
            var container = new Container(new RootRegistry());
            var screenCapture = container.GetInstance<ScreenCapture>();
            var normalScreen = screenCapture.SnapShot();
            var viewImage = normalScreen.Clone(new Rectangle(new Point(0, 0), normalScreen.Size), normalScreen.PixelFormat);
            WindowStaticPositions.View(viewImage);
            
            container.GetInstance<ImageLogger>().Log(viewImage,nameof(WindowStaticPositions));
            
        }
    }
}