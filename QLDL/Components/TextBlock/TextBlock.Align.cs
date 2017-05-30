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
        // Align
        private static readonly DependencyProperty _Align =
            DependencyProperty.Register
            (
                "Align",
                typeof(string),
                typeof(TextBlock),
                new FrameworkPropertyMetadata("Left")
            );
        public string Align
        {
            get { return (string)GetValue(_Align); }
            set { SetValue(_Align, value); }
        }
    }
}
