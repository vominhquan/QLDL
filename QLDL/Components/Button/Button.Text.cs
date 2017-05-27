using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class Button : UserControl
    {
        // Text
        private static readonly DependencyProperty _Text =
            DependencyProperty.Register
            (
                "Text",
                typeof(string),
                typeof(Button),
                new FrameworkPropertyMetadata(string.Empty)
            );
        public string Text
        {
            get { return (string)GetValue(_Text); }
            set { SetValue(_Text, value); }
        }
    }
}
