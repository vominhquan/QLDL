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
using BUS;
namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for EditNhanVien.xaml
    /// </summary>
    public partial class EditNhanVien : Window
    {
        public EditNhanVien(vwCHUCVU_NHANVIEN_TAIKHOAN vw)
        {
            InitializeComponent();
            cbChucvu.ItemsSource = BUSView.Instance.GetAllCV();
            Load(vw);
        }

        public void Load(vwCHUCVU_NHANVIEN_TAIKHOAN vw)
        {
            txbMa.Text = vw.MANV.ToString();
            txbTen.Text = vw.TENNV;
            txbDiachi.Text = vw.DIACHI;
            dpNgayinh.SelectedDate = vw.NGAYSINH;
            cbChucvu.SelectedValue = vw.MACHUCVU;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if(txbTen.Text == "" || txbDiachi.Text == "" || cbChucvu.SelectedItem == null || dpNgayinh.SelectedDate == null)
            {
                MessageBox.Show("Không thể để giá trị trống");
            }
            else { 
                MessageBoxResult result = MessageBox.Show("Bạn muốn sửa nhân viên đã chọn?", "Xác nhận sửa", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                    if (BUSQLNhanVien.Instance.Update(Int32.Parse(txbMa.Text),txbTen.Text,dpNgayinh.SelectedDate.Value,txbDiachi.Text,(int)cbChucvu.SelectedValue))
                        MessageBox.Show("Đã sửa thành công");
                    else
                        MessageBox.Show("Có lỗi xảy ra");
            }
        }
    }
}
