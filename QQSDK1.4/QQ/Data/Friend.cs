using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CWebQQ.Data
{

    /// <summary>
    /// 代表一个好友的QQ.
    /// </summary>
    [Serializable]
    public class Friend
    {
        #region 构造函数
        public Friend(string fathernumber, string qqnumber)
            : this(fathernumber, qqnumber, "", "")
        {

        }

        public Friend(string fathernumber, string qqnumber, string uin, string nikename)
        {
            _FatherNumber = fathernumber;
            _QQNumber = qqnumber;
            _Uin = uin;
            _NikeName = nikename;
        }
        #endregion

        #region 属性
        
     

        private string _FatherNumber;
        /// <summary>
        /// 隶属的QQ
        /// </summary>
        public string FatherNumber
        {
            get
            {
                return _FatherNumber;
            }
            set
            {
                _FatherNumber = value;
            }
        }


        private string _QQNumber;
        /// <summary>
        /// QQ号码.
        /// </summary>
        public string QQNumber
        {
            get
            {
                return _QQNumber;
            }
            set
            {
                _QQNumber = value;
            }
        }


        private string _Uin;
        /// <summary>
        /// QQ的Uin号码
        /// </summary>
        public string Uin
        {
            get
            {
                return _Uin;
            }
            set
            {
                _Uin = value;
            }
        }

        private string _NikeName;
        /// <summary>
        /// QQ好友昵称.
        /// </summary>
        public string NikeName
        {
            get
            {
                return _NikeName;
            }
            set
            {
                _NikeName = value;
            }
        }

        #endregion

    }

    /// <summary>
    /// 朋友的集合.
    /// </summary>
    [Serializable]
    public class FriendCollection:List<Friend>
    {
        #region 属性

        private string _FatherQQ;
        /// <summary>
        /// 父QQ.
        /// </summary>
        public string FatherQQ
        {
            get { return _FatherQQ; }
            set { _FatherQQ = value; }
        }

        

        #endregion

        #region 公共函数.

        /// <summary>
        /// 根据Uin查找好友信息.
        /// </summary>
        /// <param name="uin"></param>
        /// <returns></returns>
        public Friend FindByUin(string uin)
        {
            return Find((item) =>item.Uin == uin);
               
        }

        /// <summary>
        /// 根据QQ号查找好友信息.
        /// </summary>
        /// <param name="qqnumber"></param>
        /// <returns></returns>
        public Friend FindByNumber(string qqnumber)
        {
            return Find((item) => item.QQNumber == qqnumber);
        }

        /// <summary>
        /// 根据Uin尝试查找QQ号码
        /// </summary>
        /// <param name="uin"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public bool TryFindNumber(string uin,out string number)
        {
            Friend f = Find((item) => item.Uin == uin);
            if (f != null)
            {
                number = f.QQNumber;
                return true;
            }
            else
            {
                number = string.Empty;
                return false;
            }
        }
        /// <summary>
        /// 根据QQ号码尝试查找Uin
        /// </summary>
        /// <param name="number"></param>
        /// <param name="uin"></param>
        /// <returns></returns>
        public bool TryFindUin(string number, out string uin)
        {
            Friend f = Find((item) => item.Uin == number);
            if (f != null)
            {
                uin = f.Uin ;
                return true;
            }
            else
            {
                uin = string.Empty;
                return false;
            }
        }

        /// <summary>
        /// 如果对象已经存在,更新对象,不存在则添加到末尾处.
        /// </summary>
        /// <param name="item"></param>
        public new void Add(Friend item)
        {
            Friend f = FindByNumber(item.QQNumber);
            if (f != null)
            {
                f = item;
            }
            else
            {
                base.Add(item);
            }

        }


        #endregion

       
    }
    
}
