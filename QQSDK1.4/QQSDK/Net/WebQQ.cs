using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using QQSDK.Json;
using System.Diagnostics;
using System.Web;
using System.Threading.Tasks;
using QQSDK.Systems;
namespace QQSDK.Net
{
    /// <summary>
    /// QQ通信代理类.
    /// </summary>
    public class WebQQ
    {
        #region 字段与变量
        /// <summary>
        /// 是否掉线.
        /// </summary>
        private bool _KissOnline = false;

        /// <summary>
        /// HttpWeb 代理.
        /// </summary>
        private HttpWeb _HttpWeb;

        /// <summary>
        /// 发送消息序号.
        /// </summary>
        private int _SendNumber;
        /// <summary>
        /// PTWebQQ 标识符.
        /// </summary>
        private string _PTWebQQ = string.Empty;
        /// <summary>
        /// 登录令牌WebQQ
        /// </summary>
        private string _VFWebQQ;
        /// <summary>
        /// 登录令牌PsesionID.
        /// </summary>
        private string _PsessionID;
        /// <summary>
        /// 登录结果.
        /// </summary>
        private PostLogin _LoginResult;

        #endregion

        #region 事件与委托

        /// <summary>
        /// 接收消息事件.
        /// </summary>
        public event EventHandler<QQMessageEventArgs> ReciveMessage;

        /// <summary>
        /// 引发接收消息事件.
        /// </summary>
        /// <param name="e"></param>
        protected void OnReciveMessage(QQMessageEventArgs e)
        {
            if (ReciveMessage != null)
            {
                ReciveMessage(this, e);
            }
        }

        #endregion

        #region 构造函数
        public WebQQ()
        {
            _HttpWeb = new HttpWeb();
            _ClientID = Tool.GetRandomNumber(10);
            Random d = new Random(5000);
            _SendNumber = d.Next()+10000000;
           
        }

        #endregion

        #region 属性
        private string _NickName;
        /// <summary>
        /// 获取登录QQ昵称.
        /// </summary>
        public string NickName
        {
            get
            {
                return _NickName;
            }
        }

        private string  _ClientID;
        /// <summary>
        /// 客户端的唯一ID.
        /// </summary>	
        public string  ClientID
        {
            get
            {
                return _ClientID;
            }
        }

        private string _MyQQNumber;
        /// <summary>
        /// QQ号码.
        /// </summary>
        public string MyQQNumber
        {
            get { return _MyQQNumber; }
        }


        private QQInfoCard _InfoCard;
        /// <summary>
        /// 
        /// </summary>
        public QQInfoCard InfoCard
        {
            get
            {
                if (_InfoCard == null)
                    _InfoCard = GetQQInfoCard(MyQQNumber);
                return _InfoCard;
            }
        }




        #endregion

        #region 公共函数

        #region 登录与登出
        /// <summary>
        /// QQ登录.
        /// </summary>
        /// <param name="qqNumber">QQ号码</param>
        /// <param name="password">QQ密码.</param>
        /// <param name="verifyCode">QQ的验证码.</param>
        /// <param name="status">QQ登录的状态.</param>
        /// <returns></returns>
        public LoginResult Login(string qqNumber, string password, string verifyCode)
        {
            if (!Tool.CheckQQNumber(qqNumber)) return LoginResult.QQNumberError;
            if (!Tool.CheckQQPassword(password)) return LoginResult.PasswordError;
            verifyCode = verifyCode.ToUpper();
            StringBuilder sb = new StringBuilder(100);
            sb.Append("https://ssl.ptlogin2.qq.com/login?u=");
            sb.Append(qqNumber);
            sb.AppendFormat("&p={0}", PasswordEncrypt.MD5QQEncrypt(long.Parse(qqNumber), password, verifyCode));
            sb.AppendFormat("&verifycode={0}",verifyCode);
            sb.Append(HttpText.LoginChar);
            string url = sb.ToString();

            //web.SetReferer(HttpText.LoginReferer);
            //string text = web.HttpSendData(url);

            _HttpWeb.Referer = HttpText.LoginReferer;
            //发送登录请求.
            string text = _HttpWeb.SendToText(url);
            if (text.IndexOf("登录成功") > 0)
            {
                GetNickName(text);
                //模拟重定向.
                text=_HttpWeb.SendToText(GetDirectionUrl(text));
                if ( text.Length > 0)
                {
                    _HttpWeb.SendToText("http://d.web2.qq.com//loginproxy.html?login2qq=1&webqq_type=10");
                }
                //进行第二次的登录.
                sb.Clear();
                string ptwebqq = GetKey();
                
                
                sb.Append("r=%7B%22status%22%3A%22online%22%2C%22ptwebqq%22%3A%22");
                sb.Append(ptwebqq);
                sb.Append("%22%2C%22passwd_sig%22%3A%22%22%2C%22clientid%22%3A%22");
                sb.AppendFormat("{0}%22%2C%22psessionid%22%3A%22null%22%7D&clientid={0}&psessionid=null", _ClientID);
                
                _HttpWeb.Referer = HttpText.Login2Referer;
                text = _HttpWeb.PostWebRequest("http://d.web2.qq.com/channel/login2", sb.ToString(), Encoding.UTF8);

                if (GetLoginToken(text))
                {
                    //启动消息监听.
                    Task tast = new Task(() => { ListenMessage(); });
                    tast.Start();
                    _MyQQNumber = qqNumber;
                    return LoginResult.LoginSucceed;
                }

            }
            else if (text.IndexOf("验证码") > -1)
            {
                return LoginResult.VerifyCodeError;
            }
            else if (text.IndexOf("密码") > -1)
            {
                return LoginResult.PasswordError;
            }
            else if (text.IndexOf("您的帐号暂时无法登录") > -1)
            {
                return LoginResult.QQNumberError;
            }
            //检查登录结果.

            return LoginResult.LoginFail;
        }

        /// <summary>
        /// 重新登陆.
        /// </summary>
        /// <param name="qqnumber"></param>
        /// <param name="pp"></param>
        /// <returns></returns>
        public LoginResult ReLogin(string qqnumber, string pp)
        {
            return ReLogin();
        }


        /// <summary>
        /// 重新登陆.
        /// </summary>
        /// <returns></returns>
        public LoginResult ReLogin()
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append("r=%7B%22status%22%3A%22online%22%2C%22ptwebqq%22%3A%22");
            sb.Append(_PTWebQQ);
            sb.Append("%22%2C%22passwd_sig%22%3A%22%22%2C%22clientid%22%3A%22");
            sb.AppendFormat("{0}%22%2C%22psessionid%22%3A%22null%22%7D&clientid={0}&psessionid=null", _ClientID);

            _HttpWeb.Referer = HttpText.Login2Referer;
            string text = _HttpWeb.PostWebRequest("http://d.web2.qq.com/channel/login2", sb.ToString(), Encoding.UTF8);
            if (GetLoginToken(text))
            {
                _KissOnline = false;
                return LoginResult.LoginSucceed;
            }
            return LoginResult.LoginFail;
        }

        /// <summary>
        /// 退出登录.
        /// </summary>
        /// <returns></returns>
        public string LoginOut()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://d.web2.qq.com/channel/logout2?ids=&clientid=");
            sb.Append(_ClientID);
            sb.Append("&psessionid=");
            sb.Append(_PsessionID);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));

            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(sb.ToString());
        }



        #endregion

        #region 好友操作
#if QQSDKRobot

         /// <summary>
        /// 获取好友列表.
        /// </summary>
        /// <returns></returns>
        public UserFriend GetUserFriends()
        {
            // 填写提交表单的数据.
            StringBuilder sb = new StringBuilder();
            sb.Append("r=%7B%22h%22%3A%22hello%22%2C%22hash%22%3A%22");
            //string code = Tool.Hash((ulong)_LoginResult.Result.Uin, _PTWebQQ);
            string code = Tool.Hash(ulong.Parse(_MyQQNumber), _PTWebQQ);
            sb.Append(code);
            sb.Append("%22%2C%22vfwebqq%22%3A%22");
            sb.Append(_VFWebQQ);
            //sb.Append(_LoginResult .Result .VFWebQQ);
            sb.Append("%22%7D");
            //请求数据.
            _HttpWeb.Referer = HttpText.GetUserFriendReferer;
            string text = _HttpWeb.PostWebRequest("http://s.web2.qq.com/api/get_user_friends2", sb.ToString(), Encoding.UTF8);

            return null;
        }
#else
        /// <summary>
        /// 获取好友列表.
        /// </summary>
        /// <returns></returns>
        public UserFriend GetUserFriends()
        {
            // 填写提交表单的数据.
            StringBuilder sb = new StringBuilder();
            sb.Append("r=%7B%22h%22%3A%22hello%22%2C%22hash%22%3A%22");
            //string code = Tool.Hash((ulong)_LoginResult.Result.Uin, _PTWebQQ);
            string code = Tool.Hash2(ulong.Parse(_MyQQNumber), _PTWebQQ);
            sb.Append(code);
            sb.Append("%22%2C%22vfwebqq%22%3A%22");
            sb.Append(_VFWebQQ);
            //sb.Append(_LoginResult .Result .VFWebQQ);
            sb.Append("%22%7D");
           
            //请求数据.
            _HttpWeb.Referer = HttpText .GetUserFriendReferer;
            string text=_HttpWeb.PostWebRequest("http://s.web2.qq.com/api/get_user_friends2", sb.ToString(), Encoding.UTF8);
            try
            {
                UserFriend fm = JSON.Parse<UserFriend>(text);
                return fm;
            }
            catch (Exception)
            {
                return null;
            }
        }
#endif
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uin"></param>
        public void GetFace(string uin)
        {
            ///cgi/svr/face/getface?cache=1&type=1&fid=0&uin=1595462822&vfwebqq=388e96930e991cd6546e972ed1017be8160f87f04915e0586a31c1b358944170b75e791f5a51137e&t=9191848211
            StringBuilder sb = new StringBuilder();
            sb.Append("http://face10.qun.qq.com/cgi/svr/face/getface?cache=1&type=1&fid=0&uin=");
            sb.Append(uin);
            sb.Append("&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));
            _HttpWeb.Referer = HttpText.GetUserFriendReferer;

            string text = _HttpWeb.SendToText(sb.ToString());

        
        }

        /// <summary>
        /// 获取好友的信息.
        /// </summary>
        /// <param name="Uin">指定好友Uin.</param>
        /// <returns></returns>
        public string GetFriendInfo(string Uin)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://s.web2.qq.com/api/get_friend_info2?tuin=");
            sb.Append(Uin);
            sb.Append("&verifysession=&code=&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));
            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(sb.ToString());

            //{"retcode":0,"result":{"face":213,"birthday":{"month":4,"year":1989,"day":14},
            //"occupation":"","phone":"","allow":1,"college":"","uin":567986802,"constel":3,
            //"blood":3,"homepage":"http://910390181.qzone.qq.com","stat":10,"vip_info":0,
            //"country":"中国","city":"佛山","personal":"喜欢读书,喜欢思考.","nick":"梦寐之吕",
            //"shengxiao":6,"email":"","client_type":1,"province":"广东","gender":"male","mobile":"-"}}
        }

        /// <summary>
        /// 获取好友的信息卡.
        /// </summary>
        /// <param name="Uin"></param>
        /// <returns></returns>
        public QQInfoCard GetQQInfoCard(string Uin)
        {
            string text = GetFriendInfo(Uin);
            return JsonHelper.GetQQInfoCard(text);
        }

         /// <summary>
        /// 获取 本QQ的签名
        /// </summary>
        /// <param name="uin"></param>
        public string GetUserLnick()
        {
            return GetUserLnick(_MyQQNumber);
        }
       

        /// <summary>
        /// 获取指定 QQ签名.
        /// </summary>
        /// <param name="uin">Uin号</param>
        public string GetUserLnick(string uin)
        {
            // 获取QQ 签名
            // /api/get_single_long_nick2?tuin=398117542&vfwebqq=4c6805bae960185d6654d96c03fdcc349982b1609e8c1a4f5a73f1c50b506667fcbbdb0f68d29c87&t=9748631232 
            //获取QQ号码.
            StringBuilder sb = new StringBuilder();
            sb.Append("http://s.web2.qq.com/api/get_single_long_nick2?tuin=");
            sb.Append(uin);
            sb.Append("&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}",Tool.GetRandomNumber(10));

            _HttpWeb.Referer = "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=3";

            string text=_HttpWeb.SendToText(sb.ToString());
            int index = text.IndexOf("lnick\":\"");
            if (index > -1)
            {
                int last = text.IndexOf("}", index);
                if (last > index)
                {
                    return text.Substring(index+8, last - index-9);
                }
            }
            return "";
           
     
            //{"retcode":0,"result":[{"uin":398117542,"lnick":"更新签名进行测试"}]}


        }


        /// <summary>
        /// 从Uin中获取QQ号码.
        /// </summary>
        /// <param name="uin">Uin</param>
        /// <returns></returns>
        public string GetQQNumber(string uin)
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append("http://s.web2.qq.com/api/get_friend_uin2?tuin=");
            sb.Append(uin);
            sb.Append("&verifysession=&type=1&code=&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));
            _HttpWeb.Referer = HttpText.GetFriendUinReferer;
            string text = _HttpWeb.SendToText(sb.ToString());
            
            int index = text.IndexOf("\"account\":");
            int last = text.IndexOf(",", index);
            if (index > -1 && last > index)
            {
                return text.Substring(index + 10, last - index - 10);
            }
            return text;

        }
        #region  添加好友
        /// <summary>
        /// 添加QQ好友时,需要获取指定号码的信息,并将验证码提交.
        /// </summary>
        /// <param name="qqnumber">QQ号码</param>
        /// <param name="verifysession">验证码的值.</param>
        /// <param name="code">验证码.</param>
        /// <returns>好友的信息.</returns>
        public string GetSingleInfo(string qqnumber,string verifysession,string code)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://s.web2.qq.com/api/get_single_info2?tuin=");
            sb.Append(qqnumber);
            sb.Append("&verifysession=");
            sb.Append(verifysession);
            sb.Append("&code=");
            sb.Append(code);
            sb.Append("&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));

            _HttpWeb.Referer = "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=1";

            return _HttpWeb.SendToText(sb.ToString());
        }
        /// <summary>
        /// 发送好友验证消息.
        /// 
        /// </summary>
        /// <param name="qqnumber">qq号码</param>
        /// <param name="msg">验证消息</param>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        public string AddNeedVerify(string qqnumber,string msg,string token)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("r=%7B%22account%22%3A{0}",qqnumber);
            sb.Append("%2C%22myallow%22%3A1%2C%22groupid%22%3A0%2C%22msg%22%3A%22");
            sb.Append(Encode.ToUnicodeString(msg));
            sb.Append("%22%2C%22token%22%3A%22");
            sb.Append(token);
            sb.Append("%22%2C%22vfwebqq%22%3A%");
            sb.Append(_VFWebQQ);
            sb.Append("%22%7D");

            _HttpWeb.Referer = "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=1";


            return _HttpWeb.PostWebRequest("http://s.web2.qq.com/api/add_need_verify2",
                sb.ToString(), Encoding.UTF8);
        }

        #endregion

        #region 搜索好友

        /// <summary>
        /// 指定条件搜索QQ.
        /// </summary>
        /// <param name="contry"></param>
        /// <param name="province"></param>
        /// <param name="city"></param>
        /// <param name="age"></param>
        /// <param name="sex"></param>
        /// <param name="lang"></param>
        /// <param name="online"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public string SearchQQByKey(int contry, int province, int city, int age, int sex, int lang, int online,int page)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("/api/search_qq_by_term?");
            sb.AppendFormat("&country={0}", contry);
            sb.AppendFormat("&province={0}", province);
            sb.AppendFormat("&city={0}", city);
            sb.AppendFormat("&agerg={0}", age);
            sb.AppendFormat("&sex={0}", sex);
            sb.AppendFormat("&lang={0}", lang);
            sb.AppendFormat("&online={0}", online);
            sb.AppendFormat("&vfwebqq={0}", _VFWebQQ);
            sb.AppendFormat("&page={0}&t={1}", page, Tool.GetRandomNumber(10));

            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(sb.ToString());
        }

        public string SerchQQByKey(SearchData data)
        {           
            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(data.GetQueryString(_VFWebQQ ));
        }

        #endregion

        #endregion

        #region 群操作

        /// <summary>
        /// 获取群列表.
        /// </summary>
        /// <returns></returns>
        public string GetGroupList()
        {
            //
            //r=%7B%22vfwebqq%22%3A%2262183731819aeaaa2c20240e886034863e92c60e4de3a0f9cc7d928b08c81bd9572ba40eeca74ba0%22%7D
            string post = string.Format("r=%7B%22vfwebqq%22%3A%22{0}%22%7D", _VFWebQQ);
            _HttpWeb.Referer = "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=3";
            return _HttpWeb.PostWebRequest("http://s.web2.qq.com/api/get_group_name_list_mask2", post, Encoding.UTF8);


            //{"retcode":0,"result":{"gmasklist":[],"gnamelist":
            //[{"flag":16777217,"name":"简易生活","gid":1296072823,"code":2859592429},
            //{"flag":16777217,"name":"天箭网—华软职群","gid":3734556083,"code":180221451},
            //{"flag":1,"name":"总代理","gid":105055278,"code":3026038888},
            //{"flag":184549393,"name":"领翔网络(FlySSH)","gid":114806969,"code":1418189508},
            //{"flag":16777217,"name":"2222222222","gid":443142562,"code":921629403}],"gmarklist":[]}}
        }

        /// <summary>
        /// 根据Uin(Code)获取 群号码.
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        public string GetGroupNumber(string uin)
        {
            ///api/get_friend_uin2?tuin=2859592429
            //&verifysession=&type=4&code=
            // &vfwebqq=62183731819aeaaa2c20240e886034863e92c60e4de3a0f9cc7d928b08c81bd9572ba40eeca74ba0
            // &t=1309761146
            StringBuilder sb = new StringBuilder(200);
            sb.AppendFormat("http://s.web2.qq.com/api/get_friend_uin2?tuin={0}", uin);
            sb.AppendFormat("&verifysession=&type=4&code=&vfwebqq={0}", _VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));
            _HttpWeb.Referer = "http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=3";

           string text =_HttpWeb.SendToText(sb.ToString());
           int index = text.IndexOf("account");
           if (index > -1)
           {
               int last = text.IndexOf(",");
               if (last > index)
               {
                   return text.Substring(index + 9, last - index - 9);
               }
           }
           return "";
            //{"retcode":0,"result":{"uiuin":"","account":13523906,"uin":2859592429}}
        }

        /// <summary>
        /// 获取群成员信息列表.
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public string GetGroupMemberList(string groupCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("http://s.web2.qq.com/api/get_group_info_ext2?gcode=");
            sb.Append(groupCode);
            sb.Append("&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));


            _HttpWeb.Referer = " http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(sb.ToString());
        }

        /// <summary>
        /// 获取群信息.
        /// </summary>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public string GetGroupInfo(string groupCode)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sb.Append(groupCode);

            return null;
        }

        /// <summary>
        /// 获取群头像.
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        public Image GetGruopHead(string uin)
        {
            // /cgi/svr/face/getface?cache=0&type=4&fid=0&uin=2859592429
            //&vfwebqq=bc26a763880555d85c214cce15b85d1faf1e5f1b960c8e0c5a7775c810d63ccd856083de77617e8a

            StringBuilder sb = new StringBuilder();
            sb.Append("http://face10.qun.qq.com/cgi/svr/face/getface?cache=0&type=4&fid=0&uin=");
            sb.Append(uin);
            sb.Append("&vfwebqq=");
            sb.Append(_VFWebQQ);
            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";

            return _HttpWeb.GetImage(_HttpWeb.CreateHttpWebRequest(sb.ToString()));
            
        
        }

        /// <summary>
        /// 获取朋友的头像.
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        public Image GetFriendHead(string uin)
        {
            // /cgi/svr/face/getface?cache=1&type=1&fid=0&uin=685015839
            // &vfwebqq=bc26a763880555d85c214cce15b85d1faf1e5f1b960c8e0c5a7775c810d63ccd856083de77617e8a
            // &t=8237029416
            StringBuilder sb = new StringBuilder();
            sb.Append("http://face10.qun.qq.com/cgi/svr/face/getface?cache=1&type=1&fid=0&uin=");
            sb.Append(uin);
            sb.Append("&vfwebqq=");
            sb.Append(_VFWebQQ);
            sb.AppendFormat("&t={0}", Tool.GetRandomNumber(10));
            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.GetImage(_HttpWeb.CreateHttpWebRequest(sb.ToString()));

        }

        /// <summary>
        /// 申请加入指定的群.
        /// </summary>
        /// <param name="groupCode">群号码.</param>
        /// <param name="msg">验证的消息</param>
        /// <param name="vfy" >验证码令牌</param>
        /// <returns></returns>
        public string ApplyJoinGroup(string groupCode,string msg,string vfy)
        {
            StringBuilder sb = new StringBuilder(200);
            sb.Append("r=%7B%22gcode%22%3A");
            sb.Append(groupCode);
            sb.Append("%2C%22code%22%3A%22mhtv%22%2C%22vfy%22%3A%");
            sb.Append(vfy);//vfy
            sb.Append("%22%2C%22msg%22%3A%22");
            sb.Append(Encode.ToUnicodeString (msg));//msg
            sb.Append("%22%2C%22vfwebqq%22%3A%");
            sb.Append(_VFWebQQ);
            sb.Append("%22%7D");

            _HttpWeb.Referer = " http://s.web2.qq.com/proxy.html?v=20110412001&callback=1&id=3";
            return _HttpWeb.PostWebRequest("http://s.web2.qq.com/api/apply_join_group2", sb.ToString(), Encoding.UTF8);
            //{"retcode":0,"result":{"statu":"pending"}}

           

        }

        /// <summary>
        /// 搜索群.
        /// </summary>
        /// <param name="groupCode">指定的群的号码.</param>
        /// <param name="vfCode">验证码.</param>
        /// <returns></returns>
        public string SearchGroup(string groupCode,string vfCode)
        {
            StringBuilder sb = new StringBuilder(150);
            sb.Append("http://cgi.web2.qq.com/keycgi/qqweb/group/search.do?pg=1&perpage=10&all=");
            sb.Append(groupCode );
            sb.AppendFormat("&c1=0&c2=0&c3=0&st=0&vfcode={2}&type=1&vfwebqq={0}&t={1}",
                _VFWebQQ, Tool.GetRandomNumber(10),vfCode .ToLower());
            _HttpWeb.Referer ="http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(sb.ToString());
        }

        /// <summary>
        /// 使用条件查找群.
        /// </summary>
        /// <param name="key">关键词.</param>
        /// <returns></returns>
        public string SearchGroupByKey(string key)
        {
            StringBuilder sb = new StringBuilder(150);
            sb.Append("http://cgi.web2.qq.com/keycgi/qqweb/group/search.do?pg=1&perpage=10&all=");
            sb.Append(Encode .ToUnicodeString (key));
            sb.AppendFormat("&c1=0&c2=0&c3=0&st=0&vfcode=&type=1&vfwebqq={0}&t={1}",
                _VFWebQQ, Tool.GetRandomNumber(10));
            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            return _HttpWeb.SendToText(sb.ToString());

        }

        /// <summary>
        /// 发送群消息.
        /// </summary>
        /// <param name="groupUin"></param>
        /// <param name="content"></param>
        /// <param name="font"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public string SendGroupMessage(string groupUin, string content, Font font, Color color)
        {
            StringBuilder sb = new StringBuilder(400);

            sb.AppendFormat("r={\"group_uin\":{0}", groupUin);
            sb.AppendFormat(",\"content\":\"[\\\"{0}\\\",", Encode.ToUnicodeString(content));
            sb.Append("[\\\"font\\\",");
            sb.Append("{\\\"name\\\":");
            sb.AppendFormat("\\\"{0}\\\",", Encode.ToUnicodeString(font.Name));
            sb.AppendFormat("", font.Size);
            sb.AppendFormat("", Tool.GetFontStyle(font));
            sb.AppendFormat("", Tool.GetColor(color));
            sb.AppendFormat("", GetSendNumber());
            sb.AppendFormat( "",_PsessionID );
            sb.AppendFormat("", _ClientID);
            sb.AppendFormat("", _PsessionID);

            return null;

        }

        #endregion

        #region 基本操作

        /// <summary>
        /// 检查是否需要验证码.
        /// </summary>
        /// <param name="qqNumber">QQ号码.</param>
        /// <param name="result">验证码</param>
        /// <returns></returns>
        public LoginResult GetLoginVC(string qqNumber,out string result)
        {
            result = string.Empty;
            if (!Tool.CheckQQNumber(qqNumber)) return LoginResult.QQNumberError;
            StringBuilder sb=new StringBuilder (100);
            sb.Append ("https://ssl.ptlogin2.qq.com/check?uin=");
            sb.Append (qqNumber );
            sb.Append ("&appid=1003903&js_ver=10056&js_type=0&login_sig=MHSO-A*q-PrMoaZLgnIejVwbnE0T2KngJQTtI0-sS0inyDXPlZ1APPIX8DWjSu35&u1");
            sb.Append ("=http%3A%2F%2Fweb2.qq.com%2Floginproxy.html&r=0.");
            sb.Append (Tool .GetRandomNumber (15));
      
            string text=_HttpWeb.SendToText(sb.ToString ());
            if (text.IndexOf("ptui_checkVC('0')") > -1)
            {
                //不需要验证码.
                result = text.Substring(18, 4);
                return LoginResult.NotVerifyCode;
            }
            else
            {
                //需要验证码.
                result = string.Format("https://ssl.captcha.qq.com/getimage?aid=1003903&r=0.7291774027980864&uin={0}", qqNumber);
                return LoginResult.NeedVerifyCode;
            }
           
        }

        /// <summary>
        /// 检查登录时,是否需要验证码.
        /// </summary>
        /// <param name="qqnumber"></param>
        /// <returns></returns>
        public bool CheckLoginImage(string qqnumber)
        {
            return false;
        }

        /// <summary>
        /// 获取QQ登录的验证码图片.
        /// </summary>
        /// <param name="qqnumber">QQ号码.</param>
        /// <returns></returns>
        public Image GetLoginVCImage(string qqnumber)
        {
            string url = string.Format("https://ssl.captcha.qq.com/getimage?aid=1003903&r=0.7291774027980864&uin={0}", qqnumber);
           
            return _HttpWeb.GetImage(_HttpWeb.CreateHttpWebRequest(url));
           
        }
        
        /// <summary>
        /// 更改QQ在线状态.
        /// </summary>
        /// <param name="status">要改变的状态.</param>
        /// <returns>操作是否成功.</returns>
        public bool ChangeOnlineStatus(OnlineStatus status)
        {
            // /channel/change_status2?newstatus=hidden&clientid=1746168781
            //&psessionid=8368046764001e636f6e6e7365727665725f77656271714031302e3132382e36362e313132000063d90000012b026e0400a6caba176d0000000a405265527738515273396d000000283b4b334a755d0644a8c6cd627b5abb19981eb6e58e0df42c6716776016284c58af59422bdcf1eef5
            //&t=1403217508
            StringBuilder sb = new StringBuilder(200);
            sb.Append("http://d.web2.qq.com/channel/change_status2?newstatus=");
            sb.Append(status.ToString().ToLower());
            sb.Append("&clientid=");
            sb.Append(_ClientID);
            sb.Append("&psessionid=");
            sb.Append(_PsessionID);
            sb.AppendFormat("&t={0}",Tool.GetRandomNumber (10));

            _HttpWeb.Referer = "http://d.web2.qq.com/proxy.html?v=20110331002&callback=1&id=2";
            string text =_HttpWeb.SendToText(sb.ToString());

            if (text.IndexOf("ok") > -1) return true;
            return false;
        }

        #endregion

        #region 消息的接收与发送
        /// <summary>
        /// 发送消息.
        /// </summary>
        /// <param name="uin">指定的QQ的Uin</param>
        /// <param name="content">消息的内容.</param>
        /// <param name="font">字体.</param>
        /// <param name="color">颜色.</param>
        /// <returns></returns>
        public bool SendMessage(string uin, string content, Font font, Color color)
        {
            try
            {

                StringBuilder sb = new StringBuilder(400);

                sb.Append("r={\"to\":");
                sb.Append(uin);
                sb.AppendFormat(",\"face\":{0},", 606);
                sb.AppendFormat("\"content\":\"[\\\"{0}\\\",", Encode.ToUnicodeString(content, false));
                sb.Append("[\\\"font\\\",");
                sb.Append("{\\\"name\\\":");
                sb.AppendFormat("\\\"{0}\\\",", Encode.ToUnicodeString(font.Name, true));
                sb.AppendFormat("\\\"size\\\":");
                sb.AppendFormat("\\\"{0}\\\",", font.Size);
                sb.AppendFormat("\\\"style\\\":");
                sb.AppendFormat("[{0}],", Tool.GetFontStyle(font));
                sb.Append("\\\"color\\\":\\\"");
                sb.Append(Tool.GetColor(color));
                sb.Append("\\\"}]]\",");
                sb.Append("\"msg_id\":");
                sb.AppendFormat("{0},", GetSendNumber());
                sb.Append("\"clientid\":");
                sb.AppendFormat("\"{0}\",", _ClientID);
                sb.Append("\"psessionid\":");
                sb.AppendFormat("\"{0}\"", _PsessionID);
                sb.Append("}&clientid=");
                sb.Append(_ClientID);
                sb.AppendFormat("&psessionid={0}", _PsessionID);
                //string data = Encode.HttpUrlEncode(sb.ToString());
                string data = HttpUtility.UrlDecode(sb.ToString());
                _HttpWeb.Referer = HttpText.SendMessageReferer;
                string text = _HttpWeb.PostWebRequest("http://d.web2.qq.com/channel/send_buddy_msg2", data, Encoding.UTF8);
                if (text.IndexOf("result") > -1)
                {
                    return true;
                }
                else if(text.IndexOf ("未能解析此远程名称")>-1)
                {
                    //通知下线.
                }
                else
                {
                    return false;
                }


            }
            catch(Exception ex)
            {
                Loger.WriteLog(ex);
            }
            return false;
        }

        /// <summary>
        /// 监听消息.
        /// </summary>
        private void ListenMessage()
        {
            try
            {
                while (true)
                {
                    if (_KissOnline)
                    {
                        //QQ 掉线了.
                        Thread.Sleep(500);
                        continue;
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append("r=%7B%22clientid%22%3A%22");
                    sb.Append(_ClientID);
                    sb.Append("%22%2C%22psessionid%22%3A%22");
                    sb.Append(_PsessionID);
                    sb.Append("%22%2C%22key%22%3A0%2C%22ids%22%3A%5B%5D%7D&clientid=");
                    sb.Append(_ClientID);
                    sb.Append("&psessionid=");
                    sb.Append(_PsessionID);

                    _HttpWeb.Referer = HttpText.PollReferer;
                    string text = _HttpWeb.PollWebRequest("http://d.web2.qq.com/channel/poll2", sb.ToString(), Encoding.UTF8);

                    try
                    {
                        if (text.IndexOf("result") > -1)
                        {
                            List<QQMessageEventArgs> list = QQMessageEventArgs.Parse(text);

                            foreach (var item in list)
                            {
                                if (item.PollMsgType == PollType.KickMessage)
                                {
                                    _KissOnline = true;
                                }
                                OnReciveMessage(item);
                            }

                            
                            
                        }
                        else if(text.IndexOf ("\"retcode\":102")>-1)
                        {
                            // 没有接收到任何消息.
                        }
                        else if(text.IndexOf ("\"retcode\":121")>-1)
                        {
                            ReLogin();
                            //QQ掉线了
                            string temp=string.Format ("接收错误:\r\nQQ号码:{0} \r\nPsessionID:{1}\r\nPTWebQQ:{2}\r\nVFWebQQ:{3}",
                                _MyQQNumber,_PsessionID ,_PTWebQQ ,_VFWebQQ);

                            Loger.WriteLog(temp);
                        }
                        else if(text.IndexOf ("\"retcode\":116")>-1)
                        {
                            //改变Cookie
                            ReLogin();
                            Loger.WriteLog("接收错误116:" + text);
                        }
                        else
                        {
                            Loger.WriteLog("其他接收错误:"+text);
                        }
                        

                    }
                    catch (Exception e)
                    {
                        Loger.WriteLog(e);

                    }
                    Thread.Sleep(500);
                }
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }

        }


        #endregion

        #endregion

        #region 私有函数

        private string GetSendNumber()
        {
            _SendNumber++;
            return _SendNumber.ToString();
            
        }


        /// <summary>
        /// 获取第一次登陆 重定向的地址.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string GetDirectionUrl(string text)
        {
            int index = text.IndexOf("http");
            int end = text.IndexOf("'",index);
            if (index > -1 && end > -1)
            {
                return text.Substring(index, end - index);
            }
            return string.Empty;
        }
      

        /// <summary>
        /// 获取登录QQ的昵称
        /// </summary>
        /// <param name="text"></param>
        private void GetNickName(string text)
        {
            int index = text.IndexOf("登录成功！', '");
            int last = text.IndexOf("')", index);
            if (last > index)
            {
                _NickName=text.Substring(index + 9, last - index - 9);
            }

        }

        /// <summary>
        /// 获取登录令牌.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool GetLoginToken(string text)
        {
            try
            {
                PostLogin pl = JSON.Parse<PostLogin>(text);
                if (pl.Result != null)
                {
                    _LoginResult = pl;
                    _VFWebQQ = _LoginResult.Result.VFWebQQ;
                    _PsessionID = _LoginResult.Result.PsessionID;
                    return true;
                }
            }
            catch (Exception ex)
            {

                Loger.WriteLog(ex);
            }
            return false;
        }


        /// <summary>
        /// 获取PTWebQQ标识符.
        /// </summary>
        /// <returns></returns>
        private string GetKey()
        {
            if (string.IsNullOrEmpty(_PTWebQQ))
            {
                string cookie = _HttpWeb.GetCookieText();
                //string cookie = web.GetCookie();
                int index = cookie.IndexOf("ptwebqq=");
                if (index > -1)
                {
                    _PTWebQQ = cookie.Substring(index + 8, 64);

                }
            }
            return _PTWebQQ;
        }



        #endregion

    }

   
}
