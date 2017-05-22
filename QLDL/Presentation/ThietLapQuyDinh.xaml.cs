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
using System.Text.RegularExpressions;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ThietLapQuyDinh.xaml
    /// </summary>
    public partial class ThietLapQuyDinh : Window
    {
        Regex regex = new Regex("[0-9]");
        public ThietLapQuyDinh()
        {
            InitializeComponent();

            rbChange.IsChecked = true;
            cbBoxLoaiDL.ItemsSource = BUSView.Instance.GetAllLoaiDL();
            cbBoxQuan.ItemsSource = BUSView.Instance.GetAllQuan();
            cbDVT.ItemsSource = BUSView.Instance.GetAllDVT();
        }

        /// <summary>
        /// Them loai Daily
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuuDL_Click(object sender, RoutedEventArgs e)
        {
            if (rbIn.IsChecked == true)
            {
                if (txbLoai.Text == "" || !regex.IsMatch(txbNo.Text))
                {
                    MessageBox.Show("Gia tri khong hop le");
                }
                else
                {
                    int no = Int32.Parse(txbNo.Text);
                    if (BUSThietLapQuyDinh.Instance.InsertLoaiDL(txbLoai.Text, no))
                    {
                        MessageBox.Show("Them loai dai li thanh cong");
                        txbLoai.Clear();
                        txbNo.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Them that bai");
                    }
                }
            }
            else if(rbChange.IsChecked == true)
            {
                if (cbBoxLoaiDL == null || !regex.IsMatch(txbNo.Text))
                {
                    MessageBox.Show("Gia tri khong hop le");
                }
                else
                {
                    int no = Int32.Parse(txbNo.Text);
                    if (BUSThietLapQuyDinh.Instance.ChangeMaxNumOfTienNo((int)cbBoxLoaiDL.SelectedValue,no))
                    {
                        MessageBox.Show("Thay doi thanh cong");
                    }
                    else
                    {
                        MessageBox.Show("Thay doi that bai");
                    }
                }
            }
        }


        /// <summary>
        /// Thay doi so Dl toi da trong 1quan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LuuQuan_Click(object sender, RoutedEventArgs e)
        {
            if (!regex.IsMatch(SoDL.Text) || cbBoxQuan.SelectedValue == null)
            {
                MessageBox.Show("Gia tri khong hop le");
            }
            else
            {
                int dl = Int32.Parse(SoDL.Text);
                if (BUSThietLapQuyDinh.Instance.ChangeNumOfDaily((int)cbBoxQuan.SelectedValue,dl))
                {
                    MessageBox.Show("OK");
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }


        /// <summary>
        /// Tang so loai mat hang
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuuMH_Click(object sender, RoutedEventArgs e)
        {
            if (txbTenMH.Text =="" || !regex.IsMatch(txbDonGia.Text) || cbDVT.SelectedValue == null)
            {
                MessageBox.Show("Gia tri khong hop le");
            }
            else
            {
                int dongia = Int32.Parse(txbDonGia.Text);
                if (BUSThietLapQuyDinh.Instance.InsertMATHANG(txbTenMH.Text,(int)cbDVT.SelectedValue,dongia))
                {
                    MessageBox.Show("OK");
                    txbTenMH.Clear();
                    txbDonGia.Clear();
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }


        /// <summary>
        /// Tang so loai DVT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLuuDVT_Click(object sender, RoutedEventArgs e)
        {
            if (txbTenDVT.Text == "")
            {
                MessageBox.Show("Gia tri khong hop le");
            }
            else
            {
                if (BUSThietLapQuyDinh.Instance.InsertDVT(txbTenDVT.Text))
                {
                    MessageBox.Show("OK");
                    txbTenDVT.Clear();
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            txbNo.Clear();
            cbBoxLoaiDL.IsEnabled = false;
            txbLoai.IsEnabled = true;
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            txbLoai.Clear();
            cbBoxLoaiDL.IsEnabled = true;
            txbLoai.IsEnabled = false;
        }

    }
}
