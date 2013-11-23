namespace UILibrary
{
    partial class FrmPlayerTrackBar
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
            this.playerTrackBar1 = new CRC.Controls.PlayerTrackBar();
            this.SuspendLayout();
            // 
            // playerTrackBar1
            // 
            this.playerTrackBar1.BackColor = System.Drawing.SystemColors.Control;
            this.playerTrackBar1.EmptyColor = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.playerTrackBar1.FillColor = System.Drawing.Color.White;
            this.playerTrackBar1.Location = new System.Drawing.Point(48, 101);
            this.playerTrackBar1.MaxValue = 100;
            this.playerTrackBar1.MinValue = 0;
            this.playerTrackBar1.Name = "playerTrackBar1";
            this.playerTrackBar1.Shape = CRC.Controls.PlayerTrackBar.TrackShape.Circle;
            this.playerTrackBar1.Size = new System.Drawing.Size(167, 21);
            this.playerTrackBar1.TabIndex = 0;
            this.playerTrackBar1.Text = "playerTrackBar1";
            this.playerTrackBar1.Value = 0;
            // 
            // FrmPlayerTrackBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.playerTrackBar1);
            this.Name = "FrmPlayerTrackBar";
            this.Text = "FrmPlayerTrackBar";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.PlayerTrackBar playerTrackBar1;
    }
}