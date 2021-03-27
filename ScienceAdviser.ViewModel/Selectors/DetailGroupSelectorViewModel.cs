using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Helpers;
using ScienceAdviser.IViewModel.Selectors;

namespace ScienceAdviser.ViewModel.Selectors
{
    public class DetailGroupSelectorViewModel : IDetailGroupSelectorViewModel, INotifyPropertyChanged
    {
        private IRulesRepository _repository;

        private ObservableCollection<string> foundDetailGroups = new ObservableCollection<string>();
        public ObservableCollection<string> FoundDetailGroups
        {
            get => foundDetailGroups;
            private set
            {
                if (value != foundDetailGroups)
                {
                    foundDetailGroups = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string regexFilter = "";
        public string RegexFilter
        {
            private get => regexFilter;
            set
            {
                if(value != regexFilter)
                {
                    regexFilter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string SelectedDetailGroup
        {
            get;
            set;
        }

        public RelayCommand SelectItemCommand { get; set; }

        public RelayCommand FindByFilterCommand { get; set; }
        RelayCommand IDetailGroupSelectorViewModel.SelectItemCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        RelayCommand IDetailGroupSelectorViewModel.FindByFilterCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DetailGroupSelectorViewModel(IRulesRepository repository, Action close)
        {
            _repository = repository;
            SelectItemCommand = new RelayCommand((x) => close.Invoke());
            FindByFilterCommand = new RelayCommand((x) => UpdateFoundList());
            UpdateFoundList();
        }

        private void UpdateFoundList()
        {
            //Вынести логику выбора по regex в репозиторий!!!
            var enumGroups = _repository.GetAvailableDetailGroups().Where(group => Regex.IsMatch(group, RegexFilter));
            FoundDetailGroups = new ObservableCollection<string>(enumGroups);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
