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
        // Value
        private static readonly DependencyProperty _Value =
            DependencyProperty.Register
            (
                "Value",
                typeof(string),
                typeof(Input),
                new FrameworkPropertyMetadata
                (
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                )
            );
        public string Value
        {
            get { return GetValue(_Value) as string; }
            set { SetValue(_Value, Value); }
        }
    }
}
