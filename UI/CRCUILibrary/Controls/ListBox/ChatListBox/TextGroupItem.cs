using System;
using System.Collections.Generic;
using System.Text;

using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace CRC.Controls
{
    /// <summary>
    /// 表示一个文本组.
    /// <para>用于GroupListBox中.</para>
    /// </summary>
    public class TextGroupItem
    {
        #region 字段与变量

        #endregion

        #region 构造函数

        #endregion

        #region 属性

        private string _Text="Item";
        /// <summary>
        /// 组的标题.
        /// </summary>
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
                    if(OwnerGroupListBox !=null)
                        OwnerGroupListBox.Invalidate(Bounds);
                }
            }
        }

        private bool  _IsOpen;
        /// <summary>
        /// 是否已经展开.
        /// </summary>
        [DefaultValue(false)]
        public bool  IsOpen
        {
            get
            {
                return _IsOpen;
            }
            set
            {
                if (_IsOpen != value)
                {
                    _IsOpen = value;
                    if (OwnerGroupListBox != null)
                        OwnerGroupListBox.Invalidate();
                    //TODO:引发一个打开或关闭的事件.
                }

              
            }
        }


        private Rectangle  _Bounds;
        /// <summary>
        /// 获取列表项的显示区域
        /// </summary>
        [Browsable(false)]
        public Rectangle Bounds
        {
            get
            {
                return _Bounds;
            }
            internal set
            {
                _Bounds = value;
            }
        }

        private GroupListBox  _OwnerGroupListBox;
        /// <summary>
        /// 获取列表项所指的控件.
        /// </summary>
        [Browsable(false)]
        public GroupListBox OwnerGroupListBox
        {
            get
            {
                return _OwnerGroupListBox;
            }
            internal set
            {
                _OwnerGroupListBox = value;
            }
        }

        private TextSubItemCollection  _SubItems;
        /// <summary>
        /// 获取当前列表的所有子项的集合.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextSubItemCollection  SubItems
        {
            get
            {
                if (_SubItems == null) _SubItems = new TextSubItemCollection();
                return _SubItems;
            }
            
        }


        #endregion

        #region 公共函数

        public TextGroupItem()
        {
        }


        public TextGroupItem(string text) : this(text, false, null) { }
        public TextGroupItem(string text, bool isOpen) : this(text, isOpen, null) { }

        public TextGroupItem(string text,bool isOpen,TextSubItemCollection coll)
        {
            this.Text = text;
            _IsOpen = isOpen;
            //TODO:完成集合后,要这里恢复.
            if(coll !=null)
            {
                //_SubItems.AddRange(coll);
            }
        }


        #endregion

        #region 私有函数

        #endregion


        #region 自定义集合
        /// <summary>
        /// 
        /// </summary>
        public class TextSubItemCollection:List<TextSubItem>
        {
           
        }

        #endregion


    }
}
