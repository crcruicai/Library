/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/4 15:40:03
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


    class ChatGroupBoxManger
    {
        

        static ChatGroupBoxManger()
        {
            ForeColor = Color.DarkOrange;
            SelectCellItemColor = Color.Wheat;
            ForeItemColor = Color.WhiteSmoke;
            ForeSubItemColor = Color.White;
            MouseOnCellItemColor = Color.Turquoise;
            MouseOnSubItemColor = Color.Thistle;
            MouseOnItemColor = Color.Tan;
            BackColor = Color.White;
            Font = new Font("宋体", 12);
        }

        #region 对象属性

        public static Point MounsePos { get; set; }

        public static ChatCellItem MouseSelectCellItem { get; set; }

        #endregion


        #region 颜色属性
        /// <summary>
        /// 背景色.
        /// </summary>
        public static Color BackColor { get; set; }


        public static Color ForeItemColor { get; set; }

        /// <summary>
        /// 鼠标停留时,Item的颜色.
        /// </summary>
        public static Color MouseOnItemColor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static Color ForeSubItemColor { get; set; }

        /// <summary>
        /// 鼠标停留时,SubItem的颜色.
        /// </summary>
        public static Color MouseOnSubItemColor { get; set; }

        /// <summary>
        /// 被选中时,CellItem的颜色
        /// </summary>
        public static Color SelectCellItemColor { get; set; }

        /// <summary>
        /// 鼠标停留时,CellItem的颜色.
        /// </summary>
        public static Color MouseOnCellItemColor { get; set; }

        /// <summary>
        /// 前颜色.
        /// </summary>
        public static Color ForeColor { get; set; }

        /// <summary>
        /// 字体.
        /// </summary>
        public static Font Font { get; set; }
        #endregion
    }


    #region 接口
    /// <summary>
    /// 表示一个项 的接口.
    /// </summary>
    public interface IItem
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Rectangle Bounds { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Control Owner { get; set; }
       

        #endregion 属性

        #region 函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        Rectangle DrawItem(Graphics g, Rectangle rect);

        void MouseOn(Point mousePos);
        
      
     



        #endregion 函数
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IChatCellItem : IItem
    {
        IChatSubItem Father { get; set; }
    }



    /// <summary>
    /// 
    /// </summary>
    public interface IChatSubItem : IItem
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ChatCellCollection SubItems { get; }

        /// <summary>
        /// 
        /// </summary>
        IChatItem Father { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public interface IChatItem : IItem
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsOpen { get; set; }

        /// <summary>
        /// 
        /// </summary>
        ChatSubCollection  Items { get; }
    }


    #endregion

    #region 实现接口

    public abstract  class Item : IItem
    {

        #region 保护函数

        protected Rectangle GetNextBounds(Rectangle rect)
        {
            return new Rectangle(0, rect.Bottom, rect.Width, 0);
        }


        protected  void Invalidate()
        {
            if (_Owner != null)
                _Owner.Invalidate();
        }

        /// <summary>
        /// 绘制三角形.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rectItem"></param>
        /// <param name="isOpen"></param>
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


        #endregion 私有函数

        #region IItem 成员
        private string _Text;
        public virtual string Text
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
        public virtual  Control Owner
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


        public abstract Rectangle DrawItem(Graphics g, Rectangle rect);

        public abstract void MouseOn(Point mousePos);



        #endregion

        #region IItem 成员



        #endregion
    }

    
    /// <summary>
    /// 
    /// </summary>
    public class ChatCellItem :Item, IChatCellItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public override Control Owner
        {
            get
            {
                return _Father.Owner;
            }
            set
            {
                
            }
        }

        private IChatSubItem _Father;
        /// <summary>
        /// 
        /// </summary>
        public IChatSubItem Father
        {
            get
            {
                return _Father;
            }
            set
            {
                _Father = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <returns></returns>
        public override  Rectangle DrawItem(Graphics g, Rectangle rect)
        {
            rect.Height = 50;
            SolidBrush sb;
            if (this.Equals(ChatGroupBoxManger.MouseSelectCellItem))
            {
                sb = new SolidBrush(ChatGroupBoxManger.SelectCellItemColor);
            }
            else if (rect.Contains(ChatGroupBoxManger .MounsePos))
            {
                sb = new SolidBrush(ChatGroupBoxManger.MouseOnCellItemColor);
            }
            else
            {
                sb = new SolidBrush(ChatGroupBoxManger.BackColor);
            }

            g.FillRectangle(sb, rect);

            Rectangle r = new Rectangle(20, rect.Y, rect.Width, 50);
            PaintText(g, r, sb);

            this.Bounds = rect;

            return GetNextBounds(rect);


        }

        private void PaintText(Graphics g, Rectangle rect, SolidBrush sb)
        {
            
            StringFormat sf = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.NoClip,
            };
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            sb.Color = ChatGroupBoxManger.ForeColor;
            g.DrawString(string.Format("\t{0}", Text), ChatGroupBoxManger.Font, sb, rect, sf);

        }

        public override void MouseOn(Point mousePos)
        {
            if (Bounds.Contains(mousePos))
            {
                Owner.Invalidate(Bounds);
            }
        }
       
       
    }
  

    public class ChatSubItem : Item, IChatSubItem
    {
        public ChatSubItem()
        {
            _SubItems = new ChatCellCollection(this);
        }

        public override Rectangle DrawItem(Graphics g, Rectangle rect)
        {
            rect.Height = 25;
            SolidBrush sb;
            if (rect.Contains(ChatGroupBoxManger.MounsePos))
            {
                sb = new SolidBrush(ChatGroupBoxManger.MouseOnSubItemColor );
            }
            else
            {
                sb = new SolidBrush(ChatGroupBoxManger.ForeSubItemColor );
            }
           

            g.FillRectangle(sb, rect);
            Rectangle r = new Rectangle(20, rect.Y, rect.Width, 25);
            PaintText(g, r, sb);

            this.Bounds = rect;
            rect = GetNextBounds(rect);
            if (IsOpen)
            {
                foreach (var item in _SubItems)
                {
                    rect=item.DrawItem(g, rect);
                }
            }
            return rect;

        }
        private void PaintText(Graphics g, Rectangle rect, SolidBrush sb)
        {
            if (_SubItems.Count > 0)
                DrawIcon(g, rect, _IsOpen);
            StringFormat sf = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.NoClip,
            };
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            sb.Color = ChatGroupBoxManger.ForeColor;
            g.DrawString(string.Format("\t{0}[{1}]", Text, _SubItems.Count), ChatGroupBoxManger.Font, sb, rect, sf);

        }

        public override void MouseOn(Point mousePos)
        {
            if (Bounds.Contains(mousePos))
            {
                Owner.Invalidate(Bounds);
            }
        }

        public override Control Owner
        {
            get
            {
                return _Father.Owner;
            }
            set
            {
                
            }
        }

        #region IChatSubItem 成员
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

        private ChatCellCollection _SubItems;
        public ChatCellCollection SubItems
        {
            get { return _SubItems; }
        }

        private IChatItem _Father;
        public IChatItem Father
        {
            get
            {
                return _Father;
            }
            set
            {
                _Father = value;
            }
        }

        #endregion
    }

    public class ChatItem : Item, IChatItem
    {
        public ChatItem()
        {
            _Items = new ChatSubCollection(this);
        }


        public override Rectangle DrawItem(Graphics g, Rectangle rect)
        {
            //绘制Item
            rect.Height = 25;
            SolidBrush sb;
            if (rect.Contains(ChatGroupBoxManger .MounsePos))
            {
                sb = new SolidBrush(ChatGroupBoxManger.MouseOnItemColor);
            }
            else
            {
                sb=new SolidBrush (ChatGroupBoxManger .ForeItemColor);
            }
                 
            g.FillRectangle(sb, rect);

            PaintText(g, rect, sb);

            this.Bounds = rect;
            rect = GetNextBounds(rect);
            //绘制子Item
            if (IsOpen)
            {
                foreach (var item in _Items)
                {
                    rect = item.DrawItem(g, rect);
                }
            }

            //计算Rect.

            return rect;


        }

        private void PaintText(Graphics g, Rectangle rect,SolidBrush sb)
        {
            if (_Items.Count > 0)
                DrawIcon(g, rect, _IsOpen);
            StringFormat sf = new StringFormat()
            {
                LineAlignment = StringAlignment.Center,
                Alignment = StringAlignment.Near,
                FormatFlags = StringFormatFlags.NoClip,
            };
            sf.SetTabStops(0.0F, new float[] { 20.0F });
            sb.Color = ChatGroupBoxManger.ForeColor;
            g.DrawString(string.Format("\t{0}[{1}]", Text, _Items.Count),ChatGroupBoxManger .Font , sb, rect, sf);
        
        }

        public override void MouseOn(Point mousePos)
        {
            if (Bounds.Contains(mousePos))
            {
                Owner.Invalidate(Bounds);
            }
        }

        #region IChatItem 成员

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

        private ChatSubCollection _Items;
        public ChatSubCollection Items
        {
            get { return _Items ; }
        }

        #endregion
    }

    #endregion


    /// <summary>
    /// 
    /// </summary>
    public class ChatGroupBox : ScrollableControl
    {

        
        #region 字段与变量

        Rectangle _TempRect;

        #endregion 字段与变量

        #region 构造函数
        public ChatGroupBox()
        {
            this.AutoScroll = true;
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            _TempRect = new Rectangle(0, 0, 0, 0);
            _Items = new ChatGroupCollection(this);
        }
        #endregion 构造函数

        #region 属性

        private ChatGroupCollection  _Items;
        /// <summary>
        /// 
        /// </summary>
        public ChatGroupCollection  Items
        {
            get { return _Items; } 
        }

        public IChatCellItem SelectCellItem
        {
            get
            {
                return ChatGroupBoxManger.MouseSelectCellItem;
            }
            set
            {
                if (value != null &&
                    !ChatGroupBoxManger.MouseSelectCellItem.Equals(value))
                {
                    //滑动 项

                }
                     
            }
        }


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

        #region 重载函数

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics .TranslateTransform(-this.HorizontalScroll.Value, -this.VerticalScroll.Value);
            try
            {
                Rectangle rect = new Rectangle(0, 0, this.Width, 0);
                foreach (var item in _Items )
                {
                    rect=item.DrawItem(e.Graphics, rect);
                    //EnsureBounds(rect);
                    //rect = GetNextBounds();
                    
                }
                Debug.WriteLine("OnPaint");
                if(this.Height >=rect .Y)
                    rect = new Rectangle(0, 0, rect.Width, rect.Y);
                else
                {
                    rect = new Rectangle(0, 0, rect.Width - 15, rect.Y);
                }
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
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
            

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            ChatGroupBoxManger.MounsePos = e.Location;

            foreach (var item in _Items )
            {
                item.MouseOn(e.Location);
            }

            base.OnMouseMove(e);
        }


        #endregion 重载函数

        #region 绘制函数

        #endregion 绘制函数

        #region 私有函数

        #endregion 私有函数

    }

    #region 接口的集合

    /// <summary>
    /// 包含IChatItem的集合.
    /// </summary>
    public class ChatGroupCollection:ChatCollection<IChatItem>
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        public ChatGroupCollection(Control owner):base(owner)
        {           

        }
        #endregion 构造函数
    }

 
    /// <summary>
    /// 包含IChatCellItem的集合.
    /// </summary>
    public class ChatCellCollection : ChatCollection<IChatCellItem>
    {
        private IChatSubItem _Father;

        public ChatCellCollection(IChatSubItem father)
        {
            _Father = father;
        }


        public override void Add(IChatCellItem item)
        {
            item.Father = _Father;
            base.Add(item);
        }

        public override void AddRange(IChatCellItem[] items)
        {
            foreach (var item in items)
            {
                item.Father = _Father;
            }
            base.AddRange(items);
        }

        public override void Insert(int index, IChatCellItem item)
        {
            item.Father = _Father;
            base.Insert(index, item);
        }
    }


    /// <summary>
    /// 包含IChatSubItem的集合.
    /// </summary>
    public class ChatSubCollection : IList<IChatSubItem> 
    {
        #region 字段与变量
        /// <summary>
        /// 元素的个数.
        /// </summary>
        private int _Count;

        private IChatSubItem[] _Items;

        private Control _Owner;

        private IChatItem _Father;

        #endregion 字段与变量

        #region 构造函数

        public ChatSubCollection(IChatItem item)
        {
            _Father = item;
            _Owner = null;
        }

        public ChatSubCollection(Control owner)
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
                _Items = new IChatSubItem[Math.Max(elements, 4)];
            }
            else if (this.Count + elements > _Items.Length)
            {
                IChatSubItem[] temp = new IChatSubItem[Math.Max(this.Count + elements, _Items.Length * 2)];
                _Items.CopyTo(temp, 0);
                _Items = temp;
            }
        }
        #endregion 私有函数

        #region IList<IChatSubItem> 成员

        public int IndexOf(IChatSubItem item)
        {
            return Array.IndexOf(_Items, item);
        }

        public void Insert(int index, IChatSubItem item)
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

        public IChatSubItem this[int index]
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

        #region ICollection<IChatSubItem> 成员

        public void Add(IChatSubItem item)
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

        public void AddRange(IChatSubItem[] items)
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

        public bool Contains(IChatSubItem item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(IChatSubItem[] array, int arrayIndex)
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

        public bool Remove(IChatSubItem item)
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

        #region IEnumerable<IChatSubItem> 成员

        public IEnumerator<IChatSubItem> GetEnumerator()
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
    public class ChatItemCollection:IList<IItem>
    {
        #region 字段与变量
        /// <summary>
        /// 元素的个数.
        /// </summary>
        private int _Count;

        private IItem[] _Items;

        private Control _Owner;


        #endregion 字段与变量

        #region 构造函数

        public ChatItemCollection()
        {
            _Owner = null;
        }

        public ChatItemCollection(Control owner)
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
                _Items = new IItem[Math.Max(elements, 4)];
            }
            else if (this.Count + elements > _Items.Length)
            {
                IItem[] temp = new IItem[Math.Max(this.Count + elements, _Items.Length * 2)];
                _Items.CopyTo(temp, 0);
                _Items = temp;
            }
        }
        #endregion 私有函数

        #region IList<IItem> 成员

        public int IndexOf(IItem item)
        {
            return Array.IndexOf(_Items, item);
        }

        public void Insert(int index, IItem item)
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

        public IItem this[int index]
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

        #region ICollection<IItem> 成员

        public void Add(IItem item)
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

        public void AddRange(IItem[] items)
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
            if (_Owner == null) _Owner.Invalidate();
        }

        public void Clear()
        {
            _Count = 0;
            _Items = null;
            Invalidate();
        }

        public bool Contains(IItem item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(IItem[] array, int arrayIndex)
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

        public bool Remove(IItem item)
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

        #region IEnumerable<IItem> 成员

        public IEnumerator<IItem> GetEnumerator()
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
    public class ChatCollection<T>:IList<T> where T:IItem
    {
        #region 字段与变量
        /// <summary>
        /// 元素的个数.
        /// </summary>
        private int _Count;

        private T[] _Items;

        private Control _Owner;


        #endregion 字段与变量

        #region 构造函数

        public ChatCollection()
        {
            _Owner = null;
        }

        public ChatCollection(Control owner)
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
        protected void EnsureSpace(int elements)
        {
            if (_Items == null)
            {
                _Items = new T[Math.Max(elements, 4)];
            }
            else if (this.Count + elements > _Items.Length)
            {
                T[] temp = new T[Math.Max(this.Count + elements, _Items.Length * 2)];
                _Items.CopyTo(temp, 0);
                _Items = temp;
            }
        }
        #endregion 私有函数

        #region IList<T> 成员

        public int IndexOf(T item)
        {
            return Array.IndexOf(_Items, item);
        }

        public virtual void Insert(int index, T item)
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

        public T this[int index]
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

        #region ICollection<T> 成员

        public virtual  void Add(T item)
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

        public virtual void AddRange(T[] items)
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

        protected void Invalidate()
        {
            if (_Owner != null) _Owner.Invalidate();
        }

        public void Clear()
        {
            _Count = 0;
            _Items = null;
            Invalidate();
        }

        public bool Contains(T item)
        {
            return IndexOf(item) != -1;
        }

        public void CopyTo(T[] array, int arrayIndex)
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

        public bool Remove(T item)
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

        #region IEnumerable<T> 成员

        public IEnumerator<T> GetEnumerator()
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
#endregion
}
