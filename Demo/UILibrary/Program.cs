using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UILibrary
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new FrmGroupListbox());
            //Application.Run(new FrmChatListBox());
            //Application.Run(new FrmPictureViewer());
            //Application.Run(new FrmAutoDockManage());
            //Application.Run(new FrmListViewEmbeddedControls());
            //Application.Run(new FrmChatGroupBox());
            //Application.Run(new FrmGroupBox());
            Application.Run(new FrmMain());
            //Application.Run(new DemoApp.FrmCheckComboBox());

            //Application.Run(new FrmDragableListBox());
        }

      

    }
}
