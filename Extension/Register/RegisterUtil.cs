/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/10 21:01:38
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace CRC.Register
{
    /// <summary>
    /// 注册表工具.
    /// </summary>
    public class RegisterUtil
    {

        /// <summary>
        /// 向注册表设置文件关联 
        /// </summary>
        /// <param name="fileName">指定软件的运行路径.</param>
        /// <param name="fileTypeName">指定关联文件扩展名.例如:".txt"</param>
        public static void SetFileTypeWith(string fileName, string fileTypeName)
        {
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey("", true);              //打开注册表 

            if(regKey != null)
            {
                RegistryKey vrPkey = regKey.OpenSubKey(fileTypeName);
                if (vrPkey != null) regKey.DeleteSubKey(fileTypeName, true);
                regKey.CreateSubKey(fileTypeName);
                vrPkey = regKey.OpenSubKey(fileTypeName, true);
                if(vrPkey != null)
                    vrPkey.SetValue("", "Exec");

                vrPkey = regKey.OpenSubKey("Exec", true);
                if (vrPkey != null) regKey.DeleteSubKeyTree("Exec");         //如果等于空 就删除注册表DSKJIVR 

                regKey.CreateSubKey("Exec");
                vrPkey = regKey.OpenSubKey("Exec", true);
                if(vrPkey != null)
                {
                    vrPkey.CreateSubKey("shell");
                    vrPkey = vrPkey.OpenSubKey("shell", true);                      //写入必须路径 
                    if(vrPkey != null)
                    {
                        vrPkey.CreateSubKey("open");
                        vrPkey = vrPkey.OpenSubKey("open", true);
                        if(vrPkey != null)
                        {
                            vrPkey.CreateSubKey("command");
                            vrPkey = vrPkey.OpenSubKey("command", true);
                            string pathString = "\"" + fileName + "\" \"%1\"";
                            if(vrPkey != null)
                                vrPkey.SetValue("", pathString);                                    //写入数据 
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 删除文件关联.
        /// </summary>
        /// <param name="fileTypeName">扩展名,例如:".txt"</param>
        public static void DeleteFileWith(string fileTypeName)
        {
            RegistryKey regkey = Registry.ClassesRoot.OpenSubKey("", true);

            if(regkey != null)
            {
                RegistryKey vrPkey = regkey.OpenSubKey(fileTypeName);
                if (vrPkey != null) regkey.DeleteSubKey(fileTypeName, true);
                if (vrPkey != null) regkey.DeleteSubKeyTree("Exec");
            }
        } 

    }
}
