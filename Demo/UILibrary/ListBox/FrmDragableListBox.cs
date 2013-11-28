/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/27 16:47:48
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

namespace UILibrary
{
    public partial class FrmDragableListBox : Form
    {
        public FrmDragableListBox()
        {
            InitializeComponent();
            Init();
        }


        private void Init()
        {
            for (int i = 0; i < 15; i++)
            {
                dragableListBox1.Items.Add("Item " + i);
            }


        }
    }
}
