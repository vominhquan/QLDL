using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class ColorSV : UserControl
    {
        // Hue
        private static readonly DependencyProperty _Hue =
            DependencyProperty.Register
            (
                "Hue",
                typeof(Color),
                typeof(ColorSV),
                new FrameworkPropertyMetadata(Color.FromRgb(255,0,0))
            );
        public Color Hue
        {
            get { return (Color)GetValue(_Hue); }
            set { SetValue(_Hue, Hue); }
        }
    }
}
