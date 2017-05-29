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
    /// Interaction logic for DanhSachPhieuXuat.xaml
    /// </summary>
    public partial class DanhSachPhieuXuat : Window
    {
        //private ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> listPhieuXuat;
        //public ICollectionView collectionView;
        //public string searchstring;
        //public GroupFilter groupFilter;
        //public Predicate<object> searchFilter;
        //private MatHangBUS mhbus = new MatHangBUS();
        //private PhieuXuatBUS pxbus = new PhieuXuatBUS();
        //private PhieuThuBUS ptbus = new PhieuThuBUS();

        public DanhSachPhieuXuat()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            // Application.Current.Resources["Scale"] = 
            DataContext = new State()
            {
                LocTheoTen = "",
                DanhSachPhieuXuat = (new PhieuXuatBUS()).getAllPhieuXuat()
            };
            ((State)DataContext).SetFilter();
        }
        private class State: INotifyPropertyChanged
        {
            #region Init INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            #endregion

            #region (string) Lọc theo tên 
            private string locTheoTen;
            public string LocTheoTen
            {
                get => locTheoTen;
                set
                {
                    locTheoTen = value;
                    if (DanhSachPhieuXuat != null)
                        CollectionViewSource.GetDefaultView(DanhSachPhieuXuat).Refresh();
                }
            }
            #endregion

            #region (ObservableCollection) Danh sách phiếu xuất
            private ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> danhSachPhieuXuat;
            public ObservableCollection<vw_PhieuXuat_NhanVien_DaiLy> DanhSachPhieuXuat
            {
                get => danhSachPhieuXuat;
                set => danhSachPhieuXuat = value;
            }
            #endregion

            #region (void) SetFilter
            public void SetFilter()
            {
                #region Tạo Filters
                GroupFilter Filters = new GroupFilter();
                Filters.AddFilter(delegate (object item)
                {
                    return (item as vw_PhieuXuat_NhanVien_DaiLy).TENNV.ToLower()
                    .Contains(LocTheoTen.ToLower()) == true ? true : false;
                });
                #endregion

                ICollectionView CollectionView =
                    CollectionViewSource.GetDefaultView(DanhSachPhieuXuat);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion
        }

        #region Report

        private void OpenReport(object sender, RoutedEventArgs e)
        {
            //ReportPreview rp = new ReportPreview();
            //rp.Show();
        }

        #endregion

        #region Xem phiếu xuất
        private void XemPhieuXuat(object sender, RoutedEventArgs e)
        {
            (new ChiTietPhieuXuat(
                ListViewDanhSachPhieuXuat.SelectedItem as vw_PhieuXuat_NhanVien_DaiLy
            )).ShowDialog();
        }
        #endregion
    }
}
