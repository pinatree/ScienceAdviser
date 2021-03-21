using ScienceAdviser.Helpers;
using ScienceAdviser.Model.Repositories;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ScienceAdviser.ViewModel.Selectors
{
    public class DetailSubgroupSelectorViewModel : IDetailSubgroupSelectorViewModel, INotifyPropertyChanged
    {
        private RulesForDetailRepository _repository;

        private ObservableCollection<string> foundDetailSubgroups = new ObservableCollection<string>();
        public ObservableCollection<string> FoundDetailSubgroups
        {
            get => foundDetailSubgroups;
            set
            {
                if (value != foundDetailSubgroups)
                {
                    foundDetailSubgroups = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string regexFilter = "";
        public string RegexFilter
        {
            get => regexFilter;
            set
            {
                if(value != regexFilter)
                {
                    regexFilter = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string SelectedDetailGroup { get; set; }

        public string SelectedDetailSubgroup { get; set; }

        public RelayCommand SelectItemCommand { get; set; }

        public RelayCommand FindByFilterCommand { get; set; }


        public DetailSubgroupSelectorViewModel(RulesForDetailRepository repository, string group, Action close)
        {
            _repository = repository;
            SelectItemCommand = new RelayCommand((x) => close.Invoke());
            FindByFilterCommand = new RelayCommand((x) => UpdateFoundList());
            SelectedDetailGroup = group;
            UpdateFoundList();
        }

        private void UpdateFoundList()
        {
            //Вынести логику выбора по regex в репозиторий!!!
            var enumGroups = _repository.GetAvailableDetailSubgroups(SelectedDetailGroup).Where(group => Regex.IsMatch(group, RegexFilter));
            FoundDetailSubgroups = new ObservableCollection<string>(enumGroups);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
