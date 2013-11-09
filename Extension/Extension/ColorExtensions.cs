/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/1 17:41:20
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace CRC.Extension
{
    public static class ColorExtensions
    {
        /// <summary>
        /// <para>将颜色转换为16进制的格式.例如:Red(红色)转换为FF0000</para>
        /// Convert the color to hex format
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static string ToHexFormat(this Color c)
        {
            string f = String.Format("{0:X2}", Convert.ToInt32(c.R));
            f += String.Format("{0:X2}", Convert.ToInt32(c.G));
            f += String.Format("{0:X2}", Convert.ToInt32(c.B));

            return f;
        }
    }
}
