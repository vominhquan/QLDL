using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;

namespace QLDL.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class ColorToBrush : IValueConverter
    {
        public object Convert
            (
                object value, 
                Type targetType, 
                object parameter, 
                System.Globalization.CultureInfo culture
            )
        {
            if (value == null || !(value is Color)) {
                throw new NotImplementedException("Kiểu dữ liệu khi sử dụng Converter không đúng");
            };
            return new SolidColorBrush((Color)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
