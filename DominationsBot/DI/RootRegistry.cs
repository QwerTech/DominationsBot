using WindowsInput;
using AForge.Imaging;
using DominationsBot.Services.GameProcess;
using DominationsBot.Services.ImageProcessing;
using DominationsBot.Services.ImageProcessing.TemplateFinders;
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
                scan.ExcludeType<ExhaustiveTemplateMathingFinder>();
                
            });
            var templateFinders = For<ITemplateFinder>();
            templateFinders.Use<ResizeTemplateFinder>();

            ForConcreteType<TesseractEngine>()
                .Configure.SelectConstructor(() => new TesseractEngine(null, null, default(EngineMode)))
                .Ctor<string>("datapath").Is(@"./tessdata")
                .Ctor<string>("language").Is("eng")
                .Ctor<EngineMode>().Is(EngineMode.Default)
                .Singleton();

            For<ITemplateMatching>()
                .Use<ExhaustiveTemplateMatching>().Ctor<float>().Is(0.8f);
            For<IInputSimulator>().Use<InputSimulator>().SelectConstructor(() => new InputSimulator());

            ForConcreteType<TextReader>().Configure.Ctor<ITemplateFinder>().Is<ExhaustiveTemplateMathingFinder>();

            var workers = For<IWorker>();
            workers.Add<CollectGold>();
            workers.Add<TrainTroops>().Ctor<ICurrentResourcesType>().Is(new ResourcesType(NumberResourcesType.Barracks));
            workers.Add<CollectFood>();
        }
    }

    public interface ISettings
    {
        string SymbolsPath { get; }
    }

    public class Settings : ISettings
    {
        public string SymbolsPath => "Resources/Symbols";
    }
}