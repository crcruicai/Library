using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CWebQQ.Data;
using QQSDK.Net;
using QQSDK.Json;
using CWebQQ.Froms;
using System.Diagnostics;
using System.Windows.Forms;
using QQSDK.Systems;
using System.Threading;

namespace CWebQQ
{
    /// <summary>
    /// QQ管理者.
    /// </summary>
    public class Manager
    {
        #region 辅助类
        /// <summary>
        /// 发送消息队列.(在多线程是安全的)
        /// </summary>
        class SendQueue:Queue<SendMsg>
        {
            object _Lock;
            public SendQueue ()
	        {
                _Lock = new object();

	        }
            public bool Dequeue(out SendMsg sm)
            {
                lock (_Lock )
                {
                    if (this.Count > 0)
                    {
                        sm = base.Dequeue();
                        return true;
                    }
                    else
                    {
                        sm = new SendMsg();
                        return false;
                    }
                }

            }

            public new void Enqueue(SendMsg item)
            {
                lock (_Lock )
                {
                    base.Enqueue(item);
                }
            }
        }

        /// <summary>
        /// 发送消息的描述.
        /// </summary>
        struct SendMsg
        {
            public SendMsg(string friendqq,string context)
            {
                
                FriendQQ = friendqq;
                Context = context;

            }
            
            /// <summary>
            /// 要发送到QQ的Uin
            /// </summary>
            public string FriendQQ;
            /// <summary>
            /// 要发送到内容.
            /// </summary>
            public string Context;

        }
        #endregion

        #region 字段与变量
        /// <summary>
        /// QQ 通信代理者 列表 【qq号码,QQ代理者】
        /// </summary>
        Dictionary<string, WebQQ> _QQMap;
        /// <summary>
        /// 多个朋友列表 【qq号码,该qq号码的朋友列表】
        /// </summary>
        Dictionary<string, FriendCollection> _FriendMap;

        /// <summary>
        /// 重点QQ列表.【qq号码,该qq号码的朋友列表】
        /// </summary>
        Dictionary<string, FriendCollection> _MajorFriendMap;
        //PersonCollection _PersonList;
        /// <summary>
        /// 发送队列 列表.
        /// </summary>
        Dictionary<string, SendQueue> _SendMap = new Dictionary<string, SendQueue>();
        /// <summary>
        /// 发送对象锁.
        /// </summary>
        object _SendLock = new object();
        #endregion

        #region 事件与委托
       
        /// <summary>
        /// 添加一个好友.
        /// </summary>
        public event Action<Friend> AddFriend;

        public event Action<string, string> AddFatherQQ;
        /// <summary>
        /// 接收到指定消息的时间.
        /// 【qq号码,消息】
        /// </summary>
        public event Action<string, QQMessageEventArgs> ReciveMessage;

        /// <summary>
        /// 发送错误.
        /// </summary>
        public event Action<string, string, string> SendError;

        #endregion

        #region 构造函数

        public Manager()
        {
            _QQMap = new Dictionary<string, WebQQ>();
            _FriendMap = new Dictionary<string, FriendCollection>();
            //TODO:读取列表.
            _PersonList = new PersonCollection();
            
        }


        #endregion

        #region 属性

        /// <summary>
        /// 所有的qq的朋友列表.【登录的qq号码,该qq的朋友列表】
        /// </summary>
        public Dictionary<string, FriendCollection> FriendList
        {
            get{return _FriendMap ;}
        }


      
        /// <summary>
        /// QQ通信代理者列表.【qq号码,qq代理者】
        /// </summary>
        public Dictionary <string,WebQQ> QQMap
        {
            get { return _QQMap; }
        }
        

        private PersonCollection  _PersonList;
        /// <summary>
        /// 登录的qq列表(包括未登录的)
        /// </summary>
        public PersonCollection  PersonList
        {
            get { return _PersonList; }
            set { _PersonList = value; }
        }



        #endregion

        #region 公共函数

        #region 登录与登出
        /// <summary>
        /// QQ登录.如果登录成功,返回更新后的Person,如果失败,就返回null.
        /// <para>注意,该函数将显示登录对话框.</para>
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person Login(Person person)
        {
            try
            {
                FrmLogin login = new FrmLogin(person);
                if (login.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string qq = login.WebQQ.MyQQNumber;
                    if (!_QQMap.ContainsKey(qq))
                    {
                        _QQMap.Add(qq, login.WebQQ);
                        login.WebQQ.ReciveMessage += new EventHandler<QQMessageEventArgs>(WebQQ_ReciveMessage);
                    }
                    else
                    {
                        _QQMap[qq] = login.WebQQ;
                    }
                    //保存QQ用户消息.
                    if (login.QQPerson.IsRecord)
                    {

                        _PersonList.AddOrUpdate(login.QQPerson);
                    }

                    GetFriendTask(qq);
                }
                return login.QQPerson;
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
            return null;

        }
        /// <summary>
        /// 移除在线的QQ.
        /// </summary>
        /// <param name="qqNumber"></param>
        public void RemoveQQ(string qqNumber)
        {
            if (_QQMap.ContainsKey(qqNumber))
            {
                _QQMap.Remove(qqNumber);
            }
        }

        /// <summary>
        /// 重新登录.如果登录成功,返回true,否则返回false.
        /// </summary>
        /// <param name="qqNumber">指定要重新登录的QQ号码</param>
        /// <returns></returns>
        public bool  ReLogin(string qqNumber)
        {
            if (_QQMap.ContainsKey(qqNumber))
            {
                _QQMap[qqNumber].ReLogin();
                return true;
            }
            return false;
        }

        public bool LoginOut(string qqNumber)
        {
            if (_QQMap.ContainsKey(qqNumber))
            {
                _QQMap[qqNumber].LoginOut();
                _QQMap.Remove(qqNumber);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 所有QQ全部登出.
        /// </summary>
        /// <returns></returns>
        public bool LoginOutAll()
        {
            foreach (var item in _QQMap )
            {
                item.Value.LoginOut();
            }
            _QQMap.Clear();
            return true;
        }

        #endregion

        #region 发送消息

        /// <summary>
        /// 发送消息.
        /// </summary>
        /// <param name="myQQNumber">要发送消息的QQ.</param>
        /// <param name="friendUin">要接受消息的QQ的Uin</param>
        /// <param name="content">消息内容.</param>
        /// <returns></returns>
        public void SendMessage(string myQQNumber, string friendUin, string content)
        {

            // 2013-11-05 更新
            // 新算法,为了保证发送内容的有序性,采用逐条消息发送.
            SendMsg sm = new SendMsg(friendUin, content);
            AddSendQueue(myQQNumber, sm);

            #region 原算法
            // 原算法,不能保证内容的有序性
            //Task<bool> tast = new Task<bool>(() =>
            //{
            //    if (_QQMap.ContainsKey(myQQNumber))
            //    {
            //        WebQQ qq = _QQMap[myQQNumber];
            //        Debug.WriteLine("{0} start Time:{1} content {2}",myQQNumber , DateTime.Now.ToString(),content );
            //        qq.SendMessage(friendUin, content, new Font("宋体", 12F), Color.Black);
            //        Debug.WriteLine("{0} end Time:{1} content {2}", myQQNumber, DateTime.Now.ToString(), content);
            //    }
            //    return false;
            //}
            //);
            //tast.Start ();
            #endregion

        }

        #endregion

        #region 获取好友

        /// <summary>
        /// 启动线程获取好友列表.
        /// </summary>
        /// <param name="qqNumber">指定好友的QQ号码.</param>
        public void GetFriendTask(string qqNumber)
        {
            Task task = new Task(() =>
            {
                
                GetFriend(qqNumber);
            });

            task.Start();
        }
        /// <summary>
        /// 获取所有好友列表.
        /// </summary>
        public void GetFriends()
        {
            Task task = new Task(() =>
            {
                _FriendMap.Clear();
                foreach (var item in _QQMap.Keys)
                {
                    GetFriend(item);
                }
            }
           );

            task.Start();
        }

        /// <summary>
        /// 获取好友列表.
        /// </summary>
        /// <param name="qqNumber">指定QQ号码</param>
        public void GetFriend(string qqNumber)
        {
            try
            {
                FriendCollection list;
                if (_FriendMap.ContainsKey(qqNumber))
                {
                    list = _FriendMap[qqNumber];
                }
                else
                {
                    list = new FriendCollection();
                    _FriendMap.Add(qqNumber, list);
                }
                if (_QQMap.ContainsKey(qqNumber))
                {
                    WebQQ qq = _QQMap[qqNumber];
                    
                    if (AddFatherQQ != null) AddFatherQQ(qqNumber, qq.NickName);
                    UserFriend uf = qq.GetUserFriends();
                    if (uf != null && uf.Result != null && uf.Result.Info != null)
                    {
                        foreach (var item in uf.Result.Info)
                        {
                            string number = qq.GetQQNumber(item.Uin.ToString());
                            Friend f = new Friend(qqNumber, number)
                            {
                                Uin = item.Uin.ToString(),
                                NikeName = item.Nick,
                            };
                            list.Add(f);
                            if (AddFriend != null)
                            {
                                AddFriend(f);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
        }

        #endregion

        #endregion

        #region 私有函数(发送与接收)

        /// <summary>
        /// 接收QQ消息.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void WebQQ_ReciveMessage(object sender, QQMessageEventArgs e)
        {
            if (ReciveMessage != null)
            {
                WebQQ qq = sender as WebQQ;
                if (qq != null)
                {
                    ReciveMessage(qq.MyQQNumber, e);
                }
            }
        }

        /// <summary>
        /// 自动执行发送消息的任务.
        /// </summary>
        /// <param name="myQQ"></param>
        private void StartTask(string myQQ)
        {
            if (_QQMap.ContainsKey(myQQ) && _SendMap.ContainsKey(myQQ))
            {
                SendQueue sq = _SendMap[myQQ];
                WebQQ qq = _QQMap[myQQ];
                Task tast = new Task(() =>
                {
                    SendMsg sm;
                    while (true)
                    {
                        if (sq.Dequeue(out sm))
                        {
                            bool result = false;
                            for (int i = 0; i < 3; i++)
                            {
                                if (qq.SendMessage(sm.FriendQQ, sm.Context, new Font("宋体", 12F), Color.Black))
                                {
                                    result = true;
                                    break;
                                }
                            }
                            if (!result)
                            {
                                if (SendError != null) SendError(myQQ, sm.FriendQQ, sm.Context);
                            }
                        }
                        Thread.Sleep(200);
                    }
                }
                );

                tast.Start();
            }
        }

        /// <summary>
        /// 将消息添加到消息发送队列.
        /// </summary>
        /// <param name="myQQ"></param>
        /// <param name="sm"></param>
        private void AddSendQueue(string myQQ, SendMsg sm)
        {
            lock (_SendLock)
            {
                if (_SendMap.ContainsKey(myQQ))
                {
                    SendQueue sq = _SendMap[myQQ];
                    sq.Enqueue(sm);
                }
                else
                {
                    SendQueue sq = new SendQueue();
                    sq.Enqueue(sm);
                    _SendMap.Add(myQQ, sq);
                    StartTask(myQQ);
                }
            }

        }


        #endregion

       



       
    }
}
