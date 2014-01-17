/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 11:21:46
 * 描述说明：IP工具类.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace CRC.Net
{
    /// <summary>
    /// IP工具类.
    /// </summary>
    public static class IPUtil
    {

        /// <summary>
        /// 获取IP地址的文本形式,由字节数组转换.(4个字节)
        /// <para>例如字节数组 {192,168,0,1}将转换为192.168.0.1 </para>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns></returns>
        public static string GetIPText(byte[] data)
        {
            if (data == null) throw new NullReferenceException("Data is Null");
            if (data.Length < 4) throw new IndexOutOfRangeException("Array Length is Error");
            return string.Format("{0}.{1}.{2}.{3}", data[0], data[1], data[2], data[3]);
        }

        /// <summary>
        /// 将字节数组(4个字节)转换为IPAddress.
        /// <para>例如字节数组 {192,168,0,1}将转换为192.168.0.1 </para>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <returns></returns>
        public static IPAddress GetIPAddress(byte[] data)
        {
            return IPAddress.Parse(GetIPText(data));
        }
        /// <summary>
        /// 将字节数组(4个字节)转换为IPAddress.
        /// <para>例如字节数组 {192,168,0,1}将转换为192.168.0.1 </para>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="start">起始位置.</param>
        /// <returns></returns>
        public static IPAddress GetIPAddress(byte[] data, int start)
        {
            return IPAddress.Parse(GetIPText(data, start));
        }

        /// <summary>
        /// 获取IP地址的文本形式,由字节数组转换.
        /// <para>例如字节数组 {192,168,0,1}将转换为192.168.0.1 </para>
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="start">起始位置.</param>
        /// <returns></returns>
        public static string GetIPText(byte[] data, int start)
        {
            if (data.Length < 4 + start) throw new IndexOutOfRangeException("Array Length is Error");
            return string.Format("{0}.{1}.{2}.{3}", data[start], data[start + 1], data[start + 2], data[start + 3]);
        }

        /// <summary>
        /// 获取IP端口号.由字节数组转换
        /// </summary>
        /// <param name="data">字节数组</param>
        /// <param name="index">起始位置.</param>
        /// <returns></returns>
        public static int GetPort(byte[] data, int index)
        {
            if (data.Length < 2 + index) throw new IndexOutOfRangeException("Array Length is Error");
            return data[index] * 256 + data[index + 1];
        }

        /// <summary>
        /// 将端口号转换为字节(两个字节)
        /// </summary>
        /// <param name="port">端口号</param>
        /// <returns></returns>
        public static byte[] PortToBytes(int port)
        {
            if (port > 65535 && port < 0) throw new Exception("port is not in range");
            byte[] data = new byte[2];
            data[0] = (byte)(port / 256);
            data[1] = (byte)(port % 256);

            return data;
        }

        /// <summary>
        /// 获取本地的IPV4的地址.
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetIPAddress()
        {
            System.Net.IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            for (int i = 0; i != ipEntry.AddressList.Length; i++)
            {
                if (!ipEntry.AddressList[i].IsIPv6LinkLocal)
                {
                    return ipEntry.AddressList[i];
                }
            }
            return IPAddress.Parse("127.0.0.1");
        }

        /// <summary>
        /// 将文本转换为IP终结点.
        /// </summary>
        /// <param name="ipString">IP终结点字符串形式.例如:192.168.0.1:8001</param>
        /// <exception cref="System.FormatException">不是有效的IP终结点</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        public static IPEndPoint ParseIPEndPoint(string ipString)
        {
            if (string.IsNullOrEmpty(ipString)) throw new ArgumentNullException("ipString 不能为空或者");
            string[] str = ipString.Split(':');

            if (str.Length >= 2)
            {
                IPAddress add = IPAddress.Parse(str[0]);
                int port = int.Parse(str[1]);
                return new IPEndPoint(add, port);
            }
            else
            {
                throw new System.FormatException("ipString 不是有效的 IP 地址");
            }


        }


    }
}
