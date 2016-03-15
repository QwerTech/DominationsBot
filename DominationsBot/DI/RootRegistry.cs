using AForge.Imaging;
using DominationsBot.Services;
using DominationsBot.Services.GameProcess;
using StructureMap;
using StructureMap.Graph;
using Tesseract;

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

            ForConcreteType<TesseractEngine>()
                .Configure.SelectConstructor(() => new TesseractEngine(null, null, default(EngineMode)))
                .Ctor<string>("datapath").Is(@"./tessdata")
                .Ctor<string>("language").Is("eng")
                .Ctor<EngineMode>().Is(EngineMode.Default)
                .Singleton();

            For<ITemplateMatching>()
                .Use<ExhaustiveTemplateMatching>().Ctor<float>().Is(0.8f);
            var workers = For<IWorker>();
            workers.Add<CollectGold>();
            workers.Add<CollectFood>();
        }
    }
}