/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/30 16:53:19
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWebQQ.Data
{
    [Serializable ]
    public class Settings
    {
        public Settings()
        {
            _MajorFriendMap = new Dictionary<string, FriendCollection>();
            _Message = new MessageRecord();
            _PersonList = new PersonCollection();
        }

        private Dictionary <string,FriendCollection > _MajorFriendMap;
        /// <summary>
        /// 重点对象的qq列表
        /// </summary>
        public Dictionary <string,FriendCollection > MajorFriendMap
        {
            get { return _MajorFriendMap; }
            set { _MajorFriendMap = value; }
        }

        private MessageRecord _Message;
        /// <summary>
        /// 消息记录器
        /// </summary>
        public MessageRecord Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        private PersonCollection  _PersonList;
        /// <summary>
        /// 登录QQ记录列表.
        /// </summary>
        public PersonCollection  PersonList
        {
            get { return _PersonList; }
            set { _PersonList = value; }
        }




    }

    /// <summary>
    /// 使用的用户.
    /// </summary>
    [Serializable]
    public class User
    {
        /// <summary>
        /// 构造一个用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        public User(string name ,string password,UserType type)
        {
            _Name = name;
            _Password = QQSDK.Net.Encode.MD5Encode(password);
            Type = type;
        }

        public User(string name, string password)
        {
            _Name = name;
            _Password = QQSDK.Net.Encode.MD5Encode(password);
            Type = UserType.User;
        }


        private string _Name;
        /// <summary>
        /// 用户的名称.
        /// </summary>
        public string  Name 
        {
            get
            {
                return _Name;
            }
        }


        private string _Password;
        /// <summary>
        /// 用户的密码.
        /// </summary>
        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = QQSDK.Net.Encode.MD5Encode(value);
            }
        }

        /// <summary>
        /// 用户的类型.
        /// </summary>
        public UserType  Type { get; set; }

        /// <summary>
        /// 检查密码是否正确.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CheckPassword(string text)
        {
            string pass = QQSDK.Net.Encode.MD5Encode(text);
            if (pass == Password)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public override bool Equals(object obj)
        {
            User u = obj as User;
            if (u != null)
            {
                if (u.Name == Name && u.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + Password.GetHashCode();
        }

        /// <summary>
        /// 用户的类型.
        /// </summary>
        public enum UserType
        {
            /// <summary>
            /// 管理员
            /// </summary>
            Admin,
            /// <summary>
            /// 普通的用户.
            /// </summary>
            User
        }


    }

    
}
