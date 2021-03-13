using ScienceAdviser.Helpers;
using ScienceAdviser.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autofac;
using System.Windows;
using ScienceAdviser.View.Selectors;

namespace ScienceAdviser.ViewModel
{
    public class MainWindowViewModel
    {
        public DetailGroup SelectedDetailGroup { get; set; }
        public DetailSubgroup SelectedDetailSubgroup { get; set; }
        public Detail SelectedDetail { get; set; }
        public List<Association> AssociationsList { get; set; }

        public DateTime LastDbUpdate { get; set; } = DateTime.Now;

        public RelayCommand FindDetailGroupCommand { get; set; }
        public RelayCommand FindDetailSubgroupCommand { get; set; }
        public RelayCommand FindDetailCommand { get; set; }

        public MainWindowViewModel()
        {
            SelectedDetailGroup = new DetailGroup()
            {
                Id = 5,
                Name = "Корпус чего то там 3.14"
            };

            SelectedDetailSubgroup = new DetailSubgroup()
            {
                Name = "Какой то бак"
            };

            SelectedDetail = new Detail()
            {
                Name = "Муфта"
            };

            FindDetailGroupCommand = new RelayCommand((x) => FindDetailGroup());
            FindDetailSubgroupCommand = new RelayCommand((x) => FindDetailSubgroup());
            FindDetailCommand = new RelayCommand((x) => FindDetail());
        }


        private void FindDetailGroup()
        {
            DetailGroupSelector selector = new DetailGroupSelector();
            selector.ShowDialog();
            if(selector.FoundDetailGroup != null)
            {
                SelectedDetailGroup = selector.FoundDetailGroup;
            }
        }

        private void FindDetailSubgroup()
        {
            throw new NotImplementedException();
        }

        private void FindDetail()
        {
            throw new NotImplementedException();
        }
    }
}
