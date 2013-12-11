using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using CWebQQ.Data;


namespace CRC.Controls
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ChatMessage
    {
        #region 属性.
        
        
        private string  _Title;
        /// <summary>
        /// 标题
        /// </summary>
        public string  Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        private string  _Text;
        /// <summary>
        /// 文本.
        /// </summary>
        public string  Text
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
        private string _Uin;
        /// <summary>
        /// 提示文本.
        /// </summary>
        public string Uin
        {
            get
            {
                return _Uin ;
            }
            set
            {
                _Uin = value;
            }
        }

        private Friend _Friend;
        /// <summary>
        /// 
        /// </summary>
        public Friend Friend
        {
            get { return _Friend; }
            set { _Friend = value; }
        }
        


        #endregion

        #region 构造函数

        public ChatMessage()
        {
            _Title = "";
            _Text = "";
            _Uin = "";
        }

        public ChatMessage(string title, string text, string uin)
        {
            _Title = title;
            _Text = text;
            _Uin = uin;
        }

        #endregion

        #region 公有函数

        public void AppendTextOfEnd(string text)
        {
            _Text = string.Format("{0}\r\n{1}", _Text, text);
        }

        public void AppendTextOfStart(string text)
        {
            _Text = string.Format("{1}\r\n{0}", _Text, text);
        }

        #endregion


        #region 私有函数

       


        #endregion


        #region 重载函数.
        public override bool Equals(object obj)
        {
            ChatMessage chat = obj as ChatMessage;
            if (chat != null)
                return _Title.Equals(chat._Title) && _Uin.Equals (chat._Uin);
            else
            { 
                return base.Equals(obj);
            }
           
        }

        public override int GetHashCode()
        {
            return _Title.GetHashCode();
        }
        #endregion

    }


    /// <summary>
    /// 支持多行文本显示的ListBox.
    /// <para>但不支持编辑.</para>
    /// <para>如果支持编辑,请使用MultiLineListBox.</para>
    /// </summary>
    [ToolboxBitmap(typeof (ListBox))]
    public class ChatTextListBox:ListBox 
    {
        private int padWidth = 20;
        private int padHeigth = 10;

        public ChatTextListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
        }



        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
           
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                //设置文本绘制的方式.
                StringFormat sf = new StringFormat();
                sf.LineAlignment = StringAlignment.Near;
                sf.FormatFlags = StringFormatFlags.FitBlackBox;

                ChatMessage cm = Items[e.Index] as ChatMessage;
                if (cm != null)
                {

                    SizeF size = e.Graphics.MeasureString(cm.Text, Font, Width - padWidth, sf);
                    //重新调整文本的子项的区域大小.
                    int h = e.Index == 0 ? padHeigth * 2 + 5 : padHeigth * 2;
                    e.ItemHeight = (int)size.Height + h +Font .Height +5;
                    e.ItemWidth = Width;
                }
                else
                {
                    // 测量文本的区域.
                    string text = Items[e.Index].ToString();
                    SizeF size = e.Graphics.MeasureString(text, Font, Width - padWidth, sf);
                    //重新调整文本的子项的区域大小.
                    int h = e.Index == 0 ? padHeigth * 2 + 5 : padHeigth * 2;
                    e.ItemHeight = (int)size.Height + h;
                    e.ItemWidth = Width;
                   
                }

              

            }
            //base.OnMeasureItem(e);
        }

        /// <summary>
        /// 绘制子项.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                Graphics g = e.Graphics;
                Rectangle rect =e.Bounds;
                string text = string.Empty ;
                string title = string.Empty;
                ChatMessage cm = Items[e.Index] as ChatMessage;
                if (cm != null)
                {
                    if ((e.State & DrawItemState.Focus) == 0)
                    {
                        //子项处于焦点状态 
                        g.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);
                        g.DrawRectangle(new Pen(SystemColors.Highlight), e.Bounds);
                        g.DrawString(cm.Title, Font, new SolidBrush(SystemColors.WindowText),new Rectangle (rect.X+2 ,rect.Y +3,rect .Width -2,Font .Height));
                        g.DrawString(cm.Text, Font, new SolidBrush(SystemColors.WindowText), GetTextBounds (rect));                       
                    }
                    else
                    {
                        //子项处于非焦点状态.
                        g.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                        g.DrawString(cm.Title, Font, new SolidBrush(SystemColors.HighlightText), new Rectangle(rect.X + 2, rect.Y + 3, rect.Width - 2, Font.Height));
                        g.DrawString(cm.Text, Font, new SolidBrush(SystemColors.HighlightText), GetTextBounds(rect));  
                    }
                }
                else
                {
                    rect = GetBounds(rect);
                    text = Items[e.Index].ToString();
                    if ((e.State & DrawItemState.Focus) == 0)
                    {
                        //子项处于焦点状态 
                        g.FillRectangle(new SolidBrush(SystemColors.Window), e.Bounds);

                        g.DrawString(text, Font, new SolidBrush(SystemColors.WindowText), rect);
                        g.DrawRectangle(new Pen(SystemColors.Highlight), e.Bounds);
                    }
                    else
                    {
                        //子项处于非焦点状态.
                        g.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                        g.DrawString(text, Font, new SolidBrush(SystemColors.HighlightText), rect);
                    }
                }
                
            }
            
            //base.OnDrawItem(e);
        }

        /// <summary>
        /// 计算绘制文本的区域.
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        private Rectangle GetBounds(Rectangle rect)
        {
           
            rect.X += padWidth / 2;
            rect.Width -= padWidth/2;
            rect.Y += padHeigth / 2;
            rect.Height -= padHeigth/2;
            return rect;
        }

        private Rectangle GetTextBounds(Rectangle rect)
        {
            rect.X += padWidth / 2;
            rect.Width -= padWidth / 2;
            rect.Y += padHeigth / 2+3+Font.Height;
            rect.Height -= padHeigth / 2 + 3 + Font.Height;
            return rect;
        }

    }
}
