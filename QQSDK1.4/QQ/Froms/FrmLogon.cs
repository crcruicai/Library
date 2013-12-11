using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QQSDK.Systems;
using QQMessage;
using System.ServiceModel;
namespace CWebQQ.Froms
{
    public partial class FrmLogon : Form
    {
        private List<User> _UserList;
        private IService _Service;

        public FrmLogon()
        {
            InitializeComponent();
            //Init();
        }


        private IService GetService()
        {
            return null;
        }


        private void Init()
        {

            string path = Environment.CurrentDirectory + "\\User.Db";
            try
            {
                //_UserList = SystemHelper.Load<List<User>>(path);
                if (_UserList == null)
                {
#if　DEBUG
                    _UserList = new List<User>();
                    User u = new User("admin", "admin", User.UserType.Admin);
                    _UserList.Add(u);
#endif 
                }
            }
            catch (Exception ex)
            {
#if　DEBUG
               _UserList = new List<User>();
                User u = new User("admin", "admin", User.UserType.Admin);
                _UserList.Add(u);
#endif 

            }

        }


        //private User GetUser()
        //{
        //    User user = new User(comUserName.Text, textPassword.Text);
            
        //}

        private User  _User;
        /// <summary>
        /// 
        /// </summary>	
        public User  User
        {
            get { return _User; }
          
        }


        private void buttonOK_Click(object sender, EventArgs e)
        {

            User user = new User(comUserName.Text, textPassword.Text);
            if (_Service.Login(user) == LoginResult.Succeed)
            {
                _Service.CloseUser(user.Name);
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("帐号或密码错误");
            }
        }

        private void CheckUserOld()
        {
            User user = new User(comUserName.Text, textPassword.Text);
            int index = _UserList.IndexOf(user);
            if (index > -1)
            {
                _User = _UserList[index];
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                MessageBox.Show("帐号或密码错误");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void buttonManage_Click(object sender, EventArgs e)
        {
            User user = new User(comUserName.Text, textPassword.Text);
            if (_Service.Login(user) == LoginResult.Succeed)
            {
                user = _Service.GetUser(user.Name);
                if (user.Type == QQMessage.User.UserType.Admin)
                {
                    _UserList = _Service.GetUserList(user.Name);
                    FrmManageUser mUser = new FrmManageUser(_Service,user.Name);
                    this.Visible = false;
                    mUser.ShowDialog();
                   
                    this.Visible = true;
                }
                else
                {
                    MessageBox.Show("对不起,你没有权限");
                }
            }
            else
            {
                MessageBox.Show("帐号或密码错误");
            }
            
            
        }

        private void FrmLogon_Load(object sender, EventArgs e)
        {

            if (_Service == null)
            {
                //string address = "http://192.168.0.156:8000/Derivatives/Calculator";
                string address = Properties.Settings.Default.Address;
                ChannelFactory<IService> factory =
                    new ChannelFactory<IService>(BasicBinding, new EndpointAddress(new Uri(address)));
                _Service = factory.CreateChannel();
            }
        }

        
        private static BasicHttpBinding _BasicBingding;
        /// <summary>
        /// 获取一个绑定.
        /// </summary>
        public static BasicHttpBinding BasicBinding
        {
            get
            {
                if (_BasicBingding == null)
                {
                    _BasicBingding = new BasicHttpBinding("BasicHttpBinding");
                }
                return _BasicBingding;
            }
        }
        /// <summary>
        /// 通讯接口.
        /// </summary>
        public  IService Service
        {
            get
            {
                if (_Service == null)
                {
                    //string address = "http://192.168.0.156:8000/Derivatives/Calculator";
                    string address = Properties.Settings.Default.Address;
                    ChannelFactory<IService> factory =
                        new ChannelFactory<IService>(BasicBinding, new EndpointAddress(new Uri(address)));
                    return factory.CreateChannel();
                }
                return _Service;
            }

        }


    }
}
