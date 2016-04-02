using System.Diagnostics;
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
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly WorkingAreaFilter _workingAreaFilter;

        public CollectGold(ResizeTemplateFinder finder, ScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController,
            WorkingAreaFilter workingAreaFilter)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
            _workingAreaFilter = workingAreaFilter;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Собираем золото");
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Coin);

            foreach (var match in templateMatches.Where(tm => _workingAreaFilter.IsInWorkingArea(tm.Rectangle.Middle()))
                )
            {
                _emulatorWindowController.Click(match.Rectangle.Middle());
                Thread.Sleep(250);
            }
            Trace.TraceInformation("Собрали золото");
        }
    }
}