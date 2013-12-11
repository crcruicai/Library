/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/2 14:58:00
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QQSDK.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonHelper
    {

        public static FriendContent ParseFriendContent(string text)
        {
            FriendResult r = ParseResult(text);
            List<FriendContent> list = new List<FriendContent>();
            if (r == null) return null;
            foreach (var item in r.Items )
            {
                
            }



            return null;

        }


        private static FriendResult ParseResult(string text)
        {
            try
            {
                FriendResult result = JsonConvert.DeserializeObject<FriendResult>(text);
                if (result != null)
                {
                    result.Items.Sort(new MsgItemComparer());
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static MsgFont GetMsgFont(string temp)
        {
            if (temp.IndexOf("font") > -1)
            {
                int s = temp.IndexOf("{");
                int e = temp.IndexOf("}");
                if (s > -1 && e > s)
                {
                    temp = temp.Substring(s, e - s + 1);
                    MsgFont font = JsonConvert.DeserializeObject<MsgFont>(temp);
                    return font;
                }

            }
            return null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FriendContent : Message
    {
        public uint FromUin { get; set; }
        public uint ToUin { get; set; }
        public DateTime Time { get; set; }
        public uint ReplyIP { get; set; }

        public string Content { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Font Font { get; set; }

        /// <summary>
        ///
        /// </summary>
        public System.Drawing.Color Color { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    class FriendResult
    {
        public FriendResult()
        {
            Items = new List<MsgItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "retcode")]
        public int Retcode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "result")]
        public List<MsgItem> Items { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    class MsgItemComparer : IComparer<MsgItem>
    {


        #region IComparer<MsgItem> 成员

        public int Compare(MsgItem x, MsgItem y)
        {
            return x.Value.MsgID.CompareTo(y.Value.MsgID);
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    class MsgItem
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "poll_type")]
        public string PollType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public MsgValue Value { get; set; }


    }

    /// <summary>
    /// 
    /// </summary>
    class MsgValue
    {
        public MsgValue()
        {
            Content = new List<object>();
        }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "from_uin")]
        public uint FromUin { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "to_uin")]
        public uint ToUin { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "time")]
        public uint Time { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "msg_id")]
        public uint MsgID { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "msg_id2")]
        public uint MsgID2 { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "msg_type")]
        public int MsgType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "reply_ip")]
        public uint ReplyIP { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public List<object> Content { get; set; }



    }

   



    class MsgFont
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "color")]
        public string Color { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "style")]
        public int[] Style { get; set; }

    }
}
