using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CRC.Froms;
namespace UILibrary
{
    /// <summary>
    /// 消息通知窗体.
    /// <para>类似于QQ消息通知窗体.在屏幕的右下角弹出.</para>
    /// </summary>
    public partial class FrmTaskbarNotifier : Form
    {
        PlaceManager manager;
        public FrmTaskbarNotifier()
        {
            InitializeComponent();
            taskbarNotifier1 = NewMessageForm();
            manager = new PlaceManager(10);
        }

        void MessageHide(object sender, PlaceIndexEventArgs e)
        {
            manager.FreePlaceIndex(e.Index);
        }

        private void DisplayMessage(string strTitle,string strContent)
        {
            if (taskbarNotifier1.TaskbarState == TaskbarNotifier.TaskbarStates.hidden)//没显示
            {
                taskbarNotifier1.Show(strTitle, strContent, manager.GetPlaceIndex());
            }
            else
            {
                //New一个新窗口
                NewMessageForm().Show(strTitle, strContent,manager.GetPlaceIndex());
            }
        }
        private TaskbarNotifier NewMessageForm()
        {
            TaskbarNotifier notifier=new TaskbarNotifier ();
            notifier.SetBackgroundBitmap(Properties.Resources.skin, Color.FromArgb(255, 0, 255));
            notifier.SetCloseBitmap(Properties.Resources.close, Color.FromArgb(255, 0, 255), new Point(127, 8));
            notifier.TitleRectangle = new Rectangle(40, 9, 70, 25);
            notifier.ContentRectangle = new Rectangle(8, 41, 133, 68);
            notifier.TitleClick += new EventHandler(TitleClick);
            notifier.ContentClick += new EventHandler(ContentClick);
            notifier.CloseClick += new EventHandler(CloseClick);
            notifier.MessageHide += new IndexEventHandler(MessageHide);
            notifier.CloseClickable = true;//关闭按钮是否可单击
            notifier.TitleClickable = true;//标题是否可单击
            notifier.ContentClickable = true;//内容是否可单击
            notifier.EnableSelectionRectangle = true;//是否显示内容的框
            notifier.KeepVisibleOnMousOver = true;	// 鼠标停留时是否一直保持可见
            notifier.ReShowOnMouseOver = true;//当消隐时鼠标划过是否重现
            notifier.TimeToShow = 500;
            notifier.TimeToStay = 3000;
            notifier.TimeToHide = 500;
            return notifier;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DisplayMessage("提示", "有新的消息，请注意查收");
        }

        void CloseClick(object obj, EventArgs ea)
        {
            MessageBox.Show("Closed was Clicked");
        }

        void TitleClick(object obj, EventArgs ea)
        {
            MessageBox.Show("Title was Clicked");
        }

        void ContentClick(object obj, EventArgs ea)
        {
            MessageBox.Show("Content was Clicked");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte b = 162;

            b ^= (byte)(1 << 6);
            
        }
    }
}
