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
            var task = new Task(() =>
            {
                action();
                Task.Delay(interval).Wait();
            });
            _workingQueue.EnqueueAndSignal(task);
            task.ContinueWith(task1 => Start(interval,action));
        }
    }
}