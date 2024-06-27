using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ParameterSpecification
    {
        public string Name { get; set; }
        public string DescriptiveName { get; set; }
        public bool IsRegex { get; set; }
        public string Regex { get; set; }
        public bool IsCategorical { get; set; }
        public string[] Categories { get; set; }
        public bool IsNumeric { get; set; }
        public double[] BoundsAndStep { get; set; }
        public Type? Type { get; set; }
        public ParameterSpecification()
        {
            Name = "";
            DescriptiveName = "";
            IsRegex = false;
            Regex = "";
            IsCategorical = false;
            Categories = new string[] { };
            IsNumeric = false;
            BoundsAndStep = new double[] { };
            Type = null;
        }
    }
}
