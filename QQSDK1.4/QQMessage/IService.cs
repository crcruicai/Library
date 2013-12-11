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
using System.ServiceModel;

namespace QQMessage
{
    /// <summary>
    /// QQ 帐号管理服务.
    /// </summary>
    [ServiceContract(Name = "QQService")]
    public interface IService
    {

        /// <summary>
        /// 用户登录.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract(Name = "Login")]
        LoginResult  Login(User user);

        /// <summary>
        /// 登录后可以获取本帐号的信息.
        /// </summary>
        /// <param name="usrName"></param>
        /// <returns></returns>
        [OperationContract(Name ="GetUser")]
        User GetUser(string usrName);

        /// <summary>
        /// 创建一个新的用户.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract(Name = "CreateUser")]
        bool CreateUser(string usrName,User user);

        /// <summary>
        /// 删除一个帐号.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract(Name = "DeleteUser")]
        bool DeleteUser(string usrName, User user);

        /// <summary>
        /// 更新一个帐号.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract(Name = "UpdateUser")]
        bool UpdateUser(string usrName, User user);

        /// <summary>
        /// 获取所有用户列表.
        /// </summary>
        /// <returns></returns>
        [OperationContract(Name = "GetUserList")]
        List<User> GetUserList(string usrName);

        /// <summary>
        /// 关闭与服务器之间的通信.
        /// </summary>
        /// <param name="usrName"></param>
        /// <returns></returns>
        [OperationContract(Name = "CloseUser")]
        bool CloseUser(string usrName);

        /// <summary>
        /// 保持在线状态.
        /// </summary>
        /// <param name="usrName"></param>
        [OperationContract(Name = "KeepOnline")]
        void KeepOnline(string usrName);
    }
}
