using System;
using System.Diagnostics;
using DominationsBot.Services.GameProcess;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class TaskSheduler
    {
        private readonly AntiSleepGame _antiSleepGame;
        private readonly IWork[] _works;
        private readonly CollectFood _collectFood;
        private readonly CollectGold _collectGold;
        private readonly GameController _gameController;
        
        private readonly WorkerConsumer _workerConsumer;
        private readonly WorkerProducer _workerProducer;
        

        public TaskSheduler(WorkerProducer workerProducer, WorkerConsumer workerConsumer, 
            CollectFood collectFood, CollectGold collectGold, GameController gameController, AntiSleepGame antiSleepGame,
            IWork[] works)
        {
            _workerProducer = workerProducer;
            _workerConsumer = workerConsumer;
            
            _collectFood = collectFood;
            _collectGold = collectGold;
            _gameController = gameController;
            _antiSleepGame = antiSleepGame;
            _works = works;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Начинаем выполнять задания");
            _workerProducer.StartFill();
            _workerConsumer.StartRake();
            foreach (var intervalWork in _works)
            {
                intervalWork.StartSheduling();
            }
        }
    }
}