using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class Input : UserControl
    {
        // Text
        private static readonly DependencyProperty _Border =
            DependencyProperty.Register
            (
                "Border",
                typeof(Thickness),
                typeof(Input),
                new FrameworkPropertyMetadata(new Thickness(1))
            );
        public Thickness Border
        {
            get { return (Thickness)GetValue(_Border); }
            set { SetValue(_Border, value); }
        }
    }
}
