using CoordinateSharp;
using ScottPlot;
using System.ComponentModel.Design;
using System.Data;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Core
{
    /// <summary>
    /// Implementation of core logic for trajectory calculations 
    /// </summary>
    public static class SimpleFormulae
    {
        
        private static double RadToDeg = 180/Math.PI;
        /// <summary>
        /// sin in degrees
        /// </summary>
        public static Func<double, double> sin = x=>Math.Sin(x/RadToDeg);
        /// <summary>
        /// degrees
        /// </summary>
        public static Func<double, double> cos = x=>Math.Cos(x/RadToDeg);
        /// <summary>
        /// degrees
        /// </summary>
        public static Func<double, double> tan = x=>Math.Tan(x / RadToDeg);
        /// <summary>
        /// degrees
        /// </summary>
        public static Func<double, double> atan = x=>Math.Atan(x)*RadToDeg;
        /// <summary>
        /// degrees
        /// </summary>
        public static Func<double, double> asin = x=>Math.Asin(x)*RadToDeg;
        /// <summary>
        /// degrees
        /// </summary>
        public static Func<double, double> acos = x=>Math.Acos(x)*RadToDeg;
        /// <summary>
        /// degrees
        /// </summary>
        public static Func<double,double, double> atan2 = (x,y)=>Math.Atan2(x,y)*RadToDeg;
        public static Func<double,double> sqrt = Math.Sqrt;
        public static Func<double,double> cbrt = Math.Cbrt;
        public static Func<double,int> sign = Math.Sign;
        public static Func<double,double> sqr = x=>x*x;
        public static Func<double,double> cube = x=>x*x*x;

        /// <summary>
        /// Prime vertical radius of curvature N(phi) for WGS84
        /// </summary>
        public static Func<double,double> PrimeVerticalRadCurvature = phi=>WGS84_a/sqrt(1-WGS84_e2 * sqr(sin(phi)));


        public const double SeaLevelPressureEart = 101325;
        public const double TempLapseRateEarth = 0.00976;
        public const double ConstantPressureSpecificHeatEarth =1004.68506;
        //public const double ConstantPressureSpecificHeat =1004.68506;
        public const double SeaLevelStandTempEarth = 288.16;
        /// <summary>
        /// Molar mass of dry air SI units
        /// </summary>
        public const double MolarMassDryAirEarth = 0.02896968;
        /// <summary>
        /// Universal gas constant
        /// </summary>
        public const double UnivGasConst = 8.314462618;

        /// <summary>
        /// Atmospheric data according to https://en.wikipedia.org/wiki/Barometric_formula
        /// </summary>
        public static DataTable AtmosphericParameters= new DataTable
        {
            Columns = { { "b", typeof(int) }, { "height", typeof(double) }, { "density", typeof(double) }, { "stdTemp", typeof(double) }, { "lapse", typeof(double) } },
            Rows = {
            {0,00000,1.2250,288.15,0.0065 },
            {1,11000,0.36391,216.65 ,0 },
            {2,20000,0.08803,216.65 ,-0.001   },
            {3,32000,0.01322,228.65 ,-0.0028   },
            {4,47000,0.00143,270.65 ,0 },
            {5,51000,0.00086,270.65 ,0.0028   },
            {6,71000,0.000064,214.65 ,0.002   }
            }
        };

        /// <summary>
        /// Radius of earth in meters
        /// </summary>
        public const double radiusEarth = 6371000;
        /// <summary>
        /// Mass of Earth in kg
        /// </summary>
        public const double massEarth = 5.972e24;
        /// <summary>
        /// Big G constant, SI units
        /// </summary>
        public const double gravConstant = 6.6743e-11;
        /// <summary>
        /// Seconds for one earth rotation fixed frame of reference to earth
        /// </summary>
        public const double secondsPerRotation = 23*60*60+56*60+4.09;
        /// <summary>
        /// WGS84 a parameter (meters)
        /// </summary>
        public const double WGS84_a = 6378137;
        /// <summary>
        /// WGS84 b parameter (meters)
        /// </summary>
        public const double WGS84_b = 6356752.3142;

        /// <summary>
        /// WGS84 first eccentricity squared
        /// </summary>
        public const double WGS84_e2 = 1-WGS84_b*WGS84_b/(WGS84_a*WGS84_a);
        /// <summary>
        /// WGS84 second eccentricity squared
        /// </summary>
        public const double WGS84_ecompliment2 = WGS84_a*WGS84_a/(WGS84_b*WGS84_b)-1;
        /// <summary>
        /// WGS84 f parameter
        /// </summary>
        public const double WGS84_f = 1-WGS84_a/(WGS84_b);




        //public static IEnumerable<(int, double, int)> ExtractMinimaMaxima(List<double> values)
        //{
        //    if (values.Count < 3) yield break;
        //    for (int i = 1; i < values.Count - 1; i++)
        //    {
        //        if (isPeak(values[i - 1], values[i], values[i + 1])) yield return (1, values[i], i);
        //        else if (isThrough(values[i - 1], values[i], values[i + 1])) yield return (-1, values[i], i);
        //        else yield return (0, values[i], i);
        //    }
        //}



        /// <summary>
        /// Fixed time increment simulation with no drag
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

        /// <summary>
        /// Height above ground at horizontal distance x of projectile with no drag
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Angle"></param>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static double AnalyticNoDragHeight(double x, double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            return LaunchHeight + x * Math.Tan(Angle) - (Gravity) * (1 + Math.Pow(Math.Tan(Angle), 2)) * x * x / (2 * LaunchSpeed * LaunchSpeed);
        }
        /// <summary>
        /// horizontal range of projectile with specified launch parameters without drag
        /// </summary>
        /// <param name="Angle"></param>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static double AnalyticNoDragRange(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            if (LaunchSpeed == 0) return 0; //fix up NaN from appearing
            return LaunchSpeed * LaunchSpeed * (Math.Sin(Angle) * Math.Cos(Angle) + Math.Cos(Angle) *

                Math.Sqrt(Math.Pow(Math.Sin(Angle), 2) + 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed))//possibility of NaN

                ) / Gravity;
        }
        /// <summary>
        /// Flight time of trajectory without drag
        /// </summary>
        /// <param name="Angle"></param>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static double AnalyticNoDragFlightTime(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            if (LaunchSpeed == 0) return 0; //fix up NaN from appearing
            return LaunchSpeed * (Math.Sin(Angle) +

                Math.Sqrt(Math.Pow(Math.Sin(Angle), 2) + 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed))//possibility of NaN

                ) / Gravity; ;
        }

        /// <summary>
        /// time stamps at regular intervals for duration of flight without drag
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<double> TimeSamplesFromFlight(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight,double TimeIncrement)
        {
            double t = 0;
            double ft = AnalyticNoDragFlightTime(Angle, Gravity, LaunchSpeed, LaunchHeight);
            while (t<ft)
            {
                yield return t;
                t += TimeIncrement;
            }yield return t;
        }

        /// <summary>
        /// distance stamps at regular intervals for duration of flight without drag
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<double> DistanceSamplesFromFlight(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight, double TimeIncrement)
        {
            double t = 0;
            double uc=Math.Cos(Angle)*LaunchSpeed;
            double ft = AnalyticNoDragFlightTime(Angle, Gravity, LaunchSpeed, LaunchHeight);
            while (t < ft)
            {
                yield return t*uc;
                t += TimeIncrement;
            }
            yield return t;
        }

        /// <summary>
        /// Appogee of trajectory without drag
        /// </summary>
        /// <param name="Angle"></param>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static Coordinates AnalyticNoDragApogee(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double x = LaunchSpeed * LaunchSpeed * Math.Sin(Angle) * Math.Cos(Angle) / Gravity;
            double y = LaunchHeight + LaunchSpeed * LaunchSpeed * Math.Sin(Angle) * Math.Sin(Angle) / (2 * Gravity);
            return new Coordinates(x, y);
        }


        /// <summary>
        /// Minimum speed required to hit target
        /// </summary>
        /// <param name="Gravity"></param>
        /// <param name="ThroughX"></param>
        /// <param name="ThroughY"></param>
        /// <returns></returns>
        public static double ThroughPointMinSpeed(double Gravity, double ThroughX, double ThroughY)
        {
            return Math.Sqrt(Gravity * (ThroughY + Math.Sqrt(ThroughX * ThroughX + ThroughY * ThroughY)));
        }

        /// <summary>
        /// Minimum speed trajectory launch angle
        /// </summary>
        /// <param name="Gravity"></param>
        /// <param name="ThroughX"></param>
        /// <param name="ThroughY"></param>
        /// <returns></returns>
        public static double ThroughPointMinSpeedAngle(double Gravity, double ThroughX, double ThroughY)
        {
            return Math.Atan((ThroughY + Math.Sqrt(ThroughX * ThroughX + ThroughY * ThroughY)) / ThroughX);
        }


        /// <summary>
        /// The low ball to pass through target at specified speed
        /// Returns NaN if ball can not hit target
        /// </summary>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="ThroughX"></param>
        /// <param name="ThroughY"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
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
        /// The high ball to pass through target at specified speed
        /// Returns NaN if ball can not hit target
        /// </summary>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="ThroughX"></param>
        /// <param name="ThroughY"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
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
        /// Maximum horizontal range
        /// </summary>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static double MaxRange(double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            return AnalyticNoDragRange(MaxRangeAngle(Gravity, LaunchSpeed, LaunchHeight), Gravity, LaunchSpeed, LaunchHeight);
        }

        /// <summary>
        /// Angle in radians that give max horizontal range
        /// </summary>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static double MaxRangeAngle(double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double alpha = 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed);
            return Math.Asin(1 / Math.Sqrt(2 + alpha));
        }

        /// <summary>
        /// Bounding parabola at horizontal range x from launch
        /// </summary>
        /// <param name="x"></param>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <returns></returns>
        public static double BoundingHeightAtDistanceX(double x, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double alpha = (LaunchSpeed * LaunchSpeed) / (Gravity);
            return LaunchHeight + (alpha - x * x / alpha) / 2;
        }



        /// <summary>
        /// https://www.bpho.org.uk/bpho/computational-challenge/BPhO_CompPhys2024_Projectilesa.pdf
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static double IntegralRequiredForLengthOfTrajectory(double x)
        {

            return (Math.Log(Math.Sqrt(1 + x * x) + x) + x * Math.Sqrt(1 + x * x)) / 2;//x>=0 so always ok 
        }

        public static double LengthOfTrajectory(double X, double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            double multiplier = LaunchSpeed * LaunchSpeed * Math.Pow(Math.Cos(Angle), 2) / Gravity;
            double lower = Gravity * (1 + Math.Pow(Math.Tan(Angle), 2)) * X / (LaunchSpeed * LaunchSpeed);

            return multiplier * (IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle)) - IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle) - lower));
        }

        /// <summary>
        /// Distance from (0,0) for projectile at time T seconds
        /// </summary>
        /// <param name="t">seconds</param>
        /// <param name="Angle">launch angle radians</param>
        /// <param name="Gravity"></param>
        /// <param name="LaunchSpeed"></param>
        /// <returns></returns>
        public static double distanceFromOriginAtTimeT(double t, double Angle, double Gravity, double LaunchSpeed)
        {

            return t * Math.Sqrt(
                LaunchSpeed * LaunchSpeed
                - Gravity * t * LaunchSpeed * (Math.Sin(Angle))
                + 0.25 * (Gravity * Gravity * t * t)
                );
        }

        public static double timeMaxDistanceFromOrigin(double Angle, double Gravity, double LaunchSpeed)
        {
            var s= Math.Sin(Angle);
            return (s+sqrt(s*s-8.0/9))*1.5 * LaunchSpeed / Gravity;
            //return 3 * LaunchSpeed / (2 * Gravity) * Math.Sin(Angle) - LaunchSpeed / Gravity * Math.Sqrt(9 / 4 * Math.Pow(Math.Sin(Angle), 2) - 2) ;
        }

        public static double timeMinDistanceFromOrigin( double Angle, double Gravity, double LaunchSpeed)
        {
            var s = Math.Sin(Angle);
            var val= (s - sqrt(s * s - 8.0 / 9)) * 1.5 * LaunchSpeed / Gravity;
            return val;
            //return 3 * LaunchSpeed / (2 * Gravity) * Math.Sin(Angle) + LaunchSpeed / Gravity * Math.Sqrt(9 / 4 * Math.Pow(Math.Sin(Angle), 2) - 2);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Angle">launch angle radians</param>
        /// <param name="Gravity">m/s^2</param>
        /// <param name="LaunchSpeed">m/s</param>
        /// <param name="LaunchHeight"></param>
        /// <param name="CoeffRestitution"></param>
        /// <param name="TimeIncrement">time step for simulation</param>
        /// <param name="Bounces">Bounces to simulate</param>
        /// <returns></returns>
        public static IEnumerable<Coordinates> Bounce( double Angle, double Gravity, double LaunchSpeed, double LaunchHeight, double CoeffRestitution, double TimeIncrement = 0.01, int Bounces=5)
        {
            int bounces = 0;
            Vector a = new Vector(2);
            a[0] = 0;
            a[1] = -Gravity;
            Vector v = new Vector(2);
            v[0] = LaunchSpeed*Math.Cos(Angle);
            v[1] = LaunchSpeed*Math.Sin(Angle);
           
            Vector curPos = new Vector(2);
            curPos[0] = 0;
            curPos[1] = LaunchHeight;
            double t = 0;
            yield return curPos;
            while (bounces<Bounces) { 
                curPos= curPos + v*TimeIncrement + 0.5*a*TimeIncrement*TimeIncrement;

                //acc does not need be updated

                v = v + 0.5 * (a+a) * TimeIncrement;
                t+= TimeIncrement;
                if (curPos[1]<0)
                {
                    curPos[1] = 0;
                    v[1]*=-CoeffRestitution;
                    bounces++;
                }
                yield return curPos;
            }

        }

        /// <summary>
        /// Todo: allow user import atmospheric data
        /// </summary>
        /// <param name="json"></param>
        public static void LoadAtmosphericDataFromJSON(string json)
        {
            AtmosphericParameters = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
        }

        //Todo
        public static void RecalculateAtmosphericData()
        {

        }


        /// <summary>
        /// https://en.wikipedia.org/wiki/Barometric_formula
        /// </summary>
        /// <param name="altitude"></param>
        /// <returns></returns>
        public static double LapseRate(double altitude)
        {
            switch (altitude)
            {
                case <11000:
                    return 0.0065;
                case < 20000:
                    return 0;
                case < 32000:
                    return -0.001;
                case < 47000:
                    return -0.0028;
                case < 51000:
                    return 0.0;
                case < 71000:
                    return 0.0028;
                case < 85000:
                    return 0.002;
                default:
                    return 0; //atmosphere too thin to bother above this
            }
        }


        /// <summary>
        /// just see https://en.wikipedia.org/wiki/Atmospheric_pressure
        /// https://en.wikipedia.org/wiki/Barometric_formula
        /// </summary>
        /// <param name="Altitude"></param>
        /// <param name="SeaLevelPressure"></param>
        ///// <param name="TempLapseRate"></param>
        /// <param name="ConstantPressureSpecificHeat"></param>
        /// <param name="SeaLevelTemp"></param>
        /// <param name="Gravity"></param>
        /// <param name="MolarMassDryAir"></param>
        /// <param name="UniversalGasConst"></param>
        /// <returns></returns>
        public static double DryAirPressureAltitude(double Altitude




            ,double SeaLevelPressure = SeaLevelPressureEart
            //,double TempLapseRate=0.00976
            ,double ConstantPressureSpecificHeat = ConstantPressureSpecificHeatEarth
            ,double SeaLevelTemp = SeaLevelStandTempEarth
            ,double Gravity = 9.8
            ,double MolarMassDryAir = MolarMassDryAirEarth
            ,double UniversalGasConst = UnivGasConst
            )
        {
            //TODO read https://en.wikipedia.org/wiki/Barometric_formula
            //wrong code
            //if ((1 - Gravity * Altitude / (ConstantPressureSpecificHeat * SeaLevelTemp)) < 0)
            //{
            //    Console.WriteLine("dukmmy");
            //}
            //return SeaLevelPressure * Math.Pow((1 - Gravity * Altitude / (ConstantPressureSpecificHeat * SeaLevelTemp)), ConstantPressureSpecificHeat * MolarMassDryAir / UniversalGasConst);

            int b = -1;
            foreach (DataRow cur in AtmosphericParameters.Rows)
            {
                if ((double)cur[1] <= Altitude)
                {
                    b++;
                }
                else
                {
                    break;
                }
            }
            if (b == -1) {
                return double.NaN;
            }
            double refPressure = (double)AtmosphericParameters.Rows[b][2];
            double refTemp = (double)AtmosphericParameters.Rows[b][3];
            double refHeight = (double)AtmosphericParameters.Rows[b][1];
            double lapseRate = (double)AtmosphericParameters.Rows[b][4];
            if (lapseRate==0)
            {
                return refPressure * Math.Pow(1-lapseRate*(Altitude-refHeight)/(refTemp)
                    ,Gravity*MolarMassDryAir/(UniversalGasConst*lapseRate)
                    );
            }
            else
            {
                return refPressure * Math.Exp(
                    -Gravity * MolarMassDryAir *(Altitude-refHeight)/ (UniversalGasConst * refTemp)
                    );
            }
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Barometric_formula
        /// Ignore optional paramters, currently dont affect the value
        /// </summary>
        /// <param name="Altitude"></param>
        /// <param name="TempLapseRate"></param>
        /// <param name="SeaLevelTemp"></param>
        /// <returns></returns>
        public static double DryAirTempAltitudeSimple(double Altitude
    ,double TempLapseRate=0.00976
    , double SeaLevelTemp = 288.16

    )
        {
            //return SeaLevelTemp+Altitude*TempLapseRate;//TODO minus here
            double currenttemp = SeaLevelTemp;
            double lastHeight = 0;
            if (Altitude >0) {
                currenttemp=currenttemp+LapseRate(Altitude)*(Altitude-lastHeight);
            }
            if (Altitude > 11000)
            {
                lastHeight = 11000;
                currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            }
            if (Altitude > 20000)
            {
                lastHeight = 20000;
                currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            }
            if (Altitude > 32000)
            {
                lastHeight = 32000;
                currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            }
            if (Altitude > 47000)
            {
                lastHeight = 47000;
                currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            }
            if (Altitude > 51000)
            {
                lastHeight = 51000;
                currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            }
            if (Altitude > 71000)
            {
                lastHeight = 71000;
                currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            }
            //if (Altitude > 85000)
            //{
            //    lastHeight = 85000;
            //    currenttemp = currenttemp + LapseRate(Altitude) * (Altitude - lastHeight);
            //}
            if (currenttemp<=0)
            {
                currenttemp = 0.01;//Something close to 0
            }
            return currenttemp;
        }

        //TODO forward all the constants in the calculations to other func calls
        /// <summary>
        /// Density of dry air
        /// </summary>
        /// <param name="Altitude">above ellipsoid</param>
        /// <param name="MolarMassDryAir"></param>
        /// <param name="GasConstant"></param>
        /// <returns></returns>
        public static double DryAirDensityAltitude(double Altitude,
            double MolarMassDryAir = MolarMassDryAirEarth,
            double GasConstant = UnivGasConst
            )
        {
            return DryAirPressureAltitude(Altitude)*MolarMassDryAir/(GasConstant*DryAirTempAltitudeSimple(Altitude));
        }

        /// <summary>
        /// Redundant
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static double AirDensityLookupLinearInterpolation(double height)
        {
            int b = -1;
            foreach (DataRow cur in AtmosphericParameters.Rows)
            {
                if ((double)cur[1]<=height)
                {
                    b++;
                }
                else
                {
                    break;
                }
            }
            return -1;
        }

        /// <summary>
        /// Force of drag based on altitude (however defined)
        /// </summary>
        /// <param name="altitude">metres</param>
        /// <param name="speed">m/s</param>
        /// <param name="dragCoeff"></param>
        /// <param name="crossSectionArea">m^2</param>
        /// <returns></returns>
        public static double DragForceMagnitude(double altitude, double speed,double dragCoeff, double crossSectionArea)
        {
            return 0.5*DryAirDensityAltitude(altitude)*sqr(speed)*crossSectionArea*dragCoeff;
        }


        /// <summary>
        /// IEnumerable of ScottPlot coordinates representing trajectory from drag with 2D flat earth
        /// </summary>
        /// <param name="Angle">radians</param>
        /// <param name="Gravity">ms^-2</param>
        /// <param name="LaunchSpeed">ms^-1</param>
        /// <param name="LaunchHeight">metres above the plane</param>
        /// <param name="CoeffDrag"></param>
        /// <param name="TimeIncrement">time increment for simulation</param>
        /// <returns></returns>
        public static IEnumerable<Coordinates> Drag(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight, double CoeffDrag, double TimeIncrement = 0.01, bool ClampNegativeHeight=false)
        {
            //int bounces = 0;
            Vector a = new Vector(2);
            a[0] = 0;
            a[1] = -Gravity;
            Vector v = new Vector(2);
            v[0] = LaunchSpeed * Math.Cos(Angle);
            v[1] = LaunchSpeed * Math.Sin(Angle);

            Vector curPos = new Vector(2);
            curPos[0] = 0;
            curPos[1] = LaunchHeight;
            double t = 0;
            yield return curPos;
            //bool fl
            
            while (curPos[1]>0 )
            {
                
                a[0] = -v[0]*CoeffDrag*v.Magnitude();
                a[1] = -Gravity - v[1] * CoeffDrag * v.Magnitude();

                curPos = curPos + v * TimeIncrement + 0.5 * (a) * TimeIncrement * TimeIncrement;

                v = v + (a) * TimeIncrement;
                t += TimeIncrement;
                if (curPos[1] < 0)
                {

                    if(ClampNegativeHeight)curPos[1] = 0;
                    yield return curPos;
                    yield break;
                }
                yield return curPos;
            }

        }

        //public static double AltitudeFromCoordiantesSphere(Vector coord)
        //{
        //    return coord.Magnitude() - radiusEarth;
        //}
        //public static double AltitudeFromCoordinatesWGS84(Vector coord)
        //{
        //    return ECEFVectorToGeoditicAndHeightZhu(coord).Item2;
        //}

        /// <summary>
        /// Acceleration due to gravity from ECEF position. Negative (for direction) handled
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static Vector AccelerationDueToGrav(Vector Position)
        {
            return -1*Position * massEarth * gravConstant / (Math.Pow(Position*Position,1.5));
        }
        /// <summary>
        /// ASSUMES SPHERICAL EARTH, todo implement ellipsoid
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static Vector ProjectOntoSurfaceOfEarthVector(Vector Position)
        {
            return radiusEarth * Position / Position.Magnitude();
        }

        /// <summary>
        /// Use Heikkinen ECEF position to LLA
        /// </summary>
        /// <param name="Position"></param>
        /// <returns></returns>
        public static Coordinate ProjectOntoSurfaceOfEarthCoord(Vector Position)
        {
            return ECEFVectorToGeoditicHeikkinen(Position);
        }
       
        /// <summary>
        /// Geoditic to ECEF postion
        /// </summary>
        /// <param name="Geoditic"></param>
        /// <param name="Height"></param>
        /// <returns></returns>
        public static Vector GeoditicToECEFVector(Coordinate Geoditic, double Height=0)
        {
            var v= new Vector(3) {};
            double phi  = Geoditic.Latitude.ToDouble();
            double lamb  = Geoditic.Longitude.ToDouble();
            double h  = Height;
            v[0] = (PrimeVerticalRadCurvature(phi) + h) * cos(phi) * cos(lamb);
            v[1] = (PrimeVerticalRadCurvature(phi) + h) * cos(phi) * sin(lamb);
            v[2] = ((1-WGS84_e2)*PrimeVerticalRadCurvature(phi) + h) * sin(phi);
            return v;
        }

        /// <summary>
        /// https://en.wikipedia.org/wiki/Geographic_coordinate_conversion
        /// </summary>
        /// <param name="ECEF"></param>
        /// <returns></returns>
        public static (Coordinate,double) ECEFVectorToGeoditicAndHeightHeikkinen(Vector ECEF)
        {
            //black magic of wikipedia
            double Z2 = ECEF[2]*ECEF[2];
            double p = Math.Sqrt(ECEF[0] * ECEF[0] + ECEF[1] * ECEF[1]);
            double F = 54*WGS84_b*WGS84_b*Z2;
            double G = p * p + (1 - WGS84_e2) * Z2 -WGS84_e2 * (WGS84_a*WGS84_a-WGS84_b*WGS84_b);
            double c = WGS84_e2 * WGS84_e2 * F * p * p / (G*G*G);
            //var temp = Math.Sqrt(c * c + 2 * c);
            double s = Math.Cbrt(1+c+Math.Sqrt( c*c+2*c ) );
            double k = s + 1 + 1 / s;
            double P = F/(3*k*k*G*G);
            double Q = Math.Sqrt(1+2*WGS84_e2*WGS84_e2*P);
            double r0 = -P * WGS84_e2 * p / (1 + Q) + 
                Math.Sqrt(0.5*WGS84_a*WGS84_a*(1+1/Q)
                -P*(1-WGS84_e2)*Z2/(Q+1/Q)
                -0.5*P*p*p);
            double U = Math.Sqrt((p-WGS84_e2*r0)* (p - WGS84_e2 * r0) + Z2);
            double V = Math.Sqrt((p - WGS84_e2 * r0) * (p - WGS84_e2 * r0) + (1-WGS84_e2)*Z2);
            double z0 = WGS84_b * WGS84_b * ECEF[2] / (WGS84_a * V);



            double height = U * (1-WGS84_b*WGS84_b/(WGS84_a*V));
            double lat = Math.Atan((ECEF[2] + WGS84_ecompliment2 *z0)/(p));
            double longt = Math.Atan2(ECEF[1], ECEF[0]);
            return (new Coordinate(lat, longt),height);
        }

        /// <summary>
        /// algorithm by Zhu (1993). Turns ECEF position to LLA
        /// Test passed
        /// </summary>
        /// <param name="ECEF"></param>
        /// <returns></returns>
        public static (Coordinate, double) ECEFVectorToGeoditicAndHeightZhu(Vector ECEF)
        {
            // algorithm by Zhu (1993)
            double w = sqrt(sqr(ECEF[0]) + sqr(ECEF[1]));
            if (w == 0)
            {
                double longit = 2 * atan((w - ECEF[0]) / ECEF[1]);
                return (new Coordinate(sign(ECEF[2]) * 90,longit), sign(ECEF[2]) * ECEF[2] - WGS84_b);

            }
            double l = WGS84_e2 / 2.0;
            double m = sqr(w/WGS84_a);
            double n = sqr((1.0-WGS84_e2)*ECEF[2]/WGS84_b);
            double i = -(2 * sqr(l) + m + n) / 2.0;
            double k = sqr(l) * (sqr(l)-m-n);
            double emmp = sqr(l);
            double tmp = m + n - emmp-emmp-emmp-emmp;
            double q = cube(m + n - 4.0 * sqr(l)) * 1.0/216.0 + m * n * sqr(l);

            var temp = (2 * q - m * n * sqr(l)) * m * n * sqr(l);
            double D = sqrt((  2*q- m * n * sqr(l)  )  * m * n * sqr(l) );
            double beta = i / 3 - cbrt(q + D) - cbrt(q-D);
            double t = sqrt( sqrt(sqr(beta)-k)  -  (beta+i)/2 )
                -sign(m-n)*sqrt((beta-i)/2);
            double w1 = w / (t + l);
            double z1 = (1-WGS84_e2)*ECEF[2]/(t-l);//fixx

            double lat = atan(z1/ ((1-WGS84_e2)*w1));   //MAYBE atan2
            double longi = 2 * atan((w - ECEF[0]) / ECEF[1]);
            if (ECEF[1]==0)
            {
                if (ECEF[0]>=0)//equal zero in case we get a pole, just give it longitude 0
                {
                    longi = 0;
                }
                else
                {
                    longi = -180;
                }
            }
            double h = sign(t-1+l)*sqrt(sqr(w-w1)   + sqr(ECEF[2] -z1)); //what if ECEF 1 is 0

            return (new Coordinate(lat,longi),h);
        }


        /// <summary>
        /// Netwon Raphson iteration for ECEF position to geoditic
        /// Test passed
        /// </summary>
        /// <param name="ecef"></param>
        /// <returns></returns>
        public static (Coordinate, double) ECEFVectorToGeoditicAndHeightNewtonRaphson(Vector ecef) {

            double x = ecef[0];
            double y = ecef[1];
            double z = ecef[2];

            double e = Math.Sqrt(Math.Pow(WGS84_a, 2.0) - Math.Pow(WGS84_b, 2.0)) / WGS84_a;
            double clambda = Math.Atan2(y, x);
            double p = Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2));
            double h_old = 0.0;
            double theta = Math.Atan2(z, p * (1.0 - Math.Pow(e, 2.0)));
            double cs = Math.Cos(theta);
            double sn = Math.Sin(theta);
            double N = Math.Pow(WGS84_a, 2.0) / Math.Sqrt(Math.Pow(WGS84_a * cs, 2.0) + Math.Pow(WGS84_b * sn, 2.0));
            double h = p / cs - N;
            while (Math.Abs(h - h_old) > 1.0e-12) {
                h_old = h;
                theta = Math.Atan2(z, (p * (1.0 - Math.Pow(e, 2.0) * N / (N + h))) ); //change back to just atan
                cs = Math.Cos(theta);
                sn = Math.Sin(theta);
                N = Math.Pow(WGS84_a, 2.0) / Math.Sqrt(Math.Pow(WGS84_a * cs, 2.0) + Math.Pow(WGS84_b * sn, 2.0));
                sn = p / cs - N;
             }

            return (new Coordinate(theta*RadToDeg,clambda*RadToDeg),h);
     }

        public static Coordinate ECEFVectorToGeoditicHeikkinen(Vector ECEF)
        {
            return ECEFVectorToGeoditicAndHeightHeikkinen(ECEF).Item1;
        }
        public static Vector VelocityOfEarthSurfaceFromCoordinate(Coordinate Geoditic, double TimeIncrement)
        {
            var v = new Vector(3) { };
            v = GeoditicToECEFVector(Geoditic);
            var v2 = Matrix.RotationZ(TimeIncrement/secondsPerRotation*Math.Tau)*v;

            return v2-v;
        }

        /// <summary>
        /// credit to https://gist.github.com/govert/1b373696c9a27ff4c72a
        /// Raw position ECEF
        /// </summary>
        /// <param name="ENU">ENU position</param>
        /// <param name="Geoditic"></param>
        /// <returns></returns>
        public static Vector ENUToECEF_Position(Vector ENU, Coordinate Geoditic, double height)
        {
            //double lam = Geoditic.Longitude.ToDouble()/RadToDeg;
            //double phi = Geoditic.Latitude.ToDouble()/RadToDeg;
            //var matForEnuToEcef = Matrix.RotationZ(-(Math.PI/2 + lam)) * Matrix.RotationX(-(Math.PI/2 - phi));
            //return matForEnuToEcef* ENU;

            double lat0 = Geoditic.Latitude.ToDouble();
            double lon0 = Geoditic.Longitude.ToDouble();
            double h0 = height;

            double xEast = ENU[0];
            double yNorth = ENU[1];
            double zUp = ENU[2];

            var lambda = lat0/RadToDeg;
            var phi = lon0/RadToDeg;
            var s = Math.Sin(lambda);
            var N = WGS84_a / Math.Sqrt(1 - WGS84_e2 * s * s);

            var sin_lambda = Math.Sin(lambda);
            var cos_lambda = Math.Cos(lambda);
            var cos_phi = Math.Cos(phi);
            var sin_phi = Math.Sin(phi);


            double x0 = (h0 + N) * cos_lambda * cos_phi;
            double y0 = (h0 + N) * cos_lambda * sin_phi;
            double z0 = (h0 + (1 - WGS84_e2) * N) * sin_lambda;

            double xd = -sin_phi * xEast - cos_phi * sin_lambda * yNorth + cos_lambda * cos_phi * zUp;
            double yd = cos_phi * xEast - sin_lambda * sin_phi * yNorth + cos_lambda * sin_phi * zUp;
            double zd = cos_lambda * yNorth + sin_lambda * zUp;

            double x = xd + x0;
            double y = yd + y0;
            double z = zd + z0;

            Vector ret = new Vector(3);

            // This is the matrix multiplication
            ret[0] = x;
            ret[1] = y;
            ret[2] = z;

            return ret;
        }

        /// <summary>
        /// Returns ECEF velocity or direction
        /// </summary>
        /// <param name="ENU">ENU direction/velocity</param>
        /// <param name="Geoditic"></param>
        /// <param name="height">Above ellipsoid</param>
        /// <returns></returns>
        public static Vector ENUToECEF_Velocity(Vector ENU, Coordinate Geoditic, double height)
        {//TODO
            //double lam = Geoditic.Longitude.ToDouble()/RadToDeg;
            //double phi = Geoditic.Latitude.ToDouble()/RadToDeg;
            //var matForEnuToEcef = Matrix.RotationZ(-(Math.PI/2 + lam)) * Matrix.RotationX(-(Math.PI/2 - phi));
            //return matForEnuToEcef* ENU;

            var translate = GeoditicToECEFVector(Geoditic, height);
            var ECEF=ENUToECEF_Position(ENU, Geoditic, height);
            return ECEF-translate;//TODO, what here
        }

        /// <summary>
        /// Converts to appropriate ENU position(s) and finds difference between them
        /// Suitable for velocities or directions
        /// </summary>
        /// <param name="ECEF">ECEF velocity or direction</param>
        /// <param name="Geoditic"></param>
        /// <param name="height">above ellipsoid</param>
        /// <returns></returns>
        public static Vector ECEFtoENU_Velocity(Vector ECEF, Coordinate Geoditic, double height)
        {
            //if (Geoditic!=null) {
            //    double lam = Geoditic.Longitude.ToDouble()/RadToDeg;
            //    double phi = Geoditic.Latitude.ToDouble()/RadToDeg;
            //    var matForEnuToEcef = Matrix.RotationX((Math.PI / 2 - phi)) * (Matrix.RotationZ((Math.PI/2 + lam)) );
            //    return matForEnuToEcef * ECEF;
            //} else {
            //    Geoditic = ECEFVectorToGeoditicAndHeightZhu(ECEF).Item1;
            //    double lam = Geoditic.Longitude.ToDouble() / RadToDeg;
            //    double phi = Geoditic.Latitude.ToDouble() / RadToDeg;
            //    var matForEnuToEcef = Matrix.RotationX((Math.PI / 2 - phi))*(Matrix.RotationZ((Math.PI/2 + lam)) );
            //    return matForEnuToEcef * ECEF;
            //}
            var translate=GeoditicToECEFVector(Geoditic,height);
            ECEF += translate;
            return ECEFtoENU_Position(ECEF,Geoditic,height);
        }



        /// <summary>
        /// credit to https://gist.github.com/govert/1b373696c9a27ff4c72a
        /// </summary>
        /// <param name="ECEF"></param>
        /// <param name="Geoditic"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Vector ECEFtoENU_Position(Vector ECEF, Coordinate Geoditic, double height)
        {
            // Converts the Earth-Centered Earth-Fixed (ECEF) coordinates (x, y, z) to 
            // East-North-Up coordinates in a Local Tangent Plane that is centered at the 
            // (WGS-84) Geodetic point (lat0, lon0, h0).
            //double x, double y, double z,
            //double lat0, double lon0, double h0,
            //out double xEast, out double yNorth, out double zUp
            double x = ECEF[0];
            double y = ECEF[1];
            double z = ECEF[2];

            double lat0 = Geoditic.Latitude.ToDouble();
            double lon0 = Geoditic.Longitude.ToDouble();
            double h0 = height;
                // Convert to radians in notation consistent with the paper:
                var lambda = lat0/RadToDeg;
                var phi = lon0/RadToDeg;
                var s = Math.Sin(lambda);
                var N = WGS84_a / Math.Sqrt(1 - WGS84_e2 * s * s);

                var sin_lambda = Math.Sin(lambda);
                var cos_lambda = Math.Cos(lambda);
                var cos_phi = Math.Cos(phi);
                var sin_phi = Math.Sin(phi);

                double x0 = (h0 + N) * cos_lambda * cos_phi;
                double y0 = (h0 + N) * cos_lambda * sin_phi;
                double z0 = (h0 + (1 - WGS84_e2) * N) * sin_lambda;

                double xd, yd, zd;
                xd = x - x0;
                yd = y - y0;
                zd = z - z0;
                
                Vector ret = new Vector(3);
            // This is the matrix multiplication
            ret[0] = -sin_phi * xd + cos_phi * yd;
            ret[1] = -cos_phi * sin_lambda * xd - sin_lambda * sin_phi * yd + cos_lambda * zd;
            ret[2] = cos_lambda * cos_phi * xd + cos_lambda * sin_phi * yd + sin_lambda * zd;
            return ret;
        }


        /// <summary>
        /// Returns ENU vector (velocity/direction)
        /// </summary>
        /// <param name="Azimuth">degrees</param>
        /// <param name="Elevation">degrees</param>
        /// <returns></returns>
        public static Vector AzimuthElevationtoENU(double Azimuth, double Elevation)
        {
            Vector ENU = new Vector(3);
            ENU[0] = sin(Azimuth) * cos(Elevation);
            ENU[1] = cos(Azimuth) * cos(Elevation);
            ENU[2] = sin(Elevation);
            return ENU;
        }

        /// <summary>
        /// Converts a IEnumerable of geographic coordinates to a IEnumerable of ScottPlot (coordinates, coordinates).
        /// First item in tuple is geogeditic representatioon of point on the ground below the projectile
        /// Second item x coordinate is distance travelled (metres) on ground, y coordinate is the altitude
        /// third item x coordinate is the time (seconds), y coordinate is the altitude
        /// </summary>
        /// <param name="geoCoords">An IEnumerable<Coordinate> object containing the latitude and longitude coordinates to be converted.</param>
        /// <returns>An IEnumerable Coordinates object containing the corresponding x and y coordinates for each input coordinate.</returns>
        public static IEnumerable<(Coordinates,Coordinates,Coordinates)> LatLongToLLAandAltitudeAtTimeT(IEnumerable<(Coordinate,double,double)> geoCoords)
        {
            Coordinate? start= null;
            foreach ((Coordinate, double, double) coordHeight in geoCoords)
            {
                var coord = coordHeight.Item1;
                var height = coordHeight.Item2;
                var t = coordHeight.Item3;
                if (start==null)
                {
                    start = coordHeight.Item1;
                }
                var dist = start.Get_Distance_From_Coordinate(coord,Shape.Ellipsoid);
                var lat = coord.Latitude.ToDouble();
                var lng = coord.Longitude.ToDouble();
                var screenX = ((lng)); //+ 180)); //* (mapWidth / 360));
                var screenY = (((lat))); //+ 90)); // * (mapHeight / 180));//-1


                yield return (new Coordinates(screenX, screenY), new Coordinates(dist.Meters,height), new Coordinates(t, height));
            }
        }


        /// <summary>
        /// IEnumerable of lat, long coordiantes and height of projectile, and time at t seconds if projected down onto surface of the earth (normal to WGS 84 ellipsoid).
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Azimuth">Degrees from North</param>
        /// <param name="AngleElevation">Degrees</param>
        /// <param name="LaunchSpeed">m/s</param>
        /// <param name="LaunchHeight">metres above ellipsoid</param>
        /// <param name="CoeffDrag"></param>
        /// <param name="TimeIncrement">Time increment for simulation time steps</param>
        /// <returns></returns>

        public static IEnumerable<(Coordinate,double,double)> EarthSpinProjectile(Coordinate Start, double Azimuth, double AngleElevation, double LaunchSpeed, double LaunchHeight, double CoeffDrag, double CrossSectionArea, double TimeIncrement = 0.01)
        {
            Vector ENU_VelStart_Unit = AzimuthElevationtoENU(Azimuth,AngleElevation);
            ENU_VelStart_Unit /= ENU_VelStart_Unit.Magnitude(); //normalised
            //ENU_VelStart_Unit[0]=sin(Azimuth)*cos(AngleElevation);
            //ENU_VelStart_Unit[1]=cos(Azimuth)*cos(AngleElevation);
            //ENU_VelStart_Unit[2]=sin(AngleElevation);

            Vector ENU_Vel = LaunchSpeed * ENU_VelStart_Unit;
            double lam = Start.Longitude.ToDouble();
            double phi = Start.Latitude.ToDouble();

            Vector ECEF_ThrowVelocity = ENUToECEF_Velocity(ENU_Vel,Start, LaunchHeight);

            Vector EarthSpinVelStart = VelocityOfEarthSurfaceFromCoordinate(Start, TimeIncrement);

            Vector ECI_Start_Velocity=EarthSpinVelStart+ECEF_ThrowVelocity;

            Vector curPos = new Vector(3);
            Vector v = new Vector(3);
            Vector a = new Vector(3);
            curPos = GeoditicToECEFVector(Start, LaunchHeight);
            v = ECI_Start_Velocity;
            double t = 0;
            yield return (Start,LaunchHeight,0);
            double curHeight = ECEFVectorToGeoditicAndHeightZhu(curPos).Item2;

            var earthRotationMatrix = Matrix.RotationZ(-Math.Tau*TimeIncrement/secondsPerRotation);//needs be clockwise around z

            while (curHeight >= 0)
            {
                var tempsss = AccelerationDueToGrav(curPos);
                var temposss =-1* DragForceMagnitude(curHeight, v.Magnitude(), CoeffDrag, CrossSectionArea) * v / v.Magnitude();
                
                a =  tempsss+temposss;

                //var posDebug = ECEFVectorToGeoditicAndHeightZhu(curPos);

                //var dbg1 = ECEFtoENU_Velocity(tempsss, posDebug.Item1, posDebug.Item2);
                //var dbg2 = ECEFtoENU_Velocity(temposss, posDebug.Item1,posDebug.Item2);

                //var velDebug = ECEFtoENU_Velocity(v,posDebug.Item1, posDebug.Item2);
                //var accDebug = ECEFtoENU_Velocity(a,posDebug.Item1, posDebug.Item2);

                curPos = curPos + v * TimeIncrement + 0.5 * (a) * TimeIncrement * TimeIncrement;
                //lets work in ENU
                //var curPosChange = velDebug * TimeIncrement + 0.5 * (accDebug) * TimeIncrement * TimeIncrement;
                //curPosChange=ENUToECEF( curPosChange , posDebug.Item1);
                //curPos += curPosChange;


                v = v + (a) * TimeIncrement;
                //velDebug = ECEFtoENU_Velocity(v, posDebug.Item1, posDebug.Item2);

                var tempss = ECEFVectorToGeoditicAndHeightZhu(curPos); //become neg here
                curPos = earthRotationMatrix*curPos;
                
                v = earthRotationMatrix * v;
                t += TimeIncrement;

                var temps = ECEFVectorToGeoditicAndHeightZhu(curPos);
                var temps2 = ECEFVectorToGeoditicAndHeightZhu(Matrix.RotationZ(Math.Tau * t / secondsPerRotation) * curPos);
                curHeight = temps.Item2;

                var debug = ECEFtoENU_Velocity(v,temps.Item1, temps.Item2);
                debug.Print();
                Console.WriteLine(curHeight);
                if (curHeight < 0)
                {
                    curHeight = 0;
                    //ECI back to ECEF
                    var EcefPos = Matrix.RotationZ(Math.Tau * t / secondsPerRotation) *curPos;//anticlockwise to fix
                    var final=ECEFVectorToGeoditicAndHeightZhu(EcefPos);
                    yield return (final.Item1,final.Item2,t);
                    yield break;
                }
                //var temp = ECEFVectorToGeoditicAndHeightNewtonRaphson(curPos);
                yield return (temps2.Item1,temps.Item2,t);
                
            }



        }
    }

}
