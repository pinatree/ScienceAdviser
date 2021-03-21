using ScienceAdviser.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAdviser.ViewModel.Selectors
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
