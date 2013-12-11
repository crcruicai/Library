using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QQSDK.Json;
using QQSDK.Systems;
using System.Diagnostics;

namespace QQSDK.Net
{
    /// <summary>
    /// QQ消息事件类.
    /// </summary>
    public class QQMessageEventArgs : EventArgs
    {
        #region 构造函数
        /// <summary>
        /// QQ消息事件类.
        /// </summary>
        /// <param name="myqq"></param>
        /// <param name="recode"></param>
        /// <param name="poll"></param>
        /// <param name="data"></param>
        /// <param name="msgtext"></param>
        public QQMessageEventArgs(string myqq, int recode, PollType poll,
            Message data, string msgtext)
        {
            _MyQQNumber = myqq;
            _Recode = recode;
            _PollMsgType = poll;
            _MsgText = msgtext;
            _Data = data;
        }

        QQMessageEventArgs(string msgtext)
        {
            _MsgText = msgtext;
            
        }
        #endregion

        #region 属性


        private string _MyQQNumber;
        /// <summary>
        /// 接收消息的QQ号码.
        /// </summary>	
        public string MyQQNumber
        {
            get { return _MyQQNumber; }
            set { _MyQQNumber = value; }
        }

        private int _Recode;
        /// <summary>
        /// 记录号.
        /// </summary>	
        public int Recode
        {
            get { return _Recode; }
            set { _Recode = value; }
        }

        private PollType _PollMsgType;
        /// <summary>
        /// 消息类型.
        /// </summary>	
        public PollType PollMsgType
        {
            get { return _PollMsgType; }
            set { _PollMsgType = value; }
        }

        private string _MsgText;
        /// <summary>
        /// 消息的Json
        /// </summary>	
        public string MsgText
        {
            get { return _MsgText; }
            set { _MsgText = value; }
        }

        private Message _Data;
        /// <summary>
        /// 消息对象.
        /// </summary>	
        public Message Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
        #endregion

        #region Json 解析.

        
        //{"retcode":0,"result":[{"poll_type":"message","value":{"msg_id":21273,"from_uin":2684355364,"to_uin":496063973,"msg_id2":105299,"msg_type":9,"reply_ip":176756443,"time":1386733521,"content":[["font",{"size":15,"color":"000000","style":[0,0,0],"name":"\u5B8B\u4F53"}],"strlist\u6492\u65E6\u53D1\u8069\r "]}},{"poll_type":"message","value":{"msg_id":21271,"from_uin":2684355364,"to_uin":496063973,"msg_id2":105297,"msg_type":9,"reply_ip":176756443,"time":1386733510,"content":[["font",{"size":15,"color":"000000","style":[0,0,0],"name":"\u5B8B\u4F53"}],"str\u5012\u8428\u7A7A\u95F4 "]}},{"poll_type":"message","value":{"msg_id":21270,"from_uin":2684355364,"to_uin":496063973,"msg_id2":105296,"msg_type":9,"reply_ip":176756443,"time":1386733508,"content":[["font",{"size":15,"color":"000000","style":[0,0,0],"name":"\u5B8B\u4F53"}],"str\u5012\u8428\u7A7A\u95F4 "]}},{"poll_type":"message","value":{"msg_id":21269,"from_uin":2684355364,"to_uin":496063973,"msg_id2":105295,"msg_type":9,"reply_ip":176756443,"time":1386733506,"content":[["font",{"size":15,"color":"000000","style":[0,0,0],"name":"\u5B8B\u4F53"}],"str\u5012\u8428\u7A7A\u95F4 "]}},{"poll_type":"message","value":{"msg_id":21272,"from_uin":2684355364,"to_uin":496063973,"msg_id2":105292,"msg_type":9,"reply_ip":176756443,"time":1386733512,"content":[["font",{"size":15,"color":"000000","style":[0,0,0],"name":"\u5B8B\u4F53"}],"str\u5012\u8428\u7A7A\u95F4 "]}},{"poll_type":"message","value":{"msg_id":21268,"from_uin":2684355364,"to_uin":496063973,"msg_id2":105291,"msg_type":9,"reply_ip":176756443,"time":1386733499,"content":[["font",{"size":15,"color":"000000","style":[0,0,0],"name":"\u5B8B\u4F53"}],"str\u5012\u8428\u7A7A\u95F4 "]}}]}


        public static List<QQMessageEventArgs> Parse(string text)
        {
          
            List<QQMessageEventArgs> list = new List<QQMessageEventArgs>();
            JSONObject obj = JSONConvert.DeserializeObject(text);
            Debug.WriteLine(obj.GetString("result"));
            JSONArray array = obj.GetJSONArray("result");
            foreach (JSONObject item in array)
            {
                JSONObject newobj = item.GetJSONObject("value");
                QQMessageEventArgs m = new QQMessageEventArgs(text);

                switch (item.GetString("poll_type"))
                {
                    case "tips":
                        m = null;
                        break;
                    case "message":
                        m.PollMsgType = PollType.Friend;
                        m.Data =GetFriendMessage(newobj);
                        break;
                    case "group_message":
                        m.PollMsgType = PollType.Group;
                        m.Data =GetGroupMessage(newobj);
                        break;
                    case "kick_message":
                        m.PollMsgType = PollType.KickMessage;
                        m.Data =GetKickMessage(newobj);
                        break;
                    case "system_message":
                    case "sys_g_msg":
                        m.PollMsgType = PollType.System;
                        m = null;
                        break;
                    default:
                        m = null;
                        break;
                }
                if (m != null)
                    list.Add(m);
              
            }
           
            return list;

        }

        private static FriendMessageData GetFriendMessage(JSONObject obj)
        {
            FriendMessageData data = new FriendMessageData();
            data.MsgID = obj.GetString("msg_id");
            data.MsgID2 = obj.GetString("msg_id2");
            data.MsgType = int.Parse(obj.GetString("msg_type"));
            data.ReplyIP = obj.GetString("reply_ip");
            data.FromUin = obj.GetString("from_uin");
            data.ToUin = obj.GetString("to_uin");
            JSONArray content = obj.GetJSONArray("content");
            foreach (var item in content)
            {
                if(item is string )
                {
                    string text = item.ToString();
                    if (text.IndexOf("\\u") > -1) text = Encode.DeUnicode(text);
                    data.Content.Add(text);
                        
                }
                else if (item is JSONArray)
                {
                    JSONArray array = item as JSONArray;
                    string flag = array[0].ToString();
                    if (flag == "face")
                    {
                        data.Content.Add(string.Format("[face{0}]", array[1]));
                    }
                    //JSONArray font = item as JSONArray;
                    //foreach (var sitem in font)
                    //{
                    //    if(sitem is JSONObject )
                    //    {
                    //        JSONObject temp = sitem as JSONObject;
                            
                    //        data.Color = Tool.GetColor(temp.GetString("color"));
                            
                    //    }
                    //}
                }
               
            }




            return data;
        }

        private static GroupMessageData GetGroupMessage(JSONObject obj)
        {
            GroupMessageData data = new GroupMessageData();
            data.MsgID = obj.GetString("msg_id");
            data.MsgID2 = obj.GetString("msg_id2");
            data.MsgType = int.Parse(obj.GetString("msg_type"));
            data.ReplyIP = obj.GetString("reply_ip");
            data.FromUin = obj.GetString("from_uin");
            data.ToUin = obj.GetString("to_uin");
            data.SendUin = obj.GetString("send_uin");
            data.Seq = obj.GetString("seq");
            data.Time = obj.GetString("time");
            data.InfoSeq = obj.GetString("info_seq");
            data.GroupCode = obj.GetString("group_code");

            JSONArray content = obj.GetJSONArray("content");
            foreach (var item in content)
            {
                if (item is string)
                {
                    string text = item.ToString();
                    if (text.IndexOf("\\u") > -1) text = Encode.DeUnicode(text);
                    data.Content.Add(text);
                }
                else if (item is JSONArray)
                {
                    JSONArray array = item as JSONArray;
                    string flag = array[0].ToString();
                    if (flag == "face")
                    {
                        data.Content.Add(string.Format("[face{0}]", array[1]));
                    }
                    else if (flag == "font")
                    {

                    }

                }
            }
            return data;
        }

        private static KickMessage GetKickMessage(JSONObject obj)
        {
            KickMessage data = new KickMessage();
            data.Reason = obj.GetString("reason");
            data.ShowReason = int .Parse (obj.GetString("show_reason"));
            data.Way = obj.GetString("way");

            return data;
        }

        #endregion

        #region 字符串解析
        public static QQMessageEventArgs Parse2(string text)
        {
            string source = text;
            QQMessageEventArgs args = new QQMessageEventArgs(text);
            text = text.Replace("{", "");
            text = text.Replace("]", "");
            text = text.Replace("[", "");
            text = text.Replace("}", "");
            text = text.Replace("\"", "");

            string[] array = text.Split(',');


            if (array.Length > 2)
            {
                string[] item = array[0].Split(':');
                if (item.Length > 1)
                {
                    args.Recode = int.Parse(item[1]);
                }
                string[] a = array[1].Split(':');
                if (a.Length > 2)
                {
                    switch (a[2].ToLower())
                    {
                        case "tips":
                            args.PollMsgType = PollType.Tips;
                            Loger.WriteLog(string.Format("Tips :{0}", text));
                            args = null;
                            break;
                        case "message":
                            args.PollMsgType = PollType.Friend;
                            args.Data = GetFriendMessage(array, 2);
                            break;
                        case "group_message":
                            args.PollMsgType = PollType.Group;
                            Loger.WriteLog(string.Format("Group :{0}", text));
                            args.Data = GetGroupMessage(array, 2);
                            break;
                        case "kick_message":
                            args.PollMsgType = PollType.KickMessage;
                            args.Data = GetKiMessage(array, 2);
                            break;
                        case "system_message":
                        case "sys_g_msg":

                            Loger.WriteLog(string.Format("system_message :{0}", text));

                            break;
                        case "buddies_status_change":
                            args.PollMsgType = PollType.StatusChange;
                            args.Data = GetStatusChangerMessage(array, 2);
                            break;
                        default:
                            Loger.WriteLog(string.Format("接收到 other:{0}", text));
                            args = null;
                            break;
                    }
                }
                else
                {
                    Loger.WriteLog("接收消息出现错误,消息返回值:" + source);
                }



            }

            return args;




        }
        private static KickMessage GetKiMessage(string[] array, int index)
        {
            string item = string.Empty;
            KickMessage value = new KickMessage();
            for (int i = index; i < array.Length; i++)
            {
                item = array[i];
                string[] a = item.Split(':');
                if (a.Length > 1)
                {
                    switch (a[0])
                    {

                        case "value":
                            value.Way = a[2];
                            break;
                        case "show_reason":
                            value.ShowReason = int.Parse(a[1]);
                            break;
                        case "reason":
                            value.Reason = Encode.DeUnicode(a[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            return value;
        }


        private static GroupMessageData GetGroupMessage(string[] array, int index)
        {
            string item = string.Empty;
            GroupMessageData value = new GroupMessageData();
            string name = string.Empty;
            int size = 10;
            string style = string.Empty;
            for (int i = index; i < array.Length; i++)
            {
                item = array[i];
                string[] a = item.Split(':');
                if (a.Length > 1)
                {
                    switch (a[0])
                    {

                        case "value":
                            if (a.Length > 2)
                                value.MsgID = a[2];
                            break;
                        case "from_uin":
                            value.FromUin = a[1];
                            break;

                        case "msg_id2":
                            value.MsgID2 = a[1];
                            break;
                        case "msg_type":
                            value.MsgType = int.Parse(a[1]);
                            break;
                        case "reply_ip":
                            value.ReplyIP = a[1];
                            break;
                        case "time":
                            value.Time = a[1];
                            break;
                        case "to_uin":
                            value.ToUin = a[1];
                            break;
                        case "color":
                            value.Color = Tool.GetColor(a[1]);
                            break;
                        case "name":
                            name = Encode.DeUnicode(a[1]);
                            break;
                        case "size":
                            size = int.Parse(a[1]);
                            break;
                        case "style":
                            style = string.Format("{0}{1}{2}", a[1], array[i + 1], array[i + 2]);
                            i = i + 2;
                            break;
                        case "info_seq":
                            value.InfoSeq = a[1];
                            break;
                        case "seq":
                            value.Seq = a[1];
                            break;
                        case "send_uin":
                            value.SendUin = a[1];
                            break;
                        case "group_code":
                            value.GroupCode = a[1];
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
                        string t = Encode.DeUnicode(item);

                        value.Content.Add(t.Replace("r", "\r\n"));
                    }
                    else
                    {
                        value.Content.Add(item);
                    }
                }
            }

            return value;
        }


        private static FriendMessageData GetFriendMessage(string[] array, int index)
        {
            string item = string.Empty;
            FriendMessageData value = new FriendMessageData();
            string name = string.Empty;
            int size = 10;
            string style = string.Empty;
            for (int i = index; i < array .Length; i++)
            {
                item = array[i];
                string[] a = item.Split(':');
                if (a.Length > 1)
                {
                    switch (a[0])
                    { 
                        case "value":
                            if (a.Length > 2)
                                value.MsgID =a[2];
                            break;
                        case "from_uin":
                            value.FromUin = a[1];
                            break;
                      
                        case "msg_id2":
                            value.MsgID2 = a[1];
                            break;
                        case "msg_type":
                            value.MsgType = int.Parse(a[1]);
                            break;
                        case "reply_ip":
                            value.ReplyIP = a[1];
                            break;
                        case "time":
                            value.Time = a[1];
                            break;
                        case "to_uin":
                            value.ToUin = a[1];
                            break;
                        case "color":
                            value.Color = Tool.GetColor(a[1]);
                            break;
                        case "name":
                            name = Encode.DeUnicode(a[1]);
                            break;
                        case "size":
                            size = int.Parse(a[1]);
                            break;
                        case "style":
                            style= string.Format("{0}{1}{2}", a[1], array[i + 1], array[i + 2]);
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
                        string t=Encode.DeUnicode(item);

                        value.Content.Add(t.Replace("\r", "\r\n"));
                    }
                    else
                    {
                        value.Content.Add(item);
                    }
                }
            }

            return value;
        }

        private static StatusChange GetStatusChangerMessage(string[] array, int index)
        {
            string item = string.Empty;
            StatusChange value = new StatusChange();
            for (int i = index; i < array.Length; i++)
            {
                item = array[i];
                string[] a = item.Split(':');
                if (a.Length > 1)
                {
                    switch (a[0])
                    {

                        case "value":
                            value.Uin = a[2];
                            break;
                        case "status":
                            value.Status = GetOnLineStatus(a[1]);
                            break;
                        case "client_type":
                            value.ClientType  = int.Parse(a[1]);
                            break;
                        default:
                            break;
                    }
                }
            }
            return value;
        }
        #endregion

        /// <summary>
        /// 将文本转换为在线状态.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static  OnlineStatus GetOnLineStatus(string text)
        {
            switch (text)
            {
                case "online":
                    return OnlineStatus.OnLine;
                case "hidden":
                    return OnlineStatus.Hidden;
                case "callme":
                    return OnlineStatus.CallMe;
                case "busy":
                    return OnlineStatus.Busy;
                case "silent":
                    return OnlineStatus.Silent;
                default:
                    break;
            }
            return OnlineStatus.OnLine;
        }


    
    }


    class QQMessageComparer:IComparer <QQMessageEventArgs>
    {


        #region IComparer<QQMessageEventArgs> 成员

        public int Compare(QQMessageEventArgs x, QQMessageEventArgs y)
        {
            if(x.PollMsgType ==y.PollMsgType)
            {
                return 0;
            }
            else
            {
                return x.PollMsgType.CompareTo(y.PollMsgType);
            }
        }

        #endregion
    }
}
