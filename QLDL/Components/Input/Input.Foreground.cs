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
        // Foreground
        private static readonly DependencyProperty _Foreground =
            DependencyProperty.Register
            (
                "Foreground",
                typeof(SolidColorBrush),
                typeof(Input),
                new FrameworkPropertyMetadata(
                    (SolidColorBrush)Application.Current.Resources["ForegroundSolid"]
                )
            );
        public new SolidColorBrush Foreground
        {
            get { return (SolidColorBrush)GetValue(_Foreground); }
            set { SetValue(_Foreground, value); }
        }
    }
}
