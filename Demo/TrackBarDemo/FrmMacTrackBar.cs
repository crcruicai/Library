using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using CRC.Controls;

namespace TestApp
{
	public class FrmMacTrackBar : Form
	{
		private Label macLabel8;
		private Button btnClose;
		private Button btnStop;
		private Button btnStart;
		private Label macLabel7;
		private NumericUpDown numMinimum;
		private NumericUpDown numMaximum;
		private Label macLabel6;
		private Label macLabel5;
		private System.Windows.Forms.Timer timer1;
		private Panel macPanel2;
		private ComboBox cboBorderStyle;
		private Label macLabel4;
		private Label macLabel2;
		private Label macLabel1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private Panel macPanel1;
		private MacTrackBar macTrackBar1;
		private MacTrackBar macTrackBar2;
		private Label macLabel9;
		private NumericUpDown numTrackLineHeight;
		private NumericUpDown numTickFrequency;
		private Label macLabel10;
		private NumericUpDown numTickHeight;
		private Label macLabel11;
		private Label macLabel12;
		private Label macLabel13;
		private NumericUpDown numTrackerWidth;
		private NumericUpDown numTrackerHeight;
		private Label macLabel14;
		private Label macLabel15;
		private NumericUpDown numLargeChange;
		private Label macLabel16;
		private NumericUpDown numSmallChange;
		private Label macLabel17;
		private NumericUpDown numValue;
		private Button btnForeColor;
		private Button btnTrackerColor;
		private Button btnTrackLineColor;
		private Button btnTickColor;
		private ComboBox cboTextTickStyle;
		private ComboBox cboTickStyle;
		private Label macLabel3;
		private System.Windows.Forms.Label label1;
		private System.ComponentModel.IContainer components = null;

		public FrmMacTrackBar()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.macLabel8 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.numTrackLineHeight = new System.Windows.Forms.NumericUpDown();
            this.macLabel7 = new System.Windows.Forms.Label();
            this.numMinimum = new System.Windows.Forms.NumericUpDown();
            this.numMaximum = new System.Windows.Forms.NumericUpDown();
            this.macLabel6 = new System.Windows.Forms.Label();
            this.numTickFrequency = new System.Windows.Forms.NumericUpDown();
            this.macLabel5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.macPanel2 = new System.Windows.Forms.Panel();
            this.macTrackBar2 = new CRC.Controls.MacTrackBar();
            this.cboBorderStyle = new System.Windows.Forms.ComboBox();
            this.macLabel4 = new System.Windows.Forms.Label();
            this.macLabel2 = new System.Windows.Forms.Label();
            this.macLabel1 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.macPanel1 = new System.Windows.Forms.Panel();
            this.macTrackBar1 = new CRC.Controls.MacTrackBar();
            this.macLabel9 = new System.Windows.Forms.Label();
            this.macLabel10 = new System.Windows.Forms.Label();
            this.numTickHeight = new System.Windows.Forms.NumericUpDown();
            this.macLabel11 = new System.Windows.Forms.Label();
            this.macLabel12 = new System.Windows.Forms.Label();
            this.macLabel13 = new System.Windows.Forms.Label();
            this.numTrackerWidth = new System.Windows.Forms.NumericUpDown();
            this.numTrackerHeight = new System.Windows.Forms.NumericUpDown();
            this.macLabel14 = new System.Windows.Forms.Label();
            this.macLabel15 = new System.Windows.Forms.Label();
            this.numLargeChange = new System.Windows.Forms.NumericUpDown();
            this.macLabel16 = new System.Windows.Forms.Label();
            this.numSmallChange = new System.Windows.Forms.NumericUpDown();
            this.macLabel17 = new System.Windows.Forms.Label();
            this.numValue = new System.Windows.Forms.NumericUpDown();
            this.btnForeColor = new System.Windows.Forms.Button();
            this.btnTrackerColor = new System.Windows.Forms.Button();
            this.btnTrackLineColor = new System.Windows.Forms.Button();
            this.btnTickColor = new System.Windows.Forms.Button();
            this.cboTextTickStyle = new System.Windows.Forms.ComboBox();
            this.cboTickStyle = new System.Windows.Forms.ComboBox();
            this.macLabel3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numTrackLineHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaximum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTickFrequency)).BeginInit();
            this.macPanel2.SuspendLayout();
            this.macPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numTickHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrackerWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrackerHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLargeChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSmallChange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
            this.SuspendLayout();
            // 
            // macLabel8
            // 
            this.macLabel8.BackColor = System.Drawing.Color.Transparent;
            this.macLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel8.Location = new System.Drawing.Point(29, 211);
            this.macLabel8.Name = "macLabel8";
            this.macLabel8.Size = new System.Drawing.Size(115, 17);
            this.macLabel8.TabIndex = 45;
            this.macLabel8.Text = "Tracker Color";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(460, 500);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(115, 26);
            this.btnClose.TabIndex = 21;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnStop.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.ForeColor = System.Drawing.Color.White;
            this.btnStop.Location = new System.Drawing.Point(325, 500);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(115, 26);
            this.btnStop.TabIndex = 20;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnStart.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(191, 500);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(115, 26);
            this.btnStart.TabIndex = 19;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // numTrackLineHeight
            // 
            this.numTrackLineHeight.BackColor = System.Drawing.SystemColors.Window;
            this.numTrackLineHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numTrackLineHeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numTrackLineHeight.Location = new System.Drawing.Point(163, 396);
            this.numTrackLineHeight.Name = "numTrackLineHeight";
            this.numTrackLineHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTrackLineHeight.Size = new System.Drawing.Size(135, 21);
            this.numTrackLineHeight.TabIndex = 8;
            this.numTrackLineHeight.ValueChanged += new System.EventHandler(this.numTrackLineHeight_ValueChanged);
            // 
            // macLabel7
            // 
            this.macLabel7.BackColor = System.Drawing.Color.Transparent;
            this.macLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel7.Location = new System.Drawing.Point(29, 396);
            this.macLabel7.Name = "macLabel7";
            this.macLabel7.Size = new System.Drawing.Size(115, 26);
            this.macLabel7.TabIndex = 41;
            this.macLabel7.Text = "Line Height";
            // 
            // numMinimum
            // 
            this.numMinimum.BackColor = System.Drawing.SystemColors.Window;
            this.numMinimum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numMinimum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numMinimum.Location = new System.Drawing.Point(480, 242);
            this.numMinimum.Name = "numMinimum";
            this.numMinimum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numMinimum.Size = new System.Drawing.Size(134, 21);
            this.numMinimum.TabIndex = 16;
            this.numMinimum.ValueChanged += new System.EventHandler(this.numMinimum_ValueChanged);
            // 
            // numMaximum
            // 
            this.numMaximum.BackColor = System.Drawing.SystemColors.Window;
            this.numMaximum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numMaximum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numMaximum.Location = new System.Drawing.Point(480, 208);
            this.numMaximum.Name = "numMaximum";
            this.numMaximum.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numMaximum.Size = new System.Drawing.Size(134, 21);
            this.numMaximum.TabIndex = 15;
            this.numMaximum.ValueChanged += new System.EventHandler(this.numMaximum_ValueChanged);
            // 
            // macLabel6
            // 
            this.macLabel6.BackColor = System.Drawing.Color.Transparent;
            this.macLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel6.Location = new System.Drawing.Point(346, 241);
            this.macLabel6.Name = "macLabel6";
            this.macLabel6.Size = new System.Drawing.Size(105, 26);
            this.macLabel6.TabIndex = 40;
            this.macLabel6.Text = "Minimum";
            // 
            // numTickFrequency
            // 
            this.numTickFrequency.BackColor = System.Drawing.SystemColors.Window;
            this.numTickFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numTickFrequency.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numTickFrequency.Location = new System.Drawing.Point(480, 362);
            this.numTickFrequency.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numTickFrequency.Name = "numTickFrequency";
            this.numTickFrequency.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTickFrequency.Size = new System.Drawing.Size(154, 21);
            this.numTickFrequency.TabIndex = 10;
            this.numTickFrequency.ValueChanged += new System.EventHandler(this.numTickFrequency_ValueChanged);
            // 
            // macLabel5
            // 
            this.macLabel5.BackColor = System.Drawing.Color.Transparent;
            this.macLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel5.Location = new System.Drawing.Point(346, 207);
            this.macLabel5.Name = "macLabel5";
            this.macLabel5.Size = new System.Drawing.Size(105, 26);
            this.macLabel5.TabIndex = 37;
            this.macLabel5.Text = "Maximum";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // macPanel2
            // 
            this.macPanel2.BackColor = System.Drawing.Color.Transparent;
            this.macPanel2.Controls.Add(this.macTrackBar2);
            this.macPanel2.Location = new System.Drawing.Point(643, 111);
            this.macPanel2.Name = "macPanel2";
            this.macPanel2.Size = new System.Drawing.Size(115, 345);
            this.macPanel2.TabIndex = 22;
            // 
            // macTrackBar2
            // 
            this.macTrackBar2.BackColor = System.Drawing.Color.Transparent;
            this.macTrackBar2.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.macTrackBar2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macTrackBar2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.macTrackBar2.IndentHeight = 6;
            this.macTrackBar2.Location = new System.Drawing.Point(12, 34);
            this.macTrackBar2.Maximum = 10;
            this.macTrackBar2.Minimum = 0;
            this.macTrackBar2.Name = "macTrackBar2";
            this.macTrackBar2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.macTrackBar2.Size = new System.Drawing.Size(92, 276);
            this.macTrackBar2.TabIndex = 0;
            this.macTrackBar2.TextTickStyle = System.Windows.Forms.TickStyle.Both;
            this.macTrackBar2.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.macTrackBar2.TickHeight = 4;
            this.macTrackBar2.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.macTrackBar2.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.macTrackBar2.TrackerSize = new System.Drawing.Size(16, 16);
            this.macTrackBar2.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.macTrackBar2.TrackLineHeight = 3;
            this.macTrackBar2.Value = 0;
            this.macTrackBar2.Scroll += new System.EventHandler(this.macTrackBar2_Scroll);
            // 
            // cboBorderStyle
            // 
            this.cboBorderStyle.BackColor = System.Drawing.SystemColors.Window;
            this.cboBorderStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBorderStyle.DropDownWidth = 128;
            this.cboBorderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboBorderStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.cboBorderStyle.ItemHeight = 13;
            this.cboBorderStyle.Items.AddRange(new object[] {
            "None",
            "Dashed",
            "Dotted",
            "Inset",
            "Outset",
            "Solid",
            "Adjust",
            "Bump",
            "Etched",
            "Flat",
            "Raised",
            "RaisedInner",
            "RaisedOuter",
            "Sunken",
            "SunkenInner",
            "SunkenOuter"});
            this.cboBorderStyle.Location = new System.Drawing.Point(163, 138);
            this.cboBorderStyle.Name = "cboBorderStyle";
            this.cboBorderStyle.Size = new System.Drawing.Size(154, 21);
            this.cboBorderStyle.TabIndex = 1;
            this.cboBorderStyle.SelectedIndexChanged += new System.EventHandler(this.cboBorderStyle_SelectedIndexChanged);
            // 
            // macLabel4
            // 
            this.macLabel4.BackColor = System.Drawing.Color.Transparent;
            this.macLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel4.Location = new System.Drawing.Point(346, 362);
            this.macLabel4.Name = "macLabel4";
            this.macLabel4.Size = new System.Drawing.Size(144, 17);
            this.macLabel4.TabIndex = 32;
            this.macLabel4.Text = "Tick Frequency";
            // 
            // macLabel2
            // 
            this.macLabel2.BackColor = System.Drawing.Color.Transparent;
            this.macLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel2.Location = new System.Drawing.Point(29, 211);
            this.macLabel2.Name = "macLabel2";
            this.macLabel2.Size = new System.Drawing.Size(125, 17);
            this.macLabel2.TabIndex = 28;
            this.macLabel2.Text = "Text Tick Style";
            // 
            // macLabel1
            // 
            this.macLabel1.BackColor = System.Drawing.Color.Transparent;
            this.macLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel1.Location = new System.Drawing.Point(29, 136);
            this.macLabel1.Name = "macLabel1";
            this.macLabel1.Size = new System.Drawing.Size(115, 26);
            this.macLabel1.TabIndex = 26;
            this.macLabel1.Text = "Border Style";
            // 
            // macPanel1
            // 
            this.macPanel1.BackColor = System.Drawing.Color.Transparent;
            this.macPanel1.Controls.Add(this.macTrackBar1);
            this.macPanel1.Location = new System.Drawing.Point(178, 17);
            this.macPanel1.Name = "macPanel1";
            this.macPanel1.Size = new System.Drawing.Size(412, 95);
            this.macPanel1.TabIndex = 0;
            // 
            // macTrackBar1
            // 
            this.macTrackBar1.BackColor = System.Drawing.Color.Transparent;
            this.macTrackBar1.BorderColor = System.Drawing.SystemColors.ActiveBorder;
            this.macTrackBar1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.macTrackBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(125)))), ((int)(((byte)(123)))));
            this.macTrackBar1.IndentHeight = 6;
            this.macTrackBar1.Location = new System.Drawing.Point(34, 12);
            this.macTrackBar1.Maximum = 10;
            this.macTrackBar1.Minimum = 0;
            this.macTrackBar1.Name = "macTrackBar1";
            this.macTrackBar1.Size = new System.Drawing.Size(345, 72);
            this.macTrackBar1.TabIndex = 0;
            this.macTrackBar1.TextTickStyle = System.Windows.Forms.TickStyle.Both;
            this.macTrackBar1.TickColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(146)))), ((int)(((byte)(148)))));
            this.macTrackBar1.TickHeight = 4;
            this.macTrackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.macTrackBar1.TrackerColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(130)))), ((int)(((byte)(198)))));
            this.macTrackBar1.TrackerSize = new System.Drawing.Size(16, 16);
            this.macTrackBar1.TrackLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(93)))), ((int)(((byte)(90)))));
            this.macTrackBar1.TrackLineHeight = 3;
            this.macTrackBar1.Value = 0;
            this.macTrackBar1.Scroll += new System.EventHandler(this.macTrackBar1_Scroll);
            // 
            // macLabel9
            // 
            this.macLabel9.BackColor = System.Drawing.Color.Transparent;
            this.macLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel9.Location = new System.Drawing.Point(29, 177);
            this.macLabel9.Name = "macLabel9";
            this.macLabel9.Size = new System.Drawing.Size(96, 17);
            this.macLabel9.TabIndex = 45;
            this.macLabel9.Text = "Text Color";
            // 
            // macLabel10
            // 
            this.macLabel10.BackColor = System.Drawing.Color.Transparent;
            this.macLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel10.Location = new System.Drawing.Point(29, 431);
            this.macLabel10.Name = "macLabel10";
            this.macLabel10.Size = new System.Drawing.Size(96, 17);
            this.macLabel10.TabIndex = 45;
            this.macLabel10.Text = "Tick Color";
            // 
            // numTickHeight
            // 
            this.numTickHeight.BackColor = System.Drawing.SystemColors.Window;
            this.numTickHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numTickHeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numTickHeight.Location = new System.Drawing.Point(480, 327);
            this.numTickHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numTickHeight.Name = "numTickHeight";
            this.numTickHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTickHeight.Size = new System.Drawing.Size(134, 21);
            this.numTickHeight.TabIndex = 18;
            this.numTickHeight.ValueChanged += new System.EventHandler(this.numTickHeight_ValueChanged);
            // 
            // macLabel11
            // 
            this.macLabel11.BackColor = System.Drawing.Color.Transparent;
            this.macLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel11.Location = new System.Drawing.Point(346, 327);
            this.macLabel11.Name = "macLabel11";
            this.macLabel11.Size = new System.Drawing.Size(124, 18);
            this.macLabel11.TabIndex = 32;
            this.macLabel11.Text = "Tick Height";
            // 
            // macLabel12
            // 
            this.macLabel12.BackColor = System.Drawing.Color.Transparent;
            this.macLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel12.Location = new System.Drawing.Point(29, 284);
            this.macLabel12.Name = "macLabel12";
            this.macLabel12.Size = new System.Drawing.Size(115, 26);
            this.macLabel12.TabIndex = 37;
            this.macLabel12.Text = "Tracker Width";
            // 
            // macLabel13
            // 
            this.macLabel13.BackColor = System.Drawing.Color.Transparent;
            this.macLabel13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel13.Location = new System.Drawing.Point(29, 319);
            this.macLabel13.Name = "macLabel13";
            this.macLabel13.Size = new System.Drawing.Size(134, 26);
            this.macLabel13.TabIndex = 40;
            this.macLabel13.Text = "Tracker Height";
            // 
            // numTrackerWidth
            // 
            this.numTrackerWidth.BackColor = System.Drawing.SystemColors.Window;
            this.numTrackerWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numTrackerWidth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numTrackerWidth.Location = new System.Drawing.Point(163, 284);
            this.numTrackerWidth.Name = "numTrackerWidth";
            this.numTrackerWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTrackerWidth.Size = new System.Drawing.Size(135, 21);
            this.numTrackerWidth.TabIndex = 5;
            this.numTrackerWidth.ValueChanged += new System.EventHandler(this.numTrackerWidth_ValueChanged);
            // 
            // numTrackerHeight
            // 
            this.numTrackerHeight.BackColor = System.Drawing.SystemColors.Window;
            this.numTrackerHeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numTrackerHeight.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numTrackerHeight.Location = new System.Drawing.Point(163, 319);
            this.numTrackerHeight.Name = "numTrackerHeight";
            this.numTrackerHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numTrackerHeight.Size = new System.Drawing.Size(135, 21);
            this.numTrackerHeight.TabIndex = 6;
            this.numTrackerHeight.ValueChanged += new System.EventHandler(this.numTrackerHeight_ValueChanged);
            // 
            // macLabel14
            // 
            this.macLabel14.BackColor = System.Drawing.Color.Transparent;
            this.macLabel14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel14.Location = new System.Drawing.Point(29, 362);
            this.macLabel14.Name = "macLabel14";
            this.macLabel14.Size = new System.Drawing.Size(125, 17);
            this.macLabel14.TabIndex = 45;
            this.macLabel14.Text = "TrackLine Color";
            // 
            // macLabel15
            // 
            this.macLabel15.BackColor = System.Drawing.Color.Transparent;
            this.macLabel15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel15.Location = new System.Drawing.Point(346, 136);
            this.macLabel15.Name = "macLabel15";
            this.macLabel15.Size = new System.Drawing.Size(115, 26);
            this.macLabel15.TabIndex = 37;
            this.macLabel15.Text = "Large Change";
            // 
            // numLargeChange
            // 
            this.numLargeChange.BackColor = System.Drawing.SystemColors.Window;
            this.numLargeChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numLargeChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numLargeChange.Location = new System.Drawing.Point(480, 137);
            this.numLargeChange.Name = "numLargeChange";
            this.numLargeChange.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numLargeChange.Size = new System.Drawing.Size(134, 21);
            this.numLargeChange.TabIndex = 13;
            this.numLargeChange.ValueChanged += new System.EventHandler(this.numLargeChange_ValueChanged);
            // 
            // macLabel16
            // 
            this.macLabel16.BackColor = System.Drawing.Color.Transparent;
            this.macLabel16.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel16.Location = new System.Drawing.Point(346, 172);
            this.macLabel16.Name = "macLabel16";
            this.macLabel16.Size = new System.Drawing.Size(115, 26);
            this.macLabel16.TabIndex = 37;
            this.macLabel16.Text = "Small Change";
            // 
            // numSmallChange
            // 
            this.numSmallChange.BackColor = System.Drawing.SystemColors.Window;
            this.numSmallChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numSmallChange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numSmallChange.Location = new System.Drawing.Point(480, 173);
            this.numSmallChange.Name = "numSmallChange";
            this.numSmallChange.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numSmallChange.Size = new System.Drawing.Size(134, 21);
            this.numSmallChange.TabIndex = 14;
            this.numSmallChange.ValueChanged += new System.EventHandler(this.numSmallChange_ValueChanged);
            // 
            // macLabel17
            // 
            this.macLabel17.BackColor = System.Drawing.Color.Transparent;
            this.macLabel17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel17.Location = new System.Drawing.Point(346, 284);
            this.macLabel17.Name = "macLabel17";
            this.macLabel17.Size = new System.Drawing.Size(105, 26);
            this.macLabel17.TabIndex = 40;
            this.macLabel17.Text = "Value";
            // 
            // numValue
            // 
            this.numValue.BackColor = System.Drawing.SystemColors.Window;
            this.numValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.numValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.numValue.Location = new System.Drawing.Point(480, 285);
            this.numValue.Name = "numValue";
            this.numValue.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.numValue.Size = new System.Drawing.Size(134, 21);
            this.numValue.TabIndex = 17;
            this.numValue.ValueChanged += new System.EventHandler(this.numValue_ValueChanged);
            // 
            // btnForeColor
            // 
            this.btnForeColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnForeColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForeColor.ForeColor = System.Drawing.Color.White;
            this.btnForeColor.Location = new System.Drawing.Point(163, 172);
            this.btnForeColor.Name = "btnForeColor";
            this.btnForeColor.Size = new System.Drawing.Size(39, 26);
            this.btnForeColor.TabIndex = 2;
            this.btnForeColor.Text = "...";
            this.btnForeColor.UseVisualStyleBackColor = false;
            this.btnForeColor.Click += new System.EventHandler(this.btnForeColor_Click);
            // 
            // btnTrackerColor
            // 
            this.btnTrackerColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnTrackerColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrackerColor.ForeColor = System.Drawing.Color.White;
            this.btnTrackerColor.Location = new System.Drawing.Point(163, 241);
            this.btnTrackerColor.Name = "btnTrackerColor";
            this.btnTrackerColor.Size = new System.Drawing.Size(39, 26);
            this.btnTrackerColor.TabIndex = 4;
            this.btnTrackerColor.Text = "...";
            this.btnTrackerColor.UseVisualStyleBackColor = false;
            this.btnTrackerColor.Click += new System.EventHandler(this.btnTrackerColor_Click);
            // 
            // btnTrackLineColor
            // 
            this.btnTrackLineColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnTrackLineColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTrackLineColor.ForeColor = System.Drawing.Color.White;
            this.btnTrackLineColor.Location = new System.Drawing.Point(163, 362);
            this.btnTrackLineColor.Name = "btnTrackLineColor";
            this.btnTrackLineColor.Size = new System.Drawing.Size(39, 26);
            this.btnTrackLineColor.TabIndex = 7;
            this.btnTrackLineColor.Text = "...";
            this.btnTrackLineColor.UseVisualStyleBackColor = false;
            this.btnTrackLineColor.Click += new System.EventHandler(this.btnTrackLineColor_Click);
            // 
            // btnTickColor
            // 
            this.btnTickColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(120)))), ((int)(((byte)(189)))));
            this.btnTickColor.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTickColor.ForeColor = System.Drawing.Color.White;
            this.btnTickColor.Location = new System.Drawing.Point(163, 431);
            this.btnTickColor.Name = "btnTickColor";
            this.btnTickColor.Size = new System.Drawing.Size(39, 26);
            this.btnTickColor.TabIndex = 9;
            this.btnTickColor.Text = "...";
            this.btnTickColor.UseVisualStyleBackColor = false;
            this.btnTickColor.Click += new System.EventHandler(this.btnTickColor_Click);
            // 
            // cboTextTickStyle
            // 
            this.cboTextTickStyle.BackColor = System.Drawing.SystemColors.Window;
            this.cboTextTickStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextTickStyle.DropDownWidth = 128;
            this.cboTextTickStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboTextTickStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.cboTextTickStyle.ItemHeight = 13;
            this.cboTextTickStyle.Items.AddRange(new object[] {
            "None",
            "TopLeft",
            "BottomRight",
            "Both"});
            this.cboTextTickStyle.Location = new System.Drawing.Point(163, 209);
            this.cboTextTickStyle.Name = "cboTextTickStyle";
            this.cboTextTickStyle.Size = new System.Drawing.Size(154, 21);
            this.cboTextTickStyle.TabIndex = 3;
            this.cboTextTickStyle.SelectedIndexChanged += new System.EventHandler(this.cboTextTickStyle_SelectedIndexChanged);
            // 
            // cboTickStyle
            // 
            this.cboTickStyle.BackColor = System.Drawing.SystemColors.Window;
            this.cboTickStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTickStyle.DropDownWidth = 128;
            this.cboTickStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboTickStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.cboTickStyle.ItemHeight = 13;
            this.cboTickStyle.Items.AddRange(new object[] {
            "None",
            "TopLeft",
            "BottomRight",
            "Both"});
            this.cboTickStyle.Location = new System.Drawing.Point(480, 396);
            this.cboTickStyle.Name = "cboTickStyle";
            this.cboTickStyle.Size = new System.Drawing.Size(154, 21);
            this.cboTickStyle.TabIndex = 11;
            this.cboTickStyle.SelectedIndexChanged += new System.EventHandler(this.cboTickStyle_SelectedIndexChanged);
            // 
            // macLabel3
            // 
            this.macLabel3.BackColor = System.Drawing.Color.Transparent;
            this.macLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.macLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.macLabel3.Location = new System.Drawing.Point(346, 396);
            this.macLabel3.Name = "macLabel3";
            this.macLabel3.Size = new System.Drawing.Size(144, 18);
            this.macLabel3.TabIndex = 32;
            this.macLabel3.Text = "Tick Style";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(89)))), ((int)(((byte)(90)))));
            this.label1.Location = new System.Drawing.Point(29, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 37;
            this.label1.Text = "Tracker Color";
            // 
            // macSlider_Demo
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(796, 540);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.cboBorderStyle);
            this.Controls.Add(this.btnForeColor);
            this.Controls.Add(this.btnTrackerColor);
            this.Controls.Add(this.btnTrackLineColor);
            this.Controls.Add(this.btnTickColor);
            this.Controls.Add(this.cboTextTickStyle);
            this.Controls.Add(this.cboTickStyle);
            this.Controls.Add(this.numTrackLineHeight);
            this.Controls.Add(this.macLabel7);
            this.Controls.Add(this.numMinimum);
            this.Controls.Add(this.numMaximum);
            this.Controls.Add(this.macLabel6);
            this.Controls.Add(this.numTickFrequency);
            this.Controls.Add(this.macLabel5);
            this.Controls.Add(this.macPanel2);
            this.Controls.Add(this.macLabel4);
            this.Controls.Add(this.macLabel2);
            this.Controls.Add(this.macLabel1);
            this.Controls.Add(this.macLabel8);
            this.Controls.Add(this.macLabel9);
            this.Controls.Add(this.macLabel10);
            this.Controls.Add(this.numTickHeight);
            this.Controls.Add(this.macLabel11);
            this.Controls.Add(this.macLabel12);
            this.Controls.Add(this.macLabel13);
            this.Controls.Add(this.numTrackerWidth);
            this.Controls.Add(this.numTrackerHeight);
            this.Controls.Add(this.macLabel14);
            this.Controls.Add(this.macLabel15);
            this.Controls.Add(this.numLargeChange);
            this.Controls.Add(this.macLabel16);
            this.Controls.Add(this.numSmallChange);
            this.Controls.Add(this.macLabel17);
            this.Controls.Add(this.numValue);
            this.Controls.Add(this.macLabel3);
            this.Controls.Add(this.macPanel1);
            this.Controls.Add(this.label1);
            this.Name = "macSlider_Demo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slider (TrackBar) - Demo";
            this.Closed += new System.EventHandler(this.macSlider_Demo_Closed);
            this.Load += new System.EventHandler(this.macSlider_Demo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTrackLineHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMinimum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaximum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTickFrequency)).EndInit();
            this.macPanel2.ResumeLayout(false);
            this.macPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numTickHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrackerWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTrackerHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLargeChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSmallChange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void macSlider_Demo_Load(object sender, System.EventArgs e)
		{
			InitControls();
		}

		private void InitControls()
		{

			this.cboBorderStyle.SelectedIndex = (int)this.macTrackBar1.BorderStyle;
			this.btnForeColor.BackColor = this.macTrackBar1.ForeColor;
			this.cboTextTickStyle.SelectedIndex = (int)this.macTrackBar1.TextTickStyle;

			this.btnTickColor.BackColor = this.macTrackBar1.TickColor;
			this.numTickFrequency.Value = this.macTrackBar1.TickFrequency;
			this.numTickHeight.Value = this.macTrackBar1.TickHeight;
			this.cboTickStyle.SelectedIndex = (int)this.macTrackBar1.TickStyle;

			this.btnTrackerColor.BackColor = this.macTrackBar1.TrackerColor;
			this.numTrackerWidth.Value = this.macTrackBar1.TrackerSize.Width;
			this.numTrackerHeight.Value = this.macTrackBar1.TrackerSize.Height;
			this.btnTrackLineColor.BackColor = this.macTrackBar1.TrackLineColor;
			this.numTrackLineHeight.Value = this.macTrackBar1.TrackLineHeight;

			this.numLargeChange.Value = this.macTrackBar1.LargeChange;
			this.numSmallChange.Value = this.macTrackBar1.SmallChange;
			this.numMaximum.Value = this.macTrackBar1.Maximum;
			this.numMinimum.Value = this.macTrackBar1.Minimum;
			this.numValue.Value = this.macTrackBar1.Value;
	
		}

		private void cboBorderStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.BorderStyle = (MACBorderStyle) cboBorderStyle.SelectedIndex;
			this.macTrackBar2.BorderStyle = (MACBorderStyle) cboBorderStyle.SelectedIndex;
		
		}

		private void cboTextTickStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TextTickStyle = (TickStyle) cboTextTickStyle.SelectedIndex;
			this.macTrackBar2.TextTickStyle = (TickStyle) cboTextTickStyle.SelectedIndex;
		
		}

		private void cboTickStyle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TickStyle = (TickStyle) cboTickStyle.SelectedIndex;
			this.macTrackBar2.TickStyle = (TickStyle) cboTickStyle.SelectedIndex;
		
		}

		private void btnForeColor_Click(object sender, System.EventArgs e)
		{
			this.colorDialog1.Color = this.btnForeColor.BackColor;
			if(colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.btnForeColor.BackColor = colorDialog1.Color;
				this.macTrackBar1.ForeColor = colorDialog1.Color;
				this.macTrackBar2.ForeColor = colorDialog1.Color;
			}
		
		}

		private void btnTrackerColor_Click(object sender, System.EventArgs e)
		{
			this.colorDialog1.Color = this.btnTrackerColor.BackColor;
			if(colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.btnTrackerColor.BackColor = colorDialog1.Color;
				this.macTrackBar1.TrackerColor = colorDialog1.Color;
				this.macTrackBar2.TrackerColor = colorDialog1.Color;
			}
		
		}

		private void btnTrackLineColor_Click(object sender, System.EventArgs e)
		{
			this.colorDialog1.Color = this.btnTrackLineColor.BackColor;
			if(colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.btnTrackLineColor.BackColor = colorDialog1.Color;
				this.macTrackBar1.TrackLineColor = colorDialog1.Color;
				this.macTrackBar2.TrackLineColor = colorDialog1.Color;
			}
		
		}

		private void btnTickColor_Click(object sender, System.EventArgs e)
		{
			this.colorDialog1.Color = this.btnTickColor.BackColor;
			if(colorDialog1.ShowDialog() == DialogResult.OK)
			{
				this.btnTickColor.BackColor = colorDialog1.Color;
				this.macTrackBar1.TickColor = colorDialog1.Color;
				this.macTrackBar2.TickColor = colorDialog1.Color;
			}
		
		}

		private void numTrackerWidth_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TrackerSize = new Size((int)numTrackerWidth.Value,macTrackBar1.TrackerSize.Height);
			this.macTrackBar2.TrackerSize = new Size((int)numTrackerWidth.Value,macTrackBar2.TrackerSize.Height);
	
		}

		private void numTrackerHeight_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TrackerSize = new Size(macTrackBar1.TrackerSize.Width, (int)numTrackerHeight.Value);
			this.macTrackBar2.TrackerSize = new Size(macTrackBar2.TrackerSize.Width, (int)numTrackerHeight.Value);
		}

		private void numTrackLineHeight_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TrackLineHeight = (int)numTrackLineHeight.Value;
			this.macTrackBar2.TrackLineHeight = (int)numTrackLineHeight.Value;
		
		}

		private void numTickFrequency_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TickFrequency = (int)numTickFrequency.Value;
			this.macTrackBar2.TickFrequency = (int)numTickFrequency.Value;
		
		}

		private void numLargeChange_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.LargeChange = (int)numLargeChange.Value;
			this.macTrackBar2.LargeChange = (int)numLargeChange.Value;
	
		}

		private void numSmallChange_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.SmallChange = (int)numSmallChange.Value;
			this.macTrackBar2.SmallChange = (int)numSmallChange.Value;
		
		}

		private void numMaximum_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.Maximum = (int)numMaximum.Value;
			this.macTrackBar2.Maximum = (int)numMaximum.Value;
		
		}

		private void numMinimum_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.Minimum = (int)numMinimum.Value;
			this.macTrackBar2.Minimum = (int)numMinimum.Value;
		
		}

		private void numValue_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.Value = (int)numValue.Value;
			this.macTrackBar2.Value = (int)numValue.Value;
		
		}

		private void numTickHeight_ValueChanged(object sender, System.EventArgs e)
		{
			this.macTrackBar1.TickHeight = (int)numTickHeight.Value;
			this.macTrackBar2.TickHeight = (int)numTickHeight.Value;
		
		}

		private void macTrackBar1_Scroll(object sender, System.EventArgs e)
		{
			numValue.Value = macTrackBar1.Value;
			macTrackBar2.Value = macTrackBar1.Value;
		
		}

		private void macTrackBar2_Scroll(object sender, System.EventArgs e)
		{
			numValue.Value = macTrackBar2.Value;
			macTrackBar1.Value = macTrackBar2.Value;
		
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.macTrackBar1.Increment(macTrackBar1.SmallChange);
			if(this.macTrackBar1.Value == this.macTrackBar1.Maximum)
				this.macTrackBar1.Value = this.macTrackBar1.Minimum;

			this.macTrackBar2.Increment(macTrackBar1.SmallChange);
			if(this.macTrackBar2.Value == this.macTrackBar2.Maximum)
				this.macTrackBar2.Value = this.macTrackBar2.Minimum;
		
		}

		private void btnStart_Click(object sender, System.EventArgs e)
		{
			timer1.Start();
		
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			timer1.Stop();
		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		
		}

		private void macSlider_Demo_Closed(object sender, System.EventArgs e)
		{
			timer1.Stop();
		
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FrmMacTrackBar());
		}


	}
}

