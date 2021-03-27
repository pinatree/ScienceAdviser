using ScienceAdviser.IViewModel.Windows;
using System.Windows;

namespace ScienceAdviser
{
    public partial class MainWindow : Window
    {
        IMainWindowViewModel _datacontext;

        public MainWindow()
        {
            InitializeComponent();

            _datacontext = DI.DIContainer.GetInstance<IMainWindowViewModel>();
            this.DataContext = _datacontext;
        }
    }
}
