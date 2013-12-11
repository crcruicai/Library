/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 23:23:02
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQRobot
{
    /// <summary>
    /// 跨线程控件调用扩展.
    /// </summary>
    public static class InvokeExtension
    {
        #region 异步调用
        /// <summary>
        /// 在创建控件的基础句柄所在线程上异步执行指定委托。
        /// </summary>
        /// <param name="ctl">指定的控件</param>
        /// <param name="doit">处理的委托.</param>
        public static void QueueInvoke(this System.Windows.Forms.Control ctl, Action doit)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            ctl.BeginInvoke(doit);
        }

        /// <summary>
        /// 在创建控件的基础句柄所在线程上，用指定的参数异步执行指定委托。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctl"></param>
        /// <param name="doit"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public static void QueueInvoke<T, S>(this System.Windows.Forms.Control ctl, Action<T, S> doit, T arg1, S arg2)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            ctl.BeginInvoke(doit, arg1, arg2);
        }

        /// <summary>
        /// 在创建控件的基础句柄所在线程上，用指定的参数异步执行指定委托。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctl"></param>
        /// <param name="doit"></param>
        /// <param name="aras"></param>
        public static void QueueInvoke<T>(this System.Windows.Forms.Control ctl, Action<T> doit, T aras)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            ctl.BeginInvoke(doit, aras);
        }

        #endregion

        #region 执行委托
        /// <summary>
        /// 在拥有此控件的基础窗口句柄的线程上执行指定的委托。
        /// </summary>
        /// <param name="ctl"></param>
        /// <param name="doit"></param>
        public static void InvokeIfNeeded(this System.Windows.Forms.Control ctl, Action doit)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(doit);
            }
            else
            {
                doit();
            }
        }

        /// <summary>
        /// 在拥有控件的基础窗口句柄的线程上，用指定的参数列表执行指定委托。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctl"></param>
        /// <param name="doit"></param>
        /// <param name="args"></param>
        public static void InvokeIfNeeded<T>(this System.Windows.Forms.Control ctl, Action<T> doit, T args)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(doit, args);
            }
            else
            {
                doit(args);
            }
        }
        /// <summary>
        /// 在拥有控件的基础窗口句柄的线程上，用指定的参数列表执行指定委托。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ctl"></param>
        /// <param name="doit"></param>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public static void InvokeIfNeeded<T, S>(this System.Windows.Forms.Control ctl, Action<T, S> doit, T arg1, S arg2)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            if (ctl.InvokeRequired)
            {
                ctl.Invoke(doit, arg1, arg2);
            }
            else
            {
                doit(arg1, arg2);
            }
        }

        #endregion
    }
}
