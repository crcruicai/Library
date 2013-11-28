using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    /// 绘制文本的数据.
    /// </summary>
    internal class DrawTextData
    {
        private string _Text;
        private Font _Font;
        private Rectangle _TextRect;
        private bool _Completed;

        public DrawTextData() { }

        public DrawTextData(string text, Font font, Rectangle textRect) 
        {
            _Text = text;
            _Font = font;
            _TextRect = textRect;
        }
        /// <summary>
        /// 获取或设置文本.
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        /// <summary>
        /// 获取或设置文本的字体.
        /// </summary>
        public Font Font
        {
            get { return _Font; }
            set { _Font = value; }
        }
        /// <summary>
        /// 获取或设置文本绘制的区域.
        /// </summary>
        public Rectangle TextRect
        {
            get { return _TextRect; }
            set { _TextRect = value; }
        }

        /// <summary>
        /// 获取或设置是否已经完成.
        /// </summary>
        public bool Completed
        {
            get { return _Completed; }
            set { _Completed = value; }
        }
    }
}
