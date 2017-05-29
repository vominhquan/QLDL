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
    public partial class SuaDaiLy : Window
    {

        //private DaiLyBUS dlbus = new DaiLyBUS();
        //public ObservableCollection<LOAIDL> listLoaiDL;
        //public ObservableCollection<QUAN> listQuan;
        //public Predicate<object> searchFilter;
        //public Predicate<object> showhide;
        //public vwDAILY_LOAIDL_QUAN vw { get; set; }
        private vwDAILY_LOAIDL_QUAN Source;
        public SuaDaiLy(vwDAILY_LOAIDL_QUAN View)
        {
            InitializeComponent();
            Source = View;
            DataContext = View;
            // View;
        }
        private class State
        {
            private vwDAILY_LOAIDL_QUAN DaiLy;
            public vwDAILY_LOAIDL_QUAN DaiLy1 { get => DaiLy; set => DaiLy = value; }
        }

        //private void InitialData()
        //{
        //    //get data to list
        //    listLoaiDL = dlbus.GetAllLoaiDL();
        //    listQuan = dlbus.GetAllQuan();

        //    // get datalist to UI
        //    cbbLoaiDL.ItemsSource = listLoaiDL;
        //    cbbQuan.ItemsSource = listQuan;

        //}

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn sửa thông tin?", "Xác nhận thêm", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                //new vwDAILY_LOAIDL_QUAN()
                //{
                //};
                //vw.TENDL = txtTenDL.Text;
                //vw.DIACHI = txtDiaChi.Text;
                //vw.DIENTHOAI = txtDienThoai.Text;
                //QUAN q = cbbQuan.SelectedItem as QUAN;
                //vw.TENQUAN = q.TENQUAN;
                //LOAIDL l = cbbLoaiDL.SelectedItem as LOAIDL;
                //vw.TENLOAI = l.TENLOAI;
                //vw.TINHTRANG = 1;

                if ((new DaiLyBUS()).UpdateDaiLy((vwDAILY_LOAIDL_QUAN)DataContext))
                {
                    MessageBox.Show("Đã sửa thành công");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            DataContext = Source;
            Close();
        }
    }
}
