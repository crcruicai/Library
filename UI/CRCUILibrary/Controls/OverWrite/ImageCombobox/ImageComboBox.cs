using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-11-26
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    [ToolboxBitmap(typeof(ComboBox))]
    public class ImageComboBox : ComboBoxEx
    {
        #region Fields

        private Image _DefaultImage;
        private ImageList _DefaultImageList;
        private string _EmptyTextTip = string.Empty;
        private Color _EmptyTextTipColor = Color.DarkGray;
        private ImageList _ImageList;
        private int _Indent = 10;
        private ImageComboBoxItemCollection _Items;
        private EditorNativeWimdow _NativeWimdow;

        #endregion

        #region Constructors

        public ImageComboBox()
        {
            base.DrawMode = DrawMode.OwnerDrawFixed;
            base.ItemHeight = 14;
            _Items = new ImageComboBoxItemCollection(this);
        }

        #endregion

        #region Properties

        [Localizable(true), MergableProperty(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public new ImageComboBoxItemCollection Items
        {
            get { return _Items; }
        }

        [DefaultValue((string) null)]
        public ImageList ImageList
        {
            get { return _ImageList; }
            set
            {
                if(_ImageList != value)
                {
                    _ImageList = value;
                }
            }
        }

        [DefaultValue(typeof(Image), "")]
        public Image DefaultImage
        {
            get { return _DefaultImage; }
            set
            {
                if(_DefaultImage != value)
                {
                    _DefaultImage = value;
                    if(_DefaultImage == null && _DefaultImageList != null)
                    {
                        _DefaultImageList.Dispose();
                        _DefaultImageList = null;
                    }

                    if(_DefaultImage != null)
                    {
                        _DefaultImageList = new ImageList {ColorDepth = ColorDepth.Depth24Bit, ImageSize = new Size(ItemHeight, ItemHeight)};
                        _DefaultImageList.Images.Add(_DefaultImage);
                    }

                    if(DropDownStyle != ComboBoxStyle.DropDownList)
                    {
                        Invalidate(true);
                    }
                }
            }
        }

        [DefaultValue("")]
        public string EmptyTextTip
        {
            get { return _EmptyTextTip; }
            set
            {
                if(DropDownStyle == ComboBoxStyle.DropDown)
                {
                    _EmptyTextTip = value;
                    base.Invalidate(true);
                }
            }
        }

        [DefaultValue(typeof(Color), "DarkGray")]
        public Color EmptyTextTipColor
        {
            get { return _EmptyTextTipColor; }
            set
            {
                if(DropDownStyle == ComboBoxStyle.DropDown)
                {
                    _EmptyTextTipColor = value;
                    base.Invalidate(true);
                }
            }
        }

        [DefaultValue(10)]
        public int Indent
        {
            get { return _Indent; }
            set
            {
                if(_Indent != value)
                {
                    _Indent = value;
                    base.RefreshItems();
                }
            }
        }

        [DefaultValue(14)]
        public new int ItemHeight
        {
            get { return base.ItemHeight; }
            set { base.ItemHeight = value; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawMode DrawMode
        {
            get { return base.DrawMode; }
            set { base.DrawMode = DrawMode.OwnerDrawFixed; }
        }

        [Browsable(false), Bindable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageComboBoxItem SelectedItem
        {
            get
            {
                if(base.SelectedItem == null)
                {
                    return null;
                }
                return base.SelectedItem as ImageComboBoxItem;
            }
            set { base.SelectedItem = value; }
        }

        public new ComboBoxStyle DropDownStyle
        {
            get { return base.DropDownStyle; }
            set
            {
                if(base.DropDownStyle != value)
                {
                    if(value == ComboBoxStyle.DropDownList)
                    {
                        if(_NativeWimdow != null)
                        {
                            _NativeWimdow.Dispose();
                            _NativeWimdow = null;
                        }
                    }
                    base.DropDownStyle = value;
                }
            }
        }

        protected internal ObjectCollection OldItems
        {
            get { return base.Items; }
        }

        internal ImageList DefaultImageList
        {
            get { return _DefaultImageList; }
        }

        #endregion

        #region Ovveride Methods

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if(DropDownStyle != ComboBoxStyle.DropDownList && !DesignMode)
            {
                if(_NativeWimdow == null)
                {
                    _NativeWimdow = new EditorNativeWimdow(this);
                }
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if(_NativeWimdow != null)
            {
                _NativeWimdow.Dispose();
                _NativeWimdow = null;
            }
            base.OnHandleDestroyed(e);
        }

        protected override void OnDropDown(EventArgs e)
        {
            base.OnDropDown(e);

            int ddWidth = 0;
            int scrollBarWidth = Items.Count > MaxDropDownItems ? SystemInformation.VerticalScrollBarWidth : 0;
            Graphics g = CreateGraphics();

            foreach(ImageComboBoxItem item in Items)
            {
                int textWidth = g.MeasureString(item.Text, Font).ToSize().Width;
                int itemWidth = textWidth + ItemHeight + 8 + _Indent*item.Level + scrollBarWidth;

                if(itemWidth > ddWidth)
                    ddWidth = itemWidth;
            }

            DropDownWidth = (ddWidth > Width) ? ddWidth : Width;
            g.Dispose();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if(e.Index != -1)
            {
                ImageComboBoxItem item = Items[e.Index];
                Graphics g = e.Graphics;
                Rectangle bounds = e.Bounds;

                int indentOffset = Indent*item.Level;

                if((e.State & DrawItemState.ComboBoxEdit) == DrawItemState.ComboBoxEdit)
                {
                    indentOffset = 0;
                }

                int imageWidth = bounds.Height;
                TextFormatFlags format = TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordBreak;

                Rectangle imageRect = new Rectangle(bounds.Left + indentOffset + 2, bounds.Top, imageWidth, imageWidth);
                Rectangle textRect = new Rectangle(imageRect.Right + 3, bounds.Y, bounds.Width - imageRect.Width - indentOffset - 5, bounds.Height);

                var backRect = new Rectangle(textRect.X, textRect.Y + 1, textRect.Width, textRect.Height - 2);

                backRect.Width = TextRenderer.MeasureText(item.Text, e.Font, textRect.Size, format).Width;

                if(base.RightToLeft == RightToLeft.Yes)
                {
                    imageRect.X = bounds.Right - imageRect.Right;
                    textRect.X = bounds.Right - textRect.Right;
                    backRect.X = textRect.Right - backRect.Width;
                }

                bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

                Color backColor = selected ? SystemColors.Highlight : base.BackColor;

                using(Brush backBrush = new SolidBrush(backColor))
                {
                    g.FillRectangle(backBrush, backRect);
                }

                if(selected)
                {
                    ControlPaint.DrawFocusRectangle(g, backRect);
                }

                Image image = item.Image;
                if(image != null)
                {
                    using(var graphics = new InterpolationModeGraphics(g, InterpolationMode.HighQualityBicubic))
                    {
                        if(selected)
                        {
                            IntPtr hIcon = NativeMethods.ImageList_GetIcon(ImageList.Handle, item.ImageIndexer.ActualIndex, (int) NativeMethods.ImageListDrawFlags.ILD_SELECTED);
                            g.DrawIcon(Icon.FromHandle(hIcon), imageRect);
                            NativeMethods.DestroyIcon(hIcon);
                        }
                        else
                        {
                            g.DrawImage(image, imageRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                        }
                    }
                }

                TextRenderer.DrawText(g, item.Text, e.Font, textRect, base.ForeColor, format);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(disposing)
            {
                _ImageList = null;
                _DefaultImage = null;
                _DefaultImageList = null;
                _Items = null;
            }
        }

        #endregion

        #region EditorNativeWimdow Class

        private class EditorNativeWimdow : NativeWindow, IDisposable
        {
            #region Fields

            private const int EC_LEFTMARGIN = 0x1;
            private const int EC_RIGHTMARGIN = 0x2;
            private const int EC_USEFONTINFO = 0xFFFF;
            private const int EM_SETMARGINS = 0xD3;
            private const int EM_GETMARGINS = 0xD4;
            private ImageComboBox _Owner;

            #endregion

            #region Constructors

            public EditorNativeWimdow(ImageComboBox owner)
            {
                _Owner = owner;
                Attach();
            }

            #endregion

            #region Private Methods

            private void Attach()
            {
                if(!Handle.Equals(IntPtr.Zero))
                {
                    ReleaseHandle();
                }
                AssignHandle(_Owner.EditHandle);
                SetMargin();
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                switch(m.Msg)
                {
                    case (int) NativeMethods.WindowsMessage.WM_SETFONT:
                        SetMargin();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_PAINT:
                        RePaint();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_SETFOCUS:
                    case (int) NativeMethods.WindowsMessage.WM_KILLFOCUS:
                        RePaint();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_LBUTTONDOWN:
                    case (int) NativeMethods.WindowsMessage.WM_RBUTTONDOWN:
                    case (int) NativeMethods.WindowsMessage.WM_MBUTTONDOWN:
                        RePaint();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_LBUTTONUP:
                    case (int) NativeMethods.WindowsMessage.WM_RBUTTONUP:
                    case (int) NativeMethods.WindowsMessage.WM_MBUTTONUP:
                        RePaint();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_LBUTTONDBLCLK:
                    case (int) NativeMethods.WindowsMessage.WM_RBUTTONDBLCLK:
                    case (int) NativeMethods.WindowsMessage.WM_MBUTTONDBLCLK:
                        RePaint();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_KEYDOWN:
                    case (int) NativeMethods.WindowsMessage.WM_CHAR:
                    case (int) NativeMethods.WindowsMessage.WM_KEYUP:
                        RePaint();
                        break;
                    case (int) NativeMethods.WindowsMessage.WM_MOUSEMOVE:
                        if(!m.WParam.Equals(IntPtr.Zero))
                        {
                            RePaint();
                        }
                        break;
                }
            }

            internal void SetMargin()
            {
                NearMargin(Handle, _Owner.ItemHeight + 5);
            }

            private static bool IsRightToLeft(IntPtr handle)
            {
                int style = NativeMethods.GetWindowLong(handle, (int) NativeMethods.GWL.GWL_EXSTYLE);
                return (((style & (int) NativeMethods.WS_EX.WS_EX_RIGHT) == (int) NativeMethods.WS_EX.WS_EX_RIGHT) || ((style & (int) NativeMethods.WS_EX.WS_EX_RTLREADING) == (int) NativeMethods.WS_EX.WS_EX_RTLREADING) || ((style & (int) NativeMethods.WS_EX.WS_EX_LEFTSCROLLBAR) == (int) NativeMethods.WS_EX.WS_EX_LEFTSCROLLBAR));
            }

            private static void FarMargin(IntPtr handle, int margin)
            {
                int message = IsRightToLeft(handle) ? EC_LEFTMARGIN : EC_RIGHTMARGIN;
                if(message == EC_LEFTMARGIN)
                {
                    margin = margin & 0xFFFF;
                }
                else
                {
                    margin = margin*0x10000;
                }
                NativeMethods.SendMessage(handle, EM_SETMARGINS, message, margin);
            }

            internal static void NearMargin(IntPtr handle, int margin)
            {
                int message = IsRightToLeft(handle) ? EC_RIGHTMARGIN : EC_LEFTMARGIN;
                if(message == EC_LEFTMARGIN)
                {
                    margin = margin & 0xFFFF;
                }
                else
                {
                    margin = margin*0x10000;
                }
                NativeMethods.SendMessage(handle, EM_SETMARGINS, message, margin);
            }

            private void RePaint()
            {
                ImageComboBoxItem item = _Owner.SelectedItem;

                var rcClient = new NativeMethods.RECT();
                NativeMethods.GetClientRect(Handle, ref rcClient);
                bool rightToLeft = IsRightToLeft(Handle);

                IntPtr handle = Handle;
                IntPtr hdc = NativeMethods.GetDC(handle);
                if(hdc == IntPtr.Zero)
                {
                    return;
                }
                try
                {
                    using(Graphics g = Graphics.FromHdc(hdc))
                    {
                        int itemSize = _Owner.ItemHeight;
                        var imageRect = new Rectangle(0, rcClient.Top + (rcClient.Bottom - itemSize)/2, itemSize, itemSize);
                        var textRect = new Rectangle(0, 0, rcClient.Right - itemSize - 6, rcClient.Bottom);

                        if(rightToLeft)
                        {
                            imageRect.X = rcClient.Right - itemSize - 2;
                            textRect.X = 2;
                        }
                        else
                        {
                            imageRect.X = 2;
                            textRect.X = imageRect.Right + 2;
                        }

                        if(_Owner.Text.Length == 0)
                        {
                            DrawImage(g, imageRect, _Owner.DefaultImage, _Owner.DefaultImageList, 0, _Owner.Focused);

                            if(_Owner.Text.Length == 0 && !string.IsNullOrEmpty(_Owner.EmptyTextTip) && !_Owner.Focused)
                            {
                                TextFormatFlags format = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;

                                if(_Owner.RightToLeft == RightToLeft.Yes)
                                {
                                    format |= (TextFormatFlags.RightToLeft | TextFormatFlags.Right);
                                }

                                TextRenderer.DrawText(g, _Owner.EmptyTextTip, _Owner.Font, textRect, _Owner.EmptyTextTipColor, format);
                            }
                            return;
                        }

                        if(_Owner.Text.Length > 0)
                        {
                            using(var brush = new SolidBrush(_Owner.BackColor))
                            {
                                g.FillRectangle(brush, imageRect);
                            }
                        }

                        if(_Owner.Items.Count == 0)
                        {
                            DrawImage(g, imageRect, _Owner.DefaultImage, _Owner.DefaultImageList, 0, _Owner.Focused);
                            return;
                        }

                        if(item == null)
                        {
                            return;
                        }

                        DrawImage(g, imageRect, item.Image, _Owner.ImageList, item.ImageIndexer.ActualIndex, _Owner.Focused);
                    }
                } finally
                {
                    NativeMethods.ReleaseDC(handle, hdc);
                }
            }

            private void DrawImage(Graphics g, Rectangle imageRect, Image image, ImageList imageList, int imageIndex, bool focus)
            {
                using(var brush = new SolidBrush(_Owner.BackColor))
                {
                    g.FillRectangle(brush, imageRect);
                }

                if(image == null)
                {
                    return;
                }

                using(var graphics = new InterpolationModeGraphics(g, InterpolationMode.HighQualityBicubic))
                {
                    if(focus)
                    {
                        IntPtr hIcon = NativeMethods.ImageList_GetIcon(imageList.Handle, imageIndex, (int) NativeMethods.ImageListDrawFlags.ILD_SELECTED);
                        g.DrawIcon(Icon.FromHandle(hIcon), imageRect);
                        NativeMethods.DestroyIcon(hIcon);
                    }
                    else
                    {
                        g.DrawImage(image, imageRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
                    }
                }
            }

            #endregion

            #region IDisposable 成员

            public void Dispose()
            {
                _Owner = null;
                base.ReleaseHandle();
            }

            #endregion
        }

        #endregion

        #region ImageComboBoxItemCollection Class

        [ListBindable(false)]
        public class ImageComboBoxItemCollection : IList, ICollection, IEnumerable
        {
            #region Fields

            private readonly ImageComboBox _owner;

            #endregion

            #region Constructors

            public ImageComboBoxItemCollection(ImageComboBox owner)
            {
                _owner = owner;
            }

            #endregion

            #region Properties

            internal ImageComboBox Owner
            {
                get { return _owner; }
            }

            public ImageComboBoxItem this[int index]
            {
                get { return Owner.OldItems[index] as ImageComboBoxItem; }
                set { Owner.OldItems[index] = value; }
            }

            public int Count
            {
                get { return Owner.OldItems.Count; }
            }

            public bool IsReadOnly
            {
                get { return Owner.OldItems.IsReadOnly; }
            }

            #endregion

            #region Public Methods

            public int Add(ImageComboBoxItem item)
            {
                if(item == null)
                {
                    throw new ArgumentNullException("item");
                }
                item.Host(Owner);
                return Owner.OldItems.Add(item);
            }

            public void AddRange(ImageComboBoxItemCollection value)
            {
                foreach(ImageComboBoxItem item in value)
                    Add(item);
            }

            public void AddRange(ImageComboBoxItem[] items)
            {
                foreach(ImageComboBoxItem item in items)
                    Add(item);
            }

            public void Clear()
            {
                Owner.OldItems.Clear();
            }

            public bool Contains(ImageComboBoxItem item)
            {
                return Owner.OldItems.Contains(item);
            }

            public void CopyTo(ImageComboBoxItem[] destination, int arrayIndex)
            {
                Owner.OldItems.CopyTo(destination, arrayIndex);
            }

            public int IndexOf(ImageComboBoxItem item)
            {
                return Owner.OldItems.IndexOf(item);
            }

            public void Insert(int index, ImageComboBoxItem item)
            {
                if(item == null)
                {
                    throw new ArgumentNullException("item");
                }
                item.Host(Owner);
                Owner.OldItems.Insert(index, item);
            }

            public void Remove(ImageComboBoxItem item)
            {
                Owner.OldItems.Remove(item);
            }

            public void RemoveAt(int index)
            {
                Owner.OldItems.RemoveAt(index);
            }

            public IEnumerator GetEnumerator()
            {
                return Owner.OldItems.GetEnumerator();
            }

            #endregion

            #region IList 成员

            int IList.Add(object value)
            {
                if(!(value is ImageComboBoxItem))
                {
                    throw new ArgumentException();
                }
                return Add(value as ImageComboBoxItem);
            }

            void IList.Clear()
            {
                Clear();
            }

            bool IList.Contains(object value)
            {
                return Contains(value as ImageComboBoxItem);
            }

            int IList.IndexOf(object value)
            {
                return IndexOf(value as ImageComboBoxItem);
            }

            void IList.Insert(int index, object value)
            {
                if(!(value is ImageComboBoxItem))
                {
                    throw new ArgumentException();
                }
                Insert(index, value as ImageComboBoxItem);
            }

            bool IList.IsFixedSize
            {
                get { return false; }
            }

            bool IList.IsReadOnly
            {
                get { return IsReadOnly; }
            }

            void IList.Remove(object value)
            {
                Remove(value as ImageComboBoxItem);
            }

            void IList.RemoveAt(int index)
            {
                RemoveAt(index);
            }

            object IList.this[int index]
            {
                get { return this[index]; }
                set
                {
                    if(!(value is ImageComboBoxItem))
                    {
                        throw new ArgumentException();
                    }
                    this[index] = value as ImageComboBoxItem;
                }
            }

            #endregion

            #region ICollection 成员

            void ICollection.CopyTo(Array array, int index)
            {
                CopyTo((ImageComboBoxItem[]) array, index);
            }

            int ICollection.Count
            {
                get { return Count; }
            }

            bool ICollection.IsSynchronized
            {
                get { return false; }
            }

            object ICollection.SyncRoot
            {
                get { return this; }
            }

            #endregion

            #region IEnumerable 成员

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            #endregion
        }

        #endregion
    }
}