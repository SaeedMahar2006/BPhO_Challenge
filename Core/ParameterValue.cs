using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ParameterValue<T>
    {
        public ParameterSpecification Specification { get; set; }
        public T Value { get; set; }
        public ParameterValue(ParameterSpecification specification, T value) {
            if (typeof(T)!=Specification.Type&&Specification.Type!=null)
            {
                throw new ArgumentException("Need specification to declare same type");
            }
            Specification = specification;
            Value = value;
        }
        public void Set(T value)
        {
            Value = value;
        }
    }
}
