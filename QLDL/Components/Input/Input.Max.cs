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
        // Max
        private static readonly DependencyProperty _Max =
            DependencyProperty.Register
            (
                "Max",
                typeof(double),
                typeof(Input),
                new FrameworkPropertyMetadata(double.MaxValue)
            );
        public double Max
        {
            get { return (double)GetValue(_Max); }
            set { SetValue(_Max, value); }
        }
    }
}
