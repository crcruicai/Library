namespace UILibrary
{
    partial class FrmRadioListBox
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
            this.radioListBox1 = new CRC.Controls.RadioListBox();
            this.artTextLabel1 = new CRC.Controls.ArtTextLabel();
            this.SuspendLayout();
            // 
            // radioListBox1
            // 
            this.radioListBox1.BackColor = System.Drawing.SystemColors.Window;
            this.radioListBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.radioListBox1.FormattingEnabled = true;
            this.radioListBox1.Items.AddRange(new object[] {
            "123",
            "abc",
            "好人与坏人"});
            this.radioListBox1.Location = new System.Drawing.Point(12, 12);
            this.radioListBox1.Name = "radioListBox1";
            this.radioListBox1.Size = new System.Drawing.Size(158, 160);
            this.radioListBox1.TabIndex = 0;
            // 
            // artTextLabel1
            // 
            this.artTextLabel1.AutoSize = true;
            this.artTextLabel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.artTextLabel1.BorderColor = System.Drawing.Color.Gray;
            this.artTextLabel1.BorderSize = 8;
            this.artTextLabel1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.artTextLabel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.artTextLabel1.Location = new System.Drawing.Point(24, 217);
            this.artTextLabel1.Name = "artTextLabel1";
            this.artTextLabel1.Size = new System.Drawing.Size(345, 48);
            this.artTextLabel1.TabIndex = 1;
            this.artTextLabel1.Text = "artTextLabel1";
            // 
            // FrmRadioListBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 302);
            this.Controls.Add(this.artTextLabel1);
            this.Controls.Add(this.radioListBox1);
            this.Name = "FrmRadioListBox";
            this.Text = "FrmRadioListBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CRC.Controls.RadioListBox radioListBox1;
        private CRC.Controls.ArtTextLabel artTextLabel1;
    }
}