﻿using AForge.Imaging;
using DominationsBot.Services.GameProcess;
using StructureMap;

namespace DominationsBot.DI
{
    public class RootRegistry:Registry
    {
        public RootRegistry()
        {
            For<ITemplateMatching>()
                .Use<ExhaustiveTemplateMatching>().Ctor<float>().Is(0.8f);
            var workers = For<IWorker>();
            workers.Add<CollectGold>();
            workers.Add<CollectFood>();
        }
    }
}
