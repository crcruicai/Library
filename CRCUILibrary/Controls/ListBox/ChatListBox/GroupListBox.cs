using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using System.Diagnostics;

namespace CRC.Controls
{

    /// <summary>
    /// GroupListBox的事件类.
    /// </summary>
    public class GroupListBoxEventArgs:EventArgs 
    {
        private TextGroupItem _MouseOnsubItem;
        /// <summary>
        /// 鼠标选中时,子项所在的组.
        /// </summary>
        public TextGroupItem MouseOnsubItem
        {
            get
            {
                return _MouseOnsubItem;
            }
        }

        private TextSubItem  _SelectSubItem;
        /// <summary>
        /// 鼠标选择时的子项.
        /// </summary>
        public  TextSubItem SelectSubItem
        {
            get
            {
                return _SelectSubItem;
            }
            set
            {
                _SelectSubItem = value;
            }
        }



        public GroupListBoxEventArgs(TextGroupItem mouseOnSubItem,TextSubItem selectSubItem)
        {
            _SelectSubItem = selectSubItem;
            _MouseOnsubItem = mouseOnSubItem;
        }
    }

    /// <summary>
    /// 这是一个简单的组列表控件.
    /// <para>子项只支持显示文本.如果需要更多的功能,可以使用ChatListBox.</para>
    /// </summary>
    public class GroupListBox:Control 
    {

        #region 字段与变量
        /// <summary>
        /// 鼠标的位置
        /// </summary>
        private Point m_ptMousePos;             //
        /// <summary>
        /// 滚动条
        /// </summary>
        private ChatListVScroll chatVScroll;    //

        private TextGroupItem m_mouseOnItem;

        private TextSubItem m_mouseOnSubItem;
        #endregion

        #region 事件


        /// <summary>
        /// 当鼠标停留在子项上时,引发.
        /// </summary>
        public event EventHandler<GroupListBoxEventArgs> MouseOnSubItem;

        protected virtual void OnMouseOnSubItem(GroupListBoxEventArgs e)
        {
            if (MouseOnSubItem != null)
                MouseOnSubItem(this, e);
        }

        /// <summary>
        /// 右键点击子项时,引发事件.
        /// </summary>
        public event EventHandler<GroupListBoxEventArgs> ContextSubItem;

        protected virtual void OnContextSubItem(GroupListBoxEventArgs e)
        {
            if (ContextSubItem != null)
                ContextSubItem(this, e);
        }

        /// <summary>
        /// 当双击子项时发生
        /// </summary>
        public event EventHandler<GroupListBoxEventArgs> DoubleClickSubItem;

        /// <summary>
        /// 单击子项时发生.
        /// </summary>
        public event EventHandler<GroupListBoxEventArgs> ClickSubItem;
        
        /// <summary>
        /// 当鼠标进入组标题栏时发生.
        /// </summary>
        public event EventHandler<GroupListBoxEventArgs> MouseEnterHead;
        /// <summary>
        /// 当鼠标离开组标题栏时发生.
        /// </summary>
        public event EventHandler<GroupListBoxEventArgs> MouseLeaveHead;


        protected virtual void OnDoubleClickSubItem(GroupListBoxEventArgs e)
        {
            if (DoubleClickSubItem != null)
                DoubleClickSubItem(this, e);
        }

        protected virtual void OnClickSubItem(GroupListBoxEventArgs e)
        {
            if (ClickSubItem != null)
                ClickSubItem(this, e);
        }

        protected virtual void OnMouseLeaveHead(GroupListBoxEventArgs e)
        {
            if (MouseEnterHead != null)
                MouseEnterHead(this, e);
        }
        protected virtual void OnMouseEnterHead(GroupListBoxEventArgs e)
        {
            if (MouseLeaveHead != null)
                MouseLeaveHead(this, e);
        }
        #endregion

        #region 构造函数

        public GroupListBox()
        {
            this.SuspendLayout();
            this.Size = new Size(150, 250);
            chatVScroll = new ChatListVScroll(this);

            this.BackColor = Color.White;
            this.ForeColor = Color.DarkOrange;
            this.itemColor = Color.White;
            this.subItemColor = Color.White;
            this.itemMouseOnColor = Color.LightYellow;
            this.subItemMouseOnColor = Color.LightBlue;
            this.subItemSelectColor = Color.Wheat;
            this.arrowColor = Color.DarkGray;

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            this.ResumeLayout(false);
        }


        #endregion

        #region 属性

        private TextGroupItemCollection  _Items;
        /// <summary>
        /// 获取列表中所有组的机会.
        /// </summary>
        [DesignerSerializationVisibility (DesignerSerializationVisibility .Content )]
        public TextGroupItemCollection  Items
        {
            get
            {
                if (_Items == null) _Items = new TextGroupItemCollection(this);
                return _Items;
            }
        }

        private TextSubItem   _SelectSubItem;
        /// <summary>
        /// 获取当前选中的子项.
        /// </summary>
        [Browsable(false)]
        public TextSubItem   SelectSubItem
        {
            get
            {
                return _SelectSubItem;
            }
            set
            {
                _SelectSubItem = value;
            }
        }


        /// <summary>
        /// 获取或者设置滚动条背景色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("滚动条的背景颜色")]
        public Color ScrollBackColor
        {
            get { return chatVScroll.BackColor; }
            set { chatVScroll.BackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条滑块默认颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条滑块默认情况下的颜色")]
        public Color ScrollSliderDefaultColor
        {
            get { return chatVScroll.SliderDefaultColor; }
            set { chatVScroll.SliderDefaultColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条点下的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("滚动条滑块被点击或者鼠标移动到上面时候的颜色")]
        public Color ScrollSliderDownColor
        {
            get { return chatVScroll.SliderDownColor; }
            set { chatVScroll.SliderDownColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条箭头的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条箭头的背景颜色")]
        public Color ScrollArrowBackColor
        {
            get { return chatVScroll.ArrowBackColor; }
            set { chatVScroll.ArrowBackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条的箭头颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("滚动条箭头的颜色")]
        public Color ScrollArrowColor
        {
            get { return chatVScroll.ArrowColor; }
            set { chatVScroll.ArrowColor = value; }
        }

        private Color arrowColor;
        /// <summary>
        /// 获取或者设置列表项箭头的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("列表项上面的箭头的颜色")]
        public Color ArrowColor
        {
            get { return arrowColor; }
            set
            {
                if (arrowColor == value) return;
                arrowColor = value;
                this.Invalidate();
            }
        }

        private Color itemColor;
        /// <summary>
        /// 获取或者设置列表项背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表项的背景色")]
        public Color ItemColor
        {
            get { return itemColor; }
            set
            {
                if (itemColor == value) return;
                itemColor = value;
            }
        }

        private Color subItemColor;
        /// <summary>
        /// 获取或者设置子项的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表子项的背景色")]
        public Color SubItemColor
        {
            get { return subItemColor; }
            set
            {
                if (subItemColor == value) return;
                subItemColor = value;
            }
        }

        private Color itemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到列表项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("当鼠标移动到列表项上面的颜色")]
        public Color ItemMouseOnColor
        {
            get { return itemMouseOnColor; }
            set { itemMouseOnColor = value; }
        }

        private Color subItemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightBlue"), Category("ControlColor")]
        [Description("当鼠标移动到子项上面的颜色")]
        public Color SubItemMouseOnColor
        {
            get { return subItemMouseOnColor; }
            set { subItemMouseOnColor = value; }
        }

        private Color subItemSelectColor;
        /// <summary>
        /// 获取或者设置选中的子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Wheat"), Category("ControlColor")]
        [Description("当列表子项被选中时候的颜色")]
        public Color SubItemSelectColor
        {
            get { return subItemSelectColor; }
            set { subItemSelectColor = value; }
        }



        #endregion

        #region 公共函数
        protected virtual void DrawItem(Graphics g, TextGroupItem item, Rectangle rectItem, SolidBrush sb)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            if (item.Equals(m_mouseOnItem))           //根据列表项现在的状态绘制相应的背景色
                sb.Color = this.itemMouseOnColor;
            else
                sb.Color = this.itemColor;
            g.FillRectangle(sb, rectItem);


            if (item.IsOpen)
            {
                //如果展开的画绘制 展开的三角形
                sb.Color = this.arrowColor;
                g.FillPolygon(sb, new Point[] { 
                        new Point(2, rectItem.Top + 11), 
                        new Point(12, rectItem.Top + 11), 
                        new Point(7, rectItem.Top + 16) });
            }
            else
            {

                //绘制 不展开的三角形.
                sb.Color = this.arrowColor;
                g.FillPolygon(sb, new Point[] { 
                        new Point(5, rectItem.Top + 8), 
                        new Point(5, rectItem.Top + 18), 
                        new Point(10, rectItem.Top + 13) });
            }
            // 绘制文本.
            string strItem = "\t" + item.Text;
            sb.Color = this.ForeColor;
            sf.Alignment = StringAlignment.Near;
            g.DrawString(strItem, this.Font, sb, rectItem, sf);

          
        }

        protected virtual void DrawSubItem(Graphics g, TextSubItem subItem, ref Rectangle rectSubItem, SolidBrush sb)
        {
            //设置子项的颜色.
            if (subItem.Equals(SelectSubItem))
            {
                sb.Color = this.subItemSelectColor;
            }
            else if (subItem.Equals(m_mouseOnSubItem))
            {
                sb.Color = this.subItemMouseOnColor;
            }
            else
            {
                sb.Color = this.subItemColor;
            }

            //设置文本绘制的方式.
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Near;
            sf.FormatFlags = StringFormatFlags.FitBlackBox;
           
            //计算子项的区域的大小
            rectSubItem.Y = rectSubItem.Bottom + 1;            
            Rectangle rect = rectSubItem;
            rect.Width -= 20;
          
           
            //测量文本绘制区域的大小.
            SizeF size = g.MeasureString(subItem.Text, this.Font, rect.Width, sf);
            
            rectSubItem.Height = (int)size.Height+8;//调整子项区域的高度.
          
            //填充该区域.
            g.FillRectangle(sb, rectSubItem);
            //绘制文本.
            rect = TextRect(rectSubItem);//计算文本绘制区域.

            g.DrawString(subItem.Text, this.Font, Brushes.Black, rect, sf);
            subItem.Bounds = new Rectangle(rectSubItem.Location, rectSubItem.Size);
           
            
        }
        #endregion

        #region 私有函数

        /// <summary>
        /// 计算文本绘制区域.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Rectangle TextRect(Rectangle rect)
        {
            //
            int width=10;
            int heith=3;
            rect.X += width;
            rect.Width -= width+10;
            rect.Y += heith;
            rect.Height -= heith;
            return rect;
        }

        /// <summary>
        /// 清理鼠标所在项区域的阴影.
        /// </summary>
        private void ClearItemMouseOn()
        {
            if (m_mouseOnItem != null)
            {
                this.Invalidate(new Rectangle(
                    m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
                    m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
                m_mouseOnItem = null;
            }
        }
        
        /// <summary>
        /// 清理鼠标所在子项的区域的阴影.
        /// </summary>
        private void ClearSubItemMouseOn()
        {
            if (m_mouseOnSubItem != null)
            {
                this.Invalidate(new Rectangle(
                    m_mouseOnSubItem.Bounds.X, m_mouseOnSubItem.Bounds.Y - chatVScroll.Value,
                    m_mouseOnSubItem.Bounds.Width, m_mouseOnSubItem.Bounds.Height));
                m_mouseOnSubItem = null;
            }
        }
     

        #endregion


        #region 重载函数
        /// <summary>
        /// 绘制控件.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
           
            Graphics g = e.Graphics;
            g.TranslateTransform(0, -chatVScroll.Value);        //根据滚动条的值设置坐标偏移
            Rectangle rectItem = new Rectangle(0, 1, this.Width, 25);      //列表项区域
            Rectangle rectSubItem = new Rectangle(0, 26, this.Width, 0);   //子项区域
            SolidBrush sb = new SolidBrush(this.itemColor);
            
            try
            {
                foreach (var item in Items)
                {
                    //绘制组标题.
                    DrawItem(g, item, rectItem, sb);

                    if (item.IsOpen)
                    {
                        //如果列表需要展开,那么绘制子项.
                        rectSubItem.Y = rectItem.Bottom + 1;
                        rectSubItem.Height = 0;
                        foreach (var subitem in item.SubItems)
                        {
                            //绘制子项.
                            DrawSubItem(g, subitem, ref rectSubItem, sb);
                            //计算下一个子项的位置.
                            //rectSubItem.Y = rectSubItem.Bottom + 1;             //计算下一个子项的区域
                            //rectSubItem.Height = iconSizeMode;
                        }
                        //计算所有子项占领的区域 高度.以方便下一个项绘制时,计算它的位置.
                        rectItem.Height = rectSubItem.Bottom - rectItem.Top -1;

                    }
                    item.Bounds = new Rectangle(rectItem.Location, rectItem.Size);
                    rectItem.Y = rectItem.Bottom + 1;           //计算下一个列表项区域
                    rectItem.Height = 25;
                }
                g.ResetTransform();//重置坐标系

                chatVScroll.VirtualHeight = rectItem.Bottom - 26;   //绘制完成计算虚拟高度决定是否绘制滚动条
                if (chatVScroll.ShouldBeDraw)   //是否绘制滚动条
                    chatVScroll.ReDrawScroll(g);
           
            }
            catch (Exception)
            {
                
                throw;
            }
            finally 
            { 
                sb.Dispose();
            }
            base.OnPaint(e);
        }

        /// <summary>
        /// 鼠标按键按下,处理.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {        //如果左键在滚动条滑块上点击
                if (chatVScroll.SliderBounds.Contains(e.Location))
                {
                    chatVScroll.IsMouseDown = true;
                    chatVScroll.MouseDownY = e.Y;
                }
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                
            }

            this.Focus();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// 鼠标按键按下恢复.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                chatVScroll.IsMouseDown = false;
            base.OnMouseUp(e);
        }

        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0) chatVScroll.Value -= 50;
            if (e.Delta < 0) chatVScroll.Value += 50;
            base.OnMouseWheel(e);
        }

        /// <summary>
        /// 鼠标移动 处理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            m_ptMousePos = e.Location;
            if (chatVScroll.IsMouseDown)
            {          //如果滚动条的滑块处于被点击 那么移动
                chatVScroll.MoveSliderFromLocation(e.Y);
                return;
            }
            if (chatVScroll.ShouldBeDraw)
            {        
                //如果控件上有滚动条 判断鼠标是否在滚动条区域移动
                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {
                    ClearItemMouseOn();
                    ClearSubItemMouseOn();
                    if (chatVScroll.SliderBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnSlider = true;
                    else
                        chatVScroll.IsMouseOnSlider = false;
                    if (chatVScroll.UpBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnUp = true;
                    else
                        chatVScroll.IsMouseOnUp = false;
                    if (chatVScroll.DownBounds.Contains(m_ptMousePos))
                        chatVScroll.IsMouseOnDown = true;
                    else
                        chatVScroll.IsMouseOnDown = false;
                    return;
                }
                else
                    chatVScroll.ClearAllMouseOn();
            }

            //如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
            m_ptMousePos.Y += chatVScroll.Value;

            #region 新算法
            foreach (var item in Items)
            {
                if (item.Bounds.Contains(m_ptMousePos))
                {
                    if (item.IsOpen)
                    {
                        //子项是展开的.查找子项.
                        foreach (var subitem in item.SubItems)
                        {
                            //鼠标在子项内.
                            if (subitem.Bounds.Contains(m_ptMousePos))
                            {

                                if (subitem.Equals(m_mouseOnSubItem))//与上次的子项一致.
                                {
                                    return;
                                }
                                else
                                {
                                    //引发鼠标在子项上的事件.
                                    OnMouseOnSubItem(new GroupListBoxEventArgs(item, subitem));
                                }
                                ClearSubItemMouseOn();
                                ClearItemMouseOn();
                                m_mouseOnSubItem = subitem;
                                this.Invalidate(new Rectangle(
                                    subitem.Bounds.X, subitem.Bounds.Y - chatVScroll.Value,
                                    subitem.Bounds.Width, subitem.Bounds.Height));
                                return;
                            } 
                        }
                        ClearSubItemMouseOn();

                        //子项没有发现 那么判断是否在列表上面
                        if (new Rectangle(0, item.Bounds.Top - chatVScroll.Value, this.Width, 20).Contains(e.Location))
                        {
                            //在原先的项上.
                            if (item.Equals(m_mouseOnItem)) return;

                            //不再原先的项上
                            ClearItemMouseOn();
                            m_mouseOnItem = item;
                            this.Invalidate(new Rectangle(
                                 m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
                                    m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
                            return;
                        }
                    }
                    else
                    {
                        if (item.Equals(m_mouseOnItem)) return;
                        ClearItemMouseOn();
                        ClearSubItemMouseOn();
                        m_mouseOnItem = item;
                        //绘制指定的区域.
                        this.Invalidate(new Rectangle(
                                m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
                                m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
                        return;
                    }
                }
            }
            #endregion

            #region  老算法

            //for (int i = 0, Len = Items.Count; i < Len; i++)
            //{
            //    然后判断鼠标是否移动到某一列表项或者子项
            //    if (Items[i].Bounds.Contains(m_ptMousePos))
            //    {
            //        if (Items[i].IsOpen)
            //        {
            //            如果展开 判断鼠标是否在某一子项上面
            //            for (int j = 0, lenSubItem = Items[i].SubItems.Count; j < lenSubItem; j++)
            //            {
            //                if (Items[i].SubItems[j].Bounds.Contains(m_ptMousePos))
            //                {
            //                    if (m_mouseOnSubItem != null)
            //                    {
            //                        如果当前鼠标下子项不为空
            //                        if (Items[i].SubItems[j].HeadRect.Contains(m_ptMousePos))
            //                        {     //判断鼠标是否在头像内
            //                            //if (!m_bOnMouseEnterHeaded)
            //                            //{       //如果没有触发进入事件 那么触发用户绑定的事件
            //                            //   // OnMouseEnterHead(new ChatListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
            //                            //    //m_bOnMouseEnterHeaded = true;
            //                            //}
            //                        }
            //                        else
            //                        {
            //                            //if (m_bOnMouseEnterHeaded)
            //                            //{        //如果已经执行过进入事件 那触发用户绑定的离开事件
            //                            //    OnMouseLeaveHead(new ChatListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
            //                            //    m_bOnMouseEnterHeaded = false;
            //                            //}
            //                        }
            //                    }
            //                    if (Items[i].SubItems[j].Equals(m_mouseOnSubItem))
            //                    {
            //                        return;
            //                    }
            //                    ClearSubItemMouseOn();
            //                    ClearItemMouseOn();
            //                    m_mouseOnSubItem = Items[i].SubItems[j];
            //                    this.Invalidate(new Rectangle(
            //                        m_mouseOnSubItem.Bounds.X, m_mouseOnSubItem.Bounds.Y - chatVScroll.Value,
            //                        m_mouseOnSubItem.Bounds.Width, m_mouseOnSubItem.Bounds.Height));
            //                    this.Invalidate();
            //                    return;
            //                }
            //            }
            //            ClearSubItemMouseOn();      //循环做完没发现子项 那么判断是否在列表上面
            //            if (new Rectangle(0, Items[i].Bounds.Top - chatVScroll.Value, this.Width, 20).Contains(e.Location))
            //            {
            //                if (Items[i].Equals(m_mouseOnItem))
            //                    return;
            //                ClearItemMouseOn();
            //                m_mouseOnItem = Items[i];
            //                this.Invalidate(new Rectangle(
            //                    m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
            //                    m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
            //                this.Invalidate();
            //                return;
            //            }
            //        }
            //        else
            //        {        //如果列表项没有展开 重绘列表项
            //            if (Items[i].Equals(m_mouseOnItem))
            //                return;
            //            ClearItemMouseOn();
            //            ClearSubItemMouseOn();
            //            m_mouseOnItem = Items[i];
            //            this.Invalidate(new Rectangle(
            //                    m_mouseOnItem.Bounds.X, m_mouseOnItem.Bounds.Y - chatVScroll.Value,
            //                    m_mouseOnItem.Bounds.Width, m_mouseOnItem.Bounds.Height));
            //            this.Invalidate();
            //            return;
            //        }
            //    }
            //}
            #endregion


            //若循环结束 既不在列表上也不再子项上 清空上面的颜色
            ClearItemMouseOn();
            ClearSubItemMouseOn();
            base.OnMouseMove(e);
        }


        protected override void OnMouseLeave(EventArgs e)
        {
            ClearItemMouseOn();
            ClearSubItemMouseOn();
            chatVScroll.ClearAllMouseOn();
            //if (m_bOnMouseEnterHeaded)
            //{        //如果已经执行过进入事件 那触发用户绑定的离开事件
            //    OnMouseLeaveHead(new ChatListEventArgs(this.m_mouseOnSubItem, this.selectSubItem));
            //    m_bOnMouseEnterHeaded = false;
            //}
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (chatVScroll.IsMouseDown) return;    //MouseUp事件触发在Click后 滚动条滑块为点下状态 单击无效
            if (chatVScroll.ShouldBeDraw)
            {         //如果有滚动条 判断是否在滚动条类点击
                if (chatVScroll.Bounds.Contains(m_ptMousePos))
                {        //判断在滚动条那个位置点击
                    if (chatVScroll.UpBounds.Contains(m_ptMousePos))
                        chatVScroll.Value -= 50;
                    else if (chatVScroll.DownBounds.Contains(m_ptMousePos))
                        chatVScroll.Value += 50;
                    else if (!chatVScroll.SliderBounds.Contains(m_ptMousePos))
                        chatVScroll.MoveSliderToLocation(m_ptMousePos.Y);
                    return;
                }
            }//否则 如果在列表上点击 展开或者关闭 在子项上面点击则选中
            foreach (var item in Items)
            {
                if (item.Bounds.Contains(m_ptMousePos))
                {
                    if (item.IsOpen)
                    {
                        foreach (var subItem in item.SubItems)
                        {
                            if (subItem.Bounds.Contains(m_ptMousePos))
                            {
                                if (subItem.Equals(SelectSubItem))
                                    return;
                                SelectSubItem= subItem;
                                this.Invalidate();
                                OnClickSubItem(new GroupListBoxEventArgs(m_mouseOnItem, SelectSubItem));
                                return;
                            }
                        }
                        if (new Rectangle(0, item.Bounds.Top, this.Width, 20).Contains(m_ptMousePos))
                        {
                            SelectSubItem= null;
                            item.IsOpen = !item.IsOpen;
                            this.Invalidate();
                            return;
                        }
                    }
                    else
                    {
                        SelectSubItem = null;
                        item.IsOpen = !item.IsOpen;
                        this.Invalidate();
                        return;
                    }
                }
            }
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            this.OnClick(e);        //双击时 再次触发一下单击事件  不然双击列表项 相当于只点击了一下列表项
            if (chatVScroll.Bounds.Contains(PointToClient(MousePosition))) return;  //如果双击在滚动条上返回
            if (this.SelectSubItem!= null)     //如果选中项不为空 那么触发用户绑定的双击事件
                OnDoubleClickSubItem(new GroupListBoxEventArgs(this.m_mouseOnItem,this .SelectSubItem));
            base.OnDoubleClick(e);
        }
       
        #endregion


        /// <summary>
        /// TextGroupItem的集合.
        /// </summary>
        public class TextGroupItemCollection:List<TextGroupItem>
        {
            private GroupListBox _Owner;

            public TextGroupItemCollection(GroupListBox owner)
            {
                if (owner == null) throw new NotImplementedException("owner cannot be null");
                _Owner = owner;
            }

            public new  void Add(TextGroupItem item)
            {
                if (item == null) throw new ArgumentNullException("Item cannot be null");
                item.OwnerGroupListBox = _Owner;
                base.Add(item);
                this._Owner.Invalidate();
            }

            public new void AddRange(IEnumerable<TextGroupItem> items)
            {
                foreach (var item in items)
                {
                    item.OwnerGroupListBox = _Owner;
                    base.Add(item);
                }
                this._Owner.Invalidate();
            }

            public new void Remove(TextGroupItem item)
            {
                base.Remove(item);
                this._Owner.Invalidate();
            }

            public new void RemoveAt(int index)
            {
                base.RemoveAt(index);
                this._Owner.Invalidate();
                
            }




        }

    }
}
