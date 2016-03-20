using System.Windows;

namespace DominationsBot.UI.MainView
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel viewModel)
        {
            DataContext = viewModel;
            this.InitializeComponent();
            
        }
    }
}
