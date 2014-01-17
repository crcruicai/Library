/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/31 11:51:11
 * 描述说明：路径工具类.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace CRC.Files
{
    /// <summary>
    /// 路径 工具类.
    /// </summary>
    public static class PathTool
    {
        /// <summary>
        /// 截取字符\前面的字符.
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FatherLayer(string path)
        {
            path = path.TrimEnd('\\');
            int index = path.LastIndexOf("\\", System.StringComparison.Ordinal);
            if (index >0)
            {
                return path.Substring(0, index);
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 获取路径的上一层目录名称.
        /// <para>如输入:C:\abc\udp\pcb,函数返回值为 udp</para>
        /// <para>如果是输入为文件路径,如:C:\abc\udp\pc.txt,函数返回值为udp</para>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LastDirectoryName(string path)
        {
           
            if(path.IndexOf(":", System.StringComparison.Ordinal) > 0)
            {
                path = path.Replace("//", "\\");
                path = path.Replace('/', '\\');
                string[] array=path.Split('\\');
              
                if(array.Length > 2)
                    return array[array.Length - 2];
                else
                {
                    return array[0].Substring (0,1);
                }
              

            }
            return null;
        }

        /// <summary>
        /// 获取路径的上一层目录的路径.
        /// <para>如输入:C:\abc\udp\pcb,函数返回值为 C:\abc\udp</para>
        /// <para>如果是输入为文件路径,如:C:\abc\udp\pc.txt,函数返回值为 C:\abc\udp</para>
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string LastDirectoryPath(string path)
        {
            path = path.Replace("//", "\\");
            path = path.Replace('/', '\\');
            string dirName = LastDirectoryName(path);
            if(dirName != null)
            {
                int lenth = path.IndexOf(dirName, System.StringComparison.Ordinal);
                if(lenth == 0)
                    return dirName + ":\\";
                return path.Substring(0, lenth) + dirName;
            }
            return null;
        }

        /// <summary>
        /// 迭代返回上一级的文件目录的路径.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static IEnumerable<string> NextLastDirectoryPath(string path)
        {
            path = path.Replace("//", "\\");
            path = path.Replace('/', '\\');

            if(path.IndexOf(":", System.StringComparison.Ordinal) > 0)
            {
                string[] array = path.Split('\\');
                for(int i =  array .Length -2; i <0; i--)
                {
                    string dirName = array[i];
                    int lenth = path.IndexOf(dirName, System.StringComparison.Ordinal);
                    if (lenth == 0)
                        yield return dirName + ":\\";
                    yield return path.Substring(0, lenth) + dirName;
                }
            }
        }


    }
}
