using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRC.Controls ;
namespace UILibrary
{
    public partial class FrmGroupListBox : Form
    {
        public FrmGroupListBox()
        {
            InitializeComponent();
            groupListBox1.ClickSubItem += new EventHandler<GroupListBoxEventArgs>(groupListBox1_ClickSubItem);
            groupListBox1.DoubleClickSubItem += new EventHandler<GroupListBoxEventArgs>(groupListBox1_DoubleClickSubItem);
            groupListBox1.MouseOnSubItem += new EventHandler<GroupListBoxEventArgs>(groupListBox1_MouseOnSubItem);
            appLoad();

        }

        void groupListBox1_MouseOnSubItem(object sender, GroupListBoxEventArgs e)
        {
            this.Text = e.MouseOnsubItem.Text;
        }

        void groupListBox1_DoubleClickSubItem(object sender, GroupListBoxEventArgs e)
        {
            MessageBox.Show(e.SelectSubItem .Text);
        }

        void groupListBox1_ClickSubItem(object sender, GroupListBoxEventArgs e)
        {
            this.Text = e.SelectSubItem.Text;
        }


        private void appLoad()
        {
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                TextGroupItem item = new TextGroupItem("Group " + i);
                for (int j = 0; j < 10; j++)
                {
                    TextSubItem subItem = new TextSubItem("NicName\r\n1254563652\r\n我打击代理商尅打开来了空间框架爱哦看啦埃及罚款决定了\r\nioewjerlja发动机啊阿的江介绍了的积分来得快及io585214522554");

                    item.SubItems.Add(subItem);
                }
                item.SubItems.Sort();
                groupListBox1.Items.Add(item);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            
        }
    }
}
