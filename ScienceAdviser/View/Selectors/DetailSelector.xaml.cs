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
    public partial class DetailSelector : Window
    {
        private IDetailSelectorViewModel _dataContext;

        public string FoundDetail
        {
            get => _dataContext.SelectedDetail;
        }

        public DetailSelector(RulesForDetailRepository repository, string group, string subgroup)
        {
            InitializeComponent();

            _dataContext = new DetailSelectorViewModel(repository, group, subgroup, Close);
            this.DataContext = _dataContext;
        }
    }
}
