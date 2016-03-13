using StructureMap;

namespace DominationsBot.DI
{
    public class IoC
    {
        public static IContainer Container { get; set; }=new Container(new RootRegistry());
    }
}
