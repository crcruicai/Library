/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/3 12:08:11
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Json
{
    /// <summary>
    /// QQ群 信息描述.
    /// </summary>
    public class QQGroupItem
    {
        /// <summary>
        /// 临时ID号(json 指定为code)
        /// </summary>
        public string Uin { get; set; }
        /// <summary>
        /// 标志.
        /// </summary>
        public string Flag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Gid { get; set; }
        /// <summary>
        /// QQ群名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// QQ群号码
        /// </summary>
        public string  Number { get; set; }

    }
}
