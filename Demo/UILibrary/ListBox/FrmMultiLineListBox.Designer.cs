namespace UILibrary
{
    partial class FrmMultiLineListBox
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.multiLineListBox1 = new CRC.Controls.MultiLineListBox();
            this.multiTextListBox1 = new CRC.Controls.MultiTextListBox();
            this.SuspendLayout();
            // 
            // multiLineListBox1
            // 
            this.multiLineListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiLineListBox1.FormattingEnabled = true;
            this.multiLineListBox1.Location = new System.Drawing.Point(27, 12);
            this.multiLineListBox1.Name = "multiLineListBox1";
            this.multiLineListBox1.ScrollAlwaysVisible = true;
            this.multiLineListBox1.Size = new System.Drawing.Size(223, 317);
            this.multiLineListBox1.TabIndex = 0;
            // 
            // multiTextListBox1
            // 
            this.multiTextListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.multiTextListBox1.FormattingEnabled = true;
            this.multiTextListBox1.Location = new System.Drawing.Point(316, 19);
            this.multiTextListBox1.Name = "multiTextListBox1";
            this.multiTextListBox1.Size = new System.Drawing.Size(171, 291);
            this.multiTextListBox1.TabIndex = 1;
            // 
            // FrmMultiLineListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 368);
            this.Controls.Add(this.multiTextListBox1);
            this.Controls.Add(this.multiLineListBox1);
            this.Name = "FrmMultiLineListBox";
            this.Text = "FrmMultiLineListBox";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.MultiLineListBox multiLineListBox1;
        private CRC.Controls.MultiTextListBox multiTextListBox1;
    }
}