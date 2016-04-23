using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class WorkingQueue:ConcurrentQueue<GameTask>
    {
        public EventWaitHandle ExitThreadEvent { get; } = new ManualResetEvent(false);

        public EventWaitHandle NewItemEvent { get; } = new AutoResetEvent(false);

        public WaitHandle[] EventArray  => new WaitHandle[] {ExitThreadEvent, NewItemEvent};

        public void EnqueueAndSignal(GameTask task)
        {
            Trace.TraceInformation("Кладем новое задание в очередь");
            this.Enqueue(task);
            NewItemEvent.Set();
        }

    }

    public abstract class GameWork
    {
        public abstract string Name { get;  }
        public abstract Action Action { get;  }

    }

    public class GameTask
    {
        public GameWork Work { get; }
        public Task Task { get;  }

        public GameTask(GameWork gameWork)
        {
            Work = gameWork;
            Task = new Task(Work.Action);
        }
    }
}