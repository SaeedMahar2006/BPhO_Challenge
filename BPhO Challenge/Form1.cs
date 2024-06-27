using Core;
namespace BPhO_Challenge
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //List<ParameterSpecification> specs = new List<ParameterSpecification>();
            //var s1 = new ParameterSpecification();
            //s1.IsNumeric = true;
            //s1.Name = "Name";
            //s1.DescriptiveName = "Description";
            //s1.Type = typeof(Double);
            //s1.BoundsAndStep = new double[] { 0, 100, 1 };

            //var s2 = new ParameterSpecification();
            //s2.IsCategorical = true;
            //s2.Name = "Name 2";
            //s2.DescriptiveName = "Description";
            //s2.Type = typeof(String);
            //s2.Categories = ["a", "b", "c"];

            //specs.Add(s1);
            //specs.Add(s2);

            //selectorList1.SetParameterSpecification(specs);
            //selectorList1.LoadParameterSpecification();
            //selectorList1.Update();
            //selectorList1.Show();

            //selectorList1.ValueChanged += (s, e) => { MessageBox.Show(e.ToString()); };
            simplePlot1.taskFiveSetup();
        }

        private void selectorList1_Load(object sender, EventArgs e)
        {

        }

        private void simplePlot1_Load(object sender, EventArgs e)
        {

        }
    }
}
