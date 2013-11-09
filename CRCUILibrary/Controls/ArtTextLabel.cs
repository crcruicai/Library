using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace CRC.Controls
{
    public enum ArtTextStyle
    {
        /// <summary>
        /// 无.
        /// </summary>
        None,
        /// <summary>
        /// 边框
        /// </summary>
        Border,
        /// <summary>
        /// 浮雕(正阴影)
        /// </summary>
        Relievo,
        /// <summary>
        /// 左斜(阴影)
        /// </summary>
        Forme
    }

    /// <summary>
    /// 带艺术字效果的Lable.
    /// <para>可以通过以下属性设置ArtTextStyle和BorderSize,BorderColor调整样式.</para>
    /// </summary>
    public class ArtTextLabel : Label
    {
        private ArtTextStyle _artTextStyle = ArtTextStyle.Border;
        private int _borderSize = 1;
        private Color _borderColor = Color.White;
        /// <summary>
        /// 艺术字的样式.
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(typeof(ArtTextStyle), "1")]
        public ArtTextStyle ArtTextStyle
        {
            get
            {
                return this._artTextStyle;
            }
            set
            {
                if (this._artTextStyle != value)
                {
                    this._artTextStyle = value;
                    base.Invalidate();
                }
            }
        }

        /// <summary>
        /// 描边线条大小.
        /// </summary>
        [Browsable(true), Category("Appearance"), DefaultValue(1)]
        public int BorderSize
        {
            get
            {
                return this._borderSize;
            }
            set
            {
                if (this._borderSize != value)
                {
                    this._borderSize = value;
                    base.Invalidate();
                }
            }
        }
        [Browsable(true), Category("Appearance"), DefaultValue(typeof(Color), "White")]
        public Color BorderColor
        {
            get
            {
                return this._borderColor;
            }
            set
            {
                if (this._borderColor != value)
                {
                    this._borderColor = value;
                    base.Invalidate();
                }
            }
        }
        public ArtTextLabel()
        {
            this.SetStyles();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.ArtTextStyle == ArtTextStyle.None)
            {
                base.OnPaint(e);
            }
            else
            {
                if (base.Text.Length != 0)
                {
                    this.RenderText(e.Graphics);
                }
            }
        }
        private void RenderText(Graphics g)
        {
            using (new TextRenderingHintGraphics(g))
            {
                PointF point = this.CalculateRenderTextStartPoint(g);
                switch (this._artTextStyle)
                {
                    case ArtTextStyle.Border:
                        this.RenderBordText(g, point);
                        break;
                    case ArtTextStyle.Relievo:
                        this.RenderRelievoText(g, point);
                        break;
                    case ArtTextStyle.Forme:
                        this.RenderFormeText(g, point);
                        break;
                }
            }
        }
        private void RenderFormeText(Graphics g, PointF point)
        {
            using (Brush brush = new SolidBrush(this._borderColor))
            {
                for (int i = 1; i <= this._borderSize; i++)
                {
                    g.DrawString(base.Text, base.Font, brush, point.X - (float)i, point.Y + (float)i);
                }
            }
            using (Brush brush = new SolidBrush(base.ForeColor))
            {
                g.DrawString(base.Text, base.Font, brush, point);
            }
        }
        private void RenderRelievoText(Graphics g, PointF point)
        {
            using (Brush brush = new SolidBrush(this._borderColor))
            {
                for (int i = 1; i <= this._borderSize; i++)
                {
                    g.DrawString(base.Text, base.Font, brush, point.X + (float)i, point.Y);
                    g.DrawString(base.Text, base.Font, brush, point.X, point.Y + (float)i);
                }
            }
            using (Brush brush = new SolidBrush(base.ForeColor))
            {
                g.DrawString(base.Text, base.Font, brush, point);
            }
        }
        private void RenderBordText(Graphics g, PointF point)
        {
            using (Brush brush = new SolidBrush(this._borderColor))
            {
                for (int i = 1; i <= this._borderSize; i++)
                {
                    g.DrawString(base.Text, base.Font, brush, point.X - (float)i, point.Y);
                    g.DrawString(base.Text, base.Font, brush, point.X, point.Y - (float)i);
                    g.DrawString(base.Text, base.Font, brush, point.X + (float)i, point.Y);
                    g.DrawString(base.Text, base.Font, brush, point.X, point.Y + (float)i);
                }
            }
            using (Brush brush = new SolidBrush(base.ForeColor))
            {
                g.DrawString(base.Text, base.Font, brush, point);
            }
        }
        private PointF CalculateRenderTextStartPoint(Graphics g)
        {
            PointF empty = PointF.Empty;
            SizeF sizeF = g.MeasureString(base.Text, base.Font, PointF.Empty, StringFormat.GenericTypographic);
            if (this.AutoSize)
            {
                empty.X = (float)base.Padding.Left;
                empty.Y = (float)base.Padding.Top;
            }
            else
            {
                ContentAlignment textAlign = base.TextAlign;
                if (textAlign == ContentAlignment.TopLeft || textAlign == ContentAlignment.MiddleLeft || textAlign == ContentAlignment.BottomLeft)
                {
                    empty.X = (float)base.Padding.Left;
                }
                else
                {
                    if (textAlign == ContentAlignment.TopCenter || textAlign == ContentAlignment.MiddleCenter || textAlign == ContentAlignment.BottomCenter)
                    {
                        empty.X = ((float)base.Width - sizeF.Width) / 2f;
                    }
                    else
                    {
                        empty.X = (float)base.Width - ((float)base.Padding.Right + sizeF.Width);
                    }
                }
                if (textAlign == ContentAlignment.TopLeft || textAlign == ContentAlignment.TopCenter || textAlign == ContentAlignment.TopRight)
                {
                    empty.Y = (float)base.Padding.Top;
                }
                else
                {
                    if (textAlign == ContentAlignment.MiddleLeft || textAlign == ContentAlignment.MiddleCenter || textAlign == ContentAlignment.MiddleRight)
                    {
                        empty.Y = ((float)base.Height - sizeF.Height) / 2f;
                    }
                    else
                    {
                        empty.Y = (float)base.Height - ((float)base.Padding.Bottom + sizeF.Height);
                    }
                }
            }
            return empty;
        }
        private void SetStyles()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            base.UpdateStyles();
        }
    }
}
