using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class TextBlock : UserControl
    {
        // Icon
        private static readonly DependencyProperty _Icon =
            DependencyProperty.Register
            (
                "Icon",
                typeof(String),
                typeof(TextBlock),
                new FrameworkPropertyMetadata("")
            );
        public string Icon
        {
            get { return (String)GetValue(_Icon); }
            set { SetValue(_Icon, value); }
        }
    }
}
