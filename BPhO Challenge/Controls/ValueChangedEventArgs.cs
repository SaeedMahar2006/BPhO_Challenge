using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPhO_Challenge.Controls
{
    public class ValueChangedEventArgs<T> : EventArgs
    {
        public T Value { get; set; }
        public ValueChangedEventArgs(T Value)
        {
            this.Value = Value;
        }
    }
}
