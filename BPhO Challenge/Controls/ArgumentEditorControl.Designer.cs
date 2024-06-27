namespace BPhO_Challenge.DataControls
{
    partial class ParameterEditorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            domainUpDown1 = new DomainUpDown();
            numericUpDown = new NumericUpDown();
            panel1 = new Panel();
            ArgumentL = new Label();
            splitContainer1 = new SplitContainer();
            ((System.ComponentModel.ISupportInitialize)numericUpDown).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Fill;
            textBox1.Location = new Point(0, 0);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(293, 23);
            textBox1.TabIndex = 1;
            textBox1.TextChanged += textBox1_TextChanged;
            textBox1.Validating += textBox1_Validating;
            // 
            // domainUpDown1
            // 
            domainUpDown1.Dock = DockStyle.Fill;
            domainUpDown1.Location = new Point(0, 0);
            domainUpDown1.Name = "domainUpDown1";
            domainUpDown1.Size = new Size(293, 23);
            domainUpDown1.TabIndex = 2;
            domainUpDown1.Text = "domainUpDown1";
            domainUpDown1.SelectedItemChanged += domainUpDown1_SelectedItemChanged;
            // 
            // numericUpDown
            // 
            numericUpDown.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            numericUpDown.DecimalPlaces = 3;
            numericUpDown.Location = new Point(0, 2);
            numericUpDown.Name = "numericUpDown";
            numericUpDown.Size = new Size(293, 23);
            numericUpDown.TabIndex = 3;
            numericUpDown.ValueChanged += numericUpDown_ValueChanged;
            // 
            // panel1
            // 
            panel1.AutoSize = true;
            panel1.Dock = DockStyle.Right;
            panel1.Location = new Point(442, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(0, 43);
            panel1.TabIndex = 4;
            // 
            // ArgumentL
            // 
            ArgumentL.AutoSize = true;
            ArgumentL.Location = new Point(3, 3);
            ArgumentL.Name = "ArgumentL";
            ArgumentL.Size = new Size(27, 15);
            ArgumentL.TabIndex = 0;
            ArgumentL.Text = "null";
            ArgumentL.TextAlign = ContentAlignment.TopCenter;
            ArgumentL.Click += ArgumentL_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.AutoScroll = true;
            splitContainer1.Panel1.Controls.Add(ArgumentL);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(numericUpDown);
            splitContainer1.Panel2.Controls.Add(textBox1);
            splitContainer1.Panel2.Controls.Add(domainUpDown1);
            splitContainer1.Size = new Size(442, 43);
            splitContainer1.SplitterDistance = 145;
            splitContainer1.TabIndex = 5;
            // 
            // ParameterEditorControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Name = "ParameterEditorControl";
            Size = new Size(442, 43);
            Load += ArgumentEditorControl_Load;
            ((System.ComponentModel.ISupportInitialize)numericUpDown).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox textBox1;
        private DomainUpDown domainUpDown1;
        private NumericUpDown numericUpDown;
        private Panel panel1;
        private Label ArgumentL;
        private SplitContainer splitContainer1;
    }
}
