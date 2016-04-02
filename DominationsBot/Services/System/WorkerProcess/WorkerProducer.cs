using System.Diagnostics;
using System.Threading.Tasks;

namespace DominationsBot.Services.GameProcess.WorkerProcess
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