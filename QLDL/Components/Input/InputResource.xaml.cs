using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Paint
{
    public partial class InputResource
    {
        private void InputKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                ((TextBox)sender).Text = "20";
            }
        }
    }
}
