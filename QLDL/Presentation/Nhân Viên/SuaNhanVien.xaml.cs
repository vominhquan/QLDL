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
using System.Collections.ObjectModel;
using QLDL.Class;
using QLDL.BusinessLogic;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for EditNhanVien.xaml
    /// </summary>
    public partial class SuaNhanVien : Window
    {
        public SuaNhanVien(vwCHUCVU_NHANVIEN_TAIKHOAN View)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                NhanVien = View
                // DaiLy = View
            };
        }
        private class State
        {
            #region Nhân viên
            private vwCHUCVU_NHANVIEN_TAIKHOAN nhanVien;
            public vwCHUCVU_NHANVIEN_TAIKHOAN NhanVien { get => nhanVien; set => nhanVien = value; }
            #endregion

            #region List
            public ObservableCollection<CHUCVU> ChucVu
            {
                get => new NhanVienBUS().GetALLChucVu();
            }
            #endregion
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn sửa nhân viên đã chọn?", "Xác nhận sửa", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
                // if (BUSQLNhanVien.Instance.Update(Int32.Parse(txbMa.Text), txbTen.Text, dpNgayinh.SelectedDate.Value, txbDiachi.Text, (int)cbChucvu.SelectedValue))
                if(new NhanVienBUS().UpdateNhanVien((DataContext as State).NhanVien))
                {
                    MessageBox.Show("Đã sửa thành công");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
