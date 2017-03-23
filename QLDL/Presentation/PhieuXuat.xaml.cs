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
        private ObservableCollection<CTPX> listCTPX;
        private ObservableCollection<CTPXUserControl> listUserControl;

        public PhieuXuat()
        {
            InitializeComponent();

            listMatHang = mhbus.getAllMatHang();
            listCTPX = new ObservableCollection<CTPX>();
            listUserControl = new ObservableCollection<CTPXUserControl>();
            CreateCTPX();
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



        // to do
        // CRUD PX-CTPX 2 trong 1
        // if (số tiền trả > 0) then nhảy sang tab tạo phiếu thu tiền

    }
}
