using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.TemplateFinders;

namespace DominationsBot.Services.GameProcess
{
    public class CollectFood : IWorker
    {
        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly GameState _gameState;
        private readonly ITemplateFinder _finder;
        private readonly IScreenCapture _screenCapture;

        public CollectFood(ExhaustiveTemplateMathingFinder finder, IScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController, GameState gameState)
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
            _gameState = gameState;
        }

        public void DoWork()
        {
            if(!_gameState.IsGameOnMainWindow())
                throw new ApplicationException("Игра не на главном окне");
            Trace.TraceInformation("Начинаем собирать еду");
            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Screens.Food).ToList();
            foreach (var match in templateMatches)
            {
                _emulatorWindowController.Click(match.Rectangle.Middle());
                Thread.Sleep(250);
            }
            Trace.TraceInformation("Собрали еду");
        }
    }
}