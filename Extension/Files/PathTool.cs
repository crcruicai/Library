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
    public class PathTool
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
            int index = path.LastIndexOf("\\");
            if (index >0)
            {
                return path.Substring(0, index);
            }
            else
            {
                return "";
            }
        }


    }
}
