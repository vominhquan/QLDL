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
    /// Interaction logic for PhieuXuat.xaml
    /// </summary>
    /// 


    public partial class PhieuXuat : Window
    {
        private ObservableCollection<MATHANG> listMatHang;
        private MatHangBUS mhbus = new MatHangBUS();
        private PhieuXuatBUS pxbus = new PhieuXuatBUS();
        private ObservableCollection<CTPX> listCTPX;
        private ObservableCollection<CTPXUserControl> listUserControl;
        private int madl;

        public PhieuXuat(int madl)
        {
            InitializeComponent();

            listMatHang = mhbus.getAllMatHang();
            listCTPX = new ObservableCollection<CTPX>();
            listUserControl = new ObservableCollection<CTPXUserControl>();
            CreateCTPX();
            this.madl = madl;
        }

        private void AddCTPX(object sender, RoutedEventArgs e)
        {
            CreateCTPX();
            svCTPX.ScrollToBottom();
            if (listCTPX.Count >= listMatHang.Count)
                btnAddCTPX.IsEnabled = false;
        }

        void CreateCTPX()
        {
            // add user control
            CTPXUserControl ctpxuc = new CTPXUserControl();
            spCTPX.Children.Add(ctpxuc);
            //spCTPX.Children[0];

            //get cbb data
            ctpxuc.cbb.ItemsSource = listMatHang;
            ctpxuc.cbb.DisplayMemberPath = "TENHANG";
            ctpxuc.cbb.SelectedValuePath = "MAHANG";

            //create a ctpx and add it to list
            CTPX ct = new CTPX();
            listCTPX.Add(ct);

            //bind MAHANG to item
            var binding = new Binding();
            binding.Source = ct;
            binding.Path = new PropertyPath("MAHANG");
            ctpxuc.cbb.SetBinding(ComboBox.SelectedValueProperty, binding);

            //bind SOLUONG
            var bindSOLUONG = new Binding();
            bindSOLUONG.Source = ct;
            bindSOLUONG.Path = new PropertyPath("SOLUONG");
            ctpxuc.sl.SetBinding(TextBox.TextProperty, bindSOLUONG);
        }

        private void ThemPhieuXuat(object sender, RoutedEventArgs e)
        {
            
            CTPX[] arr_ctpx = listCTPX.ToArray();
            decimal tongtien = 0;
            foreach(CTPX ct in arr_ctpx)
            {
                tongtien += (decimal)(listMatHang.ToList().Find(x => x.MAHANG == ct.MAHANG).DONGIA * ct.SOLUONG);
            }

            PHIEUXUATHANG px = new PHIEUXUATHANG();
            px.MADL = this.madl;
            px.NGAYLAP = DateTime.Now;
            px.TONGTIEN = tongtien;
            px.SOTIENTRA = Decimal.Parse(txtSoTienTra.Text);
            px.CONLAI = tongtien - px.SOTIENTRA;
            px.NGUOIXUAT = 1;
            /////////////////if sotientra !=0 nhảy sang tab thu tiền
            //////////////// if còn lại >0, update số nợ đại lý += còn lại
            if (pxbus.insertPhieuXuat(arr_ctpx, px))
            {
                MessageBox.Show("Đã thêm thành công");
            }
            else
                MessageBox.Show("Có lỗi xảy ra");
        }



        // to do
        // CRUD PX-CTPX 2 trong 1
        // hàng nào chọn rồi thì hủy trong list danh sách cbb sau, ko cho chọn nữa
        // if (số tiền trả > 0) then nhảy sang tab tạo phiếu thu tiền

    }
}
