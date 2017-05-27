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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Applications.Components
{
    /// <summary>
    /// Interaction logic for button.xaml
    /// </summary>
    public partial class Button : UserControl
    {
        public Button()
        {
            InitializeComponent();
            this.Loaded += Button_Loaded;
            
        }

        private void Button_Loaded(object sender, RoutedEventArgs e)
        {
            Size = ActualHeight;
        }

        public event RoutedEventHandler Click;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}       
