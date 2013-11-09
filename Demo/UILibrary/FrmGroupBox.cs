/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/5 16:44:33
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
using CRC.Controls;

namespace UILibrary
{
    public partial class FrmGroupBox : Form
    {
        public FrmGroupBox()
        {
            InitializeComponent();
            init();
            groupListbox1.CellItemClick += new EventHandler<DataEventArgs<GroupCellItem>>(groupListbox1_CellItemClick);
        }

        void groupListbox1_CellItemClick(object sender, DataEventArgs<GroupCellItem> e)
        {
            
            //throw new NotImplementedException();
        }

        void init()
        {
            GroupItem item;
            for (int i = 0; i < 5; i++)
            {
                item = new GroupItem("Item " + i);
                AddSubItem(item);
                if (i % 2 == 1) item.IsOpen = true;
                groupListbox1.Items.Add(item);
            }

        }

        void AddSubItem(GroupItem item)
        {
            GroupSubItem sitem;
            for (int i = 0; i < 5; i++)
            {
                sitem = new GroupSubItem();
                sitem.Text = "Sub Item " + i;
                if (i % 2 == 0) sitem.IsOpen = true;
                AddCellItem(sitem);
                item.SubItems.Add(sitem);
            }
        }

        void AddCellItem(GroupSubItem item)
        {
            GroupCellItem sitem;
            for (int i = 0; i < 5; i++)
            {
                sitem = new GroupCellItem();
                sitem.Text = "Cell Item " + i;


                item.SubItems.Add(sitem);
            }
        }


        private void GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Client Rectangle:{0}\r\n\r\n",  groupListbox1.ClientRectangle.ToString());
            sb.AppendFormat("Client Size :{0}\r\n\r\n",  groupListbox1.ClientSize.ToString());
            sb.AppendFormat("Display Rectangle:{0}\r\n\r\n",  groupListbox1.DisplayRectangle.ToString());

            sb.AppendFormat("Location {0}\r\n\r\n",  groupListbox1.Location.ToString());
            sb.AppendFormat("Preferred Size {0}\r\n\r\n",  groupListbox1.PreferredSize.ToString());

            //sb.AppendFormat("Region {0}",  groupListbox1.Region.ToString());
            sb.AppendFormat("Size {0}\r\n\r\n",  groupListbox1.Size.ToString());

            textBox1.Text = sb.ToString();
        }

        private void groupListbox1_MouseMove(object sender, MouseEventArgs e)
        {
            GetInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = int.Parse(textBox2.Text);
            groupListbox1.SetScroll(i);
        }

    }
}
