/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/7 8:18:42
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace QQMessage
{

    /// <summary>
    /// 使用的用户.
    /// </summary>
    [DataContract]
    [Serializable]
    public class User
    {
        /// <summary>
        /// 构造一个用户
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        public User(string name, string password, UserType type)
        {
            _Name = name;
            //_Password = MD5Encode(password);
            _Password = password;
            Debug.WriteLine("User g 1");
            Type = type;
        }

        public User(string name, string password)
        {
            _Name = name;
            _Password = password;
            Debug.WriteLine("User g 2");
            Type = UserType.User;
        }

      
        private string _Name;
        /// <summary>
        /// 用户的名称.
        /// </summary>
        [DataMember]
        public string Name
        {
          

            get
            {
                return _Name;
            }
            private set
            {
                _Name = value;
            }
        }

       
        private string _Password;
        /// <summary>
        /// 用户的密码.
        /// </summary>
        [DataMember]
        public string Password
        {
            private get
            {
                return _Password;
            }
            set
            {
               _Password = value;
            }
        }

        /// <summary>
        /// 用户的类型.
        /// </summary>
        [DataMember]
        public UserType Type { get; set; }

        /// <summary>
        /// 检查密码是否正确.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CheckPassword(string text)
        {
            string pass = MD5Encode(text);
            if (pass == _Password)
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
                if (u.Name == Name && u.Password == _Password)
                {
                    return true;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() + _Password.GetHashCode();
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

        /// <summary>
        /// MD5 加密.
        /// </summary>
        /// <param name="text">要加密的字符串.</param>
        /// <param name="len">指定长度.</param>
        /// <returns>返回的加密字符串.</returns>
        static string MD5Encode(string text, int len = 32)
        {
            MD5 md = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            bytes = md.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (var item in bytes)
            {
                sb.Append(item.ToString("X2"));
            }

            if (len == 32) return sb.ToString().ToUpper();
            else
                return sb.ToString().ToUpper().Substring(8, len);

        }
    }


    public enum LoginResult
    {
        /// <summary>
        /// 登录成功.
        /// </summary>
        Succeed,
        /// <summary>
        /// 帐号错误.
        /// </summary>
        NumberError,
        /// <summary>
        /// 
        /// </summary>
        PassWordError,

    }

}
