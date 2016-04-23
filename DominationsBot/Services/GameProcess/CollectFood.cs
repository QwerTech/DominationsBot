using System;
using System.Drawing;
using System.Linq;
using DominationsBot.Extensions;
using DominationsBot.Resources;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
using DominationsBot.Services.System.WorkerProcess;
using StructureMap.Attributes;

namespace DominationsBot.Services.GameProcess
{
    public abstract class GoldAndFoodCollector : Work, IWorker
    {
        private readonly AntiSleepGame _antiSleepGame;

        private readonly EmulatorWindowController _emulatorWindowController;
        private readonly ITemplateFinder _finder;
        private readonly GameState _gameState;
        private readonly IScreenCapture _screenCapture;


        public GoldAndFoodCollector(ExhaustiveTemplateMathingFinder finder, IScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController, GameState gameState, WorkingQueue workingQueue,
            AntiSleepGame antiSleepGame) : base(workingQueue, new TimeSpan(0, 15, 0))
        {
            _finder = finder;
            _screenCapture = screenCapture;
            _emulatorWindowController = emulatorWindowController;
            _gameState = gameState;
            _antiSleepGame = antiSleepGame;
        }

        [SetterProperty]
        public Unzooming Unzooming { get; set; }

        protected abstract Bitmap Template { get; }


        public override Action Action => DoWork;

        public void DoWork()
        {
            _antiSleepGame.CheckAndAct();

            if (!_gameState.IsGameOnMainWindow())
                throw new ApplicationException("Игра не на главном окне.");

            Unzooming.CanWorkAndDo();

            var snapShot = _screenCapture.SnapShot();
            var templateMatches = _finder.FindTemplate(snapShot, Template).ToList();
            foreach (var match in templateMatches)
            {
                _emulatorWindowController.Click(match.Rectangle.Middle());
            }
        }
    }

    public class CollectGold : GoldAndFoodCollector
    {
        public CollectGold(ExhaustiveTemplateMathingFinder finder, IScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController, GameState gameState, WorkingQueue workingQueue,
            AntiSleepGame antiSleepGame)
            : base(finder, screenCapture, emulatorWindowController, gameState, workingQueue, antiSleepGame)
        {
        }

        public override string Name => "Сбор золота";
        protected override Bitmap Template => BotResources.BmpCoin;
    }

    public class CollectFood : GoldAndFoodCollector
    {
        public CollectFood(ExhaustiveTemplateMathingFinder finder, IScreenCapture screenCapture,
            EmulatorWindowController emulatorWindowController, GameState gameState, WorkingQueue workingQueue,
            AntiSleepGame antiSleepGame)
            : base(finder, screenCapture, emulatorWindowController, gameState, workingQueue, antiSleepGame)
        {
        }

        public override string Name => "Сбор еды";
        protected override Bitmap Template => BotResources.BmpFood;
    }
}