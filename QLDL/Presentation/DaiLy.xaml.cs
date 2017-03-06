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
using System.Collections.ObjectModel;

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

        public DaiLy()
        {
            InitializeComponent();

            // Lấy dữ liệu ban đầu
            lsvDL.ItemsSource = dlbus.getAllDaiLy();

            cbbLoaiDL.ItemsSource = dlbus.getAllLoaiDL();
            cbbLoaiDL.DisplayMemberPath = "TENLOAI";
            cbbLoaiDL.SelectedValuePath = "MALOAI";

            cbbQuan.ItemsSource = dlbus.getAllQuan();
            cbbQuan.DisplayMemberPath = "TENQUAN";
            cbbQuan.SelectedValuePath = "MAQUAN";

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
        private void AddDL(object sender, RoutedEventArgs e)
        {
            dynamic item = lsvDL.SelectedItem;

            if (dlbus.insertDaiLy(item.TENDL, item.DIACHI, item.DIENTHOAI, item.MAQUAN, item.LOAIDL, item.NGAYTIEPNHAN))
            {
                MessageBox.Show("Đã thêm thành công");
                lsvDL.ItemsSource = dlbus.getAllDaiLy();
            }
            else
                MessageBox.Show("Có lỗi xảy ra");
        }

        private void UpdateDL(object sender, RoutedEventArgs e)
        {
            dynamic item = lsvDL.SelectedItem;

            if (dlbus.updateDaiLy(item.MADL, item.TENDL, item.DIACHI, item.DIENTHOAI, item.MAQUAN, item.LOAIDL, item.NGAYTIEPNHAN))
            {
                MessageBox.Show("Đã sửa thành công");
                lsvDL.ItemsSource = dlbus.getAllDaiLy();
            }
            else
                MessageBox.Show("Có lỗi xảy ra");
        }

        private void RemoveDL(object sender, RoutedEventArgs e)
        {
            dynamic item = lsvDL.SelectedItem;

            if (dlbus.removeDaiLy(item.MADL))
            {
                MessageBox.Show("Đã xóa thành công");
                lsvDL.ItemsSource = dlbus.getAllDaiLy();
            }
            else
                MessageBox.Show("Có lỗi xảy ra");
        } 
        #endregion

        // to do 
        // check null foreign key
        // search: chỉ theo tên do làm biếng. fulltext: https://msdn.microsoft.com/en-us/library/ms142583.aspx
        // right click contextmenu: set dẹp tiệm (tình trạng =0), xóa đại lý(?? có nên)
        // in tất cả đại lý, in 1 đại lý


    }
}
