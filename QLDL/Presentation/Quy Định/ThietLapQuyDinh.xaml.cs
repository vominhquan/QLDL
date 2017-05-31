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

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for ThietLapQuyDinh.xaml
    /// </summary>
    public partial class ThietLapQuyDinh : Window
    {
        public ThietLapQuyDinh()
        {
            InitializeComponent();
        }

        private void DanhSachLoaiDL(object sender, RoutedEventArgs e)
        {
            new DanhSachLoaiDL().ShowDialog();
        }

        private void DanhSachQuan(object sender, RoutedEventArgs e)
        {
            new DanhSachQuan().ShowDialog();
        }

        private void DanhSachMatHang(object sender, RoutedEventArgs e)
        {
            new DanhSachMatHang().ShowDialog();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
