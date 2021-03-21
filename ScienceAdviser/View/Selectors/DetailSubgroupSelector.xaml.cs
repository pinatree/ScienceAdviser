using ScienceAdviser.Model.Repositories;
using ScienceAdviser.ViewModel.Selectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScienceAdviser.View.Selectors
{
    public partial class DetailSubgroupSelector : Window
    {
        private IDetailSubgroupSelectorViewModel _dataContext;

        public string FoundDetailSubgroup
        {
            get => _dataContext.SelectedDetailSubgroup;
        }

        public DetailSubgroupSelector(RulesForDetailRepository repository, string group)
        {
            InitializeComponent();

            _dataContext = new DetailSubgroupSelectorViewModel(repository, group, Close);
            this.DataContext = _dataContext;
        }
    }
}
