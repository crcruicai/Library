using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-08-25
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    /// 选项卡能任意改变的TabControl
    /// </summary>
    public class TabControlEx : TabControl
    {
        #region Fields

        private UpDownButtonNativeWindow _upDownButtonNativeWindow;
        private Color _baseColor = Color.FromArgb(166, 222, 255);
        private Color _backColor = Color.FromArgb(234, 247, 254);
        private Color _borderColor = Color.FromArgb(23, 169, 254);
        private Color _arrowColor = Color.FromArgb(0, 79, 125);

        private const string UpDownButtonClassName = "msctls_updown32";
        private static readonly int Radius = 8;
        private static readonly object EventPaintUpDownButton = new object();

        #endregion

        #region Constructors

        public TabControlEx()
            : base()
        {
            SetStyles();
        }

        #endregion

        #region Events

        public event UpDownButtonPaintEventHandler PaintUpDownButton
        {
            add { base.Events.AddHandler(EventPaintUpDownButton, value); }
            remove { base.Events.RemoveHandler(EventPaintUpDownButton, value); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// 选项卡基颜色.
        /// </summary>
        [DefaultValue(typeof(Color), "166, 222, 255")]
        public Color BaseColor
        {
            get { return _baseColor; }
            set
            {
                _baseColor = value;
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// 选项卡背景颜色.
        /// </summary>
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(typeof(Color), "234, 247, 254")]
        public override Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// 选项卡边框颜色
        /// </summary>
        [DefaultValue(typeof(Color), "23, 169, 254")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                base.Invalidate(true);
            }
        }

        /// <summary>
        /// 箭头颜色.
        /// </summary>
        [DefaultValue(typeof(Color), "0, 95, 152")]
        public Color ArrowColor
        {
            get { return _arrowColor; }
            set
            {
                _arrowColor = value;
                base.Invalidate(true);
            }
        }

        internal IntPtr UpDownButtonHandle
        {
            get { return FindUpDownButton(); }
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPaintUpDownButton(
            UpDownButtonPaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.ClipRectangle;

            Color upButtonBaseColor = _baseColor;
            Color upButtonBorderColor = _borderColor;
            Color upButtonArrowColor = _arrowColor;

            Color downButtonBaseColor = _baseColor;
            Color downButtonBorderColor = _borderColor;
            Color downButtonArrowColor = _arrowColor;

            Rectangle upButtonRect = rect;
            upButtonRect.X += 4;
            upButtonRect.Y += 4;
            upButtonRect.Width = rect.Width / 2 - 8;
            upButtonRect.Height -= 8;

            Rectangle downButtonRect = rect;
            downButtonRect.X = upButtonRect.Right + 2;
            downButtonRect.Y += 4;
            downButtonRect.Width = rect.Width / 2 - 8;
            downButtonRect.Height -= 8;

            if (Enabled)
            {
                if (e.MouseOver)
                {
                    if (e.MousePress)
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(_baseColor, 0, -35, -24, -9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(_baseColor, 0, -35, -24, -9);
                        }
                    }
                    else
                    {
                        if (e.MouseInUpButton)
                        {
                            upButtonBaseColor = GetColor(_baseColor, 0, 35, 24, 9);
                        }
                        else
                        {
                            downButtonBaseColor = GetColor(_baseColor, 0, 35, 24, 9);
                        }
                    }
                }
            }
            else
            {
                upButtonBaseColor = SystemColors.Control;
                upButtonBorderColor = SystemColors.ControlDark;
                upButtonArrowColor = SystemColors.ControlDark;

                downButtonBaseColor = SystemColors.Control;
                downButtonBorderColor = SystemColors.ControlDark;
                downButtonArrowColor = SystemColors.ControlDark;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            Color backColor = Enabled ? _backColor : SystemColors.Control;

            using (SolidBrush brush = new SolidBrush(_backColor))
            {
                rect.Inflate(1, 1);
                g.FillRectangle(brush, rect);
            }

            RenderButton(
                g,
                upButtonRect,
                upButtonBaseColor,
                upButtonBorderColor,
                upButtonArrowColor,
                ArrowDirection.Left);
            RenderButton(
                g,
                downButtonRect,
                downButtonBaseColor,
                downButtonBorderColor,
                downButtonArrowColor,
                ArrowDirection.Right);

            UpDownButtonPaintEventHandler handler =
                base.Events[EventPaintUpDownButton] as UpDownButtonPaintEventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            base.Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            base.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawTabContrl(e.Graphics);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (_upDownButtonNativeWindow != null)
            {
                _upDownButtonNativeWindow.Dispose();
                _upDownButtonNativeWindow = null;
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (UpDownButtonHandle != IntPtr.Zero)
            {
                if (_upDownButtonNativeWindow == null)
                {
                    _upDownButtonNativeWindow = new UpDownButtonNativeWindow(this);
                }
            }
        }

        #endregion

        #region Help Methods

        private IntPtr FindUpDownButton()
        {
            return NativeMethods.FindWindowEx(
                base.Handle,
                IntPtr.Zero,
                UpDownButtonClassName,
                null);
        }

        private void SetStyles()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        /// <param name="g"></param>
        private void DrawTabContrl(Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            DrawDrawBackgroundAndHeader(g);
            DrawTabPages(g);
            DrawBorder(g);
        }

        /// <summary>
        /// 绘制控件的边框和头部.
        /// </summary>
        /// <param name="g"></param>
        private void DrawDrawBackgroundAndHeader(Graphics g)
        {
            int x = 0;
            int y = 0;
            int width = 0;
            int height = 0;
            //计算渲染区域.
            switch (Alignment)
            {
                case TabAlignment.Top:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Bottom:
                    x = 0;
                    y = DisplayRectangle.Height;
                    width = ClientRectangle.Width;
                    height = ClientRectangle.Height - DisplayRectangle.Height;
                    break;
                case TabAlignment.Left:
                    x = 0;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
                case TabAlignment.Right:
                    x = DisplayRectangle.Width;
                    y = 0;
                    width = ClientRectangle.Width - DisplayRectangle.Width;
                    height = ClientRectangle.Height;
                    break;
            }

            Rectangle headerRect = new Rectangle(x, y, width, height);
            Color backColor = Enabled ? _backColor : SystemColors.Control;
            using (SolidBrush brush = new SolidBrush(backColor))
            {
                //渲染页区域.
                g.FillRectangle(brush, ClientRectangle);
                //渲染头部
                g.FillRectangle(brush, headerRect);
            }
        }

        /// <summary>
        /// 绘制TabPages
        /// </summary>
        /// <param name="g"></param>
        private void DrawTabPages(Graphics g)
        {
            Rectangle tabRect;
            Point cusorPoint = PointToClient(MousePosition);
            bool hover;
            bool selected;
            bool hasSetClip = false;
            bool alignHorizontal =
                (Alignment == TabAlignment.Top || 
                Alignment == TabAlignment.Bottom);
            LinearGradientMode mode = alignHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            //头是否在水平方向.
            if (alignHorizontal)
            {
                IntPtr upDownButtonHandle = UpDownButtonHandle;
                bool hasUpDown = upDownButtonHandle != IntPtr.Zero;
                if (hasUpDown)
                {
                    //该页是否可见.
                    if (NativeMethods.IsWindowVisible(upDownButtonHandle))
                    {
                        NativeMethods.RECT upDownButtonRect = new NativeMethods.RECT();
                        NativeMethods.GetWindowRect(
                            upDownButtonHandle, ref upDownButtonRect);
                        Rectangle upDownRect = Rectangle.FromLTRB(
                            upDownButtonRect.Left,
                            upDownButtonRect.Top,
                            upDownButtonRect.Right,
                            upDownButtonRect.Bottom);
                        upDownRect = RectangleToClient(upDownRect);

                        switch (Alignment)
                        {
                            case TabAlignment.Top:
                                upDownRect.Y = 0;
                                break;
                            case TabAlignment.Bottom:
                                upDownRect.Y = 
                                    ClientRectangle.Height - DisplayRectangle.Height;
                                break;
                        }
                        upDownRect.Height = ClientRectangle.Height;
                        g.SetClip(upDownRect, CombineMode.Exclude);
                        hasSetClip = true;
                    }
                }
            }

            for(int index = 0; index <base.TabCount; index ++)
            {
                TabPage page = TabPages[index];

                tabRect = GetTabRect(index);
                hover = tabRect.Contains(cusorPoint);//检查鼠标是否停留
                selected = SelectedIndex == index;//检查是否被选中.

                Color baseColor = _baseColor;
                Color borderColor = _borderColor;
                //如果选项卡被选中.更改颜色.
                if (selected)
                {
                    baseColor = GetColor(_baseColor, 0, -45, -30, -14);
                }
                else if (hover)//鼠标停留时的颜色.
                {
                    baseColor = GetColor(_baseColor, 0, 35, 24, 9);
                }

                RenderTabBackgroundInternal(
                    g,
                    tabRect,
                    baseColor,
                    borderColor,
                    .45F,
                    mode);

                bool hasImage = DrawTabImage(g, page, tabRect);
                //绘制Text
                DrawtabText(g, page, tabRect, hasImage);
            }
            if (hasSetClip)
            {
                g.ResetClip();
            }
        }

        /// <summary>
        /// 绘制TabText
        /// </summary>
        /// <param name="g"></param>
        /// <param name="page"></param>
        /// <param name="tabRect"></param>
        /// <param name="hasImage"></param>
        private void DrawtabText(
            Graphics g, TabPage page, Rectangle tabRect, bool hasImage)
        {
            Rectangle textRect = tabRect;
            RectangleF newTextRect;
            StringFormat sf;

            //绘制文本.
            switch (Alignment)
            {
                case TabAlignment.Top:
                case TabAlignment.Bottom:
                    if (hasImage)//如果有图片
                    {
                        textRect.X = tabRect.X + Radius / 2 + tabRect.Height - 2;
                        textRect.Width = tabRect.Width - Radius - tabRect.Height;
                    }
                    TextRenderer.DrawText(g,page.Text,page.Font,textRect,page.ForeColor);
                    break;
                case TabAlignment.Left:
                    if (hasImage)
                    {
                        textRect.Height = tabRect.Height - tabRect.Width + 2;
                    }
                    g.TranslateTransform(textRect.X, textRect.Bottom);
                    g.RotateTransform(270F);
                    
                    sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(page.Text,page.Font,brush,newTextRect,sf);
                        
                    }
                    g.ResetTransform();
                    
                    break;
                case TabAlignment.Right:
                    if (hasImage)
                    {
                        textRect.Y = tabRect.Y + Radius / 2 + tabRect.Width - 2;
                        textRect.Height = tabRect.Height - Radius - tabRect.Width;
                    }
                    g.TranslateTransform(textRect.Right, textRect.Y);
                    g.RotateTransform(90F);
                    sf = new StringFormat(StringFormatFlags.DirectionRightToLeft);
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Trimming = StringTrimming.Character;
                    newTextRect = textRect;
                    newTextRect.X = 0;
                    newTextRect.Y = 0;
                    newTextRect.Width = textRect.Height;
                    newTextRect.Height = textRect.Width;
                    using (Brush brush = new SolidBrush(page.ForeColor))
                    {
                        g.DrawString(page.Text,page.Font,brush,newTextRect,sf);
                    }
                    g.ResetTransform();
                    break;
            }
        }

        private void VerticalFilp(Graphics gr, RectangleF rect)
        {
            PointF[] PArr = new PointF[3];
            PArr[0] = new PointF(rect.Left, rect.Top + rect.Height);
            PArr[1] = new PointF(rect.Left + rect.Width, rect.Top + rect.Height);
            PArr[2] = new PointF(rect.Left, rect.Top);
            gr.Transform = new Matrix(rect, PArr);
        }
        /// <summary>
        /// 绘制边框.
        /// </summary>
        /// <param name="g"></param>
        private void DrawBorder(Graphics g)
        {
            if (SelectedIndex != -1)
            {
                Rectangle tabRect = GetTabRect(SelectedIndex);
                Rectangle clipRect = ClientRectangle;
                Point[] points = new Point[6];

                IntPtr upDownButtonHandle = UpDownButtonHandle;
                bool hasUpDown = upDownButtonHandle != IntPtr.Zero;
                if (hasUpDown)
                {
                    if (NativeMethods.IsWindowVisible(upDownButtonHandle))
                    {
                        NativeMethods.RECT upDownButtonRect = new NativeMethods.RECT();
                        NativeMethods.GetWindowRect(
                            upDownButtonHandle,
                            ref upDownButtonRect);
                        Rectangle upDownRect = Rectangle.FromLTRB(
                            upDownButtonRect.Left,
                            upDownButtonRect.Top,
                            upDownButtonRect.Right,
                            upDownButtonRect.Bottom);
                        upDownRect = RectangleToClient(upDownRect);

                        tabRect.X = tabRect.X > upDownRect.X ?
                            upDownRect.X : tabRect.X;
                        tabRect.Width = tabRect.Right > upDownRect.X ?
                            upDownRect.X - tabRect.X : tabRect.Width;
                    }
                }

                switch (Alignment)
                {
                    case TabAlignment.Top:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Bottom);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Bottom);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Bottom:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            clipRect.X,
                            tabRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[4] = new Point(
                            clipRect.Right - 1,
                            tabRect.Y);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        break;
                    case TabAlignment.Left:
                        points[0] = new Point(
                            tabRect.Right,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.Right,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.Right - 1,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.Right - 1,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.Right,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.Right,
                            tabRect.Bottom);
                        break;
                    case TabAlignment.Right:
                        points[0] = new Point(
                            tabRect.X,
                            tabRect.Y);
                        points[1] = new Point(
                            tabRect.X,
                            clipRect.Y);
                        points[2] = new Point(
                            clipRect.X,
                            clipRect.Y);
                        points[3] = new Point(
                            clipRect.X,
                            clipRect.Bottom - 1);
                        points[4] = new Point(
                            tabRect.X,
                            clipRect.Bottom - 1);
                        points[5] = new Point(
                            tabRect.X,
                            tabRect.Bottom);
                        break;
                }
                using (Pen pen = new Pen(_borderColor))
                {
                    g.DrawLines(pen, points);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dropDownRect"></param>
        /// <param name="direction"></param>
        /// <param name="brush"></param>
        internal void RenderArrowInternal(
             Graphics g,
             Rectangle dropDownRect,
             ArrowDirection direction,
             Brush brush)
        {
            Point point = new Point(
                dropDownRect.Left + (dropDownRect.Width / 2),
                dropDownRect.Top + (dropDownRect.Height / 2));
            Point[] points = null;
            switch (direction)
            {
                case ArrowDirection.Left:
                    points = new Point[] { 
                        new Point(point.X + 1, point.Y - 4), 
                        new Point(point.X + 1, point.Y + 4), 
                        new Point(point.X - 2, point.Y) };
                    break;

                case ArrowDirection.Up:
                    points = new Point[] { 
                        new Point(point.X - 3, point.Y + 1), 
                        new Point(point.X + 3, point.Y + 1), 
                        new Point(point.X, point.Y - 1) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 1, point.Y - 4), 
                        new Point(point.X - 1, point.Y + 4), 
                        new Point(point.X + 2, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 3, point.Y - 1), 
                        new Point(point.X + 3, point.Y - 1), 
                        new Point(point.X, point.Y + 1) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        internal void RenderButton(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color borderColor,
            Color arrowColor,
            ArrowDirection direction)
        {
            RenderBackgroundInternal(
                g,
                rect,
                baseColor,
                borderColor,
                0.45f,
                true,
                LinearGradientMode.Vertical);
            using (SolidBrush brush = new SolidBrush(arrowColor))
            {
                RenderArrowInternal(
                    g,
                    rect,
                    direction,
                    brush);
            }
        }

        internal void RenderBackgroundInternal(Graphics g,Rectangle rect,Color baseColor,Color borderColor,float basePosition,bool drawBorder,LinearGradientMode mode)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
               rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                ColorBlend blend = new ColorBlend();
                blend.Positions =
                    new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                g.FillRectangle(brush, rect);
            }
            if (baseColor.A > 80)
            {
                Rectangle rectTop = rect;
                if (mode == LinearGradientMode.Vertical)
                {
                    rectTop.Height = (int)(rectTop.Height * basePosition);
                }
                else
                {
                    rectTop.Width = (int)(rect.Width * basePosition);
                }
                using (SolidBrush brushAlpha =
                    new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                {
                    g.FillRectangle(brushAlpha, rectTop);
                }
            }

            if (drawBorder)
            {
                using (Pen pen = new Pen(borderColor))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="baseColor"></param>
        /// <param name="borderColor"></param>
        /// <param name="basePosition"></param>
        /// <param name="mode"></param>
        internal void RenderTabBackgroundInternal(
          Graphics g,
          Rectangle rect,
          Color baseColor,
          Color borderColor,
          float basePosition,
          LinearGradientMode mode)
        {
            using (GraphicsPath path = CreateTabPath(rect))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                   rect, Color.Transparent, Color.Transparent, mode))
                {
                    Color[] colors = new Color[4];
                    colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                    colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                    colors[2] = baseColor;
                    colors[3] = GetColor(baseColor, 0, 68, 69, 54);

                    ColorBlend blend = new ColorBlend();
                    blend.Positions =
                        new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                    blend.Colors = colors;
                    brush.InterpolationColors = blend;
                    g.FillPath(brush, path);
                }

                if (baseColor.A > 80)
                {
                    Rectangle rectTop = rect;
                    if (mode == LinearGradientMode.Vertical)
                    {
                        rectTop.Height = (int)(rectTop.Height * basePosition);
                    }
                    else
                    {
                        rectTop.Width = (int)(rect.Width * basePosition);
                    }
                    using (SolidBrush brushAlpha =
                        new SolidBrush(Color.FromArgb(80, 255, 255, 255)))
                    {
                        g.FillRectangle(brushAlpha, rectTop);
                    }
                }

                rect.Inflate(-1, -1);
                using (GraphicsPath path1 = CreateTabPath(rect))
                {
                    using (Pen pen = new Pen(Color.FromArgb(255, 255, 255)))
                    {
                        if (Multiline)
                        {
                            g.DrawPath(pen, path1);
                        }
                        else
                        {
                            g.DrawLines(pen, path1.PathPoints);
                        }
                    }
                }

                using (Pen pen = new Pen(borderColor))
                {
                    if (Multiline)
                    {
                        g.DrawPath(pen, path);
                    }
                    {
                        g.DrawLines(pen, path.PathPoints);
                    }
                }
            }
        }

        /// <summary>
        /// 绘制图片.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="page"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        private bool DrawTabImage(Graphics g, TabPage page, Rectangle rect)
        {
            bool hasImage = false;
            if (ImageList != null)
            {
                Image image = null;
                if (page.ImageIndex != -1)
                {
                    image = ImageList.Images[page.ImageIndex];
                }
                else if (page.ImageKey != null)
                {
                    image = ImageList.Images[page.ImageKey];
                }

                if (image != null)
                {
                    hasImage = true;
                    Rectangle destRect = Rectangle.Empty;
                    Rectangle srcRect = new Rectangle(Point.Empty, image.Size);
                    switch (Alignment)
                    {
                        case TabAlignment.Top:
                        case TabAlignment.Bottom:
                            destRect = new Rectangle(
                                 rect.X + Radius / 2 + 2,
                                 rect.Y + 2,
                                 rect.Height - 4,
                                 rect.Height - 4);
                            break;
                        case TabAlignment.Left:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Bottom - (rect.Width - 4) - Radius / 2 - 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                        case TabAlignment.Right:
                            destRect = new Rectangle(
                                rect.X + 2,
                                rect.Y + Radius / 2 + 2,
                                rect.Width - 4,
                                rect.Width - 4);
                            break;
                    }

                    g.DrawImage(
                        image,
                        destRect,
                        srcRect,
                        GraphicsUnit.Pixel);
                }
            }
            return hasImage;
        }
        /// <summary>
        /// 创建圆角矩形.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private GraphicsPath CreateTabPath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            switch (Alignment)
            {
                case TabAlignment.Top:
                    rect.X++;
                    rect.Width-=2;
                    path.AddLine(rect.X,rect.Bottom,rect.X,rect.Y + Radius / 2);
                    path.AddArc(rect.X ,rect.Y,Radius,Radius,180F,90F);
                    path.AddArc(rect.Right - Radius,rect.Y,Radius,Radius,270F,90F);
                    path.AddLine(rect.Right,rect.Y + Radius / 2,rect.Right,rect.Bottom);
                    break;
                case TabAlignment.Bottom:
                    rect.X++;
                    rect.Width-=2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.X,
                        rect.Bottom - Radius / 2);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        180,
                        -90);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        90,
                        -90);
                    path.AddLine(
                        rect.Right,
                        rect.Bottom - Radius / 2,
                        rect.Right,
                        rect.Y);

                    break;
                case TabAlignment.Left:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.Right,
                        rect.Y,
                        rect.X + Radius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.X,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        -90F);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        180F,
                        -90F);
                    path.AddLine(
                        rect.X + Radius / 2,
                        rect.Bottom,
                        rect.Right,
                        rect.Bottom);
                    break;
                case TabAlignment.Right:
                    rect.Y++;
                    rect.Height -= 2;
                    path.AddLine(
                        rect.X,
                        rect.Y,
                        rect.Right - Radius / 2,
                        rect.Y);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Y,
                        Radius,
                        Radius,
                        270F,
                        90F);
                    path.AddArc(
                        rect.Right - Radius,
                        rect.Bottom - Radius,
                        Radius,
                        Radius,
                        0F,
                        90F);
                    path.AddLine(
                        rect.Right - Radius / 2,
                        rect.Bottom,
                        rect.X,
                        rect.Bottom);
                    break;
            }
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// 获取颜色.
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private Color GetColor(Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(a + a0, 0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(r + r0, 0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(g + g0, 0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(b + b0, 0); }

            return Color.FromArgb(a, r, g, b);
        }

        #endregion

        #region UpDownButtonNativeWindow

        private class UpDownButtonNativeWindow : NativeWindow, IDisposable
        {
            private TabControlEx _owner;
            private bool _bPainting;

            public UpDownButtonNativeWindow(TabControlEx owner)
                : base()
            {
                _owner = owner;
                base.AssignHandle(owner.UpDownButtonHandle);
            }

            private bool LeftKeyPressed()
            {
                if (SystemInformation.MouseButtonsSwapped)
                {
                    return (NativeMethods.GetKeyState(NativeMethods.VK_RBUTTON) < 0);
                }
                else
                {
                    return (NativeMethods.GetKeyState(NativeMethods.VK_LBUTTON) < 0);
                }
            }

            private void DrawUpDownButton()
            {
                bool mouseOver = false;
                bool mousePress = LeftKeyPressed();
                bool mouseInUpButton = false;

                NativeMethods.RECT rect = new NativeMethods.RECT();

                NativeMethods.GetClientRect(base.Handle, ref rect);

                Rectangle clipRect = Rectangle.FromLTRB(
                    rect.Top, rect.Left, rect.Right, rect.Bottom);

                Point cursorPoint = new Point();
                NativeMethods.GetCursorPos(ref cursorPoint);
                NativeMethods.GetWindowRect(base.Handle, ref rect);

                mouseOver = NativeMethods.PtInRect(ref rect, cursorPoint);

                cursorPoint.X -= rect.Left;
                cursorPoint.Y -= rect.Top;

                mouseInUpButton = cursorPoint.X < clipRect.Width / 2;

                using (Graphics g = Graphics.FromHwnd(base.Handle))
                {
                    UpDownButtonPaintEventArgs e =
                        new UpDownButtonPaintEventArgs(
                        g,
                        clipRect,
                        mouseOver,
                        mousePress,
                        mouseInUpButton);
                    _owner.OnPaintUpDownButton(e);
                }
            }

            protected override void WndProc(ref Message m)
            {
                switch (m.Msg)
                {
                    case NativeMethods.WM_PAINT:
                        if (!_bPainting)
                        {
                            NativeMethods.PAINTSTRUCT ps = 
                                new NativeMethods.PAINTSTRUCT();
                            _bPainting = true;
                            NativeMethods.BeginPaint(m.HWnd, ref ps);
                            DrawUpDownButton();
                            NativeMethods.EndPaint(m.HWnd, ref ps);
                            _bPainting = false;
                            m.Result = NativeMethods.TRUE;
                        }
                        else
                        {
                            base.WndProc(ref m);
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }

            #region IDisposable 成员

            public void Dispose()
            {
                _owner = null;
                base.ReleaseHandle();
            }

            #endregion
        }

        #endregion
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void UpDownButtonPaintEventHandler(
        object sender,
        UpDownButtonPaintEventArgs e);

    /// <summary>
    /// 
    /// </summary>
    public class UpDownButtonPaintEventArgs : PaintEventArgs
    {
        private bool _mouseOver;
        private bool _mousePress;
        private bool _mouseInUpButton;

        public UpDownButtonPaintEventArgs(
            Graphics graphics,
            Rectangle clipRect,
            bool mouseOver,
            bool mousePress,
            bool mouseInUpButton)
            : base(graphics, clipRect)
        {
            _mouseOver = mouseOver;
            _mousePress = mousePress;
            _mouseInUpButton = mouseInUpButton;
        }

        public bool MouseOver
        {
            get { return _mouseOver; }
        }

        public bool MousePress
        {
            get { return _mousePress; }
        }

        public bool MouseInUpButton
        {
            get { return _mouseInUpButton; }
        }
    }


}
