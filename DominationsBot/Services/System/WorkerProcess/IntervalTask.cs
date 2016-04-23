using System;
using System.Threading.Tasks;

namespace DominationsBot.Services.System.WorkerProcess
{
    public interface IWork
    {
        void StartSheduling();
    }

    public abstract class Work:GameWork, IWork
    {
        private readonly WorkingQueue _workingQueue;
        private readonly TimeSpan _interval;

        protected Work(WorkingQueue workingQueue, TimeSpan interval)
        {
            _workingQueue = workingQueue;
            _interval = interval;
        }

        public void StartSheduling()
        {
            var gameTask = new GameTask(this);
            _workingQueue.EnqueueAndSignal(gameTask);
            gameTask.Task
                .ContinueWith(task1 => Task
                    .Delay(_interval)
                    .ContinueWith(task2 => StartSheduling()));
        }
    }
}