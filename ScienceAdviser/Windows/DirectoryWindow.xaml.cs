using Microsoft.Win32;
using ScienceAdviser.IViewModel.Windows;
using ScienceAdviser.ViewModel.Windows;

using System.Windows;

namespace ScienceAdviser.View.Windows
{
    public partial class DirectoryWindow : Window
    {
        IDirectoryWindowViewModel _dataContext;

        public DirectoryWindow()
        {
            InitializeComponent();

            //Да да, убрать жесткую ссылку... я просто устал. Пока так будет.
            _dataContext = new DirectoryWindowViewModel(Close,
                (fileLocation) =>
                {
                    App.WriteAutofacDependencies(fileLocation);
                    new MainWindow().Show();
                },
                new System.Func<string>(() =>
                {
                    OpenFileDialog opf = new OpenFileDialog();
                    var directorySelected = opf.ShowDialog();
                    if (directorySelected == true)
                        return opf.FileName;
                    else
                        return null;
                }));

            this.DataContext = _dataContext;
        }
    }
}
