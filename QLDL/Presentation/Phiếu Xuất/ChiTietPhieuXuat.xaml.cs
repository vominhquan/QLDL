using QLDL.BusinessLogic;
using QLDL.Class;
using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for ChiTietPhieuXuat.xaml
    /// </summary>
    public partial class ChiTietPhieuXuat : Window
    {
        public ChiTietPhieuXuat(vw_PhieuXuat_NhanVien_DaiLy View)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                ChiTietPhieuXuat = View,
                DanhSachChiTietPhieuXuat = (new PhieuXuatBUS()).GetCTPXPhieuXuatByMaPhieu(View.MAPHIEU)
            };
        }
        private class State : INotifyPropertyChanged
        {
            #region Init INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            #endregion

            #region (ObservableCollection) Chi tiết phiếu xuất
            private vw_PhieuXuat_NhanVien_DaiLy chiTietPhieuXuat;

            public vw_PhieuXuat_NhanVien_DaiLy ChiTietPhieuXuat
            {
                get => chiTietPhieuXuat;
                set => chiTietPhieuXuat = value;
            }
            #endregion

            #region (ObservableCollection) Danh sách chi tiết phiếu xuất
            private ObservableCollection<vw_PhieuXuat_CTPX_MatHang> danhSachChiTietPhieuXuat;
            public ObservableCollection<vw_PhieuXuat_CTPX_MatHang> DanhSachChiTietPhieuXuat {
                get => danhSachChiTietPhieuXuat;
                set => danhSachChiTietPhieuXuat = value;
            }
            #endregion
        };

        #region Quay lại
        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion
    }
}
