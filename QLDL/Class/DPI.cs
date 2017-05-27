using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace QLDL.Class
{
    class DPI
    {
        public static Point Initialize(object sender, RoutedEventArgs e)
        {
            PresentationSource PresentationSource = PresentationSource.FromVisual(Application.Current.MainWindow);
            Matrix DpiFactorMatrix = PresentationSource.CompositionTarget.TransformToDevice;

            double DpiWidthFactor = DpiFactorMatrix.M11;
            double DpiHeightFactor = DpiFactorMatrix.M22;

            double scaleX = 1 / DpiWidthFactor;
            double scaleY = 1 / DpiHeightFactor;

            return new Point(scaleX, scaleY);

            // ((Presentation.Presentation)Application.Current.MainWindow).Main.LayoutTransform = new ScaleTransform(scaleX, scaleY);
        }
    }
}
