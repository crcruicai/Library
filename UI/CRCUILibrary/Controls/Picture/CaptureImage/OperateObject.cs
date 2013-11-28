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
    /// 操作命令的对象.
    /// </summary>
    internal class OperateObject
    {
        private OperateType _OperateType;
        private Color _Color;
        private object _Data;

        public OperateObject() { }

        public OperateObject(OperateType operateType, Color color, object data)
        {
            _OperateType = operateType;
            _Color = color;
            _Data = data;
        }

        /// <summary>
        /// 操作命令类型
        /// </summary>
        public OperateType OperateType
        {
            get { return _OperateType; }
            set { _OperateType = value; }
        }

        /// <summary>
        /// 颜色.
        /// </summary>
        public Color Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        /// <summary>
        /// 数据.
        /// </summary>
        public object Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
}
