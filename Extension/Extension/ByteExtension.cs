/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/24 11:18:27
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Text;

namespace CRC.Extension
{

    /// <summary>
    /// Byte 类扩展方法.
    /// </summary>
    public static class ByteExtension
    {
        #region 数组相等
        /// <summary>
        /// 验证两个字节数组 是否相等.即数据是否相等.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="newobj"></param>
        /// <returns></returns>
        public static bool ArrayAreEqual(this byte[] obj, byte[] newobj)
        {
            if (obj == null && newobj == null) return true;
            if (obj == null || newobj == null) return false;
            if (obj.Length != newobj.Length) return false;
            for (int i = 0; i < obj.Length; i++)
            {
                if (obj[i] != newobj[i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 转换为字符串



        /// <summary>
        /// 将字节转换为16进制字符串.
        /// <para>例如:255 转换为FF</para>
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        /// <summary>
        /// 将枚举器的所有字节转换为十六进制字符串形式.
        /// <para>格式如下:1 15 为 010F</para>
        /// <para>每个字节占两位,并为大写形式.</para>
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHex(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }

        /// <summary>
        /// 将枚举器的所有字节转换为十六进制字符串形式.
        /// <para></para>
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="Prefix">字节的前缀,例如0X这样的字符串.</param>
        /// <param name="split">分隔符,例如空格,逗号等等</param>
        /// <returns></returns>
        public static string ToHex(this IEnumerable<byte> bytes, string Prefix, string split)
        {
            var sb = new StringBuilder();
            foreach (var item in bytes)
            {
                sb.AppendFormat("{0}{1}{2}", Prefix, item.ToString("X2"), split);
            }
            string text = sb.ToString();
            return text.Substring(0, text.Length - split.Length);
        }

        #endregion

        #region 转换为字节
        /// <summary>
        /// 将字符串转换为字节.
        /// 字符串格式必须是十六进制.如AA.
        /// 不带前缀.(大小写忽略)
        /// </summary>
        /// <param name="s">要转换的字符串,长度最大为2</param>
        /// <returns></returns>
        public static byte ToByte(this string str)
        {
            int data = 0;
            if (str == null) throw new ArgumentNullException("str");
            if (str.Length > 2) throw new Exception("str 长度大于2");
            str=str.PadLeft(2, '0');
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                int index = -1;
                if (c > 96 && c < 103) index = (c - 96) + 9;
                if (c > 64 && c < 71) index = (c - 64) + 9;
                if (c > 47 && c < 58) index = c - 48;

                if (index != -1)
                {
                    data += index * Math.Abs(i - 1) * 16 + index * i;
                }
                else
                {
                    throw new Exception("有不合法的字符.");
                }
            }
            return (byte)data;
        }


        /// <summary>
        /// 将16进制的文本(带分隔符)转换为字节数组.
        /// <para>例如0x00 0x01 0x02 分隔符为空格.</para>
        /// <para>支持前缀0x和0X,支持多行处理.</para>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="split"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str,string split)
        {
            //文字预处理.
            str = str.Trim().Replace("0X", "").Replace("0x", "").Replace("\r\n", "");
            string[] strdata = str.Split(new string[] {split}, StringSplitOptions.None );
            List<byte> list = new List<byte>(strdata.Length);
            foreach (var item in strdata )
            {
                string temp = item.Trim();
                try
                {
                    if (temp.Length < 3)
                    {
                        list.Add(temp.ToByte());
                    }
                }
                catch (Exception)
                {
                    
                    
                }
                
            }
            return list.ToArray();
        }

        /// <summary>
        /// 将16进制的文本(没有分隔符)转换为字节数组.
        /// <para>例如:000102 表示00 01 02,转换结果为3个字节.</para>
        /// <para>支持前缀0x和0X,支持多行处理.</para>
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str)
        {
            str = str.Trim().Replace("0X", "").Replace("0x", "").Replace("\r\n", "");
            List<byte> list = new List<byte>(str.Length / 2);
            for (int i = 0; i < str.Length /2; i++)
            {
                string temp = str.Substring(i * 2, 2);
                try
                {
                    list.Add(temp.ToByte());
                }
                catch (Exception e)
                {
                    
                    throw;
                }
            }
            return list.ToArray();

        }

        #endregion

        #region Byte 位运算
        //
        /// <summary>
        /// 获取取第index是否为1
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }
        //
        /// <summary>
        /// 将第index位设为1
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }
        //
        /// <summary>
        /// 将第index位设为0
        /// </summary>
        /// <param name="b">字节</param>
        /// <param name="index">地index位,</param>
        /// <returns></returns>
        public static byte ClearBit(this byte b, int index)
        {
            if (index < 0 && index > 7) throw new ArgumentException("8> index >-1 ");
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }
        //
        /// <summary>
        /// 将第index位取反
        /// </summary>
        /// <param name="b"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }
        #endregion
    }
}
