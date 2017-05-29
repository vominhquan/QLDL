using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;

namespace Applications.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class DateTimeToDate : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture
        )
        {
            if (value == null || !(value is DateTime)) {
                throw new NotImplementedException("Kiểu dữ liệu khi sử dụng Converter không đúng");
            };
            return ((DateTime)value).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture
        )
        {
            throw new NotImplementedException("Không cho phép convert nguược");
        }
    }
}
