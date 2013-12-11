/*********************************************************
* 开发人员：TopC
* 创建时间：2013/12/11 14:45:40
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

namespace CWebQQ.Froms
{
    public partial class FrmStatus : Form
    {
        private ComboBox _Box;


        public FrmStatus()
        {
            InitializeComponent();
            editListView1.BeginEditCell += editListView1_BeginEditCell;
        }

        void editListView1_BeginEditCell(object sender, CRC.Controls.ListCellEditEventArgs e)
        {
            
            ColumnHeader header = editListView1.Columns[e.SubItemIndex];
            if (header.Text == "状态")
            {
                if (_Box == null)
                {
                    _Box = new ComboBox();
                    _Box.Items.AddRange(new object[] { "在线", "隐身", "忙碌", "Q我吧", "请勿打扰" });
                    _Box.Leave += _Box_Leave;
                    editListView1.Controls.Add(_Box);
                }
                _Box.Size = e.SelectSubItem.Bounds.Size;
                _Box.Location = e.SelectSubItem.Bounds.Location;
                _Box.Show();
                _Box.Focus();

            }
        }

        void _Box_Leave(object sender, EventArgs e)
        {
           
        }
    }
}
