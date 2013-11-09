using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Drawing.Text;
using System.Drawing.Imaging;

namespace CRC.Controls
{

/* 作者：Starts_2000
* 日期：2010-03-20
* 网站：http://www.csharpwin.com CS 程序员之窗。
* 你可以免费使用或修改以下代码，但请保留版权信息。
* 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
*/

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

 

  


}
