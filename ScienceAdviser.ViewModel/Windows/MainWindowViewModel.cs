using System;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using ScienceAdviser.IModel.DataTypes;
using ScienceAdviser.IViewModel.Helpers;
using ScienceAdviser.IViewModel.Windows;
using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Callers;

namespace ScienceAdviser.ViewModel.Windows
{
    public class MainWindowViewModel : IMainWindowViewModel, INotifyPropertyChanged
    {
        private string selectedDetailGroup;
        private string selectedDetailSubgroup;
        private string selectedDetail;

        public string SelectedDetailGroup
        {
            get => selectedDetailGroup;
            private set
            {
                if(selectedDetailGroup != value)
                {
                    selectedDetailGroup = value;
                    NotifyPropertyChanged();

                    SelectedDetailSubgroup = "";
                    SelectedDetail = "";
                }
            }
        }

        public string SelectedDetailSubgroup
        {
            get => selectedDetailSubgroup;
            private set
            {
                if (selectedDetailSubgroup != value)
                {
                    selectedDetailSubgroup = value;
                    NotifyPropertyChanged();

                    SelectedDetail = "";
                }
            }
        }

        public string SelectedDetail
        {
            get => selectedDetail;
            private set
            {
                if (selectedDetail != value)
                {
                    selectedDetail = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<RuleWithDetail> recommendations = new ObservableCollection<RuleWithDetail>();
        public ObservableCollection<RuleWithDetail> Recommendations
        {
            get => recommendations;
            private set
            {
                if(value != recommendations)
                {
                    recommendations = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private ObservableCollection<DetailDefect> foundDefectDetails = new ObservableCollection<DetailDefect>();
        public ObservableCollection<DetailDefect> FoundDefectDetails
        {
            get => foundDefectDetails;
            private set
            {
                if (value != foundDefectDetails)
                {
                    foundDefectDetails = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public RelayCommand FindDetailGroupCommand { get; set; }
        public RelayCommand FindDetailSubgroupCommand { get; set; }
        public RelayCommand FindDetailCommand { get; set; }
        public RelayCommand AddToFoundListCommand { get; set; }
        public RelayCommand RemoveDetailCommand { get; set; }

        private IRulesRepository _repository;
        private IDetailGroupSelector _detailGroupSelector;
        private IDetailSubgroupSelector _detailSubgroupSelector;
        private IDetailSelector _detailSelector;

        public MainWindowViewModel(IRulesRepository repository, IDetailGroupSelector detailGroupSelector,
            IDetailSubgroupSelector detailSubgroupSelector, IDetailSelector detailSelector)
        {
            _repository = repository;
            _detailGroupSelector = detailGroupSelector;
            _detailSubgroupSelector = detailSubgroupSelector;
            _detailSelector = detailSelector;

            InitCommands();

            SelectedDetailGroup = repository.GetAvailableDetailGroups().First();
            SelectedDetailSubgroup = repository.GetAvailableDetailSubgroups(SelectedDetailGroup).First();
            SelectedDetail = repository.GetAvailableDetails(SelectedDetailGroup, SelectedDetailSubgroup).First();
        }

        private void FindDetailGroup()
        {
            string callResult = _detailGroupSelector.SelectDetailGroup(_repository);

            if (callResult != null && callResult != "")
            {
                SelectedDetailGroup = callResult;
            }
        }

        private void FindDetailSubgroup()
        {
            string callResult = _detailSubgroupSelector.SelectDetailSubgroup(_repository, SelectedDetailGroup);

            if (callResult != null && callResult != "")
            {
                SelectedDetailSubgroup = callResult;
            }
        }

        private void FindDetail()
        {
            string callResult = _detailSelector.SelectDetailGroup(_repository, SelectedDetailGroup,
                SelectedDetailSubgroup);

            if (callResult != null && callResult != "")
            {
                SelectedDetail = callResult;
            }
        }

        private void AddToList()
        {
            DetailDefect newDefect = new DetailDefect()
            {
                Detail = SelectedDetail,
                DetailSubGroup = SelectedDetailSubgroup,
                DetailGroup = SelectedDetailGroup
            };

            var exists = FoundDefectDetails.Any(defect =>
                defect.Detail == newDefect.Detail &&
                defect.DetailGroup == newDefect.DetailGroup &&
                defect.DetailSubGroup == newDefect.DetailSubGroup);

            if (!exists)
            {
                FoundDefectDetails.Add(newDefect);
                UpdateRecomendations();
            }
        }

        private void UpdateRecomendations()
        {
            var newRecommendations = new ObservableCollection<RuleWithDetail>();

            foreach (DetailDefect item in FoundDefectDetails)
            {
                IEnumerable<RuleWithDetail> associations = _repository.GetAssociations(item);

                foreach (var rule in associations)
                {
                    if (newRecommendations.Contains(rule))
                        continue;
                    else
                        newRecommendations.Add(rule);
                }
            }

            newRecommendations = new ObservableCollection<RuleWithDetail>(newRecommendations.GroupBy(c => (c.Consequence.Detail + c.Consequence.DetailGroup + c.Consequence.DetailSubGroup), (key, c) => c.FirstOrDefault()));

            Recommendations = new ObservableCollection<RuleWithDetail>(newRecommendations.OrderByDescending(recommendation => recommendation.Reliability));
        }

        private void InitCommands()
        {
            FindDetailGroupCommand = new RelayCommand((x) => FindDetailGroup());
            FindDetailSubgroupCommand = new RelayCommand((x) => FindDetailSubgroup());
            FindDetailCommand = new RelayCommand((x) => FindDetail());
            AddToFoundListCommand = new RelayCommand((x) => AddToList());
            RemoveDetailCommand = new RelayCommand((detail) =>
            {
                DetailDefect searchDetail = (DetailDefect)detail;

                if (searchDetail == null)
                    return;

                var found = FoundDefectDetails.FirstOrDefault(det =>
                    det.Detail == searchDetail.Detail &&
                    det.DetailSubGroup == searchDetail.DetailSubGroup &&
                    det.DetailGroup == searchDetail.DetailGroup);

                if(found != null)
                {
                    FoundDefectDetails.Remove(found);
                    UpdateRecomendations();
                }
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }    
}
