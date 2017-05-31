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
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : UserControl
    {
        public Input()
        {
            InitializeComponent();
            Loaded += Input_Loaded;
        }

        private void Input_Loaded(object sender, RoutedEventArgs e)
        {
            Height = ActualHeight - (Border.Top + Border.Bottom);
        }

        public event TextChangedEventHandler TextChanged;

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextChanged?.Invoke(this, e);
        }
    }
}
