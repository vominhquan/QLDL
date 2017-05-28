using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Applications.Components
{
    public partial class Button : UserControl
    {
        // BG
        private static readonly DependencyProperty _BG =
            DependencyProperty.Register
            (
                "BG",
                typeof(Color),
                typeof(Button),
                new FrameworkPropertyMetadata(
                       Color.FromRgb(255,0,0)
                    // (Color)Application.Current.Resources["GreenColor"]
                    
                )
            );
        public Color BG
        {
            get { return (Color)GetValue(_BG); }
            set { SetValue(_BG, value); }
        }
    }
}
