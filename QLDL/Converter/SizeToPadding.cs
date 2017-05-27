using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Applications.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class SizeToPadding : IValueConverter
    {
        public object Convert
            (
                object value, 
                Type targetType, 
                object parameter, 
                System.Globalization.CultureInfo culture
            )
        {
            if (value == null || !(value is double)) {
                throw new NotImplementedException();
            };
            return (double)value * 0.3;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
