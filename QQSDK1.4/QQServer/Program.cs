/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/7 8:15:12
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using QQMessage;
using System.Runtime.InteropServices;

namespace QQServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //禁用 关闭按钮.
                DisableCloseButton(Console.Title);
                Console.WriteLine("正在启动服务...");
                AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit); 
                Start();              
            }
            catch (Exception e)
            {

                Loger.WriteLog(e);
            }
        }

        #region 禁用控制台关闭按钮
        /// <summary>
        /// 禁用关闭按钮
        /// </summary>
        /// <param name="consoleName">控制台名字</param>
        public static void DisableCloseButton(string title)
        {
            IntPtr windowHandle = FindWindow(null, title);
            IntPtr closeMenu = GetSystemMenu(windowHandle, IntPtr.Zero);
            uint SC_CLOSE = 0xF060;
            RemoveMenu(closeMenu, SC_CLOSE, 0x0);
        }

        #region API
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        static extern IntPtr GetSystemMenu(IntPtr hWnd, IntPtr bRevert);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        static extern IntPtr RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

        #endregion

        #endregion

        /// <summary>
        /// 应用程序退出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            UserManger.SaveSettings();
        }

        /// <summary>
        /// 启动服务.
        /// </summary>
        static void Start()
        {
            try
            {
                using (ServiceHost host = new ServiceHost(typeof(UserManger), new Uri[] { new Uri("http://localhost/Service/QQManger.svc") }))
                {
                    //开启服务
                    BasicHttpBinding _BasicBingding = new BasicHttpBinding("BasicHttpBinding");
                    host.AddServiceEndpoint(typeof(IService), _BasicBingding, "Calculator");
                    try
                    {
                        host.Open();
                    }
                    catch (System.ServiceModel.CommunicationObjectFaultedException o)
                    {
                        Console.WriteLine(o.Message);
                        Loger.WriteLog(o);
                    }
                    catch (System.ServiceModel.CommunicationException c)
                    {
                        Loger.WriteLog(c);
                    }
                    catch (Exception e)
                    {

                        Loger.WriteLog(e);
                    }

                    Console.WriteLine("服务正在运行");
                    //TestInterface t = new TestInterface();
                    //t.Test();
                    Console.WriteLine("输入命令 exit 退出.");
                    while (Console.ReadLine() != "exit")
                    {
                        Console.WriteLine("输入命令 exit 退出.");
                    }
                   
                    host.Close();
                }
            }
            catch (Exception e)
            {
                Loger.WriteLog(e);
            }
            finally
            {
                //UserManger.SaveSettings();
            }
            
           
           
        }

    }
}
