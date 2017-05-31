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
    /// Interaction logic for PhieuThu.xaml
    /// </summary>
    public partial class PhieuThu : Window
    {
        //public vwDAILY_LOAIDL_QUAN vw { get; set; }
        //private PhieuThuBUS ptbus = new PhieuThuBUS();

        public PhieuThu(vwDAILY_LOAIDL_QUAN View)
        {
            InitializeComponent();
            DataContext = new State()
            {
                DaiLy = View,
                PhieuThu = new PHIEUTHUTIEN()
                {
                    MADL = View.MADL,
                    NGAYTHUTIEN = DateTime.Now,
                    NGUOITHU = 1
                }
            };
        }

        private class State
        {
            #region Đại lý
            private vwDAILY_LOAIDL_QUAN daiLy;
            public vwDAILY_LOAIDL_QUAN DaiLy { get => daiLy; set => daiLy = value; }
            #endregion

            #region Phiếu thu
            private PHIEUTHUTIEN phieuThu;
            public PHIEUTHUTIEN PhieuThu { get => phieuThu; set => phieuThu = value; }
            #endregion
        }

        private void LapPhieuThu(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Khi đã tạo không thể thay đổi", "Xác nhận", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if(new PhieuThuBUS().insertPhieuThu(((State)DataContext).PhieuThu))
                {
                    MessageBox.Show("Đã thêm thành công");
                    ((State)DataContext).DaiLy.SONO -=
                        ((State)DataContext).PhieuThu.SOTIEN;
                    DialogResult = true;
                }
                else
                    MessageBox.Show("Có lỗi xảy ra");
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
