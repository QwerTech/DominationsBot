using DominationsBot.Extensions;
using System.Linq;
using System.Threading;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using DominationsBot.Services.System;

namespace DominationsBot.Services.GameProcess
{
    public class CollectGold : IWorker
    {
        private readonly ResizeTemplateFinder _finder;
        private readonly ScreenCapture _screenCapture;
        private readonly BlueStackController _blueStackController;

        public CollectGold(ResizeTemplateFinder finder, ScreenCapture screenCapture, BlueStackController blueStackController)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _blueStackController = blueStackController;
        }

        public void DoWork()
        {
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Coin);

            foreach (var match in templateMatches)
            {
                _blueStackController.Click(new Win32.Point(match.Rectangle.Multiply(_finder.Divisor).Middle()));

            }
            Thread.Sleep(1000);
            if (templateMatches.Count() != 0)
                DoWork();
        }
    }
}