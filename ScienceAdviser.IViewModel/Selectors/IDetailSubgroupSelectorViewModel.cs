using ScienceAdviser.IViewModel.Helpers;
using System.Collections.ObjectModel;

namespace ScienceAdviser.IViewModel.Selectors
{
    public interface IDetailSubgroupSelectorViewModel
    {
        ObservableCollection<string> FoundDetailSubgroups { get; }

        string RegexFilter { set; }

        string SelectedDetailGroup { get; }
        string SelectedDetailSubgroup { get; }

        RelayCommand SelectItemCommand { get; set; }

        RelayCommand FindByFilterCommand { get; set; }
    }
}
