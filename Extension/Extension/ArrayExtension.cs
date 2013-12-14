/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/13 16:10:40
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Extension
{
    /// <summary>
    /// 数组方法扩展类
    /// </summary>
    public static class ArrayExtension
    {
        #region 填充数据

        public static T[] Fill<T>(this T[] array,T defult)
        {
            if (array == null) return array;
            return Fill(array, defult, 0, array.Length);
        }

        public static T[] Fill<T>(this T[] array, T defult, int startIndex)
        {
            if (array == null) return array;
            return Fill(array, defult, array.Length - startIndex);
           
        }


        public static T[] Fill<T>(this T[] array, T defult, int startIndex, int length)
        {
            if (array != null)
            {
                if (startIndex < 0 || startIndex >= array.Length)
                {
                    throw new ArgumentOutOfRangeException("startIndex");

                }
                int len = startIndex + length;
                if (length < 0 || len > array.Length)
                {
                    throw new ArgumentOutOfRangeException("length");
                }
                for (int i = startIndex ; i < len; i++)
                {
                    array[i] = defult;
                }
            }
            return array;

        }




        #endregion




    }
}
