namespace UILibrary
{
    partial class FrmControlPaintExTest
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
            this.expandPanel1 = new CRC.Controls.ExpandPanel();
            this.colorButton1 = new CRC.Controls.ColorButton();
            this.SuspendLayout();
            // 
            // expandPanel1
            // 
            this.expandPanel1.Alignment = System.Drawing.StringAlignment.Near;
            this.expandPanel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.expandPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.expandPanel1.Collapsed = false;
            this.expandPanel1.Font = new System.Drawing.Font("宋体", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.expandPanel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.expandPanel1.Location = new System.Drawing.Point(573, 83);
            this.expandPanel1.Name = "expandPanel1";
            this.expandPanel1.Size = new System.Drawing.Size(240, 175);
            this.expandPanel1.TabIndex = 1;
            this.expandPanel1.TitleBackColor1 = System.Drawing.SystemColors.ButtonFace;
            this.expandPanel1.TitleBackColor2 = System.Drawing.SystemColors.ActiveCaption;
            this.expandPanel1.TitleHeight = 25;
            this.expandPanel1.TitleText = "属性:";
            // 
            // colorButton1
            // 
            this.colorButton1.Location = new System.Drawing.Point(713, 12);
            this.colorButton1.Name = "colorButton1";
            this.colorButton1.Size = new System.Drawing.Size(100, 47);
            this.colorButton1.TabIndex = 0;
            this.colorButton1.Text = "colorButton1";
            this.colorButton1.UseVisualStyleBackColor = true;
            // 
            // FrmControlPaintExTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 462);
            this.Controls.Add(this.expandPanel1);
            this.Controls.Add(this.colorButton1);
            this.Name = "FrmControlPaintExTest";
            this.Text = "FrmControlPaintExTest";
            this.ResumeLayout(false);

        }

        #endregion

        private CRC.Controls.ColorButton colorButton1;
        private CRC.Controls.ExpandPanel expandPanel1;

    }
}