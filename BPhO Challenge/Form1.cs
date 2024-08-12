using BPhO_Challenge.Plots;
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
            tabControl1.Controls.Clear();
            tabControl1.TabPages.Clear();
            var welcome = new WelcomeControl();
            AddControlToNewTab(welcome, "Get Started");
            this.Text = "BPhO 2024 Submission";
            this.Icon = new Icon("Logo.ico");//logo by a good friend of mine using canva
        }

        private void AddControlToNewTab(Control c,string name="Plot" ,bool show = true)
        {
            TabPage newPage = new TabPage(c.Name);
            newPage.Text = name;
            tabControl1.TabPages.Insert(0, newPage);
            newPage.Controls.Add(c);
            c.Dock = DockStyle.Fill;
            c.Show();
            //ControlPageMap.Add(c, newPage);
            if (c is SimplePlot)
            {
                ((SimplePlot)c).CloseEvent += (s, e) =>
                {
                    tabControl1.TabPages.Remove(newPage);
                };
            }
            if (show)
            {
                tabControl1.SelectedTab = newPage;
            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.TaskOneSetup();
            AddControlToNewTab(c1,"Task 1" ,true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskTwoSetup();
            AddControlToNewTab(c1,"Task 2", true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskThreeSetup();
            AddControlToNewTab(c1, "Task 3",true);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskFourSetup();
            AddControlToNewTab(c1, "Task 4",true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskFiveSetup();
            AddControlToNewTab(c1, "Task 5", true);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskSixSetup();
            AddControlToNewTab(c1, "Task6", true);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskSevenSetup();
            AddControlToNewTab(c1, "Task 7", true);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskEightSetup();
            AddControlToNewTab(c1, "Task 8", true);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.taskNineSetup();
            AddControlToNewTab(c1, "Task 9", true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var c1 = new SimplePlot();
            c1.extSetup();
            AddControlToNewTab(c1, "Extension", true);
        }
    }
}
