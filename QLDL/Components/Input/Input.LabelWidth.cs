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
        // LabelWidth
        private static readonly DependencyProperty _LabelWidth =
            DependencyProperty.Register
            (
                "LabelWidth",
                typeof(double),
                typeof(Input),
                new FrameworkPropertyMetadata(double.NaN)
            );
        public double LabelWidth
        {
            get { return (double)GetValue(_LabelWidth); }
            set { SetValue(_LabelWidth, value); }
        }
    }
}
