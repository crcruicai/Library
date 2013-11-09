
#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/05 8:46:48
 * 描述说明：QQ 好友列表控件
 * 
 * 更改历史：
 * 
 * 2013-11-04 修改 变量名,使之符合规范.
 * 2013-10-21 添加事件:ContextSubItem 当右键点击子项时,引发事件.
 * 
 * *******************************************************/
#endregion

#region Using
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Diagnostics;

#endregion Using

namespace CRC.Controls
{
    /// <summary>
    /// QQ 好友列表.
    /// </summary>
    public partial class ChatListBox : Control
    {
        #region 构造函数
        /// <summary>
        /// QQ 好友列表.
        /// </summary>
        public ChatListBox()
        {
            InitializeComponent();
            this.Size = new Size(150, 250);
            this._IconSizeMode = ChatListItemIcon.Small;
            this._Items = new ChatListItemCollection(this);
            _ChatVScroll = new ChatListVScroll(this);

            this.BackColor = Color.White;
            this.ForeColor = Color.DarkOrange;
            this._ItemColor = Color.White;
            this._SubItemColor = Color.White;
            this._ItemMouseOnColor = Color.LightYellow;
            this._SubItemMouseOnColor = Color.LightBlue;
            this._SubItemSelectColor = Color.Wheat;
            this._ArrowColor = Color.DarkGray;

            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
        #endregion

        #region Properties

        private ChatListItemIcon _IconSizeMode;
        /// <summary>
        /// 获取或者设置列表的图标模式
        /// </summary>
        [DefaultValue(ChatListItemIcon.Small)]
        public ChatListItemIcon IconSizeMode
        {
            get { return _IconSizeMode; }
            set
            {
                if (_IconSizeMode == value) return;
                _IconSizeMode = value;
                this.Invalidate();
            }
        }

        private ChatListItemCollection _Items;
        /// <summary>
        /// 获取列表中所有列表项的集合
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ChatListItemCollection Items
        {
            get
            {
                if (_Items == null)
                    _Items = new ChatListItemCollection(this);
                return _Items;
            }
        }

        private ChatListSubItem _SelectSubItem;
        /// <summary>
        /// 当前选中的子项
        /// </summary>
        [Browsable(false)]
        public ChatListSubItem SelectSubItem
        {
            get { return _SelectSubItem; }
            set
            {
                if (value != null)
                {
                    _SelectSubItem = value;
                    ChatListItem item=_SelectSubItem.OwnerListItem;
                    if (item != null)
                    {
                        //滑动 项
                        if (!item.IsOpen)//该项并没有展开.
                        {
                            //计算所在组 Y的位置.
                            int pos = item.Bounds.Y + item.Bounds.Height;
                           
                            //获取 项的索引,用于计算位置.
                            int index =item.SubItems.IndexOf(_SelectSubItem);
                            pos += (index) * (int)ChatListItemIcon .Small +(int)ChatListItemIcon .Large;
                            //滑动到该位置.
                            _ChatVScroll.SetScroll(pos);
                           
                            //设置 组展开.
                            item.IsOpen = true;
                        }
                        else
                        {
                            //组 已经展开,直接获取项的位置.
                            _ChatVScroll.Value = _SelectSubItem.Bounds.Y ;
                        }
                        
                        
                    }
                    this.Invalidate();

                   
                   
                }
            }
        }
        /// <summary>
        /// 获取或者设置滚动条背景色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("滚动条的背景颜色")]
        public Color ScrollBackColor
        {
            get { return _ChatVScroll.BackColor; }
            set { _ChatVScroll.BackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条滑块默认颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条滑块默认情况下的颜色")]
        public Color ScrollSliderDefaultColor
        {
            get { return _ChatVScroll.SliderDefaultColor; }
            set { _ChatVScroll.SliderDefaultColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条点下的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("滚动条滑块被点击或者鼠标移动到上面时候的颜色")]
        public Color ScrollSliderDownColor
        {
            get { return _ChatVScroll.SliderDownColor; }
            set { _ChatVScroll.SliderDownColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条箭头的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "Gray"), Category("ControlColor")]
        [Description("滚动条箭头的背景颜色")]
        public Color ScrollArrowBackColor
        {
            get { return _ChatVScroll.ArrowBackColor; }
            set { _ChatVScroll.ArrowBackColor = value; }
        }
        /// <summary>
        /// 获取或者设置滚动条的箭头颜色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("滚动条箭头的颜色")]
        public Color ScrollArrowColor
        {
            get { return _ChatVScroll.ArrowColor; }
            set { _ChatVScroll.ArrowColor = value; }
        }

        private Color _ArrowColor;
        /// <summary>
        /// 获取或者设置列表项箭头的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "DarkGray"), Category("ControlColor")]
        [Description("列表项上面的箭头的颜色")]
        public Color ArrowColor
        {
            get { return _ArrowColor; }
            set
            {
                if (_ArrowColor == value) return;
                _ArrowColor = value;
                this.Invalidate();
            }
        }

        private Color _ItemColor;
        /// <summary>
        /// 获取或者设置列表项背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表项的背景色")]
        public Color ItemColor
        {
            get { return _ItemColor; }
            set
            {
                if (_ItemColor == value) return;
                _ItemColor = value;
            }
        }

        private Color _SubItemColor;
        /// <summary>
        /// 获取或者设置子项的背景色
        /// </summary>
        [DefaultValue(typeof(Color), "White"), Category("ControlColor")]
        [Description("列表子项的背景色")]
        public Color SubItemColor
        {
            get { return _SubItemColor; }
            set
            {
                if (_SubItemColor == value) return;
                _SubItemColor = value;
            }
        }

        private Color _ItemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到列表项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightYellow"), Category("ControlColor")]
        [Description("当鼠标移动到列表项上面的颜色")]
        public Color ItemMouseOnColor
        {
            get { return _ItemMouseOnColor; }
            set { _ItemMouseOnColor = value; }
        }

        private Color _SubItemMouseOnColor;
        /// <summary>
        /// 获取或者设置当鼠标移动到子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "LightBlue"), Category("ControlColor")]
        [Description("当鼠标移动到子项上面的颜色")]
        public Color SubItemMouseOnColor
        {
            get { return _SubItemMouseOnColor; }
            set { _SubItemMouseOnColor = value; }
        }

        private Color _SubItemSelectColor;
        /// <summary>
        /// 获取或者设置选中的子项的颜色
        /// </summary>
        [DefaultValue(typeof(Color), "Wheat"), Category("ControlColor")]
        [Description("当列表子项被选中时候的颜色")]
        public Color SubItemSelectColor
        {
            get { return _SubItemSelectColor; }
            set { _SubItemSelectColor = value; }
        }

        #endregion

        #region Events

        public delegate void ChatListEventHandler(object sender, ChatListEventArgs e);
        /// <summary>
        /// 当鼠标双击子项时,引发事件.
        /// </summary>
        public event ChatListEventHandler DoubleClickSubItem;
        /// <summary>
        /// 当鼠标进入子项的图像时,引发事件.
        /// </summary>
        public event ChatListEventHandler MouseEnterHead;
        /// <summary>
        /// 当鼠标离开子项的图像时,引发事件.
        /// </summary>
        public event ChatListEventHandler MouseLeaveHead;

        /// <summary>
        /// 当鼠标右键点击子项时,引发事件.
        /// </summary>
        public event ChatListEventHandler ContextSubItem;

        /// <summary>
        /// 引发ContextSubItem事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnContextSubItem(ChatListEventArgs e)
        {
            if (ContextSubItem != null)
            {
                ContextSubItem(this, e);
            }
        }



        /// <summary>
        /// 引发DoubleClickSubItem事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDoubleClickSubItem(ChatListEventArgs e)
        {
            if (this.DoubleClickSubItem != null)
                DoubleClickSubItem(this, e);
        }

        /// <summary>
        /// 引发MouseEnterHead事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseEnterHead(ChatListEventArgs e)
        {
            if (this.MouseEnterHead != null)
                MouseEnterHead(this, e);
        }
        /// <summary>
        /// 引发MouseLeaveHead事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseLeaveHead(ChatListEventArgs e)
        {
            if (this.MouseLeaveHead != null)
                MouseLeaveHead(this, e);
        }

        #endregion

        #region 变量与字段
        /// <summary>
        /// 鼠标的位置
        /// </summary>
        private Point _MousePos;             
        /// <summary>
        /// 滚动条
        /// </summary>
        private ChatListVScroll _ChatVScroll;    
        /// <summary>
        /// 鼠标所在的项.
        /// </summary>
        private ChatListItem _MouseOnItem;
        /// <summary>
        /// 确定用户绑定事件是否被触发
        /// </summary>
        private bool _IsMouseEnterHeaded;     
        /// <summary>
        /// 鼠标所在的子项.
        /// </summary>
        private ChatListSubItem _MouseOnSubItem;

        /// <summary>
        /// 鼠标按下的键
        /// </summary>
        private MouseButtons _MouseClickButton;
        #endregion

        #region 重载函数
        /// <summary>
        /// 
        /// </summary>
        protected override void OnCreateControl()
        {
            //支持 动态闪烁.
            Thread threadInvalidate = new Thread(new ThreadStart(() =>
            {
                Rectangle rectReDraw = new Rectangle(0, 0, this.Width, this.Height);
                while (true)
                {          //后台检测要闪动的图标然后重绘
                    for (int i = 0, lenI = this._Items.Count; i < lenI; i++)
                    {
                        if (_Items[i].IsOpen)
                        {
                            for (int j = 0, lenJ = _Items[i].SubItems.Count; j < lenJ; j++)
                            {
                                if (_Items[i].SubItems[j].IsTwinkle)
                                {
                                    _Items[i].SubItems[j].IsTwinkleHide = !_Items[i].SubItems[j].IsTwinkleHide;
                                    rectReDraw.Y = _Items[i].SubItems[j].Bounds.Y - _ChatVScroll.Value;
                                    rectReDraw.Height = _Items[i].SubItems[j].Bounds.Height;
                                    this.Invalidate(rectReDraw);
                                }
                            }
                        }
                        else
                        {
                            rectReDraw.Y = _Items[i].Bounds.Y - _ChatVScroll.Value;
                            rectReDraw.Height = _Items[i].Bounds.Height;
                            if (_Items[i].TwinkleSubItemNumber > 0)
                            {
                                _Items[i].IsTwinkleHide = !_Items[i].IsTwinkleHide;
                                this.Invalidate(rectReDraw);
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
            }));
            threadInvalidate.IsBackground = true;
            threadInvalidate.Start();
            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.TranslateTransform(0, -_ChatVScroll.Value);        //根据滚动条的值设置坐标偏移
           
            Rectangle rectItem = new Rectangle(0, 1, this.Width, 25);                       //列表项区域
            Rectangle rectSubItem = new Rectangle(0, 26, this.Width, (int)_IconSizeMode);    //子项区域
            SolidBrush sb = new SolidBrush(this._ItemColor);
            try
            {

                for (int i = 0, lenItem = _Items.Count; i < lenItem; i++)
                {
                    DrawItem(g, _Items[i], rectItem, sb);        //绘制列表项
                    if (_Items[i].IsOpen)
                    {                      //如果列表项展开绘制子项
                        rectSubItem.Y = rectItem.Bottom + 1;
                        for (int j = 0, lenSubItem = _Items[i].SubItems.Count; j < lenSubItem; j++)
                        {
                            DrawSubItem(g, _Items[i].SubItems[j], ref rectSubItem, sb);  //绘制子项
                            rectSubItem.Y = rectSubItem.Bottom + 1;             //计算下一个子项的区域
                            rectSubItem.Height = (int)_IconSizeMode;
                        }
                        rectItem.Height = rectSubItem.Bottom - rectItem.Top - (int)_IconSizeMode - 1;
                    }
                    _Items[i].Bounds = new Rectangle(rectItem.Location, rectItem.Size);
                    rectItem.Y = rectItem.Bottom + 1;           //计算下一个列表项区域
                    rectItem.Height = 25;
                }
                g.ResetTransform();             //重置坐标系
                _ChatVScroll.VirtualHeight = rectItem.Bottom - 26;   //绘制完成计算虚拟高度决定是否绘制滚动条
                if (_ChatVScroll.ShouldBeDraw)   //是否绘制滚动条
                    _ChatVScroll.ReDrawScroll(g);
            }
            finally 
            { 
                sb.Dispose();
            }
            ControlPaint.DrawBorder3D(g, ClientRectangle, Border3DStyle.Bump);
            base.OnPaint(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0) _ChatVScroll.Value -= 50;
            if (e.Delta < 0) _ChatVScroll.Value += 50;
            Debug.WriteLine(_ChatVScroll.Value);
            base.OnMouseWheel(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _MouseClickButton = e.Button;
            if (e.Button == MouseButtons.Left)
            {        //如果左键在滚动条滑块上点击
                if (_ChatVScroll.SliderBounds.Contains(e.Location))
                {
                    _ChatVScroll.IsMouseDown = true;
                    _ChatVScroll.MouseDownY = e.Y;
                }
            }
            this.Focus();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _ChatVScroll.IsMouseDown = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _MousePos = e.Location;
            if (_ChatVScroll.IsMouseDown)
            {          //如果滚动条的滑块处于被点击 那么移动
                _ChatVScroll.MoveSliderFromLocation(e.Y);
                return;
            }
            if (_ChatVScroll.ShouldBeDraw)
            {         //如果控件上有滚动条 判断鼠标是否在滚动条区域移动
                if (_ChatVScroll.Bounds.Contains(_MousePos))
                {
                    ClearItemMouseOn();
                    ClearSubItemMouseOn();
                    if (_ChatVScroll.SliderBounds.Contains(_MousePos))
                        _ChatVScroll.IsMouseOnSlider = true;
                    else
                        _ChatVScroll.IsMouseOnSlider = false;
                    if (_ChatVScroll.UpBounds.Contains(_MousePos))
                        _ChatVScroll.IsMouseOnUp = true;
                    else
                        _ChatVScroll.IsMouseOnUp = false;
                    if (_ChatVScroll.DownBounds.Contains(_MousePos))
                        _ChatVScroll.IsMouseOnDown = true;
                    else
                        _ChatVScroll.IsMouseOnDown = false;
                    return;
                }
                else
                    _ChatVScroll.ClearAllMouseOn();
            }
            _MousePos.Y += _ChatVScroll.Value;        //如果不在滚动条范围类 那么根据滚动条当前值计算虚拟的一个坐标
            for (int i = 0, Len = _Items.Count; i < Len; i++)
            {      //然后判断鼠标是否移动到某一列表项或者子项
                if (_Items[i].Bounds.Contains(_MousePos))
                {
                    if (_Items[i].IsOpen)
                    {              //如果展开 判断鼠标是否在某一子项上面
                        for (int j = 0, lenSubItem = _Items[i].SubItems.Count; j < lenSubItem; j++)
                        {
                            if (_Items[i].SubItems[j].Bounds.Contains(_MousePos))
                            {
                                if (_MouseOnSubItem != null)
                                {             //如果当前鼠标下子项不为空
                                    if (_Items[i].SubItems[j].HeadRect.Contains(_MousePos))
                                    {     //判断鼠标是否在头像内
                                        if (!_IsMouseEnterHeaded)
                                        {       //如果没有触发进入事件 那么触发用户绑定的事件
                                            OnMouseEnterHead(new ChatListEventArgs(this._MouseOnSubItem, this._SelectSubItem));
                                            _IsMouseEnterHeaded = true;
                                        }
                                    }
                                    else
                                    {
                                        if (_IsMouseEnterHeaded)
                                        {        //如果已经执行过进入事件 那触发用户绑定的离开事件
                                            OnMouseLeaveHead(new ChatListEventArgs(this._MouseOnSubItem, this._SelectSubItem));
                                            _IsMouseEnterHeaded = false;
                                        }
                                    }
                                }
                                if (_Items[i].SubItems[j].Equals(_MouseOnSubItem))
                                {
                                    return;
                                }
                                ClearSubItemMouseOn();
                                ClearItemMouseOn();
                                _MouseOnSubItem = _Items[i].SubItems[j];
                                this.Invalidate(new Rectangle(
                                    _MouseOnSubItem.Bounds.X, _MouseOnSubItem.Bounds.Y - _ChatVScroll.Value,
                                    _MouseOnSubItem.Bounds.Width, _MouseOnSubItem.Bounds.Height));
                                //this.Invalidate();
                                return;
                            }
                        }
                        ClearSubItemMouseOn();      //循环做完没发现子项 那么判断是否在列表上面
                        if (new Rectangle(0, _Items[i].Bounds.Top - _ChatVScroll.Value, this.Width, 20).Contains(e.Location))
                        {
                            if (_Items[i].Equals(_MouseOnItem))
                                return;
                            ClearItemMouseOn();
                            _MouseOnItem = _Items[i];
                            this.Invalidate(new Rectangle(
                                _MouseOnItem.Bounds.X, _MouseOnItem.Bounds.Y - _ChatVScroll.Value,
                                _MouseOnItem.Bounds.Width, _MouseOnItem.Bounds.Height));
                            //this.Invalidate();
                            return;
                        }
                    }
                    else
                    {        //如果列表项没有展开 重绘列表项
                        if (_Items[i].Equals(_MouseOnItem))
                            return;
                        ClearItemMouseOn();
                        ClearSubItemMouseOn();
                        _MouseOnItem = _Items[i];
                        this.Invalidate(new Rectangle(
                                _MouseOnItem.Bounds.X, _MouseOnItem.Bounds.Y - _ChatVScroll.Value,
                                _MouseOnItem.Bounds.Width, _MouseOnItem.Bounds.Height));
                        //this.Invalidate();
                        return;
                    }
                }
            }//若循环结束 既不在列表上也不再子项上 清空上面的颜色
            ClearItemMouseOn();
            ClearSubItemMouseOn();
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            ClearItemMouseOn();
            ClearSubItemMouseOn();
            _ChatVScroll.ClearAllMouseOn();
            if (_IsMouseEnterHeaded)
            {        //如果已经执行过进入事件 那触发用户绑定的离开事件
                OnMouseLeaveHead(new ChatListEventArgs(this._MouseOnSubItem, this._SelectSubItem));
                _IsMouseEnterHeaded = false;
            }
            base.OnMouseLeave(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (_ChatVScroll.IsMouseDown) return;    //MouseUp事件触发在Click后 滚动条滑块为点下状态 单击无效
            if (_ChatVScroll.ShouldBeDraw)
            {         //如果有滚动条 判断是否在滚动条类点击
                if (_ChatVScroll.Bounds.Contains(_MousePos))
                {        //判断在滚动条那个位置点击
                    if (_ChatVScroll.UpBounds.Contains(_MousePos))
                        _ChatVScroll.Value -= 50;
                    else if (_ChatVScroll.DownBounds.Contains(_MousePos))
                        _ChatVScroll.Value += 50;
                    else if (!_ChatVScroll.SliderBounds.Contains(_MousePos))
                        _ChatVScroll.MoveSliderToLocation(_MousePos.Y);
                    return;
                }
            }//否则 如果在列表上点击 展开或者关闭 在子项上面点击则选中
            foreach (ChatListItem item in _Items)
            {
                if (item.Bounds.Contains(_MousePos))
                {
                    if (item.IsOpen)
                    {
                        foreach (ChatListSubItem subItem in item.SubItems)
                        {
                            if (subItem.Bounds.Contains(_MousePos))
                            {
                                if (!subItem.Equals(_SelectSubItem))
                                {
                                    _SelectSubItem = subItem;
                                    this.Invalidate();
                                }
                                if (_MouseClickButton == System.Windows.Forms.MouseButtons.Right)
                                {
                                    OnContextSubItem(new ChatListEventArgs(_MouseOnSubItem , subItem));
                                    Debug.WriteLine("子项被选中");
                                }
                                return;
                            }
                        }
                        if (new Rectangle(0, item.Bounds.Top, this.Width, 20).Contains(_MousePos))
                        {
                            _SelectSubItem = null;
                            item.IsOpen = !item.IsOpen;
                            this.Invalidate();
                            return;
                        }
                    }
                    else
                    {
                        _SelectSubItem = null;
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
            if (_ChatVScroll.Bounds.Contains(PointToClient(MousePosition))) return;  //如果双击在滚动条上返回
            if (this._SelectSubItem != null)     //如果选中项不为空 那么触发用户绑定的双击事件
                OnDoubleClickSubItem(new ChatListEventArgs(this._MouseOnSubItem, this._SelectSubItem));
            base.OnDoubleClick(e);
        }

        #endregion

        #region 绘制函数

        /// <summary>
        /// 绘制列表组标题项
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="item">要绘制的列表项</param>
        /// <param name="rectItem">该列表项的区域</param>
        /// <param name="sb">画刷</param>
        protected virtual void DrawItem(Graphics g, ChatListItem item, Rectangle rectItem, SolidBrush sb)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            if (item.Equals(_MouseOnItem))           //根据列表项现在的状态绘制相应的背景色
                sb.Color = this._ItemMouseOnColor;
            else
                sb.Color = this._ItemColor;
            g.FillRectangle(sb, rectItem);


            if (item.IsOpen)
            {   
                //如果展开的画绘制 展开的三角形
                sb.Color = this._ArrowColor;
                g.FillPolygon(sb, new Point[] { 
                        new Point(2, rectItem.Top + 11), 
                        new Point(12, rectItem.Top + 11), 
                        new Point(7, rectItem.Top + 16) });
            }
            else
            {                                   
                //如果没有展开判断该列表项下面的子项闪动的个数
                if (item.TwinkleSubItemNumber > 0)
                {    //如果列表项下面有子项闪动 那么判断闪动状态 是否绘制或者不绘制
                    if (item.IsTwinkleHide)             //该布尔值 在线程中不停 取反赋值
                        return;
                }

                //绘制 不展开的三角形.
                sb.Color = this._ArrowColor;
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

            sf.Alignment = StringAlignment.Far;
            g.DrawString("[" + item.SubItems.GetOnLineNumber() + "/" + item.SubItems.Count + "]", this.Font, sb,
                new Rectangle(rectItem.X, rectItem.Y, rectItem.Width - 15, rectItem.Height), sf);
        }



        /// <summary>
        /// 绘制列表子项
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        /// <param name="sb">画刷</param>
        protected virtual void DrawSubItem(Graphics g, ChatListSubItem subItem, ref Rectangle rectSubItem, SolidBrush sb)
        {
            if (subItem.Equals(_SelectSubItem))
            {        //判断改子项是否被选中
                rectSubItem.Height = (int)ChatListItemIcon.Large;   //如果选中则绘制成大图标
                sb.Color = this._SubItemSelectColor;
                g.FillRectangle(sb, rectSubItem);
                DrawHeadImage(g, subItem, rectSubItem);         //绘制头像
                DrawLargeSubItem(g, subItem, rectSubItem);      //绘制大图标 显示的个人信息
                subItem.Bounds = new Rectangle(rectSubItem.Location, rectSubItem.Size);
                return;
            }
            else if (subItem.Equals(_MouseOnSubItem))
                sb.Color = this._SubItemMouseOnColor;
            else
                sb.Color = this._SubItemColor;
            g.FillRectangle(sb, rectSubItem);
            DrawHeadImage(g, subItem, rectSubItem);

            if (_IconSizeMode == ChatListItemIcon.Large)         //没有选中则根据 图标模式绘制
                DrawLargeSubItem(g, subItem, rectSubItem);
            else
                DrawSmallSubItem(g, subItem, rectSubItem);

            subItem.Bounds = new Rectangle(rectSubItem.Location, rectSubItem.Size);
        }
        /// <summary>
        /// 绘制列表子项的头像
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制头像的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        protected virtual void DrawHeadImage(Graphics g, ChatListSubItem subItem, Rectangle rectSubItem)
        {
            if (subItem.IsTwinkle)
            {        //判断改头像是否闪动
                if (subItem.IsTwinkleHide)  //同理该值 在线程中 取反赋值
                    return;
            }

            int imageHeight = rectSubItem.Height - 10;      //根据子项的大小计算头像的区域
            subItem.HeadRect = new Rectangle(5, rectSubItem.Top + 5, imageHeight, imageHeight);

            if (subItem.HeadImage == null)                 //如果头像位空 用默认资源给定的头像
                subItem.HeadImage = global::CRC.Properties.Resources._null;
            if (subItem.Status == ChatListSubItem.UserStatus.OffLine)
                g.DrawImage(subItem.GetDarkImage(), subItem.HeadRect);
            else
            {
                g.DrawImage(subItem.HeadImage, subItem.HeadRect);       //如果在线根据在想状态绘制小图标
                if (subItem.Status == ChatListSubItem.UserStatus.QMe)
                    g.DrawImage(global::CRC.Properties.Resources.QMe, new Rectangle(subItem.HeadRect.Right - 10, subItem.HeadRect.Bottom - 10, 11, 11));
                if (subItem.Status == ChatListSubItem.UserStatus.Away)
                    g.DrawImage(global::CRC.Properties.Resources.Away, new Rectangle(subItem.HeadRect.Right - 10, subItem.HeadRect.Bottom - 10, 11, 11));
                if (subItem.Status == ChatListSubItem.UserStatus.Busy)
                    g.DrawImage(global::CRC.Properties.Resources.Busy, new Rectangle(subItem.HeadRect.Right - 10, subItem.HeadRect.Bottom - 10, 11, 11));
                if (subItem.Status == ChatListSubItem.UserStatus.DontDisturb)
                    g.DrawImage(global::CRC.Properties.Resources.Dont_Disturb, new Rectangle(subItem.HeadRect.Right - 10, subItem.HeadRect.Bottom - 10, 11, 11));
            }

            if (subItem.Equals(_SelectSubItem))              //根据是否选中头像绘制头像的外边框
                g.DrawRectangle(Pens.Cyan, subItem.HeadRect);
            else
                g.DrawRectangle(Pens.Gray, subItem.HeadRect);
        }
        /// <summary>
        /// 绘制大图标模式的个人信息
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制信息的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        protected virtual void DrawLargeSubItem(Graphics g, ChatListSubItem subItem, Rectangle rectSubItem)
        {
            rectSubItem.Height = (int)ChatListItemIcon.Large;       //重新赋值一个高度
            string strDraw = subItem.DisplayName;
            Size szFont = TextRenderer.MeasureText(strDraw, this.Font);
            if (szFont.Width > 0)
            {             //判断是否有备注名称
                g.DrawString(strDraw, this.Font, Brushes.Black, rectSubItem.Height, rectSubItem.Top + 5);
                g.DrawString("(" + subItem.NicName + ")",
                    this.Font, Brushes.Gray, rectSubItem.Height + szFont.Width, rectSubItem.Top + 5);
            }
            else
            {                            //如果没有备注名称 这直接绘制昵称
                g.DrawString(subItem.NicName, this.Font, Brushes.Black, rectSubItem.Height, rectSubItem.Top + 5);
            }           //绘制个人签名
            g.DrawString(subItem.PersonalMsg, this.Font, Brushes.Gray, rectSubItem.Height, rectSubItem.Top + 5 + this.Font.Height);
        }
        /// <summary>
        /// 绘制小图标模式的个人信息
        /// </summary>
        /// <param name="g">绘图表面</param>
        /// <param name="subItem">要绘制信息的子项</param>
        /// <param name="rectSubItem">该子项的区域</param>
        protected virtual void DrawSmallSubItem(Graphics g, ChatListSubItem subItem, Rectangle rectSubItem)
        {
            rectSubItem.Height = (int)ChatListItemIcon.Small;               //重新赋值一个高度
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            string strDraw = subItem.DisplayName;
            if (string.IsNullOrEmpty(strDraw)) strDraw = subItem.NicName;   //如果没有备注绘制昵称
            Size szFont = TextRenderer.MeasureText(strDraw, this.Font);
            sf.SetTabStops(0.0F, new float[] { rectSubItem.Height });
            g.DrawString("\t" + strDraw, this.Font, Brushes.Black, rectSubItem, sf);
            sf.SetTabStops(0.0F, new float[] { rectSubItem.Height + 5 + szFont.Width });
            g.DrawString("\t" + subItem.PersonalMsg, this.Font, Brushes.Gray, rectSubItem, sf);
        }
        #endregion

        #region 私有函数
        private void ClearItemMouseOn()
        {
            if (_MouseOnItem != null)
            {
                this.Invalidate(new Rectangle(
                    _MouseOnItem.Bounds.X, _MouseOnItem.Bounds.Y - _ChatVScroll.Value,
                    _MouseOnItem.Bounds.Width, _MouseOnItem.Bounds.Height));
                _MouseOnItem = null;
            }
        }

        private void ClearSubItemMouseOn()
        {
            if (_MouseOnSubItem != null)
            {
                this.Invalidate(new Rectangle(
                    _MouseOnSubItem.Bounds.X, _MouseOnSubItem.Bounds.Y - _ChatVScroll.Value,
                    _MouseOnSubItem.Bounds.Width, _MouseOnSubItem.Bounds.Height));
                _MouseOnSubItem = null;
            }
        }
        #endregion

        #region 公共函数

        /// <summary>
        /// 根据id返回一组列表子项
        /// </summary>
        /// <param name="userId">要返回的id</param>
        /// <returns>列表子项的数组</returns>
        public ChatListSubItem[] GetSubItemsById(int userId)
        {
            List<ChatListSubItem> subItems = new List<ChatListSubItem>();
            for (int i = 0, lenI = this._Items.Count; i < lenI; i++)
            {
                for (int j = 0, lenJ = _Items[i].SubItems.Count; j < lenJ; j++)
                {
                    if (userId == _Items[i].SubItems[j].ID)
                        subItems.Add(_Items[i].SubItems[j]);
                }
            }
            return subItems.ToArray();
        }

        public void Srcoll(Rectangle rect)
        {
            _ChatVScroll.Value = rect.Y;
        }

        /// <summary>
        /// 根据昵称返回一组列表子项
        /// </summary>
        /// <param name="nicName">要返回的昵称</param>
        /// <returns>列表子项的数组</returns>
        public ChatListSubItem[] GetSubItemsByNicName(string nicName)
        {
            List<ChatListSubItem> subItems = new List<ChatListSubItem>();
            for (int i = 0, lenI = this._Items.Count; i < lenI; i++)
            {
                for (int j = 0, lenJ = _Items[i].SubItems.Count; j < lenJ; j++)
                {
                    if (nicName == _Items[i].SubItems[j].NicName)
                        subItems.Add(_Items[i].SubItems[j]);
                }
            }
            return subItems.ToArray();
        }

        public ChatListSubItem[] GetSubItemsByNickName(Predicate<string> match)
        {
            List<ChatListSubItem> list = new List<ChatListSubItem>();

            foreach (ChatListItem  item in Items)
            {
                foreach (ChatListSubItem sitem in item.SubItems)
                {
                    if (match(sitem.NicName))
                    {
                        list.Add(sitem);
                    }
                }

            }

            return list.ToArray();
        }

        /// <summary>
        /// 根据备注名称返回一组列表子项
        /// </summary>
        /// <param name="displayName">要返回的备注名称</param>
        /// <returns>列表子项的数组</returns>
        public ChatListSubItem[] GetSubItemsByDisplayName(string displayName)
        {
            List<ChatListSubItem> subItems = new List<ChatListSubItem>();
            for (int i = 0, lenI = this._Items.Count; i < lenI; i++)
            {
                for (int j = 0, lenJ = _Items[i].SubItems.Count; j < lenJ; j++)
                {
                    if (displayName == _Items[i].SubItems[j].DisplayName)
                        subItems.Add(_Items[i].SubItems[j]);
                }
            }
            return subItems.ToArray();
        }

        #endregion

      
    }

    #region 自定义集合
    //自定义列表项集合
    public class ChatListItemCollection : IList, ICollection, IEnumerable
    {
        private int count;      //元素个数
        public int Count { get { return count; } }
        private ChatListItem[] m_arrItem;
        private ChatListBox owner;  //所属的控件

        public ChatListItemCollection(ChatListBox owner)
        {
            this.owner = owner;
        }
        //确认存储空间
        private void EnsureSpace(int elements)
        {
            if (m_arrItem == null)
                m_arrItem = new ChatListItem[Math.Max(elements, 4)];
            else if (this.count + elements > m_arrItem.Length)
            {
                ChatListItem[] arrTemp = new ChatListItem[Math.Max(this.count + elements, m_arrItem.Length * 2)];
                m_arrItem.CopyTo(arrTemp, 0);
                m_arrItem = arrTemp;
            }
        }
        /// <summary>
        /// 获取列表项所在的索引位置
        /// </summary>
        /// <param name="item">要获取的列表项</param>
        /// <returns>索引位置</returns>
        public int IndexOf(ChatListItem item)
        {
            return Array.IndexOf<ChatListItem>(m_arrItem, item);
        }
        /// <summary>
        /// 添加一个列表项
        /// </summary>
        /// <param name="item">要添加的列表项</param>
        public void Add(ChatListItem item)
        {
            if (item == null)       //空引用不添加
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            if (-1 == this.IndexOf(item))
            {         //进制添加重复对象
                item.OwnerChatListBox = this.owner;
                m_arrItem[this.count++] = item;
                this.owner.Invalidate();            //添加进去是 进行重绘
            }
        }
        /// <summary>
        /// 添加一个列表项的数组
        /// </summary>
        /// <param name="items">要添加的列表项的数组</param>
        public void AddRange(ChatListItem[] items)
        {
            if (items == null)
                throw new ArgumentNullException("Items cannot be null");
            this.EnsureSpace(items.Length);
            try
            {
                foreach (ChatListItem item in items)
                {
                    if (item == null)
                        throw new ArgumentNullException("Item cannot be null");
                    if (-1 == this.IndexOf(item))
                    {     //重复数据不添加
                        item.OwnerChatListBox = this.owner;
                        m_arrItem[this.count++] = item;
                    }
                }
            }
            finally
            {
                this.owner.Invalidate();
            }
        }
        /// <summary>
        /// 移除一个列表项
        /// </summary>
        /// <param name="item">要移除的列表项</param>
        public void Remove(ChatListItem item)
        {
            int index = this.IndexOf(item);
            if (-1 != index)        //如果存在元素 那么根据索引移除
                this.RemoveAt(index);
        }
        /// <summary>
        /// 根据索引位置删除一个列表项
        /// </summary>
        /// <param name="index">索引位置</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            this.count--;
            for (int i = index, Len = this.count; i < Len; i++)
                m_arrItem[i] = m_arrItem[i + 1];
            this.owner.Invalidate();
        }
        /// <summary>
        /// 清空所有列表项
        /// </summary>
        public void Clear()
        {
            this.count = 0;
            m_arrItem = null;
            this.owner.Invalidate();
        }
        /// <summary>
        /// 根据索引位置插入一个列表项
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <param name="item">要插入的列表项</param>
        public void Insert(int index, ChatListItem item)
        {
            if (index < 0 || index >= this.count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            if (item == null)
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            for (int i = this.count; i > index; i--)
                m_arrItem[i] = m_arrItem[i - 1];
            item.OwnerChatListBox = this.owner;
            m_arrItem[index] = item;
            this.count++;
            this.owner.Invalidate();
        }
        /// <summary>
        /// 判断一个列表项是否在集合内
        /// </summary>
        /// <param name="item">要判断的列表项</param>
        /// <returns>是否在列表项</returns>
        public bool Contains(ChatListItem item)
        {
            return this.IndexOf(item) != -1;
        }
        /// <summary>
        /// 将列表项的集合拷贝至一个数组
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="index">拷贝的索引位置</param>
        public void CopyTo(Array array, int index)
        {
            if (array == null)
                throw new ArgumentNullException("array cannot be null");
            m_arrItem.CopyTo(array, index);
        }
        /// <summary>
        /// 根据索引获取一个列表项
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <returns>列表项</returns>
        public ChatListItem this[int index]
        {
            get
            {
                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                return m_arrItem[index];
            }
            set
            {
                if (index < 0 || index >= this.count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                m_arrItem[index] = value;
            }
        }
        //实现接口
        int IList.Add(object value)
        {
            if (!(value is ChatListItem))
                throw new ArgumentException("Value cannot convert to ListItem");
            this.Add((ChatListItem)value);
            return this.IndexOf((ChatListItem)value);
        }

        void IList.Clear()
        {
            this.Clear();
        }

        bool IList.Contains(object value)
        {
            if (!(value is ChatListItem))
                throw new ArgumentException("Value cannot convert to ListItem");
            return this.Contains((ChatListItem)value);
        }

        int IList.IndexOf(object value)
        {
            if (!(value is ChatListItem))
                throw new ArgumentException("Value cannot convert to ListItem");
            return this.IndexOf((ChatListItem)value);
        }

        void IList.Insert(int index, object value)
        {
            if (!(value is ChatListItem))
                throw new ArgumentException("Value cannot convert to ListItem");
            this.Insert(index, (ChatListItem)value);
        }

        bool IList.IsFixedSize
        {
            get { return false; }
        }

        bool IList.IsReadOnly
        {
            get { return false; }
        }

        void IList.Remove(object value)
        {
            if (!(value is ChatListItem))
                throw new ArgumentException("Value cannot convert to ListItem");
            this.Remove((ChatListItem)value);
        }

        void IList.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        object IList.this[int index]
        {
            get { return this[index]; }
            set
            {
                if (!(value is ChatListItem))
                    throw new ArgumentException("Value cannot convert to ListItem");
                this[index] = (ChatListItem)value;
            }
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.CopyTo(array, index);
        }

        int ICollection.Count
        {
            get { return this.count; }
        }

        bool ICollection.IsSynchronized
        {
           get { return true; }
        }

        object ICollection.SyncRoot
        {
            get { return this; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            for (int i = 0, Len = this.count; i < Len; i++)
                yield return m_arrItem[i];
        }
    }

    #endregion

    /// <summary>
    /// 
    /// </summary>
    public class ChatBox:Control 
    {

        #region 字段与变量

        Rectangle _TempRect;

        #endregion 字段与变量

        #region 构造函数
        public ChatBox()
        {
            _TempRect = new Rectangle(0, 0, 0, 0);
        }
        #endregion 构造函数

        #region 属性

        #endregion 属性

        #region 公共函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="height"></param>
        protected void EnsureBounds(Rectangle rect)
        {
            if (rect.Width > _TempRect.Width)
                _TempRect.Width = rect.Width;
            _TempRect.Height += rect.Height;
        }

        protected  Rectangle GetCurrentBounds()
        {
            return _TempRect;
        }

        protected Rectangle GetNextBounds()
        {
            return new Rectangle(0, _TempRect.Bottom, _TempRect.Width, 0);
        }
     

        #endregion 公共函数

        #region 私有函数

        #endregion 私有函数



    }

}
