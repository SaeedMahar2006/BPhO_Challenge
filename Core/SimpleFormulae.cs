using CoordinateSharp;
using ScottPlot;
using System.ComponentModel.Design;

namespace Core
{

    public static class SimpleFormulae
    {
        //
        private static double RadToDeg = 180/Math.PI;
        public static Func<double, double> sin = x=>Math.Sin(x/RadToDeg);
        public static Func<double, double> cos = x=>Math.Cos(x/RadToDeg);
        public static Func<double, double> tan = x=>Math.Tan(x / RadToDeg);
        public static Func<double, double> atan = x=>Math.Atan(x)*RadToDeg;
        public static Func<double, double> asin = x=>Math.Asin(x)*RadToDeg;
        public static Func<double, double> acos = x=>Math.Acos(x)*RadToDeg;
        public static Func<double,double, double> atan2 = (x,y)=>Math.Atan2(x,y)*RadToDeg;
        public static Func<double,double> sqrt = Math.Sqrt;
        public static Func<double,double> cbrt = Math.Cbrt;
        public static Func<double,int> sign = Math.Sign;
        public static Func<double,double> sqr = x=>x*x;
        public static Func<double,double> cube = x=>x*x*x;
        public static Func<double,double> PrimeVerticalRadCurvature = phi=>WGS84_a/sqrt(1-WGS84_e2 * sqr(sin(phi)));


        public const double SeaLevelPressureEart = 101325;
        public const double TempLapseRateEarth = 0.00976;
        public const double ConstantPressureSpecificHeatEarth =1004.68506;
        //public const double ConstantPressureSpecificHeat =1004.68506;
        public const double SeaLevelStandTempEarth = 288.16;
        public const double MolarMassDryAirEarth = 0.02896968;
        public const double UnivGasConst = 8.314462618;

        public const double radiusEarth = 6371000;
        public const double massEarth = 5.972e24;
        public const double gravConstant = 6.6743e-11;
        public const double secondsPerRotation = 23*60*60+56*60+4.09;
        public const double WGS84_a = 6378137;
        public const double WGS84_b = 6356752.3142;

        public const double WGS84_e2 = 1-WGS84_b*WGS84_b/(WGS84_a*WGS84_a);
        public const double WGS84_ecompliment2 = WGS84_a*WGS84_a/(WGS84_b*WGS84_b)-1;

        public const double WGS84_f = 1-WGS84_a/(WGS84_b);


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
            if (LaunchSpeed == 0) return 0; //fix up NaN from appearing
            return LaunchSpeed * LaunchSpeed * (Math.Sin(Angle) * Math.Cos(Angle) + Math.Cos(Angle) *

                Math.Sqrt(Math.Pow(Math.Sin(Angle), 2) + 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed))//possibility of NaN

                ) / Gravity;
        }
        public static double AnalyticNoDragFlightTime(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight)
        {
            if (LaunchSpeed == 0) return 0; //fix up NaN from appearing
            return LaunchSpeed * (Math.Sin(Angle) +

                Math.Sqrt(Math.Pow(Math.Sin(Angle), 2) + 2 * Gravity * LaunchHeight / (LaunchSpeed * LaunchSpeed))//possibility of NaN

                ) / Gravity; ;
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
            double multiplier = LaunchSpeed * LaunchSpeed * Math.Pow(Math.Cos(Angle), 2) / Gravity;
            double lower = Gravity * (1 + Math.Pow(Math.Tan(Angle), 2)) * X / (LaunchSpeed * LaunchSpeed);

            return multiplier * (IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle)) - IntegralRequiredForLengthOfTrajectory(Math.Tan(Angle) - lower));
        }

        public static double distanceFromOriginAtTimeT(double t, double Angle, double Gravity, double LaunchSpeed)
        {

            return t * Math.Sqrt(
                LaunchSpeed * LaunchSpeed
                - Gravity * t * LaunchSpeed * (Math.Sin(Angle))
                + 0.25 * (Gravity * Gravity * t * t)
                );
        }

        public static double timeMaxDistanceFromOrigin(double t, double Angle, double Gravity, double LaunchSpeed)
        {
            return 3 * LaunchSpeed / (2 * Gravity) * Math.Sin(Angle) - LaunchSpeed / Gravity * Math.Sqrt(9 / 4 * Math.Pow(Math.Sin(Angle), 2) - 2);
        }

        public static double timeMinDistanceFromOrigin(double t, double Angle, double Gravity, double LaunchSpeed)
        {
            return 3 * LaunchSpeed / (2 * Gravity) * Math.Sin(Angle) + LaunchSpeed / Gravity * Math.Sqrt(9 / 4 * Math.Pow(Math.Sin(Angle), 2) - 2);
        }



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
        /// just see https://en.wikipedia.org/wiki/Atmospheric_pressure
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
            return SeaLevelPressure * Math.Pow((1 - Gravity * Altitude / (ConstantPressureSpecificHeat * SeaLevelTemp)), ConstantPressureSpecificHeat * MolarMassDryAir / UniversalGasConst);
        }
        public static double DryAirTempAltitudeSimple(double Altitude
    ,double TempLapseRate=0.00976
    , double SeaLevelTemp = 288.16

    )
        {
            return SeaLevelTemp+Altitude*TempLapseRate;
        }

        //TODO forward all the constants in the calculations to other func calls
        public static double DryAirDensityAltitude(double Altitude,
            double MolarMassDryAir = MolarMassDryAirEarth,
            double GasConstant = UnivGasConst
            )
        {
            return DryAirPressureAltitude(Altitude)*MolarMassDryAir/(GasConstant*DryAirTempAltitudeSimple(Altitude));
        }

        public static double DragForceMagnitude(double altitude, double speed,double dragCoeff, double crossSectionArea)
        {
            return 0.5*DryAirDensityAltitude(altitude)*sqr(speed)*crossSectionArea*dragCoeff;
        }

        public static IEnumerable<Coordinates> Drag(double Angle, double Gravity, double LaunchSpeed, double LaunchHeight, double CoeffDrag, double TimeIncrement = 0.01)
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
            while (curPos[1]>0)
            {
                
                a[0] = -v[0]*CoeffDrag*v.Magnitude();
                a[1] = -Gravity - v[1] * CoeffDrag * v.Magnitude();

                curPos = curPos + v * TimeIncrement + 0.5 * (a) * TimeIncrement * TimeIncrement;

                v = v + (a) * TimeIncrement;
                t += TimeIncrement;
                if (curPos[1] < 0)
                {
                    curPos[1] = 0;
                    yield return curPos;
                    yield break;
                }
                yield return curPos;
            }

        }

        public static double AltitudeFromCoordiantesSphere(Vector coord)
        {
            return coord.Magnitude() - radiusEarth;
        }
        public static double AltitudeFromCoordinatesWGS84(Vector coord)
        {
            return ECEFVectorToGeoditicAndHeightNewtonRaphson(coord).Item2;
        }
        public static Vector AccelerationDueToGrav(Vector Position)
        {
            return Position * massEarth * gravConstant / (Math.Pow(Position*Position,1.5));
        }
        public static Vector ProjectOntoSurfaceOfEarthVector(Vector Position)
        {
            return radiusEarth * Position / Position.Magnitude();
        }
        public static Coordinate ProjectOntoSurfaceOfEarthCoord(Vector Position)
        {
            return ECEFVectorToGeoditicHeikkinen(Position);
        }
       

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
            double z1 = (q-WGS84_e2)*ECEF[2]/(t-l);

            double lat = atan(z1/((1-WGS84_e2)*w1));   //MAYBE atan2
            double longi =2 * atan((w - ECEF[0]) / ECEF[1]);
            double h = sign(t-1+l)*sqrt(sqr(w-w1)   + sqr(ECEF[2] -z1));

            return (new Coordinate(lat,longi),h);
        }


        public static (Coordinate, double) ECEFVectorToGeoditicAndHeightNewtonRaphson(Vector ecef) {

            double x = ecef[0];
            double y = ecef[1];
            double z = ecef[2];

            //  # --- derived constants

            double e = Math.Sqrt(Math.Pow(WGS84_a, 2.0) - Math.Pow(WGS84_b, 2.0)) / WGS84_a;
            double clambda = Math.Atan2(y, x);
            double p = Math.Sqrt(Math.Pow(x, 2.0) + Math.Pow(y, 2));
            double h_old = 0.0;
            //# first guess with h=0 meters
            double theta = Math.Atan2(z, p * (1.0 - Math.Pow(e, 2.0)));
            double cs = Math.Cos(theta);
            double sn = Math.Sin(theta);
            double N = Math.Pow(WGS84_a, 2.0) / Math.Sqrt(Math.Pow(WGS84_a * cs, 2.0) + Math.Pow(WGS84_b * sn, 2.0));
            double h = p / cs - N;
            while (Math.Abs(h - h_old) > 1.0e-12) {
                h_old = h;
                theta = Math.Atan2(z, p * (1.0 - Math.Pow(e, 2.0) * N / (N + h)));
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


        public static Vector ENUToECEF(Vector ENU, Coordinate Geoditic)
        {
            double lam = Geoditic.Longitude.ToDouble();
            double phi = Geoditic.Latitude.ToDouble();
            var matForEnuToEcef = Matrix.RotationZ(-(Math.PI + lam)) * Matrix.RotationX(-(Math.PI - phi));
            return matForEnuToEcef* ENU;
        }

        public static Vector ECEFtoENU(Vector ECEF, Coordinate? Geoditic=null)
        {
            if (Geoditic!=null) {
                double lam = Geoditic.Longitude.ToDouble();
                double phi = Geoditic.Latitude.ToDouble();
                var matForEnuToEcef = (Matrix.RotationZ(-(Math.PI + lam)) * Matrix.RotationX(-(Math.PI - phi))).Transpose();
                return matForEnuToEcef * ECEF;
            } else {
                Geoditic = ECEFVectorToGeoditicAndHeightNewtonRaphson(ECEF).Item1;
                double lam = Geoditic.Longitude.ToDouble();
                double phi = Geoditic.Latitude.ToDouble();
                var matForEnuToEcef = (Matrix.RotationZ(-(Math.PI + lam)) * Matrix.RotationX(-(Math.PI - phi))).Transpose();
                return matForEnuToEcef * ECEF;
            }
        }

        public static Vector AzimuthElevationtoENU(double Azimuth, double Elevation)
        {
            Vector ENU = new Vector(3);
            ENU[0] = sin(Azimuth) * cos(Elevation);
            ENU[1] = cos(Azimuth) * cos(Elevation);
            ENU[2] = sin(Elevation);
            return ENU;
        }


        /// <summary>
        /// Angles in degrees
        /// </summary>
        /// <param name="Start"></param>
        /// <param name="Azimuth"></param>
        /// <param name="AngleElevation"></param>
        /// <param name="LaunchSpeed"></param>
        /// <param name="LaunchHeight"></param>
        /// <param name="CoeffDrag"></param>
        /// <param name="TimeIncrement"></param>
        /// <returns></returns>

        public static IEnumerable<Coordinate> EarthSpinProjectile(Coordinate Start, double Azimuth, double AngleElevation, double LaunchSpeed, double LaunchHeight, double CoeffDrag, double CrossSectionArea, double TimeIncrement = 0.01)
        {
            Vector ENU_VelStart_Unit = AzimuthElevationtoENU(Azimuth,AngleElevation);
            ENU_VelStart_Unit /= ENU_VelStart_Unit.Magnitude(); //normalised
            //ENU_VelStart_Unit[0]=sin(Azimuth)*cos(AngleElevation);
            //ENU_VelStart_Unit[1]=cos(Azimuth)*cos(AngleElevation);
            //ENU_VelStart_Unit[2]=sin(AngleElevation);

            Vector ENU_Vel = LaunchSpeed * ENU_VelStart_Unit;
            double lam = Start.Longitude.ToDouble();
            double phi = Start.Latitude.ToDouble();

            Vector ECEF_ThrowVelocity = ENUToECEF(ENU_Vel,Start);

            Vector EarthSpinVelStart = VelocityOfEarthSurfaceFromCoordinate(Start, TimeIncrement);

            Vector ECI_Start_Velocity=EarthSpinVelStart+ECEF_ThrowVelocity;

            Vector curPos = new Vector(3);
            Vector v = new Vector(3);
            Vector a = new Vector(3);
            curPos = GeoditicToECEFVector(Start, LaunchHeight);
            v = ECI_Start_Velocity;
            double t = 0;
            yield return Start;
            double curHeight = ECEFVectorToGeoditicAndHeightNewtonRaphson(curPos).Item2;

            var earthRotationMatrix = Matrix.RotationZ(-Math.Tau*TimeIncrement/secondsPerRotation);//needs be clockwise around z

            while (curHeight >= 0)
            {
                var tempsss = AccelerationDueToGrav(curPos);
                var temposss = DragForceMagnitude(curHeight, v.Magnitude(), CoeffDrag, CrossSectionArea) * v / v.Magnitude();
                a =  tempsss+temposss;

                var posDebug = ECEFVectorToGeoditicAndHeightNewtonRaphson(curPos);
                var velDebug = ECEFtoENU(v,posDebug.Item1);
                var accDebug = ECEFtoENU(a,posDebug.Item1);

                //curPos = curPos + v * TimeIncrement + 0.5 * (a) * TimeIncrement * TimeIncrement;
                //lets work in ENU
                var curPosChange = velDebug * TimeIncrement + 0.5 * (accDebug) * TimeIncrement * TimeIncrement;
                curPosChange=ENUToECEF( curPosChange , posDebug.Item1);
                curPos += curPosChange;


                v = v + (a) * TimeIncrement;
                velDebug = ECEFtoENU(v, posDebug.Item1);

                var tempss = ECEFVectorToGeoditicAndHeightNewtonRaphson(curPos); //become neg here
                curPos = earthRotationMatrix*curPos;
                
                v = earthRotationMatrix * v;
                t += TimeIncrement;

                var temps = ECEFVectorToGeoditicAndHeightNewtonRaphson(curPos);
                curHeight = temps.Item2;

                var debug = ECEFtoENU(v,temps.Item1);
                debug.Print();
                if (curHeight < 0)
                {
                    curHeight = 0;
                    //ECI back to ECEF
                    var EcefPos = Matrix.RotationZ(Math.Tau * t / secondsPerRotation) *curPos;//anticlockwise to fix
                    yield return ECEFVectorToGeoditicAndHeightNewtonRaphson( EcefPos).Item1;
                    yield break;
                }
                //var temp = ECEFVectorToGeoditicAndHeightNewtonRaphson(curPos);
                yield return temps.Item1;
                
            }
        }
    }

}
