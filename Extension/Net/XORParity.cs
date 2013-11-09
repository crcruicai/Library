/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 11:32:41
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRC.Net
{
    /// <summary>
    /// 校验码 帮助类.
    /// </summary>
    public class XORParity
    {


        /// <summary>
        /// 计算数据的校验码.(采用异或计算)
        /// </summary>
        /// <param name="data">字节流数据.</param>
        /// <param name="start">起始的位置.</param>
        /// <param name="length">长度.</param>
        /// <returns></returns>
        public static byte GetParity(List<byte> data, int start, int length)
        {
            byte parity = 0x00;
            if (data == null) throw new Exception("Array is Null");
            if (start + length > data.Count) throw new Exception("长度溢出.");
            for (int i = 0; i < length; i++)
            {
                parity ^= data[start + i];
            }
            return parity;

        }

        /// <summary>
        /// 计算数据的校验码.(采用异或计算)
        /// </summary>
        /// <param name="data">字节流数据.</param>
        /// <param name="start">起始的位置.</param>
        /// <param name="length">长度.</param>
        /// <returns></returns>
        public static byte GetParity(byte[] data, int start, int length)
        {
            byte parity = 0x00;
            if (data == null) throw new Exception("Array is Null");
            if (start + length > data.Length) throw new Exception("长度溢出.");
            for (int i = 0; i < length; i++)
            {
                parity ^= data[start + i];
            }
            return parity;

        }

        /// <summary>
        /// 计算数据的校验码.(采用异或计算)
        /// </summary>
        /// <param name="data">字节流数据.</param>
        /// <returns>返回校验码.</returns>
        public static byte GetParity(byte[] data)
        {
            byte parity = 0x00;
            if (data == null) throw new Exception("Array is Null");
            foreach (var item in data)
            {
                parity ^= item;
            }
            return parity;
        }

        /// <summary>
        /// 计算数据的校验码.(采用异或计算)
        /// </summary>
        /// <param name="data">字节流数据.</param>
        /// <returns>返回校验码.</returns>
        public static byte GetParity(IEnumerable<byte> data)
        {
            if (data == null) throw new ArgumentNullException("data");
            byte parity = 0x00;
            foreach (var item in data)
            {
                parity ^= item;
            }
            return parity;
        }

      
    }
}
