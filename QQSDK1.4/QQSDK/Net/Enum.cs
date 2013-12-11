using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Net
{
    /// <summary>
    /// QQ 登录的状态.
    /// </summary>
    public enum LoginStatus
    {

    }
    /// <summary>
    /// QQ 登录的结果.
    /// </summary>
    public enum LoginResult
    {

        /// <summary>
        /// QQ号码被锁定.
        /// </summary>
        QQNumberError,
        /// <summary>
        /// 密码错误
        /// </summary>
        PasswordError,
        /// <summary>
        /// 登录成功
        /// </summary>
        LoginSucceed,
        /// <summary>
        /// 登录失败.
        /// </summary>
        LoginFail,
        /// <summary>
        /// 验证码错误
        /// </summary>
        VerifyCodeError,
        /// <summary>
        /// 需要验证码
        /// </summary>
        NeedVerifyCode,
        /// <summary>
        /// 无需验证码.
        /// </summary>
        NotVerifyCode

    }


    /// <summary>
    /// QQ在线状态.
    /// </summary>
    public enum OnlineStatus
    {
        /// <summary>
        /// 在线
        /// </summary>
        OnLine,
        /// <summary>
        /// 隐身
        /// </summary>
        Hidden,
        /// <summary>
        /// 忙碌
        /// </summary>
        Busy,
        /// <summary>
        /// Q我吧
        /// </summary>
        CallMe,
        /// <summary>
        /// 请勿打扰
        /// </summary>
        Silent,

    }


    /// <summary>
    /// 消息类型.
    /// </summary>
    public enum RequestDataType
    {
        /// <summary>
        /// 文本.
        /// </summary>
        Text,
        /// <summary>
        /// 流.
        /// </summary>
        Stream
    }


}