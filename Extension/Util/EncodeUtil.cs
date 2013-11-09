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

namespace CRC.Util
{
    /// <summary>
    /// 编码与解码帮助类.
    /// </summary>
    public class EncodeUtil
    {
        /// <summary>
        /// 将字节流转换为MD5 的字符串.
        /// </summary>
        /// <param name="data">字节流</param>
        /// <param name="len">指定MD5的长度.</param>
        /// <returns></returns>
        public static string MD5Encode(byte[] data, int len = 32)
        {
            if (data == null) throw new ArgumentNullException("data");
            MD5 md = MD5.Create();
            byte[] array = md.ComputeHash(data);
            StringBuilder text = new StringBuilder();
            foreach (var item in array)
            {

                text.Append(item.ToString("X2"));
            }
            if (len == 32) return text.ToString().ToUpper();
            else//TODO:这里会有问题.
                return text.ToString().ToUpper().Substring(8, len);

        }

        /// <summary>
        /// MD5 加密.
        /// </summary>
        /// <param name="text">要加密的字符串.</param>
        /// <param name="len">指定长度.</param>
        /// <returns>返回的加密字符串.</returns>
        public static string MD5Encode(string text, int len = 32)
        {
            if (string.IsNullOrEmpty(text)) throw new ArgumentException("text");
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
                return sb.ToString().ToUpper().Substring(8, len);

        }


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
    }
}
