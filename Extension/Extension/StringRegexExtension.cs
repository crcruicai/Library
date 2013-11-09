/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/24 11:18:27
 * 描述说明： String 类与正则表达结合的扩展方法.(没有测试)
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CRC.Extension
{
    /// <summary>
    /// String 类与正则表达结合的扩展方法.
    /// </summary>
    public static class StringRegexExtension
    {
        /// <summary>
        /// 指示所指定的正则表达式在指定的输入字符串中是否找到了匹配项。
        /// </summary>
        /// <param name="s">要搜索匹配项的字符串。</param>
        /// <param name="pattern">要匹配的正则表达式模式。</param>
        /// <returns> 如果正则表达式找到匹配项，则为 true；否则，为 false。</returns>
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }


        /// <summary>
        /// 在指定的输入字符串中搜索指定的正则表达式的第一个匹配项。
        /// </summary>
        /// <param name="s">要搜索匹配项的字符串。</param>
        /// <param name="pattern">要匹配的正则表达式模式。</param>
        /// <returns>一个对象，包含有关匹配项的信息。</returns>
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }


        #region 字符检查


        /// <summary>
        /// 检查字符串是否全部为数字.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumber(this string s)
        {
            //^\\d+$
            if (s == null) return false;
            return Regex.IsMatch(s, @"^\\d+$");
        }

        /// <summary>
        /// 检查字符串是否为网址形式.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUrl(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"http://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        }

        /// <summary>
        /// 检查字符串是否为Email地址形式.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmail(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        /// <summary>
        /// 检查字符串是否为HTML标记形式.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsHTML(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

        /// <summary>
        /// 检查字符串是否为中文字符.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsChineseChar(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 检查字符串是否为双字节的字符.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUnicode(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"[^\x00-\xff]");
        }

        /// <summary>
        /// 检查字符串是否为电话号码的形式.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPhoneNumber(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"(d+-)?(d{4}-?d{7}|d{3}-?d{8}|^d{7,8})(-d+)?");
        }

        /// <summary>
        /// 检查字符串是否为IP地址的形式.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsIPAddress(this string s)
        {
            if (s == null) return false;
            return Regex.IsMatch(s, @"^(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5]).(d{1,2}|1dd|2[0-4]d|25[0-5])$");
        }

        #endregion

        #region 字符提取



        /// <summary>
        /// 从网页字符串中提取网页的标题.
        /// <para>成功返回标题,不成功,返回string.Empty</para>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetPageTitle(this string s)
        {
            if (s == null) return string.Empty;
            Regex reg = new Regex(@"(?m)<title[^>]*>(?<title>(?:\w|\W)*?)</title[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Match match = reg.Match(s);
            if (match.Success)
            {
                return match.Groups["title"].Value.Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetScript(this string s)
        {
            if (s == null) return string.Empty;
            Regex reg = new Regex(@"(?m)<script[^>]*>(\w|\W)*?</script[^>]*>", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            Match match = reg.Match(s);
            return match.Value;
        }


        #endregion
    }
}
