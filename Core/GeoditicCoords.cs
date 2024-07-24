using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class GeoditicCoords
    {
        public double Latitude;
        public double Longtitude {
            get { return Longtitude; } 
            set {  }
        }

        public double Altitude { get; set; }
        public GeoditicCoords() { }
    }
}
