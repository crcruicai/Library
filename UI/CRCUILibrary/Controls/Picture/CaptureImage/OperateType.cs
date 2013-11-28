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
    /// 操作类型.
    /// </summary>
    internal enum OperateType
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// 绘制矩形
        /// </summary>
        DrawRectangle,
        /// <summary>
        /// 绘制椭圆.
        /// </summary>
        DrawEllipse,
        /// <summary>
        /// 绘制箭头.
        /// </summary>
        DrawArrow,
        /// <summary>
        /// 绘制线段
        /// </summary>
        DrawLine,
        /// <summary>
        /// 绘制文本.
        /// </summary>
        DrawText
    }
}
