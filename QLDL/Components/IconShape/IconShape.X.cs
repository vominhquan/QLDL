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
        // X
        private static readonly DependencyProperty _XSize =
            DependencyProperty.Register
            (
                "XSize",
                typeof(double),
                typeof(IconShape),
                new FrameworkPropertyMetadata(
                    (double)0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPropertyChanged
                )
            );
        public double XSize
        {
            get { return (double)GetValue(_XSize); }
            set { SetValue(_XSize, value); }
        }
    }
}
