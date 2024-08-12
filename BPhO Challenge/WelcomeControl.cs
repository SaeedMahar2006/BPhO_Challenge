using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BPhO_Challenge
{
    public partial class WelcomeControl : UserControl
    {
        public WelcomeControl()
        {
            InitializeComponent();
            richTextBox1.Text = File.ReadAllText("Text/Welcome.txt");
            pictureBox1.Image = Image.FromFile("Logo.ico");
            
        }

        private void WelcomeControl_Load(object sender, EventArgs e)
        {

        }
    }
}
