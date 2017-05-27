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
    /// Interaction logic for Line.xaml
    /// </summary>
    public partial class Line : UserControl
    {
        public Line()
        {
            InitializeComponent();
        }
        private void ControlLoaded(object sender, RoutedEventArgs e)
        {
            if (this.Parent == null || this.Parent.GetType() != typeof(StackPanel))
            {
                return;
            }
            if ((this.Parent as StackPanel).Orientation == Orientation.Horizontal)
            {
                this.Child.Height = Double.NaN;
            }
            else
            {
                this.Child.Width = Double.NaN;
            }
        }
    }
}
