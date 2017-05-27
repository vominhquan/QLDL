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
        // Saturation
        private static readonly DependencyProperty _Saturation =
            DependencyProperty.Register
            (
                "Saturation",
                typeof(double),
                typeof(ColorSV),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnSChange
                )
            );

        private static void OnSChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (double.TryParse(e.NewValue.ToString(), out double BindingS))
            {
                Canvas.SetLeft(
                    ((ColorSV)sender).Picker, ((ColorSV)sender).ActualWidth * BindingS / 100 - 10
                );
            }
        }

        public double Saturation
        {
            get { return (double)GetValue(_Saturation); }
            set { SetValue(_Saturation, Saturation); }
        }
    }
}
