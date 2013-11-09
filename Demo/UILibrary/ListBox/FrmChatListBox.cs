#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/21 11:38:14
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRC.Controls;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace UILibrary
{
    public partial class FrmChatListBox : Form
    {
        public FrmChatListBox()
        {
            InitializeComponent();
            chatListBox1.ContextSubItem += new ChatListBox.ChatListEventHandler(chatListBox1_ContextSubItem);
            contextMenuStrip1.Opening += new CancelEventHandler(contextMenuStrip1_Opening);
            Init();
        }

        void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (!_IsOpen)
            {
                e.Cancel = true;
            }
        }

        private bool _IsOpen = false;
        void chatListBox1_ContextSubItem(object sender, ChatListEventArgs e)
        {
            _IsOpen = true;
            contextMenuStrip1.Show();
            _IsOpen = false;
        }


        private void GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Client Rectangle:{0}\r\n\r\n", chatListBox1.ClientRectangle.ToString());
            sb.AppendFormat("Client Size :{0}\r\n\r\n", chatListBox1.ClientSize.ToString());
            sb.AppendFormat("Display Rectangle:{0}\r\n\r\n", chatListBox1.DisplayRectangle.ToString());

            sb.AppendFormat("Location {0}\r\n\r\n", chatListBox1.Location.ToString());
            sb.AppendFormat("Preferred Size {0}\r\n\r\n", chatListBox1.PreferredSize.ToString());

            //sb.AppendFormat("Region {0}", chatListBox1.Region.ToString());
            sb.AppendFormat("Size {0}\r\n\r\n", chatListBox1.Size.ToString());

            textBox2.Text = sb.ToString();
        }


        private void Init()
        {

            chatListBox1.Items.Clear();
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                ChatListItem item = new ChatListItem("Group " + i);
                for (int j = 0; j < 10; j++)
                {
                    ChatListSubItem subItem = new ChatListSubItem("NicName" + j, "DisplayName" + j, "Personal Message...!");
                    subItem.ID = i * 10 + j;
                    subItem.HeadImage = null;
                    subItem.Status = (ChatListSubItem.UserStatus)(j % 6);
                    item.SubItems.Add(subItem);
                }
                item.SubItems.Sort();
                chatListBox1.Items.Add(item);
            }
            ChatListItem itema = new ChatListItem("TEST");
            for (int i = 0; i < 5; i++)
            {
                chatListBox1.Items.Add(itema);
            }
            chatListBox1.Items.Remove(itema);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInfo();
            ChatListSubItem[] items =
            chatListBox1.GetSubItemsById(int.Parse (textBox1 .Text));
         
            if (items != null && items.Length > 0)
            {
                ChatListSubItem item = items[0];
                chatListBox1.SelectSubItem = item;
                //item.OwnerListItem.IsOpen = true;
                //chatListBox1.Srcoll(item.Bounds);
               
                label1.Text = string.Format("{0}  {1}", item.Bounds.Bottom.ToString(),item .OwnerListItem .Bounds  .Y .ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ChatListSubItem[] items = chatListBox1.GetSubItemsByNickName
                ((item) =>
                    {
                        return Regex.IsMatch(item, string.Format("{0}*", textBox1.Text));
                    });

            if (items != null && items.Length > 0)
            {
                ChatListSubItem item = items[0];
                chatListBox1.SelectSubItem = item;
                //item.OwnerListItem.IsOpen = true;
                //chatListBox1.Srcoll(item.Bounds);

                label1.Text = string.Format("{0}  {1}", item.Bounds.Bottom.ToString(), item.OwnerListItem.Bounds.Y.ToString());
            }
        }

    }
}
