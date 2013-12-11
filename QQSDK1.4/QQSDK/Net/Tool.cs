using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace QQSDK.Net
{


    /// <summary>
    /// 工具帮助类.
    /// </summary>
    public static class Tool
    {
        /// <summary>
        /// 获取指定长度的随机数.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetRandomNumber(int length)
        {
            if (length < 0) throw new ArgumentException("length 必须大于等于0!");
            Random r = new Random();
            string text = r.Next().ToString();
            if (text.Length >= length)
            { 
                //长度大于或等于指定长度,进行截取.
                return text.Substring (0,length);
            }
            else
            {
                //长度小于指定长度,进行不足.
                int len = length - text.Length;
                for (int i = 0; i < len; i++)
                {
                    text = string.Format("{0}0", text);
                }
                return text;
            }
        }

        /// <summary>
        /// 检查QQ密码是否符合格式.
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>符合,为true</returns>
        public static bool CheckQQPassword(string password)
        {
            int length = password.Trim().Length;
            return length < 6 ? false : length > 16 ? false : true;
        }

        /// <summary>
        /// 检查QQ号码是否符合规范.
        /// </summary>
        /// <param name="qqnumber">QQ号码.</param>
        /// <returns></returns>
        public static bool CheckQQNumber(string qqnumber)
        {
            qqnumber = qqnumber.Trim();
            //使用正则表达式验证数字.
            //"^[0-9]*[1-9][0-9]*$"正整数

            return true;

        }
        #region Hash算法


        /// <summary>
        /// 获取好友列表的Hash算法.
        /// </summary>
        /// <param name="num">QQ号码.</param>
        /// <param name="ptwebqq">PTWebQQ标识码.</param>
        /// <returns></returns>
        public static string Hash(ulong num, string ptwebqq)
        {
            ulong[] array = new ulong[]
			{
				num >> 24 & 255uL,
				num >> 16 & 255uL,
				num >> 8 & 255uL,
				num & 255uL
			};
            char[] array2 = ptwebqq.ToCharArray();
            Stack<HashItem> stack = new Stack<HashItem>();
            stack.Push(new HashItem(0, array2.Length - 1));
            while (stack.Count > 0)
            {
                HashItem qqitem = stack.Pop();
                if (qqitem.index < qqitem.Value && qqitem.index >= 0 && qqitem.Value < array2.Length)
                {
                    if (qqitem.index + 1 == qqitem.Value)
                    {
                        if (array2[qqitem.index] > array2[qqitem.Value])
                        {
                            char c = array2[qqitem.index];
                            array2[qqitem.index] = array2[qqitem.Value];
                            array2[qqitem.Value] = c;
                        }
                    }
                    else
                    {
                        int index = qqitem.index;
                        int value = qqitem.Value;
                        char c2 = array2[qqitem.index];
                        while (qqitem.index < qqitem.Value)
                        {
                            while (qqitem.index < qqitem.Value && array2[qqitem.Value] >= c2)
                            {
                                qqitem.Value--;
                                array[0] = (array[0] + 3uL & 255uL);
                            }
                            if (qqitem.index < qqitem.Value)
                            {
                                array2[qqitem.index] = array2[qqitem.Value];
                                qqitem.index++;
                                array[1] = (array[1] * 13uL + 43uL & 255uL);
                            }
                            while (qqitem.index < qqitem.Value && array2[qqitem.index] <= c2)
                            {
                                qqitem.index++;
                                array[2] = (array[2] - 3uL & 255uL);
                            }
                            if (qqitem.index < qqitem.Value)
                            {
                                array2[qqitem.Value] = array2[qqitem.index];
                                qqitem.Value--;
                                array[3] = ((array[0] ^ array[1] ^ array[2] ^ array[3] + 1uL) & 255uL);
                            }
                        }
                        array2[qqitem.index] = c2;
                        stack.Push(new HashItem(index, qqitem.index - 1));
                        stack.Push(new HashItem(qqitem.index + 1, value));
                    }
                }
            }
            array2 = new char[]{
                '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};
            string text = "";
            for (int j = 0; j < array.Length; j++)
            {
                checked
                {
                    text += array2[(int)((IntPtr)(array[j] >> 4 & 15uL))];
                    text += array2[(int)((IntPtr)(array[j] & 15uL))];
                }
            }
            return text;
        }

        //

        /// <summary>
        /// 获取朋友列表哈希算法.(2013-12-05更新)
        /// JS 代码地址:http://0.web.qstatic.com/webqqpic/pubapps/0/50/eqq.all.js
        /// 脚本引擎 http://javascriptdotnet.codeplex.com/
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <param name="ptWebQQ">ptWebQQ令牌</param>
        /// <returns></returns>
        public static string Hash2(ulong qqNumber, string ptWebQQ)
        {
            ulong[] code = new ulong[4];
            for (int i = 0; i < ptWebQQ.Length ; i++)
            {
                code[i % 4] ^= ptWebQQ[i];
            }
            var fixChar = new string[]{ "EC", "OK" };
            ulong[] newcode = new ulong[4];
            newcode[0] = qqNumber >> 24 & 255 ^ fixChar[0][0];
            newcode[1] = qqNumber >> 16 & 255 ^ fixChar[0][1];
            newcode[2] = qqNumber >> 8 & 255 ^ fixChar[1][0];
            newcode[3] = qqNumber & 255 ^ fixChar[1][1];
            var table = new ulong[8];
            for (int i = 0; i < 8; i++)
            {
                table[i] = i % 2 == 0 ? code[i >> 1] : newcode[i >> 1];
            }
            char[] charTable = new char[]{
                '0','1','2','3','4','5','6','7','8','9','A','B','C','D','E','F'};
            string result="";
            for (int i = 0; i < table.Length; i++)
            {
                result +=charTable[table[i] >> 4 & 15];
                result +=charTable[table[i] & 15];
            }
            return result;

        }


       
        /// <summary>
        /// 获取好友列表的Hash码.(不能用)
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="ptwebqq"></param>
        /// <returns></returns>
        public static string GetHash(string uin, string ptwebqq)
        {
            int[] c = new int[uin.Length];
            for (int d = 0; d < uin.Length; d++)
                c[d] = Convert.ToInt32(uin.Substring(d, 1)) - 0;

            byte[] ecode = Encoding.Default.GetBytes(ptwebqq);
            int k = -1; for (int b = 0, d = 0; d < c.Length; d++)
            {
                b += c[d]; b %= ptwebqq.Length; var f = 0;
                if (b + 4 > ptwebqq.Length)
                {
                    for (int g = 4 + b - ptwebqq.Length, h = 0; h < 4; h++)
                        f |= h < g ?
                            (ecode[b + h] & 255) << (3 - h) * 8 :
                            (ecode[h - g] & 255) << (3 - h) * 8;
                }
                else
                {
                    for (int h = 0; h < 4; h++) f |= (ecode[b + h] & 255) << (3 - h) * 8; k ^= f;
                }
            }
            c = new int[4];
            c[0] = k >> 24 & 255;
            c[1] = k >> 16 & 255;
            c[2] = k >> 8 & 255;
            c[3] = k & 255;
            string[] w = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            string v = "";
            for (int b = 0; b < c.Length; b++)
            {
                v += w[c[b] >> 4 & 15];
                v += w[c[b] & 15];
            } return v;
        }

        #endregion


        /// <summary>
        /// 将字体的样式(粗体,斜体,下划线)转换为文本表示.
        /// <para>如粗体,斜体,下划线将被表示为1,1,1</para>
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static string GetFontStyle(Font font)
        {
            string text="";
            bool[] array = new bool[] { font.Bold, font.Italic, font.Underline };
            foreach (var item in array)
            {
                if (item) text += "1,";
                else
                    text += "0,";
            }
            return text.Substring (0,text.Length -1);
        }

        /// <summary>
        /// 将文本形式的样式转换为字体样式.
        /// <para>如:1,1,1将转换为(粗体,斜体,下划线)</para>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static FontStyle GetFontStyle(string text)
        {
            string[] array = text.Split(',');
            if (array.Length > 2)
            {
                FontStyle s= FontStyle.Regular ;
                if (array[0] == "1") s = FontStyle.Bold;
                if (array[1] == "1") s |= FontStyle.Italic;
                if (array[1] == "1") s |= FontStyle.Underline;
                return s;
            }
            else
            {
                return FontStyle.Regular;
            }
        }

        /// <summary>
        /// 将字符串转换为字节.
        /// 字符串格式必须是十六进制.如AA.
        /// 不带前缀.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte GetByte(string str)
        {
            int data = 0;
            if (str.Length > 2) throw new Exception("str 长度大于2");
            for (int i = 0; i < 2; i++)
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
        /// 将字符串转换为字节.
        /// 字符串格式必须是十六进制.如AA.
        /// 不带前缀.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool TryParse(string str,out byte result)
        {
            int data = 0;
            if (str.Length > 2) throw new Exception("str 长度大于2");
            for (int i = 0; i < 2; i++)
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
                    result = (byte)data;
                    return false;
                }
            }
            result = (byte)data;
            return true;
        }

        /// <summary>
        /// 将文本转换为颜色Color.
        /// <para>如:FF0000将被转换为红色.</para>
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Color GetColor(string text)
        {
            byte t;
            int r = 0, g = 0, b = 0;
            string s;
            if (text.Length < 6) return Color.Black;
            s = text.Substring(0, 2);
            if (TryParse(s, out t))
            {
                r = t;
            }
            s = text.Substring(2, 2);
            if (TryParse(s, out t))
            {
                g = t;
            }
            s = text.Substring(4, 2);
            if (TryParse(s, out t))
            {
                b = t;
            }
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 将颜色转换为文本.如红色.转换为文本:FF0000
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string GetColor(Color color)
        {
            string text =color .R .ToString("X").PadLeft (2,'0');
            text += color.G.ToString("X").PadLeft(2, '0');
            text += color.B.ToString("X").PadLeft(2, '0');
            return text;
        }


    }


    /// <summary>
    /// 用于哈希值的运算.
    /// </summary>
    public class HashItem
    {
        public int index;
        public int Value;
        public HashItem(int index, int value)
            : base()
        {

            //base..ctor();
            this.index = index;
            this.Value = value;
        }
    }
}
