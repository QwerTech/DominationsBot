using System.Drawing;
using System.Threading;
using DominationsBot.Services.ImageProcessing.ImageComporators;
using DominationsBot.Services.System;

namespace DominationsBot.Services.GameProcess
{
    public class Unzooming:WorkabilityDo
    {
        private readonly IScreenCapture _screenCapture;
        private readonly ByteLevelComparer _byteLevelComparer;
        private readonly EmulatorWindowController _emulatorWindowController;

        public Unzooming(IScreenCapture screenCapture, ByteLevelComparer byteLevelComparer, EmulatorWindowController emulatorWindowController)
        {
            _screenCapture = screenCapture;
            _byteLevelComparer = byteLevelComparer;
            _emulatorWindowController = emulatorWindowController;
        }

        public override bool Workability()
        {
            var snapShot = _screenCapture.SnapShot(WindowStaticPositions.ZoomingButtonRect);
            var bitmap = BotResources.Symbols.Zoom.Bmp1;
            var compare = _byteLevelComparer.Compare(snapShot, bitmap);
            return compare;
        }

        public override void Do()
        {
            _emulatorWindowController.SwipeOffset(WindowStaticPositions.ZoomingButton, new Point(0, 250));

            Thread.Sleep(1000);
        }
    }
}