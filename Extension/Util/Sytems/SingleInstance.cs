/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 9:59:04
 * 描述说明：进程单一实例代理类.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

namespace CRC.Util
{
    /// <summary>
    /// 进程的单一实例代理类.
    /// <para></para>
    /// </summary>
    public class SingleInstance
    {

        #region 进程的单一实例



        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        static int SW_SHOWNOMAL = 1;

        /// <summary>
        /// 将指定的进程 显示到最前端.
        /// </summary>
        /// <param name="instance">进程</param>
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWNOMAL);//显示
            SetForegroundWindow(instance.MainWindowHandle);//当到最前端
        }
        /// <summary>
        /// 获取运行实例.
        /// </summary>
        /// <returns></returns>
        private static Process RuningInstance()
        {
            Process currentProcess = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != currentProcess.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == currentProcess.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 单一实例运行入口.
        /// </summary>
        /// <param name="runName">单一实例软件的名称.</param>
        /// <param name="action">没有实例运行时,执行的任务.</param>
        public static void Run(string runName,Action action)
        {
            bool canCreateNew;
            Mutex mutexLock = new Mutex(true, "Global\\"+runName, out canCreateNew);
            if (canCreateNew)//没有实例在运行.
            {
                action();
                mutexLock.ReleaseMutex();
            }
            else
            {
                //已经有实例在运行,那么调用该窗体,前端显示.
                Process process = RuningInstance();
                HandleRunningInstance(process);
            }
        }
        /// <summary>
        /// 单一实例运行入口.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="runName">单一实例软件的名称.</param>
        /// <param name="action">没有实例运行时,执行的任务.</param>
        /// <param name="obj">参数</param>
        public static void Run<T>(string runName, Action<T> action,T obj)
        {
            bool canCreateNew;
            Mutex mutexLock = new Mutex(true, "Global\\" + runName, out canCreateNew);
            if (canCreateNew)
            {
                action(obj);
                mutexLock.ReleaseMutex();
            }
            else
            {
                Process process = RuningInstance();
                HandleRunningInstance(process);
            }
        }

        #endregion
    }
}
