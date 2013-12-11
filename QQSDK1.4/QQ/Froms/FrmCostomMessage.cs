using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CRC.Controls;
using CWebQQ.Data;
namespace CWebQQ
{
    public partial class FrmCostomMessage : Form
    {

        TreeNode CurrentNode;

        public FrmCostomMessage()
        {
            InitializeComponent();
            
          
        }

        private Group MessageGroup=new Group ();

        public void LoadGroup(Group ms)
        {
            if (ms == null) return;
            MessageGroup = ms;
            foreach (var item in ms)
            {
                LoadItem(item.GroupName, item.GroupList);
                comGroupName.Items.Add(item.GroupName);
            }

        }

        private void LoadItem(string groupname,List<string> list)
        {
            TreeNode fnode = new TreeNode(groupname);
            int index = 0;
            foreach (var item in list)
            {
                TreeNode node = new TreeNode(item);
                node.Tag = index;
                index++;
                fnode.Nodes.Add(node);
            }
            treeView1.Nodes.Add(fnode);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            TreeNode node = FindFather(comGroupName.Text);
            if (node == null)
            {
                node = new TreeNode(comGroupName.Text);
                treeView1.Nodes.Add(node);
                comGroupName.Items.Add(comGroupName.Text);
            }
            
            TreeNode snode = new TreeNode(textMessage.Text);
            int index = node.Nodes.Count;

            //更新 
            GroupItem sitem = MessageGroup.Find((item) => item.GroupName == node.Text);
            if(sitem==null) sitem = new GroupItem() { GroupName = comGroupName.Text };
            sitem.GroupList.Add(textMessage.Text);
            node.Nodes.Add(snode);
            
          
           
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (CurrentNode != null)
            {
                CurrentNode.Text = textMessage.Text;
                //
                //int index = (int)CurrentNode.Tag;
                //GroupItem sitem = MessageGroup.Find((item) => item.GroupName == CurrentNode.Parent.Text);
                //if (sitem != null)
                //{
                //    sitem.GroupList[index] = textMessage.Text;
                //}
            }
        }


        private TreeNode FindFather(string text)
        {
            foreach (TreeNode  item in treeView1.Nodes)
            {
                if (item.Text == text)
                {
                    return item;
                }
            }
            return null;

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
                CurrentNode = e.Node;

        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (CurrentNode != null)
            {
                treeView1.Nodes.Remove(CurrentNode);
                CurrentNode = null;
            }
        }


        public Group GetGroup()
        {
            Group g = new Group();
            foreach (TreeNode  item in treeView1 .Nodes )
            {

                GroupItem gitem = new GroupItem() { GroupName = item.Text };
                foreach (TreeNode  sitem in item.Nodes )
                {
                    gitem.GroupList.Add(sitem.Text);
                }
                g.Add(gitem);
            }
            return g;
        }

        private void FrmCostomMessage_Load(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\MS\\GroupMessage.db";
            Group g = SystemHelper.Load<Group>(path);
            if (g != null)
            {
                LoadGroup(g);
            }
        }

        private void FrmCostomMessage_FormClosed(object sender, FormClosedEventArgs e)
        {
            string path = Application.StartupPath + "\\MS\\GroupMessage.db";
            SystemHelper.Save<Group>(path, GetGroup());
        }


    }
}
