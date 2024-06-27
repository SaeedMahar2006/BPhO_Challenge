namespace BPhO_Challenge
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            selectorList1 = new Controls.SelectorList();
            simplePlot1 = new Plots.SimplePlot();
            SuspendLayout();
            // 
            // selectorList1
            // 
            selectorList1.Location = new Point(12, 12);
            selectorList1.Name = "selectorList1";
            selectorList1.Size = new Size(486, 414);
            selectorList1.TabIndex = 0;
            selectorList1.Load += selectorList1_Load;
            // 
            // simplePlot1
            // 
            simplePlot1.Location = new Point(34, 35);
            simplePlot1.Name = "simplePlot1";
            simplePlot1.Size = new Size(1325, 485);
            simplePlot1.TabIndex = 1;
            simplePlot1.Load += simplePlot1_Load;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1490, 683);
            Controls.Add(simplePlot1);
            Controls.Add(selectorList1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Controls.SelectorList selectorList1;
        private Plots.SimplePlot simplePlot1;
    }
}
