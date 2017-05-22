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
using BUS;
using QLDL.DataAccess;
using QLDL.Presentation;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ViewNhanVien.xaml
    /// </summary>
    public partial class ViewNhanVien : Window
    {
        public ViewNhanVien()
        {
            InitializeComponent();
            gridAdvanced.Visibility = Visibility.Collapsed;
            cbChucvu.ItemsSource = BUSView.Instance.GetAllCV();
        }

        /// <summary>
        /// Cancel form;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Set enable or disable advance search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            gridAdvanced.Visibility = Visibility.Visible;
        }
        private void HandleUnchecked(object sender, RoutedEventArgs e)
        {
            gridAdvanced.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Search worker by Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTim_Click(object sender, RoutedEventArgs e)
        {
            if(cbChucvu.SelectedItem == null)
            {
                lv.ItemsSource = BUSView.Instance.GetWorkerByName(txbTen.Text, 0, txbDC.Text);
            }
            else
            {
                lv.ItemsSource = BUSView.Instance.GetWorkerByName(txbTen.Text, (int)cbChucvu.SelectedValue, txbDC.Text);
            }
            cbChucvu.SelectedItem = null;
            txbDC.Clear();
        }

        private void Xoa_Click(object sender, RoutedEventArgs e)
        {
            if(lv.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên muốn xóa");
            }
            else
            {
                int ma = (lv.SelectedItem as vwCHUCVU_NHANVIEN_TAIKHOAN).MANV;
                MessageBoxResult result = MessageBox.Show("Bạn muốn xóa nhân viên đã chọn?", "Xác nhận xóa", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                    if (BUSQLNhanVien.Instance.Delete(ma))
                        MessageBox.Show("Đã xóa thành công");
                    else
                        MessageBox.Show("Có lỗi xảy ra");
            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lv.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn 1 nhân viên muốn sửa");
            }
            else
            {
                EditNhanVien edit = new EditNhanVien(lv.SelectedItem as vwCHUCVU_NHANVIEN_TAIKHOAN);
                edit.Show();
            }
        }
    }
}

