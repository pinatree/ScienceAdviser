using ScienceAdviser.IViewModel.Helpers;
using System.Collections.ObjectModel;

namespace ScienceAdviser.IViewModel.Selectors
{
    public interface IDetailSelectorViewModel
    {
        ObservableCollection<string> FoundDetails { get; }

        string RegexFilter { set; }

        string SelectedDetailGroup { get; }
        string SelectedDetailSubgroup { get; }
        string SelectedDetail { get; set; }

        RelayCommand SelectItemCommand { get; set; }

        RelayCommand FindByFilterCommand { get; set; }
    }
}
