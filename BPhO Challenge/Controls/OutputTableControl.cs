﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            foreach (string column in columns) { 
                var text = new TextBox();
                text.Text = column;
                text.ReadOnly = true;
                text.Dock = DockStyle.Fill;
                tableLayoutPanel1.Controls.Add(text,i,0);
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
                tableLayoutPanel1 .Controls.Add(text,i,tableLayoutPanel1.RowCount-1);
                text.Show();
                i++;
            }

        }
        public void Refresh()
        {
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.Refresh();
        }
    }
}
