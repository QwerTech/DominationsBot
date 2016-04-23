using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using DominationsBot.Extensions;
using DominationsBot.Resources;
using DominationsBot.Services.ImageProcessing.ImageComporators;
using DominationsBot.Services.System.WorkerProcess;
using StructureMap.Attributes;

namespace DominationsBot.Services.GameProcess
{
    public class AttackWork : GameWork, IWork
    {
        public override string Name => "Атака";
        public override Action Action => Attack;

        [SetterProperty]
        public EmulatorWindowController EmulatorWindowController { get; set; }


        [SetterProperty]
        public ScreenCapture ScreenCapture { get; set; }

        [SetterProperty]
        public WorkingQueue WorkingQueue { get; set; }

        [SetterProperty]
        public ByteLevelComparer ByteLevelComparer { get; set; }

        [SetterProperty]
        public OpponentInfoReader OpponentInfoReader { get; set; }

        [SetterProperty]
        public Unzooming Unzooming { get; set; }

        public void StartSheduling()
        {
            var gameTask = new GameTask(this);
            WorkingQueue.EnqueueAndSignal(gameTask);
            gameTask.Task
                .ContinueWith(task1 => Task
                    .Delay(TimeSpan.FromMinutes(20))
                    .ContinueWith(task2 => StartSheduling()));
        }

        private void Attack()
        {
            EmulatorWindowController.Click(WindowStaticPositions.MainScreen.Battle.Middle());
            Thread.Sleep(1000);
            EmulatorWindowController.Click(WindowStaticPositions.Battle.FindOpponent);

            WaitUntilSuitable();
            Unzooming.CanWorkAndDo();
        }

        private void WaitUntilSuitable()
        {
            OpponentInfo opponentInfo = null;
            do
            {
                if (opponentInfo != null)
                {
                    EmulatorWindowController.Click(WindowStaticPositions.Battle.NextMatch);
                    Thread.Sleep(250);
                }
                WaitUntilOpponent();

                opponentInfo = OpponentInfoReader.Read();

                Trace.TraceInformation(
                    $"Еда:{opponentInfo.Food} Золото:{opponentInfo.Gold} Уровень:{opponentInfo.Level}");
            } while (opponentInfo.Sum < 100000 || opponentInfo.Level > 20);
        }

        private void WaitUntilOpponent()
        {
            Bitmap snapShot;
            do
            {
                Thread.Sleep(1000);
                snapShot = ScreenCapture.SnapShot(WindowStaticPositions.Battle.EndBattle);
            } while (ByteLevelComparer.Compare(snapShot, BotResources.Symbols.Battle.BmpEndBattle));
        }
    }

    public class OpponentInfo
    {
        public int? Food { set; get; }
        public int? Gold { set; get; }
        public int? Sum => Food + Gold;
        public int? Level { get; set; }
        public override string ToString()
        {
            return $"Gold = {Gold},Food = {Food}, Level = {Level}";
        }

        public bool Compare(OpponentInfo opponentInfo)
        {
            return Food == opponentInfo.Food && Gold == opponentInfo.Gold && Level == opponentInfo.Level;
        }
    }
}