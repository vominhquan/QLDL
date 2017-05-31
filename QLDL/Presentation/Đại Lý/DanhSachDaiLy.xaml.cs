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
using System.ComponentModel;
using QLDL.BusinessLogic;
using System.Collections.ObjectModel;
using QLDL.DataAccess;
using QLDL.Class;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for DanhSachDaiLy.xaml
    /// </summary>
    public partial class DanhSachDaiLy : Window
    {
        public DanhSachDaiLy()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                LocTheoTen = "",
                HienThiDLNgungHoatDong = true,
                DanhSachDaiLy = new DaiLyBUS().GetAllDaiLy()
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
                    SetFilter();
                }
            }
            #endregion

            #region (bool) Hiển thị ngưng hoạt động 

            private bool hienThiDLNgungHoatDong;
            public bool HienThiDLNgungHoatDong {
                get => hienThiDLNgungHoatDong;
                set
                {
                    if (hienThiDLNgungHoatDong == value) return;
                    hienThiDLNgungHoatDong = value;
                    SetFilter();
                    OnPropertyChanged("HienThiDLNgungHoatDong");
                }
            }
            #endregion

            #region (ObservableCollection) Danh sách đại lý
            private ObservableCollection<vwDAILY_LOAIDL_QUAN> danhSachDaiLy;
            public ObservableCollection<vwDAILY_LOAIDL_QUAN> DanhSachDaiLy
            {
                get => danhSachDaiLy;
                set => danhSachDaiLy = value;
            }
            #endregion

            #region (void) SetFilter
            public void SetFilter()
            {
                #region Tạo Filters
                GroupFilter Filters = new GroupFilter();
                Filters.AddFilter(delegate (object item)
                {
                    return (item as vwDAILY_LOAIDL_QUAN).TENDL.ToLower()
                    .Contains(LocTheoTen.ToLower()) == true;
                });
                Filters.AddFilter(delegate (object item)
                {
                    return (item as vwDAILY_LOAIDL_QUAN).TINHTRANG == 1 ? true : false;
                }, !HienThiDLNgungHoatDong);
                #endregion

                ICollectionView CollectionView = 
                    CollectionViewSource.GetDefaultView(DanhSachDaiLy);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion
        };

        #region Button
        private void ToggleHienThiDLNgungHoatDong(object sender, RoutedEventArgs e)
        {
            ((State)DataContext).HienThiDLNgungHoatDong ^= true;
        }
        #endregion

        #region Report

        private void OpenReport(object sender, RoutedEventArgs e)
        {
            //ReportPreview rp = new ReportPreview();
            //rp.Show();
        }

        #endregion

        #region Thêm xóa sửa Đại Lý (show)
        private void AddDL(object sender, RoutedEventArgs e)
        {
            TiepNhanDaiLy DaiLyMoi = new TiepNhanDaiLy();
            DaiLyMoi.ShowDialog();
            if (DaiLyMoi.ReturnValue != null)
            {
                ((State)DataContext).DanhSachDaiLy.Add(
                    new DaiLyBUS().GetDaiLyByMADL((int)DaiLyMoi.ReturnValue)
                );
            }
        }

        private void XemDL(object sender, RoutedEventArgs e)
        {
            vwDAILY_LOAIDL_QUAN Item = ListViewDanhSachDaiLy.SelectedItem as vwDAILY_LOAIDL_QUAN;
            int SelectedIndex = ListViewDanhSachDaiLy.SelectedIndex;
            new XemDaiLy(Item).ShowDialog();
            
            ((State)DataContext).DanhSachDaiLy[SelectedIndex] =
                new DaiLyBUS().GetDaiLyByMADL(Item.MADL);
        }

        public void SuaDL()
        {
            vwDAILY_LOAIDL_QUAN Item = ListViewDanhSachDaiLy.SelectedItem as vwDAILY_LOAIDL_QUAN;
            int SelectedIndex = ListViewDanhSachDaiLy.SelectedIndex;
            new SuaDaiLy(Item).ShowDialog();

            ((State)DataContext).DanhSachDaiLy[SelectedIndex] =
                new DaiLyBUS().GetDaiLyByMADL(Item.MADL);
        }
        private void SuaDL(object sender, RoutedEventArgs e)
        {
            SuaDL();
        }
        private void XoaDL(object sender, RoutedEventArgs e)
        {
            dynamic item = ListViewDanhSachDaiLy.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Bạn muốn xóa đại lý đã chọn?", "Xác nhận xóa", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (new DaiLyBUS().RemoveDaiLy(item.MADL))
                {
                    MessageBox.Show("Đã xóa thành công");
                    ((State)DataContext).DanhSachDaiLy.Remove(item);
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }
        #endregion
    }
}
