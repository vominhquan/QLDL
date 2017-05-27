using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QLDL.Components
{
    public partial class Button : UserControl
    {
        // Icon
        private static readonly DependencyProperty _Icon =
            DependencyProperty.Register
            (
                "Icon",
                typeof(String),
                typeof(Button),
                new FrameworkPropertyMetadata("")
            );
        public string Icon
        {
            get { return (String)GetValue(_Icon); }
            set { SetValue(_Icon, value); }
        }
    }
}
