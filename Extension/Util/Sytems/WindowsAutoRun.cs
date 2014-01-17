/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 10:14:09
 * 描述说明：Windows开机启动服务代理类.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using Microsoft.Win32;

namespace CRC.Util
{
    /// <summary>
    /// Windows开机启动服务代理.
    /// </summary>
    public class WindowsAutoRun
    {

        #region 开机启动服务
        /// <summary>
        /// 获取开机启动的项是否存在.
        /// </summary>
        /// <param name="keyName">项名称.</param>
        /// <returns>存在为true</returns>
        public static bool GetAutoRun(string keyName)
        {
            try
            {
                bool result = false;
                RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if (runKey != null && runKey.GetValue(keyName) != null)
                {
                    result = true;  
                    runKey.Close();
                }
              
                return result;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 设置开机启动的程序.
        /// </summary>
        /// <param name="keyName">项名称.(一般指定为软件名称)</param>
        /// <param name="filePath">软件的文件路径.</param>
        /// <returns></returns>
        public static bool SetAutoRun(string keyName, string filePath)
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runKey = local.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
                if (runKey != null)
                {
                    runKey.SetValue(keyName, filePath);
                    runKey.Close();
                }

            }
            catch (Exception e)
            {

                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除开机启动项.
        /// </summary>
        /// <param name="keyName">项名称.</param>
        /// <returns></returns>
        public static bool DeleteAutoRun(string keyName)
        {
            try
            {
                RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                if(runKey != null)
                {
                    runKey.DeleteValue(keyName);
                    runKey.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }



        #endregion
    }
}
