/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/23 22:42:11
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRC.Controls;
using System.Net;
using System.Drawing.Drawing2D;
namespace UILibrary
{
    public partial class FrmControlPaintExTest : Form
    {
        public FrmControlPaintExTest()
        {
            InitializeComponent();
         

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            DrawCheckedFlagTest(g);
            DrawScrollBarArrawTest(g);
            DrawScrollBarSizerTest(g);
            CreateTrackBarThumbPathTest(g);
            RenderButton(g);
            base.OnPaint(e);
        }

      


        private void DrawCheckedFlagTest(Graphics g)
        {
           
            Rectangle rect = new Rectangle(50, 50, 20, 20);
            ControlPaintEx.DrawCheckedFlag(g, rect, Color.Black);

        }

        private void DrawScrollBarArrawTest(Graphics g)
        {
           
            Rectangle rect = new Rectangle(100, 50, 20, 20);
            ControlPaintEx.DrawScrollBarArraw(g, rect, Color.Black, Color.Bisque, Color.BlueViolet,
                Color.CadetBlue, Color.Chartreuse, Orientation.Vertical, ArrowDirection.Down, true);
        }

        private void DrawScrollBarSizerTest(Graphics g)
        {
            Rectangle rect = new Rectangle(100, 70, 20, 50);
            ControlPaintEx.DrawScrollBarSizer(g, rect, Color.Black, Color.Bisque);
        }

        private void CreateTrackBarThumbPathTest(Graphics g)
        {
            Rectangle rect = new Rectangle(100, 200, 20, 50);
            GraphicsPath path=GraphicsPathHelper.CreateTrackBarThumbPath(rect, ThumbArrowDirection.Up);

            g.FillPath(Brushes.Black, path);

            rect = new Rectangle(50, 150, 50, 50);
            path = GraphicsPathHelper.CreateFilletRectangle (rect, 5, RoundStyle.Top, true);
            g.FillPath(Brushes.Blue , path);
          
            
            rect = new Rectangle(250, 100, 100, 50);
            g.DrawRectangle(new Pen(Color.Red), rect);
            DrawText(g, rect);



        }

        private void DrawText(Graphics g,Rectangle textRect)
        {
            RectangleF newTextRect;
            StringFormat sf;

            //g.TranslateTransform(textRect.Right, textRect.Y );
            //g.RotateTransform(90F);

            sf = new StringFormat(StringFormatFlags.DirectionVertical);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.Character;

            newTextRect = textRect;
            //newTextRect.X = 0;
            //newTextRect.Y = 0;
            //newTextRect.Width = textRect.Height;
            //newTextRect.Height = textRect.Width;

            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawString("测试文本", Font, brush, newTextRect, sf);

            }
            g.ResetTransform();
        }

        private void VerticalFilp(Graphics gr, RectangleF rect)
        {
            PointF[] PArr = new PointF[3];
            PArr[0] = new PointF(rect.Left, rect.Top + rect.Height);
            PArr[1] = new PointF(rect.Left + rect.Width, rect.Top + rect.Height);
            PArr[2] = new PointF(rect.Left, rect.Top);
            gr.Transform = new Matrix(rect, PArr);
        }


        private void RenderButton(Graphics g)
        {
            Color baseColor = Color.FromArgb(51, 161, 200);
            Color innerBorderColor = Color.FromArgb(200, 255, 255, 255);
            Rectangle rect = new Rectangle(35, 200, 50, 30);

            RenderHelper.RenderBackgroundInternal(g, rect, baseColor,baseColor,innerBorderColor, RoundStyle.All  , 10, 0.35f, true, true, LinearGradientMode.Vertical);


        }


    }
}
