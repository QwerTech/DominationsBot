using System.Diagnostics;
using System.Threading.Tasks;
using DominationsBot.Services.GameProcess;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class TaskSheduler
    {
        private readonly CollectFood _collectFood;
        private readonly CollectGold _collectGold;
        private readonly GameController _gameController;
        private readonly AntiSleepGame _antiSleepGame;
        private readonly WorkerConsumer _workerConsumer;
        private readonly WorkerProducer _workerProducer;
        private readonly WorkingQueue _workingQueue;

        public TaskSheduler(WorkerProducer workerProducer, WorkerConsumer workerConsumer, WorkingQueue workingQueue,
            CollectFood collectFood, CollectGold collectGold, GameController gameController,AntiSleepGame antiSleepGame)
        {
            _workerProducer = workerProducer;
            _workerConsumer = workerConsumer;
            _workingQueue = workingQueue;
            _collectFood = collectFood;
            _collectGold = collectGold;
            _gameController = gameController;
            _antiSleepGame = antiSleepGame;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Начинаем выполнять задания");
            _workerProducer.StartFill();
            _workerConsumer.StartRake();
            _workingQueue.EnqueueAndSignal(new Task(() => _antiSleepGame.DoWork()));
            _workingQueue.EnqueueAndSignal(new Task(() => _gameController.Unzoom()));
            _workingQueue.EnqueueAndSignal(new Task(() => _collectFood.DoWork()));
            _workingQueue.EnqueueAndSignal(new Task(() => _collectGold.DoWork()));
            
        }
    }
}