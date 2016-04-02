using System.Diagnostics;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using System.Threading;

namespace DominationsBot.Services.GameProcess
{
    public class CollectFood : IWorker
    {
        private readonly ResizeTemplateFinder _finder;
        private readonly ScreenCapture _screenCapture;
        private readonly EmulatorWindowController _emulatorWindowController;

        public CollectFood(ResizeTemplateFinder finder, ScreenCapture screenCapture, EmulatorWindowController emulatorWindowController)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Начинаем собирать еду");
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Food);

            foreach (var match in templateMatches)
            {
                _emulatorWindowController.Click(match.Rectangle.Middle());
                Thread.Sleep(250);
            }
            Trace.TraceInformation("Собрали еду");
        }
    }
}