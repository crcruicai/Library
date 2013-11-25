using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CRC.Controls
{
    /// <summary>
    /// 信号发生器
    /// </summary>
    public class SignalGeneratorPanel : Panel
    {


        /// <summary>
        /// 绘制信号.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="st"></param>
        /// <param name="c"></param>
        private void PaintSignal(Graphics g, Rectangle r, SignalType st, Color c)
        {
            int X1 = 0;
            int Y1 = 0;

            int X2 = X1 + r.Width;
            int Y2 = Y1 + r.Height;

            int Xw = r.Width;
            int Yh = r.Height;

            int Xm = X1 + Xw / 2; 	// X possition of middle point.
            int Ym = Y1 + Yh / 2;		// Y possition of middle point.

            int Nper = 3; 		// Number of periodes that schud be shown.
            int Xpw = Xw / Nper; 	// One Periode length in pixel.
            int Yah = 3 * Yh / 8;	// Signal amplitude height in picel. 
            g.SmoothingMode = SmoothingMode.HighQuality;
            // Create a custom pen:
            Pen myPen = new Pen(Color.LightGray);
            myPen.DashStyle = DashStyle.Dot;

            //Draw vertical grid lines:
            for (int i = 1; i < 2 * Nper; i++)
                g.DrawLine(myPen, X1 + (Xpw / 2) * i, Y1, X1 + (Xpw / 2) * i, Y2);

            //Draw horisontal grid lines:
            g.DrawLine(myPen, X1, Ym - Yah, X2, Ym - Yah);
            g.DrawLine(myPen, X1, Ym, X2, Ym);
            g.DrawLine(myPen, X1, Ym + Yah, X2, Ym + Yah);

            // Create requider signal generator: 
            SignalGenerator sg = new SignalGenerator(st);

            // Adjust aignal generator:
            sg.Frequency = 1f / Xpw;
            sg.Phase = 0f;
            sg.Amplitude = Yah;
            sg.Offset = 0f;
            sg.Invert = false;

            // Generate signal and draw it:
            float Xold = 0f;
            float Yold = 0f;
            float Xnew = 0f;
            float Ynew = 0f;
            myPen.Color = c;
            myPen.DashStyle = DashStyle.Solid;

            myPen.Width = 2;
            for (int i = 0; i < Xw; i++)
            {
                Xnew = i;
                Ynew = (float)sg.GetValue(i); // NOTE: Only for debug, not for release configuration!
                if (i > 0) g.DrawLine(myPen, X1 + Xold, Ym - Yold, X1 + Xnew, Ym - Ynew);
                Xold = Xnew; Yold = Ynew;
            }

            // Draw the name of signal form:
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            g.DrawString(st.ToString(),
                         new Font("Times", 8, FontStyle.Bold),
                         new SolidBrush(Color.Black),
                         X2 - Xpw / 4, 10, format);

            // Draw border rectangle:
            g.DrawRectangle(new Pen(Color.Tomato, 2), X1 + 1, Y1 + 1, Xw - 2, Yh - 2);
        }

        public float UserDefinedSignal(float Time)
        {
            return (float)Math.Sin(2f * Math.PI * Time);
        }

        private Color _LineColor = Color.Red;
        /// <summary>
        /// 绘制线条的颜色.
        /// </summary>
        public Color LineColor
        {
            get
            {
                return _LineColor;
            }
            set
            {
                _LineColor = value;
            }
        }


        private SignalType _PaintSiganlType = SignalType.Sine;
        /// <summary>
        /// 
        /// </summary>
        public SignalType PaintSiganlType
        {
            get
            {
                return _PaintSiganlType;
            }
            set
            {
                _PaintSiganlType = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintSignal(e.Graphics, Bounds, _PaintSiganlType, _LineColor);

        }

    }
}
