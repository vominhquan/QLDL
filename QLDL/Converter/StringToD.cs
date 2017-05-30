using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Applications.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class DecimalToD : IValueConverter
    {
        public object Convert(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture
        )
        {
            if (value == null || !(value is Decimal))
            {
                return "";
            };
            return ((decimal)value).ToString("#,##0 đ", CultureInfo.GetCultureInfo("vi-VN"));
        }
        public object ConvertBack(
            object value, 
            Type targetType, 
            object parameter, 
            System.Globalization.CultureInfo culture
        )
        {
            string OnlyNumberString = (new Regex(@"[^\d]")).Replace(
                value.ToString(),
                String.Empty
            );
            if(decimal.TryParse((string)OnlyNumberString, out decimal NumberString))
            {
                return NumberString;
            }
            return 0;
        }
    }
}
