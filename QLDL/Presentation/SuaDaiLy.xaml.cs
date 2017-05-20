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
    /// Interaction logic for SuaDaiLy.xaml
    /// </summary>
    public partial class SuaDaiLy : Window, IDisposable
    {

        private DaiLyBUS dlbus = new DaiLyBUS();
        public ObservableCollection<LOAIDL> listLoaiDL;
        public ObservableCollection<QUAN> listQuan;
        public Predicate<object> searchFilter;
        public Predicate<object> showhide;
        public vwDAILY_LOAIDL_QUAN vw { get; set; }

        public SuaDaiLy(vwDAILY_LOAIDL_QUAN vw1)
        {
            InitializeComponent();
            this.vw = vw1;
        }

        public void Dispose()
        {

        }


        private void InitialData()
        {
            //get data to list
            listLoaiDL = dlbus.getAllLoaiDL();
            listQuan = dlbus.getAllQuan();

            // get datalist to UI
            cbbLoaiDL.ItemsSource = listLoaiDL;
            cbbQuan.ItemsSource = listQuan;
        }

        private void SuaDL(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn thêm thông tin đã chọn?", "Xác nhận thêm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                vw = new vwDAILY_LOAIDL_QUAN();
                vw.TENDL = txtTenDL.Text;
                vw.DIACHI = txtDiaChi.Text;
                vw.DIENTHOAI = txtDienThoai.Text;
                vw.NGAYTIEPNHAN = DateTime.Today;
                QUAN q = cbbQuan.SelectedItem as QUAN;
                vw.TENQUAN = q.TENQUAN;
                LOAIDL l = cbbLoaiDL.SelectedItem as LOAIDL;
                vw.TENLOAI = l.TENLOAI;
                vw.TINHTRANG = 1;

                if (dlbus.updateDaiLy(vw.MADL, vw.TENDL, vw.DIACHI, vw.DIENTHOAI, q.MAQUAN, l.MALOAI))
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
