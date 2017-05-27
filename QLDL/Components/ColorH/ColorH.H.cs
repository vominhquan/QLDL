using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Applications.Components
{
    public partial class ColorH : UserControl
    {
        // Hue
        private static readonly DependencyProperty _Hue =
            DependencyProperty.Register
            (
                "Hue",
                typeof(double),
                typeof(ColorH),
                new FrameworkPropertyMetadata(
                    0.0,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnHChange
                )
            );

        private static void OnHChange(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (double.TryParse(e.NewValue.ToString(), out double BindingH))
            {
                Canvas.SetTop(
                    ((ColorH)sender).Picker, 
                    ((ColorH)sender).ActualHeight * BindingH / 360 - 8
                );
            }
        }

        public double Hue
        {
            get { return (double)GetValue(_Hue); }
            set { SetValue(_Hue, Hue); }
        }
    }
}
