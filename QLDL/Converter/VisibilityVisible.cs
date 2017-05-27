using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Applications.Converter
{
    public class VisibilityVisible : IValueConverter
    {
        public object Convert
            (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is string))
            {
                throw new NotImplementedException();
            };
            return (string)value != "" ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
