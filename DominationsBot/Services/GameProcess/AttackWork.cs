using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using DominationsBot.Extensions;
using DominationsBot.Services.ImageProcessing.ImageComporators;
using DominationsBot.Services.ImageProcessing.TextReading;
using DominationsBot.Services.System.WorkerProcess;
using StructureMap.Attributes;

namespace DominationsBot.Services.GameProcess
{
    public class AttackWork : GameWork, IWork
    {
        private readonly Queue<SizeF> _stepsFor4Ways = new Queue<SizeF>(new[]
        {
            WindowStaticPositions.UnzoomedStep,
            WindowStaticPositions.UnzoomedStep.Rotate90(),
            WindowStaticPositions.UnzoomedStep.Rotate90().Rotate90(),
            WindowStaticPositions.UnzoomedStep.Rotate90().Rotate90().Rotate90()
        });

        private readonly Queue<Expression<Func<Point>>> findDeployPointQueue = new Queue<Expression<Func<Point>>>();
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
        public NumberReader NumberReader { get; set; }

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
            Unzooming.Do();
            DeployUnits();
        }

        private void DeployUnits()
        {
            findDeployPointQueue.Clear();
            findDeployPointQueue.Enqueue(
                () => FindDeployPosition(WindowStaticPositions.Dimensions.Middle(), _stepsFor4Ways));
            var deployPoint = Point.Empty;
            while (findDeployPointQueue.Any() && deployPoint == Point.Empty)
            {
                var expression = findDeployPointQueue.Dequeue();
                var func = expression.Compile();
                deployPoint = func();
            }

        }

        [SetterProperty]
        public BattleWorkingAreaFilter BattleWorkingAreaFilter { get; set; }
        private Point FindDeployPosition(PointF pointF, Queue<SizeF> ways)
        {
            var point = pointF.ToPoint();
            if (BattleWorkingAreaFilter.IsInWorkingArea(point))
            {
                var snapShot = ScreenCapture.SnapShot(WindowStaticPositions.Battle.FirstTroopsCount);
                var beforeClick = NumberReader.Read(snapShot, NumberResourcesType.BeforeBattleTroops);
                

                EmulatorWindowController.Click(point);
                snapShot = ScreenCapture.SnapShot(WindowStaticPositions.Battle.FirstTroopsCount);
                var afterClick = NumberReader.Read(snapShot, NumberResourcesType.BeforeBattleTroops);

                if (beforeClick > afterClick)
                    return point;
            }
            for (var i = 0; i < ways.Count; i++)
            {
                var newRemainsQueue = new Queue<SizeF>(ways.Where((f, i1) => i1 < ways.Count/2f));
                var sizeF = ways.Dequeue();
                ways.Enqueue(sizeF);
                findDeployPointQueue.Enqueue(() => FindDeployPosition(pointF + sizeF, newRemainsQueue));
            }
            return Point.Empty;
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
            } while (opponentInfo.Sum < 10000 || opponentInfo.Level > 20);
        }

        private void WaitUntilOpponent()
        {
            Bitmap snapShot;
            do
            {
                Thread.Sleep(1000);
                snapShot = ScreenCapture.SnapShot(WindowStaticPositions.Battle.EndBattle);
            } while (!ByteLevelComparer.Compare(snapShot, BotResources.Symbols.Battle.BmpEndBattle));
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