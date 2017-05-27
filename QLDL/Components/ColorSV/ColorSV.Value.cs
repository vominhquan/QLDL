using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class ColorSV : UserControl
    {
        // Value
        private static readonly DependencyProperty _Value =
            DependencyProperty.Register
            (
                "Value",
                typeof(double),
                typeof(ColorSV),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnVChange
                )
            );
        private static void OnVChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (double.TryParse(e.NewValue.ToString(), out double BindingV))
            {
                Canvas.SetTop(
                    ((ColorSV)sender).Picker, ((ColorSV)sender).ActualHeight * (1 - BindingV / 100) - 10
                );
            }
        }
        public double Value
        {
            get { return (double)GetValue(_Value); }
            set { SetValue(_Value, value); }
        }
    }
}
