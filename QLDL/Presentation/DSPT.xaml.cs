using QLDL.BusinessLogic;
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
    /// Interaction logic for DSPT.xaml
    /// </summary>
    public partial class DSPT : Window
    {
        public vwDAILY_LOAIDL_QUAN vwdl { get; set; }
        private ObservableCollection<vw_PhieuThu_NhanVien_DaiLy> listPhieuThu;
        public ICollectionView collectionView;
        public string searchstring;
        public GroupFilter groupFilter;
        public Predicate<object> searchFilter;
        private PhieuThuBUS ptbus = new PhieuThuBUS();

        public DSPT(vwDAILY_LOAIDL_QUAN vwdl)
        {
            InitializeComponent();
            this.vwdl = vwdl;

            // Lấy dữ liệu ban đầu
            InitialData();
        }

        private void InitialData()
        {
            //get data to list
            listPhieuThu = ptbus.getPhieuThuByDaiLy(vwdl.MADL);

            //create and apply 2 filters
            CreateFilter();

            // get datalist to UI
            lsvPT.ItemsSource = listPhieuThu;
        }

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
            collectionView = CollectionViewSource.GetDefaultView(listPhieuThu);

            searchFilter = delegate(object item)
            {
                return (item as vw_PhieuThu_NhanVien_DaiLy).TENNV.ToLower().Contains(txtSearch.Text.ToLower()) == true ? true : false;
            };

            groupFilter = new GroupFilter();
            groupFilter.AddFilter(searchFilter);
            collectionView.Filter = groupFilter.Filter;
        }
        //filter dựa trên thanh search
        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lsvPT.ItemsSource).Refresh();
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

    }
}
