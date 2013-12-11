/*********************************************************
* 开发人员：TopC
* 创建时间：2013/12/3 10:16:10
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
using QQSDK.Systems;
using QQSDK.Json;
using System.Diagnostics;

namespace CWebQQ
{
    public partial class FrmTest : Form
    {
        public FrmTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            JsonParson parson = new JsonParson();
            //string text = "{[123][1525][2541][5552][222][222][222][]555}";
            
            textBox1.Text = parson.Parser(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //JSONObject obj = JSONConvert.DeserializeObject(textBox1.Text);
            //if (obj.ContainsKey("result"))
            //{
            //    JSONArray array = obj["result"] as JSONArray;
            //    if (array != null)
            //    {
            //        foreach (var item in array)
            //        {
            //            JSONObject sobj = item as JSONObject;
            //            if (sobj != null)
            //            {
            //                switch (sobj["poll_type"] as string)
            //                {
            //                    case "message":
            //                        Debug.WriteLine("message \r\n");
            //                        break;
            //                    case "":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //            }

            //        }
            //    }
            //}
            //foreach (var item in obj)
            //{
            //    Debug .WriteLine (string.Format ("{0},{1}",item.Key ,item .Value));
            //    if(item.Value is JSONObject)
            //    {
            //        Debug.WriteLine("dic \r\n");
            //    }
            //}
            QQSDK.Net.QQMessageEventArgs.Parse(textBox1.Text);
        }


        private void Test(JSONObject obj)
        {

            Debug.WriteLine(obj.GetString("result"));
            JSONArray array = obj.GetJSONArray("result");
            foreach (JSONObject item in array)
            {
                switch (item.GetString("poll_type"))
                {
                    case "message":

                        break;
                    case "group_message":
                        break;
                    default:
                        break;
                }
                Debug.WriteLine(item.GetString("poll_type"));
                JSONObject nobj = item.GetJSONObject("value");

                Debug.WriteLine(nobj.GetString("msg_id2"));

                Debug.WriteLine(nobj.GetString("to_uin"));

            }



        }

    }
}
