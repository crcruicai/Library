using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */


    /// <summary>
    /// 插值渲染模式.通过Dispose()函数可恢复上次渲染模式.
    /// </summary>
    public class InterpolationModeGraphics : IDisposable
    {
        private InterpolationMode _oldMode;
        private Graphics _graphics;
        /// <summary>
        /// 构造插值渲染模式,默认为高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
        /// </summary>
        /// <param name="graphics"></param>
        public InterpolationModeGraphics(Graphics graphics)
            : this(graphics, InterpolationMode.HighQualityBicubic)
        {
        }
        /// <summary>
        /// 构造插值渲染模式
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="newMode"> System.Drawing.Drawing2D.InterpolationMode 枚举指定在缩放或旋转图像时使用的算法。</param>
        public InterpolationModeGraphics(
            Graphics graphics, InterpolationMode newMode)
        {
            _graphics = graphics;
            _oldMode = graphics.InterpolationMode;
            graphics.InterpolationMode = newMode;
        }

        #region IDisposable 成员
        /// <summary>
        /// 恢复上次渲染模式.
        /// </summary>
        public void Dispose()
        {
            _graphics.InterpolationMode = _oldMode;
        }

        #endregion
    }


}
