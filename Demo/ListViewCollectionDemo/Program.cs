/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/22 17:37:46
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ListViewCollectionDemo
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
