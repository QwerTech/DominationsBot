using DominationsBot.Extensions;
using System.Linq;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using DominationsBot.Services.System;

namespace DominationsBot.Services.GameProcess
{
    public class CollectFood : IWorker
    {
        private readonly ResizeTemplateFinder _finder;
        private readonly ScreenCapture _screenCapture;
        private readonly BlueStackController _blueStackController;

        public CollectFood(ResizeTemplateFinder finder, ScreenCapture screenCapture, BlueStackController blueStackController)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _blueStackController = blueStackController;
        }

        public void DoWork()
        {
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Food);

            foreach (var match in templateMatches)
            {
                _blueStackController.Click(new Win32.Point(match.Rectangle.Multiply(_finder.Divisor).Middle()));
            }
            if (templateMatches.Count() != 0)
                DoWork();
        }
    }
}