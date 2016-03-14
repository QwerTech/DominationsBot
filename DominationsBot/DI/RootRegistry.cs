using AForge.Imaging;
using DominationsBot.Services;
using DominationsBot.Services.GameProcess;
using StructureMap;
using StructureMap.Graph;

namespace DominationsBot.DI
{
    public class RootRegistry : Registry
    {
        public RootRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<ITemplateFinder>();
            });
            var templateFinders = For<ITemplateFinder>();
            templateFinders.Use<ExhaustiveTemplateMathingFinder>();
            For<ITemplateMatching>()
                .Use<ExhaustiveTemplateMatching>().Ctor<float>().Is(0.8f);
            var workers = For<IWorker>();
            workers.Add<CollectGold>();
            workers.Add<CollectFood>();
        }
    }
}