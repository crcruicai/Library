
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using CRC.Controls;
using QQSDK.Net;
using CWebQQ.Data;
using QQSDK.Json;
using QQSDK.Systems;
using System.Text.RegularExpressions;

namespace CWebQQ
{
    public partial class FrmMain : Form
    {
        #region 字段与变量
        Manager QQMangers;
        /// <summary>
        /// 是否有回车键按下.
        /// </summary>
        private bool _IsEnterDown = false;
        //AllMessage MessageList = new AllMessage();

        /// <summary>
        /// qq号码,qq消息.
        /// </summary>
        Dictionary<string, string> QQMessage = new Dictionary<string, string>();
        /// <summary>
        /// 当前正在聊天的QQ号码.
        /// </summary>
        private string _CurrentQQ = string.Empty;
        /// <summary>
        /// 消息记录器.
        /// </summary>
        MessageRecord _MsgRecord;
        private Friend _CurrentFriend = null;
        /// <summary>
        /// 历史消息是否与当前聊天对象一致.
        /// </summary>
        private bool _IsHistoryFriend = false;
        /// <summary>
        /// 指示FriendList控件的右键是否可以打开.
        /// </summary>
        private bool _IsFriendOpen;
        /// <summary>
        /// 鼠标右键点击FriendList控件所选中的子项.
        /// </summary>
        private ChatListSubItem _MouseRightClickSubItem;

        /// <summary>
        /// 鼠标右键点击MajorFriendList控件所选中的子项.
        /// </summary>
        private ChatListSubItem _MarjorRightSubItem;
        #endregion

        #region 构造函数

        public FrmMain()
        {
            InitializeComponent();
            InitList();
            QQMangers = new Manager();
            _MsgRecord = new MessageRecord();

            QQMangers.AddFriend += new Action<Data.Friend>(QQMangers_AddFriend);
            QQMangers.ReciveMessage += new Action<string, QQMessageEventArgs>(QQMangers_ReciveMessage);
            QQMangers.SendError += new Action<string, string, string>(QQMangers_SendError);
            QQMangers.AddFatherQQ += QQMangers_AddFatherQQ;

            FriendList.DoubleClickSubItem += new ChatListBox.ChatListEventHandler(FriendList_DoubleClickSubItem);
            FriendList.ContextSubItem += new ChatListBox.ChatListEventHandler(FriendList_ContextSubItem);

          
            MajorFriendList.DoubleClickSubItem += new ChatListBox.ChatListEventHandler(MajorFriendList_DoubleClickSubItem);
            MajorFriendList.ContextSubItem += new ChatListBox.ChatListEventHandler(MajorFriendList_ContextSubItem);

            searchBox1.SearchClick += new EventHandler(searchBox1_SearchClick);
           
            searchBox1.Box.SelectedIndexChanged +=new EventHandler(Box1_SelectedIndexChanged);
            searchBox1.Box.DisplayMember = "NicName";

            searchBox2.SearchClick += new EventHandler(searchBox2_SearchClick);
            searchBox2.Box.SelectedIndexChanged += new EventHandler(Box2_SelectedIndexChanged);
            searchBox2.Box.DisplayMember = "NicName";
          
        }


        /// <summary>
        /// 窗体加载.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //splitContainerEx3.Collapse();
            LoadGroupMessage();
            LoadSettings();

        }

        /// <summary>
        /// 窗体关闭.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveSettings();
        }
       


        #endregion

        #region 属性

        #endregion

        #region 公共函数

        #endregion

        #region 私有函数

        #region MajorFriendList

        /// <summary>
        /// 添加一个
        /// </summary>
        /// <param name="obj"></param>
        private void AddMajorFriend(Friend obj)
        {
            try
            {

                foreach (ChatListItem item in MajorFriendList.Items)
                {
                    if (item.Value == obj.FatherNumber)
                    {
                        ChatListSubItem sitem = new ChatListSubItem(obj.NikeName);
                        sitem.ID = int.Parse(obj.QQNumber);
                        sitem.Info = obj;
                        item.SubItems.Add(sitem);
                        return;
                    }
                }
                //没有找到FatherQQ帐号,意味着没有该项,先添加FatherQQ.再添加一个朋友.
                ChatListItem citem = new ChatListItem(obj.FatherNumber);
                citem.Text = obj.FatherNumber;
                citem.Value = obj.FatherNumber;
                ChatListSubItem subitem = new ChatListSubItem(obj.NikeName);
                subitem.Info = obj;
                citem.SubItems.Add(subitem);
                MajorFriendList.Items.Add(citem);
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
        }

        /// <summary>
        /// 重点对象 列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MajorFriendList_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            //获取历史消息.
            string fatherqq = e.SelectSubItem.Info.FatherNumber;
            string qqnum = e.SelectSubItem.Info.QQNumber;
            if (_CurrentFriend != null &&
                _CurrentFriend.FatherNumber == fatherqq && _CurrentFriend.QQNumber == qqnum)
            {
                _IsHistoryFriend = true;
            }
            else
            {
                _IsHistoryFriend = false;
            }
            richMessageHistory.Text = _MsgRecord.FindContent(fatherqq, qqnum);

            //显示历史消息.
            splitContainerEx3.Expand();
        }

        /// <summary>
        /// 重点对象朋友列表 右键菜单.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MajorFriendList_ContextSubItem(object sender, ChatListEventArgs e)
        {

            _MarjorRightSubItem = e.SelectSubItem;
            contextMajorFriend.Show(Cursor.Position);

        }



        /// <summary>
        /// 将朋友设置为重点对象.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMajor_Click(object sender, EventArgs e)
        {
            if (_MouseRightClickSubItem != null)
                AddMajorFriend(_MouseRightClickSubItem.Info);
        }

        /// <summary>
        /// 删除重点对象.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMajorDelete_Click(object sender, EventArgs e)
        {
            try
            {
                _MarjorRightSubItem.OwnerListItem.SubItems.Remove(_MarjorRightSubItem);

            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
        }
        #endregion

        #region FriendList

        /// <summary>
        /// 朋友列表 右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FriendList_ContextSubItem(object sender, ChatListEventArgs e)
        {
            _IsFriendOpen = true;
            _MouseRightClickSubItem = e.SelectSubItem;
            contextFriendList.Show(Cursor.Position);
            _IsFriendOpen = false;
        }

        /// <summary>
        /// 朋友列表双击子项,切换聊天对象,并显示QQ消息记录.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FriendList_DoubleClickSubItem(object sender, ChatListEventArgs e)
        {
            _CurrentFriend = e.SelectSubItem.Info;
            _CurrentQQ = e.SelectSubItem.OwnerListItem.Value;
            toolShowLable.Text = "当前你正在交谈的QQ:{1}({0})".FormatWith(_CurrentFriend.QQNumber, _CurrentFriend.NikeName);

            textMessage.Text = _MsgRecord.FindContent(_CurrentQQ, _CurrentFriend.QQNumber);
            textMessage.SelectionStart = textMessage.Text.Length;
            textMessage.ScrollToCaret();
        }


       

        #endregion

        #region QQMangers
        void QQMangers_AddFatherQQ(string fatherQQ, string nickName)
        {
            FriendList.QueueInvoke(() =>
            {
                //当获取到QQ昵称时,在朋友列表添加 一个QQ帐号.
                ChatListItem citem = new ChatListItem(fatherQQ);
                citem.Text = string.Format("{0}({1})", fatherQQ, nickName);
                citem.Value = fatherQQ;
                FriendList.Items.Add(citem);

                //更新 重点qq对象.
                if (_Settings.MajorFriendMap.ContainsKey(fatherQQ))
                {
                    FriendCollection coll = _Settings.MajorFriendMap[fatherQQ];
                    foreach (var item in coll)
                    {
                        AddMajorFriend(item);
                    }
                }
            }
                );
        }

        void QQMangers_SendError(string arg1, string arg2, string arg3)
        {

        }

     
       
        /// <summary>
        /// QQ消息接收,分发处理.
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="e"></param>
        void QQMangers_ReciveMessage(string arg1, QQMessageEventArgs e)
        {
            switch (e.PollMsgType)
            {
                case QQSDK.Json.PollType.Tips:
                    break;
                case QQSDK.Json.PollType.System:
                    break;
                case QQSDK.Json.PollType.Friend:
                    DealFriend(arg1, (FriendMessageData)e.Data);
                    break;
                case QQSDK.Json.PollType.Group:
                    break;
                case QQSDK.Json.PollType.KickMessage:
                    this.QueueInvoke<string, KickMessage>((id, m) =>
                        {
                            if (MessageBox.Show(string.Format("你的QQ帐号:{0} {1} {2},\r\n是否重新登录,选择【是】,重新登录.\r\n选择【否】,将关闭QQ.",
                                arg1, m.Reason, m.ShowReason), "警告! QQ下线通知", MessageBoxButtons.YesNo)
                                == System.Windows.Forms.DialogResult.Yes)
                            {
                                QQMangers.ReLogin(id);
                            }
                            else
                            {
                                foreach (ChatListItem  item in FriendList .Items)
                                {
                                    if (item.Value == id)
                                    {
                                        FriendList.Items.Remove(item);
                                    }
                                }
                                QQMangers.PersonList.SetNotLogin(id);
                                QQMangers.RemoveQQ(id);
                            }
                        }, arg1, (KickMessage)e.Data);


                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 添加朋友消息,处理.
        /// </summary>
        /// <param name="obj"></param>
        void QQMangers_AddFriend(Data.Friend obj)
        {
            try
            {
                FriendList.QueueInvoke<Friend>(AddFriend, obj);
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                
            }
            
        }

        /// <summary>
        /// 处理朋友的消息.
        /// </summary>
        /// <param name="qq"></param>
        /// <param name="data"></param>
        void DealFriend(string qq, FriendMessageData data)
        {
            try
            {
                if (QQMangers.FriendList.ContainsKey(qq))
                {
                    FriendCollection c = QQMangers.FriendList[qq];
                    Friend f = c.FindByUin(data.FromUin);
                    string qqnumber;
                    if (f != null)
                    {
                        qqnumber = f.QQNumber;
                        //闪动 qq

                        //转换文本.
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in data.Content)
                        {
                            sb.Append(item);
                        }
                        string text = sb.ToString();
                        //
                        listBoxNewMessage.QueueInvoke<ChatMessage>((item) => MessageIn(item),
                            new ChatMessage() { Title = "{1}({0})".FormatWith(qqnumber, f.NikeName), Text = text, Friend = f, Uin = qq });
                        text = "{3}({2}) {1}\r\n{0}".FormatWith(text, DateTime.Now.ToString(), qqnumber, f.NikeName);
                        if (_CurrentQQ == qq && _CurrentFriend.Uin == data.FromUin)
                        {
                            textMessage.QueueInvoke<string>((item) =>
                            {
                                textMessage.AppendText(item);
                                textMessage.ScrollToCaret();
                            }, text);

                            if (_IsHistoryFriend)
                            {
                                richMessageHistory.QueueInvoke<string>((item) =>
                                {
                                    richMessageHistory.AppendText(item);
                                }, text);
                            }
                        }


                        _MsgRecord.Add(qq, qqnumber, text);

                    }
                }
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
           
        }


        /// <summary>
        /// 处理添加朋友的界面显示.
        /// </summary>
        /// <param name="obj"></param>
        private void AddFriend(Friend obj)
        {
            try
            {
                //找到FatherQQ,再添加一个朋友.
                foreach (ChatListItem item in FriendList.Items)
                {
                    if (item.Value == obj.FatherNumber)
                    {
                        ChatListSubItem sitem = new ChatListSubItem(obj.NikeName);
                        //sitem.ID = int.Parse(obj.QQNumber);
                        sitem.DisplayName = obj.QQNumber;
                        
                        sitem.Info = obj;
                        item.SubItems.Add(sitem);
                        return;
                    }
                }

                //没有找到FatherQQ帐号,意味着没有该项,先添加FatherQQ.再添加一个朋友.
                ChatListItem citem = new ChatListItem(obj.FatherNumber);
                citem.Text = obj.FatherNumber;
                citem.Value = obj.FatherNumber;
                
                ChatListSubItem subitem = new ChatListSubItem(obj.NikeName);
                subitem.DisplayName = obj.QQNumber;
                subitem.Info = obj;
                citem.SubItems.Add(subitem);
                FriendList.Items.Add(citem);

                //更新 重点qq对象.
                if (_Settings.MajorFriendMap.ContainsKey(obj.FatherNumber))
                {
                    FriendCollection coll = _Settings.MajorFriendMap[obj.FatherNumber];
                    foreach (var item in coll)
                    {
                        AddMajorFriend(item);
                    }
                }
            }
            catch(Exception ex)
            {
                Loger.WriteLog(ex);
            }

        }

        #endregion QQMangers

        #region 消息发送
        /// <summary>
        /// 发送指定的消息.
        /// </summary>
        /// <param name="text"></param>
        private void SendMessage(string text)
        {
            if (_CurrentFriend != null)
            {
                string newtext = text.Replace("\r\n", "\\\\r\\\\n");
                QQMangers.SendMessage(_CurrentQQ, _CurrentFriend.Uin, newtext);
                string str = string.Format("{0} {1}\r\n{2}\r\n", _CurrentQQ, DateTime.Now.ToString(), text);
                _MsgRecord.Add(_CurrentQQ, _CurrentFriend.QQNumber, str);
                if (_IsHistoryFriend)
                {
                    richMessageHistory.AppendText(str);
                }
                textMessage.AppendText(str);
                textMessage.ScrollToCaret();
            }
        }

        #endregion

        #endregion

        #region 自动回复消息加载

        /// <summary>
        /// 加载自定义消息.
        /// </summary>
        private void LoadGroupMessage()
        {
            string path = Application.StartupPath + "\\MS\\GroupMessage.db";
            Data.Group g = SystemHelper.Load<Data.Group>(path);
            if (g == null) g = new Data.Group();
            groupListBox1.Items.Clear();
            foreach (var item in g)
            {
                TextGroupItem  gitem = new TextGroupItem ();
                gitem.Text = item.GroupName;
                foreach (var sitme in item.GroupList)
                {
                    TextSubItem subitem = new TextSubItem(sitme);
                    gitem.SubItems.Add(subitem);
                }
                groupListBox1.Items.Add(gitem);
            }

        }

        /// <summary>
        /// 初始化自定义消息框.
        /// </summary>
        private void InitGroupMessage()
        {
            groupListBox1.DoubleClickSubItem += new EventHandler<GroupListBoxEventArgs>(groupListBox1_DoubleClickSubItem);
        }

        /// <summary>
        /// 双击自定义消息框,自动发送.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void groupListBox1_DoubleClickSubItem(object sender, GroupListBoxEventArgs e)
        {
            SendMessage(e.SelectSubItem.Text);
        }


        #endregion

        #region 消息队列框

        /// <summary>
        /// QQ最新消息进入控制函数.
        /// </summary>
        /// <param name="cm"></param>
        private void MessageIn(ChatMessage cm)
        {
            int index = listBoxNewMessage.Items.IndexOf(cm);
            if(index>-1)
            {
                listBoxNewMessage.Items.RemoveAt(index);
                listBoxNewMessage.Items.Insert(0, cm);
                
            }
            else
            {
                listBoxNewMessage.Items.Insert(0,cm);
            }
            //根据用户的要求.当消息队列大于100项时,删除.
            if (listBoxNewMessage.Items.Count > 100)
            {
                listBoxNewMessage.Items.RemoveAt(100);
            }

        }

        /// <summary>
        /// 选择消息,切换聊天对象和显示消息.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listBoxNewMessage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChatMessage cm = listBoxNewMessage.SelectedItem as ChatMessage;
            if (cm != null)
            {
                //查找朋友消息.
                textMessage.Text = _MsgRecord.FindContent(cm.Friend.FatherNumber, cm.Friend.QQNumber);
                textMessage.SelectionStart = textMessage.Text.Length;
                textMessage.ScrollToCaret();
                _CurrentFriend = cm.Friend;
                _CurrentQQ = cm.Uin;
                toolShowLable.Text = "当前你正在交谈的QQ:{1}({0})".FormatWith(_CurrentFriend.QQNumber, _CurrentFriend.NikeName);

                //移除该选项.
                //listBoxNewMessage.Items.RemoveAt(listBoxNewMessage.SelectedIndex);
            }
        }
        #endregion

        #region 触发的事件

        #region 菜单栏

        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutBox1 box = new AboutBox1();
            box.ShowDialog();
        }

        /// <summary>
        /// 设置自定义消息.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemAutoMessage_Click(object sender, EventArgs e)
        {
            FrmCostomMessage cm = new FrmCostomMessage();
            if (cm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               
            }

            LoadGroupMessage();
        }


        /// <summary>
        /// 登录QQ.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemLoginQQ_Click(object sender, EventArgs e)
        {
            //if (QQMangers.QQMap.Count > 3) MenuItemLoginQQ.Enabled = false;
#if DEBUG
            Person p = new Person() { QQ = "398117542", Password = "crcruicai392759" };
            QQMangers.Login(p);
#else

            QQMangers.Login(null);
#endif

        }
        /// <summary>
        /// 多个QQ登录.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItemMulLoginQQ_Click(object sender, EventArgs e)
        {
            FrmMulLogon logon = new FrmMulLogon(QQMangers);
            this.Visible = false;
            logon.ShowDialog();
            this.Visible = true;
        }

        #endregion

        #region 消息发送事件处理

        /// <summary>
        /// 捕捉回车键,发送消息.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textMyMessage_KeyDown(object sender, KeyEventArgs e)
        {
            //我的聊天消息,当按下回车键,如果同时按下ctrl,则忽略
            if (!e.Control && e.KeyCode == Keys.Enter)
            {
                _IsEnterDown = true;
                if (textMyMessage.Text != "") SendMessage();//发送消息.
            }

        }

        /// <summary>
        /// 发送消息后,清空发送消息框.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textMyMessage_KeyUp(object sender, KeyEventArgs e)
        {
            if (_IsEnterDown)
            {
                _IsEnterDown = false;
                textMyMessage.Clear();
            }
        }

        /// <summary>
        /// 将消息发送框中的消息发送出去.
        /// </summary>
        private void SendMessage()
        {
            SendMessage(textMyMessage.Text);
            textMyMessage.Text = "";
        }


        /// <summary>
        /// 发送消息.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }
        /// <summary>
        /// 选择自定义消息,加入消息发送框.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void groupListBox1_DoubleClickSubItem_1(object sender, GroupListBoxEventArgs e)
        {
            textMyMessage.Text = e.SelectSubItem.Text;
        }

        #endregion


       

        #endregion

        #region 聊天消息加载与保存
        /// <summary>
        /// 保存消息.
        /// </summary>
        private  void SaveMessage()
        {
            string path = Application.StartupPath + "\\Message.db";
            SystemHelper.Save(path, _MsgRecord);
        }

        /// <summary>
        /// 加载消息记录.
        /// </summary>
        private void LoadMessage()
        {
            string path = Application.StartupPath + "\\Message.db";
            try
            {
                _MsgRecord = SystemHelper.Load<MessageRecord>(path);
            }
            catch (Exception e)
            {
                _MsgRecord = new MessageRecord();
                
            }
        }

        private void GetMajorFriend()
        {
            _Settings.MajorFriendMap.Clear();
            FriendCollection coll;
            foreach (ChatListItem  item in MajorFriendList .Items )
            {
                coll = new FriendCollection();
                foreach (ChatListSubItem  sitem in item.SubItems )
                {
                    coll.Add(sitem.Info);
                }
                //
                if (_Settings.MajorFriendMap.ContainsKey(item.Value))
                {
                    _Settings.MajorFriendMap[item.Value]= coll;
                }
                else
                {
                    _Settings.MajorFriendMap.Add(item.Value, coll);
                }
              
            }

        }

        private Settings _Settings;
        private void SaveSettings()
        {
            _Settings.PersonList = QQMangers.PersonList;
            GetMajorFriend();
            string path = Application.StartupPath + "\\Settings.db";
            SystemHelper.Save(path,_Settings);
        }

        private void LoadSettings()
        {
            string path = Application.StartupPath + "\\Settings.db";
            try
            {
               _Settings  = SystemHelper.Load <Settings> (path);
               _Settings.PersonList.SetNotLogin();
               QQMangers.PersonList = _Settings.PersonList;
               _MsgRecord = _Settings.Message;
            }
            catch (Exception e)
            {
                _Settings = new Settings();

            }
        }

        #endregion

        #region SearchBox1
        void searchBox1_SearchClick(object sender, EventArgs e)
        {
            searchBox1.Box.Items.Clear();
            string text = searchBox1.Box.Text;
            int i;
            if (int.TryParse(text, out i))
            {
                ChatListSubItem[] nums = MajorFriendList.GetSubItemsByDisplayName((id) =>
                {
                    return id == text;
                });
                searchBox1.Box.Items.AddRange(nums);
            }
            ChatListSubItem[] items = MajorFriendList.GetSubItemsByNickName
                ((item) =>
                {
                    return Regex.IsMatch(item, string.Format("{0}*", text));
                });

            searchBox1.Box.Items.AddRange(items);
            if (searchBox1.Box.Items.Count > 0)
            {
                searchBox1.Box.SelectedIndex = 0;
                MajorFriendList.SelectSubItem = (ChatListSubItem)searchBox1.Box.Items[0];
            }
        }


        void Box1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChatListSubItem item = searchBox1.Box.SelectedItem as ChatListSubItem;
            if (item != null)
            {
                MajorFriendList.SelectSubItem = item;
            }
        }

        #endregion

        #region SearchBox2
        void Box2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChatListSubItem item = searchBox2.Box.SelectedItem as ChatListSubItem;
            if (item != null)
            {
                FriendList.SelectSubItem = item;
            }
        }

        void searchBox2_SearchClick(object sender, EventArgs e)
        {
            searchBox2.Box.Items.Clear();
            int i;
            string text = searchBox2.Box.Text;
            if (int.TryParse(searchBox2.Box.Text, out i))
            {
                ChatListSubItem[] nums = FriendList.GetSubItemsByDisplayName((id) =>
                {
                    return id == text;
                });
                searchBox2.Box.Items.AddRange(nums);
            }
            ChatListSubItem[] items = FriendList.GetSubItemsByNickName
                ((item) =>
                {
                    return item.IndexOf(text) > -1;
                });
            searchBox2.Box.Items.AddRange(items);
            if (searchBox2.Box.Items.Count > 0)
            {
                searchBox2.Box.SelectedIndex = 0;
                FriendList.SelectSubItem = (ChatListSubItem)searchBox2.Box.Items[0];
            }

        }

        #endregion

    }
 
}
