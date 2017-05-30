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
            DataContext = new State()
            {
                DaiLy = View
            };
        }
        private class State : INotifyPropertyChanged
        {
            #region Init INotifyPropertyChanged
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string name)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
            #endregion

            #region Đại lý
            private vwDAILY_LOAIDL_QUAN daiLy;
            public vwDAILY_LOAIDL_QUAN DaiLy {
                get => daiLy;
                set => daiLy = value;
            }
            #endregion
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        private void Edit(object sender, RoutedEventArgs e)
        {
            if((bool)new SuaDaiLy(((State)DataContext).DaiLy).ShowDialog())
            {
                int MADL = ((State)DataContext).DaiLy.MADL;
                ((State)DataContext).DaiLy = new DaiLyBUS().GetDaiLyByMADL(MADL);
            }
        }

        private void XuatHang(object sender, RoutedEventArgs e)
        {
            //new PhieuXuat((DataContext as State).DaiLy)
            //    .ShowDialog();
        }

        private void ThuTien(object sender, RoutedEventArgs e)
        {
            if ((bool)new PhieuThu(((State)DataContext).DaiLy).ShowDialog())
            {
                ((State)DataContext).DaiLy = new DaiLyBUS().GetDaiLyByMADL(((State)DataContext).DaiLy.MADL);
            };
        }

        private void XemPhieuXuat(object sender, RoutedEventArgs e)
        {
            int MADL = (DataContext as State).DaiLy.MADL;
            new DanhSachPhieuXuat(MADL).ShowDialog();
        }

        private void XemPhieuThu(object sender, RoutedEventArgs e)
        {
            int MADL = (DataContext as State).DaiLy.MADL;
            new DanhSachPhieuThu(MADL).ShowDialog();
        }
    }
}
