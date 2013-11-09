#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/21 10:45:47
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

namespace UILibrary
{
    public partial class FrmLayerTreeView : Form
    {
        public FrmLayerTreeView()
        {
            InitializeComponent();

            for (int index = 0; index < 5; index++)
            {
                TreeNode node = new TreeNode(string.Format("根菜单项{0}", index));
                for (int innerIndex = 0; innerIndex < 3; innerIndex++)
                {
                    TreeNode subnode = new TreeNode(string.Format("子菜单项{0}", index));
                    subnode.Nodes.Add(string.Format("子-子菜单项{0}", innerIndex));
                    node.Nodes.Add(subnode);
                }
                m_LayerTree.Nodes.Add(node);
            }
        }
    }
}
