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
    /// Interaction logic for ChiTietPhieuXuat.xaml
    /// </summary>
    public partial class ChiTietPhieuXuat : Window
    {

        public vw_PhieuXuat_NhanVien_DaiLy vwPX { get; set; }
        private ObservableCollection<vw_PhieuXuat_CTPX_MatHang> listCTPX;
        private PhieuXuatBUS pxbus = new PhieuXuatBUS();


        public ChiTietPhieuXuat(vw_PhieuXuat_NhanVien_DaiLy px)
        {
            InitializeComponent();
            this.vwPX = px;

            //get data to list
            listCTPX = pxbus.getCTPXPhieuXuatByMaPhieu(px.MAPHIEU);

            // get datalist to UI
            lsvCTPX.ItemsSource = listCTPX;

            //MessageBox.Show(px.MAPHIEU.ToString());

            //foreach(vw_PhieuXuat_CTPX_MatHang item in listCTPX ){
            //    MessageBox.Show(item.TENHANG.ToString());
            //}
        }


    }
}
