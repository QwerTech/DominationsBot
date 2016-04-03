using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using NLog;

namespace DominationsBot.Services.System.WorkerProcess
{
    public class WorkerConsumer
    {
        private readonly WorkingQueue _workingQueue;

        public Task CurrentWork;

        public WorkerConsumer(WorkingQueue workingQueue)
        {
            _workingQueue = workingQueue;
        }

        public Task StartRake()
        {
            Trace.TraceInformation("Начинаем разбирать задания");
            return Task.Run(() =>
            {
                while (true)
                {
                    
                    int waitedSignalIndex = Array.IndexOf(_workingQueue.EventArray,_workingQueue.NewItemEvent);
                    if(_workingQueue.IsEmpty)
                        waitedSignalIndex = WaitHandle.WaitAny(_workingQueue.EventArray);

                    if (_workingQueue.EventArray[waitedSignalIndex] == _workingQueue.NewItemEvent)
                        TryStartWorking();
                    if (_workingQueue.EventArray[waitedSignalIndex] == _workingQueue.ExitThreadEvent)
                    {
                        Trace.TraceInformation("Заканчиваем делать задания");
                        break;
                    }
                }
            });
        }

        protected void TryStartWorking()
        {
            Task result;
            if (_workingQueue.TryDequeue(out result))
            {
                try
                {

                    CurrentWork?.Wait();
                    CurrentWork = result;
                    Trace.TraceInformation("Делаем новое задание");
                    CurrentWork.ContinueWith(task =>
                    {
                        if (task.Exception != null)
                            LogManager.GetCurrentClassLogger().Error(task.Exception);
                    });
                    CurrentWork.Start();
                }
                catch (Exception e)
                {
                    LogManager.GetCurrentClassLogger().Error(e);
                    throw;
                }
            }
        }
    }
}