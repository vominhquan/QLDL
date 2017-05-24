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
    /// Interaction logic for DaiLy.xaml
    /// </summary>
    /// 

    public partial class DaiLy : Window
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

        public DaiLy()

        {
            InitializeComponent();

            // Lấy dữ liệu ban đầu
            InitialData();
        }

        #region Report
        
        private void OpenReport(object sender, RoutedEventArgs e)
        {
            //ReportPreview rp = new ReportPreview();
            //rp.Show();
        }

        #endregion

        #region Đại lý CRUD

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
            cbbLoaiDL.ItemsSource = listLoaiDL;
            cbbQuan.ItemsSource = listQuan;
        }

        private void AddDL(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn thêm thông tin đã chọn?", "Xác nhận thêm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                vwDAILY_LOAIDL_QUAN vw = new vwDAILY_LOAIDL_QUAN();
                vw.TENDL = txtTenDL.Text;
                vw.DIACHI = txtDiaChi.Text;
                vw.DIENTHOAI = txtDienThoai.Text;
                vw.NGAYTIEPNHAN = DateTime.Today;
                QUAN q = cbbQuan.SelectedItem as QUAN;
                vw.TENQUAN = q.TENQUAN;
                LOAIDL l = cbbLoaiDL.SelectedItem as LOAIDL;
                vw.TENLOAI = l.TENLOAI;
                vw.TINHTRANG = 1;

                if (dlbus.insertDaiLy(vw.TENDL, vw.DIACHI, vw.DIENTHOAI, q.MAQUAN, l.MALOAI))
                {
                    MessageBox.Show("Đã thêm thành công");
                    // thêm dòng mới trong list view, thay vì load lại tất cả dữ liệu vì sẽ tốn thời gian nếu quá nhiều dữ liệu
                    listDL.Add(vw);
                    //listDL.Insert(0, vw); //thêm đầu
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void UpdateDL(object sender, RoutedEventArgs e)
        {
            dynamic item = lsvDL.SelectedItem;

            item.TENDL = txtTenDL.Text;
            item.DIACHI = txtDiaChi.Text;
            item.DIENTHOAI = txtDienThoai.Text;
            item.MAQUAN = Int32.Parse(cbbQuan.SelectedValue.ToString());
            item.LOAIDL = Int32.Parse(cbbLoaiDL.SelectedValue.ToString());

            MessageBoxResult result = MessageBox.Show("Bạn muốn sửa thông tin đã chọn?", "Xác nhận sửa", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                if (dlbus.updateDaiLy(item.MADL, item.TENDL, item.DIACHI, item.DIENTHOAI, item.MAQUAN, item.LOAIDL))
                    MessageBox.Show("Đã sửa thành công");
                else
                    MessageBox.Show("Có lỗi xảy ra");
        }

        private void RemoveDL(object sender, RoutedEventArgs e)
        {
            dynamic item = lsvDL.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Bạn muốn sửa thông tin đã chọn?", "Xác nhận sửa", MessageBoxButton.YesNo);
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

        // bỏ chọn trong listview
        private void RemoveSelected(object sender, MouseButtonEventArgs e)
        {
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
                lsvDL.UnselectAll();
        }

        // load mặc định combobox, đang tìm cách khác
        private void loadcbb(object sender, EventArgs e)
        {
            cbbLoaiDL.SelectedIndex = 0;
            cbbQuan.SelectedIndex = 0;
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

        private void abc(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(lsvDL.SelectedItem.ToString());

        }

        

        // to do
        // right click contextmenu: set dẹp tiệm (tình trạng =0), in 1 đại lý
        // in tất cả đại lý

    }
}
