using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;

namespace Applications.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class DoubleToPercent : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture
        )
        {
            if (value == null || !(value is double)) {
                throw new NotImplementedException("Kiểu dữ liệu khi sử dụng Converter không đúng");
            };
            return ((double)value) * 100;
        }
        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture
        )
        {
            if(double.TryParse((string)value, out double number))
            {
                return number / 100;
            }
            else
            {
                throw new NotImplementedException("Không thể ép kiểu về double");
            }
        }
    }
}
