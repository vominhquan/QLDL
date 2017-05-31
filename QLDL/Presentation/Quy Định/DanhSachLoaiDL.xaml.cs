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
    public partial class DanhSachLoaiDL : Window
    {
        public DanhSachLoaiDL()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                LocTheoTen = "",
                DanhSachLoaiDL = new DaiLyBUS().GetAllLoaiDL()
            };
            ((State)DataContext).SetFilter();
            
        }

        private void DanhSachLoaiDL_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
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

            #region (ObservableCollection) Danh sách loại đại lý
            private ObservableCollection<LOAIDL> danhSachLoaiDL;
            public ObservableCollection<LOAIDL> DanhSachLoaiDL
            {
                get => danhSachLoaiDL;
                set => danhSachLoaiDL = value;
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
                    CollectionViewSource.GetDefaultView(DanhSachLoaiDL);
                if (CollectionView != null) CollectionView.Filter = Filters.Filter;
            }
            #endregion
        };
        private void Add(object sender, RoutedEventArgs e)
        {
            (DataContext as State).DanhSachLoaiDL.Add(
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
                (DataContext as State).DanhSachLoaiDL.ToArray()
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
            (DataContext as State).DanhSachLoaiDL = new DaiLyBUS().GetAllLoaiDL();
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
