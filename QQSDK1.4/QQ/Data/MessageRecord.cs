using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CWebQQ.Data
{
    /// <summary>
    /// 多QQ消息记录器
    /// </summary>
    [Serializable]
    public class MessageRecord:Dictionary <string,PerQQMessage>
    {
        protected MessageRecord(SerializationInfo info, StreamingContext context) : base(info, context) { }
        
        /// <summary>
        /// 多QQ消息记录器.
        /// </summary>
        public MessageRecord():base()
        {

        }

       
        /// <summary>
        /// 添加消息.
        /// </summary>
        /// <param name="fatherQQ">父qq号码.</param>
        /// <param name="friendQQ">朋友的QQ号</param>
        /// <param name="content">聊天内容.</param>
        public void Add(string fatherQQ, string friendQQ, string content)
        {
            PerQQMessage per = null;
            if (this.ContainsKey(fatherQQ))
            {
                 per = this[fatherQQ];
            }
            else
            {
                per = new PerQQMessage();
                this.Add(fatherQQ, per);
            }
            per.FriendMessage.Add(friendQQ, content);
        }

        /// <summary>
        /// 查找消息.
        /// </summary>
        /// <param name="fatherQQ">父qq号码.</param>
        /// <param name="friendQQ">朋友的QQ号</param>
        /// <returns></returns>
        public string FindContent(string fatherQQ, string friendQQ)
        {
            PerQQMessage per = null;
            if (this.ContainsKey(fatherQQ))
            {
                per = this[fatherQQ];
                if (per.FriendMessage.ContainsKey(friendQQ))
                {
                    return per.FriendMessage[friendQQ];
                }
            }
            return string.Empty;
        }



    }

    /// <summary>
	/// 一个QQ 的消息记录.
	/// </summary>
    [Serializable]
    public class PerQQMessage
	{
        public PerQQMessage()
        {
            _FriendMessage = new PerFriendMessage();
        }

        private PerFriendMessage  _FriendMessage;
        /// <summary>
        /// 朋友消息记录的字典.
        /// </summary>
        public PerFriendMessage  FriendMessage
        {
            get { return _FriendMessage; }
            set { _FriendMessage = value; }
        }


	}

    /// <summary>
    /// 朋友消息记录.(字典)
    /// 【朋友qq号码,消息记录】
    /// </summary>
    [Serializable]
    public class PerFriendMessage:Dictionary <string,string>
    {
        protected PerFriendMessage(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public PerFriendMessage()
        {

        }

        /// <summary>
        /// 如果key已经存在,则追加.
        /// 不存在,则添加.
        /// </summary>
        /// <param name="key">朋友qq号码</param>
        /// <param name="value">聊天消息</param>
        public new void Add(string key, string value)
        {
            if (base.ContainsKey(key))
            {
                this[key] = string.Format("{0}\r\n{1}", this[key], value);
            }
            else
            {
                base.Add(key, value);
            }
        }
    }

    /// <summary>
    /// 群消息.
    /// </summary>
    [Serializable]
    public class PerGroupMessage
    {

    }
}
