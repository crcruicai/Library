/*********************************************************
* 开发人员：TopC
* 创建时间：2013/12/25 17:17:48
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

namespace CWebQQ
{
    public partial class FrmInputCode : Form
    {
        public FrmInputCode(Image image)
        {
            InitializeComponent();
            pictureBox1.Image = image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Text = textBox1.Text.Trim();
            if (_Text.Length == 4)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private string _Text;
        public string InputText { get { return _Text; } }


    }
}
