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
    /// Interaction logic for PhieuXuat.xaml
    /// </summary>
    /// 


    public partial class PhieuXuat : Window
    {
        public PhieuXuat(vwDAILY_LOAIDL_QUAN View)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                PhieuXuatHang = new PHIEUXUATHANG()
                {
                    NGUOIXUAT = 1,
                    MADL = View.MADL,
                    NGAYLAP = DateTime.Now,
                    TONGTIEN = 0,
                    SOTIENTRA = 0,
                    CONLAI = 0
                },
                DanhSachChiTietPhieuXuat = new ObservableCollection<CTPX>()
            };
        }
        public class State : INotifyPropertyChanged
        {
            #region Init INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            #endregion

            #region (ObservableCollection) Chi tiết phiếu xuất
            private PHIEUXUATHANG phieuXuatHang;

            public PHIEUXUATHANG PhieuXuatHang
            {
                get => phieuXuatHang;
                set => phieuXuatHang = value;
            }
            #endregion

            #region (ObservableCollection) Danh sách chi tiết phiếu xuất
            private ObservableCollection<CTPX> danhSachChiTietPhieuXuat;
            public ObservableCollection<CTPX> DanhSachChiTietPhieuXuat
            {
                get => danhSachChiTietPhieuXuat;
                set => danhSachChiTietPhieuXuat = value;
            }
            #endregion

            public void Update()
            {
                PhieuXuatHang.TONGTIEN = 0;
                foreach (CTPX ct in CTPXs)
                {
                    MATHANG mh = MatHang.ToList().Find(x => x.MAHANG == ct.MAHANG);
                    if(mh != null)
                    {
                        PhieuXuatHang.TONGTIEN += (decimal)(
                            mh.DONGIA * ct.SOLUONG
                        );
                    }
                }
                PhieuXuatHang.CONLAI = PhieuXuatHang.TONGTIEN - PhieuXuatHang.SOTIENTRA;
                OnPropertyChanged("DanhSachChiTietPhieuXuat");
                OnPropertyChanged("PhieuXuatHang");
            }

            #region List
            public CTPX[] CTPXs
            {
                get => DanhSachChiTietPhieuXuat.ToArray();
            }
            public ObservableCollection<MATHANG> MatHang
            {
                get => new MatHangBUS().GetAllMatHang();
            }
            #endregion
        };
        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void RemoveItem(object sender, RoutedEventArgs e)
        {
            ((State)DataContext).DanhSachChiTietPhieuXuat.Remove(
                ListViewDSPX.SelectedItem as CTPX
            );
            Update();
        }
        private void AddItem(object sender, RoutedEventArgs e)
        {
            int MatHangCount = ((State)DataContext).MatHang.Count,
                DSCTPXCount = ((State)DataContext).DanhSachChiTietPhieuXuat.Count;

            if (MatHangCount > DSCTPXCount)
            {
                ((State)DataContext).DanhSachChiTietPhieuXuat
                    .Add(new CTPX()
                    {
                        MAHANG = -1,
                        SOLUONG = 1
                    }
                );
            }
            Update();
        }

        private void Update()
        {
            ((State)DataContext).Update(); 
        }
        private void ComboBoxUpdate(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Update(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        private void LapPhieuXuat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Khi đã tạo không thể thay đổi", "Xác nhận", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                Update();
                if (new PhieuXuatBUS().InsertPhieuXuat(
                    (DataContext as State).CTPXs,
                    (DataContext as State).PhieuXuatHang)
                )
                {
                    MessageBox.Show("Đã thêm thành công");
                    DialogResult = true;
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }
    }
}
