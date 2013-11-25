/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/22 20:32:48
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
using System.Text;
using System.Windows.Forms;

namespace ListViewCollectionDemo
{
    public partial class FrmListViewFilter : Form
    {
        public FrmListViewFilter()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            this.toolTip1.SetToolTip(this.button1, "Create some items to test");
            this.toolTip1.SetToolTip(this.button2, "Toggle filters mode");
            this.toolTip1.SetToolTip(this.button3, "Get the filter data of the sorted column");
            this.toolTip1.SetToolTip(this.button4, "Set the filter for the sorted column");
            this.toolTip1.SetToolTip(this.button5, "Set default format and alignment");
            this.toolTip1.SetToolTip(this.textBox1, "Text from/for the sorted column filter");

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            listViewFilter1.BeginUpdate();
            listViewFilter1.Items.Clear();

            ListViewItem i;

            i = listViewFilter1.Items.Add("AB");
            i.SubItems.Add("5.3");
            i.SubItems.Add("Jan 29, 1958");

            i = listViewFilter1.Items.Add("abc");
            i.SubItems.Add("2");
            i.SubItems.Add("April 15, 2003");

            i = listViewFilter1.Items.Add("BCDE");
            i.SubItems.Add("15.25");
            i.SubItems.Add("Dec 31, 1999");

            i = listViewFilter1.Items.Add("CDE");
            i.SubItems.Add("12");
            i.SubItems.Add("Mar 15, 0012");


            listViewFilter1.EndUpdate();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            listViewFilter1.Filtered = !listViewFilter1.Filtered;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            textBox1.Text = listViewFilter1.Header.Filter[listViewFilter1.SortColumn];
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            listViewFilter1.Header.Filter[listViewFilter1.SortColumn] = textBox1.Text;
        }

        private void button5_Click(object sender, System.EventArgs e)
        {

            listViewFilter1.SortColumn = 0;
            listViewFilter1.SortOrder = true;
            listViewFilter1.Filtered = true;

            listViewFilter1.Header.DataType[0] = CRC.Controls.LVFDataType.String;
            listViewFilter1.Header.DataType[1] = CRC.Controls.LVFDataType.Number;
            listViewFilter1.Header.DataType[2] = CRC.Controls.LVFDataType.Date;

            listViewFilter1.Header.Alignment[0] = HorizontalAlignment.Left;
            listViewFilter1.Header.Alignment[1] = HorizontalAlignment.Right;
            listViewFilter1.Header.Alignment[2] = HorizontalAlignment.Center;

            listViewFilter1.Header.Filter[0] = "";
            listViewFilter1.Header.Filter[1] = "";
            listViewFilter1.Header.Filter[2] = "";

        }
    }
}
