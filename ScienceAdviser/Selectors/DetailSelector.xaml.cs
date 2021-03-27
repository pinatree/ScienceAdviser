using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Selectors;
using ScienceAdviser.ViewModel.Selectors;
using System.Windows;

namespace ScienceAdviser.View.Selectors
{
    public partial class DetailSelector : Window
    {
        private IDetailSelectorViewModel _dataContext;

        public string FoundDetail
        {
            get => _dataContext.SelectedDetail;
        }

        public DetailSelector(IRulesRepository repository, string group, string subgroup)
        {
            InitializeComponent();

            _dataContext = new DetailSelectorViewModel(repository, group, subgroup, Close);
            this.DataContext = _dataContext;
        }
    }
}
