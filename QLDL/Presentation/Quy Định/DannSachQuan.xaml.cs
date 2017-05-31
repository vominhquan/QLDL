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
    public partial class DanhSachQuan : Window
    {
        public DanhSachQuan()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                LocTheoTen = "",
                DanhSachQuan = new DaiLyBUS().GetAllLoaiDL()
            };
            ((State)DataContext).SetFilter();
            
        }

        private void DanhSachQuan_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

            #region (ObservableCollection) Danh sách quận
            private ObservableCollection<LOAIDL> danhSachQuan;
            public ObservableCollection<LOAIDL> DanhSachQuan
            {
                get => danhSachQuan;
                set => danhSachQuan = value;
            }
            #endregion

            #region (void) SetFilter
            public void SetFilter()
            {
                #region Tạo Filters
                GroupFilter Filters = new GroupFilter();
                Filters.AddFilter(delegate (object item)
                {
                    return (item as LOAIDL).TENLOAI.ToLower()
                    .Contains(LocTheoTen.ToLower()) == true;
                });
                #endregion

                ICollectionView CollectionView = 
                    CollectionViewSource.GetDefaultView(DanhSachQuan);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion
        };
        private void Add(object sender, RoutedEventArgs e)
        {
            (DataContext as State).DanhSachQuan.Add(
                new LOAIDL()
                {
                    MALOAI = 0,
                    TENLOAI = "Loại đại lý mới",
                    SONOTOIDA = null
                }
            );
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            if (new LoaiDLBUS().InsertOrUpdateLoaiDL(
                (DataContext as State).DanhSachQuan.ToArray()
            ))
            {
                MessageBox.Show("Lưu thành công");
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra");

            }
        }
        private void Refresh(object sender, RoutedEventArgs e)
        {
            (DataContext as State).DanhSachQuan = new DaiLyBUS().GetAllLoaiDL();
            MessageBox.Show("Đã làm mới");
        }
        private void RemoveSoNoToiDa(object sender, RoutedEventArgs e)
        {
            LOAIDL item = ((sender as Applications.Components.Button).Tag as LOAIDL);
            item.SONOTOIDA = null;
            item.OnPropertyChanged("SONOTOIDA");
        }
    }
}
