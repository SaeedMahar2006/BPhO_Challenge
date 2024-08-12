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
            CloseB = new Button();
            outputTableControl1 = new Controls.OutputTableControl();
            extPlotterWorker = new System.ComponentModel.BackgroundWorker();
            splitContainer1 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            selectorList1 = new Controls.SelectorList();
            richTextBox = new RichTextBox();
            Close = new Button();
            splitContainer2 = new SplitContainer();
            tableLayourForPlots = new TableLayoutPanel();
            formsPlot = new ScottPlot.WinForms.FormsPlot();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tableLayourForPlots.SuspendLayout();
            SuspendLayout();
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
            outputTableControl1.AutoScroll = true;
            outputTableControl1.AutoSize = true;
            outputTableControl1.Dock = DockStyle.Fill;
            outputTableControl1.Location = new Point(0, 0);
            outputTableControl1.Name = "outputTableControl1";
            outputTableControl1.Size = new Size(939, 382);
            outputTableControl1.TabIndex = 3;
            outputTableControl1.Load += outputTableControl1_Load;
            // 
            // extPlotterWorker
            // 
            extPlotterWorker.WorkerSupportsCancellation = true;
            extPlotterWorker.DoWork += extPlotterWorker_DoWork;
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
            splitContainer1.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.AutoScroll = true;
            splitContainer1.Panel2.Controls.Add(Close);
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(1411, 771);
            splitContainer1.SplitterDistance = 468;
            splitContainer1.TabIndex = 4;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(selectorList1);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(richTextBox);
            splitContainer3.Size = new Size(468, 771);
            splitContainer3.SplitterDistance = 343;
            splitContainer3.TabIndex = 2;
            // 
            // selectorList1
            // 
            selectorList1.AutoScroll = true;
            selectorList1.Dock = DockStyle.Fill;
            selectorList1.Location = new Point(0, 0);
            selectorList1.Name = "selectorList1";
            selectorList1.Size = new Size(468, 343);
            selectorList1.TabIndex = 0;
            // 
            // richTextBox
            // 
            richTextBox.Dock = DockStyle.Fill;
            richTextBox.Font = new Font("Cambria Math", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBox.Location = new Point(0, 0);
            richTextBox.Name = "richTextBox";
            richTextBox.ReadOnly = true;
            richTextBox.Size = new Size(468, 424);
            richTextBox.TabIndex = 0;
            richTextBox.Text = "";
            // 
            // Close
            // 
            Close.Dock = DockStyle.Right;
            Close.Location = new Point(916, 0);
            Close.MaximumSize = new Size(23, 23);
            Close.MinimumSize = new Size(23, 23);
            Close.Name = "Close";
            Close.Size = new Size(23, 23);
            Close.TabIndex = 5;
            Close.Text = "✕";
            Close.UseVisualStyleBackColor = true;
            Close.Click += Close_Click_1;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.AutoScroll = true;
            splitContainer2.Panel1.Controls.Add(tableLayourForPlots);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.AutoScroll = true;
            splitContainer2.Panel2.Controls.Add(outputTableControl1);
            splitContainer2.Size = new Size(939, 771);
            splitContainer2.SplitterDistance = 385;
            splitContainer2.TabIndex = 4;
            // 
            // tableLayourForPlots
            // 
            tableLayourForPlots.AutoScroll = true;
            tableLayourForPlots.AutoSize = true;
            tableLayourForPlots.ColumnCount = 1;
            tableLayourForPlots.ColumnStyles.Add(new ColumnStyle());
            tableLayourForPlots.Controls.Add(formsPlot, 0, 0);
            tableLayourForPlots.Dock = DockStyle.Fill;
            tableLayourForPlots.Location = new Point(0, 0);
            tableLayourForPlots.MaximumSize = new Size(2000, 2000);
            tableLayourForPlots.Name = "tableLayourForPlots";
            tableLayourForPlots.RowCount = 1;
            tableLayourForPlots.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayourForPlots.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayourForPlots.Size = new Size(939, 385);
            tableLayourForPlots.TabIndex = 2;
            // 
            // formsPlot
            // 
            formsPlot.DisplayScale = 1F;
            formsPlot.Dock = DockStyle.Fill;
            formsPlot.Location = new Point(3, 3);
            formsPlot.Name = "formsPlot";
            formsPlot.Size = new Size(933, 379);
            formsPlot.TabIndex = 0;
            // 
            // SimplePlot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(CloseB);
            Name = "SimplePlot";
            Size = new Size(1411, 771);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tableLayourForPlots.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button CloseB;
        private Controls.OutputTableControl outputTableControl1;
        private System.ComponentModel.BackgroundWorker extPlotterWorker;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer3;
        private Controls.SelectorList selectorList1;
        private RichTextBox richTextBox;
        private TableLayoutPanel tableLayourForPlots;
        private ScottPlot.WinForms.FormsPlot formsPlot;
        private Button Close;
    }
}
