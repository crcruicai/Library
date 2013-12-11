using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQSDK.Net;
namespace QQParser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ulong num = ulong.Parse(textBoxNumber.Text);
            textBoxResult.Text = Tool.Hash(num, textBoxPTWebQQ.Text);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = Tool.GetHash(textBoxNumber.Text, textBoxPTWebQQ.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ulong num = ulong.Parse(textBoxNumber.Text);
            textBoxResult.Text = Tool.Hash2(num, textBoxPTWebQQ.Text);
        }
    }
}
