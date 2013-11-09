/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/24 11:18:27
 * 描述说明：Random(随机器) 扩展方法.
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
    /// Random 类扩展方法.
    /// </summary>
    public static class RandomExtension
    {
        /// <summary>
        /// 随机产生一个bool.
        /// </summary>
        /// <param name="random"></param>
        /// <returns></returns>
        public static bool NextBool(this Random random)
        {
            if (random == null) throw new ArgumentNullException("random");
            return random.NextDouble() > 0.5;
        }

        /// <summary>
        /// 在指定的枚举中,随机产生一个枚举值.
        /// <code >
        /// <![CDATA[ 
        /// enum Shape { Ellipse, Rectangle, Triangle }
        /// Shape shape = random.NextEnum<Shape>();
        /// ]]>
        /// </code>
        /// </summary>
        /// 
        /// <typeparam name="T">枚举类型.</typeparam>
        /// <param name="random"></param>
        /// <returns>返回一个枚举值.</returns>
        public static T NextEnum<T>(this Random random) where T : struct
        {
            if (random == null) throw new ArgumentNullException("random");
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        /// <summary>
        /// 随机产生一个指定长度的字节数组.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] NextBytes(this Random random, int length)
        {
            if (random == null) throw new ArgumentNullException("random");
            if (length < 0) throw new ArgumentException("length 必须大于等于0");
            var data = new byte[length];
            random.NextBytes(data);
            
            return data;
        }

        /// <summary>
        /// 随机产生一个指定长度的数字字符串.
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string NextNumber(this Random random, int length)
        {
            if (random == null) throw new ArgumentNullException("random");
            if (length < 0) throw new ArgumentException("length 必须大于等于0");
            string text = random.Next().ToString();
            if (text.Length >= length)
            {
                //长度大于或等于指定长度,进行截取.
                return text.Substring(0, length);
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
        /// 随机产生指定长度的字符串(只包括字母和数字)
        /// </summary>
        /// <param name="random"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string NextString(this Random random, int length)
        {
            if (random == null) throw new ArgumentNullException("random");
            if (length < 0) throw new ArgumentException("length 必须大于等于0");

            return string.Empty;
        }

        /// <summary>
        /// 在指定的列表中,随机产生一个元素.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="etor"></param>
        /// <returns></returns>
        public static T NextItem<T>(this Random random, IList<T> etor)
        {
            if (random == null) throw new ArgumentNullException("random");
            if (etor == null) throw new ArgumentException();
            int i = random.Next(etor.Count);
            return etor[i];
        }

        /// <summary>
        /// 在指定的数组中,随机产生一个元素.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="random"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T NextItem<T>(this Random random, T[] array)
        {
            if (random == null) throw new ArgumentNullException("random");
            if (array == null) throw new ArgumentException();
            int i = random.Next(array.Length);
            return array[i];
        }


    }
}
