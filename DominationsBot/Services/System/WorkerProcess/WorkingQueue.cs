using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class WorkingQueue:ConcurrentQueue<Task>
    {
        public EventWaitHandle ExitThreadEvent { get; } = new ManualResetEvent(false);

        public EventWaitHandle NewItemEvent { get; } = new AutoResetEvent(false);

        public WaitHandle[] EventArray  => new WaitHandle[] {ExitThreadEvent, NewItemEvent};

        public void EnqueueAndSignal(Task task)
        {
            Trace.TraceInformation("Кладем новое задание в очередь");
            this.Enqueue(task);
            NewItemEvent.Set();
        }

    }
}