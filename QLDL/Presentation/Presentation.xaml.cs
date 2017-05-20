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
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class Presentation : Window
    {
        public Presentation()
        {
            InitializeComponent();
            Application.Current.MainWindow.Loaded += Class.DPI.Initialize;
        }

        private void Input_PasswordChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }
    }
}
