using DominationsBot.DI;
using DominationsBot.Services.GameProcess;
using DominationsBot.Tools;

namespace DominationsBot
{
    class Program
    {
        static void Main(string[] args)
        {

            var blueStackWindowHandle = BlueStackHelper.GetBlueStackWindowHandle();

            IoC.Container.GetInstance<CollectGold>().DoWork();


        }
    }
}
