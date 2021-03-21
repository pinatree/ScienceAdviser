using ScienceAdviser.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAdviser.ViewModel.Selectors
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
