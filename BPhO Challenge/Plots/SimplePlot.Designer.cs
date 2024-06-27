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
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot.DisplayScale = 1F;
            formsPlot.Location = new Point(560, 0);
            formsPlot.Name = "formsPlot1";
            formsPlot.Size = new Size(720, 402);
            formsPlot.TabIndex = 0;
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
            // SimplePlot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(CloseB);
            Controls.Add(selectorList1);
            Controls.Add(formsPlot);
            Name = "SimplePlot";
            Size = new Size(1325, 485);
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot;
        private Controls.SelectorList selectorList1;
        private Button CloseB;
    }
}
