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

namespace CRC.Controls
{
    //http://www.cnblogs.com/HopeGi/p/3300276.html

    /// <summary>
    /// 可拖放到ListBox
    /// </summary>
    public class DragableListBox:ListBox
    {
        #region 字段
        private bool isDraw; //是否执行绘制
        SolidBrush evenRowBursh ;
        SolidBrush oddRowBursh;
        Brush normalFontBursh=SystemBrushes.ControlText;
        Brush selectFontBursh = SystemBrushes.HighlightText;
        Brush selectRowBursh =  SystemBrushes.Highlight;
        private bool dragAcross;
        private bool dragSort;

        public static ListBox dragSource;
        #endregion

        public DragableListBox()
        {
            this.DoubleBuffered = true;
            this.OddColor = this.BackColor;
        }

        #region 外放成员

        #region 属性

        [Description("跨ListBox拖放元素"), Category("行为")]
        public bool DragAcross 
        {
            get { return (dragAcross&&AllowDrop&&SelectionMode== System.Windows.Forms.SelectionMode.One); }
            set 
            {
                dragAcross = value;
                if (value) this.AllowDrop = true;
            }
        }

        [Description("元素拖动排序"),Category("行为")]
        public bool DragSort
        {
            get { return dragSort && AllowDrop && SelectionMode == System.Windows.Forms.SelectionMode.One; }
            set
            {
                dragSort = value;
                if (value) this.AllowDrop = true;
            }
        }

        private Color oddColor;
        [Description("单数行的底色"), Category("外观")]
        public Color OddColor
        {
            get { return oddColor; }
            set 
            {
                oddColor = value;
                isDraw = oddColor != this.BackColor;
                if (isDraw) DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
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

            object item = dragSource.SelectedItem;

            if (DragAcross && !DragSort && dragSource != this)
            {
                dragSource.Items.Remove(item);
                this.Items.Add(item);
                if (DraggedAcross != null)
                    DraggedAcross(this, new DraggedEventArgs() { DragItem=item, SourceControl=dragSource });
            }
            else if (DragSort &&(( dragSource == this&&! DragAcross)||DragAcross))
            {
                int index = this.IndexFromPoint(this.PointToClient(new Point(drgevent.X, drgevent.Y)));
                dragSource.Items.Remove(item);
                if (index < 0)
                    this.Items.Add(item);
                else
                    this.Items.Insert(index, item);
                if (DragAcross && DraggedAcross != null)
                    DraggedAcross(this, new DraggedEventArgs() { DragItem=item,SourceControl=dragSource });
                if (DraggedSort != null)
                    DraggedSort(this, new DraggedEventArgs() { DragItem=item,SourceControl=dragSource, DestineIndex=index });
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
            dragSource = this;
            
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
            if (!isDraw) return;
            if (e.Index < 0) return;
            bool selected = (e.State & DrawItemState.Selected) == DrawItemState.Selected;
            if(selected)
            //if (e.Index == this.SelectedIndex)
                e.Graphics.FillRectangle(selectRowBursh, e.Bounds);
            else if (e.Index % 2 != 0)
                e.Graphics.FillRectangle(OddRowBursh, e.Bounds);
            else
                e.Graphics.FillRectangle(EvenRowBursh, e.Bounds);

            //if (e.Index == this.SelectedIndex )
            if(selected)
            {
                e.Graphics.DrawString(this.GetItemText(e.Index), e.Font,
                            selectFontBursh, e.Bounds);
                
            }
            else
            {
                e.Graphics.DrawString(this.GetItemText(e.Index), e.Font,
                            normalFontBursh, e.Bounds);
            }
            e.DrawFocusRectangle();
            base.OnDrawItem(e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (oddRowBursh != null) oddRowBursh.Dispose();
            if (evenRowBursh != null) evenRowBursh.Dispose();
        }

        #endregion

        #endregion

        #region 私有方法和属性

        private SolidBrush EvenRowBursh
        {
            get 
            {
                if(evenRowBursh==null)
                {
                    evenRowBursh = new SolidBrush(this.BackColor);
                    return evenRowBursh;
                }
                if ( evenRowBursh.Color == this.BackColor)return evenRowBursh;
                evenRowBursh.Dispose();
                evenRowBursh = new SolidBrush(this.BackColor);
                return evenRowBursh;
            }
            set 
            {
                if(evenRowBursh!=null) evenRowBursh.Dispose();
                evenRowBursh = value;
            }
        }

        private SolidBrush OddRowBursh
        {
            get 
            {
                if (oddRowBursh == null)
                {
                    oddRowBursh = new SolidBrush(this.OddColor);
                    return oddRowBursh;
                }
                if (oddRowBursh.Color == this.OddColor) return oddRowBursh;
                oddRowBursh.Dispose();
                oddRowBursh = new SolidBrush(this.OddColor);
                return oddRowBursh;
            }
            set 
            {
                if (oddRowBursh != null) oddRowBursh.Dispose();
                oddRowBursh = value;
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
