using QLDL.BusinessLogic;
using QLDL.DataAccess;
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
    /// Interaction logic for PhieuThu.xaml
    /// </summary>
    public partial class PhieuThu : Window
    {
        public vwDAILY_LOAIDL_QUAN vw { get; set; }
        private PhieuThuBUS ptbus = new PhieuThuBUS();

        public PhieuThu(vwDAILY_LOAIDL_QUAN vw)
        {
            InitializeComponent();
            this.vw = vw;
        }

        private void ThemPhieu(object sender, RoutedEventArgs e)
        {
             MessageBoxResult result = MessageBox.Show("Khi đã tạo không thể thay đổi", "Xác nhận", MessageBoxButton.YesNo);

             if (result == MessageBoxResult.Yes)
             {
                 PHIEUTHUTIEN pt = new PHIEUTHUTIEN();
                 pt.MADL = vw.MADL;
                 pt.NGAYTHUTIEN = DateTime.Now;
                 pt.NGUOITHU = 1;
                 pt.SOTIEN = Decimal.Parse(txtSoTien.Text);
                 if (ptbus.insertPhieuThu(pt))
                 {
                     MessageBox.Show("Đã thêm thành công");
                     vw.SONO -= pt.SOTIEN;
                     this.DialogResult = true;
                 }
                 else
                     MessageBox.Show("Có lỗi xảy ra");
             }
        }

    }
}
