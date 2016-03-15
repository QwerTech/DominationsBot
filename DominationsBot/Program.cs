using Castle.Core.Internal;
using DominationsBot.DI;
using DominationsBot.Services;
using DominationsBot.Services.GameProcess;

namespace DominationsBot
{
    class Program
    {
        static void Main(string[] args)
        {
            IoC.Container.GetInstance<SelfDiagnostics>().Check();
            IoC.Container.GetAllInstances<IWorker>().ForEach(w =>
            {
                w.DoWork();
            });
        }
    }
}
