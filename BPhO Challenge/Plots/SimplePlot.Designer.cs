namespace BPhO_Challenge.Plots
{
    partial class SimplePlot
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
            formsPlot = new ScottPlot.WinForms.FormsPlot();
            selectorList1 = new Controls.SelectorList();
            CloseB = new Button();
            outputTableControl1 = new Controls.OutputTableControl();
            SuspendLayout();
            // 
            // formsPlot
            // 
            formsPlot.DisplayScale = 1F;
            formsPlot.Location = new Point(560, 0);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new Size(720, 402);
            formsPlot.TabIndex = 0;
            formsPlot.Load += formsPlot_Load;
            // 
            // selectorList1
            // 
            selectorList1.Location = new Point(0, 3);
            selectorList1.Name = "selectorList1";
            selectorList1.Size = new Size(491, 416);
            selectorList1.TabIndex = 1;
            selectorList1.Load += selectorList1_Load;
            // 
            // CloseB
            // 
            CloseB.Location = new Point(1274, 14);
            CloseB.Name = "CloseB";
            CloseB.Size = new Size(31, 31);
            CloseB.TabIndex = 2;
            CloseB.Text = "🗙";
            CloseB.UseVisualStyleBackColor = true;
            CloseB.Click += CloseB_Click;
            // 
            // outputTableControl1
            // 
            outputTableControl1.AutoSize = true;
            outputTableControl1.Location = new Point(560, 433);
            outputTableControl1.Name = "outputTableControl1";
            outputTableControl1.Size = new Size(423, 402);
            outputTableControl1.TabIndex = 3;
            outputTableControl1.Load += outputTableControl1_Load;
            // 
            // SimplePlot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(outputTableControl1);
            Controls.Add(CloseB);
            Controls.Add(selectorList1);
            Controls.Add(formsPlot);
            Name = "SimplePlot";
            Size = new Size(1425, 838);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private Controls.SelectorList selectorList1;
        private Button CloseB;
        private Controls.OutputTableControl outputTableControl1;
    }
}
