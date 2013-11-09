#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/22 9:46:59
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
using System.Text.RegularExpressions;

namespace UILibrary
{
    public partial class FrmEditListView : Form
    {
        public FrmEditListView()
        {
            InitializeComponent();
             listViewEdit1.BeginEditCell += new EventHandler<ListCellEditEventArgs>(listViewEdit1_BeginEditCell);
            Analyse(_TestText);
        }

           string _TestText =
         "{<1><目测电路板><OK>}{<1><15V 测试= 13.32V><NO>}{<1><5V测试><NO>}";


       

        void listViewEdit1_BeginEditCell(object sender, ListCellEditEventArgs e)
        {
            ColumnHeader header = listViewEdit1.Columns[e.SubItemIndex];
            if (header.Text == "结果")
            {
                if (_CheckBox == null)
                {
                    _CheckBox = new CheckBox();
                   
                    _CheckBox.Leave += new EventHandler(_CheckBox_Leave);
                    listViewEdit1.Controls.Add(_CheckBox);
                    //this.Controls.Add(_CheckBox);
                }
                _CheckBox.Size = e.SelectSubItem.Bounds.Size;
                _CheckBox.Location = e.SelectSubItem.Bounds.Location;
                _CheckBox.CheckAlign = ContentAlignment.MiddleCenter;
                _CheckBox.Visible = true;
                _CheckBox.Show();
                if (e.SelectSubItem.Text == "OK")
                {
                    _CheckBox.Checked = true;
                }
                else
                {
                    _CheckBox.Checked = false;
                }
                _CheckBox.Focus();

            }
                





        }

        void _CheckBox_Leave(object sender, EventArgs e)
        {
            _CheckBox.Visible = false;
        }

        CheckBox _CheckBox;

        /// <summary>
        /// 分析初检信息.
        /// </summary>
        /// <param name="str"></param>
        private void Analyse(string str)
        {
            MatchCollection mc = Regex.Matches(str, @"{(.*?)}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            if (mc.Count > 0)
            {
                string temp = null;
                int i = 0;
                foreach (Match item in mc)
                {
                    MatchCollection mk = Regex.Matches(item.Groups[0].Value, @"<(.*?)>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
                    temp = mk[1].Groups[0].Value;
                    temp = temp.Substring(1, temp.Length - 2);
                    ListViewItem listItem = new ListViewItem(i.ToString());
                    listItem.SubItems.Add(temp);
                    temp = mk[2].Groups[0].Value;
                    temp = temp.Substring(1, temp.Length - 2);
                    if (temp != "OK")
                        listItem.BackColor = Color.Red;
                    listItem.SubItems.Add(temp);
                    listViewEdit1.Items.Add(listItem);
                    i++;
                }
            }
            else
            {

            }

        }
    }
}
