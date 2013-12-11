/*********************************************************
* 开发人员：TopC
* 创建时间：2013/12/11 15:17:39
* 描述说明：
*
* 更改历史：
*
* *******************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Json
{

    //{"retcode":0,"result":{"face":213,"birthday":{"month":4,"year":1989,"day":14},
    //"occupation":"","phone":"","allow":1,"college":"","uin":567986802,"constel":3,
    //"blood":3,"homepage":"http://910390181.qzone.qq.com","stat":10,"vip_info":0,
    //"country":"中国","city":"佛山","personal":"喜欢读书,喜欢思考.","nick":"梦寐之吕",
    //"shengxiao":6,"email":"","client_type":1,"province":"广东","gender":"male","mobile":"-"}}

    public class QQInfoCard
    {
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string  Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string  Personal { get; set; }

        public string City { get; set; }

        public string Country { get; set; }



    }


    class JsonHelper
    {
        public static QQInfoCard  GetQQInfoCard(string text)
        {
            JSONObject json = JSONConvert.DeserializeObject(text);
            JSONObject obj=json .GetJSONObject ("result");
            if(obj!=null)
            {
                QQInfoCard info = new QQInfoCard();
                info.City = obj.GetString("city");
                info.Country = obj.GetString("country");
                info.Email = obj.GetString("email");
                info.Nick = obj.GetString("nick");
                info.Personal = obj.GetString("personal");
                info.Phone = obj.GetString("phone");

                return info;

            }
            return null;
        }

        public static string GetTrees(string text)
        {
            JSONObject json = JSONConvert.DeserializeObject(text);
            StringBuilder sb = new StringBuilder();
            foreach (var item in json)
            {
                
            }
            return null;



        }


    }

}
