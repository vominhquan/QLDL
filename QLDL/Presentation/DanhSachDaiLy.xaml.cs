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
using Microsoft.Reporting.WinForms;
using QLDL.DataAccess;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for DanhSachDaiLy.xaml
    /// </summary>
    public partial class DanhSachDaiLy : Window
    {

        private DaiLyBUS dlbus = new DaiLyBUS();
        public ObservableCollection<vwDAILY_LOAIDL_QUAN> listDL;
        public ObservableCollection<LOAIDL> listLoaiDL;
        public ObservableCollection<QUAN> listQuan;
        public ICollectionView collectionView;
        public GroupFilter groupFilter;
        public Predicate<object> searchFilter;
        public Predicate<object> showhide;
        public string searchstring;

        public DanhSachDaiLy()
        {
            InitializeComponent();
            // Lấy dữ liệu ban đầu
            InitialData();
        }

        #region Report

        private void OpenReport(object sender, RoutedEventArgs e)
        {
            ReportPreview rp = new ReportPreview();
            rp.Show();
        }

        #endregion

        #region Filter

        // khởi tạo filter
        private void CreateFilter()
        {
            collectionView = CollectionViewSource.GetDefaultView(listDL);

            searchFilter = delegate(object item)
            {
                return (item as vwDAILY_LOAIDL_QUAN).TENDL.ToLower().Contains(txtSearch.Text.ToLower()) == true ? true : false;
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
            if (cbTinhTrang.IsChecked == true)
                groupFilter.RemoveFilter(showhide);
            else
                groupFilter.AddFilter(showhide);
            collectionView.Filter = groupFilter.Filter;
        }

        //filter dựa trên thanh search
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lsvDL.ItemsSource).Refresh();
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
            listDL = dlbus.getAllDaiLy();
            listLoaiDL = dlbus.getAllLoaiDL();
            listQuan = dlbus.getAllQuan();

            //create and apply 2 filters
            CreateFilter();

            // get datalist to UI
            lsvDL.ItemsSource = listDL;
        }

        private void AddDL(object sender, RoutedEventArgs e)
        {
            using (TiepNhanDaiLy tndl = new TiepNhanDaiLy())
            {
                tndl.ShowDialog();
                if (tndl.DialogResult.HasValue && tndl.DialogResult.Value)
                {
                    listDL.Add(tndl.vw);
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
                if (dlbus.removeDaiLy(item.MADL))
                {
                    MessageBox.Show("Đã xóa thành công");
                    listDL.Remove(item);
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }

    }
}
