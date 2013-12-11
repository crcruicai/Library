using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QQSDK.Net;

namespace QQSDK.Json
{

    /// <summary>
    /// 指示消息的类型.
    /// </summary>
    public enum PollType
    {
        /// <summary>
        /// 提示消息.
        /// </summary>
        Tips,
        /// <summary>
        /// 系统消息.
        /// </summary>
        System,
        /// <summary>
        /// 好友消息.
        /// </summary>
        Friend,
        /// <summary>
        /// 群消息.
        /// </summary>
        Group,
        /// <summary>
        /// 掉线消息.
        /// </summary>
        KickMessage,
        /// <summary>
        /// QQ状态改变消息
        /// </summary>
        StatusChange
        

    }

    /// <summary>
    /// 
    /// </summary>
    public  class Message
    {

    }

    public abstract class MessageData:Message,IComparer <MessageData>
    {

        #region 字段与变量

        #endregion

        #region 构造函数
        public MessageData()
        {
            Content = new List<string>();
        }
        #endregion

        #region 属性





        private string _FromUin;
        /// <summary>
        /// 
        /// </summary>
        public string FromUin
        {
            get { return _FromUin; }
            set { _FromUin = value; }
        }


        private string  _ToUin;
        /// <summary>
        /// 
        /// </summary>
        public string  ToUin
        {
            get { return _ToUin; }
            set { _ToUin = value; }
        }


        private List<string> _Content;
        /// <summary>
        /// 
        /// </summary>
        public List<string> Content
        {
            get { return _Content; }
            set { _Content = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Drawing.Font Font { get; set; }

        /// <summary>
        ///
        /// </summary>
        public System.Drawing.Color Color { get; set; }

       

        #endregion

        #region 公共函数
        public virtual void Parste(string text)
        {
           

        }


        #endregion

        #region 私有函数

        #endregion



        #region IComparer<MessageData> 成员

        public abstract int Compare(MessageData x, MessageData y);
        

        #endregion
    }




    public class FriendMessageData : MessageData
    {
        public FriendMessageData()
        {

        }

        #region  属性

        private string _MsgID;
        /// <summary>
        /// 
        /// </summary>
        public string MsgID
        {
            get { return _MsgID; }
            set { _MsgID = value; }
        }

        private string _MsgID2;
        /// <summary>
        /// 
        /// </summary>
        public string MsgID2
        {
            get { return _MsgID2; }
            set { _MsgID2 = value; }
        }

        private int _MsgType;
        /// <summary>
        /// 
        /// </summary>
        public int MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
        }

        private string  _ReplyIP;
        /// <summary>
        /// 
        /// </summary>
        public string  ReplyIP
        {
            get { return _ReplyIP; }
            set { _ReplyIP = value; }
        }

       




        #endregion




        public override int Compare(MessageData x, MessageData y)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupMessageData : MessageData
    {
        public GroupMessageData()
        {

        }

        #region 属性
        private string _MsgID;
        /// <summary>
        /// 
        /// </summary>
        public string MsgID
        {
            get { return _MsgID; }
            set { _MsgID = value; }
        }

        private string _MsgID2;
        /// <summary>
        /// 
        /// </summary>
        public string MsgID2
        {
            get { return _MsgID2; }
            set { _MsgID2 = value; }
        }

        private int _MsgType;
        /// <summary>
        /// 
        /// </summary>
        public int MsgType
        {
            get { return _MsgType; }
            set { _MsgType = value; }
        }

        private string _ReplyIP;
        /// <summary>
        /// 
        /// </summary>
        public string ReplyIP
        {
            get { return _ReplyIP; }
            set { _ReplyIP = value; }
        }

        private string _GroupCode;
        /// <summary>
        /// 群的Uin
        /// </summary>	
        public string GroupCode
        {
            get { return _GroupCode; }
            set { _GroupCode = value; }
        }

        private string _SendUin;
        /// <summary>
        /// 发送者的Uin
        /// </summary>	
        public string SendUin
        {
            get { return _SendUin; }
            set { _SendUin = value; }
        }

        private string _Seq;
        /// <summary>
        /// 
        /// </summary>	
        public string Seq
        {
            get { return _Seq; }
            set { _Seq = value; }
        }

        private string _InfoSeq;
        /// <summary>
        /// 
        /// </summary>	
        public string InfoSeq
        {
            get { return _InfoSeq; }
            set { _InfoSeq = value; }
        }


        #endregion

        public override int Compare(MessageData x, MessageData y)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 掉线的消息.
    /// </summary>
    public class KickMessage:Message 
    {
        #region 字段与变量

        #endregion

        #region 构造函数

        #endregion

        #region 属性
        private string _Way;
        /// <summary>
        /// 
        /// </summary>
        public string Way
        {
            get { return _Way; }
            set { _Way = value; }
        }

        private int _ShowReason;
        /// <summary>
        /// 
        /// </summary>
        public int ShowReason
        {
            get { return _ShowReason; }
            set { _ShowReason = value; }
        }

        private string _Reason;
        /// <summary>
        /// 
        /// </summary>
        public string Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }


        #endregion

    }

    public class StatusChange : Message
    {
        private string _Uin;
        /// <summary>
        /// 
        /// </summary>	
        public string Uin
        {
            get { return _Uin; }
            set { _Uin = value; }
        }

        private OnlineStatus _Status;
        /// <summary>
        /// 
        /// </summary>	
        public OnlineStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private int _ClientType;
        /// <summary>
        /// 
        /// </summary>	
        public int ClientType
        {
            get { return _ClientType; }
            set { _ClientType = value; }
        }


    }
}
