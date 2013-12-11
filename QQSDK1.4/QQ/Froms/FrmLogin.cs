using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CWebQQ.Data;
using QQSDK.Net;
using System.Threading.Tasks;

namespace CWebQQ.Froms
{
    /// <summary>
    /// QQ 登录窗体.
    /// </summary>
    public partial class FrmLogin : Form
    {
        #region 字段与变量
        //private CHttpWeb _HttpWeb;
        private WebQQ _WebQQ;

        private Person _Person = null;
        /// <summary>
        /// 是否需要验证码.
        /// </summary>
        bool IsNeedCode = false;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent();
            //初始化 对象.
            _Person =null;
            _WebQQ = new WebQQ();
            IsNeedCode = true;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="password"></param>
        public FrmLogin(string number, string password)
        {
            InitializeComponent();
            //初始化 对象.
            _Person = null;
            _WebQQ = new WebQQ();
            textNumber.Text = number;
            textPassword.Text = password;
        }


        public FrmLogin(Person person)
        {
            InitializeComponent();
            _WebQQ = new WebQQ();
            IsNeedCode = true;
            //初始化 对象.
            if (person != null)
            {
                _Person = person;
                textNumber.Text = person.QQ;
                textPassword.Text = person.Password;
                checkBox1.Checked = person.IsRecord;
                //IsNeedCode = false;
                //GetLoginCodePicture(person.QQ);

            }
        }
        #endregion

        #region 属性

        #endregion

        #region 公共函数

        #endregion

        #region 私有函数

       




        /// <summary>
        /// 保存密码.
        /// </summary>
        private void SavePassword()
        {
            ////记住密码的功能.

            //if (checkBox1.Checked)
            //{
            //    if (PublicSouces.Settings.PersonList.Find(_Person.QQ) == null)
            //    {
            //        PublicSouces.Settings.PersonList.Add(_Person);
            //    }

            //}
        }

        private void Logon()
        {
            string number = textNumber.Text.Trim();
            string password = textPassword.Text.Trim();
            string code = textCheckCode.Text.Trim();

            LoginResult ret= _WebQQ.Login(number, password, code);
            switch (ret)
            {
                case LoginResult.QQNumberError:
                    MessageBox.Show("您的帐号暂时无法登录!\r\n请到 http://aq.qq.com/007 恢复正常使用");
                    //GetLoginCodePicture(number);
                    textCheckCode.Text = "";
                    break;
                case LoginResult.PasswordError:
                    MessageBox.Show("密码错误!");
                    GetLoginCodePicture(number);
                    textCheckCode.Text = "";
                    break;
                case LoginResult.LoginSucceed:
                    //保存密码和消息.
                    if (_Person == null)
                    {
                        _Person = new Person();
                    }
                    _Person.QQ = textNumber.Text;
                    _Person.Password = textPassword.Text;
                    _Person.State = comState.Text;
                    _Person.IsRecord = checkBox1.Checked;
                    SavePassword();
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    break;
                case LoginResult.LoginFail:
                    MessageBox.Show("登录失败!");
                    GetLoginCodePicture(number);
                    textCheckCode.Text = "";
                    break;
                case LoginResult.VerifyCodeError:
                    MessageBox.Show("验证码错误!");
                    GetLoginCodePicture(number);
                    textCheckCode.Text = "";
                    break;
                case LoginResult.NeedVerifyCode:
                    GetLoginCodePicture(number);
                    textCheckCode.Text = "";
                    break;
                case LoginResult.NotVerifyCode:
                    break;
                default:
                    MessageBox.Show("您输入的账号或密码有误" + ret);
                    GetLoginCodePicture(number);
                    textCheckCode.Text = "";
                    break;
            }
        }


        private void GetLoginCodePicture(string loginQQ)
        {
            if (!Tool.CheckQQNumber(loginQQ)) return;
            var tast = new Task<Image>(() =>
            {
                return _WebQQ.GetLoginVCImage(loginQQ);
            });
            tast.Start();
            tast.ContinueWith((item) =>
            {
                if (item.IsCompleted)
                {
                    pictureCheckCode.Image = tast.Result;
                    if (IsNeedCode)
                        ShowCodePicture(true);
                    else
                    {
                        ShowCodePicture(false);
                    }

                }
            }
            );

        }

        /// <summary>
        /// 刷新验证码图片.
        /// </summary>
        /// <param name="loginQQ"></param>
        /// <param name="isRefresh"></param>
        private void UpdateCode(string loginQQ, bool isRefresh = false)
        {
            if (!Tool.CheckQQNumber(loginQQ)) return;
            //强制刷新验证码图片.
            if (isRefresh)
            {
                pictureCheckCode.Image = _WebQQ.GetLoginVCImage(loginQQ);
                ShowCodePicture(true);
                return;
            }
            string temp;
            _WebQQ.GetLoginVC(loginQQ, out temp);
            if (textCheckCode.Text.Length != 4)
            {
                IsNeedCode = true;
                pictureCheckCode.Image = _WebQQ.GetLoginVCImage(loginQQ);
                ShowCodePicture(true);
            }
            else
            {
                IsNeedCode = false;
                textCheckCode.Text = temp;
                ShowCodePicture(false);
            }



        }


        /// <summary>
        /// 是否显示验证码图片.
        /// </summary>
        /// <param name="IsShow"></param>
        private void ShowCodePicture(bool IsShow)
        {

            if (pictureCheckCode.InvokeRequired)
            {
                Action a = new Action(() => ShowCodePicture(IsShow));
                pictureCheckCode.Invoke(a);
            }
            else
            {

                if (IsShow)
                {
                    pictureCheckCode.Visible = true;
                    linkLabelChange.Visible = true;
                }
                else
                {
                    pictureCheckCode.Visible = false;
                    linkLabelChange.Visible = false;

                }
            }
        }



        #endregion

        #region 界面事件
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        /// <summary>
        /// 窗体加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLogon_Load(object sender, EventArgs e)
        {

            comState.SelectedIndex = 0;
        }
       

        private void linkLabelChange_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //强制获取 验证码.
            //UpdateCode(textNumber.Text, true);
            GetLoginCodePicture(textNumber.Text);

        }

        private void buttonLogon_Click(object sender, EventArgs e)
        {
            Logon();
        }

        private void textNumber_Leave(object sender, EventArgs e)
        {
            //输入QQ号码失去焦点时,获取验证码.
            GetLoginCodePicture(textNumber.Text);
            if (IsNeedCode)
            {

            }
            else
            {
                IsNeedCode = true;
            }
        }


        #endregion

        #region 属性

        /// <summary>
        /// 获取WebQQ代理类.
        /// </summary>
        public WebQQ WebQQ
        {
            get
            {
                return _WebQQ;
            }
        }

        /// <summary>
        /// 登录的QQ号码.
        /// </summary>
        public string QQNumber
        {
            get
            {
                return textNumber.Text.Trim();
            }
        }

        /// <summary>
        /// 用户信息.
        /// </summary>
        public Person QQPerson
        {
            get
            {
                return _Person;
            }
        }

        #endregion

    }
}
