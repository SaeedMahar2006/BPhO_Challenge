using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BPhO_Challenge.DataControls;
using Core;
namespace BPhO_Challenge.Controls
{
    public partial class SelectorList : UserControl
    {
        public event EventHandler FinishedCreating;
        public event EventHandler ValueChanged;
        public List<ParameterSpecification> parameterSpecifications = new List<ParameterSpecification>();
        public SelectorList()
        {
            InitializeComponent();
        }

        public void SetParameterSpecification(List<ParameterSpecification> spec)
        {
            parameterSpecifications = spec;
        }
        public void LoadParameterSpecification()
        {
            ParameterEditorTableLayout.Controls.Clear();
            ParameterEditorTableLayout.RowCount = 1;
            ParameterEditorTableLayout.ColumnCount = 2;
            ParameterEditorTableLayout.RowStyles.Clear();
            ParameterEditorTableLayout.ColumnStyles.Clear();
            int r = 1;
            foreach (var ps in parameterSpecifications)
            {
                var ctrl = new ParameterEditorControl();
                ctrl.Name = ps.Name;
                ParameterEditorTableLayout.Controls.Add(ctrl, 0, r);
                ctrl.SetParameterSpecification(ps);
                ctrl.ValueChanged += (s, e) => OnValueChanged(e); //cascade ValueChangedEventArgs
                ctrl.Show();
                r++;
            }

        }

        private void ArgumentEditorTableLayout_Paint(object sender, PaintEventArgs e)
        {

        }

        private void OnValueChanged(dynamic ValueChangedEventArgsFromControl)
        {
            EventHandler handler = ValueChanged;
            //object _val = GetValue();
            //var type = typeof(ValueChangedEventArgs<>).MakeGenericType(_val.GetType());
            //dynamic eventArg = Activator.CreateInstance(type, [_val]);
            //if (handler != null) handler(this, eventArg);
            if(handler!=null) handler(this, ValueChangedEventArgsFromControl);
        }

        public dynamic GetValueByName(string Name)
        {
            foreach (var ctrl in ParameterEditorTableLayout.Controls)
            {
                var c = ((ParameterEditorControl)ctrl);
                
                if (c.GetParameterSpecification().Name==Name )
                {
                    return c.GetValue();
                }
            }return null;
        }
    }

}
