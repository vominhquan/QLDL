using QLDL.Class;
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

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            Closing += CloseAll;
            // Application.Current.MainWindow.
        }

        private void CloseAll(object sender, CancelEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void DanhSachDaiLy(object sender, RoutedEventArgs e)
        {
            new DanhSachDaiLy().ShowDialog();
        }
        private void DanhSachPhieuXuat(object sender, RoutedEventArgs e)
        {
            new DanhSachPhieuXuat().ShowDialog();
        }
        private void DanhSachPhieuThu(object sender, RoutedEventArgs e)
        {
            new DanhSachPhieuThu().ShowDialog();
        }

        private void DanhSachNhanVien(object sender, RoutedEventArgs e)
        {
            new DanhSachNhanVien().ShowDialog();
        }

        private void ThietLapQuyDinh(object sender, RoutedEventArgs e)
        {
            new ThietLapQuyDinh().ShowDialog(); 
        }
    }
}
