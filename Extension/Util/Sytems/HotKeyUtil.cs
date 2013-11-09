/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 10:51:11
 * 描述说明：系统热键帮助类.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace CRC.Util
{
 

    /// <summary>
    /// 系统热键帮助类
    /// </summary>
    public class HotKeyHelper
    {

        //如果函数执行成功，返回值不为0。
        //如果函数执行失败，返回值为0。要得到扩展错误信息，调用GetLastError。
        /// <summary>
        /// 向系统注册热键
        /// </summary>
        /// <param name="hWnd">定义热键的窗口的句柄</param>
        /// <param name="id">定义热键ID（不能与其它ID重复</param>
        /// <param name="fsModifiers">标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效</param>
        /// <param name="vk">定义热键的内容</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(
            IntPtr hWnd,                //要定义热键的窗口的句柄
            int id,                     //定义热键ID（不能与其它ID重复）           
            KeyModifiers fsModifiers,   //标识热键是否在按Alt、Ctrl、Shift、Windows等键时才会生效
            System.Windows.Forms.Keys vk                     //定义热键的内容
            );
        /// <summary>
        /// 注销热键
        /// </summary>
        /// <param name="hWnd">要取消热键的窗口的句柄</param>
        /// <param name="id">要取消热键的ID</param>
        /// <returns></returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );


        /// <summary>
        /// 定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
        /// </summary>
        [Flags()]
        public enum KeyModifiers
        {
            None = 0,
            Alt = 1,
            Ctrl = 2,
            Shift = 4,
            WindowsKey = 8
        }



        public Dictionary<int, HotKeyInfo> Dic;
        public HotKeyHelper()
        {
            Dic = new Dictionary<int, HotKeyInfo>();

        }
        /// <summary>
        /// 添加一个热键。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Add(HotKeyInfo info)
        {
            Dic.Add(info.ID, info);
            return RegisterHotKey(info.Hand, info.ID, info.Modifiers, info.Key);

        }
        /// <summary>
        /// 移除一个热键。
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public bool Remove(HotKeyInfo info)
        {
            Dic.Remove(info.ID);
            UnregisterHotKey(info.Hand, info.ID);
            return true;

        }

        /// <summary>
        /// 注销所有热键。
        /// </summary>
        /// <returns></returns>
        public bool RemoveAll()
        {
            foreach (HotKeyInfo info in Dic.Values)
            {
                UnregisterHotKey(info.Hand, info.ID);
            }
            return true;
        }
        /// <summary>
        /// 根据ID获取热键的信息。
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotKeyInfo GetHotKeyInfo(int id)
        {
            HotKeyInfo info = null;
            if (Dic.ContainsKey(id))
            {
                Dic.TryGetValue(id, out info);
            }
            return info;

        }

    }


    /// <summary>
    /// 代表系统热键的信息。
    /// </summary>
    public class HotKeyInfo
    {
        /// <summary>
        /// 命令行。
        /// </summary>
        public string Commond;
        /// <summary>
        /// 按键
        /// </summary>
        public System.Windows.Forms.Keys Key;
        /// <summary>
        /// 功能按键
        /// </summary>
        public HotKeyHelper.KeyModifiers Modifiers;
        /// <summary>
        /// 热键ID号。
        /// </summary>
        public int ID;
        /// <summary>
        /// 句柄。
        /// </summary>
        public IntPtr Hand;
        /// <summary>
        /// 命令参数。
        /// </summary>
        public string Arguments;
        /// <summary>
        /// 构造一个热键。
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="id"></param>
        /// <param name="modifiers"></param>
        /// <param name="key"></param>
        /// <param name="commond"></param>
        public HotKeyInfo(IntPtr hand, int id, HotKeyHelper.KeyModifiers modifiers, System.Windows.Forms.Keys key, string commond)
        {
            Commond = commond;
            Key = key;
            Modifiers = modifiers;
            ID = id;
            Hand = hand;

        }
        public HotKeyInfo(IntPtr hand, int id, HotKeyHelper.KeyModifiers modifiers, System.Windows.Forms.Keys key, string commond, string arguments)
        {
            Commond = commond;
            Key = key;
            Modifiers = modifiers;
            ID = id;
            Hand = hand;
            Arguments = arguments;

        }
        public HotKeyInfo()
        {


        }

    }

}
