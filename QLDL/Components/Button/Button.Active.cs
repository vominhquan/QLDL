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
        // Text
        private static readonly DependencyProperty _Active =
            DependencyProperty.Register
            (
                "Active",
                typeof(bool),
                typeof(Button),
                new FrameworkPropertyMetadata(false)
            );
        public bool Active
        {
            get { return (bool)GetValue(_Active); }
            set { SetValue(_Active, value); }
        }
    }
}
