using System;
using System.Collections.Generic;
using System.Text;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */
    /// <summary>
    /// 绘制的样式.
    /// </summary>
    public enum DrawStyle
    {
        /// <summary>
        /// 无.
        /// </summary>
        None = 0,
        /// <summary>
        /// 矩形.
        /// </summary>
        Rectangle,
        /// <summary>
        /// 椭圆
        /// </summary>
        Ellipse,
        /// <summary>
        /// 箭头
        /// </summary>
        Arrow,
        /// <summary>
        /// 文本
        /// </summary>
        Text,
        /// <summary>
        /// 线段
        /// </summary>
        Line
    }
}
