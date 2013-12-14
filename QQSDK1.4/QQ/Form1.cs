using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQSDK.Json;
using QQSDK.Net;
using System.Diagnostics;
namespace CWebQQ
{
    public partial class Form1 : Form
    {
        WebQQ _QQ = new WebQQ();


        public Form1()
        {
            InitializeComponent();
            GetPic();
        }

        private void GetPic()
        {
            pictureBox1.Image = _QQ.GetLoginVCImage("398117542");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetPic();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_QQ.Login("398117542", "crcruicai392759", textBox1.Text) == LoginResult.LoginSucceed)
            {
                MessageBox.Show("登录成功");
                Debug.WriteLine(_QQ.GetGroupList());

                Debug.WriteLine("\r\n");
               
              

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(_QQ .GetGroupInfo (textBox1 .Text ));
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }



    }
}
