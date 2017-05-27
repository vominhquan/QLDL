using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Applications.Components
{
    public partial class IconShape : Shape
    {
        // YSize
        private static readonly DependencyProperty _YSize =
            DependencyProperty.Register
            (
                "YSize",
                typeof(double),
                typeof(IconShape),
                new FrameworkPropertyMetadata(
                    (double)0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPropertyChanged
                )
            );
        public double YSize
        {
            get { return (double)GetValue(_YSize); }
            set { SetValue(_YSize, value); }
        }
    }
}
