/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 10:47:31
 * 描述说明：系统关机的静态类
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace CRC.Util
{
    /// <summary>
    /// 指示如何进行关闭计算机.
    /// </summary>
    public enum ShutdownOperation
    {
        /// <summary>
        /// 关闭电脑
        /// </summary>
        ShutDown,
        /// <summary>
        /// 重启电脑
        /// </summary>
        Reboot,
        /// <summary>
        /// 注销电脑
        /// </summary>
        Logoff,
        /// <summary>
        /// 休眠电脑
        /// </summary>
        Hibernate,
        /// <summary>
        /// 锁定电脑屏幕
        /// </summary>
        LockWorkStation,
        /// <summary>
        /// 挂起电脑
        /// </summary>
        Suspend
    };
    /// <summary>
    /// 系统关机的静态类
    /// </summary>
    public static class ShutDownComputer
    {
        [DllImport("user32.dll")]
        private static extern void LockWorkStation();
        /// <summary>
        /// 指定如何进行系统关机.
        /// </summary>
        /// <param name="so"></param>
        public static void ShutDown(ShutdownOperation so)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            switch (so)
            {
                case ShutdownOperation.ShutDown:
                    try
                    {
                        p.StandardInput.WriteLine("shutdown -f -s -t 0"); p.StandardInput.WriteLine("exit");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK); break;
                    }
                    break;

                case ShutdownOperation.Reboot:
                    try
                    {
                        p.StandardInput.WriteLine("shutdown -f -r -t 0"); p.StandardInput.WriteLine("exit");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK); break;
                    }
                    break;
                case ShutdownOperation.Logoff:
                    try
                    {
                        p.StandardInput.WriteLine("shutdown -l"); p.StandardInput.WriteLine("exit");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK); break;
                    }
                    break;
                case ShutdownOperation.Hibernate:
                    try
                    {
                        p.StandardInput.WriteLine("powercfg -h on"); Application.SetSuspendState(PowerState.Hibernate, true, true);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK); break;
                    }
                    break;
                case ShutdownOperation.Suspend:
                    try
                    {
                        Application.SetSuspendState(PowerState.Suspend, true, false);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK); break;
                    }
                    break;
                case ShutdownOperation.LockWorkStation:
                    try
                    {
                        LockWorkStation();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "警告", MessageBoxButtons.OK); break;
                    }
                    break;
            }
        }
    }
}
