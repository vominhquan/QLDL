using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Paint.Components
{
    public partial class Input : UserControl
    {
        // Label
        private static readonly DependencyProperty _Label =
            DependencyProperty.Register
            (
                "Label",
                typeof(string),
                typeof(Input),
                new FrameworkPropertyMetadata("")
            );
        public string Label
        {
            get { return (string)GetValue(_Label); }
            set { SetValue(_Label, value); }
        }
    }
}
