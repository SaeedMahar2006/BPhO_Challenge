namespace BPhO_Challenge.Controls
{
    partial class SelectorList
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
            ParameterEditorTableLayout = new TableLayoutPanel();
            SuspendLayout();
            // 
            // ParameterEditorTableLayout
            // 
            ParameterEditorTableLayout.AutoScroll = true;
            ParameterEditorTableLayout.AutoSize = true;
            ParameterEditorTableLayout.ColumnCount = 2;
            ParameterEditorTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ParameterEditorTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            ParameterEditorTableLayout.Dock = DockStyle.Fill;
            ParameterEditorTableLayout.Location = new Point(0, 0);
            ParameterEditorTableLayout.Name = "ParameterEditorTableLayout";
            ParameterEditorTableLayout.RowCount = 2;
            ParameterEditorTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ParameterEditorTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            ParameterEditorTableLayout.Size = new Size(491, 416);
            ParameterEditorTableLayout.TabIndex = 0;
            ParameterEditorTableLayout.Paint += ArgumentEditorTableLayout_Paint;
            // 
            // SelectorList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ParameterEditorTableLayout);
            Name = "SelectorList";
            Size = new Size(491, 416);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel ParameterEditorTableLayout;
    }
}
