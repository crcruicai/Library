using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using CWebQQ.Data;
using QQMessage;
using QQServer;
namespace CWebQQ.Froms
{
    public partial class FrmManageUser : Form
    {
        private List<User> _UserList;
        private User _CurrentUser;
        IService _Service;
        string _Manger;
        public FrmManageUser(IService service,string manger)
        {
            if (service == null) throw new ArgumentNullException("service");
            InitializeComponent();
            _Manger = manger;
            _Service = service;
            _UserList =_Service.GetUserList (manger);
            if (_UserList == null)
            {
                MessageBox.Show("通信异常.");
            }
            
        }

        private void Init()
        {
            listBoxUser.DisplayMember = "Name";
            foreach (var item in _UserList )
            {
                listBoxUser.Items.Add(item);
            }
        }

        private void FrmManageUser_Load(object sender, EventArgs e)
        {
            Init();
            timer1.Interval = 60000;
            timer1.Start();
        }



        private void listBoxUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            User user = listBoxUser.SelectedItem as User;
            if (user != null)
            {
                _CurrentUser = user;
                textUserName.Text = user.Name;
                textPassWordOne.Text = "";
                if (user.Type == User.UserType.Admin)
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (textUserName.Text == "")
                {
                    MessageBox.Show("请输入用户名");
                    return;
                }
                if (textPassWordOne.Text.Length < 6)
                {
                    MessageBox.Show("密码长度必须大于6");
                    return;
                }
                User user = new User(textUserName.Text, textPassWordOne.Text);
                if (checkBox1.Checked)
                {
                    user.Type = User.UserType.Admin;
                }
                if (_Service.CreateUser(_Manger, user))
                {
                    listBoxUser.Items.Add(user);
                    _UserList.Add(user);
                }
                else
                {
                    ShowMessage(false, "添加用户失败");
                }
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
            

        }
        private void ShowMessage(bool result,string msg)
        {
            if (!result)
            {
                MessageBox.Show(msg);
            }
            else
            {

            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                User user = listBoxUser.SelectedItem as User;
                if (user != null)
                {
                    if (_Service.DeleteUser(_Manger, user))
                    {
                        _UserList.RemoveAt(listBoxUser.SelectedIndex);
                        listBoxUser.Items.RemoveAt(listBoxUser.SelectedIndex);
                    }
                    else
                    {
                        ShowMessage(false, "删除用户失败.");
                    }

                }
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
          
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (textPassWordOne.Text.Length < 6)
            {
                MessageBox.Show("密码长度必须大于6");
                return;
            }
            try
            {
                if (_CurrentUser != null)
                {

                    _CurrentUser.Password = textPassWordOne.Text;
                    _CurrentUser.Type = checkBox1.Checked ?
                        User.UserType.Admin : User.UserType.User;
                    if (_Service.UpdateUser(_Manger, _CurrentUser))
                    {
                        listBoxUser.Items[listBoxUser.SelectedIndex] = _CurrentUser;
                        _UserList[listBoxUser.SelectedIndex] = _CurrentUser;

                    }
                    else
                    {
                        ShowMessage(false, "更新用户失败.");
                    }
                }
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                
            }
         
        }

        private bool _Result = false;
        private void buttonOK_Click(object sender, EventArgs e)
        {

            _Result = true;
           
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void FrmManageUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(!_Result)
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                _Service.KeepOnline(_Manger);
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
            }
            
        }



    }
}
