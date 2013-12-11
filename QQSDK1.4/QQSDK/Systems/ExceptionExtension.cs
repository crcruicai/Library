using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Systems
{
    /// <summary>
    /// 异常的扩展方法.
    /// </summary>
    public static class ExceptionExtension
    {
        /// <summary>
        /// 将异常写入日志.
        /// </summary>
        /// <param name="e"></param>
        public static void WriteLog(this Exception e)
        {
            try
            {

                string str = string.Format("错误消息:{0}\r\n堆栈消息:{1}\r\n ", e.Message, e.StackTrace);
                if (e.TargetSite != null)
                {
                    str = string.Format("{0}异常方法:{1}\r\n", str, e.TargetSite);
                }
                WriteLog(str);
            }
            catch (Exception ex)
            {
                Console.WriteLine("写入文件出错:{0}", ex.Message);
            }
        }

        /// <summary>
        /// 对象锁.
        /// </summary>
        private static object _ObjectLock = new object();

        /// <summary>
        /// 写入日志.
        /// </summary>
        /// <param name="text">文本内容.</param>
        public static void WriteLog(string text)
        {
            try
            {
                lock (_ObjectLock)
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(Environment.CurrentDirectory + "\\Log.txt", true))
                    {
                        sw.WriteLine("Time:{0}", DateTime.Now.ToString());
                        sw.WriteLine("{0}\r\n", text);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("写入日志异常:{0}", e.Message);
                System.Diagnostics.EventLog.WriteEntry(Environment.UserName, "CommonLibrary 写入日志异常.");
            }
        }


    }
}
