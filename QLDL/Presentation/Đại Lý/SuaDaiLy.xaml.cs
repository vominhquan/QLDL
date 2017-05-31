using QLDL.BusinessLogic;
using QLDL.Class;
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
    /// Interaction logic for SuaDaiLy.xaml
    /// </summary>
    public partial class SuaDaiLy : Window
    {
        public SuaDaiLy(vwDAILY_LOAIDL_QUAN View)
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += DPI.Initialize;
            DataContext = new State()
            {
                DaiLy = View
            };
        }
        private class State
        {
            #region Đại lý
            private vwDAILY_LOAIDL_QUAN daiLy;
            public vwDAILY_LOAIDL_QUAN DaiLy { get => daiLy; set => daiLy = value; }
            #endregion

            #region List
            public ObservableCollection<LOAIDL> LoaiDL {
                get => new DaiLyBUS().GetAllLoaiDL();
            }
            public ObservableCollection<QUAN> Quan
            {
                get => new DaiLyBUS().GetAllQuan();
            }
            #endregion
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn muốn sửa thông tin?", "Xác nhận thêm", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (new DaiLyBUS().UpdateDaiLy(((State)DataContext).DaiLy))
                {
                    MessageBox.Show("Đã sửa thành công");
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra");
                }
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
