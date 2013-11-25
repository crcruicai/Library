using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.Runtime.InteropServices;

namespace CRC.Controls
{
    
    public class EXListView : ListView
    {
        /// <summary>
        /// clicked ListViewSubItem
        /// </summary>
        private ListViewItem.ListViewSubItem _ClickedSubItem; 
        /// <summary>
        /// clicked ListViewItem
        /// </summary>
        private ListViewItem _ClickedItem; 
        /// <summary>
        /// index of doubleclicked ListViewSubItem
        /// </summary>
        private int _ColumnIndex;
        /// <summary>
        /// the default edit control
        /// </summary>
        private TextBox _EditTextBox; 
        /// <summary>
        /// index of clicked ColumnHeader
        /// </summary>
        private int _SortColumnIndex; 
        /// <summary>
        /// color of items in sorted column
        /// </summary>
        private Brush _SortColumnBrush; 
        /// <summary>
        /// color of highlighted items
        /// </summary>
        private Brush _HighLightBrush; 
        /// <summary>
        /// padding of the embedded controls
        /// </summary>
        private int _ControlPadding;
        /// <summary>
        /// 
        /// </summary>
        private ArrayList _Controls;

        private const UInt32 LVM_FIRST = 0x1000;
        private const UInt32 LVM_SCROLL = (LVM_FIRST + 20);
        private const int WM_HSCROLL = 0x114;
        private const int WM_VSCROLL = 0x115;
        private const int WM_MOUSEWHEEL = 0x020A;
        private const int WM_PAINT = 0x000F;

        private struct EmbeddedControl
        {
            public Control Control;
            public EXControlListViewSubItem SubItem;
        }

      

        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hWnd, UInt32 m, int wParam, int lParam);

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT)
            {
                foreach (EmbeddedControl c in _Controls)
                {
                    Rectangle r = c.SubItem.Bounds;
                    if (r.Y > 0 && r.Y < this.ClientRectangle.Height)
                    {
                        c.Control.Visible = true;
                        c.Control.Bounds = new Rectangle(r.X + _ControlPadding, r.Y + _ControlPadding, r.Width - (2 * _ControlPadding), r.Height - (2 * _ControlPadding));
                    }
                    else
                    {
                        c.Control.Visible = false;
                    }
                }
            }
            switch (m.Msg)
            {
                case WM_HSCROLL:
                case WM_VSCROLL:
                case WM_MOUSEWHEEL:
                    this.Focus();
                    break;
            }
            base.WndProc(ref m);
        }

        private void ScrollMe(int x, int y)
        {
            SendMessage((IntPtr)this.Handle, LVM_SCROLL, x, y);
        }

        public EXListView()
        {
            _ControlPadding = 4;
            _Controls = new ArrayList();
            _SortColumnIndex = -1;
            _SortColumnBrush = SystemBrushes.ControlLight;
            _HighLightBrush = SystemBrushes.Highlight;
            this.OwnerDraw = true;
            this.FullRowSelect = true;
            this.View = View.Details;
            this.MouseDown += new MouseEventHandler(this_MouseDown);
            this.MouseDoubleClick += new MouseEventHandler(this_MouseDoubleClick);
            this.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(this_DrawColumnHeader);
            this.DrawSubItem += new DrawListViewSubItemEventHandler(this_DrawSubItem);
            this.MouseMove += new MouseEventHandler(this_MouseMove);
            this.ColumnClick += new ColumnClickEventHandler(this_ColumnClick);
            _EditTextBox = new TextBox();
            _EditTextBox.Visible = false;
            this.Controls.Add(_EditTextBox);
            _EditTextBox.Leave += new EventHandler(c_Leave);
            _EditTextBox.KeyPress += new KeyPressEventHandler(txtbx_KeyPress);
        }

        public void AddControlToSubItem(Control control, EXControlListViewSubItem subitem)
        {
            this.Controls.Add(control);
            subitem.Control = control;
            EmbeddedControl ec;
            ec.Control = control;
            ec.SubItem = subitem;
            this._Controls.Add(ec);
        }

        public void RemoveControlFromSubItem(EXControlListViewSubItem subitem)
        {
            Control c = subitem.Control;
            for (int i = 0; i < this._Controls.Count; i++)
            {
                if (((EmbeddedControl)this._Controls[i]).SubItem == subitem)
                {
                    this._Controls.RemoveAt(i);
                    subitem.Control = null;
                    this.Controls.Remove(c);
                    c.Dispose();
                    return;
                }
            }
        }

        public int ControlPadding
        {
            get { return _ControlPadding; }
            set { _ControlPadding = value; }
        }

        public Brush SortColumnBrush
        {
            get { return _SortColumnBrush; }
            set { _SortColumnBrush = value; }
        }

        public Brush HighLightBrush
        {
            get { return _HighLightBrush; }
            set { _HighLightBrush = value; }
        }

        private void txtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                _ClickedSubItem.Text = _EditTextBox.Text;
                _EditTextBox.Visible = false;
                _ClickedItem.Tag = null;
            }
        }

        private void c_Leave(object sender, EventArgs e)
        {
            Control c = (Control)sender;
            _ClickedSubItem.Text = c.Text;
            c.Visible = false;
            _ClickedItem.Tag = null;
        }

        private void this_MouseDown(object sender, MouseEventArgs e)
        {
            ListViewHitTestInfo lstvinfo = this.HitTest(e.X, e.Y);
            ListViewItem.ListViewSubItem subitem = lstvinfo.SubItem;
            if (subitem == null) return;
            int subx = subitem.Bounds.Left;
            if (subx < 0)
            {
                this.ScrollMe(subx, 0);
            }
        }

        private void this_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EXListViewItem lstvItem = this.GetItemAt(e.X, e.Y) as EXListViewItem;
            if (lstvItem == null) return;
            _ClickedItem = lstvItem;
            int x = lstvItem.Bounds.Left;
            int i;
            for (i = 0; i < this.Columns.Count; i++)
            {
                x = x + this.Columns[i].Width;
                if (x > e.X)
                {
                    x = x - this.Columns[i].Width;
                    _ClickedSubItem = lstvItem.SubItems[i];
                    _ColumnIndex = i;
                    break;
                }
            }
            if (!(this.Columns[i] is EXColumnHeader)) return;
            EXColumnHeader col = (EXColumnHeader)this.Columns[i];
            if (col.GetType() == typeof(EXEditableColumnHeader))
            {
                EXEditableColumnHeader editcol = (EXEditableColumnHeader)col;
                if (editcol.Control != null)
                {
                    Control c = editcol.Control;
                    if (c.Tag != null)
                    {
                        this.Controls.Add(c);
                        c.Tag = null;
                        if (c is ComboBox)
                        {
                            ((ComboBox)c).SelectedValueChanged += new EventHandler(cmbx_SelectedValueChanged);
                        }
                        c.Leave += new EventHandler(c_Leave);
                    }
                    c.Location = new Point(x, this.GetItemRect(this.Items.IndexOf(lstvItem)).Y);
                    c.Width = this.Columns[i].Width;
                    if (c.Width > this.Width) c.Width = this.ClientRectangle.Width;
                    c.Text = _ClickedSubItem.Text;
                    c.Visible = true;
                    c.BringToFront();
                    c.Focus();
                }
                else
                {
                    _EditTextBox.Location = new Point(x, this.GetItemRect(this.Items.IndexOf(lstvItem)).Y);
                    _EditTextBox.Width = this.Columns[i].Width;
                    if (_EditTextBox.Width > this.Width) _EditTextBox.Width = this.ClientRectangle.Width;
                    _EditTextBox.Text = _ClickedSubItem.Text;
                    _EditTextBox.Visible = true;
                    _EditTextBox.BringToFront();
                    _EditTextBox.Focus();
                }
            }
            else if (col.GetType() == typeof(EXBoolColumnHeader))
            {
                EXBoolColumnHeader boolcol = (EXBoolColumnHeader)col;
                if (boolcol.Editable)
                {
                    EXBoolListViewSubItem boolsubitem = (EXBoolListViewSubItem)_ClickedSubItem;
                    if (boolsubitem.BoolValue == true)
                    {
                        boolsubitem.BoolValue = false;
                    }
                    else
                    {
                        boolsubitem.BoolValue = true;
                    }
                    this.Invalidate(boolsubitem.Bounds);
                }
            }
        }

        private void cmbx_SelectedValueChanged(object sender, EventArgs e)
        {
            if (((Control)sender).Visible == false || _ClickedSubItem == null) return;
            if (sender.GetType() == typeof(EXComboBox))
            {
                EXComboBox excmbx = (EXComboBox)sender;
                object item = excmbx.SelectedItem;
                //Is this an combobox item with one image?
                if (item.GetType() == typeof(EXComboBox.ImageItem))
                {
                    EXComboBox.ImageItem imgitem = (EXComboBox.ImageItem)item;
                    //Is the first column clicked -- in that case it's a ListViewItem
                    if (_ColumnIndex == 0)
                    {
                        if (_ClickedItem.GetType() == typeof(EXImageListViewItem))
                        {
                            ((EXImageListViewItem)_ClickedItem).MyImage = imgitem.Image;
                        }
                        else if (_ClickedItem.GetType() == typeof(EXMultipleImagesListViewItem))
                        {
                            EXMultipleImagesListViewItem imglstvitem = (EXMultipleImagesListViewItem)_ClickedItem;
                            imglstvitem.Images.Clear();
                            imglstvitem.Images.AddRange(new object[] { imgitem.Image });
                        }
                        //another column than the first one is clicked, so we have a ListViewSubItem
                    }
                    else
                    {
                        if (_ClickedSubItem.GetType() == typeof(EXImageListViewSubItem))
                        {
                            EXImageListViewSubItem imgsub = (EXImageListViewSubItem)_ClickedSubItem;
                            imgsub.MyImage = imgitem.Image;
                        }
                        else if (_ClickedSubItem.GetType() == typeof(EXMultipleImagesListViewSubItem))
                        {
                            EXMultipleImagesListViewSubItem imgsub = (EXMultipleImagesListViewSubItem)_ClickedSubItem;
                            imgsub.Images.Clear();
                            imgsub.Images.Add(imgitem.Image);
                            imgsub.Value = imgitem.Value;
                        }
                    }
                    //or is this a combobox item with multiple images?
                }
                else if (item.GetType() == typeof(EXComboBox.MultipleImagesItem))
                {
                    EXComboBox.MultipleImagesItem imgitem = (EXComboBox.MultipleImagesItem)item;
                    if (_ColumnIndex == 0)
                    {
                        if (_ClickedItem.GetType() == typeof(EXImageListViewItem))
                        {
                            ((EXImageListViewItem)_ClickedItem).MyImage = (Image)imgitem.Images[0];
                        }
                        else if (_ClickedItem.GetType() == typeof(EXMultipleImagesListViewItem))
                        {
                            EXMultipleImagesListViewItem imglstvitem = (EXMultipleImagesListViewItem)_ClickedItem;
                            imglstvitem.Images.Clear();
                            imglstvitem.Images.AddRange(imgitem.Images);
                        }
                    }
                    else
                    {
                        if (_ClickedSubItem.GetType() == typeof(EXImageListViewSubItem))
                        {
                            EXImageListViewSubItem imgsub = (EXImageListViewSubItem)_ClickedSubItem;
                            if (imgitem.Images != null)
                            {
                                imgsub.MyImage = (Image)imgitem.Images[0];
                            }
                        }
                        else if (_ClickedSubItem.GetType() == typeof(EXMultipleImagesListViewSubItem))
                        {
                            EXMultipleImagesListViewSubItem imgsub = (EXMultipleImagesListViewSubItem)_ClickedSubItem;
                            imgsub.Images.Clear();
                            imgsub.Images.AddRange(imgitem.Images);
                            imgsub.Value = imgitem.Value;
                        }
                    }
                }
            }
            ComboBox c = (ComboBox)sender;
            _ClickedSubItem.Text = c.Text;
            c.Visible = false;
            _ClickedItem.Tag = null;
        }

        private void this_MouseMove(object sender, MouseEventArgs e)
        {
            ListViewItem item = this.GetItemAt(e.X, e.Y);
            if (item != null && item.Tag == null)
            {
                this.Invalidate(item.Bounds);
                item.Tag = "t";
            }
        }

        private void this_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void this_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawBackground();
            if (e.ColumnIndex == _SortColumnIndex)
            {
                e.Graphics.FillRectangle(_SortColumnBrush, e.Bounds);
            }
            if ((e.ItemState & ListViewItemStates.Selected) != 0)
            {
                e.Graphics.FillRectangle(_HighLightBrush, e.Bounds);
            }
            int fonty = e.Bounds.Y + ((int)(e.Bounds.Height / 2)) - ((int)(e.SubItem.Font.Height / 2));
            int x = e.Bounds.X + 2;
            if (e.ColumnIndex == 0)
            {
                EXListViewItem item = (EXListViewItem)e.Item;
                if (item.GetType() == typeof(EXImageListViewItem))
                {
                    EXImageListViewItem imageitem = (EXImageListViewItem)item;
                    if (imageitem.MyImage != null)
                    {
                        Image img = imageitem.MyImage;
                        int imgy = e.Bounds.Y + ((int)(e.Bounds.Height / 2)) - ((int)(img.Height / 2));
                        e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                        x += img.Width + 2;
                    }
                }
                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), x, fonty);
                return;
            }
            EXListViewSubItemAB subitem = e.SubItem as EXListViewSubItemAB;
            if (subitem == null)
            {
                e.DrawDefault = true;
            }
            else
            {
                x = subitem.DoDraw(e, x, this.Columns[e.ColumnIndex] as EXColumnHeader);
                e.Graphics.DrawString(e.SubItem.Text, e.SubItem.Font, new SolidBrush(e.SubItem.ForeColor), x, fonty);
            }
        }

        private void this_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (this.Items.Count == 0) return;
            for (int i = 0; i < this.Columns.Count; i++)
            {
                this.Columns[i].ImageKey = null;
            }
            for (int i = 0; i < this.Items.Count; i++)
            {
                this.Items[i].Tag = null;
            }
            if (e.Column != _SortColumnIndex)
            {
                _SortColumnIndex = e.Column;
                this.Sorting = SortOrder.Ascending;
                this.Columns[e.Column].ImageKey = "up";
            }
            else
            {
                if (this.Sorting == SortOrder.Ascending)
                {
                    this.Sorting = SortOrder.Descending;
                    this.Columns[e.Column].ImageKey = "down";
                }
                else
                {
                    this.Sorting = SortOrder.Ascending;
                    this.Columns[e.Column].ImageKey = "up";
                }
            }
            if (_SortColumnIndex == 0)
            {
                //ListViewItem
                if (this.Items[0].GetType() == typeof(EXListViewItem))
                {
                    //sorting on text
                    this.ListViewItemSorter = new ListViewItemComparerText(e.Column, this.Sorting);
                }
                else
                {
                    //sorting on value
                    this.ListViewItemSorter = new ListViewItemComparerValue(e.Column, this.Sorting);
                }
            }
            else
            {
                //ListViewSubItem
                if (this.Items[0].SubItems[_SortColumnIndex].GetType() == typeof(EXListViewSubItemAB))
                {
                    //sorting on text
                    this.ListViewItemSorter = new ListViewSubItemComparerText(e.Column, this.Sorting);
                }
                else
                {
                    //sorting on value
                    this.ListViewItemSorter = new ListViewSubItemComparerValue(e.Column, this.Sorting);
                }
            }
        }

        class ListViewSubItemComparerText : System.Collections.IComparer
        {

            private int _col;
            private SortOrder _order;

            public ListViewSubItemComparerText()
            {
                _col = 0;
                _order = SortOrder.Ascending;
            }

            public ListViewSubItemComparerText(int col, SortOrder order)
            {
                _col = col;
                _order = order;
            }

            public int Compare(object x, object y)
            {
                int returnVal = -1;

                string xstr = ((ListViewItem)x).SubItems[_col].Text;
                string ystr = ((ListViewItem)y).SubItems[_col].Text;

                decimal dec_x;
                decimal dec_y;
                DateTime dat_x;
                DateTime dat_y;

                if (Decimal.TryParse(xstr, out dec_x) && Decimal.TryParse(ystr, out dec_y))
                {
                    returnVal = Decimal.Compare(dec_x, dec_y);
                }
                else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
                {
                    returnVal = DateTime.Compare(dat_x, dat_y);
                }
                else
                {
                    returnVal = String.Compare(xstr, ystr);
                }
                if (_order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

        }

        class ListViewSubItemComparerValue : System.Collections.IComparer
        {

            private int _col;
            private SortOrder _order;

            public ListViewSubItemComparerValue()
            {
                _col = 0;
                _order = SortOrder.Ascending;
            }

            public ListViewSubItemComparerValue(int col, SortOrder order)
            {
                _col = col;
                _order = order;
            }

            public int Compare(object x, object y)
            {
                int returnVal = -1;

                string xstr = ((EXListViewSubItemAB)((ListViewItem)x).SubItems[_col]).Value;
                string ystr = ((EXListViewSubItemAB)((ListViewItem)y).SubItems[_col]).Value;

                decimal dec_x;
                decimal dec_y;
                DateTime dat_x;
                DateTime dat_y;

                if (Decimal.TryParse(xstr, out dec_x) && Decimal.TryParse(ystr, out dec_y))
                {
                    returnVal = Decimal.Compare(dec_x, dec_y);
                }
                else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
                {
                    returnVal = DateTime.Compare(dat_x, dat_y);
                }
                else
                {
                    returnVal = String.Compare(xstr, ystr);
                }
                if (_order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

        }

        class ListViewItemComparerText : System.Collections.IComparer
        {

            private int _col;
            private SortOrder _order;

            public ListViewItemComparerText()
            {
                _col = 0;
                _order = SortOrder.Ascending;
            }

            public ListViewItemComparerText(int col, SortOrder order)
            {
                _col = col;
                _order = order;
            }

            public int Compare(object x, object y)
            {
                int returnVal = -1;

                string xstr = ((ListViewItem)x).Text;
                string ystr = ((ListViewItem)y).Text;

                decimal dec_x;
                decimal dec_y;
                DateTime dat_x;
                DateTime dat_y;

                if (Decimal.TryParse(xstr, out dec_x) && Decimal.TryParse(ystr, out dec_y))
                {
                    returnVal = Decimal.Compare(dec_x, dec_y);
                }
                else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
                {
                    returnVal = DateTime.Compare(dat_x, dat_y);
                }
                else
                {
                    returnVal = String.Compare(xstr, ystr);
                }
                if (_order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

        }

        class ListViewItemComparerValue : System.Collections.IComparer
        {

            private int _col;
            private SortOrder _order;

            public ListViewItemComparerValue()
            {
                _col = 0;
                _order = SortOrder.Ascending;
            }

            public ListViewItemComparerValue(int col, SortOrder order)
            {
                _col = col;
                _order = order;
            }

            public int Compare(object x, object y)
            {
                int returnVal = -1;

                string xstr = ((EXListViewItem)x).MyValue;
                string ystr = ((EXListViewItem)y).MyValue;

                decimal dec_x;
                decimal dec_y;
                DateTime dat_x;
                DateTime dat_y;

                if (Decimal.TryParse(xstr, out dec_x) && Decimal.TryParse(ystr, out dec_y))
                {
                    returnVal = Decimal.Compare(dec_x, dec_y);
                }
                else if (DateTime.TryParse(xstr, out dat_x) && DateTime.TryParse(ystr, out dat_y))
                {
                    returnVal = DateTime.Compare(dat_x, dat_y);
                }
                else
                {
                    returnVal = String.Compare(xstr, ystr);
                }
                if (_order == SortOrder.Descending) returnVal *= -1;
                return returnVal;
            }

        }

    }

    public class EXColumnHeader : ColumnHeader
    {

        public EXColumnHeader()
        {

        }

        public EXColumnHeader(string text)
        {
            this.Text = text;
        }

        public EXColumnHeader(string text, int width)
        {
            this.Text = text;
            this.Width = width;
        }

    }

    public class EXEditableColumnHeader : EXColumnHeader
    {

        private Control _Control;

        public EXEditableColumnHeader()
        {

        }

        public EXEditableColumnHeader(string text)
        {
            this.Text = text;
        }

        public EXEditableColumnHeader(string text, int width)
        {
            this.Text = text;
            this.Width = width;
        }

        public EXEditableColumnHeader(string text, Control control)
        {
            this.Text = text;
            this.Control = control;
        }

        public EXEditableColumnHeader(string text, Control control, int width)
        {
            this.Text = text;
            this.Control = control;
            this.Width = width;
        }

        public Control Control
        {
            get { return _Control; }
            set
            {
                _Control = value;
                _Control.Visible = false;
                _Control.Tag = "not_init";
            }
        }

    }

    public class EXBoolColumnHeader : EXColumnHeader
    {

        private Image _TrueImage;
        private Image _FalseImage;
        private bool _Editable;

        public EXBoolColumnHeader()
        {
            Init();
        }

        public EXBoolColumnHeader(string text)
        {
            Init();
            this.Text = text;
        }

        public EXBoolColumnHeader(string text, int width)
        {
            Init();
            this.Text = text;
            this.Width = width;
        }

        public EXBoolColumnHeader(string text, Image trueimage, Image falseimage)
        {
            Init();
            this.Text = text;
            _TrueImage = trueimage;
            _FalseImage = falseimage;
        }

        public EXBoolColumnHeader(string text, Image trueimage, Image falseimage, int width)
        {
            Init();
            this.Text = text;
            _TrueImage = trueimage;
            _FalseImage = falseimage;
            this.Width = width;
        }

        private void Init()
        {
            _Editable = false;
        }

        public Image TrueImage
        {
            get { return _TrueImage; }
            set { _TrueImage = value; }
        }

        public Image FalseImage
        {
            get { return _FalseImage; }
            set { _FalseImage = value; }
        }

        public bool Editable
        {
            get { return _Editable; }
            set { _Editable = value; }
        }

    }

    public abstract class EXListViewSubItemAB : ListViewItem.ListViewSubItem
    {

        private string _Value = "";

        public EXListViewSubItemAB()
        {

        }

        public EXListViewSubItemAB(string text)
        {
            this.Text = text;
        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        //return the new x coordinate
        public abstract int DoDraw(DrawListViewSubItemEventArgs e, int x, CRC.Controls.EXColumnHeader ch);

    }

    public class EXListViewSubItem : EXListViewSubItemAB
    {

        public EXListViewSubItem()
        {

        }

        public EXListViewSubItem(string text)
        {
            this.Text = text;
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, CRC.Controls.EXColumnHeader ch)
        {
            return x;
        }

    }

    public class EXControlListViewSubItem : EXListViewSubItemAB
    {

        private Control _Control;

        public EXControlListViewSubItem()
        {

        }

        public Control Control
        {
            get { return _Control; }
            set { _Control = value; }
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
        {
            return x;
        }

    }

    public class EXImageListViewSubItem : EXListViewSubItemAB
    {

        private Image _Image;

        public EXImageListViewSubItem()
        {

        }

        public EXImageListViewSubItem(string text)
        {
            this.Text = text;
        }

        public EXImageListViewSubItem(Image image)
        {
            _Image = image;
        }

        public EXImageListViewSubItem(Image image, string value)
        {
            _Image = image;
            this.Value = value;
        }

        public EXImageListViewSubItem(string text, Image image, string value)
        {
            this.Text = text;
            _Image = image;
            this.Value = value;
        }

        public Image MyImage
        {
            get { return _Image; }
            set { _Image = value; }
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, CRC.Controls.EXColumnHeader ch)
        {
            if (this.MyImage != null)
            {
                Image img = this.MyImage;
                int imgy = e.Bounds.Y + ((int)(e.Bounds.Height / 2)) - ((int)(img.Height / 2));
                e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                x += img.Width + 2;
            }
            return x;
        }

    }

    public class EXMultipleImagesListViewSubItem : EXListViewSubItemAB
    {

        private ArrayList _Images;

        public EXMultipleImagesListViewSubItem()
        {

        }

        public EXMultipleImagesListViewSubItem(string text)
        {
            this.Text = text;
        }

        public EXMultipleImagesListViewSubItem(ArrayList images)
        {
            _Images = images;
        }

        public EXMultipleImagesListViewSubItem(ArrayList images, string value)
        {
            _Images = images;
            this.Value = value;
        }

        public EXMultipleImagesListViewSubItem(string text, ArrayList images, string value)
        {
            this.Text = text;
            _Images = images;
            this.Value = value;
        }

        public ArrayList Images
        {
            get { return _Images; }
            set { _Images = value; }
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
        {
            if (this.Images != null && this.Images.Count > 0)
            {
                for (int i = 0; i < this.Images.Count; i++)
                {
                    Image img = (Image)this.Images[i];
                    int imgy = e.Bounds.Y + ((int)(e.Bounds.Height / 2)) - ((int)(img.Height / 2));
                    e.Graphics.DrawImage(img, x, imgy, img.Width, img.Height);
                    x += img.Width + 2;
                }
            }
            return x;
        }

    }

    public class EXBoolListViewSubItem : EXListViewSubItemAB
    {

        private bool _Value;

        public EXBoolListViewSubItem()
        {

        }

        public EXBoolListViewSubItem(bool val)
        {
            _Value = val;
            this.Value = val.ToString();
        }

        public bool BoolValue
        {
            get { return _Value; }
            set
            {
                _Value = value;
                this.Value = value.ToString();
            }
        }

        public override int DoDraw(DrawListViewSubItemEventArgs e, int x, EXColumnHeader ch)
        {
            EXBoolColumnHeader boolcol = (EXBoolColumnHeader)ch;
            Image boolimg;
            if (this.BoolValue == true)
            {
                boolimg = boolcol.TrueImage;
            }
            else
            {
                boolimg = boolcol.FalseImage;
            }
            int imgy = e.Bounds.Y + ((int)(e.Bounds.Height / 2)) - ((int)(boolimg.Height / 2));
            e.Graphics.DrawImage(boolimg, x, imgy, boolimg.Width, boolimg.Height);
            x += boolimg.Width + 2;
            return x;
        }

    }

    public class EXListViewItem : ListViewItem
    {

        private string _value;

        public EXListViewItem()
        {

        }

        public EXListViewItem(string text)
        {
            this.Text = text;
        }

        public string MyValue
        {
            get { return _value; }
            set { _value = value; }
        }

    }

    public class EXImageListViewItem : EXListViewItem
    {

        private Image _image;

        public EXImageListViewItem()
        {

        }

        public EXImageListViewItem(string text)
        {
            this.Text = text;
        }

        public EXImageListViewItem(Image image)
        {
            _image = image;
        }

        public EXImageListViewItem(string text, Image image)
        {
            _image = image;
            this.Text = text;
        }

        public EXImageListViewItem(string text, Image image, string value)
        {
            this.Text = text;
            _image = image;
            this.MyValue = value;
        }

        public Image MyImage
        {
            get { return _image; }
            set { _image = value; }
        }

    }

    public class EXMultipleImagesListViewItem : EXListViewItem
    {

        private ArrayList _Images;

        public EXMultipleImagesListViewItem()
        {

        }

        public EXMultipleImagesListViewItem(string text)
        {
            this.Text = text;
        }

        public EXMultipleImagesListViewItem(ArrayList images)
        {
            _Images = images;
        }

        public EXMultipleImagesListViewItem(string text, ArrayList images)
        {
            this.Text = text;
            _Images = images;
        }

        public EXMultipleImagesListViewItem(string text, ArrayList images, string value)
        {
            this.Text = text;
            _Images = images;
            this.MyValue = value;
        }

        public ArrayList Images
        {
            get { return _Images; }
            set { _Images = value; }
        }

    }

}