/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/5 17:21:01
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Controls
{

    /// <summary>
    /// string.Format 扩展方法.
    /// </summary>
    static class StringFomatExtension
    {

        /// <summary>
        /// 将指定字符串中的格式项替换为两个指定对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象。</param>
        /// <param name="arg1">要设置格式的第二个对象。</param>
        /// <returns>format 的副本，其中的格式项替换为 arg0 和 arg1 的字符串表示形式。</returns>
        public static string FormatWith(this string format, object arg0, object arg1)
        {
            return string.Format(format, arg0, arg1);
        }

        // 异常:
        //   System.ArgumentNullException:
        //     format 为 null。
        //
        //   System.FormatException:
        //     format 无效。- 或 -格式项的索引小于零或大于二。
        /// <summary>
        /// 将指定字符串中的格式项替换为三个指定对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0">要设置格式的第一个对象。</param>
        /// <param name="arg1">要设置格式的第二个对象。</param>
        /// <param name="arg2">要设置格式的第三个对象。</param>
        /// <returns>format 的副本，其中的格式项已替换为 arg0、arg1 和 arg2 的字符串表示形式。</returns>
        public static string FormatWith(this string format, object arg0, object arg1, object arg2)
        {
            return string.Format(format, arg0, arg1, arg2);
        }


        // 异常:
        //   System.ArgumentNullException:
        //     format 为 null。
        //
        //   System.FormatException:
        //     format 中的格式项无效。- 或 -格式项的索引大于或小于零。
        /// <summary>
        /// 将指定字符串中的一个或多个格式项替换为指定对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="arg0"> 要设置格式的对象。</param>
        /// <returns> format 的副本，其中的任何格式项均替换为 arg0 的字符串表示形式。</returns>
        public static string FormatWith(this string format, object arg0)
        {
            return string.Format(format, arg0);
        }

        //   System.ArgumentNullException:
        //     format 或 args 为 null。
        //
        //   System.FormatException:
        //     format 无效。- 或 -格式项的索引小于零或大于等于 args 数组的长度。
        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="format">复合格式字符串。</param>
        /// <param name="args"> 一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns>format 的副本，其中的格式项已替换为 args 中相应对象的字符串表示形式。</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
    }
}
