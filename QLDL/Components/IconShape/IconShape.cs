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
        private Geometry TextGeometry;
        protected override Geometry DefiningGeometry {
            get => TextGeometry ?? Geometry.Empty;
        }

        private static void OnPropertyChanged(DependencyObject self, DependencyPropertyChangedEventArgs e)
        {
           ((IconShape)self).CreateTextGeometry();
        }
        private void CreateTextGeometry()
        {
            FormattedText FText =
                new FormattedText(
                    Text,
                    Thread.CurrentThread.CurrentUICulture,
                    FlowDirection.LeftToRight,
                    new Typeface(
                        (FontFamily)(Application.Current.Resources["IconFont"]),
                        FontStyles.Normal,
                        FontWeights.Normal,
                        FontStretches.Normal
                    ),
                    1,
                    Brushes.Black
                );
            TextGeometry = FText.BuildGeometry(new Point(0, 0));
            TextGeometry.Transform = new ScaleTransform(XSize, YSize);
            StrokeEndLineCap = PenLineCap.Triangle;
            StrokeEndLineCap = PenLineCap.Flat;

        }
    }
}
