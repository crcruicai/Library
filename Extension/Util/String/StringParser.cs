/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 10:55:14
 * 描述说明：字符串解析器.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace CRC.Util
{
    /// <summary>
    /// 字符串解析器.(主要使用Html手动解析)
    /// </summary>
    public class StringParser
    {
        #region 字段

        /// <summary>
        /// 当前所处的位置.
        /// </summary>
        private int m_nIndex;
        /// <summary>
        /// 解析的内容.
        /// </summary>
        private string m_strContent;
        /// <summary>
        /// 解析内容的小写形式.
        /// </summary>
        private string m_strContentLC;

        #endregion

        #region  构造器
        /// <summary>
        /// 字符串解析器
        /// </summary>
        public StringParser()
        {
            this.m_strContent = "";
            this.m_strContentLC = "";
            this.m_nIndex = 0;
        }

        /// <summary>
        /// 字符串解析器.
        /// </summary>
        /// <param name="strContent"></param>
        public StringParser(string strContent)
        {
            this.m_strContent = "";
            this.m_strContentLC = "";
            this.m_nIndex = 0;
            this.Content = strContent;
        }
        #endregion

        #region 公共函数
        /// <summary>
        /// 检查指定的关键字是否在当前的位置.
        /// </summary>
        /// <param name="strString">关键字.</param>
        /// <returns></returns>
        public bool At(string strString)
        {
            return (this.m_strContent.IndexOf(strString, this.Position) == this.Position);
        }
        /// <summary>
        /// 检查指定的关键字是否在当前的位置.(忽略大小写)
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public bool AtNoCase(string strString)
        {
            strString = strString.ToLower();
            return (this.m_strContentLC.IndexOf(strString, this.Position) == this.Position);
        }
        /// <summary>
        /// 提取从当前位置到关键字位置之间的内容.(属性Position受影响)
        /// </summary>
        /// <param name="strString">关键字</param>
        /// <param name="strExtract">返回的内容.</param>
        /// <returns></returns>
        public bool ExtractTo(string strString, ref string strExtract)
        {
            int index = this.m_strContent.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this.m_strContent.Substring(this.m_nIndex, index - this.m_nIndex);
                this.m_nIndex = index + strString.Length;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 提取到结束位置的内容.
        /// </summary>
        /// <param name="strExtract"></param>
        public void ExtractToEnd(ref string strExtract)
        {
            strExtract = "";
            if (this.Position < this.m_strContent.Length)
            {
                int length = this.m_strContent.Length - this.Position;
                strExtract = this.m_strContent.Substring(this.Position, length);
            }
        }
        /// <summary>
        /// 从当前位置提取包含特定字符串的内容.
        /// </summary>
        /// <param name="strString">要查找的字符串.</param>
        /// <param name="strExtract">返回提取的内容.</param>
        /// <returns></returns>
        public bool ExtractToNoCase(string strString, ref string strExtract)
        {
            strString = strString.ToLower();
            int index = this.m_strContentLC.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this.m_strContent.Substring(this.m_nIndex, index - this.m_nIndex);
                this.m_nIndex = index + strString.Length;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 提取指定的内容,并提升字符串的位置.
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="strExtract"></param>
        /// <returns></returns>
        public bool ExtractUntil(string strString, ref string strExtract)
        {
            int index = this.m_strContent.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this.m_strContent.Substring(this.m_nIndex, index - this.m_nIndex);
                this.m_nIndex = index;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 提取指定的内容,并提升字符串的位置.(忽略大小写)
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="strExtract"></param>
        /// <returns></returns>
        public bool ExtractUntilNoCase(string strString, ref string strExtract)
        {
            strString = strString.ToLower();
            int index = this.m_strContentLC.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this.m_strContent.Substring(this.m_nIndex, index - this.m_nIndex);
                this.m_nIndex = index;
                return true;
            }
            return false;
        }
     
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strOccurrence"></param>
        /// <param name="strReplacement"></param>
        /// <returns></returns>
        public int ReplaceEvery(string strOccurrence, string strReplacement)
        {
            int num = 0;
            strOccurrence = strOccurrence.ToLower();
            for (int i = this.m_strContentLC.IndexOf(strOccurrence); i != -1; i = this.m_strContentLC.IndexOf(strOccurrence))
            {
                string str = this.m_strContent.Substring(0, i) + strReplacement;
                int startIndex = i + strOccurrence.Length;
                if (startIndex < this.m_strContent.Length)
                {
                    string str2 = this.m_strContent.Substring(startIndex, this.m_strContent.Length - startIndex);
                    str = str + str2;
                }
                this.m_strContent = str;
                this.m_strContentLC = this.m_strContent.ToLower();
                num++;
            }
            return num;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strOccurrence"></param>
        /// <param name="strReplacement"></param>
        /// <returns></returns>
        public int ReplaceEveryExact(string strOccurrence, string strReplacement)
        {
            int num = 0;
            while (this.m_strContent.IndexOf(strOccurrence) != -1)
            {
                this.m_strContent = this.m_strContent.Replace(strOccurrence, strReplacement);
                num++;
            }
            this.m_strContentLC = this.m_strContent.ToLower();
            return num;
        }
        /// <summary>
        /// 
        /// </summary>
        public void ResetPosition()
        {
            this.m_nIndex = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strString">查找的字符串.</param>
        /// <param name="bNoCase">是否忽略大小写.</param>
        /// <param name="bPositionAfter">是否在查找过后移动位置.</param>
        /// <returns></returns>
        private bool SeekTo(string strString, bool bNoCase, bool bPositionAfter)
        {
            if (this.Position >= this.m_strContent.Length)
            {
                return false;
            }
            int index = 0;
            if (bNoCase)
            {
                strString = strString.ToLower();
                index = this.m_strContentLC.IndexOf(strString, this.Position);
            }
            else
            {
                index = this.m_strContent.IndexOf(strString, this.Position);
            }
            if (index == -1)
            {
                return false;
            }
            this.m_nIndex = index;
            if (bPositionAfter)
            {
                this.m_nIndex += strString.Length;
            }
            return true;
        }

        /// <summary>
        /// 向后查找字符,如果找到返回true.
        /// </summary>
        /// <param name="strString">要查找的字符</param>
        /// <returns></returns>
        public bool SkipToEndOf(string strString)
        {
            return this.SeekTo(strString, false, true);
        }
        /// <summary>
        /// 向后查找字符,忽略大小写.如果找到返回true.
        /// </summary>
        /// <param name="strText">要查找的字符</param>
        /// <returns></returns>
        public bool SkipToEndOfNoCase(string strText)
        {
            return this.SeekTo(strText, true, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public bool SkipToStartOf(string strString)
        {
            return this.SeekTo(strString, false, false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public bool SkipToStartOfNoCase(string strText)
        {
            return this.SeekTo(strText, true, false);
        }

        #endregion

        #region 静态函数
        /// <summary>
        /// 获取Link.
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="strRootUrl"></param>
        /// <param name="documents"></param>
        /// <param name="images"></param>
        public static void GetLinks(string strString, string strRootUrl, ref ArrayList documents, ref ArrayList images)
        {
            strString = RemoveComments(strString);
            strString = RemoveScripts(strString);
            StringParser parser = new StringParser(strString);
            parser.ReplaceEvery("'", "\"");
            string uri = "";
            if (strRootUrl != null)
            {
                uri = strRootUrl.Trim();
            }
            if ((uri.Length > 0) && !uri.EndsWith("/"))
            {
                uri = uri + "/";
            }
            string strExtract = "";
            parser.ResetPosition();
            while (parser.SkipToEndOfNoCase("href=\""))
            {
                if (parser.ExtractTo("\"", ref strExtract))
                {
                    strExtract = strExtract.Trim();
                    if ((strExtract.Length > 0) && (strExtract.IndexOf("mailto:") == -1))
                    {
                        if (!strExtract.StartsWith("http://") && !strExtract.StartsWith("ftp://"))
                        {
                            try
                            {
                                strExtract = new UriBuilder(uri) { Path = strExtract }.Uri.ToString();
                            }
                            catch (Exception)
                            {
                                strExtract = "http://" + strExtract;
                            }
                        }
                        if (!documents.Contains(strExtract))
                        {
                            documents.Add(strExtract);
                        }
                    }
                }
            }
            parser.ResetPosition();
            while (parser.SkipToEndOfNoCase("src=\""))
            {
                if (parser.ExtractTo("\"", ref strExtract))
                {
                    strExtract = strExtract.Trim();
                    if (strExtract.Length > 0)
                    {
                        if (!strExtract.StartsWith("http://") && !strExtract.StartsWith("ftp://"))
                        {
                            try
                            {
                                strExtract = new UriBuilder(uri) { Path = strExtract }.Uri.ToString();
                            }
                            catch (Exception)
                            {
                                strExtract = "http://" + strExtract;
                            }
                        }
                        if (!images.Contains(strExtract))
                        {
                            images.Add(strExtract);
                        }
                    }
                }
            }
        }

        public static string RemoveComments(string strString)
        {
            string str = "";
            string strExtract = "";
            StringParser parser = new StringParser(strString);
            while (parser.ExtractTo("<!--", ref strExtract))
            {
                str = str + strExtract;
                if (!parser.SkipToEndOf("-->"))
                {
                    return strString;
                }
            }
            parser.ExtractToEnd(ref strExtract);
            return (str + strExtract);
        }

        public static string RemoveEnclosingAnchorTag(string strString)
        {
            string str = strString.ToLower();
            int index = str.IndexOf("<a");
            if (index != -1)
            {
                index++;
                index = str.IndexOf(">", index);
                if (index != -1)
                {
                    index++;
                    int num2 = str.LastIndexOf("</a>");
                    if (num2 != -1)
                    {
                        return strString.Substring(index, num2 - index);
                    }
                }
            }
            return strString;
        }

        public static string RemoveEnclosingQuotes(string strString)
        {
            int index = strString.IndexOf("\"");
            if (index != -1)
            {
                int num2 = strString.LastIndexOf("\"");
                if (num2 > index)
                {
                    return strString.Substring(index, (num2 - index) - 1);
                }
            }
            return strString;
        }
        /// <summary>
        /// 转换HTML特殊定义的字符串.
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public static string RemoveHtml(string strString)
        {
            Hashtable hashtable = new Hashtable();
            hashtable.Add("&nbsp;", " ");
            hashtable.Add("&amp;", "&");
            hashtable.Add("&aring;", "");
            hashtable.Add("&auml;", "");
            hashtable.Add("&eacute;", "");
            hashtable.Add("&iacute;", "");
            hashtable.Add("&igrave;", "");
            hashtable.Add("&ograve;", "");
            hashtable.Add("&ouml;", "");
            hashtable.Add("&quot;", "\"");
            hashtable.Add("&szlig;", "");
            hashtable.Add("&#8221;", "\"");
            hashtable.Add("&gt;", ">");
            hashtable.Add("&laquo;", "∝");
            hashtable.Add("&#8212;", "—");
            hashtable.Add("&#8211;", "–");

            StringParser parser = new StringParser(strString);
            foreach (string str in hashtable.Keys)
            {
                string strReplacement = hashtable[str] as string;
                if (strString.IndexOf(str) != -1)
                {
                    parser.ReplaceEveryExact(str, strReplacement);
                }
            }
            parser.ReplaceEveryExact("&#0", "&#");
            parser.ReplaceEveryExact("&#39;", "'");
            parser.ReplaceEveryExact("</", " <~/");
            parser.ReplaceEveryExact("<~/", "</");
            hashtable.Clear();
            hashtable.Add("<br>", " ");
            hashtable.Add("<p>", " ");
            foreach (string str3 in hashtable.Keys)
            {
                string str4 = hashtable[str3] as string;
                if (strString.IndexOf(str3) != -1)
                {
                    parser.ReplaceEvery(str3, str4);
                }
            }
            strString = parser.Content;
            string str5 = "";
            int startIndex = 0;
            int num2 = 0;
            while ((num2 = strString.IndexOf("<", startIndex)) != -1)
            {
                string str6 = strString.Substring(startIndex, num2 - startIndex);
                str5 = str5 + str6;
                startIndex = num2 + 1;
                int index = strString.IndexOf(">", startIndex);
                if (index == -1)
                {
                    break;
                }
                startIndex = index + 1;
            }
            if (startIndex < strString.Length)
            {
                str5 = str5 + strString.Substring(startIndex, strString.Length - startIndex);
            }
            strString = str5;
            str5 = "";
            parser.Content = strString;
            parser.ReplaceEveryExact("  ", " ");
            strString = parser.Content.Trim();
            return strString;
        }
        /// <summary>
        /// 去除脚本.
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public static string RemoveScripts(string strString)
        {
            string str = "";
            string strExtract = "";
            StringParser parser = new StringParser(strString);
            while (parser.ExtractToNoCase("<script", ref strExtract))
            {
                str = str + strExtract;
                if (!parser.SkipToEndOfNoCase("</script>"))
                {
                    parser.Content = str;
                    return strString;
                }
            }
            parser.ExtractToEnd(ref strExtract);
            return (str + strExtract);
        }
        #endregion

        #region 属性

       
        /// <summary>
        /// 获取内容.
        /// </summary>
        public string Content
        {
            get
            {
                return this.m_strContent;
            }
            set
            {
                this.m_strContent = value;
                this.m_strContentLC = this.m_strContent.ToLower();
                this.ResetPosition();
            }
        }
        /// <summary>
        /// 当前的位置.
        /// </summary>
        public int Position
        {
            get
            {
                return this.m_nIndex;
            }
        }

        #endregion
    }
}
