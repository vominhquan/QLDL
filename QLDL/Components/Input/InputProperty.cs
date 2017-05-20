using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Paint
{
    public static class InputProperty
    {
        public static readonly DependencyProperty IsNumber = 
            DependencyProperty.RegisterAttached(
                "IsNumber",
                typeof(bool),
                typeof(InputProperty),
                new FrameworkPropertyMetadata(true)
            );
        //public bool IsNumber
        //{
        //    get { return GetValue(_Value) as bool; }
        //    set { SetValue(_Value, Value); }
        //}
    }
}
