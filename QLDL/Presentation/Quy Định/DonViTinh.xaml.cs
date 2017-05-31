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
    public partial class DonViTinh : Window
    {
        public DonViTinh()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                LocTheoTen = "",
                DanhSachDonViTinh = new DonViTinhBUS().GetAllDVT()
            };
            ((State)DataContext).SetFilter();
            
        }

        private void DanhSachDonViTinh_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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
            private ObservableCollection<DVT> danhSachDonViTinh;
            public ObservableCollection<DVT> DanhSachDonViTinh
            {
                get => danhSachDonViTinh;
                set => danhSachDonViTinh = value;
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
                    CollectionViewSource.GetDefaultView(DanhSachDonViTinh);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion
        };
        private void Add(object sender, RoutedEventArgs e)
        {
            (DataContext as State).DanhSachDonViTinh.Add(
                new DVT()
                {
                    DVT1 = "Tên đơn vị tính",
                }
            );
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (new DonViTinhBUS().InsertOrUpdateDonViTinh(
                (DataContext as State).DanhSachDonViTinh.ToArray()
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
            (DataContext as State).DanhSachDonViTinh = new DonViTinhBUS().GetAllDVT();
            MessageBox.Show("Đã làm mới");
        }
    }
}
