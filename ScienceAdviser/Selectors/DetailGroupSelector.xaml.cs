using ScienceAdviser.IModel.Repositories;
using ScienceAdviser.IViewModel.Selectors;
using ScienceAdviser.ViewModel.Selectors;
using System.Windows;

namespace ScienceAdviser.View.Selectors
{
    public partial class DetailGroupSelector : Window
    {
        private IDetailGroupSelectorViewModel _dataContext;

        public string FoundDetailGroup
        {
            get => _dataContext.SelectedDetailGroup;
        }

        public DetailGroupSelector(IRulesRepository repository)
        {
            InitializeComponent();

            _dataContext = new DetailGroupSelectorViewModel(repository, Close);
            this.DataContext = _dataContext;
        }
    }
}
