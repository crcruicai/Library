/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/9 14:56:26
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace CRC.Controls
{
    /// <summary>
    /// 多行文本框,支持字符截断,并追加省略号.
    /// 支持背景透明颜色.
    /// </summary>
    public class MultiLineLabel : Control
    {
        /// <summary>
        /// 多行文本框.
        /// </summary>
        public MultiLineLabel()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            PaintText(e.Graphics);
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.CadetBlue, ButtonBorderStyle.Solid);
            base.OnPaint(e);
        }

        /// <summary>
        /// 绘制文本.
        /// </summary>
        /// <param name="g"></param>
        private void PaintText(Graphics g)
        {
            //设置文本绘制的方式.
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Near;
            sf.FormatFlags = StringFormatFlags.NoClip;
            sf.Alignment = StringAlignment.Near;
            sf.Trimming = StringTrimming.EllipsisCharacter;

            SolidBrush sb = new SolidBrush(ForeColor);
            Rectangle rect = GetTextRect();

            //测量文本.
            //SizeF size = g.MeasureString(Text, this.Font, rect.Width, sf);
            //Debug.WriteLine("size {0} rect{1}".FormatWith(size, rect));
            //Debug.WriteLine("Font size{0},SizeInPoints {1}", this.Font.Size, this.Font.SizeInPoints);
            //Debug.WriteLine("Line height {0}", this.Font.Height);
            string text = Text;
            //if (size.Height >= rect.Height)
            //{
            //    float  w = this.Font.Size;
            //    float h=this.Font .Height ;
            //    int len = (int)(rect.Width*rect.Height /w/h);
            //    Debug.WriteLine("Font size {0} Count {1} Text {2}", w,len ,Text.Length );

            //    if(Text.Length >len-4)
            //    text = Text.Substring(len-4)+"....";
            //    else
            //    {
            //        text = Text;
            //    }

            //}
            //else
            //{
            //    text = Text;
            //}

            g.DrawString(text, this.Font, sb, rect, sf);
        }

        /// <summary>
        /// 计算可以绘制的文本区域.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetTextRect()
        {
            Rectangle rect = this.ClientRectangle;
            rect.Inflate(-4, -4);
            return rect;
        }





    }
    


}
