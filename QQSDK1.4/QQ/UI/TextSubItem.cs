using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CRC.Controls
{
    public class TextSubItem : IComparable
    {

        #region 字段与变量

        #endregion

        #region 构造函数
        public TextSubItem()
        {
            this.Text = "TextSubItem";
        }

        public TextSubItem(string text)
        {
            this.Text = text;
        }

        public TextSubItem(string text, object tag)
        {
            this.Text = text;
            Tag = tag;
        }

        #endregion

        #region 属性

        private string _Text;
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }

        private object  _Tag;
        /// <summary>
        /// 
        /// </summary>
        public object  Tag
        {
            get
            {
                return _Tag;
            }
            set
            {
                _Tag = value;
            }
        }


        private Rectangle _Bounds;
        /// <summary>
        /// 
        /// </summary>
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

        


        #endregion

        #region 公共函数

        #endregion

        #region 私有函数

        #endregion







        #region IComparable 成员

        public int CompareTo(object obj)
        {
            TextSubItem item = obj as TextSubItem;
            if (item == null) throw new NotImplementedException("");
            return this.Text.CompareTo(item.Text);
            
        }

        #endregion
    }
}
