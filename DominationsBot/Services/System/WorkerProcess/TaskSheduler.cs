using System;
using System.Diagnostics;
using DominationsBot.Services.GameProcess;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class TaskSheduler
    {
        private readonly AntiSleepGame _antiSleepGame;
        private readonly IntervalTask _intervalTask;
        private readonly CollectFood _collectFood;
        private readonly CollectGold _collectGold;
        private readonly GameController _gameController;
        
        private readonly WorkerConsumer _workerConsumer;
        private readonly WorkerProducer _workerProducer;
        

        public TaskSheduler(WorkerProducer workerProducer, WorkerConsumer workerConsumer, 
            CollectFood collectFood, CollectGold collectGold, GameController gameController, AntiSleepGame antiSleepGame,
            IntervalTask intervalTask)
        {
            _workerProducer = workerProducer;
            _workerConsumer = workerConsumer;
            
            _collectFood = collectFood;
            _collectGold = collectGold;
            _gameController = gameController;
            _antiSleepGame = antiSleepGame;
            _intervalTask = intervalTask;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Начинаем выполнять задания");
            _workerProducer.StartFill();
            _workerConsumer.StartRake();

            var timeSpan = TimeSpan.FromMinutes(15);
            _intervalTask.Start(timeSpan, () => _antiSleepGame.DoWork());
            _intervalTask.Start(timeSpan, () => _gameController.Unzoom());
            //_intervalTask.Start(timeSpan, () => _collectFood.DoWork());
            _intervalTask.Start(timeSpan, () => _collectGold.DoWork());
        }
    }
}