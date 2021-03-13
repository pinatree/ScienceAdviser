using ScienceAdviser.Model.DataTypes;
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
    /// <summary>
    /// Interaction logic for DetailGroupSelector.xaml
    /// </summary>
    public partial class DetailGroupSelector : Window
    {
        public DetailGroup FoundDetailGroup
        {
            get => _dataContext.DetailGroup;
        }

        private DetailGroupSelectorViewModel _dataContext;
        public DetailGroupSelector()
        {
            InitializeComponent();

            _dataContext = new DetailGroupSelectorViewModel();
        }
    }
}
