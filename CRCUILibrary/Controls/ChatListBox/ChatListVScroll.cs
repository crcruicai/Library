using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace CRC.Controls
{

    /// <summary>
    /// 滚动栏代理类.
    /// </summary>
    internal class ChatListVScroll
    {

        #region 属性
        /// <summary>
        /// 滚动条自身的区域.
        /// </summary>
        private Rectangle bounds;
        /// <summary>
        /// 滚动条自身的区域
        /// </summary>
        public Rectangle Bounds
        {
            get { return bounds; }
        }
        /// <summary>
        /// 上边箭头区域
        /// </summary>
        private Rectangle upBounds;
        /// <summary>
        /// 上边箭头区域
        /// </summary>
        public Rectangle UpBounds
        {
            get { return upBounds; }
        }
        /// <summary>
        /// 下边箭头区域
        /// </summary>
        private Rectangle downBounds;
        /// <summary>
        /// 下边箭头区域
        /// </summary>
        public Rectangle DownBounds
        {
            get { return downBounds; }
        }
        /// <summary>
        /// 滑块区域.
        /// </summary>
        private Rectangle sliderBounds;
        /// <summary>
        /// 滑块区域
        /// </summary>
        public Rectangle SliderBounds
        {
            get { return sliderBounds; }
        }
        /// <summary>
        /// 背景颜色
        /// </summary>
        private Color backColor;
        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackColor
        {
            get { return backColor; }
            set { backColor = value; }
        }

        /// <summary>
        /// 滑块默认颜色
        /// </summary>
        private Color sliderDefaultColor;
        /// <summary>
        /// 滑块默认颜色
        /// </summary>
        public Color SliderDefaultColor
        {
            get { return sliderDefaultColor; }
            set
            {
                if (sliderDefaultColor == value)
                    return;
                sliderDefaultColor = value;
                ctrl.Invalidate(this.sliderBounds);
            }
        }

        private Color sliderDownColor;
        /// <summary>
        /// 滑块
        /// </summary>
        public Color SliderDownColor
        {
            get { return sliderDownColor; }
            set
            {
                if (sliderDownColor == value)
                    return;
                sliderDownColor = value;
                ctrl.Invalidate(this.sliderBounds);
            }
        }

        private Color arrowBackColor;
        public Color ArrowBackColor
        {
            get { return arrowBackColor; }
            set
            {
                if (arrowBackColor == value)
                    return;
                arrowBackColor = value;
                ctrl.Invalidate(this.bounds);
            }
        }

        private Color arrowColor;
        /// <summary>
        /// 箭头的颜色.
        /// </summary>
        public Color ArrowColor
        {
            get { return arrowColor; }
            set
            {
                if (arrowColor == value)
                    return;
                arrowColor = value;
                ctrl.Invalidate(this.bounds);
            }
        }
        //绑定的控件
        /// <summary>
        /// 绑定的控件
        /// </summary>
        private Control ctrl;
        /// <summary>
        /// 绑定的控件.
        /// </summary>
        public Control Ctrl
        {
            get { return ctrl; }
            set { ctrl = value; }
        }
        //
        /// <summary>
        /// 虚拟的一个高度(控件中内容的高度)
        /// </summary>
        private int virtualHeight;
        /// <summary>
        /// 虚拟的一个高度(控件中内容的高度)
        /// </summary>
        public int VirtualHeight
        {
            get { return virtualHeight; }
            set
            {
                if (value <= ctrl.Height)
                {
                    if (shouldBeDraw == false)
                        return;
                    shouldBeDraw = false;
                    if (this.value != 0)
                    {
                        this.value = 0;
                        ctrl.Invalidate();
                    }
                }
                else
                {
                    shouldBeDraw = true;
                    if (value - this.value < ctrl.Height)
                    {
                        this.value -= ctrl.Height - value + this.value;
                        ctrl.Invalidate();
                    }
                }
                virtualHeight = value;
            }
        }

        public void SetScroll(int value)
        {
            //如果 不需要绘制滚动栏且所滚动的高度小于控件高度.
            if (!shouldBeDraw && value < ctrl.Height)
                return;
            //极值情况.
            if (value < 0)
            {
                if (this.value == 0)
                    return;
                this.value = 0;

            }
            else if (value > virtualHeight)//大于虚拟值.
            {
                this.value = Math.Max(value - ctrl.Height, 0);
                Debug.WriteLine(string.Format("NN {0}", this.value));
                return;
            }
            else if (value < ctrl.Height)//小于控件的高度值.
            {
                this.value = 0;
                Debug.WriteLine(string.Format("VV {0}", this.value));
            }
            else
            {
                if (value > virtualHeight - ctrl.Height)
                {
                    if (this.value == virtualHeight - ctrl.Height)
                    {
                        Debug.WriteLine("值无效,退出.");
                        return;
                    }

                    this.value = virtualHeight - ctrl.Height;
                    Debug.WriteLine(string.Format("PP {0}", this.value));
                }
                else
                {
                    this.value = value;
                    Debug.WriteLine(string.Format("XX {0}", this.value));
                }

            }
            ctrl.Invalidate();
            return;
        }
        /// <summary>
        /// 当前滚动条位置
        /// </summary>
        private int value;
        /// <summary>
        /// 当前滚动条位置
        /// </summary>
        public int Value
        {
            get { return value; }
            set
            {
                if (!shouldBeDraw)
                    return;
                if (value < 0)
                {
                    if (this.value == 0)
                        return;
                    this.value = 0;
                    ctrl.Invalidate();
                    return;
                }
                if (value > virtualHeight - ctrl.Height)
                {
                    if (this.value == virtualHeight - ctrl.Height)
                        return;
                    this.value = virtualHeight - ctrl.Height;
                    ctrl.Invalidate();
                    return;
                }
                this.value = value;
                ctrl.Invalidate();
                
            }
        }
        /// <summary>
        /// 指示是否在控件上绘制滚动条.
        /// </summary>
        private bool shouldBeDraw;
        /// <summary>
        /// 是否有必要在控件上绘制滚动条
        /// </summary>
        public bool ShouldBeDraw
        {
            get { return shouldBeDraw; }
        }
        /// <summary>
        /// 是否有鼠标键按下
        /// </summary>
        private bool isMouseDown;
        /// <summary>
        /// 是否有鼠标键按下.
        /// </summary>
        public bool IsMouseDown
        {
            get { return isMouseDown; }
            set
            {
                if (value)
                {
                    m_nLastSliderY = sliderBounds.Y;
                }
                isMouseDown = value;
            }
        }
        /// <summary>
        /// 是否有鼠标悬浮在滑块上面.
        /// </summary>
        private bool isMouseOnSlider;
        /// <summary>
        /// 是否有鼠标悬浮在滑块上面.
        /// </summary>
        public bool IsMouseOnSlider
        {
            get { return isMouseOnSlider; }
            set
            {
                if (isMouseOnSlider == value)
                    return;
                isMouseOnSlider = value;
                ctrl.Invalidate(this.SliderBounds);
            }
        }
        /// <summary>
        /// 是否有鼠标按下之后释放.
        /// </summary>
        private bool isMouseOnUp;
        /// <summary>
        /// 是否有鼠标按下之后释放.
        /// </summary>
        public bool IsMouseOnUp
        {
            get { return isMouseOnUp; }
            set
            {
                if (isMouseOnUp == value)
                    return;
                isMouseOnUp = value;
                ctrl.Invalidate(this.UpBounds);
            }
        }
        /// <summary>
        /// 是否有鼠标按下.
        /// </summary>
        private bool isMouseOnDown;
        /// <summary>
        /// 是否有鼠标按下.
        /// </summary>
        public bool IsMouseOnDown
        {
            get { return isMouseOnDown; }
            set
            {
                if (isMouseOnDown == value)
                    return;
                isMouseOnDown = value;
                ctrl.Invalidate(this.DownBounds);
            }
        }
        /// <summary>
        /// 鼠标在滑块点下时候的y坐标
        /// </summary>
        private int mouseDownY;
        /// <summary>
        /// 鼠标在滑块点下时候的y坐标
        /// </summary>
        public int MouseDownY
        {
            get { return mouseDownY; }
            set { mouseDownY = value; }
        }
        #endregion


        //
        /// <summary>
        /// 滑块移动前的 滑块的y坐标
        /// </summary>
        private int m_nLastSliderY;
        /// <summary>
        /// 滚动条代理类.
        /// </summary>
        /// <param name="c">需要滚动条的控件.</param>
        public ChatListVScroll(Control c)
        {
            this.ctrl = c;
            virtualHeight = 400;
            bounds = new Rectangle(0, 0, 10, 10);
            upBounds = new Rectangle(0, 0, 10, 10);
            downBounds = new Rectangle(0, 0, 10, 10);
            sliderBounds = new Rectangle(0, 0, 10, 10);
            this.backColor = Color.LightYellow;
            this.sliderDefaultColor = Color.Gray;
            this.sliderDownColor = Color.DarkGray;
            this.arrowBackColor = Color.Gray;
            this.arrowColor = Color.White;
        }

        public void ClearAllMouseOn()
        {
            if (!this.isMouseOnDown && !this.isMouseOnSlider && !this.isMouseOnUp)
                return;
            this.isMouseOnDown =
                this.isMouseOnSlider =
                this.isMouseOnUp = false;
            ctrl.Invalidate(this.bounds);
        }
        //
        /// <summary>
        /// 将滑块跳动至一个地方
        /// </summary>
        /// <param name="nCurrentMouseY"></param>
        public void MoveSliderToLocation(int nCurrentMouseY)
        {
            if (nCurrentMouseY - sliderBounds.Height / 2 < 11)
                sliderBounds.Y = 11;
            else if (nCurrentMouseY + sliderBounds.Height / 2 > ctrl.Height - 11)
                sliderBounds.Y = ctrl.Height - sliderBounds.Height - 11;
            else
                sliderBounds.Y = nCurrentMouseY - sliderBounds.Height / 2;
            this.value = (int)((double)(sliderBounds.Y - 11) / (ctrl.Height - 22 - SliderBounds.Height) * (virtualHeight - ctrl.Height));
            ctrl.Invalidate();
        }
        //

        /// <summary>
        /// 根据鼠标位置移动滑块
        /// </summary>
        /// <param name="nCurrentMouseY"></param>
        public void MoveSliderFromLocation(int nCurrentMouseY)
        {
            //if (!this.IsMouseDown) return;
            if (m_nLastSliderY + nCurrentMouseY - mouseDownY < 11)
            {
                if (sliderBounds.Y == 11)
                    return;
                sliderBounds.Y = 11;
            }
            else if (m_nLastSliderY + nCurrentMouseY - mouseDownY > ctrl.Height - 11 - SliderBounds.Height)
            {
                if (sliderBounds.Y == ctrl.Height - 11 - sliderBounds.Height)
                    return;
                sliderBounds.Y = ctrl.Height - 11 - sliderBounds.Height;
            }
            else
            {
                sliderBounds.Y = m_nLastSliderY + nCurrentMouseY - mouseDownY;
            }
            this.value = (int)((double)(sliderBounds.Y - 11) / (ctrl.Height - 22 - SliderBounds.Height) * (virtualHeight - ctrl.Height));
            ctrl.Invalidate();
        }
        //
        /// <summary>
        /// 绘制滚动条
        /// </summary>
        /// <param name="g"></param>
        public void ReDrawScroll(Graphics g)
        {
            if (!shouldBeDraw)
                return;
            bounds.X = ctrl.Width - 10;
            bounds.Height = ctrl.Height;
            upBounds.X = downBounds.X = bounds.X;
            downBounds.Y = ctrl.Height - 10;
            //计算滑块位置
            sliderBounds.X = bounds.X;
            sliderBounds.Height = (int)(((double)ctrl.Height / virtualHeight) * (ctrl.Height - 22));
            if (sliderBounds.Height < 3) sliderBounds.Height = 3;
            sliderBounds.Y = 11 + (int)(((double)value / (virtualHeight - ctrl.Height)) * (ctrl.Height - 22 - sliderBounds.Height));
            SolidBrush sb = new SolidBrush(this.backColor);
            try
            {
                g.FillRectangle(sb, bounds);
                sb.Color = this.arrowBackColor;
                g.FillRectangle(sb, upBounds);
                g.FillRectangle(sb, downBounds);
                if (this.isMouseDown || this.isMouseOnSlider)
                    sb.Color = this.sliderDownColor;
                else
                    sb.Color = this.sliderDefaultColor;
                g.FillRectangle(sb, sliderBounds);
                sb.Color = this.arrowColor;
                if (this.isMouseOnUp)
                    g.FillPolygon(sb, new Point[]{
                    new Point(ctrl.Width - 5,3),
                    new Point(ctrl.Width - 9,7),
                    new Point(ctrl.Width - 2,7)
                });
                if (this.isMouseOnDown)
                    g.FillPolygon(sb, new Point[]{
                    new Point(ctrl.Width - 5,ctrl.Height - 4),
                    new Point(ctrl.Width - 8,ctrl.Height - 7),
                    new Point(ctrl.Width - 2,ctrl.Height - 7)
                });
            }
            finally
            {
                sb.Dispose();
            }
        }
    }
}
