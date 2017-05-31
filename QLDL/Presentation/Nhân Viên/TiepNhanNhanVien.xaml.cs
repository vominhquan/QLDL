using QLDL.BusinessLogic;
using QLDL.Class;
using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TiepNhanNhanVien.xaml
    /// </summary>
    public partial class TiepNhanNhanVien : Window
    {
        public int? ReturnValue = null;
        public TiepNhanNhanVien()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                NhanVien = new NHANVIEN(),
                TaiKhoan = new TAIKHOAN()
            };
        }
        private class State
        {
            #region Nhân viên
            private NHANVIEN nhanVien;
            public NHANVIEN NhanVien { get => nhanVien; set => nhanVien = value; }
            #endregion

            #region Tài khoản
            private TAIKHOAN taiKhoan;
            public TAIKHOAN TaiKhoan { get => taiKhoan; set => taiKhoan = value; }
            #endregion

            #region List
            public ObservableCollection<CHUCVU> ChucVu
            {
                get => new NhanVienBUS().GetALLChucVu();
            }
            #endregion
        }

        private void Add(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Bạn muốn thêm nhân viên?", "Xác nhận thêm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                int? MANV = new NhanVienBUS().AddNhanVien_TaiKhoan(
                    (DataContext as State).NhanVien, (DataContext as State).TaiKhoan
                );
                if (MANV != null)
                {
                    MessageBox.Show("Thêm thành công");
                    ReturnValue = MANV;
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
