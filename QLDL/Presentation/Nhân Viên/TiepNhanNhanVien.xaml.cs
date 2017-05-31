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
    /// Interaction logic for TiepNhanNhanVien.xaml
    /// </summary>
    public partial class TiepNhanNhanVien : Window
    {
        public TiepNhanNhanVien()
        {
            InitializeComponent();

            cbxChucvu.ItemsSource = BUSView.Instance.GetAllCV();
        }

        void Clear()
        {
            txbTen.Clear();
            txbDc.Clear();
            txbAcc.Clear();
            txbPass.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txbAcc.Text == "" || txbPass.Password == "" || txbTen.Text == "" || txbDc.Text == "" 
                || dpNgay.SelectedDate == null || cbxChucvu.SelectedValue == null)
            {
                MessageBox.Show("Value is empty");
            }
            else
            {
                if (BUSQLNhanVien.Instance.Insert(txbTen.Text, dpNgay.SelectedDate.Value, txbDc.Text,(int)cbxChucvu.SelectedValue, txbAcc.Text, txbPass.Password))
                {
                    MessageBox.Show("OK");
                    Clear();
                }
                else
                {
                    MessageBox.Show("Fail");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
