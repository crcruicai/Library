using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace QQSDK.Json
{

    #region  获取朋友列表
    /// <summary>
    ///获取朋友列表.
    /// </summary>
    [DataContract]
    public class UserFriend
    {
        [DataMember(Name = "result")]
        public UserFriendResult  Result { get; set; }
        [DataMember(Name = "retcode")]
        public int RetCode { get; set; }
    }


    /// <summary>
    /// 朋友的
    /// </summary>
    [DataContract]
    public class UserFriendResult
    {
        [DataMember]
        public CategoriesItem[] Categories { get; set; }
        [DataMember(Name = "friends")]
        public FriendItem [] Friends { get; set; }
        [DataMember(Name = "info")]
        public InfoItem [] Info { get; set; }
        [DataMember(Name = "marknames")]
        public MarkNamesItem []  MarkNames { get; set; }
        [DataMember(Name = "vipinfo")]
        public VipInfoItem []  VIPInfo{ get; set; }

    }
    [DataContract]
    public class CategoriesItem
    {
        [DataMember(Name = "index")]
        public int Index { get; set; }
        [DataMember(Name = "name")]
        public string  Name { get; set; }
        [DataMember(Name = "sort")]
        public int Sort { get; set; }
    }
    [DataContract]
    public class FriendItem
    {
        [DataMember(Name = "categories")]
        public int Categories { get; set; }
        [DataMember(Name = "flag")]
        public int Flag { get; set; }
        [DataMember(Name = "uin")]
        public long Uin { get; set; }

    }
    [DataContract]
    public class InfoItem
    {
        [DataMember(Name = "face")]
        public int Face { get; set; }
        [DataMember(Name = "flag")]
        public int Flag { get; set; }
        [DataMember(Name = "nick")]
        public string  Nick { get; set; }
        [DataMember(Name = "uin")]
        public long Uin { get; set; }


    }
    [DataContract]
    public class MarkNamesItem
    {
        [DataMember(Name = "markname")]
        public string  MarkName { get; set; }
        [DataMember(Name = "type")]
        public int Type { get; set; }
        [DataMember(Name = "uin")]
        public long Uin { get; set; }

    }
    [DataContract]
    public class VipInfoItem
    {
        [DataMember(Name = "is_vip")]
        public int IsVIP { get; set; }
        [DataMember(Name = "u")]
        public long Uin { get; set; }

        [DataMember(Name = "vip_level")]
        public int VIPLevel { get; set; }
    }


    #endregion 

    #region 第二次登录的结果.

    [DataContract]
    public class PostLogin
    {
        [DataMember(Name="result")]
        public PostResult Result { get; set; }

        [DataMember(Name="retcode")]
        public int RetCode { get; set; }

    }


    [DataContract]
    public class PostResult
    {
        [DataMember(Name="cip")]
        public long Cip { get; set; }

        [DataMember(Name="f")]
        public int F { get; set; }

        [DataMember(Name="index")]
        public int Index { get; set; }

        [DataMember(Name="port")]
        public int Port { get; set; }

        [DataMember(Name="psessionid")]
        public string PsessionID { get; set; }

        [DataMember(Name="status")]
        public string  Status { get; set; }

        [DataMember(Name="uin")]
        public long Uin { get; set; }

        [DataMember(Name="user_state")]
        public int UserState { get; set; }

        [DataMember(Name="vfwebqq")]
        public string VFWebQQ { get; set; }

    }
    #endregion

}
