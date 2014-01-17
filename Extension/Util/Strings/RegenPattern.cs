/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 9:43:45
 * 描述说明：常用正则表达式类
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CRC.Util
{
    /// <summary>
    /// 封装常用的正则表达式.
    /// </summary>
    public static class RegenPattern
    {
        #region 网页相关


        /// <summary>
        /// 网址的正则表达式
        /// </summary>
        public static readonly string Url = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$";

        /// <summary>
        /// email 正则表达式
        /// </summary>
        public static readonly String Email = @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$";

        /// <summary>
        /// html 页面中图片的正则表达式，获取&lt;img src="" /&gt; 的src部分
        /// </summary>
        public static readonly String Img = "(?<=<img.+?src\\s*?=\\s*?\"?)([^\\s\"]+?)(?=[\\s\"])";



        #endregion

        #region 货币类
        /// <summary>
        /// 货币值(小数)的正则表达式
        /// </summary>
        public static readonly String Currency = @"^\d+(\.\d\d)?$"; //1.00

        /// <summary>
        /// (负数)货币值(小数)的正则表达式
        /// </summary>
        public static readonly String NegativeCurrency = @"^(-)?\d+(\.\d\d)?$"; //-1.20

        #endregion

        #region IP类

        /// <summary>
        /// IPv4 地址正则表达式.形式如:192.168.0.1
        /// </summary>
        public static readonly string IPAddress = @"((25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)\.){3}(25[0-5]|2[0-4]\d|1\d\d|[1-9]\d|\d)";

        #endregion

        #region 数字类.
        /// <summary>
        /// 纯数字的正则表达式.
        /// </summary>
        public static readonly string Number = "^[0-9]*$";

        /// <summary>
        /// 身份证(15位或18位)正则表达式.
        /// </summary>
        public static readonly string IdentityCard = @"\d{18}|\d{15}";
        /// <summary>
        /// 电话号码,兼容固话和手机,支持区号,"-"和分机.
        /// </summary>
        public static readonly string PhoneNumber = @"(\(?\d{3,4}\)?)?[\s-]?\d{7,8}[\s-]?\d{0,4}";

        /// <summary>
        /// 年龄,区间为 1-129.
        /// </summary>
        public static readonly string Age = @"^1[0-2]\d{1}$|^\d{2}$|^[1-9]$";

        /// <summary>
        /// <para>自然数</para>
        /// 只能是零和非零开头的数字.例如0, 456
        /// </summary>
        public static readonly string NaturalNumber = @"^(0|[1-9][0-9]*)$";

        /// <summary>
        /// <para>有两位小数的正实数,例如45.13, 0.56</para>
        /// </summary>
        public static readonly string ReadNumberTwo = @"^[0-9]+(.[0-9]{2})?$";

        /// <summary>
        /// 有一至三位小数的实数.
        /// <para>例如15.96, 78.126,9.1,7.056</para>
        /// </summary>
        public static readonly string ReadNumberThree = @"^[0-9]+(.[0-9]{1,3})?$";

        #endregion

        #region 字母类

        /// <summary>
        /// 
        /// </summary>
        public static readonly string Password = "[\x21-\x7E]";

        /// <summary>
        /// 中文姓名(2-4个字符);
        /// </summary>
        public static readonly string ChinaName = @"[\u3000-\u9FA5\x20]{2,4}";

        /// <summary>
        /// 用户名,包括英文字母,数字,-,_ 字符.
        /// </summary>
        public static readonly string UserName = @"[w\-]{6-15}";


        #endregion


        #region 验证函数.
        /// <summary>
        /// 检查 input 字符串是否和指定的正则表达式匹配
        /// </summary>
        /// <param name="input">需要检查的字符串</param>
        /// <param name="pattern">正则表达式</param>
        /// <returns></returns>
        public static Boolean IsMatch(String input, String pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        /// <summary>
        /// 检查 input 字符串是否为指定长度的数字.
        /// </summary>
        /// <param name="input">需要检查的字符串</param>
        /// <param name="length">指定长度</param>
        /// <returns></returns>
        public static bool IsNumber(string input, int length)
        {
            string t = @"^\d{" + length.ToString() + "}$";
            return Regex.IsMatch(input, t);
        }

        /// <summary>
        /// 检查 input 字符串是否至少为length位长度的数字.
        /// </summary>
        /// <param name="input">需要检查的字符串</param>
        /// <param name="length">指定长度</param>
        /// <returns></returns>
        public static bool IsNumberMore(string input, int length)
        {
            string t = @"^\d{" + length.ToString() + ",}$";
            return Regex.IsMatch(input, t);
        }

        /// <summary>
        /// 检查 input 字符串是否在指定长度范围内的数字.
        /// <para>比如:匹配6-9位长度的数字,start设置为6,end设置为9.</para>
        /// <para>那么,123456789 将被匹配成功.</para>
        /// </summary>
        /// <param name="input">输入的字符串.</param>
        /// <param name="start">起始长度</param>
        /// <param name="end">结束长度</param>
        /// <returns></returns>
        public static bool IsNumberRange(string input, int start, int end)
        {
            string t = @"^\d{" + start + "," + end + "}$";
            return Regex.IsMatch(input, t);
        }

        #endregion


    }
}
