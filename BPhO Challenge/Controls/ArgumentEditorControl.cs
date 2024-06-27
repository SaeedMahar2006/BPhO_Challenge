
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Core;
using BPhO_Challenge.Controls;
namespace BPhO_Challenge.DataControls
{
    public partial class ParameterEditorControl : UserControl
    {
        public event EventHandler ValueChanged;
        bool first = true;
        Regex regex_filter=new Regex(@".+");
        string[] categories;
        //bool isParameterSpecification = true;
        ParameterSpecification _ParameterSpecification;
        //SettingsItem _settingsItemInfo;
        public ParameterEditorControl()
        {
            InitializeComponent();
        }
        public void SetParameterSpecification(ParameterSpecification ParameterSpecification)
        {
            //isParameterSpecification = true;
            first = true;
            if (ParameterSpecification.IsRegex)
            {
                regex_filter = new Regex(ParameterSpecification.Regex);
                textBox1.Show();
                textBox1.Text = "";
                domainUpDown1.Hide();
                numericUpDown.Hide();
            }
            else if (ParameterSpecification.IsCategorical)
            {
                categories = ParameterSpecification.Categories;
                textBox1.Hide();
                numericUpDown.Hide();
                domainUpDown1.Show();
                domainUpDown1.Items.Clear();
                foreach (string category in categories) domainUpDown1.Items.Add(category);
                domainUpDown1.SelectedItem = domainUpDown1.Items[0];
                first = false;
            }
            else if (ParameterSpecification.IsNumeric)
            {
                textBox1.Hide();
                domainUpDown1.Hide();
                numericUpDown.Show();
                numericUpDown.Minimum = (decimal) ParameterSpecification.BoundsAndStep[0];
                numericUpDown.Maximum = (decimal) ParameterSpecification.BoundsAndStep[1];
                numericUpDown.Increment = (decimal) ParameterSpecification.BoundsAndStep[2];
                numericUpDown.DecimalPlaces = BitConverter.GetBytes(decimal.GetBits((decimal) (ParameterSpecification.BoundsAndStep[2])  )[3])[2]; //stack overflow https://stackoverflow.com/questions/13477689/find-number-of-decimal-places-in-decimal-value-regardless-of-culture/13477964#13477964
                numericUpDown.Value = (decimal)ParameterSpecification.BoundsAndStep[0];
                first = false;
            }
            else
            {
                regex_filter = new Regex(@".+"); //anything except newline, must be something
                textBox1.Show();
                textBox1.Text = "";
                domainUpDown1.Hide();
                numericUpDown.Hide();
            }
            _ParameterSpecification = ParameterSpecification;
            setArgumentLabelText(_ParameterSpecification.DescriptiveName);
        }
        //public void SetInfo(SettingsItem settingsItem)
        //{
        //    isParameterSpecification=false;
        //    first = true;

        //}
        public string getText()
        {
            if (_ParameterSpecification.IsRegex)
            {
                return textBox1.Text;
            }
            else if (_ParameterSpecification.IsCategorical)
            {
                return domainUpDown1.SelectedItem.ToString();
            }
            else
            {
                return textBox1.Text;
            }
        }
        public ParameterSpecification GetParameterSpecification()
        {
            return _ParameterSpecification;
        }
        public object GetValue()
        {
            if (_ParameterSpecification.IsRegex)
            {
                return textBox1.Text;
            }
            else if (_ParameterSpecification.IsCategorical)
            {
                return domainUpDown1.SelectedItem;
            }
            else if (_ParameterSpecification.IsNumeric)
            {
                return numericUpDown.Value;
            }
            else
            {
                return textBox1.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //private bool Verify()
        //{
        //    return true;
        //}

        //with reference to https://stackoverflow.com/questions/8915151/c-sharp-validating-input-for-textbox-on-winforms
        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!regex_filter.IsMatch(textBox1.Text)) e.Cancel = true;
            //ValueChanged(this, new EventArgs());
            OnValueChanged(textBox1.Text);
        }

        private void ArgumentL_Click(object sender, EventArgs e)
        {

        }
        public void setArgumentLabelText(string t)
        {
            ArgumentL.Text = t;
        }

        private void ArgumentEditorControl_Load(object sender, EventArgs e)
        {

        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            try { if (!first) OnValueChanged(domainUpDown1.SelectedItem); } catch { }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (!first) OnValueChanged(numericUpDown.Value);
            }catch { }
        }

        private void OnValueChanged(object val)
        {
            EventHandler handler = ValueChanged;
            object _val = GetValue();
            var type = typeof(ValueChangedEventArgs<>).MakeGenericType(_val.GetType());
            dynamic eventArg = Activator.CreateInstance(type, [_val]);
            if (handler != null) handler(this, eventArg);
        }
    }

}
