/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/14 9:27:03
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
    /// 字符验证方法.
    /// </summary>
    public static class StringVerifyExtension
    {
        /// <summary>
        /// 检查string是否为Null或Empty.如果是,将显示MessageBox,提示用户.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="message">提示用户输入错误的文本.</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string text,string message)
        {
            if(text==null || text.Length ==0) 
            {
                System.Windows.Forms.MessageBox.Show(message,"警告");
                return true;
            }
            return false;    
        }

       

    }
}
