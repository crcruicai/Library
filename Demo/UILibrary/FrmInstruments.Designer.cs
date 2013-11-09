/*
 * Created by SharpDevelop.
 * User: Tefik Becirovic
 * Date: 15.10.2008
 * Time: 19:42
 * 
 */
using CRC.Controls;
namespace UILibrary
{
	partial class FrmInstruments
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
			
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            ChartPen chartPen1 = new ChartPen();
            ChartPen chartPen2 = new ChartPen();
            ChartPen chartPen3 = new ChartPen();
            ChartPen chartPen4 = new ChartPen();
            this.grpBxValueGen = new System.Windows.Forms.GroupBox();
            this.comboBox_UserDefinedSignalType = new System.Windows.Forms.ComboBox();
            this.button_ResetSignal = new System.Windows.Forms.Button();
            this.checkBox_Invert = new System.Windows.Forms.CheckBox();
            this.label_Frequency = new System.Windows.Forms.Label();
            this.label_Amplitude = new System.Windows.Forms.Label();
            this.trackBar_Frequency = new System.Windows.Forms.TrackBar();
            this.trackBar_Amplitude = new System.Windows.Forms.TrackBar();
            this.label_SignalType = new System.Windows.Forms.Label();
            this.comboBox_SignalType = new System.Windows.Forms.ComboBox();
            this.button_Clear = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buton_Close = new System.Windows.Forms.Button();
            this.label_Gain = new System.Windows.Forms.Label();
            this.label_Offset = new System.Windows.Forms.Label();
            this.label_SamplingRate = new System.Windows.Forms.Label();
            this.trackBar_Gain = new System.Windows.Forms.TrackBar();
            this.trackBar_Offset = new System.Windows.Forms.TrackBar();
            this.trackBar_SamplingRate = new System.Windows.Forms.TrackBar();
            this.button_Default = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.slidingScale5 = new SlidingScale();
            this.slidingScale4 = new SlidingScale();
            this.slidingScale3 = new SlidingScale();
            this.slidingScale2 = new SlidingScale();
            this.slidingScale1 = new SlidingScale();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.runChart1 = new PerfChart();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.grpBxValueGen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Frequency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Amplitude)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Gain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Offset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SamplingRate)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpBxValueGen
            // 
            this.grpBxValueGen.Controls.Add(this.comboBox_UserDefinedSignalType);
            this.grpBxValueGen.Controls.Add(this.button_ResetSignal);
            this.grpBxValueGen.Controls.Add(this.checkBox_Invert);
            this.grpBxValueGen.Controls.Add(this.label_Frequency);
            this.grpBxValueGen.Controls.Add(this.label_Amplitude);
            this.grpBxValueGen.Controls.Add(this.trackBar_Frequency);
            this.grpBxValueGen.Controls.Add(this.trackBar_Amplitude);
            this.grpBxValueGen.Controls.Add(this.label_SignalType);
            this.grpBxValueGen.Controls.Add(this.comboBox_SignalType);
            this.grpBxValueGen.Location = new System.Drawing.Point(12, 180);
            this.grpBxValueGen.Name = "grpBxValueGen";
            this.grpBxValueGen.Size = new System.Drawing.Size(270, 130);
            this.grpBxValueGen.TabIndex = 2;
            this.grpBxValueGen.TabStop = false;
            this.grpBxValueGen.Text = " SIGNAL: ";
            // 
            // comboBox_UserDefinedSignalType
            // 
            this.comboBox_UserDefinedSignalType.FormattingEnabled = true;
            this.comboBox_UserDefinedSignalType.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.comboBox_UserDefinedSignalType.Location = new System.Drawing.Point(104, 40);
            this.comboBox_UserDefinedSignalType.Name = "comboBox_UserDefinedSignalType";
            this.comboBox_UserDefinedSignalType.Size = new System.Drawing.Size(32, 20);
            this.comboBox_UserDefinedSignalType.TabIndex = 8;
            this.comboBox_UserDefinedSignalType.Text = "0";
            this.comboBox_UserDefinedSignalType.Visible = false;
            // 
            // button_ResetSignal
            // 
            this.button_ResetSignal.Location = new System.Drawing.Point(17, 94);
            this.button_ResetSignal.Name = "button_ResetSignal";
            this.button_ResetSignal.Size = new System.Drawing.Size(90, 21);
            this.button_ResetSignal.TabIndex = 7;
            this.button_ResetSignal.Text = "&RESET";
            this.button_ResetSignal.Click += new System.EventHandler(this.Button_ResetSignal_Click);
            // 
            // checkBox_Invert
            // 
            this.checkBox_Invert.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBox_Invert.Location = new System.Drawing.Point(17, 66);
            this.checkBox_Invert.Name = "checkBox_Invert";
            this.checkBox_Invert.Size = new System.Drawing.Size(90, 22);
            this.checkBox_Invert.TabIndex = 1;
            this.checkBox_Invert.Text = "INVERT";
            this.checkBox_Invert.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_Invert.CheckedChanged += new System.EventHandler(this.CheckBox_InvertCheckedChanged);
            // 
            // label_Frequency
            // 
            this.label_Frequency.Location = new System.Drawing.Point(129, 18);
            this.label_Frequency.Name = "label_Frequency";
            this.label_Frequency.Size = new System.Drawing.Size(133, 15);
            this.label_Frequency.TabIndex = 5;
            this.label_Frequency.Text = "FREQUENCY:";
            this.label_Frequency.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label_Amplitude
            // 
            this.label_Amplitude.Location = new System.Drawing.Point(129, 73);
            this.label_Amplitude.Name = "label_Amplitude";
            this.label_Amplitude.Size = new System.Drawing.Size(133, 15);
            this.label_Amplitude.TabIndex = 4;
            this.label_Amplitude.Text = "AMPLITUDE:";
            this.label_Amplitude.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // trackBar_Frequency
            // 
            this.trackBar_Frequency.Location = new System.Drawing.Point(129, 31);
            this.trackBar_Frequency.Name = "trackBar_Frequency";
            this.trackBar_Frequency.Size = new System.Drawing.Size(133, 45);
            this.trackBar_Frequency.TabIndex = 3;
            this.trackBar_Frequency.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Frequency.Value = 5;
            this.trackBar_Frequency.ValueChanged += new System.EventHandler(this.TrackBar_Frequency_ValueChanged);
            // 
            // trackBar_Amplitude
            // 
            this.trackBar_Amplitude.Location = new System.Drawing.Point(129, 86);
            this.trackBar_Amplitude.Name = "trackBar_Amplitude";
            this.trackBar_Amplitude.Size = new System.Drawing.Size(133, 45);
            this.trackBar_Amplitude.TabIndex = 2;
            this.trackBar_Amplitude.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar_Amplitude.Value = 5;
            this.trackBar_Amplitude.ValueChanged += new System.EventHandler(this.TrackBar_Amplitude_ValueChanged);
            // 
            // label_SignalType
            // 
            this.label_SignalType.Location = new System.Drawing.Point(17, 21);
            this.label_SignalType.Name = "label_SignalType";
            this.label_SignalType.Size = new System.Drawing.Size(90, 16);
            this.label_SignalType.TabIndex = 1;
            this.label_SignalType.Text = "TYPE:";
            this.label_SignalType.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // comboBox_SignalType
            // 
            this.comboBox_SignalType.FormattingEnabled = true;
            this.comboBox_SignalType.Location = new System.Drawing.Point(17, 40);
            this.comboBox_SignalType.Name = "comboBox_SignalType";
            this.comboBox_SignalType.Size = new System.Drawing.Size(90, 20);
            this.comboBox_SignalType.TabIndex = 0;
            this.comboBox_SignalType.SelectedIndexChanged += new System.EventHandler(this.ComboBox_SignalType_SelectedIndexChanged);
            // 
            // button_Clear
            // 
            this.button_Clear.Location = new System.Drawing.Point(26, 115);
            this.button_Clear.Name = "button_Clear";
            this.button_Clear.Size = new System.Drawing.Size(84, 21);
            this.button_Clear.TabIndex = 6;
            this.button_Clear.Text = "&CLEAR";
            this.button_Clear.Click += new System.EventHandler(this.Button_Clear_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 60;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // buton_Close
            // 
            this.buton_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.buton_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buton_Close.ForeColor = System.Drawing.Color.White;
            this.buton_Close.Location = new System.Drawing.Point(318, 281);
            this.buton_Close.Name = "buton_Close";
            this.buton_Close.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buton_Close.Size = new System.Drawing.Size(84, 30);
            this.buton_Close.TabIndex = 7;
            this.buton_Close.Text = "CLOSE";
            this.buton_Close.UseVisualStyleBackColor = false;
            this.buton_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // label_Gain
            // 
            this.label_Gain.Location = new System.Drawing.Point(6, 13);
            this.label_Gain.Name = "label_Gain";
            this.label_Gain.Size = new System.Drawing.Size(122, 21);
            this.label_Gain.TabIndex = 8;
            this.label_Gain.Text = "GAIN";
            this.label_Gain.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_Offset
            // 
            this.label_Offset.Location = new System.Drawing.Point(6, 63);
            this.label_Offset.Name = "label_Offset";
            this.label_Offset.Size = new System.Drawing.Size(122, 30);
            this.label_Offset.TabIndex = 9;
            this.label_Offset.Text = "OFFSET";
            this.label_Offset.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label_SamplingRate
            // 
            this.label_SamplingRate.Location = new System.Drawing.Point(6, 150);
            this.label_SamplingRate.Name = "label_SamplingRate";
            this.label_SamplingRate.Size = new System.Drawing.Size(122, 30);
            this.label_SamplingRate.TabIndex = 10;
            this.label_SamplingRate.Text = "SAMPLING  RATE";
            this.label_SamplingRate.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // trackBar_Gain
            // 
            this.trackBar_Gain.Location = new System.Drawing.Point(6, 26);
            this.trackBar_Gain.Name = "trackBar_Gain";
            this.trackBar_Gain.Size = new System.Drawing.Size(122, 45);
            this.trackBar_Gain.TabIndex = 11;
            this.trackBar_Gain.Value = 5;
            this.trackBar_Gain.ValueChanged += new System.EventHandler(this.TrackBar_Gain_ValueChanged);
            // 
            // trackBar_Offset
            // 
            this.trackBar_Offset.Location = new System.Drawing.Point(6, 84);
            this.trackBar_Offset.Name = "trackBar_Offset";
            this.trackBar_Offset.Size = new System.Drawing.Size(122, 45);
            this.trackBar_Offset.TabIndex = 12;
            this.trackBar_Offset.Value = 5;
            this.trackBar_Offset.ValueChanged += new System.EventHandler(this.TrackBar_Offset_ValueChanged);
            // 
            // trackBar_SamplingRate
            // 
            this.trackBar_SamplingRate.Location = new System.Drawing.Point(6, 162);
            this.trackBar_SamplingRate.Name = "trackBar_SamplingRate";
            this.trackBar_SamplingRate.Size = new System.Drawing.Size(122, 45);
            this.trackBar_SamplingRate.TabIndex = 13;
            this.trackBar_SamplingRate.Value = 5;
            this.trackBar_SamplingRate.ValueChanged += new System.EventHandler(this.TrackBar_SamplingRate_ValueChanged);
            // 
            // button_Default
            // 
            this.button_Default.Location = new System.Drawing.Point(318, 242);
            this.button_Default.Name = "button_Default";
            this.button_Default.Size = new System.Drawing.Size(84, 30);
            this.button_Default.TabIndex = 14;
            this.button_Default.Text = "&DEFAULT";
            this.button_Default.Click += new System.EventHandler(this.Button_Default_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_Clear);
            this.groupBox1.Controls.Add(this.trackBar_Offset);
            this.groupBox1.Controls.Add(this.label_Offset);
            this.groupBox1.Controls.Add(this.trackBar_SamplingRate);
            this.groupBox1.Controls.Add(this.trackBar_Gain);
            this.groupBox1.Controls.Add(this.label_SamplingRate);
            this.groupBox1.Controls.Add(this.label_Gain);
            this.groupBox1.Location = new System.Drawing.Point(292, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(134, 203);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DISPLAY: ";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.slidingScale5);
            this.tabPage2.Controls.Add(this.slidingScale4);
            this.tabPage2.Controls.Add(this.slidingScale3);
            this.tabPage2.Controls.Add(this.slidingScale2);
            this.tabPage2.Controls.Add(this.slidingScale1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(262, 124);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "Example 2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // slidingScale5
            // 
            this.slidingScale5.BackColor = System.Drawing.Color.White;
            this.slidingScale5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.slidingScale5.ForeColor = System.Drawing.Color.Black;
            this.slidingScale5.Location = new System.Drawing.Point(208, 5);
            this.slidingScale5.Name = "slidingScale5";
            this.slidingScale5.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slidingScale5.Size = new System.Drawing.Size(45, 117);
            this.slidingScale5.TabIndex = 4;
            // 
            // slidingScale4
            // 
            this.slidingScale4.BackColor = System.Drawing.Color.White;
            this.slidingScale4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.slidingScale4.ForeColor = System.Drawing.Color.Black;
            this.slidingScale4.Location = new System.Drawing.Point(158, 5);
            this.slidingScale4.Name = "slidingScale4";
            this.slidingScale4.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slidingScale4.Size = new System.Drawing.Size(45, 117);
            this.slidingScale4.TabIndex = 3;
            // 
            // slidingScale3
            // 
            this.slidingScale3.BackColor = System.Drawing.Color.White;
            this.slidingScale3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.slidingScale3.ForeColor = System.Drawing.Color.Black;
            this.slidingScale3.Location = new System.Drawing.Point(108, 5);
            this.slidingScale3.Name = "slidingScale3";
            this.slidingScale3.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slidingScale3.Size = new System.Drawing.Size(45, 117);
            this.slidingScale3.TabIndex = 2;
            // 
            // slidingScale2
            // 
            this.slidingScale2.BackColor = System.Drawing.Color.White;
            this.slidingScale2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.slidingScale2.ForeColor = System.Drawing.Color.Black;
            this.slidingScale2.Location = new System.Drawing.Point(58, 5);
            this.slidingScale2.Name = "slidingScale2";
            this.slidingScale2.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slidingScale2.Size = new System.Drawing.Size(45, 117);
            this.slidingScale2.TabIndex = 1;
            // 
            // slidingScale1
            // 
            this.slidingScale1.BackColor = System.Drawing.Color.White;
            this.slidingScale1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.slidingScale1.ForeColor = System.Drawing.Color.Black;
            this.slidingScale1.Location = new System.Drawing.Point(8, 5);
            this.slidingScale1.Name = "slidingScale1";
            this.slidingScale1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.slidingScale1.Size = new System.Drawing.Size(45, 117);
            this.slidingScale1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.runChart1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(262, 124);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Example 1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // runChart1
            // 
            this.runChart1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runChart1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.World);
            this.runChart1.Location = new System.Drawing.Point(3, 3);
            this.runChart1.Name = "runChart1";
            this.runChart1.PerfChartStyle.AntiAliasing = true;
            chartPen1.Color = System.Drawing.Color.LawnGreen;
            chartPen1.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            chartPen1.Width = 1F;
            this.runChart1.PerfChartStyle.AvgLinePen = chartPen1;
            this.runChart1.PerfChartStyle.BackgroundColorBottom = System.Drawing.Color.DodgerBlue;
            this.runChart1.PerfChartStyle.BackgroundColorTop = System.Drawing.Color.SkyBlue;
            chartPen2.Color = System.Drawing.Color.Yellow;
            chartPen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            chartPen2.Width = 2F;
            this.runChart1.PerfChartStyle.ChartLinePen = chartPen2;
            chartPen3.Color = System.Drawing.Color.DarkOliveGreen;
            chartPen3.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            chartPen3.Width = 1F;
            this.runChart1.PerfChartStyle.HorizontalGridPen = chartPen3;
            this.runChart1.PerfChartStyle.ShowAverageLine = false;
            this.runChart1.PerfChartStyle.ShowHorizontalGridLines = true;
            this.runChart1.PerfChartStyle.ShowVerticalGridLines = true;
            chartPen4.Color = System.Drawing.Color.DarkOliveGreen;
            chartPen4.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            chartPen4.Width = 1F;
            this.runChart1.PerfChartStyle.VerticalGridPen = chartPen4;
            this.runChart1.ScaleMode = ScaleMode.Absolute;
            this.runChart1.Size = new System.Drawing.Size(256, 118);
            this.runChart1.TabIndex = 0;
            this.runChart1.TimerInterval = 100;
            this.runChart1.TimerMode = TimerMode.Disabled;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 14);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(270, 150);
            this.tabControl1.TabIndex = 16;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(438, 321);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buton_Close);
            this.Controls.Add(this.grpBxValueGen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Default);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = " TB.Instruments Test Application";
            this.grpBxValueGen.ResumeLayout(false);
            this.grpBxValueGen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Frequency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Amplitude)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Gain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Offset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_SamplingRate)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.ComboBox comboBox_UserDefinedSignalType;
		private System.Windows.Forms.Label label_SamplingRate;
		private System.Windows.Forms.TrackBar trackBar_SamplingRate;
		private PerfChart runChart1;
		private System.Windows.Forms.TabPage tabPage2;
		private SlidingScale slidingScale2;
		private SlidingScale slidingScale3;
		private SlidingScale slidingScale4;
		private SlidingScale slidingScale5;
		private System.Windows.Forms.CheckBox checkBox_Invert;
		private System.Windows.Forms.Button button_Default;
		private System.Windows.Forms.Button button_ResetSignal;
		private SlidingScale slidingScale1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label_Offset;
		private System.Windows.Forms.TrackBar trackBar_Offset;
		private System.Windows.Forms.Button button_Clear;
		private System.Windows.Forms.Button buton_Close;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.TrackBar trackBar_Gain;
		private System.Windows.Forms.Label label_Gain;
		private System.Windows.Forms.TrackBar trackBar_Frequency;
		private System.Windows.Forms.Label label_Amplitude;
		private System.Windows.Forms.Label label_Frequency;
		private System.Windows.Forms.TrackBar trackBar_Amplitude;
		private System.Windows.Forms.ComboBox comboBox_SignalType;
		private System.Windows.Forms.Label label_SignalType;

		#endregion

		private System.Windows.Forms.GroupBox grpBxValueGen;
	}
}
