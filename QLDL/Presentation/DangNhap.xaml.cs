using QLDL.BusinessLogic;
using QLDL.Class;
using QLDL.DataAccess;
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

namespace QLDL.Presentation
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class DangNhap : Window
    {
        public DangNhap()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new TAIKHOAN()
            {
                TENDANGNHAP = "admin",
                PASSWORD = "admin"
            };
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            int? MANV = new NhanVienBUS().KiemTraDangNhap((DataContext as TAIKHOAN));
            if(MANV == null)
            {
                MessageBox.Show("Thông tin đăng nhập sai");
            }
            else
            {
                Application.Current.Resources.Add("MANV", MANV);
                Menu menu = new Menu()
                {
                    Owner = this
                };
                Hide();
                menu.ShowDialog();
            }
        }
    }
}
