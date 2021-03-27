using ScienceAdviser.IViewModel.Helpers;
using System.Collections.ObjectModel;

namespace ScienceAdviser.IViewModel.Selectors
{
    public interface IDetailGroupSelectorViewModel
    {
        ObservableCollection<string> FoundDetailGroups { get; }

        string RegexFilter { set; }

        string SelectedDetailGroup { get; set; }

        RelayCommand SelectItemCommand { get; set; }

        RelayCommand FindByFilterCommand { get; set; }
    }
}
