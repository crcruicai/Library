/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/4 21:15:44
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
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
    public partial class FrmChatGroupBox : Form
    {
        public FrmChatGroupBox()
        {
            InitializeComponent();
            Init();
        }


        void Init()
        {

            ChatItem item;
            for (int i = 0; i < 5; i++)
            {
                item = new ChatItem()
                {
                    Text = "Item " + i,
                };
                AddSubItem(item);

                if (i % 2 == 1) item.IsOpen = true;
                chatGroupBox1.Items.Add(item);

            }

        }


        void AddSubItem(ChatItem item)
        {
            ChatSubItem subitem;
            for (int i = 0; i < 5; i++)
            {
                subitem = new ChatSubItem()
                {
                    Text ="Sub Item "+i,
                };

                if (i % 2 == 0) subitem.IsOpen = true;
                AddCellItem(subitem);
                item.Items.Add(subitem);

                
            }



        }


        void AddCellItem(ChatSubItem item)
        {
            ChatCellItem cellitem;
            for (int i = 0; i < 5; i++)
            {
                cellitem = new ChatCellItem()
                {
                    Text = "Cell item " + i,
                };

                item.SubItems.Add(cellitem);

            }

        }


    }
}
