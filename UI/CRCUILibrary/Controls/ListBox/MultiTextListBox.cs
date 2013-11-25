using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace CRC.Controls
{
    /// <summary>
    /// 支持多行文本显示的ListBox.
    /// <para>但不支持编辑.</para>
    /// <para>如果支持编辑,请使用MultiLineListBox.</para>
    /// </summary>
    [ToolboxBitmap(typeof (ListBox ))]
    public class MultiTextListBox:ListBox 
    {
        private int padWidth = 20;
        private int padHeigth = 10;

        public MultiTextListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
        }


        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
           
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                //设置文本绘制的方式.
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Near;
                sf.FormatFlags = StringFormatFlags.FitBlackBox;

                // 测量文本的区域.
                string text = Items[e.Index].ToString();
                SizeF size=e.Graphics.MeasureString(text,Font ,Width-padWidth ,sf);
                //重新调整文本的子项的区域大小.
                int h = e.Index == 0 ? padHeigth*2 + 5 : padHeigth*2;
                e.ItemHeight = (int)size.Height + h;
                e.ItemWidth = Width;

            }
            //base.OnMeasureItem(e);
        }

        /// <summary>
        /// 绘制子项.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                string text = Items[e.Index].ToString();
                Graphics g = e.Graphics;
                Rectangle rect = GetBounds(e.Bounds);
                if ((e.State & DrawItemState.Focus) == 0)
                {
                    //子项处于焦点状态 
                    g.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                    g.DrawString(text, Font, new SolidBrush(SystemColors.WindowText), rect);
                    g.DrawRectangle(new Pen(SystemColors.Highlight), e.Bounds);
                }
                else
                {
                    //子项处于非焦点状态.
                    g.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                    g.DrawString(text, Font, new SolidBrush(SystemColors.HighlightText), rect);
                }
            }
            
            //base.OnDrawItem(e);
        }

        /// <summary>
        /// 计算绘制文本的区域.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Rectangle GetBounds(Rectangle rect)
        {
           
            rect.X += padWidth / 2;
            rect.Width -= padWidth/2;
            rect.Y += padHeigth / 2;
            rect.Height -= padHeigth/2;
            return rect;
        }


    }
}
