using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SkiaSharp;
namespace BPhO_Challenge.Controls
{
    public partial class OutputTableControl : UserControl
    {
        public OutputTableControl()
        {
            InitializeComponent();
        }

        public void ClearTable()
        {
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = 0;
            tableLayoutPanel1.RowCount = 0;
        }

        public void SetColumnNames(List<string> columns)
        {
            tableLayoutPanel1.ColumnCount = columns.Count;
            tableLayoutPanel1.RowCount += 1;
            int i = 0;
            foreach (string column in columns)
            {
                var text = new TextBox();
                text.Text = column;
                text.ReadOnly = true;
                text.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(text, i, 0);
                text.Show();
                i++;
            }

        }

        public void InsertRecord(List<object> values)
        {
            tableLayoutPanel1.RowCount += 1;
            int i = 0;
            foreach (object value in values)
            {
                var text = new TextBox();
                text.Text = value.ToString();
                text.ReadOnly = true;
                text.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(text, i, tableLayoutPanel1.RowCount - 1);
                text.Show();
                i++;
            }
            tableLayoutPanel1.RowStyles.Clear();

        }
        public void Refresh()
        {
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try {
                //if ((myStream = saveFileDialog1.OpenFile()) != null) {
                  

                        // Code to write the stream goes here.
                        var csv = new StringBuilder();

                        for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                        {
                            List<string> values = new List<string>();
                            for (int c = 0; c < tableLayoutPanel1.ColumnCount; c++)
                            {
                                var contrl = (TextBox)tableLayoutPanel1.GetControlFromPosition(c, i);

                                values.Add(contrl.Text); //if excpetion thn jsut leave it

                            }
                            csv.AppendLine(String.Join(",", values));
                        }

                        File.WriteAllText(saveFileDialog1.FileName, csv.ToString());


                   // }
                }
                    catch
                    {
                        MessageBox.Show("Failed to save");
                    }
                
                
            }
        }
    }
}
