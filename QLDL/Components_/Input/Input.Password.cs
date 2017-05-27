using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QLDL.Components
{
    public partial class Input : UserControl
    {
        // Password
        private static readonly DependencyProperty _Password =
            DependencyProperty.Register
            (
                "Password",
                typeof(bool),
                typeof(Input),
                new FrameworkPropertyMetadata(false)
            );
        public bool Password
        {
            get { return (bool)GetValue(_Password); }
            set { SetValue(_Password, Password); }
        }
    }
}
