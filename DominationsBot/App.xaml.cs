using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Castle.Core.Internal;
using DominationsBot.DI;
using DominationsBot.Services;
using DominationsBot.Services.GameProcess;
using DominationsBot.UI.MainView;

namespace DominationsBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //IoC.Container.GetInstance<SelfDiagnostics>().Check();
            //IoC.Container.GetAllInstances<IWorker>().ForEach(w =>
            //{
            //    w.DoWork();
            //});
            base.OnStartup(e);
            IoC.Container.GetInstance<MainWindow>().Show();
        }
    }
}
