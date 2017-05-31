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
    /// Interaction logic for DanhSachPhieuThu.xaml
    /// </summary>
    public partial class DanhSachPhieuThu : Window
    {
        public DanhSachPhieuThu()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                DaiLyWidth = double.NaN,
                LocTheoTen = "",
                DanhSachPhieuThu = (new PhieuThuBUS()).GetAllPhieuThu()
            };
            ((State)DataContext).SetFilter();
        }
        public DanhSachPhieuThu(int MADL)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                DaiLyWidth = 0,
                LocTheoTen = "",
                DanhSachPhieuThu = (new PhieuThuBUS()).GetPhieuThuByDaiLy(MADL)
            };
            ((State)DataContext).SetFilter();
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

            #region Ẩn cột đại lý
            private double daiLyWidth;
            public double DaiLyWidth { get => daiLyWidth; set => daiLyWidth = value; }
            #endregion

            #region (string) Lọc theo tên 
            private string locTheoTen;
            public string LocTheoTen
            {
                get => locTheoTen;
                set
                {
                    locTheoTen = value;
                    if (DanhSachPhieuThu != null)
                        CollectionViewSource.GetDefaultView(DanhSachPhieuThu).Refresh();
                }
            }
            #endregion

            #region (ObservableCollection) Danh sách phiếu thu
            private ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> danhSachPhieuThu;
            public ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> DanhSachPhieuThu
            {
                get => danhSachPhieuThu;
                set => danhSachPhieuThu = value;
            }
            #endregion

            #region (void) SetFilter
            public void SetFilter()
            {
                #region Tạo Filters
                GroupFilter Filters = new GroupFilter();
                Filters.AddFilter(delegate (object item)
                {
                    return (item as vw_PhieuThu_NhanVien_DaiLy).TENNV.ToLower()
                    .Contains(LocTheoTen.ToLower()) == true ? true : false;
                });
                //Filters.AddFilter(delegate (object item)
                //{
                //    return (item as vw_PhieuThu_NhanVien_DaiLy).TENNV.ToLower()
                //    .Contains(LocTheoTen.ToLower()) == true ? true : false;
                //});
                #endregion

                ICollectionView CollectionView =
                    CollectionViewSource.GetDefaultView(DanhSachPhieuThu);
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

        #region Button
        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

    }
}
