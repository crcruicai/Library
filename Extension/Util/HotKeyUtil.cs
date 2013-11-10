/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/10 21:18:08
 * 描述说明：
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
    /// 热键 帮助器.(未完成,待测试)
    /// </summary>
    public class HotKeyUtil : System.Windows.Forms.IMessageFilter
    {
        #region 字段与变量
        Dictionary<int, HotKeyData> _HotMap;
        #endregion

        #region 构造函数


        public HotKeyUtil()
        {
            _HotMap = new Dictionary<int, HotKeyData>();
            
        }
        #endregion

        #region 属性

        #endregion

        #region 公共函数

        /// <summary>
        /// 执行热键任务.
        /// <para>注意,执行任务时,默认为Action优先.调用线程的任务将不会执行.</para>
        /// </summary>
        /// <param name="id"></param>
        public bool RunHotKey(int id)
        {
            if (_HotMap.ContainsKey(id))
            {
                HotKeyData data = _HotMap[id];
                if (data.Action != null)
                {
                    data.Action();//执行委托.
                }
                else
                {
                    //开启线程 执行任务.
                    System.Diagnostics.Process.Start(data.Command,data.Arguments);
                }
                return true;
            }
            return false;
        }



        /// <summary>
        /// 获取指定的热键.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotKeyData GetHotKey(int id)
        {
            HotKeyData data;
            _HotMap.TryGetValue(id, out data);
            return data;
        }

        /// <summary>
        /// 向系统注册一个热键.
        /// </summary>
        /// <param name="data">热键的描述信息.</param>
        /// <returns></returns>
        public bool RegisterHotKey(HotKeyData data)
        {
            if (data == null) throw new ArgumentNullException("data");
            if (_HotMap.ContainsKey(data.ID))
            {
                _HotMap.Add(data.ID, data);
                return HotKeyUtil.RegisterHotKey(data.Hand, data.ID, data.Modifiers, data.Key);
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 移除所有热键.
        /// </summary>
        /// <returns></returns>
        public bool RemoveAll()
        {
            bool result = true;
            foreach (var item in _HotMap.Values)
            {
                result &=HotKeyUtil.UnregisterHotKey(item.Hand, item.ID);
            }
            return result;
        }

        /// <summary>
        /// 移除指定的热键.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Remove(HotKeyData data)
        {
            if (_HotMap.ContainsKey(data.ID))
            {
                _HotMap.Remove(data.ID);
                return HotKeyUtil.UnregisterHotKey(data.Hand, data.ID);
            }
            return false;
        }

        /// <summary>
        /// 移除热键.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Remove(int id)
        {
            if (_HotMap.ContainsKey(id))
            {
                HotKeyData data = _HotMap[id];
                _HotMap.Remove(id);
                return HotKeyUtil.UnregisterHotKey(data.Hand, data.ID);
            }
            return false;
        }

        #endregion

        #region 私有函数

        #endregion

        #region API

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
        private static extern bool RegisterHotKey(
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
        private static extern bool UnregisterHotKey(
            IntPtr hWnd,                //要取消热键的窗口的句柄
            int id                      //要取消热键的ID
            );



        #endregion

        #region IMessageFilter 成员

        public virtual bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x312) /*WM_HOTKEY*/
            {
                return RunHotKey(m.WParam.ToInt32());
            }
            return false;
        }

        #endregion
    }


    /// <summary>
    /// 定义了辅助键的名称（将数字转变为字符以便于记忆，也可去除此枚举而直接使用数值）
    /// </summary>
    [Flags()]
    public enum KeyModifiers
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,
        /// <summary>
        /// Alt功能键.
        /// </summary>
        Alt = 1,
        /// <summary>
        /// Ctrl功能键.
        /// </summary>
        Ctrl = 2,
        /// <summary>
        /// Shift功能键.
        /// </summary>
        Shift = 4,
        /// <summary>
        /// Windows功能键.
        /// </summary>
        WindowsKey = 8
    }


    public class HotKeyData
    {
        #region 属性
        private string  _Command;
        /// <summary>
        /// 命令行.
        /// </summary>	
        public string  Command
        {
            get { return _Command; }
            set { _Command = value; }
        }

        private int _ID;
        /// <summary>
        /// 热键的ID号,用户可以自行定义.
        /// </summary>	
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private System.Windows.Forms.Keys _Key;
        /// <summary>
        /// 该热键的键盘按键
        /// </summary>	
        public System.Windows.Forms.Keys Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        private KeyModifiers  _Modifiers;
        /// <summary>
        /// 热键的功能键.
        /// </summary>	
        public KeyModifiers  Modifiers
        {
            get { return _Modifiers; }
            set { _Modifiers = value; }
        }

        private IntPtr  _Hand;
        /// <summary>
        /// 热键依附的句柄.
        /// </summary>	
        public IntPtr  Hand
        {
            get { return _Hand; }
            set { _Hand = value; }
        }

        private string _Arguments;
        /// <summary>
        /// 命令参数.
        /// </summary>	
        public string Arguments
        {
            get { return _Arguments; }
            set { _Arguments = value; }
        }

        private Action  _Action;
        /// <summary>
        /// 执行一个任务.
        /// </summary>	
        public Action  Action
        {
            get { return _Action; }
            set { _Action = value; }
        }


        #endregion

        #region 构造函数

        public HotKeyData(IntPtr hand, int id, KeyModifiers modifiers, System.Windows.Forms.Keys key, string command,string arguments)
        {
            _Hand = hand;
            _ID = id;
            _Modifiers = modifiers;
            _Key = key;
            _Command = command;
            _Arguments = arguments;
        }

        public HotKeyData(IntPtr hand, int id, KeyModifiers modifiers, System.Windows.Forms.Keys key, string command)
            : this(hand, id, modifiers, key, command, null) { }


        public HotKeyData(IntPtr hand, int id, KeyModifiers modifiers, System.Windows.Forms.Keys key, Action action)
        {
            _Hand = hand;
            _ID = id;
            _Modifiers = modifiers;
            _Key = key;
            _Action = action;
        }

        #endregion

    }


    /// <summary>
    /// 为热键提供方法.
    /// </summary>
    /// <param name="HotKeyID"></param>
    public delegate void HotkeyEventHandler(int HotKeyID);
    /// <summary>
    /// 向系统注册热键
    /// </summary>
    public class GlobalHotkey : System.Windows.Forms.IMessageFilter
    {
        System.Collections.Hashtable keyIDs = new System.Collections.Hashtable();
        IntPtr hWnd;
        /// <summary> 
        /// 当系统热键被按下时发生
        /// </summary> 
        public event HotkeyEventHandler OnHotkey;
        /// <summary>
        /// 按键的枚举.
        /// </summary>
        public enum KeyFlags
        {
            None = 0x0,
            Alt = 0x1,
            Control = 0x2,
            Shift = 0x4,
            Win = 0x8
        }

        [DllImport("user32.dll")]
        public static extern UInt32 RegisterHotKey(IntPtr hWnd, UInt32 id,
        UInt32 fsModifiers, UInt32 vk);
        [DllImport("user32.dll")]
        public static extern UInt32 UnregisterHotKey(IntPtr hWnd, UInt32 id);
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(String lpString);
        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);

        /// <summary> 
        /// 构造函数 Adds this instance to the MessageFilters so that this class can raise Hotkey events 
        /// </summary> 
        /// <param name="hWnd">句柄</param> 
        public GlobalHotkey(IntPtr hWnd)
        {
            this.hWnd = hWnd;
            System.Windows.Forms.Application.AddMessageFilter(this);
        }
        /// <summary> 
        /// 注册系统热键
        /// </summary> 
        /// <param name="hWnd">句柄</param> 
        /// <param name="Key">热键</param> 
        /// <returns>返回注册热键的ID号 Use this to know which hotkey was pressed.</returns> 
        public int RegisterHotkey(System.Windows.Forms.Keys Key, KeyFlags keyflags)
        {
            UInt32 hotkeyid = GlobalAddAtom(System.Guid.NewGuid().ToString());
            RegisterHotKey((IntPtr)hWnd, hotkeyid, (UInt32)keyflags, (UInt32)Key);
            keyIDs.Add(hotkeyid, hotkeyid);
            return (int)hotkeyid;
        }
        /// <summary> 
        /// 删除全局键
        /// </summary> 
        public void UnRegisterHotkeys()
        {
            System.Windows.Forms.Application.RemoveMessageFilter(this);
            foreach (UInt32 key in keyIDs.Values)
            {
                UnregisterHotKey(hWnd, key);
                GlobalDeleteAtom(key);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x312) /*WM_HOTKEY*/
            {
                if (OnHotkey != null)
                {
                    foreach (UInt32 key in keyIDs.Values)
                    {
                        if ((UInt32)m.WParam == key)
                        {
                            OnHotkey((int)m.WParam);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
