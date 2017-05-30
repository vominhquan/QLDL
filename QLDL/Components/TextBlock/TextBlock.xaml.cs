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
    /// Interaction logic for TextBlock.xaml
    /// </summary>
    public partial class TextBlock : UserControl
    {
        public TextBlock()
        {
            InitializeComponent();
            this.Loaded += TextBlock_Loaded;
            
        }

        private void TextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            Size = ActualHeight;
        }
    }
}       
