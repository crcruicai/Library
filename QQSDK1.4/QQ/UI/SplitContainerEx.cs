using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWebQQ.UI
{

    /* 作者：Starts_2000
* 日期：2010-03-20
* 网站：http://www.csharpwin.com CS 程序员之窗。
* 你可以免费使用或修改以下代码，但请保留版权信息。
* 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
*/

    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.ComponentModel;
    using System.Drawing.Text;
    using System.Drawing.Imaging;

    #region SplitContainerEx

    /// <summary>
    /// 能折叠的SplitContainer
    /// </summary>
    public class SplitContainerEx : SplitContainer
    {
        private CollapsePanel _collapsePanel = CollapsePanel.Panel1;
        private SpliterPanelState _spliterPanelState = SpliterPanelState.Expanded;
        private ControlState _mouseState;
        private int _lastDistance;
        private int _minSize;
        private HistTest _histTest;
        private readonly object EventCollapseClick = new object();

        /// <summary>
        /// 构造一个能够折叠的SplitContainer
        /// </summary>
        public SplitContainerEx()
        {
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);
            _lastDistance = base.SplitterDistance;
        }

        /// <summary>
        /// 折叠时发生.
        /// </summary>
        public event EventHandler CollapseClick
        {
            add { base.Events.AddHandler(EventCollapseClick, value); }
            remove { base.Events.RemoveHandler(EventCollapseClick, value); }
        }

        /// <summary>
        /// 折叠的控件.
        /// </summary>
        [DefaultValue(typeof(CollapsePanel), "1")]
        public CollapsePanel CollapsePanel
        {
            get { return _collapsePanel; }
            set
            {
                if (_collapsePanel != value)
                {
                    Expand();
                    _collapsePanel = value;
                }
            }
        }

        /// <summary>
        /// 分隔栏的宽度.
        /// </summary>
        protected virtual int DefaultCollapseWidth
        {
            get { return 80; }
        }

        /// <summary>
        /// 箭头的宽度.
        /// </summary>
        protected virtual int DefaultArrowWidth
        {
            get { return 16; }
        }

        /// <summary>
        /// 获取折叠时,分隔栏的区域.
        /// </summary>
        protected Rectangle CollapseRect
        {
            get
            {
                if (_collapsePanel == CollapsePanel.None)
                {
                    return Rectangle.Empty;
                }

                Rectangle rect = base.SplitterRectangle; //获取拆分器的大小与位置.
                if (base.Orientation == Orientation.Horizontal)//水平方式.
                {
                    rect.X = (base.Width - DefaultCollapseWidth) / 2;
                    rect.Width = DefaultCollapseWidth;
                }
                else
                {
                    rect.Y = (base.Height - DefaultCollapseWidth) / 2;
                    rect.Height = DefaultCollapseWidth;
                }

                return rect;
            }
        }

        /// <summary>
        /// 获取分隔栏的状态.
        /// </summary>
        internal SpliterPanelState SpliterPanelState
        {
            get { return _spliterPanelState; }
            set
            {
                if (_spliterPanelState != value)
                {
                    switch (value)
                    {
                        case SpliterPanelState.Expanded:
                            Expand();
                            break;
                        case SpliterPanelState.Collapsed:
                            Collapse();
                            break;

                    }
                    _spliterPanelState = value;
                }
            }
        }

        /// <summary>
        /// 鼠标在控件上的状态.
        /// </summary>
        internal ControlState MouseState
        {
            get { return _mouseState; }
            set
            {
                if (_mouseState != value)
                {
                    _mouseState = value;
                    base.Invalidate(CollapseRect);
                }
            }
        }

        /// <summary>
        /// 折叠Panel
        /// </summary>
        public void Collapse()
        {
            if (_collapsePanel != CollapsePanel.None &&
                _spliterPanelState == SpliterPanelState.Expanded)//处于展开状态
            {
                _lastDistance = base.SplitterDistance;
                if (_collapsePanel == CollapsePanel.Panel1)
                {
                    _minSize = base.Panel1MinSize;
                    base.Panel1MinSize = 0;
                    base.SplitterDistance = 0;
                }
                else
                {
                    int width = base.Orientation == Orientation.Horizontal ?
                        base.Height : base.Width;
                    _minSize = base.Panel2MinSize;
                    base.Panel2MinSize = 0;
                    base.SplitterDistance = width - base.SplitterWidth - base.Padding.Vertical;
                }
                base.Invalidate(base.SplitterRectangle);
            }
        }

        /// <summary>
        /// 展开Panel
        /// </summary>
        public void Expand()
        {
            if (_collapsePanel != CollapsePanel.None &&
               _spliterPanelState == SpliterPanelState.Collapsed)
            {
                if (_collapsePanel == CollapsePanel.Panel1)
                {
                    base.Panel1MinSize = _minSize;
                }
                else
                {
                    base.Panel2MinSize = _minSize;
                }
                base.SplitterDistance = _lastDistance;
                base.Invalidate(base.SplitterRectangle);
            }
        }

        protected virtual void OnCollapseClick(EventArgs e)
        {
            if (_spliterPanelState == SpliterPanelState.Collapsed)
            {
                SpliterPanelState = SpliterPanelState.Expanded;
            }
            else
            {
                SpliterPanelState = SpliterPanelState.Collapsed;
            }

            EventHandler handler = base.Events[EventCollapseClick] as EventHandler;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //只要有一个处于折叠的状态,退出绘制.
            if (base.Panel1Collapsed || base.Panel2Collapsed)
            {
                return;
            }

            Graphics g = e.Graphics;
            Rectangle rect = base.SplitterRectangle;
            bool bHorizontal = base.Orientation == Orientation.Horizontal;
            //获取线性渲染模式.
            LinearGradientMode gradientMode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;
            //
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Color.FromArgb(206, 238, 255),
                Color.FromArgb(105, 200, 254), gradientMode))
            {
                Blend blend = new Blend();
                blend.Positions = new float[] { 0f, .5f, 1f };
                blend.Factors = new float[] { .5F, 1F, .5F };

                brush.Blend = blend;
                g.FillRectangle(brush, rect);
            }

            if (_collapsePanel == CollapsePanel.None)
            {
                return;
            }

            Rectangle arrowRect;
            Rectangle topLeftRect;
            Rectangle bottomRightRect;

            CalculateRect(
                CollapseRect,
                out arrowRect,
                out topLeftRect,
                out bottomRightRect);

            //绘制 箭头
            ArrowDirection direction = ArrowDirection.Left;

            switch (_collapsePanel)
            {
                case CollapsePanel.Panel1:
                    if (bHorizontal)
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Down : ArrowDirection.Up;
                    }
                    else
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Right : ArrowDirection.Left;
                    }
                    break;
                case CollapsePanel.Panel2:
                    if (bHorizontal)
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Up : ArrowDirection.Down;
                    }
                    else
                    {
                        direction =
                            _spliterPanelState == SpliterPanelState.Collapsed ?
                            ArrowDirection.Left : ArrowDirection.Right;
                    }
                    break;
            }

            //
            Color foreColor = _mouseState == ControlState.Hover ?
                Color.FromArgb(21, 66, 139) : Color.FromArgb(80, 136, 228);
            using (SmoothingModeGraphics sg = new SmoothingModeGraphics(g))
            {
                //绘制网格点.
                RenderHelper.RenderGrid(g, topLeftRect, new Size(3, 3), foreColor);
                RenderHelper.RenderGrid(g, bottomRightRect, new Size(3, 3), foreColor);

                using (Brush brush = new SolidBrush(foreColor))
                {
                    //渲染箭头.
                    RenderHelper.RenderArrowInternal(
                        g,
                        arrowRect,
                        direction,
                        brush);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            //如果鼠标的左键没有按下，重置HistTest
            if (e.Button != MouseButtons.Left)
            {
                _histTest = HistTest.None;
            }

            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            //鼠标在Button矩形里，并且不是在拖动
            if (collapseRect.Contains(mousePoint) &&
                _histTest != HistTest.Spliter)
            {
                base.Capture = false;
                SetCursor(Cursors.Hand);
                MouseState = ControlState.Hover;
                return;
            }//鼠标在分隔栏矩形里
            else if (base.SplitterRectangle.Contains(mousePoint))
            {
                MouseState = ControlState.Normal;

                //如果已经在按钮按下了鼠标或者已经收缩，就不允许拖动了
                if (_histTest == HistTest.Button ||
                    (_collapsePanel != CollapsePanel.None &&
                    _spliterPanelState == SpliterPanelState.Collapsed))
                {
                    base.Capture = false;
                    base.Cursor = Cursors.Default;
                    return;
                }

                //鼠标没有按下，设置Split光标
                if (_histTest == HistTest.None &&
                    !base.IsSplitterFixed)
                {
                    if (base.Orientation == Orientation.Horizontal)
                    {
                        SetCursor(Cursors.HSplit);
                    }
                    else
                    {
                        SetCursor(Cursors.VSplit);
                    }
                    return;
                }
            }

            MouseState = ControlState.Normal;

            //正在拖动分隔栏
            if (_histTest == HistTest.Spliter &&
                !base.IsSplitterFixed)
            {
                if (base.Orientation == Orientation.Horizontal)
                {
                    SetCursor(Cursors.HSplit);
                }
                else
                {
                    SetCursor(Cursors.VSplit);
                }
                base.OnMouseMove(e);
                return;
            }

            base.Cursor = Cursors.Default;
            base.OnMouseMove(e);
        }

        /// <summary>
        /// 鼠标离开的时候.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.Cursor = Cursors.Default;
            MouseState = ControlState.Normal;
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// 鼠标按下的时候.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            if (collapseRect.Contains(mousePoint) ||
                (_collapsePanel != CollapsePanel.None &&
                _spliterPanelState == SpliterPanelState.Collapsed))
            {
                _histTest = HistTest.Button;
                return;
            }

            if (base.SplitterRectangle.Contains(mousePoint))
            {
                _histTest = HistTest.Spliter;
            }

            base.OnMouseDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            base.Invalidate(base.SplitterRectangle);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            base.Invalidate(base.SplitterRectangle);

            Rectangle collapseRect = CollapseRect;
            Point mousePoint = e.Location;

            if (_histTest == HistTest.Button &&
                e.Button == MouseButtons.Left &&
                collapseRect.Contains(mousePoint))
            {
                OnCollapseClick(EventArgs.Empty);
            }
            _histTest = HistTest.None;
        }

        /// <summary>
        /// 设置鼠标箭头的状态.
        /// </summary>
        /// <param name="cursor"></param>
        private void SetCursor(Cursor cursor)
        {
            if (base.Cursor != cursor)
            {
                base.Cursor = cursor;
            }
        }

        /// <summary>
        /// 计算绘制箭头和网格的区域.
        /// </summary>
        /// <param name="collapseRect"></param>
        /// <param name="arrowRect">箭头区域</param>
        /// <param name="topLeftRect">网格左边的区域</param>
        /// <param name="bottomRightRect">网格右边的区域</param>
        private void CalculateRect(
            Rectangle collapseRect,
            out Rectangle arrowRect,
            out Rectangle topLeftRect,
            out Rectangle bottomRightRect)
        {
            int width;
            if (base.Orientation == Orientation.Horizontal)
            {
                width = (collapseRect.Width - DefaultArrowWidth) / 2;
                arrowRect = new Rectangle(
                    collapseRect.X + width,
                    collapseRect.Y,
                    DefaultArrowWidth,
                    collapseRect.Height);

                topLeftRect = new Rectangle(
                    collapseRect.X,
                    collapseRect.Y + 1,
                    width,
                    collapseRect.Height - 2);

                bottomRightRect = new Rectangle(
                    arrowRect.Right,
                    collapseRect.Y + 1,
                    width,
                    collapseRect.Height - 2);
            }
            else
            {
                width = (collapseRect.Height - DefaultArrowWidth) / 2;
                arrowRect = new Rectangle(
                    collapseRect.X,
                    collapseRect.Y + width,
                    collapseRect.Width,
                    DefaultArrowWidth);

                topLeftRect = new Rectangle(
                    collapseRect.X + 1,
                    collapseRect.Y,
                    collapseRect.Width - 2,
                    width);

                bottomRightRect = new Rectangle(
                    collapseRect.X + 1,
                    arrowRect.Bottom,
                    collapseRect.Width - 2,
                    width);
            }
        }

        private enum HistTest
        {
            None,
            Button,
            Spliter
        }
    }

    #endregion

    #region  Rendering 渲染

    /// <summary>
    /// 文本渲染提示的Graphics,可以使用Dispose()方法恢复上一次的渲染的质量类型.
    /// </summary>
    internal class TextRenderingHintGraphics : IDisposable
    {
        private Graphics _graphics;
        private TextRenderingHint _oldTextRenderingHint;

        /// <summary>
        /// 构建文本渲染提示的Graphics
        /// </summary>
        /// <param name="graphics"></param>
        public TextRenderingHintGraphics(Graphics graphics)
            : this(graphics, TextRenderingHint.AntiAlias)
        {
        }
        /// <summary>
        /// 构建文本渲染提示的Graphics
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="newTextRenderingHint">指定文本渲染质量.</param>
        public TextRenderingHintGraphics(
            Graphics graphics,
            TextRenderingHint newTextRenderingHint)
        {
            _graphics = graphics;
            _oldTextRenderingHint = graphics.TextRenderingHint;
            _graphics.TextRenderingHint = newTextRenderingHint;
        }

        #region IDisposable 成员

        /// <summary>
        /// 恢复上次渲染质量类型.
        /// </summary>
        public void Dispose()
        {
            _graphics.TextRenderingHint = _oldTextRenderingHint;
        }

        #endregion
    }

    /// <summary>
    /// 平滑渲染模式,通过Dispose()函数可恢复上次渲染模式.
    /// </summary>
    public class SmoothingModeGraphics : IDisposable
    {
        private SmoothingMode _oldMode;
        private Graphics _graphics;
        /// <summary>
        /// 构建平滑渲染模式,渲染模式为消除锯齿.
        /// </summary>
        /// <param name="graphics"></param>
        public SmoothingModeGraphics(Graphics graphics)
            : this(graphics, SmoothingMode.AntiAlias)
        {
        }
        /// <summary>
        /// 构建平滑渲染模式
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="newMode">新的渲染模式.</param>
        public SmoothingModeGraphics(Graphics graphics, SmoothingMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.SmoothingMode;
            graphics.SmoothingMode = newMode;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.SmoothingMode = _oldMode;
        }

        #endregion
    }

    /// <summary>
    /// 插值渲染模式.通过Dispose()函数可恢复上次渲染模式.
    /// </summary>
    public class InterpolationModeGraphics : IDisposable
    {
        private InterpolationMode _oldMode;
        private Graphics _graphics;
        /// <summary>
        /// 构造插值渲染模式,默认为高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
        /// </summary>
        /// <param name="graphics"></param>
        public InterpolationModeGraphics(Graphics graphics)
            : this(graphics, InterpolationMode.HighQualityBicubic)
        {
        }
        /// <summary>
        /// 构造插值渲染模式
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="newMode"> System.Drawing.Drawing2D.InterpolationMode 枚举指定在缩放或旋转图像时使用的算法。</param>
        public InterpolationModeGraphics(
            Graphics graphics, InterpolationMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.InterpolationMode;
            graphics.InterpolationMode = newMode;
        }

        #region IDisposable 成员
        /// <summary>
        /// 恢复上次渲染模式.
        /// </summary>
        public void Dispose()
        {
            _graphics.InterpolationMode = _oldMode;
        }

        #endregion
    }


    #endregion

    #region  Enum
    /// <summary>
    /// 箭头的方向.
    /// </summary>
    public enum ThumbArrowDirection
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        Left = 1,
        Right = 2,
        Up = 3,
        Down = 4,
        /// <summary>
        /// 左右?
        /// </summary>
        LeftRight = 5,
        /// <summary>
        /// 上下?
        /// </summary>
        UpDown = 6
    }

    /// <summary>
    /// 建立圆角路径的样式。
    /// </summary>
    public enum RoundStyle
    {
        /// <summary>
        /// 四个角都不是圆角。
        /// </summary>
        None = 0,
        /// <summary>
        /// 四个角都为圆角。
        /// </summary>
        All = 1,
        /// <summary>
        /// 左边两个角为圆角。
        /// </summary>
        Left = 2,
        /// <summary>
        /// 右边两个角为圆角。
        /// </summary>
        Right = 3,
        /// <summary>
        /// 上边两个角为圆角。
        /// </summary>
        Top = 4,
        /// <summary>
        /// 下边两个角为圆角。
        /// </summary>
        Bottom = 5,
        /// <summary>
        /// 左下角为圆角。
        /// </summary>
        BottomLeft = 6,
        /// <summary>
        /// 右下角为圆角。
        /// </summary>
        BottomRight = 7,
    }

    /// <summary>
    /// 分隔栏的状态.
    /// </summary>
    internal enum SpliterPanelState
    {
        /// <summary>
        /// 收缩
        /// </summary>
        Collapsed = 0,
        /// <summary>
        /// 展开
        /// </summary>
        Expanded = 1,
    }

    /// <summary>
    /// 点击SplitContainer控件收缩按钮时隐藏的Panel。
    /// </summary>
    public enum CollapsePanel
    {
        None = 0,
        Panel1 = 1,
        Panel2 = 2,
    }

    /// <summary>
    /// 控件的状态。
    /// </summary>
    public enum ControlState
    {
        /// <summary>
        ///  正常。
        /// </summary>
        Normal,
        /// <summary>
        /// 鼠标进入。
        /// </summary>
        Hover,
        /// <summary>
        /// 鼠标按下。
        /// </summary>
        Pressed,
        /// <summary>
        /// 获得焦点。
        /// </summary>
        Focused,
    }

    #endregion

    #region  Helper
    public static class RegionHelper
    {
        public static void CreateRegion(
            Control control,
            Rectangle bounds,
            int radius,
            RoundStyle roundStyle)
        {
            using (GraphicsPath path =
                GraphicsPathHelper.CreatePath(
                bounds, radius, roundStyle, true))
            {
                Region region = new Region(path);
                path.Widen(Pens.White);
                region.Union(path);
                if (control.Region != null)
                {
                    control.Region.Dispose();
                }
                control.Region = region;
            }
        }

        public static void CreateRegion(
            Control control,
            Rectangle bounds)
        {
            CreateRegion(control, bounds, 8, RoundStyle.All);
        }
    }


    /// <summary>
    /// 渲染帮助器.
    /// </summary>
    internal class RenderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">渲染的区域.</param>
        /// <param name="baseColor">基色</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        /// <param name="style">指定圆角的风格.</param>
        /// <param name="drawBorder"></param>
        /// <param name="drawGlass"></param>
        /// <param name="mode">指定线性渐变的方向.</param>
        internal static void RenderBackgroundInternal(
            Graphics g,
            Rectangle rect,
            Color baseColor,
            Color borderColor,
            Color innerBorderColor,
            RoundStyle style,
            bool drawBorder,
            bool drawGlass,
            LinearGradientMode mode)
        {
            RenderBackgroundInternal(
                g,
                rect,
                baseColor,
                borderColor,
                innerBorderColor,
                style,
                8,
                drawBorder,
                drawGlass,
                mode);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">渲染的区域.</param>
        /// <param name="baseColor">基色</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        /// <param name="style">指定圆角的风格.</param>
        /// <param name="roundWidth">指定圆角的半径</param>
        /// <param name="drawBorder"></param>
        /// <param name="drawGlass"></param>
        /// <param name="mode">指定线性渐变的方向.</param>
        internal static void RenderBackgroundInternal(
           Graphics g,
           Rectangle rect,
           Color baseColor,
           Color borderColor,
           Color innerBorderColor,
           RoundStyle style,
           int roundWidth,
           bool drawBorder,
           bool drawGlass,
           LinearGradientMode mode)
        {
            RenderBackgroundInternal(
                 g,
                 rect,
                 baseColor,
                 borderColor,
                 innerBorderColor,
                 style,
                 8,
                 0.45f,
                 drawBorder,
                 drawGlass,
                 mode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect">渲染的区域.</param>
        /// <param name="baseColor">基色</param>
        /// <param name="borderColor">边框颜色</param>
        /// <param name="innerBorderColor">内边框颜色.</param>
        /// <param name="style">指定圆角的风格.</param>
        /// <param name="roundWidth">指定圆角的半径</param>
        /// <param name="drawBorder"></param>
        /// <param name="drawGlass"></param>
        /// <param name="mode">指定线性渐变的方向.</param>
        /// <param name="basePosition"></param>
        internal static void RenderBackgroundInternal(
           Graphics g,
           Rectangle rect,
           Color baseColor,
           Color borderColor,
           Color innerBorderColor,
           RoundStyle style,
           int roundWidth,
           float basePosition,
           bool drawBorder,
           bool drawGlass,
           LinearGradientMode mode)
        {
            if (drawBorder)
            {
                rect.Width--;
                rect.Height--;
            }

            if (rect.Width == 0 || rect.Height == 0)
            {
                return;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, Color.Transparent, Color.Transparent, mode))
            {
                Color[] colors = new Color[4];
                colors[0] = GetColor(baseColor, 0, 35, 24, 9);
                colors[1] = GetColor(baseColor, 0, 13, 8, 3);
                colors[2] = baseColor;
                colors[3] = GetColor(baseColor, 0, 35, 24, 9);

                ColorBlend blend = new ColorBlend();
                blend.Positions = new float[] { 0.0f, basePosition, basePosition + 0.05f, 1.0f };
                blend.Colors = colors;
                brush.InterpolationColors = blend;
                if (style != RoundStyle.None)
                {
                    using (GraphicsPath path =
                        GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                    {
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
                        using (GraphicsPath pathTop = GraphicsPathHelper.CreatePath(
                            rectTop, roundWidth, RoundStyle.Top, false))
                        {
                            using (SolidBrush brushAlpha =
                                new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
                            {
                                g.FillPath(brushAlpha, pathTop);
                            }
                        }
                    }

                    if (drawGlass)
                    {
                        RectangleF glassRect = rect;
                        if (mode == LinearGradientMode.Vertical)
                        {
                            glassRect.Y = rect.Y + rect.Height * basePosition;
                            glassRect.Height = (rect.Height - rect.Height * basePosition) * 2;
                        }
                        else
                        {
                            glassRect.X = rect.X + rect.Width * basePosition;
                            glassRect.Width = (rect.Width - rect.Width * basePosition) * 2;
                        }
                        ControlPaintEx.DrawGlass(g, glassRect, 170, 0);
                    }

                    if (drawBorder)
                    {
                        using (GraphicsPath path =
                            GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            using (Pen pen = new Pen(borderColor))
                            {
                                g.DrawPath(pen, path);
                            }
                        }

                        rect.Inflate(-1, -1);
                        using (GraphicsPath path =
                            GraphicsPathHelper.CreatePath(rect, roundWidth, style, false))
                        {
                            using (Pen pen = new Pen(innerBorderColor))
                            {
                                g.DrawPath(pen, path);
                            }
                        }
                    }
                }
                else
                {
                    g.FillRectangle(brush, rect);
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
                            new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
                        {
                            g.FillRectangle(brushAlpha, rectTop);
                        }
                    }

                    if (drawGlass)
                    {
                        RectangleF glassRect = rect;
                        if (mode == LinearGradientMode.Vertical)
                        {
                            glassRect.Y = rect.Y + rect.Height * basePosition;
                            glassRect.Height = (rect.Height - rect.Height * basePosition) * 2;
                        }
                        else
                        {
                            glassRect.X = rect.X + rect.Width * basePosition;
                            glassRect.Width = (rect.Width - rect.Width * basePosition) * 2;
                        }
                        ControlPaintEx.DrawGlass(g, glassRect, 200, 0);
                    }

                    if (drawBorder)
                    {
                        using (Pen pen = new Pen(borderColor))
                        {
                            g.DrawRectangle(pen, rect);
                        }

                        rect.Inflate(-1, -1);
                        using (Pen pen = new Pen(innerBorderColor))
                        {
                            g.DrawRectangle(pen, rect);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 渲染箭头.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dropDownRect"></param>
        /// <param name="direction"></param>
        /// <param name="brush"></param>
        internal static void RenderArrowInternal(
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
                        new Point(point.X - 4, point.Y + 1), 
                        new Point(point.X + 4, point.Y + 1), 
                        new Point(point.X, point.Y - 2) };
                    break;

                case ArrowDirection.Right:
                    points = new Point[] {
                        new Point(point.X - 2, point.Y - 4), 
                        new Point(point.X - 2, point.Y + 4), 
                        new Point(point.X + 1, point.Y) };
                    break;

                default:
                    points = new Point[] {
                        new Point(point.X - 4, point.Y - 1), 
                        new Point(point.X + 4, point.Y - 1), 
                        new Point(point.X, point.Y + 2) };
                    break;
            }
            g.FillPolygon(brush, points);
        }

        /// <summary>
        /// 渲染图片.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="image"></param>
        /// <param name="imageRect"></param>
        /// <param name="alpha"></param>
        internal static void RenderAlphaImage(
            Graphics g,
            Image image,
            Rectangle imageRect,
            float alpha)
        {
            using (ImageAttributes imageAttributes = new ImageAttributes())
            {
                ColorMap colorMap = new ColorMap();

                colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
                colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

                ColorMap[] remapTable = { colorMap };

                imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

                float[][] colorMatrixElements = { 
                    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
                    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
                    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
                    new float[] {0.0f,  0.0f,  0.0f,  alpha, 0.0f},        
                    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
                ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

                imageAttributes.SetColorMatrix(
                    wmColorMatrix,
                    ColorMatrixFlag.Default,
                    ColorAdjustType.Bitmap);

                g.DrawImage(
                    image,
                    imageRect,
                    0,
                    0,
                    image.Width,
                    image.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }
        }

        /// <summary>
        /// 渲染网格点.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="pixelsBetweenDots"></param>
        /// <param name="outerColor"></param>
        internal static void RenderGrid(
            Graphics g,
            Rectangle rect,
            Size pixelsBetweenDots,
            Color outerColor)
        {
            int outerWin32Corlor = ColorTranslator.ToWin32(outerColor);
            IntPtr hdc = g.GetHdc();

            for (int x = rect.X; x <= rect.Right; x += pixelsBetweenDots.Width)
            {
                for (int y = rect.Y; y <= rect.Bottom; y += pixelsBetweenDots.Height)
                {
                    SetPixel(hdc, x, y, outerWin32Corlor);
                }
            }

            g.ReleaseHdc(hdc);
        }

        /// <summary>
        /// 基于一种颜色,获取另一种颜色.
        /// </summary>
        /// <param name="colorBase"></param>
        /// <param name="a"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        internal static Color GetColor(
            Color colorBase, int a, int r, int g, int b)
        {
            int a0 = colorBase.A;
            int r0 = colorBase.R;
            int g0 = colorBase.G;
            int b0 = colorBase.B;

            if (a + a0 > 255) { a = 255; } else { a = Math.Max(0, a + a0); }
            if (r + r0 > 255) { r = 255; } else { r = Math.Max(0, r + r0); }
            if (g + g0 > 255) { g = 255; } else { g = Math.Max(0, g + g0); }
            if (b + b0 > 255) { b = 255; } else { b = Math.Max(0, b + b0); }

            return Color.FromArgb(a, r, g, b);
        }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern uint SetPixel(IntPtr hdc, int X, int Y, int crColor);
    }

    /// <summary>
    /// GDI 绘制帮助器.
    /// </summary>
    public static class GraphicsPathHelper
    {
        /// <summary>
        /// 建立带有圆角样式的路径。
        /// </summary>
        /// <param name="rect">用来建立路径的矩形。</param>
        /// <param name="_radius">圆角的大小。</param>
        /// <param name="style">圆角的样式。</param>
        /// <param name="correction">是否把矩形长宽减 1,以便画出边框。</param>
        /// <returns>建立的路径。</returns>
        public static GraphicsPath CreatePath(
            Rectangle rect, int radius, RoundStyle style, bool correction)
        {
            GraphicsPath path = new GraphicsPath();
            int radiusCorrection = correction ? 1 : 0;
            switch (style)
            {
                case RoundStyle.None:
                    path.AddRectangle(rect);
                    break;
                case RoundStyle.All:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y,
                        radius,
                        radius,
                        270,
                        90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius, 0, 90);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    break;
                case RoundStyle.Left:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddLine(
                        rect.Right - radiusCorrection, rect.Y,
                        rect.Right - radiusCorrection, rect.Bottom - radiusCorrection);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    break;
                case RoundStyle.Right:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y,
                        radius,
                        radius,
                        270,
                        90);
                    path.AddArc(
                       rect.Right - radius - radiusCorrection,
                       rect.Bottom - radius - radiusCorrection,
                       radius,
                       radius,
                       0,
                       90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    break;
                case RoundStyle.Top:
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Y,
                        radius,
                        radius,
                        270,
                        90);
                    path.AddLine(
                        rect.Right - radiusCorrection, rect.Bottom - radiusCorrection,
                        rect.X, rect.Bottom - radiusCorrection);
                    break;
                case RoundStyle.Bottom:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        0,
                        90);
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
                case RoundStyle.BottomLeft:
                    path.AddArc(
                        rect.X,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        90,
                        90);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    path.AddLine(
                        rect.Right - radiusCorrection,
                        rect.Y,
                        rect.Right - radiusCorrection,
                        rect.Bottom - radiusCorrection);
                    break;
                case RoundStyle.BottomRight:
                    path.AddArc(
                        rect.Right - radius - radiusCorrection,
                        rect.Bottom - radius - radiusCorrection,
                        radius,
                        radius,
                        0,
                        90);
                    path.AddLine(rect.X, rect.Bottom - radiusCorrection, rect.X, rect.Y);
                    path.AddLine(rect.X, rect.Y, rect.Right - radiusCorrection, rect.Y);
                    break;
            }
            path.CloseFigure();

            return path;
        }

        public static GraphicsPath CreateTrackBarThumbPath(
            Rectangle rect, ThumbArrowDirection arrowDirection)
        {
            GraphicsPath path = new GraphicsPath();
            PointF centerPoint = new PointF(
                rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
            float offset = 0;

            switch (arrowDirection)
            {
                case ThumbArrowDirection.Left:
                case ThumbArrowDirection.Right:
                    offset = rect.Width / 2f - 4;
                    break;
                case ThumbArrowDirection.Up:
                case ThumbArrowDirection.Down:
                    offset = rect.Height / 2f - 4;
                    break;
            }

            switch (arrowDirection)
            {
                case ThumbArrowDirection.Left:
                    path.AddLine(
                        rect.X, centerPoint.Y, rect.X + offset, rect.Y);
                    path.AddLine(
                        rect.Right, rect.Y, rect.Right, rect.Bottom);
                    path.AddLine(
                        rect.X + offset, rect.Bottom, rect.X, centerPoint.Y);
                    break;
                case ThumbArrowDirection.Right:
                    path.AddLine(
                        rect.Right, centerPoint.Y, rect.Right - offset, rect.Bottom);
                    path.AddLine(
                        rect.X, rect.Bottom, rect.X, rect.Y);
                    path.AddLine(
                        rect.Right - offset, rect.Y, rect.Right, centerPoint.Y);
                    break;
                case ThumbArrowDirection.Up:
                    path.AddLine(
                        centerPoint.X, rect.Y, rect.X, rect.Y + offset);
                    path.AddLine(
                        rect.X, rect.Bottom, rect.Right, rect.Bottom);
                    path.AddLine(
                        rect.Right, rect.Y + offset, centerPoint.X, rect.Y);
                    break;
                case ThumbArrowDirection.Down:
                    path.AddLine(
                         centerPoint.X, rect.Bottom, rect.X, rect.Bottom - offset);
                    path.AddLine(
                        rect.X, rect.Y, rect.Right, rect.Y);
                    path.AddLine(
                        rect.Right, rect.Bottom - offset, centerPoint.X, rect.Bottom);
                    break;
                case ThumbArrowDirection.LeftRight:
                    break;
                case ThumbArrowDirection.UpDown:
                    break;
                case ThumbArrowDirection.None:
                    path.AddRectangle(rect);
                    break;
            }

            path.CloseFigure();
            return path;
        }
    }

    public sealed class ControlPaintEx
    {
        private ControlPaintEx() { }

        /// <summary>
        /// 绘制检测标记
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public static void DrawCheckedFlag(
            Graphics g, Rectangle rect, Color color)
        {
            PointF[] points = new PointF[3];
            points[0] = new PointF(
                rect.X + rect.Width / 4.5f,
                rect.Y + rect.Height / 2.5f);
            points[1] = new PointF(
                rect.X + rect.Width / 2.5f,
                rect.Bottom - rect.Height / 3f);
            points[2] = new PointF(
                rect.Right - rect.Width / 4.0f,
                rect.Y + rect.Height / 4.5f);
            using (Pen pen = new Pen(color, 2F))
            {
                g.DrawLines(pen, points);
            }
        }

        /// <summary>
        /// 绘制玻璃效果(默认效果)
        /// </summary>
        /// <param name="g"></param>
        /// <param name="glassRect">表示定义椭圆的边框</param>
        /// <param name="alphaCenter">椭圆中心的System.Drawing.Color 的 alpha 值</param>
        /// <param name="alphaSurround">椭圆边界的System.Drawing.Color 的 alpha 值</param>
        public static void DrawGlass(
            Graphics g, RectangleF glassRect,
            int alphaCenter, int alphaSurround)
        {
            DrawGlass(g, glassRect, Color.White, alphaCenter, alphaSurround);
        }

        /// <summary>
        /// 绘制玻璃效果
        /// </summary>
        /// <param name="g"></param>
        /// <param name="glassRect">表示定义椭圆的边框</param>
        /// <param name="glassColor">椭圆内部的颜色</param>
        /// <param name="alphaCenter">椭圆中心的System.Drawing.Color 的 alpha 值</param>
        /// <param name="alphaSurround">椭圆边界的System.Drawing.Color 的 alpha 值</param>
        public static void DrawGlass(
           Graphics g,
            RectangleF glassRect,
            Color glassColor,
            int alphaCenter,
            int alphaSurround)
        {
            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(glassRect); //绘制一个椭圆
                using (PathGradientBrush brush = new PathGradientBrush(path))
                {
                    //设置中心的颜色
                    brush.CenterColor = Color.FromArgb(alphaCenter, glassColor);
                    //设置周边的颜色.
                    brush.SurroundColors = new Color[] { 
                        Color.FromArgb(alphaSurround, glassColor) };
                    //指定中心点.
                    brush.CenterPoint = new PointF(
                        glassRect.X + glassRect.Width / 2,
                        glassRect.Y + glassRect.Height / 2);
                    //绘制.
                    g.FillPath(brush, path);
                }
            }
        }
        /// <summary>
        /// 绘制背景图片.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect)
        {
            DrawBackgroundImage(
                g,
                backgroundImage,
                backColor,
                backgroundImageLayout,
                bounds,
                clipRect,
                Point.Empty,
                RightToLeft.No);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        /// <param name="scrollOffset"></param>
        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect,
            Point scrollOffset)
        {
            DrawBackgroundImage(
                g,
                backgroundImage,
                backColor,
                backgroundImageLayout,
                bounds,
                clipRect,
                scrollOffset,
                RightToLeft.No);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="backColor"></param>
        /// <param name="backgroundImageLayout"></param>
        /// <param name="bounds"></param>
        /// <param name="clipRect"></param>
        /// <param name="scrollOffset"></param>
        /// <param name="rightToLeft"></param>
        public static void DrawBackgroundImage(
            Graphics g,
            Image backgroundImage,
            Color backColor,
            ImageLayout backgroundImageLayout,
            Rectangle bounds,
            Rectangle clipRect,
            Point scrollOffset,
            RightToLeft rightToLeft)
        {
            if (g == null)
            {
                throw new ArgumentNullException("g");
            }
            if (backgroundImageLayout == ImageLayout.Tile)
            {
                using (TextureBrush brush = new TextureBrush(backgroundImage, WrapMode.Tile))
                {
                    if (scrollOffset != Point.Empty)
                    {
                        Matrix transform = brush.Transform;
                        transform.Translate((float)scrollOffset.X, (float)scrollOffset.Y);
                        brush.Transform = transform;
                    }
                    g.FillRectangle(brush, clipRect);
                    return;
                }
            }
            Rectangle rect = CalculateBackgroundImageRectangle(
                bounds,
                backgroundImage,
                backgroundImageLayout);
            if ((rightToLeft == RightToLeft.Yes) &&
                (backgroundImageLayout == ImageLayout.None))
            {
                rect.X += clipRect.Width - rect.Width;
            }
            using (SolidBrush brush2 = new SolidBrush(backColor))
            {
                g.FillRectangle(brush2, clipRect);
            }
            if (!clipRect.Contains(rect))
            {
                if ((backgroundImageLayout == ImageLayout.Stretch) ||
                    (backgroundImageLayout == ImageLayout.Zoom))
                {
                    rect.Intersect(clipRect);
                    g.DrawImage(backgroundImage, rect);
                }
                else if (backgroundImageLayout == ImageLayout.None)
                {
                    rect.Offset(clipRect.Location);
                    Rectangle destRect = rect;
                    destRect.Intersect(clipRect);
                    Rectangle rectangle3 = new Rectangle(Point.Empty, destRect.Size);
                    g.DrawImage(
                        backgroundImage,
                        destRect,
                        rectangle3.X,
                        rectangle3.Y,
                        rectangle3.Width,
                        rectangle3.Height,
                        GraphicsUnit.Pixel);
                }
                else
                {
                    Rectangle rectangle4 = rect;
                    rectangle4.Intersect(clipRect);
                    Rectangle rectangle5 = new Rectangle(
                        new Point(rectangle4.X - rect.X, rectangle4.Y - rect.Y),
                        rectangle4.Size);
                    g.DrawImage(
                        backgroundImage,
                        rectangle4,
                        rectangle5.X,
                        rectangle5.Y,
                        rectangle5.Width,
                        rectangle5.Height,
                        GraphicsUnit.Pixel);
                }
            }
            else
            {
                ImageAttributes imageAttr = new ImageAttributes();
                imageAttr.SetWrapMode(WrapMode.TileFlipXY);
                g.DrawImage(
                    backgroundImage,
                    rect,
                    0,
                    0,
                    backgroundImage.Width,
                    backgroundImage.Height,
                    GraphicsUnit.Pixel,
                    imageAttr);
                imageAttr.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="orientation"></param>
        public static void DrawScrollBarTrack(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Orientation orientation)
        {
            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRect(
                g,
                rect,
                begin,
                end,
                begin,
                begin,
                blend,
                mode,
                true,
                false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="orientation"></param>
        /// <param name="changeColor"></param>
        public static void DrawScrollBarThumb(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Orientation orientation,
            bool changeColor)
        {
            if (changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            if (bHorizontal)
            {
                rect.Inflate(0, -1);
            }
            else
            {
                rect.Inflate(-1, 0);
            }

            DrawGradientRoundRect(
                g,
                rect,
                begin,
                end,
                border,
                innerBorder,
                blend,
                mode,
                4,
                RoundStyle.All,
                true,
                true);
        }

        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="fore"></param>
        /// <param name="orientation"></param>
        /// <param name="arrowDirection"></param>
        /// <param name="changeColor"></param>
        public static void DrawScrollBarArraw(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Color fore,
            Orientation orientation,
            ArrowDirection arrowDirection,
            bool changeColor)
        {
            if (changeColor)
            {
                Color tmp = begin;
                begin = end;
                end = tmp;
            }

            bool bHorizontal = orientation == Orientation.Horizontal;
            LinearGradientMode mode = bHorizontal ?
                LinearGradientMode.Vertical : LinearGradientMode.Horizontal;

            rect.Inflate(-1, -1);

            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRoundRect(
                g,
                rect,
                begin,
                end,
                border,
                innerBorder,
                blend,
                mode,
                4,
                RoundStyle.All,
                true,
                true);

            using (SolidBrush brush = new SolidBrush(fore))
            {
                RenderHelper.RenderArrowInternal(
                    g,
                    rect,
                    arrowDirection,
                    brush);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        public static void DrawScrollBarSizer(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end)
        {
            Blend blend = new Blend();
            blend.Factors = new float[] { 1f, 0.5f, 0f };
            blend.Positions = new float[] { 0f, 0.5f, 1f };

            DrawGradientRect(
                 g,
                 rect,
                 begin,
                 end,
                 begin,
                 begin,
                 blend,
                 LinearGradientMode.Horizontal,
                 true,
                 false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="blend"></param>
        /// <param name="mode"></param>
        /// <param name="drawBorder"></param>
        /// <param name="drawInnerBorder"></param>
        internal static void DrawGradientRect(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Blend blend,
            LinearGradientMode mode,
            bool drawBorder,
            bool drawInnerBorder)
        {
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect, begin, end, mode))
            {
                brush.Blend = blend;
                g.FillRectangle(brush, rect);
            }

            if (drawBorder)
            {
                ControlPaint.DrawBorder(
                    g, rect, border, ButtonBorderStyle.Solid);
            }

            if (drawInnerBorder)
            {
                rect.Inflate(-1, -1);
                ControlPaint.DrawBorder(
                    g, rect, border, ButtonBorderStyle.Solid);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="border"></param>
        /// <param name="innerBorder"></param>
        /// <param name="blend"></param>
        /// <param name="mode"></param>
        /// <param name="radios"></param>
        /// <param name="roundStyle"></param>
        /// <param name="drawBorder"></param>
        /// <param name="drawInnderBorder"></param>
        internal static void DrawGradientRoundRect(
            Graphics g,
            Rectangle rect,
            Color begin,
            Color end,
            Color border,
            Color innerBorder,
            Blend blend,
            LinearGradientMode mode,
            int radios,
            RoundStyle roundStyle,
            bool drawBorder,
            bool drawInnderBorder)
        {
            using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                rect, radios, roundStyle, true))
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                      rect, begin, end, mode))
                {
                    brush.Blend = blend;
                    g.FillPath(brush, path);
                }

                if (drawBorder)
                {
                    using (Pen pen = new Pen(border))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            if (drawInnderBorder)
            {
                rect.Inflate(-1, -1);
                using (GraphicsPath path = GraphicsPathHelper.CreatePath(
                    rect, radios, roundStyle, true))
                {
                    using (Pen pen = new Pen(innerBorder))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
        /// <summary>
        /// 计算背景图片的区域.
        /// </summary>
        /// <param name="bounds"></param>
        /// <param name="backgroundImage">背景图片</param>
        /// <param name="imageLayout">指定控件上图片的位置.</param>
        /// <returns></returns>
        internal static Rectangle CalculateBackgroundImageRectangle(
            Rectangle bounds,
            Image backgroundImage,
            ImageLayout imageLayout)
        {
            Rectangle rectangle = bounds;
            if (backgroundImage != null)
            {
                switch (imageLayout)
                {
                    case ImageLayout.None://无 
                        rectangle.Size = backgroundImage.Size;
                        return rectangle;

                    case ImageLayout.Tile://
                        return rectangle;

                    case ImageLayout.Center://图片居中显示.
                        {
                            rectangle.Size = backgroundImage.Size;
                            Size size = bounds.Size;
                            if (size.Width > rectangle.Width)
                            {
                                rectangle.X = (size.Width - rectangle.Width) / 2;
                            }
                            if (size.Height > rectangle.Height)
                            {
                                rectangle.Y = (size.Height - rectangle.Height) / 2;
                            }
                            return rectangle;
                        }
                    case ImageLayout.Stretch://图片拉伸.
                        rectangle.Size = bounds.Size;
                        return rectangle;

                    case ImageLayout.Zoom://图片放大.
                        {
                            Size size2 = backgroundImage.Size;
                            float num = ((float)bounds.Width) / ((float)size2.Width);
                            float num2 = ((float)bounds.Height) / ((float)size2.Height);
                            if (num >= num2)
                            {
                                rectangle.Height = bounds.Height;
                                rectangle.Width = (int)((size2.Width * num2) + 0.5);
                                if (bounds.X >= 0)
                                {
                                    rectangle.X = (bounds.Width - rectangle.Width) / 2;
                                }
                                return rectangle;
                            }
                            rectangle.Width = bounds.Width;
                            rectangle.Height = (int)((size2.Height * num) + 0.5);
                            if (bounds.Y >= 0)
                            {
                                rectangle.Y = (bounds.Height - rectangle.Height) / 2;
                            }
                            return rectangle;
                        }
                }
            }
            return rectangle;
        }
    }
    #endregion

}
