#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/22 9:43:48
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubItem = System.Windows.Forms.ListViewItem.ListViewSubItem;
namespace CRC.Controls
{
    /// <summary>
    /// 可编辑的ListView.
    /// <para>使用方法:当鼠标双击单元格时,就会引发BeginEditCell事件,请捕捉该事件.</para>
    /// 然后在该事件设置显示控件,然后添加到ListViewEdit的Controls属性中.才能显示该控件
    /// <example >
    /// <code >
    /// 在BeginEditCell事件中的部分代码.
    /// <![CDATA[ 
    /// 
    /// ColumnHeader header = listViewEdit1.Columns[e.SubItemIndex];
    /// if (header.Text == "结果")
    /// {
    ///     if (_CheckBox == null)
    ///     {
    ///         _CheckBox = new CheckBox();
    ///        
    ///         _CheckBox.Leave += new EventHandler(_CheckBox_Leave);
    ///         listViewEdit1.Controls.Add(_CheckBox);
    ///         //this.Controls.Add(_CheckBox);
    ///     }
    ///     _CheckBox.Size = e.SelectSubItem.Bounds.Size;
    ///     _CheckBox.Location = e.SelectSubItem.Bounds.Location;
    ///     _CheckBox.CheckAlign = ContentAlignment.MiddleCenter;
    ///     _CheckBox.Show();
    ///     if (e.SelectSubItem.Text == "OK")
    ///     {
    ///         _CheckBox.Checked = true;
    ///     }
    ///     else
    ///     {
    ///         _CheckBox.Checked = false;
    ///     }
    ///     _CheckBox.Focus();
    /// 
    /// }
    /// 
    /// ]]>
    /// </code>
    /// </example>
    /// </summary>
    public class EditListView : ListView
    {

         #region 字段与变量
        
        
        /// <summary>
        /// 当前选中的项.
        /// </summary>
        private ListViewItem _CurrentItem;
        /// <summary>
        /// 当前选中的子项的索引
        /// </summary>
        private int _SubItemIndex;

        /// <summary>
        /// 鼠标的X坐标
        /// </summary>
        private int _MouseX;
        /// <summary>
        /// 鼠标的Y坐标
        /// </summary>
        private int _MouseY;
        /// <summary>
        /// 当前选中的子项.
        /// </summary>
        SubItem _SelectSubItem;
        #endregion

        #region 事件与委托
        /// <summary>
        /// 双击单元格,开始编辑时发生.
        /// </summary>
        public event EventHandler<ListCellEditEventArgs> BeginEditCell;

        /// <summary>
        /// 引发BeginEditCell事件.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnBeginEditCell(ListCellEditEventArgs e)
        {
            if (BeginEditCell != null)
                BeginEditCell(this, e);
        }

        #endregion

        /// <summary>
        /// 可编辑的ListView.
        /// </summary>
        public EditListView()
        {
            FullRowSelect = true;
            View = System.Windows.Forms.View.Details;
        }

        #region 重载函数
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (_CurrentItem != null && _SelectSubItem != null)
            {
                ListCellEditEventArgs args = new ListCellEditEventArgs(
                    _CurrentItem, _SelectSubItem, _SubItemIndex, _MouseX, _MouseY);
                OnBeginEditCell(args);

            }
            base.OnMouseDoubleClick(e);
        }
      

        //
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            //获取当前被选中的项.
            _CurrentItem = this.GetItemAt(e.X, e.Y);
            if (_CurrentItem != null)
            {
                //获取当前被选中的子项.
                _SelectSubItem = _CurrentItem.GetSubItemAt(e.X, e.Y);
                //计算子项在项的索引.
                _SubItemIndex = _CurrentItem.SubItems.IndexOf(_SelectSubItem);
            }
            else
            {
                _SelectSubItem = null;
                _SubItemIndex = -1;
            }
            //记录鼠标的位置.
            _MouseX = e.X;
            _MouseY = e.Y;


        }
        #endregion
    }


    /// <summary>
    /// ListViewEidt类编辑时引发事件的数据.
    /// </summary>
    public class ListCellEditEventArgs : EventArgs
    {

        public ListCellEditEventArgs(ListViewItem item, SubItem subItem, int subIndex)
            : this(item, subItem, subIndex, subItem.Bounds.X, subItem.Bounds.Y)
        {

        }
        public ListCellEditEventArgs(ListViewItem item, SubItem subItem, int subIndex, int mouseX, int mouseY)
        {
            _SelectItem = item;
            _SelectSubItem = subItem;
            _SubItemIndex = subIndex;
            _MouseX = mouseX;
            _MouseY = mouseY;
        }
        private ListViewItem _SelectItem;
        /// <summary>
        /// 被选中的项
        /// </summary>
        public ListViewItem SelectItem
        {
            get { return _SelectItem; }
            set { _SelectItem = value; }
        }

        private SubItem _SelectSubItem;
        /// <summary>
        /// 被选中的子项.
        /// </summary>
        public SubItem SelectSubItem
        {
            get { return _SelectSubItem; }
            set { _SelectSubItem = value; }
        }

        private int _SubItemIndex;
        /// <summary>
        /// 被选中的子项的索引.
        /// </summary>
        public int SubItemIndex
        {
            get { return _SubItemIndex; }
            set { _SubItemIndex = value; }
        }

        private int _MouseX;
        /// <summary>
        /// 鼠标的X坐标
        /// </summary>
        public int MouseX
        {
            get { return _MouseX; }
            set { _MouseX = value; }
        }

        private int _MouseY;
        /// <summary>
        /// 鼠标的Y坐标
        /// </summary>
        public int MouseY
        {
            get { return _MouseY; }
            set { _MouseY = value; }
        }


    }
}
