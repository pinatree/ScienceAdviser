using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Globalization;
using System.Windows.Data;

using System.Windows;
using System.Windows.Media;

namespace ScienceAdviser.Converters
{
    [ValueConversion(typeof(string), typeof(bool))]
    public class NullOrEmpryStrToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
               System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException("The target must be a boolean");

            if (value == null)
                return false;

            string converted = (string)value;

            if (converted == string.Empty)
                return false;

            return true;            
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
