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
    public partial class Input : UserControl
    {
        // Overlay
        private static readonly DependencyProperty _Overlay =
            DependencyProperty.Register
            (
                "Overlay",
                typeof(SolidColorBrush),
                typeof(Input),
                new FrameworkPropertyMetadata(
                    (SolidColorBrush)Application.Current.Resources["OverlaySolid"]
                )
            );
        public SolidColorBrush Overlay
        {
            get { return (SolidColorBrush)GetValue(_Overlay); }
            set { SetValue(_Overlay, value); }
        }
    }
}
