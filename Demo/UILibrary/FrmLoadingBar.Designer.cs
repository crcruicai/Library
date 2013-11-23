namespace UILibrary
{
    partial class FrmLoadingBar
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
            this.loadingBar1 = new CRC.Controls.LoadingBar();
            this.SuspendLayout();
            // 
            // loadingBar1
            // 
            this.loadingBar1.Location = new System.Drawing.Point(53, 93);
            this.loadingBar1.Name = "loadingBar1";
            this.loadingBar1.Size = new System.Drawing.Size(192, 23);
            this.loadingBar1.TabIndex = 0;
            this.loadingBar1.Text = "loadingBar1";
            // 
            // FrmLoadingBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.loadingBar1);
            this.Name = "FrmLoadingBar";
            this.Text = "FrmLoadingBar";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.LoadingBar loadingBar1;
    }
}