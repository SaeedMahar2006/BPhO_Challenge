using CoordinateSharp;
using System;
namespace Core
{
    public class LaunchParameters3D
    {
        public LaunchParameters3D(Coordinate Launch, double AngleAz, double AngleEl, double Speed, double Height, double CoeffDrag, double CrossArea, double TimeIncrement)
        {
            launch = Launch;
            angleAz = AngleAz;
            angleEl = AngleEl;
            speed = Speed;
            height = Height;
            coeffDrag = CoeffDrag;
            crossarea = CrossArea;
            timeIncrement = TimeIncrement;

        }
        public Coordinate launch; public double angleAz; public double angleEl; public double speed; public double height; public double coeffDrag; public double crossarea; public double timeIncrement;
        //public int mapHeight; public int mapWidth;


        //public override string ToString() => $"({X}, {Y})";
    }
}