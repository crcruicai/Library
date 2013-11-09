/*
 * Created by SharpDevelop.
 * User: Tefik Becirovic
 * Date: 15.09.2008
 * Time: 19:42
 * 
 */
using CRC.Controls;
namespace UILibrary
{
	partial class FrmSlidingScale
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.trackBar1 = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.slidingScale1 = new SlidingScale();
			this.trackBar2 = new System.Windows.Forms.TrackBar();
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
			this.SuspendLayout();
			// 
			// trackBar1
			// 
			this.trackBar1.LargeChange = 10;
			this.trackBar1.Location = new System.Drawing.Point(22, 110);
			this.trackBar1.Maximum = 100;
			this.trackBar1.Minimum = -100;
			this.trackBar1.Name = "trackBar1";
			this.trackBar1.Size = new System.Drawing.Size(254, 42);
			this.trackBar1.TabIndex = 3;
			this.trackBar1.TickFrequency = 10;
			this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBar1.Scroll += new System.EventHandler(this.TrackBarsScroll);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.Gainsboro;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(114, 77);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(69, 21);
			this.label1.TabIndex = 2;
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// slidingScale1
			// 
			this.slidingScale1.BackColor = System.Drawing.Color.White;
			this.slidingScale1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.slidingScale1.ForeColor = System.Drawing.Color.Black;
			this.slidingScale1.Location = new System.Drawing.Point(22, 12);
			this.slidingScale1.Name = "slidingScale1";
			this.slidingScale1.ScaleRange = 50;
			this.slidingScale1.Size = new System.Drawing.Size(254, 45);
			this.slidingScale1.SmallTickCount = 9;
			this.slidingScale1.TabIndex = 1;
			this.slidingScale1.TabStop = false;
			// 
			// trackBar2
			// 
			this.trackBar2.LargeChange = 1;
			this.trackBar2.Location = new System.Drawing.Point(22, 158);
			this.trackBar2.Maximum = 9;
			this.trackBar2.Minimum = -9;
			this.trackBar2.Name = "trackBar2";
			this.trackBar2.Size = new System.Drawing.Size(254, 42);
			this.trackBar2.TabIndex = 4;
			this.trackBar2.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
			this.trackBar2.Scroll += new System.EventHandler(this.TrackBarsScroll);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(294, 208);
			this.Controls.Add(this.slidingScale1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.trackBar2);
			this.Controls.Add(this.trackBar1);
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "MainForm";
			this.Text = " SlidingScale.Test";
			((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TrackBar trackBar2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TrackBar trackBar1;
		private SlidingScale slidingScale1;
		
	}
}
