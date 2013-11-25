using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */


    /// <summary>
    /// 平滑渲染模式,通过Dispose()函数可恢复上次渲染模式.
    /// </summary>
    public class SmoothingModeGraphics : IDisposable
    {
        private SmoothingMode _oldMode;
        private Graphics _graphics;
        /// <summary>
        /// 构建平滑渲染模式,渲染模式为消除锯齿.
        /// </summary>
        /// <param name="graphics"></param>
        public SmoothingModeGraphics(Graphics graphics)
            : this(graphics, SmoothingMode.AntiAlias)
        {
        }
        /// <summary>
        /// 构建平滑渲染模式
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="newMode">新的渲染模式.</param>
        public SmoothingModeGraphics(Graphics graphics, SmoothingMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.SmoothingMode;
            graphics.SmoothingMode = newMode;
        }

        #region IDisposable 成员

        public void Dispose()
        {
            _graphics.SmoothingMode = _oldMode;
        }

        #endregion
    }
}
