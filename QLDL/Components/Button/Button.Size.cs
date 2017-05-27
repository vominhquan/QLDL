using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class Button : UserControl
    {
        // Size
        private static readonly DependencyProperty _Size =
            DependencyProperty.Register
            (
                "Size",
                typeof(double),
                typeof(Button),
                new FrameworkPropertyMetadata((double)Application.Current.Resources["Size"])
            );
        public double Size
        {
            get { return (double)GetValue(_Size); }
            set { SetValue(_Size, value); }
        }
    }
}
