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
        // Value [ String, Number ]
        private static readonly DependencyProperty _Type =
            DependencyProperty.Register
            (
                "Type",
                typeof(string),
                typeof(Input),
                new FrameworkPropertyMetadata("String")
            );
        public string Type
        {
            get { return GetValue(_Type) as string; }
            set { SetValue(_Type, _Type); }
        }
    }
}
