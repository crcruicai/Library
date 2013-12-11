using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQSDK.Net;
using QQSDK.Json;
using System.Diagnostics;
using QQRobot.Util;
namespace QQRobot
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
            AutoSpeaker.LoadMap();
            pic();
            pictureBox2.Image = qq2.GetLoginVCImage("398117542");

            _Seg = new Segment();
            _Seg.InitWordDics();
            _Seg.EnablePrefix = true;
            _Seg.Separator = " ";
        }

        Segment _Seg;
        WebQQ qq = new WebQQ();

        WebQQ qq2 = new WebQQ();
        private void pic()
        {


            pictureBox1.Image = qq.GetLoginVCImage("496063973");

            


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (qq.Login("496063973", "crc392759", textBox1.Text) == LoginResult.LoginSucceed)
            {
                MessageBox.Show("登录成功");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //UserFriend f = JSON.parse<UserFriend>(textBox2.Text);

            //textBox3.Text = JSON.stringify(f);

            //UserFriend f = JSON.parse<UserFriend>(textBox2.Text);
            //textBox3.Text = JSON.stringify(f);

            parse(textBox2.Text);

        }


        private MessageValue parse(string text)
        {
            text = text.Replace("{", "");
            text = text.Replace("]", "");
            text = text.Replace("[", "");
            text = text.Replace("}", "");
            text = text.Replace("\"", "");
            //text = text.Replace("]", "");
            string[] array = text.Split(',');
            string item = string.Empty;
            MessageValue value = new MessageValue();
            for (int i = 0; i < array.Length; i++)
            {
                item = array[i];
                string[] a = item.Split(':');
                if (a.Length > 1)
                {
                    switch (a[0])
                    {
                        case "result":
                            if (a.Length > 2)
                                value.PollType = a[2];
                            break;
                        case "value":
                            if (a.Length > 2)
                                value.MsgID = long.Parse(a[2]);
                            break;
                        case "from_uin":
                            value.FromUin = long.Parse(a[1]);
                            break;
                        case "msg_id":
                            value.MsgID = long.Parse(a[1]);
                            break;
                        case "msg_id2":
                            value.MsgID2 = int.Parse(a[1]);
                            break;
                        case "msg_type":
                            value.MsgType = int.Parse(a[1]);
                            break;
                        case "reply_ip":
                            value.ReplyIP = int.Parse(a[1]);
                            break;
                        case "time":
                            value.Time = int.Parse(a[1]);
                            break;
                        case "to_uin":
                            value.ToUin = long.Parse(a[1]);
                            break;
                        case "color":
                            value.FontColor = GetColor(a[1]);
                            break;
                        case "name":
                            value.Name = Encode.DeUnicode(a[1]);
                            break;
                        case "size":
                            value.FontSize = int.Parse(a[1]);
                            break;
                        case "style":
                            value.FontStyle = string.Format("{0}{1}{2}", a[1], array[i + 1], array[i + 2]);
                            i = i + 2;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    //Debug.WriteLine(item);
                    if (item.IndexOf("\\u") > -1)
                    {
                        value.MsgContent = Encode.DeUnicode(item);
                        value.MsgContent = value.MsgContent.Replace("r", "\r\n");
                        Debug.WriteLine(value.MsgContent);
                    }
                    else
                    {
                        value.MsgContent = item;
                    }
                }

            }
            return value;


        }

        /// <summary>
        /// 将文本转换为颜色Color
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Color GetColor(string text)
        {
            byte t;
            int r=0, g=0, b=0;
            string s;
            if (text.Length < 6) return Color.Black;
            s = text.Substring(0, 2);
            if(byte.TryParse (s,out t))
            {
                r = t;
            }
            s = text.Substring(2, 2);
            if (byte.TryParse(s, out t))
            {
                g = t;
            }
            s = text.Substring(4, 2);
            if (byte.TryParse(s, out t))
            {
                b = t;
            }
            return Color.FromArgb(r, g, b);


        }
        AutoSpeaker _Speaker = new AutoSpeaker();
        private void button4_Click(object sender, EventArgs e)
        {
            _Speaker.RobotName = "小天";
            _Speaker.Speaker = "亲们";
            string text =_Seg.SegmentText(textBox2.Text, false);
            textBox3.Text=_Speaker.Speak(text," ");
            
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pic();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserFriend fm = qq.GetUserFriends();
            if (fm!=null && fm.Result !=null  )
            {
                if(fm.Result.Info != null)
                {

                    foreach (var item in fm.Result.Info)
                    {
                        Debug.WriteLine(item.Uin);
                        Debug.WriteLine(item.Nick);
                    }
                }

               

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            qq.ListenMessage();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            qq.SendMessage(textBox4.Text, "成功了吗?", new Font("宋体", 10F), Color.Red);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string text = qq.GetQQNumber(textBox4.Text);

            Debug.WriteLine(text);
            Debug.WriteLine("获取QQ帐号:");
            Debug.WriteLine(qq.GetUserLnick());
            Debug.WriteLine("获取好友信息.");
            Debug.WriteLine(qq.GetFriendInfo(textBox4.Text));

            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (qq2.Login("398117542", "crcruicai392759", textBox5.Text) == LoginResult.LoginSucceed)
            {
                MessageBox.Show("登录成功");
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = qq2.GetLoginVCImage("398117542");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            qq2.SendMessage(textBox6.Text, "成功了吗?", new Font("宋体", 10F), Color.Red);

        }

        private void button12_Click(object sender, EventArgs e)
        {
            UserFriend fm = qq2.GetUserFriends();
            if (fm != null && fm.Result != null)
            {
                if (fm.Result.Info != null)
                {

                    foreach (var item in fm.Result.Info)
                    {
                        Debug.WriteLine(item.Uin);
                        Debug.WriteLine(item.Nick);
                    }
                }



            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("获取QQ签名:");
            Debug.WriteLine(qq2.GetUserLnick());
        }


    }
}
