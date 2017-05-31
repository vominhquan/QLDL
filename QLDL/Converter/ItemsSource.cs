using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Data;
using System.Globalization;
using QLDL.DataAccess;
using System.Collections.ObjectModel;
using QLDL.BusinessLogic;

namespace Applications.Converter
{
    //[ValueConversion(typeof(double), typeof(double))]
    public class ItemsSource : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // 0: self                      : CTPX
            // 1: DanhSachChiTietPhieuXuat  : ObservableCollection<CTPX>
            // 2: MatHang                   : ObservableCollection<MATHANG>
            
            foreach (CTPX ct in (values[1] as ObservableCollection<CTPX>).ToArray())
            {
                MATHANG mh = (values[2] as ObservableCollection<MATHANG>)
                    .ToList().Find(x => 
                        (
                            x.MAHANG == ct.MAHANG && 
                            (
                                values[0] == null || 
                                x.MAHANG != (values[0] as CTPX).MAHANG
                            )
                        )
                    );
                if (mh != null)                                
                {
                    (values[2] as ObservableCollection<MATHANG>).Remove(mh);
                }
            }
            return values[2];
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
