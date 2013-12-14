using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace QQSDK.Net
{
    /// <summary>
    /// 编码与解码器.
    /// </summary>
    public class Encode
    {
        /// <summary>
        /// 将字节转换为MD5 的字符串.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string BytesToMD5String(byte[] data, int len = 32)
        {
            MD5 md = MD5.Create();
            byte[] array = md.ComputeHash(data);
            StringBuilder text = new StringBuilder();
            foreach (var item in array)
            {
               
                text.Append(item.ToString("X").PadLeft(2, '0'));
            }
            if (len == 32) return text.ToString().ToUpper();
            else
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
        /// <returns></returns>
        public static string Sha1Encode(string text)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            ASCIIEncoding encode = new ASCIIEncoding();
            byte[] bytes = encode.GetBytes(text);
            bytes = sha.ComputeHash(bytes);
            return BitConverter.ToString(bytes).Replace("", "");
        }

        /// <summary>
        /// 将文本转换为UTF8编码格式的文本.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isAll"></param>
        /// <param name="isUpper"></param>
        /// <returns></returns>
        public  static string ToUTF8(string text, bool isAll = false, bool isUpper = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            StringBuilder sb = new StringBuilder();
            byte item;
            for (int i = 0; i < bytes.Length; i++)
            {
                item = bytes[i];
                if (item < 128)
                {
                    bool flag = false;
                    //char[] array = "".ToCharArray();
                    //foreach (var sitem in array)
                    //{
                    //    if (sitem == (char)item) flag = true;
                    //}
                    if (flag || isAll)
                        sb.Append("%" + byteToUpper(item, isAll));
                    else
                        sb.Append(Convert.ToChar(item).ToString());

                }
                else
                {

                    sb.AppendFormat("%{0}", byteToUpper(bytes[i++], isUpper));
                    if (bytes.Length > i)
                    {
                        sb.AppendFormat("%{0}", byteToUpper(bytes[i], isUpper));
                    }

                }

            }


            return sb.ToString();
        }


        private static string byteToUpper(byte item, bool flag)
        {
            return flag == true ? item.ToString("X2") : item.ToString("x2");
        }


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
            byte item;
            for (int i = 0; i < bytes.Length; i++)
            {
                item = bytes[i];
                bool a = false;
                if (item > 32 && item < 37) a = true;
                else if (item > 37 && item < 60) a = true;
                else if (item == 61 || item == 91 || item == 95) a = true;
                else if (item > 62 && item < 92) a = true;
                else if (item > 96 && item < 123) a = true;
                else a = false;

                //! * ' ( ) ; : @ & = + $ , / ? # [ ]

                if (a)
                {
                    if (isAll)
                        sb.Append("%" + byteToUpper(item, isAll));
                    else
                        sb.Append(Convert.ToChar(item).ToString());
                }
                else if (item == 32)//空格.
                {
                    if (isAll)
                        sb.Append("%" + byteToUpper(item, isAll));
                    else
                        sb.Append('+');
                }
                else
                {
                    sb.AppendFormat("%{0}", byteToUpper(bytes[i], isUpper));
                }

            }
            return sb.ToString();
        }

        /// <summary>
        /// GB2312 编码.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isAll"></param>
        /// <param name="isUpper"></param>
        /// <returns></returns>
        public static string ToGB2312(string text,bool isAll=false ,bool isUpper=false )
        {
             byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(text);
            StringBuilder sb = new StringBuilder();
            byte item;
            for (int i = 0; i < bytes.Length; i++)
            {
                item = bytes[i];
                if (item < 128)
                {
                    bool flag = false;
                    //char[] array = "".ToCharArray();
                    //foreach (var sitem in array)
                    //{
                    //    if (sitem == (char)item) flag = true;
                    //}
                    if (flag || isAll)
                        sb.Append("%" + byteToUpper(item, isAll));
                    else
                        sb.Append(Convert.ToChar(item).ToString());

                }
                else
                {

                    sb.AppendFormat("%{0}", byteToUpper(bytes[i++], isUpper));
                    if (bytes.Length > i)
                    {
                        sb.AppendFormat("%{0}", byteToUpper(bytes[i], isUpper));
                    }

                }

            }


            return sb.ToString();
        }

        /// <summary>
        /// Unicode 编码.(有问题)
        /// </summary>
        /// <param name="text"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public static string ToUnicode(string text, bool isAll = false)
        {
            StringBuilder sb=new StringBuilder ();
            char[] array = text.ToCharArray();
            for (int i = 0; i < array .Length ; i++)
            {
                if(array[i]<='\0' || array [i]>='â' ||isAll)
                {
                    byte[] bytes=Encoding .Unicode .GetBytes (array [i].ToString ());
                    sb.AppendFormat ("\\u{0}",bytes[1].ToString ());
                }
                else
                {
                    sb.Append(array[i]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将字符串转换为Unicode编码方式.
        /// <para>形式如:</para>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isAll"></param>
        /// <returns></returns>
        public static string ToUnicodeString(string str, bool isAll = false)
        {
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {

                for (int i = 0; i < str.Length; i++)
                {

                    int value = (int)str[i];
                    if (value < 256 && !isAll)
                    {
                        strResult.Append(str[i]);
                    }
                    else
                    {
                        strResult.Append("\\u");
                        strResult.Append(value.ToString("X").PadLeft(4, '0'));
                    }

                }
            }
            return strResult.ToString();
        }


        /// <summary>
        /// Unicode 解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DeUnicode(string str)
        {

            //最直接的方法Regex.Unescape(str);
            StringBuilder strResult = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                str = str.Replace("\\r", "\\\r");
                string[] strlist = str.Replace("\\", "").Split('u');
                try
                {
                    if (strlist.Length > 0)
                        strResult.Append(strlist[0]);
                    for (int i = 1; i < strlist.Length; i++)
                    {
                        string text = strlist[i];
                        if (text.Length > 4)
                        {
                            int charCode = Convert.ToInt32(text.Substring(0, 4), 16);
                            strResult.Append((char)charCode);
                            strResult.Append(text.Substring(4, text.Length - 4));
                        }
                        else
                        {
                            int charCode = Convert.ToInt32(strlist[i], 16);
                            strResult.Append((char)charCode);
                        }

                    }
                }
                catch (FormatException)
                {
                    return Regex.Unescape(str);
                }
            }

            return strResult.ToString().Replace ("\r","\r\n");
        }

        /// <summary>
        /// base64 编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string ToBase64(string str)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64 解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string DeBase64(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(bytes);
        }
    }
}
