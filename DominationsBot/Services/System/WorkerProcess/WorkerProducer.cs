using System.Diagnostics;
using System.Threading.Tasks;
using DominationsBot.Services.GameProcess;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class WorkerProducer
    {
        private readonly WorkingQueue _workingQueue;

        public WorkerProducer(IWorker[] workers, WorkingQueue workingQueue)
        {
            _workingQueue = workingQueue;
        }

        public Task StartFill()
        {
            Trace.TraceInformation("Начинаем создавать задания");
            return Task.Delay(100);
        }
    }
}