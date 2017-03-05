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
using QLDL.BusinessLogic;
using QLDL.DataAccess;
using System.Collections.ObjectModel;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for DaiLy.xaml
    /// </summary>
    /// 

    public partial class DaiLy : Window
    {
        private DaiLyBUS dlbus = new DaiLyBUS();

        public DaiLy()
        {
            InitializeComponent();

            // Lấy dữ liệu ban đầu
            lsvDL.ItemsSource = dlbus.getAllDaiLy();
            cbbLoaiDL.ItemsSource = dlbus.getAllLoaiDL();
            cbbLoaiDL.DisplayMemberPath = "TENLOAI";
            cbbLoaiDL.SelectedValuePath = "MALOAI";
            cbbQuan.ItemsSource = dlbus.getAllQuan();
            cbbQuan.DisplayMemberPath = "TENQUAN";
            cbbQuan.SelectedValuePath = "MAQUAN";
            
        }

        private void AddDL(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(cbbQuan.SelectedValue.ToString());
            DAILY dl = new DAILY
            {
                MADL=2,
                TENDL = txtTenDL.Text,
                DIACHI = txtDiaChi.Text,
                DIENTHOAI = txtDienThoai.Text,
                NGAYTIEPNHAN = dpNgayTiepNhan.SelectedDate,
                MAQUAN = Int32.Parse(cbbQuan.SelectedValue.ToString()),
                LOAIDL = Int32.Parse(cbbLoaiDL.SelectedValue.ToString()),
                TINHTRANG = 1
            };
            if (dlbus.insertDaiLy(dl))
                MessageBox.Show("Đã thêm thành công");
            else
                MessageBox.Show("Có lỗi xảy ra");
        }
    }
}
