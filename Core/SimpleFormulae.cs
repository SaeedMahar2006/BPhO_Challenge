using System.Linq.Expressions;
using System.Numerics;
using ScottPlot;
namespace Core
{
    public static class SimpleFormulae
    {
//Code comments and summary provided by Codestral 



        /// <summary>
        /// 
        /// </summary>
        /// <param name="Angle">Launch angle above the horizontal in radians</param>
        /// <param name="Gravity">Gravitational strength constant</param>
        /// <param name="LaunchSpeed">Launch speed in m/s</param>
        /// <param name="LaunchHeight">launch height above horizontal</param>
        /// <param name="TimeIncrement">Time increment in seconds</param>
        /// <returns></returns>
        public static IEnumerable<Coordinates> FixedTimeIncrementNoDragHeightEnumerable(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight, double TimeIncrement = 0.01)
        {
            double h = LaunchHeight;
            double sy = 0;
            double sx;
            

            double vy = Math.Sin(Angle) * LaunchSpeed;
            double vx = Math.Cos(Angle) * LaunchSpeed;

            double t = 0;
            yield return new Coordinates(0, LaunchHeight);
            while (h > 0)
            {

                sy = vy * t + 0.5 * -Gravity * (t * t);
                h = LaunchHeight + sy;
                sx = vx * t;
                t += TimeIncrement;
                //s=ut+0.5at^2

                yield return new Coordinates(sx, h);
                if (Gravity == 0 && t / TimeIncrement > 1000) yield break; //in the edge case gravity is 0, return after only 1000 iterations
            }
            yield break;
        }


        public static double AnalyticNoDragHeight(double x, double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            return LaunchHeight + x * Math.Tan(Angle) - (Gravity) * (1 + Math.Pow(Math.Tan(Angle), 2)) * x * x / (2 * LaunchSpeed * LaunchSpeed);
        }
        public static double AnalyticNoDragRange(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            if(LaunchSpeed==0) return 0; //fix up NaN from appearing
            return LaunchSpeed * LaunchSpeed * (Math.Sin(Angle) * Math.Cos(Angle) + Math.Cos(Angle) * 
                
                Math.Sqrt( Math.Pow(Math.Sin(Angle), 2 ) + 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed))//possibility of NaN
                
                ) / Gravity;
        }
        public static Coordinates AnalyticNoDragApogee(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double x = LaunchSpeed * LaunchSpeed * Math.Sin(Angle) * Math.Cos(Angle) / Gravity;
            double y = LaunchHeight + LaunchSpeed * LaunchSpeed * Math.Sin(Angle) * Math.Sin(Angle) / (2 * Gravity);
            return new Coordinates(x, y);
        }



        public static double ThroughPointMinSpeed(double Gravity, double ThroughX, double ThroughY)
        {
            return Math.Sqrt(Gravity * (ThroughY + Math.Sqrt(ThroughX * ThroughX + ThroughY * ThroughY)));
        }

        public static double ThroughPointMinSpeedAngle(double Gravity, double ThroughX, double ThroughY)
        {
            return Math.Atan((ThroughY + Math.Sqrt(ThroughX * ThroughX + ThroughY * ThroughY)) / ThroughX);
        }



        public static double ThroughPointLowBallAngle(double Gravity, double LaunchSpeed, double ThroughX, double ThroughY, double LaunchHeight = 0)
        {
            double a = Gravity * ThroughX * ThroughX / (2 * LaunchSpeed * LaunchSpeed);
            double b = -ThroughX;
            double c = ThroughY - LaunchHeight + a;
            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0) { return double.NaN; };
            return Math.Atan((-b - Math.Sqrt(discriminant)) / (2 * a));
        }


        public static double ThroughPointHighBallAngle(double Gravity, double LaunchSpeed, double ThroughX, double ThroughY, double LaunchHeight = 0)
        {
            double a = Gravity * ThroughX * ThroughX / (2 * LaunchSpeed * LaunchSpeed);
            double b = -ThroughX;
            double c = ThroughY - LaunchHeight + a;
            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0) { return double.NaN; };
            return Math.Atan((-b + Math.Sqrt(discriminant)) / (2 * a));
        }





        public static double MaxRange(double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            return AnalyticNoDragRange(MaxRangeAngle(Gravity, LaunchSpeed, LaunchHeight), Gravity, LaunchSpeed, LaunchHeight);
        }


        public static double MaxRangeAngle(double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double alpha = 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed);
            return Math.Asin(1 / Math.Sqrt(2 + alpha));
        }


        public static double BoundingHeightAtDistanceX(double x, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double alpha = (LaunchSpeed * LaunchSpeed) / (Gravity);
            return LaunchHeight + (alpha - x * x / alpha) / 2;
        }




        private static double IntegralRequiredForLengthOfTrajectory(double x)
        {

            return (Math.Log(Math.Sqrt(1 + x * x) + x) + x * Math.Sqrt(1 + x * x)) / 2;//x>=0 so always ok 
        }

        public static double LengthOfTrajectory(double X, double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double multiplier = LaunchSpeed*LaunchSpeed*Math.Pow(Math.Cos(Angle),2)/Gravity;
            double lower = Gravity * (1 + Math.Pow(Math.Tan(Angle), 2)) * X / (LaunchSpeed * LaunchSpeed);

            return multiplier * ( IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle)) - IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle)-lower) );
        }



    }

}
