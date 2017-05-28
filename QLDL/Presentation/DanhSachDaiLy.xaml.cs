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

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for DanhSachDaiLy.xaml
    /// </summary>
    public partial class DanhSachDaiLy : Window
    {

        private DaiLyBUS dlbus = new DaiLyBUS();
        private ObservableCollection<vwDAILY_LOAIDL_QUAN> listDL;
        //public ObservableCollection<LOAIDL> listLoaiDL;
        //public ObservableCollection<QUAN> listQuan;
        private ICollectionView collectionView;
        public GroupFilter groupFilter;
        public Predicate<object> searchFilter;
        public Predicate<object> showhide;
        public string searchstring;

        public DanhSachDaiLy()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPIInitialize;
            DataContext = new State()
            {
                // Cài đặt cái giá trị mặc định ban đầu ở đây
                Loc = "asd",
                HienThiDLNgungHoatDong = true,
                DanhSachDaiLy = dlbus.GetAllDaiLy()
            };
            // CreateFilter();
        }
        private void DPIInitialize(object sender, RoutedEventArgs e)
        {
            Point Scale = Class.DPI.Initialize(sender, e);
            Main.LayoutTransform = new ScaleTransform(Scale.X, Scale.Y);
        }
        private class State
        {
            private string loc;
            private bool hienThiDLNgungHoatDong;
            private ObservableCollection<vwDAILY_LOAIDL_QUAN> danhSachDaiLy;

            public string Loc {
                get {
                    return loc;
                }
                set
                {
                    loc = value;
                    if(DanhSachDaiLy != null)
                        CollectionViewSource.GetDefaultView(DanhSachDaiLy).Refresh();
                }
            }
            public bool HienThiDLNgungHoatDong { get => hienThiDLNgungHoatDong; set => hienThiDLNgungHoatDong = value; }
            public ObservableCollection<vwDAILY_LOAIDL_QUAN> DanhSachDaiLy {
                get => danhSachDaiLy;
                set => danhSachDaiLy = value;
            }
        };

        #region Report

        private void OpenReport(object sender, RoutedEventArgs e)
        {
            //ReportPreview rp = new ReportPreview();
            //rp.Show();
        }

        #endregion

        #region Filter

        // khởi tạo filter
        private void CreateFilter()
        {
            collectionView = CollectionViewSource.GetDefaultView(listDL);

            searchFilter = delegate(object item)
            {
                return (item as vwDAILY_LOAIDL_QUAN).TENDL.ToLower()
                .Contains( ((State)DataContext).Loc.ToLower() ) == true ? true : false;
            };

            showhide = delegate(object item)
            {
                return (item as vwDAILY_LOAIDL_QUAN).TINHTRANG == 1 ? true : false;
            };

            groupFilter = new GroupFilter();
            groupFilter.AddFilter(showhide);
            groupFilter.AddFilter(searchFilter);

            collectionView.Filter = groupFilter.Filter;
        }

        // show/hide đại lí đã ngưng hoạt động
        private void StoppedDL(object sender, RoutedEventArgs e)
        {
            if (((State)DataContext).HienThiDLNgungHoatDong)
            {
                groupFilter.RemoveFilter(showhide);
            }
            else
            {
                groupFilter.AddFilter(showhide);
            }
            collectionView.Filter = groupFilter.Filter;
        }

        //filter dựa trên thanh search
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        // group filter khi có nhiều filter
        public class GroupFilter
        {
            private List<Predicate<object>> _filters;

            public Predicate<object> Filter { get; private set; }

            public GroupFilter()
            {
                _filters = new List<Predicate<object>>();
                Filter = InternalFilter;
            }

            private bool InternalFilter(object o)
            {
                foreach (var filter in _filters)
                {
                    if (!filter(o))
                    {
                        return false;
                    }
                }

                return true;
            }

            public void AddFilter(Predicate<object> filter)
            {
                _filters.Add(filter);
            }

            public void RemoveFilter(Predicate<object> filter)
            {
                if (_filters.Contains(filter))
                {
                    _filters.Remove(filter);
                }
            }
        }

        #endregion

        private void InitialData()
        {
            //get data to list
            listDL = dlbus.GetAllDaiLy();
            //listLoaiDL = dlbus.GetAllLoaiDL();
            //listQuan = dlbus.GetAllQuan();

            //create and apply 2 filters
            CreateFilter();

            // get datalist to UI
            ((State)DataContext).DanhSachDaiLy = listDL;
            // lsvDL.ItemsSource = listDL;
        }

        #region Thêm xóa sửa Đại Lý (show)
        private void AddDL(object sender, RoutedEventArgs e)
        {
            using (TiepNhanDaiLy tndl = new TiepNhanDaiLy())
            {
                tndl.ShowDialog();
                if (tndl.DialogResult.HasValue && tndl.DialogResult.Value)
                {
                    listDL.Add(tndl.VW);
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
                if (dlbus.RemoveDaiLy(item.MADL))
                {
                    MessageBox.Show("Đã xóa thành công");
                    listDL.Remove(item);
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }
        #endregion
    }
}
