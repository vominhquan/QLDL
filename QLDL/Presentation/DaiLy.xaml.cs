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
    /// Interaction logic for DaiLy.xaml
    /// </summary>
    /// 

    public partial class DaiLy : Window
    {
        private DaiLyBUS dlbus = new DaiLyBUS();
        private bool _isReportViewerLoaded;
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
            _reportViewer.Load += ReportViewer_Load;
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            if (!_isReportViewerLoaded)
            {
                this._reportViewer.LocalReport.ReportEmbeddedResource = "QLDL.Report.HoSoDaiLy.rdlc";
                _reportViewer.RefreshReport();

                _isReportViewerLoaded = true;
            }
        }

        #region Đại lý CRUD

        private void InitialData()
        {
            //get data to list
            listDL = dlbus.getAllDaiLy();
            listLoaiDL = dlbus.getAllLoaiDL();
            listQuan = dlbus.getAllQuan();

            //create and apply 2 filters
            collectionView = CollectionViewSource.GetDefaultView(listDL);

            searchFilter = delegate(object item)
            {
                return (item as vwDAILY_LOAIDL_QUAN).TENDL.Contains(txtSearch.Text) == true ? true : false;
            };

            showhide = delegate(object item)
            {
                return (item as vwDAILY_LOAIDL_QUAN).TINHTRANG == 1 ? true : false;
            };

            groupFilter = new GroupFilter();
            groupFilter.AddFilter(showhide);
            groupFilter.AddFilter(searchFilter);
            collectionView.Filter = groupFilter.Filter;

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
                vw.NGAYTIEPNHAN = dpNgayTiepNhan.SelectedDate;
                QUAN q = cbbQuan.SelectedItem as QUAN;
                vw.TENQUAN = q.TENQUAN;
                LOAIDL l = cbbLoaiDL.SelectedItem as LOAIDL;
                vw.TENLOAI = l.TENLOAI;

                if (dlbus.insertDaiLy(vw.TENDL, vw.DIACHI, vw.DIENTHOAI, q.MAQUAN, l.MALOAI, (DateTime)dpNgayTiepNhan.SelectedDate))
                {
                    MessageBox.Show("Đã thêm thành công");
                    // thêm dòng mới trong list view, thay vì load lại tất cả dữ liệu vì sẽ tốn thời gian nếu quá nhiều dữ liệu
                    listDL.Add(vw);
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
            item.NGAYTIEPNHAN = dpNgayTiepNhan.SelectedDate;
            
            MessageBoxResult result = MessageBox.Show("Bạn muốn sửa thông tin đã chọn?", "Xác nhận sửa", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (dlbus.updateDaiLy(item.MADL, item.TENDL, item.DIACHI, item.DIENTHOAI, item.MAQUAN, item.LOAIDL, item.NGAYTIEPNHAN))
                {
                    MessageBox.Show("Đã sửa thành công");
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
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

        private void RemoveSelected(object sender, MouseButtonEventArgs e)
        {
            HitTestResult r = VisualTreeHelper.HitTest(this, e.GetPosition(this));
            if (r.VisualHit.GetType() != typeof(ListBoxItem))
                lsvDL.UnselectAll();
        }

        private void loadcbb(object sender, EventArgs e)
        {
            cbbLoaiDL.SelectedIndex = 0;
            cbbQuan.SelectedIndex = 0;
        }

        private void StoppedDL(object sender, RoutedEventArgs e)
        {
            if (cbTinhTrang.IsChecked == true)
                groupFilter.RemoveFilter(showhide);
            else
                groupFilter.AddFilter(showhide);
            collectionView.Filter = groupFilter.Filter;
        }

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

        // to do
        // right click contextmenu: set dẹp tiệm (tình trạng =0), xóa đại lý(?? có nên)
        // in tất cả đại lý, in 1 đại lý
        // tìm cách gán default combobox


    }
}
