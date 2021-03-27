using ScienceAdviser.IModel.DataTypes;
using ScienceAdviser.IViewModel.Helpers;
using System.Collections.ObjectModel;

namespace ScienceAdviser.IViewModel.Windows
{
    public interface IMainWindowViewModel
    {
        string SelectedDetailGroup { get; }
        string SelectedDetailSubgroup { get; }
        string SelectedDetail { get; }

        ObservableCollection<RuleWithDetail> Recommendations { get; }
        ObservableCollection<DetailDefect> FoundDefectDetails { get; }

        RelayCommand FindDetailGroupCommand { get; set; }
        RelayCommand FindDetailSubgroupCommand { get; set; }
        RelayCommand FindDetailCommand { get; set; }
        RelayCommand AddToFoundListCommand { get; set; }
    }
}
