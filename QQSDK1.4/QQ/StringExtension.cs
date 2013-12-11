#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/20 8:46:24
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWebQQ
{
    public static class StringExtension
    {
        /// <summary>
        /// 将指定字符串中的格式项替换为指定数组中相应对象的字符串表示形式。
        /// </summary>
        /// <param name="obj">复合格式字符串。</param>
        /// <param name="para">一个对象数组，其中包含零个或多个要设置格式的对象。</param>
        /// <returns>format 的副本，其中的格式项已替换为 args 中相应对象的字符串表示形式。</returns>
        public static string FormatWith(this string obj, params object[] para)
        {
            return string.Format(obj, para);
        }
        /// <summary>
        /// 将指定字符串中的一个或多个格式项替换为指定对象的字符串表示形式。
        /// </summary>
        /// <param name="obj">复合格式字符串。</param>
        /// <param name="para">要设置格式的对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string obj, object para)
        {
            return string.Format(obj, para);
        }
        /// <summary>
        /// 将指定字符串中的格式项替换为两个指定对象的字符串表示形式。
        /// </summary>
        /// <param name="obj">复合格式字符串。</param>
        /// <param name="para1">要设置格式的第一个对象。</param>
        /// <param name="para2">要设置格式的第二个对象。</param>
        /// <returns></returns>
        public static string FormatWith(this string obj,string para1,string para2)
        {
            return string.Format(obj, para1, para2);
        }
        /// <summary>
        /// 将指定字符串中的格式项替换为三个指定对象的字符串表示形式。
        /// </summary>
        /// <param name="obj">复合格式字符串。</param>
        /// <param name="para1">要设置格式的第一个对象。</param>
        /// <param name="para2">要设置格式的第二个对象。</param>
        /// <param name="para3">要设置格式的第三个对象</param>
        /// <returns></returns>
        public static string FormatWith(this string obj, object para1, object para2,object para3)
        {
            return string.Format(obj, para1, para2,para3);
        }
    }
}
