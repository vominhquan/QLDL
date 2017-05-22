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
            cbcv.ItemsSource = BUSView.Instance.GetAllCV();
        }

        /// <summary>
        /// Update nhan vien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (cbcv.SelectedValue == null || txbTen.Text == "" || txbDC.Text == "")
            {
                MessageBox.Show("Gia tri khong hop le");
            }
            else
            {
                int manv = Int32.Parse(txbMa.Text);
                if (BUSQLNhanVien.Instance.Update(manv, txbTen.Text, dpNS.SelectedDate.Value, txbDC.Text, (int)cbcv.SelectedValue))
                {
                    MessageBox.Show(cbcv.SelectedValue.ToString());
                }
                else
                {
                    MessageBox.Show("Upadate Fail");
                }
            }
        }

        /// <summary>
        /// Delete Nhan vien
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lv.SelectedItem == null)
            {
                MessageBox.Show("Chon 1 nhan vien");
            }
            else
            {
                int manv = Int32.Parse(txbMa.Text);
                if (BUSQLNhanVien.Instance.Delete(manv))
                {
                    MessageBox.Show("Delete OK");
                }
                else
                {
                    MessageBox.Show("Delete Fail");
                }
            }
        }

        /// <summary>
        /// View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (cbcv.SelectedValue == null)
            {
                MessageBox.Show("Khong hop le");
            }
            else
            {
                lv.ItemsSource = BUSQLNhanVien.Instance.Search((int)cbcv.SelectedValue, txbTen.Text, txbDC.Text);
            }
        }
    }
}

