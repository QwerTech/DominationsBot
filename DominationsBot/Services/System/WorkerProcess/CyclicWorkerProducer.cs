﻿namespace DominationsBot.Services.System.WorkerProcess
{
    public class CyclicWorkerProducer
    {
        private readonly WorkingQueue _workingQueue;

        public CyclicWorkerProducer(WorkingQueue workingQueue)
        {
            _workingQueue = workingQueue;
        }

    }
}