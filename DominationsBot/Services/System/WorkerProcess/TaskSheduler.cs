using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DominationsBot.Services.GameProcess.WorkerProcess
{
    public class TaskSheduler
    {
        private readonly CollectFood _collectFood;
        private readonly CollectGold _collectGold;
        private readonly WorkerConsumer _workerConsumer;
        private readonly WorkerProducer _workerProducer;
        private readonly WorkingQueue _workingQueue;

        public TaskSheduler(WorkerProducer workerProducer, WorkerConsumer workerConsumer, WorkingQueue workingQueue,
            CollectFood collectFood, CollectGold collectGold)
        {
            _workerProducer = workerProducer;
            _workerConsumer = workerConsumer;
            _workingQueue = workingQueue;
            _collectFood = collectFood;
            _collectGold = collectGold;
        }

        public void DoWork()
        {
            Trace.TraceInformation("Начинаем выполнять задания");
            _workerProducer.StartFill();
            _workerConsumer.StartRake();
            _workingQueue.EnqueueAndSignal(new Task(() => _collectFood.DoWork()));
            
            while (_workingQueue.ExitThreadEvent.WaitOne(0))
            {
                Task currentTask = null;
                Task.Run(() =>
                {
                    if (currentTask == null || !_workingQueue.Contains(currentTask))
                        _workingQueue.EnqueueAndSignal(currentTask = new Task(() => _collectGold.DoWork()));
                })
                    .ContinueWith(task => Task.Delay(new TimeSpan(0, 30, 0)))
                    .Wait();
                
            }
        }
    }
}