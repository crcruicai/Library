#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/24 15:32:59
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

namespace CRC.Util
{
    /// <summary>
    /// 指示选用哪种16进制隔离符
    /// </summary>
    public enum Separate
    {
        /// <summary>
        /// 无分隔符
        /// </summary>
        None,
        /// <summary>
        /// 空格
        /// </summary>
        Bank,
        /// <summary>
        /// 使用"0X"分隔符
        /// </summary>
        OX,
        /// <summary>
        /// 使用"0x"分隔符
        /// </summary>
        Ox
    }

    /// <summary>
    /// 提供字节与字符串之间互相转换的函数.
    /// <para>修改时间:2013-10-24 15:19</para>
    /// </summary>
    public class ConvertCode
    {
        #region 转换为16进制形式的字符串

        /// <summary>
        /// 将字符串转换为16进制码.
        /// </summary>
        /// <param name="str">要转换的字符串</param>
        /// <returns></returns>
        public static string StringToHex(string str, Separate se)
        {
            StringBuilder outstring = new StringBuilder();
            byte[] bytes = Encoding.Default.GetBytes(str);
            string temp = AddSeparate(se);
            for (int i = 0; i < bytes.Length; i++)
            {
                int strInt = Convert.ToInt16(bytes[i] - '\0');

                outstring.AppendFormat("{0}{1}", temp, strInt.ToString("X2"));
            }
            return outstring.ToString();

        }

        #endregion

        #region 转换为字节数组

        /// <summary>
        /// 字符串转换成byte[]
        /// </summary>
        /// <param name="inSting"></param>
        /// <returns></returns>
        public static byte[] StringToBtyes(string inSting)
        {
            inSting = StringToHex(inSting, Separate.None);//把字符串转换成16进制数
            return HexToBtyes(inSting);//把16进制数转换成byte[]
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="se"></param>
        /// <param name="instring"></param>
        /// <returns></returns>
        public static byte[] StringToBytes(string text, Separate se, string instring)
        {
            text = text.Replace("0x", "");
            text = text.Replace("0X", "");
            string[] strdata = text.Split(' ');


            return null;

        }


        /// <summary>
        /// 把16进制形式的字符串转换成byte[]
        /// <para>1 字符串形式如:0x01 0x02</para>
        /// <para>2 字符串形式如:0X01 0X02</para>
        /// <para>3 字符串形式如:0102</para>
        /// <para>4 字符串形式如:01 02</para>
        /// </summary>
        /// <param name="inSting"></param>
        /// <returns></returns>
        public static byte[] HexToBtyes(string inSting)
        {
            inSting = DelSeparate(inSting);//去掉隔离符
            byte[] strBt = new byte[inSting.Length / 2];
            for (int i = 0, j = 0; i < inSting.Length; i = i + 2, j++)
            {
                try
                {
                    string s = inSting.Substring(i, 2);
                    strBt[j] = (byte)Convert.ToInt16(s, 16);
                }
                catch (Exception e)
                {
                    throw new Exception("你填写的数据不是纯16进制数，请检查。");
                }
            }
            return strBt;
        }

        #endregion

        #region 汉字字符串转换为字符串
        /// <summary>
        /// 把16进制字符串变成英文数字和汉字混合的格式。
        /// </summary>
        /// <param name="str">需要转换的16进制字符串</param>
        /// <returns>转换好的字符串</returns>
        public static string HexToString(string inSting)
        {
            inSting = DelSeparate(inSting);
            return Encoding.Default.GetString(HexToBtyes(inSting));
        }




        /// <summary>
        /// 把byte[]转换成String,变成英文数字和汉字混合的格式。
        /// </summary>
        /// <param name="bytes">需要转换的byte[]</param>
        /// <param name="enum16">隔离符</param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes, Separate se)
        {
            return HexToString(BytesTo16(bytes, se));
        }

        #endregion

        #region 字节流转换为字符串
        /// <summary>
        /// byte[]转换成16进制字符串
        /// <para>如:0x01 0x02</para>
        /// </summary>
        /// <param name="bytes">需要转换的byte[]</param>
        /// <param name="enum16"></param>
        /// <returns></returns>
        public static string BytesTo16(byte[] bytes, Separate se)
        {
            StringBuilder outString = new StringBuilder();
            string temp = AddSeparate(se);
            for (int i = 0; i < bytes.Length; i++)
            {
                outString.AppendFormat("{0}{1}", temp, bytes[i].ToString("X2"));//转成16进制数据

                //追加空格.
                switch (se)
                {
                    case Separate.None:
                        break;
                    case Separate.Bank:
                        break;
                    case Separate.OX:
                    case Separate.Ox:
                        outString.Append(" ");
                        break;
                    default:
                        break;
                }

            }
            return outString.ToString();
        }


        /// <summary>
        /// 把byte[]直接转换成二进制形式的字符串，直接以2进制形式显示出来。
        /// <para>如:0x0F 0x01 为00001111 00000001</para>
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToBinary(byte[] bytes, Separate se)
        {
            if (se == Separate.Ox || se == Separate.OX) se = Separate.Bank;
            string temp = AddSeparate(se);
            StringBuilder outString = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string tempString = Convert.ToString(bytes[i], 2).PadLeft(8, '0');

                outString.AppendFormat("{0}{1}", tempString, temp);

            }
            return outString.ToString();

        }
        #endregion

        #region 字节转换
        /// <summary>
        /// Byte 转换为二进制形式的字符串.
        /// <para>如:0x0F 为00001111</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteToBinary(byte data)
        {
            string str = Convert.ToString(data, 2);

            if (str.Length < 8)
            {
                string add = "";
                for (int i = 0; i < 8 - str.Length; i++)
                {
                    add += "0";
                }
                str = add + str;
            }
            return str;
        }

        /// <summary>
        /// 将二进制格式字符串如00001100转换为字节格式.
        /// <para>注意只支持一个字节.</para>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte BinaryToByte(string data)
        {
            int length = Math.Min(data.Length, 8);
            double value = 0;
            int min = length - 1;
            for (int i = 0; i < length; i++)
            {
                if (data[i] == '1')
                {
                    value += Math.Pow(2, min - i);
                }
            }
            return (byte)(value % 256);
        }
        #endregion

        #region  分隔符操作

        /// <summary>
        /// 把Enum16进制隔离符转换成实际的字符串
        /// </summary>
        /// <param name="se">16进制隔离符</param>
        /// <returns></returns>
        private static string AddSeparate(Separate se)
        {
            switch (se)
            {
                case Separate.None:
                    return "";
                case Separate.Ox:
                    return "0x";
                case Separate.OX:
                    return "0X";
                case Separate.Bank:
                    return " ";
                default:
                    return "";
            }
        }
        /// <summary>
        /// 去掉16进制字符串中的隔离符
        /// </summary>
        /// <param name="inString">需要转换的字符串</param>
        /// <returns></returns>
        private static string DelSeparate(string inString)
        {
            StringBuilder outString = new StringBuilder();
            string[] del = { " ", "0x", "0X" };
            if (inString.Contains(" ") || inString.Contains("0x") || inString.Contains("0X"))//存在隔离符
            {
                string[] strS = inString.Split(del, System.StringSplitOptions.RemoveEmptyEntries);//以隔离符进行转换数组，去掉隔离符,去掉空格。
                for (int i = 0; i < strS.Length; i++)
                {
                    outString.Append(strS[i].ToString());
                }
                return outString.ToString();
            }
            else//不存在隔离符
            {
                return inString;
            }
        }
        #endregion
    }
}
