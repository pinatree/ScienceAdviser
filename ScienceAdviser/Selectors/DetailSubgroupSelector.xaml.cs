using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Selectors;
using ScienceAdviser.ViewModel.Selectors;
using System.Windows;

namespace ScienceAdviser.View.Selectors
{
    public partial class DetailSubgroupSelector : Window
    {
        private IDetailSubgroupSelectorViewModel _dataContext;

        public string FoundDetailSubgroup
        {
            get => _dataContext.SelectedDetailSubgroup;
        }

        public DetailSubgroupSelector(IRulesRepository repository, string group)
        {
            InitializeComponent();

            _dataContext = new DetailSubgroupSelectorViewModel(repository, group, Close);
            this.DataContext = _dataContext;
        }
    }
}
