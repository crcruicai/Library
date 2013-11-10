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
            RegistryKey _RegKey = Registry.ClassesRoot.OpenSubKey("", true);              //打开注册表 

            RegistryKey _VRPkey = _RegKey.OpenSubKey(fileTypeName);
            if (_VRPkey != null) _RegKey.DeleteSubKey(fileTypeName, true);
            _RegKey.CreateSubKey(fileTypeName);
            _VRPkey = _RegKey.OpenSubKey(fileTypeName, true);
            _VRPkey.SetValue("", "Exec");

            _VRPkey = _RegKey.OpenSubKey("Exec", true);
            if (_VRPkey != null) _RegKey.DeleteSubKeyTree("Exec");         //如果等于空 就删除注册表DSKJIVR 

            _RegKey.CreateSubKey("Exec");
            _VRPkey = _RegKey.OpenSubKey("Exec", true);
            _VRPkey.CreateSubKey("shell");
            _VRPkey = _VRPkey.OpenSubKey("shell", true);                      //写入必须路径 
            _VRPkey.CreateSubKey("open");
            _VRPkey = _VRPkey.OpenSubKey("open", true);
            _VRPkey.CreateSubKey("command");
            _VRPkey = _VRPkey.OpenSubKey("command", true);
            string _PathString = "\"" + fileName + "\" \"%1\"";
            _VRPkey.SetValue("", _PathString);                                    //写入数据 

        }

        /// <summary>
        /// 删除文件关联.
        /// </summary>
        /// <param name="fileTypeName">扩展名,例如:".txt"</param>
        public static void DeleteFileWith(string fileTypeName)
        {
            RegistryKey _Regkey = Registry.ClassesRoot.OpenSubKey("", true);

            RegistryKey _VRPkey = _Regkey.OpenSubKey(fileTypeName);
            if (_VRPkey != null) _Regkey.DeleteSubKey(fileTypeName, true);
            if (_VRPkey != null) _Regkey.DeleteSubKeyTree("Exec");
        } 

    }
}
