/*
 * Created by SharpDevelop.
 * User: Tefik Becirovic
 * Date: 15.10.2008
 * Time: 19:42
 * 
 */

using System;
using System.Windows.Forms;
using System.Threading;
using CRC.Controls;
namespace UILibrary
{

	public partial class FrmInstruments : Form
	{
		private SignalGenerator[] signalGenerator = new SignalGenerator[6];
		private int gain = 40;
		private int offset = 50;

		private Random random = new Random();
		
		private void ComboBox_SignalType_SelectedIndexChanged(object sender, EventArgs e)
		{
			SignalType st = (SignalType)Enum.Parse(
				typeof(SignalType), comboBox_SignalType.SelectedItem.ToString());
			for (int i=0; i<6; i++)
				signalGenerator[i].SignalType = st;
			
			comboBox_UserDefinedSignalType.Visible = st == SignalType.UserDefined;
		}

		private void Timer_Tick(object sender, System.EventArgs e)
		{
			runChart1.AddValue((decimal)Math.Min(gain*signalGenerator[0].GetValue()+offset,100));
			slidingScale1.Value = gain*signalGenerator[1].GetValue()+offset;
			slidingScale2.Value = gain*signalGenerator[2].GetValue()+offset;
			slidingScale3.Value = gain*signalGenerator[3].GetValue()+offset;
			slidingScale4.Value = gain*signalGenerator[4].GetValue()+offset;
			slidingScale5.Value = gain*signalGenerator[5].GetValue()+offset;
		}

		private void TrackBar_Amplitude_ValueChanged(object sender, EventArgs e)
		{
			for (int i=0; i<6; i++)
				signalGenerator[i].Amplitude = 0.2f*(float)trackBar_Amplitude.Value;
		}
		
		private void TrackBar_Frequency_ValueChanged(object sender, EventArgs e)
		{
			for (int i=0; i<6; i++)
				signalGenerator[i].Frequency = 0.2f*(float)trackBar_Frequency.Value;
		}
		

		private int[] SamplingRates = {2000,1000, 500,200,100, 50,20,10, 5,2,1};
		
		private void TrackBar_SamplingRate_ValueChanged(object sender, EventArgs e)
		{
			timer.Interval = SamplingRates[trackBar_SamplingRate.Value];
		}
		
		private void TrackBar_Gain_ValueChanged(object sender, EventArgs e)
		{
			gain = 8*trackBar_Gain.Value;
		}
		
		private void TrackBar_Offset_ValueChanged(object sender, EventArgs e)
		{
			offset = 20*(trackBar_Offset.Value-5)+50;
		}
		
		private void Button_Default_Click(object sender, EventArgs e)
		{
			trackBar_Frequency.Value = 5;
			trackBar_Amplitude.Value = 5;
			
			trackBar_Gain.Value = 5;
			trackBar_Offset.Value = 5;
			
			trackBar_SamplingRate.Value = 5;
		}

		private void Button_Clear_Click(object sender, EventArgs e)
		{
			runChart1.Clear();
		}
		
		private void Button_Close_Click(object sender, EventArgs e)
		{
			Close();
		}

		
		private void Button_ResetSignal_Click(object sender, EventArgs e)
		{
			for (int i=0; i<6; i++)
				signalGenerator[i].Reset();
		}

		private void CheckBox_InvertCheckedChanged(object sender, EventArgs e)
		{
			for (int i=0; i<6; i++)
				signalGenerator[i].Invert = checkBox_Invert.Checked;
		}

		
		public float UserDefinedSignal( float time )
		{
			// http://en.wikipedia.org/wiki/Radian // EN
			// http://www.ooowiki.de/BogenMa%C3%9F // DE
			
			switch (comboBox_UserDefinedSignalType.SelectedIndex)
			{
				case 0:
					//
					if ((time % 1) > 0.5f)
						return (float)Math.Sin(Math.PI*time);
					else
						return 0f;
					
				case 1:
					// http://mathworld.wolfram.com/SweepSignal.html:
					time = time % 5;
					return (float)Math.Sin(Math.PI*Math.Pow(time+1,2)-1);
					
				case 2:
					//
					time = (float)Math.Cos(time)/2;
					return (float)Math.Sin(Math.PI*Math.Pow(time+1,2)-1);
					
				case 3:
					// 
					return 2*(float)Math.Abs(Math.Sin(Math.PI*time))-1;

				case 4:
					// http://www.omatrix.com/sptmanual/signalgeneratormain.htm
					//  binbits - Random Binary Bits Function
					//	nrzbits - Randon Non-Return-Zero Binary Data
					// http://en.wikipedia.org/wiki/Non-return-to-zero
					return 1-2*random.Next(2);
					
				case 5:
					// -1,0,1
					return random.Next(3)-1;
				
				case 6:
					//
					return (float)(Math.Floor(5*time % 10))/5f-0.9f;

				default:
					return 0f;
			}
		}
		
		public FrmInstruments()
		{
			InitializeComponent();

			Font = SystemInformation.MenuFont;
			for (int i=0; i<6; i++)
			{
				Thread.Sleep(100);
				signalGenerator[i] = new SignalGenerator(SignalType.Sine);
				signalGenerator[i].GetValueCallback = UserDefinedSignal;
				if (i>0) signalGenerator[i].Synchronize(signalGenerator[0]);
			}
			
			foreach (String item in System.Enum.GetNames(typeof(SignalType)))
				comboBox_SignalType.Items.Add(item);
			comboBox_SignalType.SelectedItem = signalGenerator[0].SignalType.ToString();
		}

	}

}
