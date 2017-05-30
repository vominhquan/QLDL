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
using QLDL.Class;

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for TiepNhanDaiLy.xaml
    /// </summary>
    public partial class XemDaiLy : Window
    {
        public string ReturnValue;
        public XemDaiLy(vwDAILY_LOAIDL_QUAN View)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = View;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            ReturnValue = "Edit";
            Close();
        }

        private void XuatHang(object sender, RoutedEventArgs e)
        {
            //PhieuXuat px = new PhieuXuat(VW);
            //px.ShowDialog();
        }

        private void ThuTien(object sender, RoutedEventArgs e)
        {
            //PhieuThu pt = new PhieuThu(VW);
            //pt.ShowDialog();
        }

        private void XemPhieuXuat(object sender, RoutedEventArgs e)
        {
            (new DanhSachPhieuXuat((DataContext as vwDAILY_LOAIDL_QUAN).MADL)).ShowDialog();
        }

        private void XemPhieuThu(object sender, RoutedEventArgs e)
        {
            (new DanhSachPhieuThu((DataContext as vwDAILY_LOAIDL_QUAN).MADL)).ShowDialog();
        }
    }
}
