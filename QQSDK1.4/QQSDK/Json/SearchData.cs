/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/1 20:45:39
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
    /// 搜索条件.
    /// </summary>
    public class SearchData
    {
        /// <summary>
        /// 国家
        /// </summary>
        public int Contry { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public int Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public int City { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int  Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public SexCategory Sex { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public int Language { get; set; }
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnLine { get; set; }
        /// <summary>
        /// 翻页索引
        /// </summary>
        public int PageIndex { get; set; }


        public string GetQueryString(string vfWebQQ)
        {
            StringBuilder sb = new StringBuilder(100);
            sb.Append("/api/search_qq_by_term?");
            sb.AppendFormat("&country={0}", Contry);
            sb.AppendFormat("&province={0}", Province);
            sb.AppendFormat("&city={0}", City);
            sb.AppendFormat("&agerg={0}", Age);
            sb.AppendFormat("&sex={0}", Sex);
            sb.AppendFormat("&lang={0}", Language);
            sb.AppendFormat("&online={0}", IsOnLine ? 1:0);
            sb.AppendFormat("&vfwebqq={0}", vfWebQQ);
            sb.AppendFormat("&page={0}&t={1}", PageIndex, QQSDK.Net.Tool.GetRandomNumber(10));

            return sb.ToString();
        }

    }

    /// <summary>
    /// 性别类型.
    /// </summary>
    public enum SexCategory
    {
        /// <summary>
        /// 男
        /// </summary>
        Man,
        /// <summary>
        /// 女
        /// </summary>
        Woman,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow
    }
}
