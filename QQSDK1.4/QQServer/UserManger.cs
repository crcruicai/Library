/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/7 8:49:29
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QQMessage;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace QQServer
{
    class UserManger : IService
    {
        #region 静态函数

        /// <summary>
        /// 从文件中 加载对象.
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path)) return default(T);
            try
            {
                //读取文件
                using (FileStream fs = File.OpenRead(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T temp = (T)bf.Deserialize(fs);//序列化
                    fs.Close();
                    return temp;
                }
            }
            catch (Exception e)
            {
                Loger.WriteLog(e);
                throw e;
            }
        }

        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="path">保存文件的路径</param>
        /// <param name="temp">要保存的对象</param>
        /// <returns></returns>
        public static bool Save<T>(string path, T temp)
        {

            try
            {
                using (FileStream fs = File.Create(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, temp);
                    fs.Close();
                    return true;
                }
            }
            catch (IOException io)
            {
                Console.WriteLine(io.Message);
                Loger.WriteLog(io);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Loger.WriteLog(e);
                return false;
            }

        }

        #endregion

        #region 静态 部分
        static object _LoginLock = new object();
        static object _UserListLock = new object();
        static Dictionary<string, LoginState> _LoginList = new Dictionary<string, LoginState>();

        static List<User> _UserList;

        static UserManger()
        {
             string path = Environment.CurrentDirectory + "\\User.Db";
             try
             {
                 _UserList = Load<List<User>>(path);
                 if (_UserList == null)
                 {
                     _UserList = new List<User>();
                     User user = new User("Administrator", "Administrator", User.UserType.Admin);
                     _UserList.Add(user);
                 }
             }
             catch
             {
                 _UserList = new List<User>();
                 User user = new User("Administrator", "Administrator", User.UserType.Admin);
                 _UserList.Add(user);
             }


        }

        public static void SaveSettings()
        {
            string path = Environment.CurrentDirectory + "\\User.Db";
            try
            {
                lock (_UserListLock )
                {
                    Save(path, _UserList);
                }
               
            }
            catch(Exception e)
            {
                Loger.WriteLog(e);
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        /// <summary>
        /// 用户管理类.
        /// </summary>
        public UserManger()
        {
            Thread task = new Thread(() =>
                {
                    CheckState();
                });
            task.IsBackground = true;
            task.Start();
        }

        /// <summary>
        /// 检查用户登录状态,如果登录超时,将移除账户.
        /// </summary>
        private void CheckState()
        {
            List<LoginState> list = new List<LoginState>();
            while (true)
            {
                try
                {
                    lock (_LoginLock)
                    {
                        foreach (var item in _LoginList)
                        {
                            if (item.Value.IsTimeOut())
                            {
                                list.Add(item.Value);
                            }
                        }

                        foreach (var item in list)
                        {
                            _LoginList.Remove(item.Name);
                        }
                    }
                    //等待一分钟.
                    for (int i = 0; i < 60; i++)
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    Loger.WriteLog(ex);
                    Console.WriteLine(ex.Message);
                    //throw;
                }
               
            }
        }

        /// <summary>
        /// 添加一个登录账户.让他保持登录状态.
        /// </summary>
        /// <param name="ls"></param>
        private void AddLoginState(LoginState ls)
        {
            lock (_LoginLock)
            {
                if (_LoginList.ContainsKey(ls.Name))
                {
                    _LoginList[ls.Name].UpdateTime();
                }
                else
                {
                    _LoginList.Add(ls.Name, ls);
                }
            }
        }

        /// <summary>
        /// 移除一个登录状态.
        /// </summary>
        /// <param name="name"></param>
        private void RemoveLoginState(string name)
        {
            lock (_LoginLock )
            {
                if (_LoginList.ContainsKey(name))
                {
                    _LoginList.Remove(name);
                }
            }
        }

        /// <summary>
        /// 检查用户是否处于登录状态.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckUserExite(string name)
        {
            lock (_LoginLock )
            {
                if (_LoginList.ContainsKey(name))
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// 检查用户是否存在用户列表
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsUserExist(string name)
        {
            lock (_UserListLock)
            {
                if (_UserList.Find((item) => item.Name == name) != null)
                {
                    return true;
                }
                return false;
            }
        }

        #region IService 成员

        /// <summary>
        /// 用户登录.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public LoginResult Login(User user)
        {
            lock (_UserListLock)
            {
                int index = _UserList.IndexOf(user);
                if (index > -1)
                {
                    user = _UserList[index];
                    LoginState ls=new LoginState (user.Name );
                    if (user.Type == User.UserType.Admin)
                    {
                        ls.IsAdmin = true;
                    }
                    AddLoginState(ls);
                    return LoginResult.Succeed;
                }
            }

            return LoginResult.PassWordError;

        }

        /// <summary>
        /// 创建用户.
        /// </summary>
        /// <param name="usrName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CreateUser(string usrName, User user)
        {
            if (CheckUserExite(usrName) && !IsUserExist (user.Name))
            {
                lock (_UserListLock )
                {
                    _UserList.Add(user);
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除用户.
        /// </summary>
        /// <param name="usrName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(string usrName, User user)
        {
            if (CheckUserExite(usrName))
            {
                lock (_UserListLock)
                {
                    RemoveLoginState(user.Name);
                    return _UserList.Remove(user);
                }
            }
            return false;
        }

        /// <summary>
        /// 更新用户.
        /// </summary>
        /// <param name="usrName"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(string usrName, User user)
        {
            if (CheckUserExite(usrName))
            {
                lock (_UserListLock )
                {
                    User sitem= _UserList .Find ((item)=>item.Name ==user.Name );
                    if (sitem!=null)
                    {
                        sitem = user;
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 获取用户列表.
        /// </summary>
        /// <param name="usrName">管理员名称</param>
        /// <returns></returns>
        public List<User> GetUserList(string usrName)
        {

            if (CheckUserExite(usrName))
            {
                return _UserList;
            }
            return null;
        }

        /// <summary>
        /// 关闭账户退出.
        /// </summary>
        /// <param name="usrName">指定的账户</param>
        /// <returns></returns>
        public bool CloseUser(string usrName)
        {
            if (CheckUserExite(usrName))
            {
                RemoveLoginState(usrName);
                return true;
            }
            return false;
        }


        /// <summary>
        /// 指定账户保存在线.
        /// </summary>
        /// <param name="usrName"></param>
        public void KeepOnline(string usrName)
        {
            lock (_LoginLock)
            {
                if (_LoginList.ContainsKey(usrName))
                {
                    _LoginList[usrName].UpdateTime();
                }
            }
        }


        /// <summary>
        /// 获取登录用户的信息.
        /// </summary>
        /// <param name="usrName"></param>
        /// <returns></returns>
        public User GetUser(string usrName)
        {
            if (CheckUserExite(usrName))
            {
                lock (_UserListLock)
                {                 
                   return _UserList.Find((item) => item.Name == usrName);       
                }
            }
            return null;
        }

        #endregion
    }

    /// <summary>
    /// 
    /// </summary>
    class LoginState
    {
        public LoginState(string name):this(name,false)
        {
            
        }
        public LoginState(string name,bool isAdmin)
        {
            Name = name;
            IsAdmin = isAdmin;
            _LoginStart = DateTime.Now;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        private DateTime _LoginStart;
        /// <summary>
        /// 
        /// </summary>
        public DateTime  LoginStart
        {
            get
            {
                return _LoginStart;
            }
        }

        public bool IsAdmin { get; set; }

        public void UpdateTime()
        {
            _LoginStart = DateTime.Now;
        }

        public bool IsTimeOut()
        {
            TimeSpan e= new TimeSpan(DateTime.Now.Ticks);
            TimeSpan s = new TimeSpan(_LoginStart.Ticks);
            TimeSpan t = s-e;
            if(t.Minutes >10)return true;
            return false;
            
        }
    }

}
