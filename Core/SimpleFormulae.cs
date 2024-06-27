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


    /// <summary>
    /// Calculates the minimum initial speed required for a projectile to reach a specific point (ThroughX, ThroughY)
    /// under the influence of gravity (Gravity).
    /// </summary>
    /// <param name="Gravity">The acceleration due to gravity (m/s²).</param>
    /// <param name="ThroughX">The horizontal distance to the target point (m).</param>
    /// <param name="ThroughY">The vertical distance to the target point (m).</param>
    /// <returns>The minimum initial speed required to reach the target point (m/s).</returns>
        public static double ThroughPointMinSpeed(double Gravity, double ThroughX, double ThroughY)
        {
            return Math.Sqrt(Gravity * (ThroughY + Math.Sqrt(ThroughX * ThroughX + ThroughY * ThroughY)));
        }

            /// <summary>
    /// Calculates the angle at which the projectile must be launched to reach a specific point (ThroughX, ThroughY)
    /// with the minimum initial speed, under the influence of gravity (Gravity).
    /// </summary>
    /// <param name="Gravity">The acceleration due to gravity (m/s²).</param>
    /// <param name="ThroughX">The horizontal distance to the target point (m).</param>
    /// <param name="ThroughY">The vertical distance to the target point (m).</param>
    /// <returns>The angle at which the projectile must be launched to reach the target point with the minimum initial speed (radians).</returns>
        public static double ThroughPointMinSpeedAngle(double Gravity, double ThroughX, double ThroughY)
        {
            return Math.Atan((ThroughY + Math.Sqrt(ThroughX * ThroughX + ThroughY * ThroughY)) / ThroughX);
        }

            /// <summary>
    /// Calculates the angle at which the projectile must be launched to reach a specific point (ThroughX, ThroughY)
    /// with a given initial speed (LaunchSpeed) and launch height (LaunchHeight), under the influence of gravity (Gravity).
    /// This method returns the lower angle of the two possible angles.
    /// </summary>
    /// <param name="Gravity">The acceleration due to gravity (m/s²).</param>
    /// <param name="LaunchSpeed">The initial speed of the projectile (m/s).</param>
    /// <param name="ThroughX">The horizontal distance to the target point (m).</param>
    /// <param name="ThroughY">The vertical distance to the target point (m).</param>
    /// <param name="LaunchHeight">The height from which the projectile is launched (m). Default is 0.</param>
    /// <returns>The lower angle at which the projectile must be launched to reach the target point with the given initial speed (radians).
    /// Returns NaN if there is no solution.</returns>

        public static double ThroughPointLowBallAngle(double Gravity, double LaunchSpeed, double ThroughX, double ThroughY, double LaunchHeight = 0)
        {
            double a = Gravity * ThroughX * ThroughX / (2 * LaunchSpeed * LaunchSpeed);
            double b = -ThroughX;
            double c = ThroughY - LaunchHeight + a;
            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0) { return double.NaN; };
            return Math.Atan((-b - Math.Sqrt(discriminant)) / (2 * a));
        }

            /// <summary>
    /// Calculates the angle at which the projectile must be launched to reach a specific point (ThroughX, ThroughY)
    /// with a given initial speed (LaunchSpeed) and launch height (LaunchHeight), under the influence of gravity (Gravity).
    /// This method returns the larger angle of the two possible angles.
    /// </summary>
    /// <param name="Gravity">The acceleration due to gravity (m/s²).</param>
    /// <param name="LaunchSpeed">The initial speed of the projectile (m/s).</param>
    /// <param name="ThroughX">The horizontal distance to the target point (m).</param>
    /// <param name="ThroughY">The vertical distance to the target point (m).</param>
    /// <param name="LaunchHeight">The height from which the projectile is launched (m). Default is 0.</param>
    /// <returns>The lower angle at which the projectile must be launched to reach the target point with the given initial speed (radians).
    /// Returns NaN if there is no solution.</returns>
        public static double ThroughPointHighBallAngle(double Gravity, double LaunchSpeed, double ThroughX, double ThroughY, double LaunchHeight = 0)
        {
            double a = Gravity * ThroughX * ThroughX / (2 * LaunchSpeed * LaunchSpeed);
            double b = -ThroughX;
            double c = ThroughY - LaunchHeight + a;
            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0) { return double.NaN; };
            return Math.Atan((-b + Math.Sqrt(discriminant)) / (2 * a));
        }




/// <summary>
/// Calculates the maximum range of a projectile given the acceleration due to gravity, launch speed, and launch height.
/// </summary>
/// <param name="Gravity">The acceleration due to gravity.</param>
/// <param name="LaunchSpeed">The initial launch speed of the projectile.</param>
/// <param name="LaunchHeight">The initial launch height of the projectile.</param>
/// <returns>The maximum range of the projectile.</returns>
        public static double MaxRange(double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            return AnalyticNoDragRange(MaxRangeAngle(Gravity, LaunchSpeed, LaunchHeight), Gravity, LaunchSpeed, LaunchHeight);
        }

        /// <summary>
/// Calculates the angle of launch that results in the maximum range of a projectile given the acceleration due to gravity, launch speed, and launch height.
/// </summary>
/// <param name="Gravity">The acceleration due to gravity.</param>
/// <param name="LaunchSpeed">The initial launch speed of the projectile.</param>
/// <param name="LaunchHeight">The initial launch height of the projectile.</param>
/// <returns>The angle of launch that results in the maximum range of the projectile.</returns>
        public static double MaxRangeAngle(double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double alpha = 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed);
            return Math.Asin(1 / Math.Sqrt(2 + alpha));
        }


/// <summary>
/// Calculates the bounding height of a projectile at a given horizontal distance.
/// </summary>
/// <param name="x">The horizontal distance from the launch point.</param>
/// <param name="Gravity">The acceleration due to gravity.</param>
/// <param name="LaunchSpeed">The initial launch speed of the projectile.</param>
/// <param name="LaunchHeight">The initial launch height of the projectile.</param>
/// <returns>The bounding height of the projectile at the given horizontal distance.</returns>
        public static double BoundingHeightAtDistanceX(double x, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double alpha = (LaunchSpeed * LaunchSpeed) / (Gravity);
            return LaunchHeight + (alpha - x * x / alpha) / 2;
        }




        private static double IntegralRequiredForLengthOfTrajectory(double x)
        {

            return (Math.Log(Math.Sqrt(1 + x * x) + x) + x * Math.Sqrt(1 + x * x)) / 2;//x>=0 so always ok 
        }

    /// <summary>
    /// Calculates the length of a trajectory given the horizontal distance, angle of launch, gravity, launch speed, and launch height.
    /// </summary>
    /// <param name="X">The horizontal distance.</param>
    /// <param name="Angle">The angle of launch in radians.</param>
    /// <param name="Gravity">The acceleration due to gravity.</param>
    /// <param name="LaunchSpeed">The initial launch speed.</param>
    /// <param name="LaunchHeight">The initial launch height.</param>
    /// <returns>The length of the trajectory.</returns>
        public static double LengthOfTrajectory(double X, double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double multiplier = LaunchSpeed*LaunchSpeed*Math.Pow(Math.Cos(Angle),2)/Gravity;
            double lower = Gravity * (1 + Math.Pow(Math.Tan(Angle), 2)) * X / (LaunchSpeed * LaunchSpeed);

            return multiplier * ( IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle)) - IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle)-lower) );
        }



    }

}
