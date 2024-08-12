using CoordinateSharp;
using Core;
using ScottPlot;
using ScottPlot.Plottables;
using ScottPlot.WinForms;
using System;
using System.Collections.Concurrent;
//using System.Collections.Generic;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace BPhO_Challenge.Plots
{
    public partial class SimplePlot : UserControl
    {
        public event EventHandler CloseEvent;
        private ConcurrentQueue<Coordinates> ToPlotQueue = new ConcurrentQueue<Coordinates>();

        readonly System.Windows.Forms.Timer UpdatePlotTimer = new() { Interval = 50, Enabled = true };
        private ScottPlot.Plottables.DataLogger Logger = new DataLogger();
        readonly ScottPlot.Image earthImage = new ScottPlot.Image("earth.png");
        private object Lock = new object();


        // This will get the current WORKING directory (i.e. \bin\Debug)
        //string workingDirectory = Environment.CurrentDirectory;
        //// or: Directory.GetCurrentDirectory() gives the same result

        //// This will get the current PROJECT bin directory (ie ../bin/)
        //string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

        // This will get the current PROJECT directory
        //string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;


        string Directory = Environment.CurrentDirectory;

        //public Func<double, double>? FuncToPlot=null;
        //public Func<double[], IEnumerable<(double,double)>>? FunctionPointsToPlot=null;
        //public double[]? FunctionPointsToPlotArgs=null;
        public SimplePlot()
        {
            InitializeComponent();
            //UpdatePlotTimer.Tick += ExtLivePlot;

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

            var data = SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList();
            var noDragPlot = formsPlot.Plot.Add.Scatter(data);
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();

            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Time", "Horizontal Range", "Height" });

            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }

            double t = 0;
            foreach (var datum in data) { outputTableControl1.InsertRecord(new List<object> { t, datum.X, datum.Y }); t += timeIncrement; }
            outputTableControl1.Refresh();

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

            richTextBox.Text = File.ReadAllText("Text/Task1.txt");

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
            Func<double,double> f = x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height);
            var fplot = formsPlot.Plot.Add.Function(f);
            fplot.MinX = 0;
            var range= SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            fplot.MaxX = range;
            var apog = SimpleFormulae.AnalyticNoDragApogee(angle, gravity, speed, height);
            var apogeeMarker = formsPlot.Plot.Add.Marker(apog);
            apogeeMarker.LegendText = "Apogee";

            outputTableControl1.ClearTable(); outputTableControl1.SetColumnNames(new List<string> { "Fraction of Range", "Horizontal Distance", "Vertical Distance", "Angle", "Speed", "Gravity", "Apogee Height", "Apogee Range", "Range" });
        

            foreach (double x in SimpleFormulae.DistanceSamplesFromFlight(angle,gravity,speed,height,timeIncrement)) {
                outputTableControl1.InsertRecord(new List<object> {x/range ,x, f(x),angle, speed, gravity, apog.Y, apog.X, range });
            }

            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }

            //double t = 0;


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
            s5.BoundsAndStep = [0.01, 10000, 0.01];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);

            richTextBox.Text = File.ReadAllText("Text/Task2.txt");

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
            //double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();

            double translatedY = Y - height;

            double MinUAngle = SimpleFormulae.ThroughPointMinSpeedAngle(gravity, X, translatedY);
            double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, translatedY);
            var MinUParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, MinUAngle, gravity, MinU, height));
            MinUParabola.MinX = 0;
            MinUParabola.MaxX = X;
            MinUParabola.LegendText = "minimal speed trajectory";


            double lowBallAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, translatedY);
            var LowBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, lowBallAngle, gravity, speed, height));
            LowBallParabola.MinX = 0;
            LowBallParabola.MaxX = X;
            LowBallParabola.LegendText = "low ball trajectory";

            double HighBallAngle = SimpleFormulae.ThroughPointHighBallAngle(gravity, speed, X, translatedY);
            var HighBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, HighBallAngle, gravity, speed, height));
            HighBallParabola.MinX = 0;
            HighBallParabola.MaxX = X;
            HighBallParabola.LegendText = "high ball trajectory";

            var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            apogeeMarker.LegendText = "Target";


            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Launch Speed", "Gravity", "Target X", "Target Y", "Low ball angle", "High ball angle", "Minimum speed", "Minimum speed angle" });

            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }

            //double t = 0;
            outputTableControl1.InsertRecord(new List<object> { speed, gravity, X, Y, lowBallAngle, HighBallAngle, MinU, MinUAngle });

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

            //var s8 = new ParameterSpecification();
            //s8.Name = "TimeIncrement";
            //s8.DescriptiveName = "Seconds per time step";
            //s8.IsNumeric = true;
            //s8.BoundsAndStep = [0.000001, 10000, 0.000001];

            //specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);//yes oopsies missed 5
            specs.Add(s6);
            specs.Add(s7);
            //specs.Add(s8);
            SetParameterList(specs);

            richTextBox.Text = File.ReadAllText("Text/Task3.txt");

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
            //double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            //double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            maxRangeParabola.MinX = 0;
            var maxrange = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.MaxX = maxrange;
            maxRangeParabola.LegendText = "max range trajectory";


            //double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            userParabola.MinX = 0;
            var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            userParabola.MaxX = range;
            userParabola.LegendText = "user trajectory";



            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Angle", "Speed", "Gravity", "Range", "Max Range Angle", "Max Range" });

            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }

            //double t = 0;
            outputTableControl1.InsertRecord(new List<object> { angle, speed, gravity, range, maxRangeAngle, maxrange });


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

            //var s5 = new ParameterSpecification();
            //s5.Name = "TimeIncrement";
            //s5.DescriptiveName = "Seconds per time step";
            //s5.IsNumeric = true;
            //s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            //specs.Add(s5);
            SetParameterList(specs);

            richTextBox.Text = File.ReadAllText("Text/Task4.txt");

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
            //double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            double translatedY = Y - height;

            //formsPlot.Plot.Add.Scatter(SimpleFormulae.FixedTimeIncrementNoDragHeightEnumerable(angle, gravity, speed, height, timeIncrement).ToList());
            //ScottPlot.Rendering.RenderActions;
            //formsPlot.Render();
            double maxRangeAngle = SimpleFormulae.MaxRangeAngle(gravity, speed, height);
            //double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, Y);
            var maxRangeParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, maxRangeAngle, gravity, speed, height));
            maxRangeParabola.MinX = 0;
            var maxrange = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.MaxX = SimpleFormulae.MaxRange(gravity, speed, height);
            maxRangeParabola.LegendText = "max range trajectory";




            double MinUAngle = SimpleFormulae.ThroughPointMinSpeedAngle(gravity, X, translatedY);
            double MinU = SimpleFormulae.ThroughPointMinSpeed(gravity, X, translatedY);
            var MinUParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, MinUAngle, gravity, MinU, height));
            MinUParabola.MinX = 0;
            MinUParabola.MaxX = X;
            MinUParabola.LegendText = "minimal speed trajectory";


            double lowBallAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, translatedY);
            var LowBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, lowBallAngle, gravity, speed, height));
            LowBallParabola.MinX = 0;
            LowBallParabola.MaxX = X;
            LowBallParabola.LegendText = "low ball trajectory";

            double HighBallAngle = SimpleFormulae.ThroughPointHighBallAngle(gravity, speed, X, translatedY);
            var HighBallParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, HighBallAngle, gravity, speed, height));
            HighBallParabola.MinX = 0;
            HighBallParabola.MaxX = X;
            HighBallParabola.LegendText = "high ball trajectory";

            var targetMarker = formsPlot.Plot.Add.Marker(X, Y);
            targetMarker.LegendText = $"Target ({X},{Y})";





            //double userAngle = SimpleFormulae.ThroughPointLowBallAngle(gravity, speed, X, Y);
            var userParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            userParabola.MinX = 0;
            var range = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);
            userParabola.MaxX = range;
            userParabola.LegendText = "user trajectory";

            var boundingParabola = formsPlot.Plot.Add.Function(x => SimpleFormulae.BoundingHeightAtDistanceX(x, gravity, speed, height));
            //var v =boundingParabola.Axes.XAxis=;
            boundingParabola.MinX = 0;
            //if(speed!=0)boundingParabola.MaxX = maxRangeParabola.MaxX;
            //else { boundingParabola.MaxX = double.PositiveInfinity; }
            boundingParabola.MaxX = maxrange;
            boundingParabola.LegendText = "Boudning parabola";

            //var apogeeMarker = formsPlot.Plot.Add.Marker(X, Y);
            //apogeeMarker.LegendText = "Target";

            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Angle", "Speed", "Gravity", "Range", "Max Range Angle", "Max Range", "Target X", "Target Y", "Low ball angle", "High ball angle", "Minimum speed", "Minimum speed angle" });

            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }

            //double t = 0;
            outputTableControl1.InsertRecord(new List<object> { angle, speed, gravity, range, maxRangeAngle, maxrange, X, Y, lowBallAngle, HighBallAngle, MinU, MinUAngle });

            //richTextBox.Text = File.ReadAllText("Text/Task5.txt");
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

            //var s5 = new ParameterSpecification();
            //s5.Name = "TimeIncrement";
            //s5.DescriptiveName = "Seconds per time step";
            //s5.IsNumeric = true;
            //s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(sx);
            specs.Add(sy);
            // specs.Add(s5);
            SetParameterList(specs);

            richTextBox.Text = File.ReadAllText("Text/Task5.txt");

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
            //double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


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
            outputTableControl1.SetColumnNames(new List<string> { "Angle", "Speed", "Gravity", "Range", "Distance traveled" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            outputTableControl1.InsertRecord(new List<object> { angle, speed, gravity, range, f(range) });
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

            //var s5 = new ParameterSpecification();
            //s5.Name = "TimeIncrement";
            //s5.DescriptiveName = "Seconds per time step";
            //s5.IsNumeric = true;
            //s5.BoundsAndStep = [0.000001, 10000, 0.000001];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            //specs.Add(s5);
            SetParameterList(specs);
            richTextBox.Text = File.ReadAllText("Text/Task6.txt");
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

            var doubleMinDistTime = SimpleFormulae.timeMinDistanceFromOrigin(angle,gravity,speed);
            var doubleMaxDistTime = SimpleFormulae.timeMaxDistanceFromOrigin(angle,gravity,speed);

            var maxima = formsPlot.Plot.Add.Marker(doubleMaxDistTime,f(doubleMaxDistTime));
            var minima = formsPlot.Plot.Add.Marker(doubleMinDistTime,f(doubleMinDistTime));

            maxima.LegendText = "Local maximum";
            minima.LegendText = "Local minimum";

            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Time", "Launch Angle", "Launch Speed", "Gravity", "Distance from origin" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            if(gravity!=0) foreach (double t in SimpleFormulae.TimeSamplesFromFlight(angle, gravity, speed, Height, timeIncrement)) { outputTableControl1.InsertRecord(new List<object> { t, angle, speed, gravity, f(t) }); }
            outputTableControl1.Refresh();


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
            s5.BoundsAndStep = [0.01, 10000, 0.01];

            specs.Add(s1);
            specs.Add(s2);
            //specs.Add(s3);
            specs.Add(s4);
            specs.Add(s5);
            SetParameterList(specs);
            richTextBox.Text = File.ReadAllText("Text/Task7.txt");
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
            var data = SimpleFormulae.Bounce(angle, gravity, speed, height, restit, timeIncrement, bounces);
            var plotData = new List<Coordinates>();
            var tableData = new List<(double, int)>();//time , bounces
            int bounced = (height == 0) ? -1 : 0;
            double t = 0;
            foreach (var d in data)
            {
                if (d.Y == 0)//we leveled it to 0
                {
                    bounced++;
                }
                plotData.Add(d);
                tableData.Add((t, bounced));
                t += timeIncrement;
            }

            var bounceCurve = formsPlot.Plot.Add.Scatter(plotData);
            bounceCurve.LegendText = "Trajectory";


            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Time", "Launch Angle", "Launch Speed", "Gravity", "Height", "Bounces" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            foreach (var d in tableData) { outputTableControl1.InsertRecord(new List<object> { d.Item1, angle, speed, gravity, d.Item2 }); }
            outputTableControl1.Refresh();

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
            s5.BoundsAndStep = [0.01, 10000, 0.01];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s6);
            specs.Add(s7);
            specs.Add(s5);
            SetParameterList(specs);
            richTextBox.Text = File.ReadAllText("Text/Task8.txt");
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
            var data = SimpleFormulae.Drag(angle, gravity, speed, height, coeffDrag, timeIncrement).ToList();
            //var plotData = new List<(double, double)>(); //time height
            var dragCurve = formsPlot.Plot.Add.ScatterLine(data);
            dragCurve.LegendText = "Trajectory with Drag";

            var noDrag = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            noDrag.LegendText = "Trajectory without Drag";

            noDrag.MinX = 0;
            noDrag.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);


            outputTableControl1.ClearTable();
            outputTableControl1.SetColumnNames(new List<string> { "Time", "Launch Angle", "Launch Speed", "Gravity", "Height" });
            //for (int i = 0; i < 10; i++)
            //{ outputTableControl1.InsertRecord(new List<object> { (time * i / 10), (range*i/10) }); }


            foreach (var d in data.Select((x, i) => (i * timeIncrement, x.Y))) { outputTableControl1.InsertRecord(new List<object> { d.Item1, angle, speed, gravity, d.Item2 }); }
            outputTableControl1.Refresh();

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
            s5.BoundsAndStep = [0.01, 10000, 0.01];

            specs.Add(s1);
            specs.Add(s2);
            specs.Add(s3);
            specs.Add(s4);
            specs.Add(s6);

            specs.Add(s5);
            SetParameterList(specs);
            richTextBox.Text = File.ReadAllText("Text/Task9.txt");
            RedrawGraphTask9();
            selectorList1.ValueChanged += (s, e) => RedrawGraphTask9();
        }












        private void ExtAddPointsLiveFromQueue(List<Coordinates> Coords, ConcurrentQueue<Coordinates> queue)
        {
            while (!queue.IsEmpty)
            {
                Coordinates item;
                //ToPlotQueue.TryDequeue(out item);
                if (queue.TryDequeue(out item))
                {//should always be as deque on UI thread
                    Coords.Add(item);
                }
                else
                {
                    break;
                }
            }
        }


        public void RedrawGraphExt(FormsPlot formsPlot2)
        {

            formsPlot.Plot.Clear<Scatter>();
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
            var earth = new ScottPlot.Image("earth.png");
            int width = earth.Width;
            int heightearth = earth.Height;

            double speed = (double)selectorList1.GetValueByName("Speed");
            //double height = (double)selectorList1.GetValueByName("Height");
            double angleAz = (double)selectorList1.GetValueByName("AngleAzimuth");
            double angleEl = (double)selectorList1.GetValueByName("AngleElevation");
            //double Y = (double)selectorList1.GetValueByName("Y");

            double lat = (double)selectorList1.GetValueByName("Lat");
            double longi = (double)selectorList1.GetValueByName("Long");

            double height = (double)selectorList1.GetValueByName("Height");
            double coeffDrag = (double)selectorList1.GetValueByName("Drag");
            double crossarea = (double)selectorList1.GetValueByName("Area");

            //double gravity = (double)selectorList1.GetValueByName("Gravity");
            double timeIncrement = (double)selectorList1.GetValueByName("TimeIncrement");


            //var args = new List<double>() {
            //launch, angleAz, angleEl, speed, height, coeffDrag, crossarea, timeIncrement, width, heightearth
            //};


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
            Coordinate launch = new Coordinate(lat, longi);
            //UpdatePlotTimer.Start();

            var args = new LaunchParameters3D(launch, angleAz, angleEl, speed, height, coeffDrag, crossarea, timeIncrement);

            //var points = SimpleFormulae.EarthSpinProjectile(args.launch, args.angleAz, args.angleEl, args.speed, args.height, args.coeffDrag, args.crossarea, args.timeIncrement);
            //var toPlot = SimpleFormulae.LatLongToXY(points, earthImage.Width, earthImage.Height);
            //List<Coordinates> pointsForScatter = new List<Coordinates>();
            //ConcurrentQueue<Coordinates> pointsForScatterQueue = new ConcurrentQueue<Coordinates>();


            //var t = Task.Run(() =>
            //{
            //    lock (Lock)
            //    {
            //        foreach (var point in toPlot)
            //        {
            //            pointsForScatterQueue.Enqueue(point);
            //        }
            //    }
            //});
            ////t.Start();
            //UpdatePlotTimer.Tick += (s, e) =>
            //{
            //    if (pointsForScatterQueue.IsEmpty) return;
            //    ExtAddPointsLiveFromQueue(pointsForScatter, pointsForScatterQueue);
            //    formsPlot.Plot.Clear<Scatter>();
            //    var dragCurve = formsPlot.Plot.Add.ScatterPoints(pointsForScatter);
            //    dragCurve.LegendText = "Trajectory with Drag and Coriolis Force";
            //    formsPlot.Plot.ShowLegend();
            //    formsPlot.Refresh();
            //};


            //var dragCurve = formsPlot.Plot.Add.ScatterPoints(;

            //var spList = new ;//ScatterPlotList();

            //Logger = formsPlot.Plot.Add.DataLogger();

            //Logger.LegendText = "Trajectory with Drag and Coriolis Effect";
            //dragCurve.MarkerSize = 10;
            //Logger.LineWidth = 10;
            //extPlotterWorker.RunWorkerCompleted += (s, e) =>
            //{
            //   formsPlot.Refresh();
            //   formsPlot.Update();
            //};
            //Coordinate lanch = new Coordinate(lat,longi);

            if (extPlotterWorker.IsBusy)
            {
                extPlotterWorker.CancelAsync();
                extPlotterWorker = new BackgroundWorker();//old one will be garbage collected i think
                extPlotterWorker.WorkerSupportsCancellation = true;
                //outputTableControl1.SetColumnNames(new List<string>() { "Time", "Lat", "Long", "Altitude", "Distance on ground" });
                outputTableControl1.ClearTable();
                outputTableControl1.SetColumnNames(new List<string>() { "Time", "Lat", "Long", "Altitude", "Distance on ground" });
                extPlotterWorker.DoWork += extPlotterWorker_DoWork;
            }

            

            var loadLabel1 = formsPlot.Plot.Add.Annotation("Calculating...");
            var loadLabel2 = formsPlot2.Plot.Add.Annotation("Calculating...");
            //var formsPlot2 = tableLayourForPlots.Contr

            extPlotterWorker.RunWorkerCompleted += (s, e) =>
            {
                
                outputTableControl1.ClearTable();
                outputTableControl1.SetColumnNames(new List<string>() { "Time", "Lat", "Long", "Altitude", "Distance on ground" });
                //foreach (var item in e.Result as List<List<object>>)
                //{
                //    outputTableControl1.InsertRecord(item);
                //}
                formsPlot.Plot.Remove(loadLabel1);
                formsPlot2.Plot.Remove(loadLabel2);
                formsPlot.Refresh();
                formsPlot.Update();
                formsPlot2.Refresh();
                formsPlot2.Update();
                extPlotterWorker = new BackgroundWorker();//old one will be garbage collected i think
                extPlotterWorker.WorkerSupportsCancellation = true;
                //outputTableControl1.SetColumnNames(new List<string>() { "Time", "Lat", "Long", "Altitude", "Distance on ground" });
                extPlotterWorker.DoWork += extPlotterWorker_DoWork;
            };


            extPlotterWorker.RunWorkerAsync(new Tuple<LaunchParameters3D, ScottPlot.Plot, ScottPlot.Plot>(args, formsPlot.Plot, formsPlot2.Plot));



            //var noDrag = formsPlot.Plot.Add.Function(x => SimpleFormulae.AnalyticNoDragHeight(x, angle, gravity, speed, height));
            //noDrag.LegendText = "Trajectory without Drag";

            //noDrag.MinX = 0;
            //noDrag.MaxX = SimpleFormulae.AnalyticNoDragRange(angle, gravity, speed, height);




            formsPlot.Plot.ShowLegend();



        }
        public void extSetup()
        {
            List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.Name = "Angle";
            //s1.DescriptiveName = "Launch Angle (radians)";
            //s1.IsNumeric = true;
            //s1.BoundsAndStep = [0, Math.PI / 2, 0.001];

            //List<ParameterSpecification> specs = new List<ParameterSpecification>();




            var s1 = new ParameterSpecification();
            s1.Name = "AngleElevation";
            s1.DescriptiveName = "Launch Elevation Angle (degrees)";
            s1.IsNumeric = true;
            s1.BoundsAndStep = [0, 90, 0.00001];

            var ss = new ParameterSpecification();
            ss.Name = "AngleAzimuth";
            ss.DescriptiveName = "Launch Azimuth Angle (degrees)";
            ss.IsNumeric = true;
            ss.BoundsAndStep = [0, 360, 0.00001];

            var s2 = new ParameterSpecification();
            s2.Name = "Speed";
            s2.DescriptiveName = "Launch Speed (m/s)";
            s2.IsNumeric = true;
            s2.BoundsAndStep = [0, 100000, 0.001];

            var sa = new ParameterSpecification();
            sa.Name = "Lat";
            sa.DescriptiveName = "Launch Latitude (degrees)";
            sa.IsNumeric = true;
            sa.BoundsAndStep = [-90, 90, 0.00001];

            var sb = new ParameterSpecification();
            sb.Name = "Long";
            sb.DescriptiveName = "Launch Longitude (degrees)";
            sb.IsNumeric = true;
            sb.BoundsAndStep = [-180, 180, 0.00001];

            var s3 = new ParameterSpecification();
            s3.Name = "Height";
            s3.DescriptiveName = "Launch Height (m)";
            s3.IsNumeric = true;
            s3.BoundsAndStep = [0, 10000, 0.001];




            var s6 = new ParameterSpecification();
            s6.Name = "Drag";
            s6.DescriptiveName = "Coefficient of Drag";
            s6.IsNumeric = true;
            s6.BoundsAndStep = [0, 1, 0.001];

            var s7 = new ParameterSpecification();
            s7.Name = "Area";
            s7.DescriptiveName = "Cross Sectional Area m^2";
            s7.IsNumeric = true;
            s7.BoundsAndStep = [0, 100, 0.001];


            var s5 = new ParameterSpecification();
            s5.Name = "TimeIncrement";
            s5.DescriptiveName = "Seconds per time step";
            s5.IsNumeric = true;
            s5.BoundsAndStep = [0.0001, 10000, 0.0001];

            specs.Add(s1);
            specs.Add(ss);
            specs.Add(s2);
            specs.Add(sa);
            specs.Add(sb);
            specs.Add(s3);
            specs.Add(s6);
            specs.Add(s7);

            specs.Add(s5);
            SetParameterList(specs);
            CoordinateRect rect = new(left: -180, right: 180, bottom: -90, top: 90);

            formsPlot.Plot.Add.ImageRect(earthImage, rect);

            formsPlot.Refresh();
            formsPlot.Update();
            var formsPlot2 = new FormsPlot();
            int n = tableLayourForPlots.RowCount;
            tableLayourForPlots.RowCount = n + 1;
            tableLayourForPlots.Controls.Add(formsPlot2, 0, n);
            formsPlot2.Dock = DockStyle.Fill;
            //formsPlot2.Plot.Axes.AddBottomAxis();
            formsPlot2.Plot.Axes.AddRightAxis();

            tableLayourForPlots.RowStyles.Clear();

            outputTableControl1.SetColumnNames(new List<string>() {"Time","Lat","Long","Altitude","Distance on ground" });
            //var formsPlot3 = new FormsPlot();
            //n = tableLayourForPlots.RowCount;
            //tableLayourForPlots.RowCount = n + 1;
            //tableLayourForPlots.Controls.Add(formsPlot3, 0, n);
            //formsPlot3.Dock = DockStyle.Fill;
            //formsPlot3.Plot.Axes.AddBottomAxis();
            ////tableLayourForPlots.RowStyles.Clear();
            //tableLayourForPlots.RowStyles.Clear();

            //formsPlot.Plot.DataBackground.Image = earth;
            //formsPlot.Plot.Axes.SetLimitsY(0,earth.Height);
            //formsPlot.Plot.Axes.SetLimitsX(0,earth.Width);
            //formsPlot.Plot.Axes.ContinuouslyAutoscale = false;
            //formsPlot.Plot.Axes.
            richTextBox.Text = File.ReadAllText("Text/Ext.txt");
            RedrawGraphExt(formsPlot2);
            selectorList1.ValueChanged += (s, e) => RedrawGraphExt(formsPlot2);
        }


        private void CloseB_Click(object sender, EventArgs e)
        {
            EventHandler handler = CloseEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void outputTableControl1_Load(object sender, EventArgs e)
        {

        }

        private void formsPlot_Load(object sender, EventArgs e)
        {

        }

        private void extPlotterWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var (args, plotCoords, plotHeight) = e.Argument as Tuple<LaunchParameters3D, ScottPlot.Plot, ScottPlot.Plot>; //ugly nonsense
            BackgroundWorker worker = sender as BackgroundWorker;


            if (args != null && plotCoords != null && worker != null)
            {
                ////(launch, angleAz, angleEl, speed, height, coeffDrag, crossarea, timeIncrement, width, heightearth) =args;

                var coordsToPlot = SimpleFormulae.EarthSpinProjectile(args.launch, args.angleAz, args.angleEl, args.speed, args.height, args.coeffDrag, args.crossarea, args.timeIncrement);
                var XYCoordsToPlot = SimpleFormulae.LatLongToLLAandAltitudeAtTimeT(coordsToPlot);
                var XYCoordsList = new List<Coordinates>();
                var HeightDistCoordsList = new List<Coordinates>();
                var HeightTimeCoordsList = new List<Coordinates>();
                List<List<object>> tableStuff=new List<List<object>>();
                double t = 0;

                foreach (var plottable in XYCoordsToPlot)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    XYCoordsList.Add(plottable.Item1);
                    //HeightDistCoordsList.Add(plottable.Item2);
                    HeightTimeCoordsList.Add(new Coordinates(plottable.Item3.Y, plottable.Item3.X)); //transpose
                    //tableStuff.Add(new List<object>() {t,plottable.Item1.Y,plottable.Item1.X,plottable.Item2.Y,plottable.Item2.X});
                    //t += args.timeIncrement;
                    
                }
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                plotCoords.Clear<Scatter>();
                plotHeight.Clear<Scatter>();
                var s = plotCoords.Add.ScatterPoints(XYCoordsList);
                s.LegendText = "Trajectory with Drag and Coriolis Force";
                s.MarkerSize = 5;
                
                //UNCOMMENT HERE IF WANT DISTANCE TOO
                //var t = plotHeight.Add.Scatter(HeightDistCoordsList);
                //t.LegendText = "Trajectory Height at Surface Distance x Meters";
                //t.MarkerSize = 5;
                //var mySignalPlot2 = plt.AddSignal(myData2);
                var u = plotHeight.Add.Scatter(HeightTimeCoordsList);
                u.LegendText = "Trajectory Height at Time t";
                u.MarkerSize = 5;

                //outputTableControl1.InsertRecord();

                //UNCOMMENT HERE FOR SEPARATE AXIS
                //u.Axes.XAxis = (IXAxis)plotHeight.Axes.GetAxes(Edge.Bottom).ElementAt(1);

                //var v = plotHeight.Add.Function(alt=>SimpleFormulae.DryAirDensityAltitude(alt));
                //v.MinX = 0;
                //v.LegendText = "Air density at altitude(metres)";
                //v.Axes.YAxis = (IYAxis)(plotHeight.Axes.GetAxes(Edge.Right).First());
                if (worker.CancellationPending)
                {
                    e.Cancel= true;
                    return;
                }
                e.Result = tableStuff;
            }
        }




        private void Close_Click_1(object sender, EventArgs e)
        {
            EventHandler handler = CloseEvent;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
