﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Applications.Components
{
    public partial class Input : UserControl
    {
        // Background
        private static readonly DependencyProperty _Background =
            DependencyProperty.Register
            (
                "Background",
                typeof(SolidColorBrush),
                typeof(Input),
                new FrameworkPropertyMetadata(
                    (SolidColorBrush) Application.Current.Resources["BackgroundSolid"]
                )
            );
        public new SolidColorBrush Background
        {
            get { return (SolidColorBrush)GetValue(_Background); }
            set { SetValue(_Background, value); }
        }
    }
}
