using System;
using System.Threading.Tasks;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class IntervalTask
    {
        private readonly WorkingQueue _workingQueue;

        public IntervalTask(WorkingQueue workingQueue)
        {
            _workingQueue = workingQueue;
        }

        public void Start(TimeSpan interval, Action action)
        {
            var task = new Task(action);
            _workingQueue.EnqueueAndSignal(task);
            task
                .ContinueWith(task1 => Task
                    .Delay(interval)
                    .ContinueWith(task2 => Start(interval, action)));
        }
    }
}