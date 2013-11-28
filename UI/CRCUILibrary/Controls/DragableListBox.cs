/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/18 9:40:55
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
using System.Reflection;
using System.Drawing;
using System.ComponentModel;
using System.Diagnostics;

namespace CRC.Controls
{
    //http://www.cnblogs.com/HopeGi/p/3300276.html

    /// <summary>
    /// 可拖放到ListBox
    /// </summary>
    public class DragableListBox:ListBox
    {
        #region 字段
        private bool _isDraw; //是否执行绘制
        SolidBrush _EvenRowBursh ;
        SolidBrush _OddRowBursh;
        Brush _NormalFontBursh=SystemBrushes.ControlText;
        Brush _SelectFontBursh = SystemBrushes.HighlightText;
        Brush _SelectRowBursh =  SystemBrushes.Highlight;
        private bool _DragAcross;
        private bool _DragSort;

        public static ListBox _DragSource;
        #endregion

        public DragableListBox()
        {
            this.DoubleBuffered = true;
            this.OddColor = this.BackColor;
        }

        #region 外放成员

        #region 属性

        /// <summary>
        /// 获取或设置是否允许在不同的ListBox之间拖放元素.
        /// </summary>
        [Description("跨ListBox拖放元素"), Category("行为")]
        public bool DragAcross 
        {
            get { return (_DragAcross && AllowDrop && SelectionMode== System.Windows.Forms.SelectionMode.One); }
            set 
            {
                _DragAcross = value;
                if (value) this.AllowDrop = true;
            }
        }

        /// <summary>
        /// 获取或设置是否允许拖动排序.
        /// </summary>
        [Description("元素拖动排序"),Category("行为")]
        public bool DragSort
        {
            get { return _DragSort && AllowDrop && SelectionMode == System.Windows.Forms.SelectionMode.One; }
            set
            {
                _DragSort = value;
                if (value) this.AllowDrop = true;
            }
        }

        private Color oddColor=Color.RoyalBlue ;
        [Description("单数行的底色"), Category("外观")]
        public Color OddColor
        {
            get { return oddColor; }
            set 
            {
                oddColor = value;
                _isDraw = oddColor != this.BackColor;
                if (_isDraw) DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
                else DrawMode = System.Windows.Forms.DrawMode.Normal;
            }
        }

        #endregion

        #region 事件

        [Description("跨ListBox拖拽完成后触发"),Category("行为")]
        public event DraggdHandler DraggedAcross;

        [Description("拖拽排序后触发"), Category("行为")]
        public event DraggdHandler DraggedSort;

        #endregion

        #endregion

        #region 重写方法

        #region 拖拽

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
            if (!DragAcross && !DragSort) return;

            object item = _DragSource.SelectedItem;
            Debug.WriteLine("OnDragDrop {0}  {1}".FormatWith(Name,item));

            if (DragAcross && !DragSort && _DragSource != this)
            {
                _DragSource.Items.Remove(item);
                this.Items.Add(item);
                if (DraggedAcross != null)
                    DraggedAcross(this, new DraggedEventArgs() { DragItem=item, SourceControl=_DragSource });
            }
            else if (DragSort &&(( _DragSource == this&&! DragAcross)||DragAcross))
            {
                int index = this.IndexFromPoint(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
                _DragSource.Items.Remove(item);
                if (index < 0)
                    this.Items.Add(item);
                else
                    this.Items.Insert(index, item);
                if (DragAcross && DraggedAcross != null)
                    DraggedAcross(this, new DraggedEventArgs() { DragItem=item,SourceControl=_DragSource });
                if (DraggedSort != null)
                    DraggedSort(this, new DraggedEventArgs() { DragItem=item,SourceControl=_DragSource, DestineIndex=index });
            }
            
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if (!DragAcross&&!DragSort) return;

            //dragDestince=this;
            drgevent.Effect = DragDropEffects.Move;
        }

        protected override void  OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            if (!DragAcross && !DragSort ) return;

            if (this.Items.Count == 0 || e.Button != MouseButtons.Left || this.SelectedIndex == -1 || e.Clicks == 2)
                return;
            _DragSource = this;
            
            int index = this.SelectedIndex;
            object item = this.Items[index];
            DragDropEffects dde = DoDragDrop(item,
                DragDropEffects.All);
        }

        #endregion 

        #region 绘制

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.Items.Count < 0) return;
            if (!_isDraw) return;
            if (e.Index < 0) return;
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            if(selected)
            //if (e.Index == this.SelectedIndex)
                e.Graphics.FillRectangle(_SelectRowBursh, e.Bounds);
            else if (e.Index % 2 != 0)
                e.Graphics.FillRectangle(OddRowBursh, e.Bounds);
            else
                e.Graphics.FillRectangle(EvenRowBursh, e.Bounds);

            //if (e.Index == this.SelectedIndex )
            if(selected)
            {
                e.Graphics.DrawString(this.GetItemText(e.Index), e.Font,
                            _SelectFontBursh, e.Bounds);
                
            }
            else
            {
                e.Graphics.DrawString(this.GetItemText(e.Index), e.Font,
                            _NormalFontBursh, e.Bounds);
            }
            e.DrawFocusRectangle();
            base.OnDrawItem(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (_OddRowBursh != null) _OddRowBursh.Dispose();
            if (_EvenRowBursh != null) _EvenRowBursh.Dispose();
        }

        #endregion

        #endregion

        #region 私有方法和属性

        private SolidBrush EvenRowBursh
        {
            get 
            {
                if(_EvenRowBursh==null)
                {
                    _EvenRowBursh = new SolidBrush(this.BackColor);
                    return _EvenRowBursh;
                }
                if ( _EvenRowBursh.Color == this.BackColor)return _EvenRowBursh;
                _EvenRowBursh.Dispose();
                _EvenRowBursh = new SolidBrush(this.BackColor);
                return _EvenRowBursh;
            }
            set 
            {
                if(_EvenRowBursh!=null) _EvenRowBursh.Dispose();
                _EvenRowBursh = value;
            }
        }

        private SolidBrush OddRowBursh
        {
            get 
            {
                if (_OddRowBursh == null)
                {
                    _OddRowBursh = new SolidBrush(this.OddColor);
                    return _OddRowBursh;
                }
                if (_OddRowBursh.Color == this.OddColor) return _OddRowBursh;
                _OddRowBursh.Dispose();
                _OddRowBursh = new SolidBrush(this.OddColor);
                return _OddRowBursh;
            }
            set 
            {
                if (_OddRowBursh != null) _OddRowBursh.Dispose();
                _OddRowBursh = value;
            }
        }

        private string GetItemText(int index)
        {
            try
            {
                object item = this.Items[index];
                if (string.IsNullOrEmpty(this.DisplayMember) || string.IsNullOrWhiteSpace(this.DisplayMember))
                    return item.ToString();
                PropertyInfo proInfo = item.GetType().GetProperty(this.DisplayMember);
                return proInfo.GetValue(item, null).ToString();
            }
            catch { return this.Name; }
        }

        #endregion

        public class DraggedEventArgs:EventArgs
        {
            public ListBox SourceControl { get; set; }

            public object DragItem { get; set; }

            public int DestineIndex { get; set; }

            public DraggedEventArgs()
            {
                DestineIndex = -1;
            }
        }

        public delegate void DraggdHandler(object sender, DraggedEventArgs e);
    }


}
