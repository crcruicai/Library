
//#define Release

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CWebQQ.Froms;
using QQMessage;
using Microsoft.Win32;
namespace CWebQQ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if Release
            if (!ReadKey())
            {
                MessageBox.Show("对不起试用版已经过期!请尽快付费!");
                return;
            }
            FrmLogon logon = new FrmLogon();
            if (logon.ShowDialog() == DialogResult.OK)
            {
                logon.Close();
                Application.Run(new FrmMain());
            }
#else
            Application.Run(new FrmMain());
            //Application.Run(new FrmTest());
#endif

            
        }


        static bool ReadKey()
        {

            try
            {
                object obj;
                try
                {
                    RegistryKey runKey = Registry.LocalMachine.OpenSubKey(@"Software\QQ", true);
                    obj = runKey.GetValue("WebQQSecond2");
                    object count = runKey.GetValue("Count3");
                    int index;
                    if (int.TryParse(count.ToString(), out index))
                    {
                        index++;
                        runKey.SetValue("Count3", index);
                    }
                    runKey.Close();
                    if (index > 10) return false;

                   
                }
                catch (Exception)
                {

                    WriteKey();
                    return true;
                }
               
                if (obj!=null)
                {
                    DateTime t = DateTime.Parse(obj.ToString());
                    t = t.AddDays(3);
                    if (t > DateTime.Now)
                    {
                        return true;
                    }  
                }
                return false;
               
            }
            catch (Exception )
            {
                return false;
            }
        }

        static void WriteKey()
        {
            try
            {
                RegistryKey local = Registry.LocalMachine;
                RegistryKey runKey = local.CreateSubKey(@"Software\QQ");
                if (runKey != null)
                {
                    runKey.SetValue("WebQQSecond2", DateTime.Now.ToString());
                    runKey.SetValue("Count3", 1);
                    runKey.Close();
                }

            }
            catch (Exception e)
            {
                
                
            }
           
        }

    }
}
