/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/21 14:33:38
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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCheckComboBox box = new FrmCheckComboBox();
            box.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmAutoDockManage box = new FrmAutoDockManage();
            box.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var box = new FrmTaskbarNotifier();
            box.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var box = new FrmChatListBox();
            box.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var box = new FrmColorListBox();
            box.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var box = new FrmDragableListBox();
            box.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var box = new  FrmGroupListBox ();
            box.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var box = new FrmMultiLineListBox () ;
            box.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
             var box = new FrmRadioListBox () ;
            box.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
             var box = new FrmSelectListSwap ();
            box.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var box = new FrmDbListView ();
            box.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var box = new FrmEditListView ();
            box.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var box = new FrmListViewEmbeddedControls ();
            box.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var box = new FrmGroupBox  ();
            box.Show();

        }

        private void button15_Click(object sender, EventArgs e)
        {
            var box = new FrmGrouper ();
            box.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var box = new FrmLayerTreeView ();
            box.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var box = new FrmAGauge ();
            box.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var box = new  FrmChatGroupBox ();
            box.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var box = new FrmInstruments ();
            box.Show();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            var box = new  FrmPictureViewer ();
            box.Show();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var box = new  FrmReadOnlyRichTextBox();
            box.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var box = new FrmSlidingScale();
            box.Show();
        }




    }
}
