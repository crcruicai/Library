using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Text;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-10-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    /// 文本渲染提示的Graphics,可以使用Dispose()方法恢复上一次的渲染的质量类型.
    /// </summary>
    internal class TextRenderingHintGraphics : IDisposable
    {
        private Graphics _graphics;
        private TextRenderingHint _oldTextRenderingHint;

        /// <summary>
        /// 构建文本渲染提示的Graphics
        /// </summary>
        /// <param name="graphics"></param>
        public TextRenderingHintGraphics(Graphics graphics)
            : this(graphics, TextRenderingHint.AntiAlias)
        {
        }
        /// <summary>
        /// 构建文本渲染提示的Graphics
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="newTextRenderingHint">指定文本渲染质量.</param>
        public TextRenderingHintGraphics(
            Graphics graphics,
            TextRenderingHint newTextRenderingHint)
        {
            _graphics = graphics;
            _oldTextRenderingHint = graphics.TextRenderingHint;
            _graphics.TextRenderingHint = newTextRenderingHint;
        }

        #region IDisposable 成员

        /// <summary>
        /// 恢复上次渲染质量类型.
        /// </summary>
        public void Dispose()
        {
            _graphics.TextRenderingHint = _oldTextRenderingHint;
        }

        #endregion
    }
}
