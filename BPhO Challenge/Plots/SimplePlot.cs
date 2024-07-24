using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPhO_Challenge.Plots
{
    public partial class SimplePlot : UserControl
    {
        public event EventHandler Close;
        //public Func<double, double>? FuncToPlot=null;
        //public Func<double[], IEnumerable<(double,double)>>? FunctionPointsToPlot=null;
        //public double[]? FunctionPointsToPlotArgs=null;
        public SimplePlot()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void selectorList1_Load(object sender, EventArgs e)
        {

        }
        public void SetParameterList(List<ParameterSpecification> parameterList)
        {
            selectorList1.SetParameterSpecification(parameterList);
            selectorList1.LoadParameterSpecification();
        }
        public void RedrawGraphTask1()
        {
            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            double height = (double)selectorList1.GetValueByName("Height");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            var noDragPlot = formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            noDragPlot.LegendText = "No Drag Fixed Time Increment";
            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();
        }
        public void TaskOneSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask1();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask1();
        }




        public void RedrawGraphTask2()
        {
            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            double height = (double)selectorList1.GetValueByName("Height");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            var f = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            f.MinX = 0;
            f.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            var apogeeMarker = formsPlot.Plot.Add.Marker(SimpleFormulae.AnalyticNoDragApogee(angle, gravity, speed, height));
            apogeeMarker.LegendText = "Apogee";
            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();
        }
        public void taskTwoSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask2();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask2();
        }




        public void RedrawGraphTask3()
        {
            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            double height = (double)selectorList1.GetValueByName("Height");
            double X = (double)selectorList1.GetValueByName("X");
            double Y = (double)selectorList1.GetValueByName("Y");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            double MinUAngle = SimpleFormulae.ThroughPointMinSpeedAngle(gravity, X, Y);
            double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var MinUParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, MinUAngle, gravity, MinU, height));
            MinUParabola.MinX = 0;
            MinUParabola.MaxX = X;
            MinUParabola.LegendText = "minimal speed trajectory";


            double lowBallAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var LowBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, lowBallAngle, gravity, speed, height));
            LowBallParabola.MinX = 0;
            LowBallParabola.MaxX = X;
            LowBallParabola.LegendText = "low ball trajectory";

            double HighBallAngle = SimpleFormulae.ThroughPointHighBallAngle(gravity, speed, X, Y);
            var HighBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, HighBallAngle, gravity, speed, height));
            HighBallParabola.MinX = 0;
            HighBallParabola.MaxX = X;
            HighBallParabola.LegendText = "high ball trajectory";

            var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            apogeeMarker.LegendText = "Target";

            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();
        }
        public void taskThreeSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "X";
            s3.DescriptiveName = "X Coordinate";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];

            var s4 = new ParameterSpecification();
            s4.Name = "Y";
            s4.DescriptiveName = "Y Coordinate";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s6 = new ParameterSpecification();
            s6.Name = "Height";
            s6.DescriptiveName = "Launch Height";
            s6.IsNumeric = true;
            s6.BoundsAndStep = [0, 10000, 0.001];


            var s7 = new ParameterSpecification();
            s7.Name = "Gravity";
            s7.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s7.IsNumeric = true;
            s7.BoundsAndStep = [0, 10000, 0.001];

            var s8 = new ParameterSpecification();
            s8.Name = "TimeIncrement";
            s8.DescriptiveName = "Seconds per time step";
            s8.IsNumeric = true;
            s8.BoundsAndStep = [0.000001, 10000, 0.000001];

            //specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);//yes oopsies missed 5
            specs.Add(s6);
            specs.Add(s7);
            specs.Add(s8);
            SetParameterList(specs);

            RedrawGraphTask3();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask3();
        }





        public void RedrawGraphTask4()
        {
            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            double height = (double)selectorList1.GetValueByName("Height");
            double angle = (double)selectorList1.GetValueByName("Angle");
            //double Y = (double)selectorList1.GetValueByName("Y");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            //double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            maxRangeParabola.MinX = 0;
            maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.LegendText = "max range trajectory";


            //double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            userParabola.MinX = 0;
            userParabola.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            userParabola.LegendText = "user trajectory";



            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";

            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();
        }
        public void taskFourSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask4();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask4();
        }






        public void RedrawGraphTask5()
        {
            formsPlot.Plot.Clear();

            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            double height = (double)selectorList1.GetValueByName("Height");
            double angle = (double)selectorList1.GetValueByName("Angle");
            //double Y = (double)selectorList1.GetValueByName("Y");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double X = (double)selectorList1.GetValueByName("X");
            double Y = (double)selectorList1.GetValueByName("Y");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            //double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            maxRangeParabola.MinX = 0;
            var d = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.LegendText = "max range trajectory";




            double MinUAngle = SimpleFormulae.ThroughPointMinSpeedAngle(gravity, X, Y);
            double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var MinUParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, MinUAngle, gravity, MinU, height));
            MinUParabola.MinX = 0;
            MinUParabola.MaxX = X;
            MinUParabola.LegendText = "minimal speed trajectory";


            double lowBallAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var LowBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, lowBallAngle, gravity, speed, height));
            LowBallParabola.MinX = 0;
            LowBallParabola.MaxX = X;
            LowBallParabola.LegendText = "low ball trajectory";

            double HighBallAngle = SimpleFormulae.ThroughPointHighBallAngle(gravity, speed, X, Y);
            var HighBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, HighBallAngle, gravity, speed, height));
            HighBallParabola.MinX = 0;
            HighBallParabola.MaxX = X;
            HighBallParabola.LegendText = "high ball trajectory";

            var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            apogeeMarker.LegendText = $"Target ({X},{Y})";





            //double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            userParabola.MinX = 0;
            userParabola.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            userParabola.LegendText = "user trajectory";

            var boundingParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.BoundingHeightAtDistanceX(x, gravity, speed, height));
            //var v =boundingParabola.Axes.XAxis=;
            boundingParabola.MinX = 0;
            //if(speed!=0)boundingParabola.MaxX = maxRangeParabola.MaxX;
            //else { boundingParabola.MaxX = double.PositiveInfinity; }
            boundingParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            boundingParabola.LegendText = "Boudning parabola";

            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";

            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();
        }
        public void taskFiveSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var sx = new ParameterSpecification();
            sx.Name = "X";
            sx.DescriptiveName = "X Coordinate";
            sx.IsNumeric = true;
            sx.BoundsAndStep = [0, 10000, 0.001];

            var sy = new ParameterSpecification();
            sy.Name = "Y";
            sy.DescriptiveName = "Y Coordinate";
            sy.IsNumeric = true;
            sy.BoundsAndStep = [0, 10000, 0.001];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(sx);
            specs.Add(sy);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask5();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask5();
        }
















        public void RedrawGraphTask6()
        {

            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            double height = (double)selectorList1.GetValueByName("Height");
            double angle = (double)selectorList1.GetValueByName("Angle");
            //double Y = (double)selectorList1.GetValueByName("Y");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            //double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            maxRangeParabola.MinX = 0;
            maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.LegendText = "max range trajectory";


            //double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            userParabola.MinX = 0;
            userParabola.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            userParabola.LegendText = "user trajectory";



            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";
            Func<double, double> f = x => SimpleFormulae.LengthOfTrajectory(x, angle, gravity, speed, height);
            var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Distance traveled" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            outputTableControl1.InsertRecord(new List<object> { f(range) });
            outputTableControl1.Refresh();

            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();

        }
        public void taskSixSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask6();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask6();
        }












        public void RedrawGraphTask7()
        {

            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            //double height = (double)selectorList1.GetValueByName("Height");
            double angle = (double)selectorList1.GetValueByName("Angle");
            //double Y = (double)selectorList1.GetValueByName("Y");
            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            ////formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            ////ScottPlot.Rendering.RenderActions;
            ////formsPlot.Render();
            //double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            ////double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            //var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            //maxRangeParabola.MinX = 0;
            //maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            //maxRangeParabola.LegendText = "max range trajectory";


            ////double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            //var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            //userParabola.MinX = 0;
            //userParabola.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //userParabola.LegendText = "user trajectory";



            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";
            //Func<double, double> f = x => SimpleFormulae.LengthOfTrajectory(x, angle, gravity, speed, height);
            //var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            //outputTableControl1.ClearTable();
            //outputTableControl1.SetColumnNames(new List<string> { "Distance traveled" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            //outputTableControl1.InsertRecord(new List<object> { f(range) });
            //outputTableControl1.Refresh();

            Func<double, double> f = t => SimpleFormulae.distanceFromOriginAtTimeT(t, angle, gravity, speed);
            //var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            var distanceCurve = formsPlot.Plot.Add.Function(f);
            distanceCurve.LegendText = "Distance from origin";

            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();

        }
        public void taskSevenSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            //var s3 = new ParameterSpecification();
            //s3.Name = "Height";
            //s3.DescriptiveName = "Launch Height (m)";
            //s3.IsNumeric = true;
            //s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            //specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask7();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask7();
        }













        public void RedrawGraphTask8()
        {

            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            //double height = (double)selectorList1.GetValueByName("Height");
            double angle = (double)selectorList1.GetValueByName("Angle");
            //double Y = (double)selectorList1.GetValueByName("Y");
            double restit = (double)selectorList1.GetValueByName("Restitution");
            double height = (double)selectorList1.GetValueByName("Height");
            int bounces = (int)selectorList1.GetValueByName("Bounces");

            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            ////formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            ////ScottPlot.Rendering.RenderActions;
            ////formsPlot.Render();
            //double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            ////double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            //var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            //maxRangeParabola.MinX = 0;
            //maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            //maxRangeParabola.LegendText = "max range trajectory";


            ////double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            //var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            //userParabola.MinX = 0;
            //userParabola.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //userParabola.LegendText = "user trajectory";



            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";
            //Func<double, double> f = x => SimpleFormulae.LengthOfTrajectory(x, angle, gravity, speed, height);
            //var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            //outputTableControl1.ClearTable();
            //outputTableControl1.SetColumnNames(new List<string> { "Distance traveled" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            //outputTableControl1.InsertRecord(new List<object> { f(range) });
            //outputTableControl1.Refresh();

            //Func<double, double> f = t => SimpleFormulae.distanceFromOriginAtTimeT(t, angle, gravity, speed);
            //var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            var bounceCurve = formsPlot.Plot.Add.Scatter(SimpleFormulae.Bounce(angle,gravity,speed,height,restit,timeIncrement,bounces).ToList());
            bounceCurve.LegendText = "Trajectory";

            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();

        }
        public void taskEightSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s6 = new ParameterSpecification();
            s6.Name = "Restitution";
            s6.DescriptiveName = "Constant of Restitution";
            s6.IsNumeric = true;
            s6.BoundsAndStep = [0, 1, 0.001];

            var s7 = new ParameterSpecification();
            s7.Name = "Bounces";
            s7.DescriptiveName = "Bounces";
            s7.IsNumeric = true;
            s7.BoundsAndStep = [0, 100, 1];

            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.0001, 10000, 0.0001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s6);
            specs.Add(s7);
            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask8();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask8();
        }









        public void RedrawGraphTask9()
        {

            formsPlot.Plot.Clear();
            //formsPlot.Plot.Legend.
            //if (FuncToPlot!=null)
            //{
            //    formsPlot.Plot.Add.Function(FuncToPlot);
            //}
            //else if (FunctionPointsToPlot != null)
            //{
            //    var source = FunctionPointsToPlot(FunctionPointsToPlotArgs).ToList();
            //    formsPlot.Plot.Add.Scatter( source.Select(x=>x.Item1).ToList(), source.Select(x => x.Item2).ToList());
            //}
            //double angle = (double)selectorList1.GetValueByName("Angle");
            double speed = (double)selectorList1.GetValueByName("Speed");
            //double height = (double)selectorList1.GetValueByName("Height");
            double angle = (double)selectorList1.GetValueByName("Angle");
            //double Y = (double)selectorList1.GetValueByName("Y");

            double height = (double)selectorList1.GetValueByName("Height");
            double coeffDrag = (double)selectorList1.GetValueByName("Drag");


            double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            ////formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            ////ScottPlot.Rendering.RenderActions;
            ////formsPlot.Render();
            //double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            ////double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            //var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            //maxRangeParabola.MinX = 0;
            //maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            //maxRangeParabola.LegendText = "max range trajectory";


            ////double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            //var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            //userParabola.MinX = 0;
            //userParabola.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //userParabola.LegendText = "user trajectory";



            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";
            //Func<double, double> f = x => SimpleFormulae.LengthOfTrajectory(x, angle, gravity, speed, height);
            //var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            //outputTableControl1.ClearTable();
            //outputTableControl1.SetColumnNames(new List<string> { "Distance traveled" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            //outputTableControl1.InsertRecord(new List<object> { f(range) });
            //outputTableControl1.Refresh();

            //Func<double, double> f = t => SimpleFormulae.distanceFromOriginAtTimeT(t, angle, gravity, speed);
            //var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            //var time = SimpleFormulae.AnalyticNoDragFlightTime(angle, gravity, speed, height);
            var dragCurve = formsPlot.Plot.Add.ScatterLine(SimpleFormulae.Drag(angle, gravity, speed, height, coeffDrag, timeIncrement).ToList());
            dragCurve.LegendText = "Trajectory with Drag";

            var noDrag = formsPlot.Plot.Add.Function(x =>SimpleFormulae.AnalyticNoDragHeight(x,angle,gravity,speed,height));
            noDrag.LegendText = "Trajectory without Drag";

            noDrag.MinX = 0;
            noDrag.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);


            formsPlot.Plot.ShowLegend();
            formsPlot.Refresh();
            formsPlot.Update();

        }
        public void taskNineSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            var s1 = new ParameterSpecification();
            s1.Name = "Angle";
            s1.DescriptiveName = "Launch Angle (radians)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];


            var s4 = new ParameterSpecification();
            s4.Name = "Gravity";
            s4.DescriptiveName = "Acceleration due to gravity (m/s^2)";
            s4.IsNumeric = true;
            s4.BoundsAndStep = [0, 10000, 0.001];

            var s6 = new ParameterSpecification();
            s6.Name = "Drag";
            s6.DescriptiveName = "Coefficient of Drag";
            s6.IsNumeric = true;
            s6.BoundsAndStep = [0, 1, 0.001];


            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.0001, 10000, 0.0001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s6);

            specs.Add(s5);
            SetParameterList(specs);

            RedrawGraphTask9();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask9();
        }





        private void CloseB_Click(object sender, EventArgs e)
        {
            EventHandler handler = Close;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void outputTableControl1_Load(object sender, EventArgs e)
        {

        }

        private void formsPlot_Load(object sender, EventArgs e)
        {

        }
    }
}
