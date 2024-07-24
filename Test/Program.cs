using Core;
using CoordinateSharp;
namespace Test
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var c1 = new Coordinate(51.5072, 0.1276);

            var testcase1 = new Vector(3);
            testcase1[0] = 3978.018;
            testcase1[1] = 8.859;
            testcase1[2] = 4968.869;
            testcase1 *= 1000;

            var result1 = SimpleFormulae.ECEFVectorToGeoditicAndHeightNewtonRaphson(testcase1);
            Console.WriteLine(result1.Item2);
            //Console.WriteLine(c1.ToString());
            //Console.WriteLine(c1.ECEF.X);
            //Console.WriteLine(c1.ECEF.Y);
            //Console.WriteLine(c1.ECEF.Z);
            //Console.WriteLine(c1.ECEF.GeoDetic_Height.Meters);

            //var v = SimpleFormulae.GeoditicToECEFVector(c1);
            //v.Print();
            //var c = SimpleFormulae.ECEFVectorToGeoditicAndHeightNewtonRaphson( v );
            //var c2 = c.Item1;
            //Console.WriteLine(c2.ToString());
            //Console.WriteLine(c2.ECEF.X);
            //Console.WriteLine(c2.ECEF.Y);
            //Console.WriteLine(c2.ECEF.Z);
            //Console.WriteLine(c.Item2);
            foreach (var i in SimpleFormulae.EarthSpinProjectile(c1,149.04,80,1000,10,0.0000001,1,0.01))
            {
                Console.WriteLine(i);
            }

        }
    }
}