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
        // Value
        private static readonly DependencyProperty _Value =
            DependencyProperty.Register
            (
                "Value",
                typeof(string),
                typeof(Input),
                new FrameworkPropertyMetadata
                (
                    string.Empty,
                    FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    OnValueChange
                )
            );
        public string Value
        {
            get { return GetValue(_Value) as string; }
            set { SetValue(_Value, value); }
        }
        private static void OnValueChange (DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Input self = (Input)sender;
            if (self.Type == "Number")
            {
                double number;
                if (double.TryParse(self.Value, out number))
                {
                    if (number > self.Max)
                    {
                        self.Value = self.Max.ToString();
                    }
                    else if(number < self.Min)
                    {
                        self.Value = self.Min.ToString();
                    }
                }
                else
                {
                    //
                }
            }
        }
    }
}
