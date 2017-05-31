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
using Applications.Components;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for DanhSachDaiLy.xaml
    /// </summary>
    public partial class DanhSachMatHang : Window
    {
        public DanhSachMatHang()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                LocTheoTen = "",
                DanhSachMatHang = new MatHangBUS().GetAllMatHang()
            };
            ((State)DataContext).SetFilter();
            
        }

        private void DanhSachMatHang_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
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

            #region (ObservableCollection) Danh sách mặt hàng
            private ObservableCollection<MATHANG> danhSachMatHang;
            public ObservableCollection<MATHANG> DanhSachMatHang
            {
                get => danhSachMatHang;
                set => danhSachMatHang = value;
            }
            #endregion

            public ObservableCollection<DVT> DonViTinh
            {
                get => new DonViTinhBUS().GetAllDVT();
            }
            #region (void) SetFilter
            public void SetFilter()
            {
                #region Tạo Filters
                GroupFilter Filters = new GroupFilter();
                Filters.AddFilter(delegate (object item)
                {
                    return (item as MATHANG).TENHANG.ToLower()
                    .Contains(LocTheoTen.ToLower()) == true;
                });
                #endregion

                ICollectionView CollectionView = 
                    CollectionViewSource.GetDefaultView(DanhSachMatHang);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion
        };
        private void Add(object sender, RoutedEventArgs e)
        {
            (DataContext as State).DanhSachMatHang.Add(
                new MATHANG()
                {
                    MAHANG = 0,
                    DONGIA = 0,
                    MADVT = 1,
                    TENHANG = "Tên mặt hàng"
                }
            );
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (new MatHangBUS().InsertOrUpdateMatHang(
                (DataContext as State).DanhSachMatHang.ToArray()
            ))
            {
                MessageBox.Show("lưu thành công");
            }
            else
            {
                MessageBox.Show("có lỗi xảy ra");
            }
        }
        private void Refresh(object sender, RoutedEventArgs e)
        {
            (DataContext as State).DanhSachMatHang = new MatHangBUS().GetAllMatHang();
            MessageBox.Show("Đã làm mới");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
