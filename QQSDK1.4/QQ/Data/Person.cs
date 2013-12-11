using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWebQQ.Data
{
    /// <summary>
    /// 一个QQ帐号的信息.
    /// </summary>
    [Serializable]
    public class Person
    {
        private string _QQ;


        /// <summary>
        /// QQ号码.
        /// </summary>
        public string QQ
        {
            get { return _QQ; }
            set { _QQ = value; }
        }

        private string _NikeName;

        /// <summary>
        /// QQ昵称
        /// </summary>
        public string NikeName
        {
            get { return _NikeName; }
            set { _NikeName = value; }
        }



        private string _Password;

        /// <summary>
        /// 加密过的QQ密码.
        /// </summary>
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }


        private string _State;
        /// <summary>
        /// QQ 登录的状态.
        /// </summary>
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }

        private int _Picture = -1;

        /// <summary>
        /// 图片的索引.
        /// </summary>
        public int PictureIndex
        {
            get { return _Picture; }
            set { _Picture = value; }
        }

        private bool _IsRecord=false;
        /// <summary>
        /// 是否已经记录.
        /// </summary>
        public bool IsRecord
        {
            get { return _IsRecord; }
            set { _IsRecord = value; }
        }

        private bool _IsLogin=false;

        /// <summary>
        /// 是否已经登录.
        /// </summary>	
        public bool IsLogin
        {
            get { return _IsLogin; }
            set { _IsLogin = value; }
        }
        
    }

    
    /// <summary>
	/// QQ用户列表
	/// </summary>
    [Serializable]
    public class PersonCollection:List<Person>
	{
        public Person Find(string qq)
        {
            return this.Find((item) =>
            {
                return item.QQ == qq;
            }
                );
        }

        /// <summary>
        /// 自动添加或更新.
        /// </summary>
        /// <param name="item"></param>
        public void AddOrUpdate(Person item)
        {
            if (item == null) return;
            Person person = Find(item.QQ);
            item.IsLogin = true;
            if (person != null)
            {
               
                person = item;
            }
            else
            {
                base.Add(item);
            }
        }

        /// <summary>
        /// 将指定的QQ设置为未登录的状态.
        /// </summary>
        /// <param name="qqNumber"></param>
        public void SetNotLogin(string qqNumber)
        {
            Person person = Find(qqNumber);
            if (person != null)
            {
                person.IsLogin = false;
            }

        }

        /// <summary>
        /// 将所有的QQ设置为未登录的状态.
        /// </summary>
        public void SetNotLogin()
        {
            foreach (var item in this )
            {
                item.IsLogin = false;
            }
        }
	}

}
