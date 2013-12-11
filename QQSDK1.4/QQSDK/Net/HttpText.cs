using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Web;

namespace QQSDK.Net
{

    /// <summary>
    /// 一些固定的文本.
    /// <para>为了防止变化.故此集中起来.</para>
    /// </summary>
    static class HttpText
    {
        #region 静态字段
        /// <summary>
        /// 第一次登录时,需要的字符串.
        /// </summary>
        public static string LoginChar =
            @"&webqq_type=10&remember_uin=1&login2qq=1&aid=1003903&u1=http%3A%2F%2Fweb2.qq.com%2Floginproxy.html%3Flogin2qq%3D1%26webqq_type%3D10&h=1&ptredirect=0&ptlang=2052&daid=164&from_ui=1&pttype=1&dumy=&fp=loginerroralert&action=6-25-30907&mibao_css=m_webqq&t=1&g=1&js_type=0&js_ver=10038&login_sig=4GncyROmhxUBMTyE1kjsnXk5ob-kchdfhCL5ZCV0HuZ2PS6hrEFoHaNEf7bx9iPA HTTP/1.1";
        /// <summary>
        /// 第一次登录时的Referer
        /// </summary>
        public static string LoginReferer =
            @"https://ui.ptlogin2.qq.com/cgi-bin/login?daid=164&target=self&style=5&mibao_css=m_webqq&appid=1003903&enable_qlogin=0&no_verifyimg=1&s_url=http%3A%2F%2Fweb2.qq.com%2Floginproxy.html&f_url=loginerroralert&strong_login=1&login_state=10&t=20130903001";
        /// <summary>
        /// 第二次登录时的Referer.
        /// </summary>
        public static string Login2Referer =
            @"http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
        /// <summary>
        /// 获取好友列表的Referer
        /// </summary>
        public static string GetUserFriendReferer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";

        public static string GetUserInfoReferer = "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=3";

        /// <summary>
        /// Poll消息的Referer
        /// </summary>
        public static string PollReferer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";


        /// <summary>
        /// 发送消息的Referer.
        /// </summary>
        public static string SendMessageReferer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";

        /// <summary>
        /// 获取好友的QQ号码的Referer
        /// </summary>
        public static string GetFriendUinReferer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
        #endregion

        #region 静态函数

        public static string Login(string qq,string password, string vfCode)
        {
            vfCode = vfCode.ToUpper();
            StringBuilder sb = new StringBuilder(100);
            sb.Append("https://ssl.ptlogin2.qq.com/login?u=");
            sb.Append(qq);
            sb.AppendFormat("&p={0}", PasswordEncrypt.MD5QQEncrypt(long.Parse(qq), password, vfCode));
            sb.AppendFormat("&verifycode={0}", vfCode);
            sb.Append(HttpText.LoginChar);
            return sb.ToString();
        }


        public static string Login2(string ptwebqq, string clientid)
        {
            StringBuilder sb = new StringBuilder(150);
            sb.Append("r=%7B%22status%22%3A%22online%22%2C%22ptwebqq%22%3A%22");
            sb.Append(ptwebqq);
            sb.Append("%22%2C%22passwd_sig%22%3A%22%22%2C%22clientid%22%3A%22");
            sb.AppendFormat("{0}%22%2C%22psessionid%22%3A%22null%22%7D&clientid={0}&psessionid=null",clientid);
            return sb.ToString();    

        }


        public static string UserFriendsList(string myqqnumber, string ptwebqq,string vfwebqq)
        {
            StringBuilder sb = new StringBuilder(150);
            sb.Append("r=%7B%22h%22%3A%22hello%22%2C%22hash%22%3A%22");
            //string code = Tool.Hash((ulong)_LoginResult.Result.Uin, _PTWebQQ);
            string code = Tool.Hash(ulong.Parse(myqqnumber), ptwebqq);
            sb.Append(code);
            sb.Append("%22%2C%22vfwebqq%22%3A%22");
            sb.Append(vfwebqq);
            //sb.Append(_LoginResult .Result .VFWebQQ);
            sb.Append("%22%7D");
            return sb.ToString();
        }


        public static string PollMessage(string clientid, string psessionid)
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append("r=%7B%22clientid%22%3A%22");
            sb.Append(clientid);
            sb.Append("%22%2C%22psessionid%22%3A%22");
            sb.Append(psessionid);
            sb.Append("%22%2C%22key%22%3A0%2C%22ids%22%3A%5B%5D%7D&clientid=");
            sb.Append(clientid);
            sb.Append("&psessionid=");
            sb.Append(psessionid);

            return sb.ToString();
        }

        public static string SendMessage(uint uin ,string content ,Font font,Color color,string clientid,string pessionid)
        {
             StringBuilder sb = new StringBuilder(400);

            sb.Append("r={\"to\":");
            sb.Append(uin);
            sb.AppendFormat(",\"face\":{0},",606);
            sb.AppendFormat("\"content\":\"[\\\"{0}\\\",",Encode .ToUnicodeString(content,true));
            sb.Append("[\\\"font\\\",");
            sb.Append("{\\\"name\\\":");
            sb.AppendFormat("\\\"{0}\\\",", Encode.ToUnicodeString(font.Name, true));
            sb.AppendFormat("\\\"size\\\":");
            sb.AppendFormat("\\\"{0}\\\",", font.Size);
            sb.AppendFormat("\\\"style\\\":");
            sb.AppendFormat("[{0}],", Tool.GetFontStyle(font));
            sb.Append("\\\"color\\\":\\\"");
            sb.Append(Tool.GetColor (color));
            sb.Append("\\\"}]]\",");
            sb.Append("\"msg_id\":");
            sb.AppendFormat("{0},",Tool .GetRandomNumber (8));
            sb.Append("\"clientid\":");
            sb.AppendFormat("\"{0}\",",clientid);
            sb.Append("\"psessionid\":");
            sb.AppendFormat("\"{0}\"", pessionid);
            sb.Append("}&clientid=");
            sb.Append(clientid);
            sb.AppendFormat("&psessionid={0}", pessionid);
            return HttpUtility.UrlDecode(sb.ToString());
        }

        #endregion



    }

}
