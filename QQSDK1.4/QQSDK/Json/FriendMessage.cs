using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using QQSDK.Net;

namespace QQSDK.Json
{
    // using System.Runtime.Serialization.Json;

  

    public class FriendResultItem
    {

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string poll_type { get; set; }
        [DataMember]
        public MessageValue value { get; set; }
    }



 

    /// <summary>
    /// 朋友消息.
    /// </summary>
    [DataContract]
    public class FriendMessage
    {
        
        [DataMember]
        public int retcode { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class MessageValue
    {
        public string PollType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long MsgID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long FromUin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long ToUin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int MsgID2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int MsgType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ReplyIP{ get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public long Time { get; set; }

        public string Name { get; set; }

        public int FontSize { get; set; }

        public string  MsgContent { get; set; }

        public System.Drawing.Color FontColor { get; set; }

        public string  FontStyle { get; set; }

        private MessageValue ParseMessage(string text)
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
                            value.FontColor = Tool.GetColor(a[1]);
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
                    }
                    else
                    {
                        value.MsgContent = item;
                    }
                }

            }
            return value;


        }
    }

    

  


    

}
