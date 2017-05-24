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
//using Microsoft.Reporting.WinForms;
using QLDL.DataAccess;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for TiepNhanDaiLy.xaml
    /// </summary>
    public partial class XemDaiLy : Window, IDisposable
    {
        public vwDAILY_LOAIDL_QUAN vw { get; set; }

        public XemDaiLy(vwDAILY_LOAIDL_QUAN vw)
        {
            InitializeComponent();
            this.vw = vw;
        }

        public void Dispose()
        {

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void XuatHang(object sender, RoutedEventArgs e)
        {
            PhieuXuat px = new PhieuXuat(vw);
            px.ShowDialog();
        }

        private void ThuTien(object sender, RoutedEventArgs e)
        {
            PhieuThu pt = new PhieuThu(vw);
            pt.ShowDialog();
        }

        private void XemPhieuXuat(object sender, RoutedEventArgs e)
        {
            DSPX px = new DSPX(vw);
            px.ShowDialog();
        }

        private void XemPhieuThu(object sender, RoutedEventArgs e)
        {
            DSPT pt = new DSPT(vw);
            pt.ShowDialog();
        }


    }
}
