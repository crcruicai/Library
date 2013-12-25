using System;
using System.Drawing;
using System.Drawing.Text;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
// ��ʾ���ΪListBox�ؼ���ÿһ��Item��ӱ���ɫ���޸����ֵ���ɫ
namespace UILibrary
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	/// 

	public class FrmColorListBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lstColor;
		private string []data;
		private Color []color;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public FrmColorListBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//

			//Add items
			data= new String[10]{"This is Red","This is Blue","This is Green","This is Yellow","This is Black",
								"This is Aqua","This is Brown","This is Cyan","This is Gray","This is Pink"};
			lstColor.DataSource= data;

			color= new Color[10]{Color.Red,Color.Blue,Color.Green,Color.Yellow,Color.Black,
							   Color.Aqua,Color.Brown,Color.Cyan,Color.Gray, Color.Pink};

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lstColor = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstColor
            // 
            this.lstColor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstColor.Location = new System.Drawing.Point(10, 17);
            this.lstColor.Name = "lstColor";
            this.lstColor.Size = new System.Drawing.Size(326, 271);
            this.lstColor.TabIndex = 0;
            this.lstColor.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.DrawItemHandler);
            this.lstColor.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.MeasureItemHandler);
            // 
            // FrmColorListBox
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(354, 315);
            this.Controls.Add(this.lstColor);
            this.Name = "FrmColorListBox";
            this.Text = "ColorListBox Demo by Sanjay Ahuja";
            this.ResumeLayout(false);

		}
		#endregion

        ///// <summary>
        ///// The main entry point for the application.
        ///// </summary>
        //[STAThread]
        //static void Main() 
        //{
        //    Application.Run(new FrmColorListBox());
			
        //}

		private void DrawItemHandler(object sender,  DrawItemEventArgs e)
		{
			e.DrawBackground();
			e.DrawFocusRectangle();
			e.Graphics.DrawString(data[e.Index],new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold),new SolidBrush(color[e.Index]),e.Bounds);
	
		}
		private void MeasureItemHandler(object sender, MeasureItemEventArgs e)
		{
			e.ItemHeight= 22;
		}

	}
}
