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
using QLDL.Presentation;
using QLDL.Class;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for ViewNhanVien.xaml
    /// </summary>
    public partial class DanhSachNhanVien : Window
    {
        public DanhSachNhanVien()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                LocTheoTen = "",
                LocTheoDiaChi = "",
                DanhSachNhanVien = new NhanVienBUS().GetAllNhanVien()
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

            #region (int) Lọc theo mã Chức vụ
            private int locTheoMaChucVu;
            public int LocTheoMaChucVu
            {
                get => locTheoMaChucVu;
                set
                {
                    locTheoMaChucVu = value;
                    SetFilter();
                }
            }
            #endregion

            #region (int) Lọc theo địa chỉ
            private string locTheoDiaChi;
            public string LocTheoDiaChi
            {
                get => locTheoDiaChi;
                set
                {
                    locTheoDiaChi = value;
                    SetFilter();
                }
            }
            #endregion

            #region (bool) Hiển thị nhiều bộ lọc khác
            private bool moreFilter;
            public bool MoreFilter
            {
                get => moreFilter;
                set
                {
                    if (moreFilter == value) return;
                    moreFilter = value;
                    SetFilter();
                }
            }
            #endregion

            #region (ObservableCollection) Danh sách nhân viên
            private ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN> danhSachNhanVien;
            public ObservableCollection<vwCHUCVU_NHANVIEN_TAIKHOAN> DanhSachNhanVien
            {
                get => danhSachNhanVien;
                set => danhSachNhanVien = value;
            }
            #endregion

            #region (void) SetFilter
            public void SetFilter()
            {
                #region Tạo Filters
                GroupFilter Filters = new GroupFilter();
                Filters.AddFilter(delegate (object item)
                {
                    return (item as vwCHUCVU_NHANVIEN_TAIKHOAN).TENNV.ToLower()
                    .Contains(LocTheoTen.ToLower()) == true;
                });
                Filters.AddFilter(delegate (object item)
                {
                    return LocTheoMaChucVu == 0 ? true : (item as vwCHUCVU_NHANVIEN_TAIKHOAN).MACHUCVU == LocTheoMaChucVu;
                }, MoreFilter);
                Filters.AddFilter(delegate (object item)
                {
                    return (item as vwCHUCVU_NHANVIEN_TAIKHOAN).DIACHI.ToLower()
                    .Contains(LocTheoDiaChi.ToLower()) == true;
                }, MoreFilter);
                #endregion

                ICollectionView CollectionView =
                    CollectionViewSource.GetDefaultView(DanhSachNhanVien);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion

            #region List
            public ObservableCollection<CHUCVU> ChucVu
            {
                get => new NhanVienBUS().GetALLChucVu();
            }
            #endregion
        };

        #region Button

        private void ThemNhanVien(object sender, RoutedEventArgs e)
        {

        }

        private void SuaNV(object sender, RoutedEventArgs e)
        {

        }

        private void ToggleMoreFilter(object sender, RoutedEventArgs e)
        {
            (DataContext as State).MoreFilter ^= true;
        }

        private void ClearChucVu(object sender, RoutedEventArgs e)
        {
            (DataContext as State).LocTheoMaChucVu = 0;
        }
        #endregion

        ///// <summary>
        ///// Cancel form;
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnHuy_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        ///// <summary>
        ///// Set enable or disable advance search
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void HandleCheck(object sender, RoutedEventArgs e)
        //{
        //    gridAdvanced.Visibility = Visibility.Visible;
        //}
        //private void HandleUnchecked(object sender, RoutedEventArgs e)
        //{
        //    gridAdvanced.Visibility = Visibility.Collapsed;
        //}

        ///// <summary>
        ///// Search worker by Name
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnTim_Click(object sender, RoutedEventArgs e)
        //{
        //    if(cbChucvu.SelectedItem == null)
        //    {
        //        lv.ItemsSource = BUSView.Instance.GetWorkerByName(txbTen.Text, 0, txbDC.Text);
        //    }
        //    else
        //    {
        //        lv.ItemsSource = BUSView.Instance.GetWorkerByName(txbTen.Text, (int)cbChucvu.SelectedValue, txbDC.Text);
        //    }
        //    cbChucvu.SelectedItem = null;
        //    txbDC.Clear();
        //}

        //private void Xoa_Click(object sender, RoutedEventArgs e)
        //{
        //    if(lv.SelectedItem == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn 1 nhân viên muốn xóa");
        //    }
        //    else
        //    {
        //        int ma = (lv.SelectedItem as vwCHUCVU_NHANVIEN_TAIKHOAN).MANV;
        //        MessageBoxResult result = MessageBox.Show("Bạn muốn xóa nhân viên đã chọn?", "Xác nhận xóa", MessageBoxButton.YesNo);

        //        if (result == MessageBoxResult.Yes)
        //            if (BUSQLNhanVien.Instance.Delete(ma))
        //                MessageBox.Show("Đã xóa thành công");
        //            else
        //                MessageBox.Show("Có lỗi xảy ra");
        //    }
        //}

        //private void btnSua_Click(object sender, RoutedEventArgs e)
        //{
        //    if (lv.SelectedItem == null)
        //    {
        //        MessageBox.Show("Vui lòng chọn 1 nhân viên muốn sửa");
        //    }
        //    else
        //    {
        //        // EditNhanVien edit = new EditNhanVien(lv.SelectedItem as vwCHUCVU_NHANVIEN_TAIKHOAN);
        //        // edit.Show();
        //    }
        //}
    }
}

