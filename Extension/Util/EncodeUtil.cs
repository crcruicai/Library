/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/31 8:35:57
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;

namespace CRC.Util
{
    /// <summary>
    /// 编码与解码帮助类.
    /// </summary>
    public static class EncodeUtil
    {
        #region MD5 编码
        /// <summary>
        /// 将字节流转换为MD5 的字符串.
        /// </summary>
        /// <param name="data">字节流</param>
        /// <param name="len">指定MD5的长度.</param>
        /// <returns></returns>
        public static string Md5Encode(byte[] data, int len = 32)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (len > 32)
                throw new ArgumentException("len must be less than 33");
            MD5 md = MD5.Create();
            byte[] array = md.ComputeHash(data);
            StringBuilder text = new StringBuilder();
            foreach (var item in array)
            {
                text.Append(item.ToString("X2"));
            }
            if(len == 32)
                return text.ToString().ToUpper();
            else
            {
                return text.ToString().ToUpper().Substring(0,len);
            }
               

        }

        /// <summary>
        /// MD5 加密.
        /// </summary>
        /// <param name="text">要加密的字符串.</param>
        /// <param name="len">指定长度.</param>
        /// <returns>返回的加密字符串.</returns>
        public static string Md5Encode(string text, int len = 32)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("text");
            if(len > 32)
                throw new ArgumentException("len must be less than 33");
            MD5 md = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            bytes = md.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes)
            {
                sb.Append(item.ToString("X2"));
            }

            if (len == 32) return sb.ToString().ToUpper();
            else
                return sb.ToString().ToUpper().Substring(0,len);

        }

        #endregion MD5 编码

        #region SHA 编码

        /// <summary>
        /// SHA1 加密.
        /// </summary>
        /// <param name="text">要加密的文本.</param>
        /// <returns>SHA1 加密后的文本.</returns>
        public static string Sha1Encode(string text)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] bytes = encode.GetBytes(text);
            bytes = sha.ComputeHash(bytes);
            return BitConverter.ToString(bytes).Replace(" ", "");
        }

        #endregion SHA 编码

        #region BASE 64 编码

        /// <summary>
        /// base64 编码
        /// </summary>
        /// <param name="str">要进行Base64 编码的字符串</param>
        /// <returns>Base64 编码后的字符串</returns>
        public static string ToBase64(string str)
        {

            Encoding encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="str">要进行Base64 解码的字符串</param>
        /// <returns>Base64 解码后的字符串</returns>
        public static string DeBase64(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(bytes);
        }

        #endregion BASE 64 编码

        #region Unicode 操作

        /// <summary>
        /// 将字符串中的 \u,\U 和 \x 转义转换为对应的字符。
        /// </summary>
        /// <param name="str">要转换的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        /// <remarks>
        /// <para>解码方法支持 \x，\u 和 \U 转义，其中 \x 之后可跟 1~4 个十六进制字符，
        /// \u 后面是 4 个十六进制字符，\U 后面则是 8 个十六进制字符。
        /// 使用 \U 转义时，大于 0x10FFFF 的值不被支持，会被舍弃。</para>
        /// <para>如果不满足上面的情况，则不会进行转义，也不会报错。</para></remarks>
        public static string DecodeUnicode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            int idx = str.IndexOf('\\');
            if (idx < 0)
            {
                return str;
            }
            int len = str.Length, start = 0;
            StringBuilder builder = new StringBuilder(len);
            while (idx >= 0)
            {
                // 添加当前 '\' 之前的字符串。
                if (idx > start)
                {
                    builder.Append(str, start, idx - start);
                    start = idx;
                }
                // 跳过 '\' 字符。
                idx++;
                // '\' 字符后的字符数小于 2，不可能是转义字符，直接返回。
                if (idx + 1 >= len)
                {
                    break;
                }
                // 十六进制字符的长度。
                int hexLen = 0;
                // 处理 Unicode 转义。
                switch (str[idx])
                {
                    case 'x':
                        // \x 后面可以是 1 至 4 位。
                        hexLen = GetHexLength(str, idx + 1, 4);
                        break;
                    case 'u':
                        // \u 后面必须是 4 位。
                        if (idx + 4 < len && GetHexLength(str, idx + 1, 4) == 4)
                        {
                            hexLen = 4;
                        }
                        else
                        {
                            hexLen = 0;
                        }
                        break;
                    case 'U':
                        // \U 后面必须是 8 位。
                        if (idx + 8 < len && GetHexLength(str, idx + 1, 8) == 8)
                        {
                            hexLen = 8;
                        }
                        else
                        {
                            hexLen = 0;
                        }
                        break;
                }
                if (hexLen > 0)
                {
                    idx++;
                    int charNum = int.Parse(str.Substring(idx, hexLen), NumberStyles.HexNumber,
                        CultureInfo.InvariantCulture);
                    if (charNum < 0xFFFF)
                    {
                        // 单个字符。
                        builder.Append((char)charNum);
                    }
                    else
                    {
                        // 代理项对的字符。
                        builder.Append(char.ConvertFromUtf32(charNum & 0x1FFFFF));
                    }
                    idx = start = idx + hexLen;
                }
                idx = str.IndexOf('\\', idx);
            }
            // 添加剩余的字符串。
            if (start < len)
            {
                builder.Append(str.Substring(start));
            }
            return builder.ToString();
        }
        /// <summary>
        /// 返回字符串指定索引位置之后的十六进制字符的个数。
        /// </summary>
        /// <param name="str">要获取十六进制字符个数的字符串。</param>
        /// <param name="index">要开始计算十六进制字符个数的其实索引。</param>
        /// <param name="maxLength">需要的最长的十六进制字符个数。</param>
        /// <returns>实际的十六进制字符的个数。</returns>
        internal static int GetHexLength(string str, int index, int maxLength)
        {
            if (index + maxLength > str.Length)
            {
                maxLength = str.Length - index;
            }
            for (int i = 0; i < maxLength; i++, index++)
            {
                if (!IsHex(str, index))
                {
                    return i;
                }
            }
            return maxLength;
        }
        /// <summary>
        /// 将字符串中不可显示字符（0x00~0x1F, 0x7F之后）转义为 \\u 形式，
        /// 其中十六进制以大写字母形式输出。
        /// </summary>
        /// <param name="str">要转换的字符串。</param>
        /// <returns>转换后的字符串。</returns>
        public static string EncodeUnicode(string str)
        {
            return EncodeUnicode(str, true);
        }
        /// <summary>
        /// 将字符串中不可显示字符（0x00~0x1F, 0x7F之后）转义为 \\u 形式。
        /// </summary>
        /// <param name="str">要转换的字符串。</param>
        /// <param name="upperCase">是否以大写字母形式输出十六进制。
        /// 如果为 <c>true</c> 则是以大写字母形式输出十六进制，否则以小写字母形式输出。</param>
        /// <returns>转换后的字符串。</returns>
        /// <overloads>
        /// <summary>
        /// 将字符串中不可显示字符（0x00~0x1F, 0x7F之后）转义为 \\u 形式。
        /// </summary>
        /// </overloads>
        public static string EncodeUnicode(string str, bool upperCase)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            string format = upperCase ? "X4" : "x4";
            StringBuilder builder = new StringBuilder(str.Length * 2);
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c >= ' ' && c <= '~')
                {
                    // 可显示字符。
                    builder.Append(c);
                }
                else
                {
                    builder.Append("\\u");
                    builder.Append(((int)c).ToString(format, CultureInfo.InvariantCulture));
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 指示指定的 Unicode 字符是否属于十六进制数字类别。(包含字符0-9或A-F或a-f)
        /// </summary>
        /// <param name="ch">要计算的 Unicode 字符。</param>
        /// <returns>如果 <paramref name="ch"/> 是十进制数字，则为 <c>true</c>；
        /// 否则，为 <c>false</c>。</returns>
        /// <overloads>
        /// <summary>
        /// 指示指定的 Unicode 字符是否属于十六进制数字类别。
        /// </summary>
        /// </overloads>
        public static bool IsHex( char ch)
        {
            if (ch <= 'f')
            {
                if (ch >= 'A')
                {
                    return ch <= 'F' || ch >= 'a';
                }
                else
                {
                    return ch >= '0' && ch <= '9';
                }
            }
            return false;
        }
        /// <summary>
        /// 指示指定字符串中位于指定位置处的字符是否属于十六进制数字类别。
        /// </summary>
        /// <param name="str">一个字符串。</param>
        /// <param name="index">要计算的字符在 <paramref name="str"/> 中的位置。</param>
        /// <returns>如果 <paramref name="str"/> 中位于 <paramref name="index"/> 处的字符是十进制数字，
        /// 则为 <c>true</c>；否则，为 <c>false</c>。</returns>
        /// <exception cref="System.IndexOutOfRangeException"><paramref name="index"/> 大于等于字符串的长度或小于零。</exception>
        public static bool IsHex(string str, int index)
        {
            return IsHex(str[index]);
        }
        #endregion

        #region URL编码

        /// <summary>
        /// 将文本编码为Http Url的形式.
        /// </summary>
        /// <param name="text">要编码的文本.</param>
        /// <param name="isAll">是否全部编码.</param>
        /// <param name="isUpper">是否全部为大写.</param>
        /// <returns></returns>
        public static string HttpUrlEncode(string text, bool isAll = false, bool isUpper = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                byte item = bytes[i];
                bool a = false;
                if (item > 32 && item < 37) a = true;
                else if (item > 37 && item < 60) a = true;
                else if (item == 61 || item == 95) a = true;
                else if (item > 62 && item < 91) a = true;
                else if (item > 96 && item < 123) a = true;
                else a = false;

                //! * ' ( ) ; : @ & = + $ , / ? # [ ]

                if (a)
                {
                    if (isAll)
                        sb.Append("%" + ByteToUpper(item, true));
                    else
                        sb.Append(Convert.ToChar(item).ToString());
                }
                else if (item == 32)//空格.
                {
                    if (isAll)
                        sb.Append("%" + ByteToUpper(item, true));
                    else
                        sb.Append('+');
                }
                else
                {
                    sb.AppendFormat("%{0}", ByteToUpper(bytes[i], isUpper));
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字节输出为16进制字符
        /// </summary>
        /// <param name="item">字节.</param>
        /// <param name="isUpper">true 大写形式,false 小写形式</param>
        /// <returns></returns>
        private static string ByteToUpper(byte item, bool isUpper)
        {
            return isUpper == true ? item.ToString("X2") : item.ToString("x2");
        }


        #endregion URL编码

    }
}
