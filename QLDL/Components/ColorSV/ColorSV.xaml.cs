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
    /// Interaction logic for ColorSV.xaml
    /// </summary>
    public partial class ColorSV : UserControl
    {
        public ColorSV()
        {
            InitializeComponent();
        }

        private void PickColor(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                BindingSaturation.Text = Math.Floor(e.GetPosition((Canvas)sender).X * 100.0 / ActualWidth).ToString();
                BindingValue.Text = (100 - Math.Floor(e.GetPosition((Canvas)sender).Y * 100.0 / ActualHeight)).ToString();
            }
        }
    }
}
