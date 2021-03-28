using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

using ScienceAdviser.IViewModel.Helpers;
using ScienceAdviser.IViewModel.Windows;

namespace ScienceAdviser.ViewModel.Windows
{
    public class DirectoryWindowViewModel : IDirectoryWindowViewModel, INotifyPropertyChanged
    {
        private string selectedDirectory = "";
        public string SelectedDirectory
        {
            get => selectedDirectory;
            private set
            {
                if(selectedDirectory != value)
                {
                    selectedDirectory = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string throwedExceptionMeesage = "";
        public string ThrowedExceptionMeesage
        {
            get => throwedExceptionMeesage;
            private set
            {
                if (throwedExceptionMeesage != value)
                {
                    throwedExceptionMeesage = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private bool completionAvailable = false;
        public bool CompletionAvailable
        {
            get => completionAvailable;
            private set
            {
                if (completionAvailable != value)
                {
                    completionAvailable = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand CompleteCommand { get; set; }
        public RelayCommand SelectDirectoryCommand { get; set; }

        private Action _closeCallback;
        private Action<string> _completeCallback;
        Func<string> _getFileWithDialog;

        public DirectoryWindowViewModel(Action closeCallback, Action<string> completeCallback, Func<string> getFileWithDialog)
        {
            _closeCallback = closeCallback;
            _completeCallback = completeCallback;
            _getFileWithDialog = getFileWithDialog;
            InitCommands();
        }

        private void InitCommands()
        {
            CompleteCommand = new RelayCommand((x) =>
            {
                if (CompletionAvailable)
                {
                    _completeCallback.Invoke(SelectedDirectory);
                    _closeCallback.Invoke();
                }
            });

            SelectDirectoryCommand = new RelayCommand((x) =>
            {
                string path = _getFileWithDialog.Invoke();

                SelectedDirectory = path;

                if (CheckFileIsCorrect(path))
                {
                    ThrowedExceptionMeesage = string.Empty;
                    CompletionAvailable = true;
                }
                else
                {
                    ThrowedExceptionMeesage = "File is not exists.";
                    CompletionAvailable = false;
                }
            });
        }

        private bool CheckFileIsCorrect(string path)
        {
            return File.Exists(path);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
