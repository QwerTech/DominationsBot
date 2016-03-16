using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using System.Linq;
using System.Threading;

namespace DominationsBot.Services.GameProcess
{
    public class CollectGold : IWorker
    {
        private readonly ResizeTemplateFinder _finder;
        private readonly ScreenCapture _screenCapture;
        private readonly BlueStackController _blueStackController;
        private readonly WorkingAreaFilter _workingAreaFilter;

        public CollectGold(ResizeTemplateFinder finder, ScreenCapture screenCapture,
            BlueStackController blueStackController,
            WorkingAreaFilter workingAreaFilter)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _blueStackController = blueStackController;
            _workingAreaFilter = workingAreaFilter;
        }

        public void DoWork()
        {
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Coin);

            foreach (var match in templateMatches.Where(tm => _workingAreaFilter.IsInWorkingArea(tm.Rectangle.Middle()))
                )
            {
                _blueStackController.Click(match.Rectangle.Middle());
            }
            Thread.Sleep(1000);
        }
    }
}