using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using System;
using System.Threading;

namespace DominationsBot.Services.GameProcess
{
    public class AntiSleepGame : IWorker
    {
        private readonly ITemplateFinder _sleepScreenFinder;
        private readonly ScreenCapture _screenCapture;
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly SaeedTemplateFinder _buttosFinder;

        public AntiSleepGame(Func<double, SaeedTemplateFinder> saeedTemplateFinderProvider, ScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController)
        {
            _sleepScreenFinder = saeedTemplateFinderProvider(0);
            _buttosFinder = saeedTemplateFinderProvider(0.3);
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
        }

        public void DoWork()
        {
            var snapShot = _screenCapture.SnapShot();

            if (_sleepScreenFinder.Exists(snapShot, Screens.SleepDialog))
            {
                _emulatorWindowController.Click(WindowStaticPositions.SleepReloadGame);
                Thread.Sleep(10000);
            }
            snapShot = _screenCapture.SnapShot();
            var i = 15;
            while ((!_buttosFinder.Single(snapShot, Screens.StoreButton) ||
                    !_buttosFinder.Single(snapShot, Screens.BattleButton))
                   && i > 0)
            {
                Thread.Sleep(1000);
                snapShot = _screenCapture.SnapShot();
                i--;
            }

            Thread.Sleep(1000);
        }
    }
}