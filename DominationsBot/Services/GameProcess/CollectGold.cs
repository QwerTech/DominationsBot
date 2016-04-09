using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.GameProcess
{
    public class CollectGold : IWorker
    {
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly ResizeTemplateFinder _finder;
        private readonly IScreenCapture _screenCapture;
        private readonly WorkingAreaFilter _workingAreaFilter;
        private readonly GameState _gameState;

        public CollectGold(ResizeTemplateFinder finder, IScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController,
            WorkingAreaFilter workingAreaFilter, GameState gameState)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
            _workingAreaFilter = workingAreaFilter;
            _gameState = gameState;
        }

        public void DoWork()
        {
            if (!_gameState.IsGameOnMainWindow())
                throw new ApplicationException("Игра не на главном окне");
            Trace.TraceInformation("Собираем золото");
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Coin).ToList();
            Trace.TraceInformation($"Результаты поиска золота: {string.Join(", ", templateMatches.Select(t=>t.Rectangle))}");
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