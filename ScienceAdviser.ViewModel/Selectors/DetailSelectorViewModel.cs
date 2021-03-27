using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Helpers;
using ScienceAdviser.IViewModel.Selectors;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ScienceAdviser.ViewModel.Selectors
{
    public class DetailSelectorViewModel : IDetailSelectorViewModel, INotifyPropertyChanged
    {
        private IRulesRepository _repository;

        private ObservableCollection<string> foundDetails = new ObservableCollection<string>();
        public ObservableCollection<string> FoundDetails
        {
            get => foundDetails;
            private set
            {
                if (value != foundDetails)
                {
                    foundDetails = value;
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

        public string SelectedDetailGroup { get; }
        public string SelectedDetailSubgroup { get; }

        public string SelectedDetail { get; set; }

        public RelayCommand SelectItemCommand { get; set; }

        public RelayCommand FindByFilterCommand { get; set; }
        RelayCommand IDetailSelectorViewModel.SelectItemCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        RelayCommand IDetailSelectorViewModel.FindByFilterCommand { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DetailSelectorViewModel(IRulesRepository repository, string group, string subgroup, Action close)
        {
            _repository = repository;
            SelectItemCommand = new RelayCommand(() => close.Invoke());
            FindByFilterCommand = new RelayCommand(() => UpdateFoundList());
            SelectedDetailGroup = group;
            SelectedDetailSubgroup = subgroup;
            UpdateFoundList();
        }

        private void UpdateFoundList()
        {
            //Вынести логику выбора по regex в репозиторий!!!
            var enumGroups = _repository.GetAvailableDetails(SelectedDetailGroup, SelectedDetailSubgroup).Where(group => Regex.IsMatch(group, RegexFilter));
            FoundDetails = new ObservableCollection<string>(enumGroups);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
