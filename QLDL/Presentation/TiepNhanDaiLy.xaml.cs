using QLDL.BusinessLogic;
using QLDL.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for TiepNhanDaiLy.xaml
    /// </summary>
    public partial class TiepNhanDaiLy : Window, IDisposable
    {
        private DaiLyBUS dlbus = new DaiLyBUS();
        public ObservableCollection<LOAIDL> listLoaiDL;
        public ObservableCollection<QUAN> listQuan;
        public Predicate<object> searchFilter;
        public Predicate<object> showhide;
        public vwDAILY_LOAIDL_QUAN VW { get; set; }

        public TiepNhanDaiLy()
        {
            InitializeComponent();
            InitialData();
        }

        public void Dispose()
        {

        }


        private void InitialData()
        {
            //get data to list
            listLoaiDL = dlbus.GetAllLoaiDL();
            listQuan = dlbus.GetAllQuan();

            // get datalist to UI
            cbbLoaiDL.ItemsSource = listLoaiDL;
            cbbQuan.ItemsSource = listQuan;
        }

        private void ThemDL(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn thêm thông tin đã chọn?", "Xác nhận thêm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                VW = new vwDAILY_LOAIDL_QUAN()
                {
                    TENDL = txtTenDL.Text,
                    DIACHI = txtDiaChi.Text,
                    DIENTHOAI = txtDienThoai.Text,
                    NGAYTIEPNHAN = DateTime.Today
                };
                QUAN q = cbbQuan.SelectedItem as QUAN;
                VW.TENQUAN = q.TENQUAN;
                LOAIDL l = cbbLoaiDL.SelectedItem as LOAIDL;
                VW.TENLOAI = l.TENLOAI;
                VW.TINHTRANG = 1;

                if (dlbus.InsertDaiLy(VW.TENDL, VW.DIACHI, VW.DIENTHOAI, q.MAQUAN, l.MALOAI))
                {
                    MessageBox.Show("Đã thêm thành công");
                    this.DialogResult = true;
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
