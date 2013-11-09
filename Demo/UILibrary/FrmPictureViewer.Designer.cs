namespace UILibrary
{
    partial class FrmPictureViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPictureViewer));
            this.pictureViewer1 = new CRC.Controls.PictureViewer();
            this.SuspendLayout();
            // 
            // pictureViewer1
            // 
            this.pictureViewer1.AutoScroll = true;
            this.pictureViewer1.AutoScrollMinSize = new System.Drawing.Size(600, 525);
            this.pictureViewer1.FitMode = CRC.Controls.FitMode.FitPercent;
            this.pictureViewer1.Image = ((System.Drawing.Image)(resources.GetObject("pictureViewer1.Image")));
            this.pictureViewer1.ImageFile = null;
            this.pictureViewer1.Location = new System.Drawing.Point(144, 108);
            this.pictureViewer1.MaxZoomPercent = 1000;
            this.pictureViewer1.Name = "pictureViewer1";
            this.pictureViewer1.Size = new System.Drawing.Size(306, 248);
            this.pictureViewer1.TabIndex = 0;
            this.pictureViewer1.Text = "pictureViewer1";
            this.pictureViewer1.ZoomPercent = 100;
            this.pictureViewer1.ZoomStep = 10;
            // 
            // FrmPictureViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 464);
            this.Controls.Add(this.pictureViewer1);
            this.Name = "FrmPictureViewer";
            this.Text = "FrmPictureViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.PictureViewer pictureViewer1;
    }
}