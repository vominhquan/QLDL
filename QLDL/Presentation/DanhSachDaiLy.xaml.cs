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
//using Microsoft.Reporting.WinForms;
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
            Application.Current.MainWindow.Loaded += DPIInitialize;
            DataContext = new State()
            {
                // Cài đặt cái giá trị mặc định ban đầu ở đây
                LocTheoTen = "",
                HienThiDLNgungHoatDong = true,
                DanhSachDaiLy = (new DaiLyBUS()).GetAllDaiLy()
            };
            ((State)DataContext).SetFilter();
        }
        private void DPIInitialize(object sender, RoutedEventArgs e)
        {
            Point Scale = Class.DPI.Initialize(sender, e);
            Main.LayoutTransform = new ScaleTransform(Scale.X, Scale.Y);
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
                    if (DanhSachDaiLy != null)
                        CollectionViewSource.GetDefaultView(DanhSachDaiLy).Refresh();
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
                    .Contains(LocTheoTen.ToLower()) == true ? true : false;
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
            using (TiepNhanDaiLy tndl = new TiepNhanDaiLy())
            {
                tndl.ShowDialog();
                if (tndl.DialogResult.HasValue && tndl.DialogResult.Value)
                {
                    ((State)DataContext).DanhSachDaiLy.Add(tndl.VW);
                }
            }
        }

        private void XemDL(object sender, RoutedEventArgs e)
        {
            XemDaiLy xdl = new XemDaiLy(lsvDL.SelectedItem as vwDAILY_LOAIDL_QUAN);
            xdl.ShowDialog();
        }

        private void SuaDL(object sender, RoutedEventArgs e)
        {
            SuaDaiLy sdl = new SuaDaiLy(lsvDL.SelectedItem as vwDAILY_LOAIDL_QUAN);
            sdl.ShowDialog();
        }
        private void XoaDL(object sender, RoutedEventArgs e)
        {
            dynamic item = lsvDL.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Bạn muốn xóa đại lý đã chọn?", "Xác nhận xóa", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if ((new DaiLyBUS()).RemoveDaiLy(item.MADL))
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
