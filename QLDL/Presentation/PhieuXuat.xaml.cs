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
    /// Interaction logic for PhieuXuat.xaml
    /// </summary>
    /// 


    public partial class PhieuXuat : Window
    {
        public vwDAILY_LOAIDL_QUAN Vwdl { get; set; }
        private ObservableCollection<MATHANG> listMatHang;
        public ICollectionView collectionView;
        public string searchstring;
        public Predicate<object> searchFilter;
        private MatHangBUS mhbus = new MatHangBUS();
        private PhieuXuatBUS pxbus = new PhieuXuatBUS();
        private PhieuThuBUS ptbus = new PhieuThuBUS();
        private ObservableCollection<CTPX> listCTPX;
        private ObservableCollection<CTPXUserControl> listUserControl;

        public PhieuXuat(vwDAILY_LOAIDL_QUAN vwdl)
        {
            InitializeComponent();
            this.Vwdl = vwdl;

            // Lấy dữ liệu ban đầu
            InitialData();

            
        }

        private void InitialData()
        {                        
            // tab xuất hàng
            listMatHang = mhbus.getAllMatHang();
            listCTPX = new ObservableCollection<CTPX>();
            listUserControl = new ObservableCollection<CTPXUserControl>();
            CreateCTPX();
        }

        #region Report

        private void OpenReport(object sender, RoutedEventArgs e)
        {
            //ReportPreview rp = new ReportPreview();
            //rp.Show();
        }

        #endregion



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

            //get cbb data
            ctpxuc.cbb.ItemsSource = listMatHang;
            ctpxuc.cbb.DisplayMemberPath = "TENHANG";
            ctpxuc.cbb.SelectedValuePath = "MAHANG";

            //create a ctpx and add it to list
            CTPX ct = new CTPX()
            {
                SOLUONG = 1
            };
            listCTPX.Add(ct);

            //bind MAHANG to item
            var binding = new Binding()
            {
                Source = ct,
                Path = new PropertyPath("MAHANG")
            };
            ctpxuc.cbb.SetBinding(ComboBox.SelectedValueProperty, binding);

            //bind SOLUONG
            var bindSOLUONG = new Binding()
            {
                Source = ct,
                Path = new PropertyPath("SOLUONG")
            };
            ctpxuc.sl.SetBinding(TextBox.TextProperty, bindSOLUONG);
        }

        private void ThemPhieuXuat(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Khi đã tạo không thể thay đổi", "Xác nhận", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                CTPX[] arr_ctpx = listCTPX.ToArray();
                decimal tongtien = 0;
                foreach (CTPX ct in arr_ctpx)
                {
                    tongtien += (decimal)(listMatHang.ToList().Find(x => x.MAHANG == ct.MAHANG).DONGIA * ct.SOLUONG);
                }

                PHIEUXUATHANG px = new PHIEUXUATHANG()
                {
                    MADL = this.Vwdl.MADL,
                    NGAYLAP = DateTime.Now,
                    TONGTIEN = tongtien,
                    SOTIENTRA = Decimal.Parse(txtSoTienTra.Text)
                };
                px.CONLAI = tongtien - px.SOTIENTRA;
                if (px.CONLAI < 0) px.CONLAI = 0;
                px.NGUOIXUAT = 1;
                /////////////////if sotientra !=0 nhảy sang tab thu tiền
                if (pxbus.insertPhieuXuat(arr_ctpx, px))
                {
                    MessageBox.Show("Đã thêm thành công");
                    Vwdl.SONO += px.CONLAI;
                    this.DialogResult = true;
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }

        }

        // to do
        // CRUD PX-CTPX 2 trong 1
        // hàng nào chọn rồi thì hủy trong list danh sách cbb sau, ko cho chọn nữa
        // if (số tiền trả > 0) then nhảy sang tab tạo phiếu thu tiền

    }
}
