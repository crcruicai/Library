using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    /// 颜色标签.
    /// </summary>
    public class ColorLabel : Control
    {
        #region Fields
        /// <summary>
        /// 边框的颜色.
        /// </summary>
        private Color _BorderColor = Color.FromArgb(65, 173, 236);

        #endregion

        #region Constructors

        /// <summary>
        /// 颜色标签.
        /// </summary>
        public ColorLabel()
            : base()
        {
            SetStyles();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置边框的颜色.
        /// </summary>
        [DefaultValue(typeof(Color),"65, 173, 236")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set 
            {
                _BorderColor = value;
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取默认设置的Size
        /// </summary>
        protected override Size DefaultSize
        {
            get { return new Size(16, 16); }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 设置控件的绘制样式
        /// </summary>
        private void SetStyles()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw, true);
            base.UpdateStyles();
        }

        #endregion

        #region OverideMethods

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Rectangle rect = ClientRectangle;
            using (SolidBrush brush = new SolidBrush(base.BackColor))
            {
                g.FillRectangle(brush,rect);
            }

            ControlPaint.DrawBorder(g,rect,_BorderColor,ButtonBorderStyle.Solid);
            rect.Inflate(-1, -1);
            ControlPaint.DrawBorder(g,rect,Color.White,ButtonBorderStyle.Solid);
                
        }

        #endregion
    }
}
