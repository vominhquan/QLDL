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
        // Min
        private static readonly DependencyProperty _Min =
            DependencyProperty.Register
            (
                "Min",
                typeof(double),
                typeof(Input),
                new FrameworkPropertyMetadata(double.MinValue)
            );
        public double Min
        {
            get { return (double)GetValue(_Min); }
            set { SetValue(_Min, value); }
        }
    }
}
