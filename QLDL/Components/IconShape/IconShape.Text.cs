using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Applications.Components
{
    public partial class IconShape : Shape
    {
        private static readonly DependencyProperty _Text =
            DependencyProperty.Register(
                "Text",
                typeof(string),
                typeof(IconShape),
                new FrameworkPropertyMetadata(
                    ".",
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnPropertyChanged
                )
            );
        public string Text
        {
            get { return (string)GetValue(_Text); }
            set { SetValue(_Text, value); }
        }
    }
}
