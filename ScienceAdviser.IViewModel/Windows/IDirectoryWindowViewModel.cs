using ScienceAdviser.IViewModel.Helpers;

namespace ScienceAdviser.IViewModel.Windows
{
    public interface IDirectoryWindowViewModel
    {
        string SelectedDirectory { get; }

        string ThrowedExceptionMeesage { get; }

        bool CompletionAvailable { get; }

        RelayCommand CompleteCommand { get; set; }

        RelayCommand SelectDirectoryCommand { get; set; }
    }
}
