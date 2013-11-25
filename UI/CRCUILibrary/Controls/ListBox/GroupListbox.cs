/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/5 15:46:24
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
    /// 
    /// </summary>
    public class DataEventArgs<T>:EventArgs 
    {
        public DataEventArgs(T value)
        {
            _Value = value;
        }


        private T _Value;
        /// <summary>
        /// 
        /// </summary>
        public T Value
        {
            get { return _Value; }
           
        }

    }
   

    /// <summary>
    /// 支持两级的分组的ListBox.
    /// </summary>
    public class GroupListbox : ScrollableControl
    {
        #region 事件与委托


        /// <summary>
        /// 当鼠标左键或右键点击 GroupItem项 引发事件.
        /// </summary>
        public event EventHandler<DataEventArgs <GroupItem>> ItemClick;

        protected virtual void OnItemClick(DataEventArgs<GroupItem> e)
        {
            if (ItemClick != null)
                ItemClick(this, e);
        }


        /// <summary>
        /// 当鼠标左键或右键点击 GroupSubItem项 引发事件.
        /// </summary>
        public event EventHandler<DataEventArgs <GroupSubItem>> SubItemClick;

        protected virtual void OnSubItemClick(DataEventArgs <GroupSubItem> e)
        {
            if (SubItemClick != null)
                SubItemClick(this, e);
        }


        /// <summary>
        /// 当鼠标左键或右键点击 GroupCellItem项 引发事件.
        /// </summary>
        public event EventHandler<DataEventArgs <GroupCellItem >> CellItemClick;

        protected virtual void OnCellItemClick(DataEventArgs <GroupCellItem > e)
        {
            if (CellItemClick != null)
                CellItemClick(this, e);
        }


        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<DataEventArgs<GroupCellItem>> CellItemDoubleClick;

        protected virtual void OnCellItemDoubleClick(DataEventArgs<GroupCellItem> e)
        {
            if (CellItemDoubleClick != null)
                CellItemDoubleClick(this, e);
        }


        /// <summary>
        /// 当鼠标停留在 GroupCellItem项时 引发事件.
        /// </summary>
        public event EventHandler<DataEventArgs<GroupCellItem>> MouseOnCellItem;

        protected virtual void OnMouseOnCellItem(DataEventArgs<GroupCellItem> e)
        {
            if (MouseOnCellItem != null)
                MouseOnCellItem(this, e);
        }
        #endregion 事件与委托
        
        #region 字段与变量
        Point _MounsePos;
     
        GroupItem _MouseOnItem = null;
        GroupSubItem _MouseOnSubItem = null;
        GroupCellItem _MouseOnCellItem = null;
        int _ItemHeight = 25;
        int _SubItemHeight = 25;
        int _CellItemHeight = 25;
        #endregion 字段与变量

        #region 构造函数
        public GroupListbox()
        {
            _Items = new GroupItemCollection(this);
            SelectCellItemColor = Color.Wheat;
            //ForeItemColor = Color.WhiteSmoke;
            ForeSubItemColor = Color.White;
            MouseOnCellItemColor = Color.Turquoise;
            MouseOnSubItemColor = Color.Turquoise;
            MouseOnItemColor = Color.Turquoise;

            this.AutoScroll = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);        
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

        }
        #endregion 构造函数

        #region 属性
        /// <summary>
        /// 鼠标停留时,Item的颜色.
        /// </summary>
        public Color MouseOnItemColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Color ForeSubItemColor { get; set; }

        /// <summary>
        /// 鼠标停留时,SubItem的颜色.
        /// </summary>
        public Color MouseOnSubItemColor { get; set; }

        /// <summary>
        /// 被选中时,CellItem的颜色
        /// </summary>
        public Color SelectCellItemColor { get; set; }

        /// <summary>
        /// 鼠标停留时,CellItem的颜色.
        /// </summary>
        public Color MouseOnCellItemColor { get; set; }

        private GroupItemCollection  _Items;
        /// <summary>
        /// 
        /// </summary>
        public GroupItemCollection  Items
        {
            get { return _Items; }
            
        }

        private GroupCellItem _MouseSelectCellItem = null;

        public  void SetScroll(int pos)
        {
            if (pos < 0) return;
            if (pos < ClientRectangle.Height)
                VerticalScroll.Value = 0;
            else
            {
                if (pos - ClientRectangle.Height < VerticalScroll.Maximum)
                    VerticalScroll.Value = pos - ClientRectangle.Height;
                else
                    VerticalScroll.Value = VerticalScroll.Maximum;
                
            }
            this.Invalidate();
        }

        public GroupCellItem SelecctCellItem
        {
            get { return _MouseSelectCellItem; }
            set
            {
                if (value != null &&_MouseSelectCellItem != value)
                {
                    _MouseSelectCellItem = value;
                    if (!value.Father.Father.IsOpen)
                    {
                        //第一组没有展开 
                        GroupItem gitem=value.Father.Father;
                        gitem.IsOpen = true;
                        int pos = gitem.Bounds.Bottom;
                        int index = gitem.SubItems.IndexOf(value.Father);

                        SetScroll(value.Bounds.Y);

                    }
                    else
                    {
                        if (!value.Father.IsOpen)
                        {

                        }
                        else
                        {
                            //该项 的两个组都是展开的.
                            int pos = value.Bounds.Y;
                        }
                    }
                    this.Invalidate();
                }
            }
        }
        #endregion 属性

        #region 公共函数

        #region 绘制函数
        /// <summary>
        /// 绘制函数.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
             e.Graphics .TranslateTransform(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
             try
             {

                 Graphics g = e.Graphics;
                 Rectangle rect = new Rectangle(0,0, this.Width, 0);
                 foreach (var item in _Items)
                 {
                     rect = DrawItem(g, item, rect);
                 }

                rect = new Rectangle(0, 0, rect.Width, rect.Y);
                 // 设置滚动条出现的最小Size
                 if (this.AutoScrollMinSize != rect.Size)
                 {
                     this.AutoScrollMinSize = rect.Size;
                 }
               
               

             }
             catch (Exception)
             {

                 throw;
             }
        }

        /// <summary>
        /// 绘制GroupItem.
        /// </summary>
        /// <param name="g">绘制图面.</param>
        /// <param name="item">要绘制的GroupItem</param>
        /// <param name="rect">绘制的区域.只提供起点(x,y),具体大小可以在重载时自行确定.</param>
        /// <returns>下一个GroupItem的起点区域.</returns>
        protected virtual Rectangle DrawItem(Graphics g, GroupItem item, Rectangle rect)
        {
            //绘制Item
            rect.Height = _ItemHeight;
            SolidBrush sb;
            if (item.Equals (_MouseOnItem))
            {
                sb = new SolidBrush(MouseOnItemColor);
            }
            else
            {
                sb = new SolidBrush(this.BackColor);
            }

            g.FillRectangle(sb, rect);

            PaintText(g, item.Text,rect, sb,item.IsOpen);

            item.Bounds = rect;
            rect = GetNextBounds(rect);
            //绘制子Item
            if (item.IsOpen)
            {
                rect.X = 20;
                foreach (var sitem in item.SubItems)
                {
                    rect=DrawSubItem(g, sitem, rect);
                }
                rect.X = 0;
            }
            return rect;
        }
        /// <param name="g">绘制图面.</param>
        /// <param name="item">要绘制的GroupSubItem</param>
        /// <param name="rect">绘制的区域.只提供起点(x,y),具体大小可以在重载时自行确定.</param>
        /// <returns>下一个GroupItem的起点区域.</returns>
        protected virtual Rectangle DrawSubItem(Graphics g, GroupSubItem item, Rectangle rect)
        {
            //绘制Item
            rect.Height = _SubItemHeight;
            SolidBrush sb;
            if (item.Equals(_MouseOnSubItem))
            {
                sb = new SolidBrush(MouseOnSubItemColor);
            }
            else
            {
                sb = new SolidBrush(this.BackColor);
            }
            g.FillRectangle(sb, rect);
           
            rect =PaintText(g, item.Text ,rect, sb, item.IsOpen);

            item.Bounds = rect;
            rect = GetNextBounds(rect);
            //绘制子Item
            if (item.IsOpen)
            {
                rect.X += 20;
                foreach (var sitem in item.SubItems)
                {
                    rect=DrawCellItem(g, sitem, rect);
                }
                rect.X -= 20;
            }
            return rect;
        }

        private static Rectangle FillItem(Graphics g, Rectangle rect, SolidBrush sb)
        {
            Rectangle r = rect;
            r.X = 0;
            g.FillRectangle(sb, rect);
            return rect;
        }
        /// <param name="g">绘制图面.</param>
        /// <param name="item">要绘制的GroupCellItem</param>
        /// <param name="rect">绘制的区域.只提供起点(x,y),具体大小可以在重载时自行确定.</param>
        /// <returns>下一个GroupItem的起点区域.</returns>
        protected virtual Rectangle DrawCellItem(Graphics g, GroupCellItem item, Rectangle rect)
        {
            //绘制Item
            rect.Height = _CellItemHeight;
            SolidBrush sb;
            if (item.Equals (_MouseOnCellItem))
            {
                sb = new SolidBrush(MouseOnCellItemColor);
            }
            else
            {
                sb = new SolidBrush(BackColor);
            }
            //FillItem(g, rect, sb);
            g.FillRectangle(sb, rect);

            StringFormat sf = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near   ,
                FormatFlags = StringFormatFlags.NoClip,
            };
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            sb.Color = Color.Black;
            g.DrawString(item.Text, Font, sb, rect, sf);

            item.Bounds = rect;
            return  GetNextBounds(rect);
           
        }

        /// <summary>
        /// 绘制文本.
        /// </summary>
        /// <param name="g">绘制图面.</param>
        /// <param name="isOpen">是否可展开项</param>
        /// <param name="text">要绘制的文本.</param>
        /// <param name="rect">绘制的区域.只提供起点(x,y),具体大小可以在重载时自行确定.</param>
        /// <param name="sb">绘笔.</param>
        /// <returns>下一个GroupItem的起点区域.</returns>
        private Rectangle PaintText(Graphics g, string text, Rectangle rect, SolidBrush sb, bool isOpen)
        {
            if (_Items.Count > 0)
                DrawIcon(g, rect, isOpen);
            StringFormat sf = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.NoClip,
            };
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            sb.Color = Color.Black;
            text=string.Format("\t{0}  [{1}]", text,_Items.Count);
            SizeF size=g.MeasureString(text, Font);
            rect.Width = size.Width>rect.Width ? (int)size .Width :rect.Width;
            g.DrawString(text, Font, sb, rect, sf);
            return rect;


        }
        /// <summary>
        /// 获取下一个绘制区域的起始位置.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        protected Rectangle GetNextBounds(Rectangle rect)
        {
            return new Rectangle(rect.X, rect.Bottom, rect.Width, 0);
        }

        /// <summary>
        /// 绘制三角形.
        /// </summary>
        /// <param name="g">绘制平面.</param>
        /// <param name="rectItem">绘制区域.</param>
        /// <param name="isOpen">项是否展开.</param>
        protected virtual void DrawIcon(Graphics g, Rectangle rectItem, bool isOpen)
        {
            using (SolidBrush sb = new SolidBrush(Color.Black))
            {
                if (isOpen)
                {
                    g.FillPolygon(sb, new Point[] { 
                        new Point(rectItem.X +2, rectItem.Top + 11), 
                        new Point(rectItem.X +12, rectItem.Top + 11), 
                        new Point(rectItem.X +7, rectItem.Top + 16) });
                }
                else
                {
                    g.FillPolygon(sb, new Point[] { 
                        new Point(rectItem.X +5, rectItem.Top + 8), 
                        new Point(rectItem.X +5, rectItem.Top + 18), 
                        new Point(rectItem.X +10, rectItem.Top + 13) });
                }
            }

        }

        #endregion 绘制函数

        #region 区域清理函数
        /// <summary>
        /// 清理GroupItem区域.
        /// </summary>
        private void ClearItemBounds()
        {
            if (_MouseOnItem != null)
            {
                Rectangle rect = _MouseOnItem.Bounds;
                rect.Y -= VerticalScroll.Value;
                this.Invalidate(rect);
                _MouseOnItem = null;
            }
        }
        /// <summary>
        /// 清理GroupSubItem区域.
        /// </summary>
        private void ClearSubItemBounds()
        {
            if (_MouseOnSubItem != null)
            {
                Rectangle rect = _MouseOnSubItem.Bounds;
                rect.Y -= VerticalScroll.Value;
                rect.X = 0;
                this.Invalidate(rect);
                _MouseOnSubItem = null;
            }
        }

        /// <summary>
        /// 清理GroupCellItem区域.
        /// </summary>
        private void ClearCellItemBounds()
        {
            if (_MouseOnCellItem != null)
            {
                Rectangle rect = _MouseOnCellItem.Bounds;
                rect.Y -= VerticalScroll.Value;
                rect.X = 0;
                this.Invalidate(rect);
                _MouseOnCellItem = null;
            }
        }

        /// <summary>
        /// 清理所有的鼠标停留的子项
        /// </summary>
        private void ClearBounds()
        {
            ClearCellItemBounds();
            ClearItemBounds();
            ClearSubItemBounds();
        }
        #endregion 区域清理

        #region 鼠标事件处理

        /// <summary>
        /// 鼠标移动处理.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _MounsePos = e.Location;
            int index = this.VerticalScroll.Value;
            _MounsePos.X += this.HorizontalScroll.Value;
            _MounsePos.Y += this.VerticalScroll.Value;

            Rectangle rect;
            foreach (var item in _Items )
            {
                if (item.Bounds.Contains(_MounsePos))
                {
                    if (item.Equals(_MouseOnItem))//子项与上一次是相等的.
                    {
                        Debug.WriteLine("Item 与上次一样 ");
                        return;
                    }
                    else
                    {
                        
                        ClearBounds();
                        _MouseOnItem = item;
                        rect = item.Bounds;
                        rect.Y -= index;
                        this.Invalidate(rect);
                        Debug.WriteLine("被选中的是Item {0}".FormatWith(item.Bounds));
                        return;
                    }
                }
                else
                {
                    if (item.IsOpen)
                    {
                        foreach (var sitem in item.SubItems )
                        {
                            if (sitem.Bounds.Contains(_MounsePos))
                            {
                                if (sitem.Equals(_MouseOnSubItem))
                                {
                                    Debug.WriteLine("SubItem 与上次一样 ");
                                    return;
                                }
                                else
                                {
                                    ClearBounds();
                                    _MouseOnSubItem = sitem;
                                    rect = sitem.Bounds;
                                    rect.Y -= index;
                                    this.Invalidate(rect);
                                    Debug.WriteLine("被选中的是SubItem {0}".FormatWith(sitem.Bounds));
                                    return;
                                }
                            }
                            else
                            {
                                if (sitem.IsOpen)
                                {
                                    foreach (var citem in sitem .SubItems )
                                    {
                                        if (citem.Bounds.Contains(_MounsePos))
                                        {
                                            ClearBounds();
                                            _MouseOnCellItem = citem;
                                            rect = citem.Bounds;
                                            rect.Y -= index;
                                            OnMouseOnCellItem(new DataEventArgs<GroupCellItem>(citem));
                                            this.Invalidate(rect);
                                            Debug.WriteLine("被选中的是CellItem {0}".FormatWith(citem.Bounds));
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            ClearBounds();
            Debug.WriteLine("没有找到任何项");
            foreach (var item in Items)
            {
                Debug.WriteLine(item.Bounds);
            }
        }

        /// <summary>
        /// 鼠标离开处理.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            ClearBounds();
            base.OnMouseLeave(e);
        }
        /// <summary>
        /// 鼠标点击处理.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {

            Debug.WriteLine("VS {0} Height {2} sum{3} Mouse {1} ".FormatWith(VerticalScroll.Value, e.Y, this.Height, VerticalScroll.Value + this.Height));
            Point Pos = e.Location;

            int index = this.VerticalScroll.Value;
            Pos.X += this.HorizontalScroll.Value;
            Pos.Y += this.VerticalScroll.Value;


            if (e.Button == System.Windows.Forms.MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Debug.WriteLine("鼠标点击位置:{0}".FormatWith(Pos));
                foreach (var item in Items)
                {

                    if (item.Bounds.Contains(Pos))
                    {
                        _MouseSelectCellItem = null;
                        item.IsOpen = !item.IsOpen;
                        OnItemClick(new DataEventArgs<GroupItem>(item));
                        //this.Invalidate();
                        Debug.WriteLine("Item 位置{0}".FormatWith(item.Bounds));
                        return;
                    }
                    else
                    {
                        if (item.IsOpen)
                        {
                            foreach (var sitem in item.SubItems)
                            {
                                if (sitem.Bounds.Contains(Pos))
                                {
                                    _MouseSelectCellItem = null;
                                    sitem.IsOpen = !sitem.IsOpen;
                                    //this.Invalidate();
                                    OnSubItemClick(new DataEventArgs<GroupSubItem>(sitem));
                                    Debug.WriteLine("{2} {1} 位置{0}".FormatWith(sitem.Bounds, sitem.Text, sitem.Father.Text));
                                    return;
                                }
                                else
                                {
                                    if (sitem.IsOpen)
                                    {
                                        foreach (var citem in sitem.SubItems)
                                        {
                                            if (!citem.Equals(_MouseSelectCellItem) && citem.Bounds.Contains(Pos))
                                            {
                                                _MouseSelectCellItem = citem;
                                                this.Invalidate();
                                                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                                                {
                                                    OnCellItemClick(new DataEventArgs<GroupCellItem>(citem));
                                                }
                                                else
                                                {
                                                    OnCellItemDoubleClick(new DataEventArgs<GroupCellItem>(citem));   
                                                }
                                                Debug.WriteLine("{1} -{2} -{3}- Cell 位置{0}".FormatWith(citem.Bounds, item.Text, sitem.Text, citem.Text));
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //Debug.WriteLine("没有找到任何项");
                //foreach (var item in Items)
                //{
                //    Debug.WriteLine(item.Bounds);
                //}
            }
            base.OnMouseClick(e);
        }
        #endregion

        #region 查找

        public GroupCellItem[] GetCellItems(Predicate <string> match)
        {
            List<GroupCellItem> list = new List<GroupCellItem>();

            foreach (var item in Items)
            {
                foreach (var sitem in item.SubItems )
                {
                    foreach (var citem in sitem.SubItems )
                    {
                        if(match (citem.Text ))
                        {
                            list.Add (citem );
                        }
                    }

                }
            }
            return list.ToArray();

        }

        public GroupCellItem[] GetCellItems(Predicate<object> match)
        {
            List<GroupCellItem> list = new List<GroupCellItem>();

            foreach (var item in Items)
            {
                foreach (var sitem in item.SubItems)
                {
                    foreach (var citem in sitem.SubItems)
                    {
                        if (match(citem.Tag))
                        {
                            list.Add(citem);
                        }
                    }

                }
            }
            return list.ToArray();

        }

        public GroupSubItem[] GetSubItems(Predicate<string> match)
        {
            List<GroupSubItem> list = new List<GroupSubItem>();

            foreach (var item in Items)
            {
                foreach (var sitem in item.SubItems)
                {
                    
                    if (match(sitem.Text))
                    {
                        list.Add(sitem);
                    }
                   

                }
            }
            return list.ToArray();
        }

        

        #endregion

        #endregion 公共函数

        #region 私有函数

        #endregion 私有函数

    }


    #region 项

    /// <summary>
    /// 
    /// </summary>
    public class GroupCellItem
    {

        #region 保护函数

        protected void Invalidate()
        {
            if (Owner != null)
                Owner.Invalidate();
        }

        #endregion 

        #region 属性
        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    Invalidate();
                }
            }
        }


        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }

       
        public Control Owner
        {
            get
            {
                if (_Father == null) return null;
                return _Father.Owner;
            }
            set
            {
                
            }
        }


        private object  _Tag;
        /// <summary>
        /// 
        /// </summary>
        public object  Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        private GroupSubItem  _Father;
        /// <summary>
        /// 
        /// </summary>
        public GroupSubItem  Father
        {
            get { return _Father; }
            set { _Father = value; }
        }




        #endregion

     
    }

    /// <summary>
    /// 
    /// </summary>
    public class GroupSubItem
    {
        #region 构造函数
        public GroupSubItem()
        {
            _SubItems = new GroupCellItemCollection(this);
        }
        #endregion 构造函数

        #region 保护函数

        protected void Invalidate()
        {
            if (Owner != null)
                Owner.Invalidate();
        }

        #endregion 

        #region 属性
        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    Invalidate();
                }
            }
        }


        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }

     
        public Control Owner
        {
            get
            {
                if (_Father == null) return null;
                return _Father.Owner;
            }
            set
            {
                
            }
        }


        private object _Tag;
        /// <summary>
        /// 
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        private GroupItem  _Father;
        /// <summary>
        /// 
        /// </summary>
        public GroupItem  Father
        {
            get { return _Father; }
            set { _Father = value; }
        }

        private bool _IsOpen;
        public bool IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                _IsOpen = value;
                this.Invalidate();
            }
        }

        public  void SetSubItemBounds()
        {
            Rectangle rect = new Rectangle(0, 0, 0, 0);
            foreach (var item in _SubItems)
            {
                item.Bounds = rect;

            }
        }

        private GroupCellItemCollection _SubItems;
        /// <summary>
        /// 
        /// </summary>
        public GroupCellItemCollection SubItems
        {
            get { return _SubItems; }
            set { _SubItems = value; }
        }


        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class GroupItem
    {
        public GroupItem(string text)
        {
            _SubItems = new GroupSubItemCollection(this);
            _Text = text;
        }

        #region 保护函数

        protected void Invalidate()
        {
            if (_Owner != null)
                _Owner.Invalidate();
        }

        #endregion

        #region 属性
        private string _Text;
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                if (_Text != value)
                {
                    _Text = value;
                    Invalidate();
                }
            }
        }


        private Rectangle _Bounds;
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            set
            {
                _Bounds = value;
            }
        }

        private Control _Owner;
        public Control Owner
        {
            get
            {
                return _Owner;
            }
            set
            {
                _Owner = value;
            }
        }


        private object _Tag;
        /// <summary>
        /// 
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }

        private bool _IsOpen;
        public bool IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                _IsOpen = value;
                //if (!_IsOpen) SetSubItemBounds();
                this.Invalidate();
            }
        }

        private void SetSubItemBounds()
        {
            Rectangle rect=new Rectangle (0,0,0,0);
            foreach (var item in _SubItems )
            {
                item.Bounds = rect;
                item.SetSubItemBounds();
            }
        }

        private GroupSubItemCollection _SubItems;
        /// <summary>
        /// 
        /// </summary>
        public GroupSubItemCollection SubItems
        {
            get { return _SubItems; }
            set { _SubItems = value; }
        }

        #endregion
    }

#endregion 项

    #region 集合
    /// <summary>
    /// 
    /// </summary>
    public class GroupCellItemCollection:IList <GroupCellItem>
    {
                #region 字段与变量
        /// <summary>
        /// 元素的个数.
        /// </summary>
        private int _Count;

        private GroupCellItem[] _Items;

        private Control _Owner;

        private GroupSubItem _Father;

        #endregion 字段与变量

        #region 构造函数

        public GroupCellItemCollection(GroupSubItem  item)
        {
            _Father = item;
            _Owner = null;
        }

        public GroupCellItemCollection(Control owner)
        {
            if (owner == null) throw new ArgumentNullException("owner"); 
            _Owner = owner;
        }

        #endregion 构造函数

        #region 私有函数
        //
        /// <summary>
        /// 确认存储空间
        /// </summary>
        /// <param name="elements"></param>
        private void EnsureSpace(int elements)
        {
            if (_Items == null)
            {
                _Items = new GroupCellItem[Math.Max(elements, 4)];
            }
            else if (this.Count + elements > _Items.Length)
            {
                GroupCellItem[] temp = new GroupCellItem[Math.Max(this.Count + elements, _Items.Length * 2)];
                _Items.CopyTo(temp, 0);
                _Items = temp;
            }
        }
        #endregion 私有函数

        #region IList<GroupCellItem> 成员

        public int IndexOf(GroupCellItem item)
        {
            return Array.IndexOf(_Items, item);
        }

        public void Insert(int index, GroupCellItem item)
        {
            if (index < 0 || index >=_Count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            if (item == null)
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            for (int i = _Count ; i > index ; i--)
            {
                _Items[i] = _Items[i - 1];
            }

            _Items[index] = item;
            _Count++;
            Invalidate();

        }

        public void RemoveAt(int index)
        {
           if(index <0 || index >=_Count )
               throw new IndexOutOfRangeException("Index was outside the bounds of the array");
           _Count--;
           for (int i = index, Len = _Count; i < Len; i++)
               _Items[i] = _Items[i + 1];
           Invalidate();
        }

        public GroupCellItem this[int index]
        {
            get
            {
                if (index < 0 || index >= _Count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                return _Items[index];
            }
            set
            {
                if (index < 0 || index >= _Count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                _Items[index] = value;
            }
        }

        #endregion

        #region ICollection<GroupCellItem> 成员

        public void Add(GroupCellItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            this.EnsureSpace(1);
            
            //不允许添加重复对象
            if (IndexOf(item) == -1)
            {
                item.Owner= _Owner;
                _Items[_Count++] = item;
                item.Father = _Father;
                Invalidate();
            }
        }

        public void AddRange(GroupCellItem[] items)
        {
            if (items == null) throw new ArgumentNullException("items");
            this.EnsureSpace(items.Length);
            try
            {
                foreach (var item in items)
                {
                    if (item == null) throw new ArgumentNullException("item");
                    if (IndexOf(item) == -1)
                    {
                        item.Owner = _Owner;
                        _Items[_Count++] = item;
                    }

                }

            }
            finally
            {
                Invalidate();
            }

        }

        private void Invalidate()
        {
            if (_Owner != null) 
                _Owner.Invalidate();
        }

        public void Clear()
        {
            _Count = 0;
            _Items = null;
            Invalidate();
        }

        public bool Contains(GroupCellItem item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GroupCellItem[] array, int arrayIndex)
        {
            if (array  == null) throw new ArgumentNullException("array ");
            _Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(GroupCellItem item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable<GroupCellItem> 成员

        public IEnumerator<GroupCellItem> GetEnumerator()
        {
            for (int i = 0, Len = _Count; i < Len; i++)
            {
                yield return _Items[i];
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            for (int i = 0, Len = _Count; i < Len; i++)
            {
                yield return _Items[i];
            }
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    public class GroupSubItemCollection:IList <GroupSubItem >
    {
                #region 字段与变量
        /// <summary>
        /// 元素的个数.
        /// </summary>
        private int _Count;

        private GroupSubItem[] _Items;

        private Control _Owner;

        private GroupItem _Father;

        #endregion 字段与变量

        #region 构造函数

        public GroupSubItemCollection(GroupItem item)
        {
            _Father = item;
            _Owner = null;
        }

        public GroupSubItemCollection(Control owner)
        {
            if (owner == null) throw new ArgumentNullException("owner"); 
            _Owner = owner;
        }

        #endregion 构造函数

        #region 私有函数
        //
        /// <summary>
        /// 确认存储空间
        /// </summary>
        /// <param name="elements"></param>
        private void EnsureSpace(int elements)
        {
            if (_Items == null)
            {
                _Items = new GroupSubItem[Math.Max(elements, 4)];
            }
            else if (this.Count + elements > _Items.Length)
            {
                GroupSubItem[] temp = new GroupSubItem[Math.Max(this.Count + elements, _Items.Length * 2)];
                _Items.CopyTo(temp, 0);
                _Items = temp;
            }
        }
        #endregion 私有函数

        #region IList<GroupSubItem> 成员

        public int IndexOf(GroupSubItem item)
        {
            return Array.IndexOf(_Items, item);
        }

        public void Insert(int index, GroupSubItem item)
        {
            if (index < 0 || index >=_Count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            if (item == null)
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            for (int i = _Count ; i > index ; i--)
            {
                _Items[i] = _Items[i - 1];
            }

            _Items[index] = item;
            _Count++;
            Invalidate();

        }

        public void RemoveAt(int index)
        {
           if(index <0 || index >=_Count )
               throw new IndexOutOfRangeException("Index was outside the bounds of the array");
           _Count--;
           for (int i = index, Len = _Count; i < Len; i++)
               _Items[i] = _Items[i + 1];
           Invalidate();
        }

        public GroupSubItem this[int index]
        {
            get
            {
                if (index < 0 || index >= _Count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                return _Items[index];
            }
            set
            {
                if (index < 0 || index >= _Count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                _Items[index] = value;
            }
        }

        #endregion

        #region ICollection<GroupSubItem> 成员

        public void Add(GroupSubItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            this.EnsureSpace(1);
            
            //不允许添加重复对象
            if (IndexOf(item) == -1)
            {
                item.Owner= _Owner;
                _Items[_Count++] = item;
                item.Father = _Father;
                Invalidate();
            }
        }

        public void AddRange(GroupSubItem[] items)
        {
            if (items == null) throw new ArgumentNullException("items");
            this.EnsureSpace(items.Length);
            try
            {
                foreach (var item in items)
                {
                    if (item == null) throw new ArgumentNullException("item");
                    if (IndexOf(item) == -1)
                    {
                        item.Owner = _Owner;
                        _Items[_Count++] = item;
                    }

                }

            }
            finally
            {
                Invalidate();
            }

        }

        private void Invalidate()
        {
            if (_Owner != null) _Owner.Invalidate();
        }

        public void Clear()
        {
            _Count = 0;
            _Items = null;
            Invalidate();
        }

        public bool Contains(GroupSubItem item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GroupSubItem[] array, int arrayIndex)
        {
            if (array  == null) throw new ArgumentNullException("array ");
            _Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(GroupSubItem item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable<GroupSubItem> 成员

        public IEnumerator<GroupSubItem> GetEnumerator()
        {
            for (int i = 0, Len = _Count; i < Len; i++)
            {
                yield return _Items[i];
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            for (int i = 0, Len = _Count; i < Len; i++)
            {
                yield return _Items[i];
            }
        }

        #endregion
    }


    public class GroupItemCollection : IList<GroupItem>
    {
               #region 字段与变量
        /// <summary>
        /// 元素的个数.
        /// </summary>
        private int _Count;

        private GroupItem[] _Items;

        private Control _Owner;


        #endregion 字段与变量

        #region 构造函数


        public GroupItemCollection(Control owner)
        {
            if (owner == null) throw new ArgumentNullException("owner"); 
            _Owner = owner;
        }

        #endregion 构造函数

        #region 私有函数
        //
        /// <summary>
        /// 确认存储空间
        /// </summary>
        /// <param name="elements"></param>
        private void EnsureSpace(int elements)
        {
            if (_Items == null)
            {
                _Items = new GroupItem[Math.Max(elements, 4)];
            }
            else if (this.Count + elements > _Items.Length)
            {
                GroupItem[] temp = new GroupItem[Math.Max(this.Count + elements, _Items.Length * 2)];
                _Items.CopyTo(temp, 0);
                _Items = temp;
            }
        }
        #endregion 私有函数

        #region IList<GroupItem> 成员

        public int IndexOf(GroupItem item)
        {
            return Array.IndexOf(_Items, item);
        }

        public void Insert(int index, GroupItem item)
        {
            if (index < 0 || index >=_Count)
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            if (item == null)
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            for (int i = _Count ; i > index ; i--)
            {
                _Items[i] = _Items[i - 1];
            }

            _Items[index] = item;
            _Count++;
            Invalidate();

        }

        public void RemoveAt(int index)
        {
           if(index <0 || index >=_Count )
               throw new IndexOutOfRangeException("Index was outside the bounds of the array");
           _Count--;
           for (int i = index, Len = _Count; i < Len; i++)
               _Items[i] = _Items[i + 1];
           Invalidate();
        }

        public GroupItem this[int index]
        {
            get
            {
                if (index < 0 || index >= _Count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                return _Items[index];
            }
            set
            {
                if (index < 0 || index >= _Count)
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                _Items[index] = value;
            }
        }

        #endregion

        #region ICollection<GroupItem> 成员

        public void Add(GroupItem item)
        {
            if (item == null) throw new ArgumentNullException("item");
            this.EnsureSpace(1);
            
            //不允许添加重复对象
            if (IndexOf(item) == -1)
            {
                item.Owner= _Owner;
                _Items[_Count++] = item;
              
                Invalidate();
            }
        }

        public void AddRange(GroupItem[] items)
        {
            if (items == null) throw new ArgumentNullException("items");
            this.EnsureSpace(items.Length);
            try
            {
                foreach (var item in items)
                {
                    if (item == null) throw new ArgumentNullException("item");
                    if (IndexOf(item) == -1)
                    {
                        item.Owner = _Owner;
                        _Items[_Count++] = item;
                    }

                }

            }
            finally
            {
                Invalidate();
            }

        }

        private void Invalidate()
        {
            if (_Owner != null) 
                _Owner.Invalidate();
        }

        public void Clear()
        {
            _Count = 0;
            _Items = null;
            Invalidate();
        }

        public bool Contains(GroupItem item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(GroupItem[] array, int arrayIndex)
        {
            if (array  == null) throw new ArgumentNullException("array ");
            _Items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(GroupItem item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region IEnumerable<GroupItem> 成员

        public IEnumerator<GroupItem> GetEnumerator()
        {
            for (int i = 0, Len = _Count; i < Len; i++)
            {
                yield return _Items[i];
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            for (int i = 0, Len = _Count; i < Len; i++)
            {
                yield return _Items[i];
            }
        }

        #endregion
    }

#endregion 集合






}
