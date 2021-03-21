using ScienceAdviser.Helpers;
using System;
using System.Linq;

using Autofac;
using System.Windows;
using ScienceAdviser.View.Selectors;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ScienceAdviser.Model.Repositories;
using System.Collections.ObjectModel;
using ScienceAdviser.Model.DataReaders.Excel.WithDetail.DataTypes;
using System.Collections.Generic;
using Microsoft.Win32;

namespace ScienceAdviser.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string selectedDetailGroup;
        private string selectedDetailSubgroup;
        private string selectedDetail;

        public string SelectedDetailGroup
        {
            get => selectedDetailGroup;
            set
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
            set
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
            set
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
            set
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
            set
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

        private RulesForDetailRepository _repository;

        public MainWindowViewModel()
        {
            string dev_default_location = @"C:\Users\pinat\Desktop\Анализ дефектов\RULES_WITH_DETAILS.xlsx";

            InitCommands();

            MessageBox.Show("Выберите файл excel, из которого будет осуществляться выборка данных.");

            OpenFileDialog opf = new OpenFileDialog();
            var dialogResult = opf.ShowDialog();
            if(dialogResult == true)
            {
                _repository = new RulesForDetailRepository(opf.FileName, true, "-!$-");
            }
            else
            {
                MessageBox.Show("Увы, вам нужно было выбрать файл. Без него программа работать не будет.");
                Application.Current.Shutdown();
                return;
            }


            try
            {

                SelectedDetailGroup = _repository.GetAvailableDetailGroups().First();
                SelectedDetailSubgroup = _repository.GetAvailableDetailSubgroups(SelectedDetailGroup).First();
                SelectedDetail = _repository.GetAvailableDetails(SelectedDetailGroup, SelectedDetailSubgroup).First();
            }
            catch
            {
                MessageBox.Show("Указанный файл Excel либо пустой, либо заполнен некорректно. Выполнение анализа невозможно.");
                Application.Current.Shutdown();
            }
        }

        private void FindDetailGroup()
        {
            var kek = new DetailGroupSelector(_repository);
            kek.ShowDialog();
            if(kek.FoundDetailGroup != null && kek.FoundDetailGroup != "")
            {
                SelectedDetailGroup = kek.FoundDetailGroup;
            }
        }

        private void FindDetailSubgroup()
        {
            var kek = new DetailSubgroupSelector(_repository, SelectedDetailGroup);
            kek.ShowDialog();
            if (kek.FoundDetailSubgroup != null && kek.FoundDetailSubgroup != "")
            {
                SelectedDetailSubgroup = kek.FoundDetailSubgroup;
            }
        }

        private void FindDetail()
        {
            var kek = new DetailSelector(_repository, SelectedDetailGroup, SelectedDetailSubgroup);
            kek.ShowDialog();
            if (kek.FoundDetail != null && kek.FoundDetail != "")
            {
                SelectedDetail = kek.FoundDetail;
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

            if (exists)
                MessageBox.Show("Такое повреждение уже выбрано");
            else
            {
                FoundDefectDetails.Add(newDefect);
                UpdateRecomendations();
            }
        }

        private void UpdateRecomendations()
        {
            foreach(DetailDefect item in FoundDefectDetails)
            {
                List<RuleWithDetail> associations =_repository.GetAssociations(item);

                foreach(var rule in associations)
                {
                    if (Recommendations.Contains(rule))
                        continue;
                    else
                        Recommendations.Add(rule);
                }
            }
        }

        private void InitCommands()
        {
            FindDetailGroupCommand = new RelayCommand((x) => FindDetailGroup());
            FindDetailSubgroupCommand = new RelayCommand((x) => FindDetailSubgroup());
            FindDetailCommand = new RelayCommand((x) => FindDetail());
            AddToFoundListCommand = new RelayCommand((x) => AddToList());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
