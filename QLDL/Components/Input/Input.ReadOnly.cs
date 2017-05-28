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
        private static readonly DependencyProperty _ReadOnly =
            DependencyProperty.Register
            (
                "ReadOnly",
                typeof(bool),
                typeof(Input),
                new FrameworkPropertyMetadata(false)
            );
        public bool ReadOnly
        {
            get { return (bool)GetValue(_ReadOnly); }
            set { SetValue(_ReadOnly, value); }
        }
    }
}
