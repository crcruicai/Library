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
        private int _CurrentIndex;
        /// <summary>
        /// 解析的内容.
        /// </summary>
        private string _Content;
        /// <summary>
        /// 解析内容的小写形式.
        /// </summary>
        private string _ContentLower;

        #endregion

        #region  构造器
        /// <summary>
        /// 字符串解析器
        /// </summary>
        public StringParser()
        {
            this._Content = "";
            this._ContentLower = "";
            this._CurrentIndex = 0;
        }

        /// <summary>
        /// 字符串解析器.
        /// </summary>
        /// <param name="strContent"></param>
        public StringParser(string strContent)
        {
            this._Content = "";
            this._ContentLower = "";
            this._CurrentIndex = 0;
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
            return (this._Content.IndexOf(strString, this.Position) == this.Position);
        }
        /// <summary>
        /// 检查指定的关键字是否在当前的位置.(忽略大小写)
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public bool AtNoCase(string strString)
        {
            strString = strString.ToLower();
            return (this._ContentLower.IndexOf(strString, this.Position) == this.Position);
        }
        /// <summary>
        /// 提取从当前位置到关键字位置之间的内容.(属性Position受影响)
        /// <para>例如:字符串 "Font is song ti", 位置为0,查找关键字song</para>
        /// 那么提取的内容为 "Font is ",此时位置自动提升为12,即Font is song的长度.
        /// </summary>
        /// <param name="strString">关键字</param>
        /// <param name="strExtract">返回的内容.</param>
        /// <returns></returns>
        public bool ExtractTo(string strString, ref string strExtract)
        {
            int index = this._Content.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this._Content.Substring(this._CurrentIndex, index - this._CurrentIndex);
                this._CurrentIndex = index + strString.Length;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 从当前位置提取到结束位置的内容.
        /// </summary>
        /// <param name="strExtract"></param>
        public void ExtractToEnd(ref string strExtract)
        {
            strExtract = "";
            if (this.Position < this._Content.Length)
            {
                int length = this._Content.Length - this.Position;
                strExtract = this._Content.Substring(this.Position, length);
            }
        }
        /// <summary>
        /// 从当前位置提取包含指定的字符串的内容,并自动提升位置.
        /// <para>例如:字符串 "Font is song ti", 位置为0,查找关键字song</para>
        /// 那么提取的内容为 "Font is ",此时位置自动提升为12,即Font is song的长度.
        /// </summary>
        /// <param name="strString">要查找的字符串.</param>
        /// <param name="strExtract">返回提取的内容.</param>
        /// <returns></returns>
        public bool ExtractToNoCase(string strString, ref string strExtract)
        {
            strString = strString.ToLower();
            int index = this._ContentLower.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this._Content.Substring(this._CurrentIndex, index - this._CurrentIndex);
                this._CurrentIndex = index + strString.Length;
                return true;
            }
            return false;
        }
        /// <summary>
        /// 提取指定的内容,并提升字符串的位置(设置为关键字的位置)
        /// <para>例如:字符串 "Font is song ti", 位置为0,查找关键字song</para>
        /// 那么提取的内容为 "Font is ",此时位置自动提升为8,即Font is 的长度.
        /// </summary>
        /// <param name="strString"></param>
        /// <param name="strExtract"></param>
        /// <returns></returns>
        public bool ExtractUntil(string strString, ref string strExtract)
        {
            int index = this._Content.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this._Content.Substring(this._CurrentIndex, index - this._CurrentIndex);
                this._CurrentIndex = index;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 提取指定的内容,并提升字符串的位置.(查找关键字忽略大小写)
        /// <para>例如:字符串 "Font is song ti", 位置为0,查找关键字song</para>
        /// 那么提取的内容为 "Font is ",此时位置自动提升为8,即Font is 的长度.
        /// </summary>
        /// <param name="strString">查找关键字(忽略大小写)</param>
        /// <param name="strExtract">提取的内容.</param>
        /// <returns></returns>
        public bool ExtractUntilNoCase(string strString, ref string strExtract)
        {
            strString = strString.ToLower();
            int index = this._ContentLower.IndexOf(strString, this.Position);
            if (index != -1)
            {
                strExtract = this._Content.Substring(this._CurrentIndex, index - this._CurrentIndex);
                this._CurrentIndex = index;
                return true;
            }
            return false;
        }
     
        /// <summary>
        /// 替换字符,并返回替换的次数.
        /// </summary>
        /// <param name="strOccurrence">替换的关键字</param>
        /// <param name="strReplacement">替换的内容.</param>
        /// <returns>替换的次数</returns>
        public int ReplaceEvery(string strOccurrence, string strReplacement)
        {
            int num = 0;
            strOccurrence = strOccurrence.ToLower();
            for (int i = this._ContentLower.IndexOf(strOccurrence); i != -1; i = this._ContentLower.IndexOf(strOccurrence))
            {
                string str = this._Content.Substring(0, i) + strReplacement;
                int startIndex = i + strOccurrence.Length;
                if (startIndex < this._Content.Length)
                {
                    string str2 = this._Content.Substring(startIndex, this._Content.Length - startIndex);
                    str = str + str2;
                }
                this._Content = str;
                this._ContentLower = this._Content.ToLower();
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
            while (this._Content.IndexOf(strOccurrence) != -1)
            {
                this._Content = this._Content.Replace(strOccurrence, strReplacement);
                num++;
            }
            this._ContentLower = this._Content.ToLower();
            return num;
        }
        /// <summary>
        /// 重置Position
        /// </summary>
        public void ResetPosition()
        {
            this._CurrentIndex = 0;
        }
        /// <summary>
        /// 查找字符串.并指示是否提升位置.
        /// </summary>
        /// <param name="strString">查找的字符串.</param>
        /// <param name="bNoCase">是否忽略大小写.</param>
        /// <param name="bPositionAfter">是否在查找过后移动位置.</param>
        /// <returns></returns>
        private bool SeekTo(string strString, bool bNoCase, bool bPositionAfter)
        {
            if (this.Position >= this._Content.Length)
            {
                return false;
            }
            int index = 0;
            if (bNoCase)
            {
                strString = strString.ToLower();
                index = this._ContentLower.IndexOf(strString, this.Position);
            }
            else
            {
                index = this._Content.IndexOf(strString, this.Position);
            }
            if (index == -1)
            {
                return false;
            }
            this._CurrentIndex = index;
            if (bPositionAfter)
            {
                this._CurrentIndex += strString.Length;
            }
            return true;
        }

        /// <summary>
        /// 查找字符,如果找到返回true. (找到后,提升位置)
        /// </summary>
        /// <param name="strString">要查找的字符</param>
        /// <returns></returns>
        public bool SkipToEndOf(string strString)
        {
            return this.SeekTo(strString, false, true);
        }
        /// <summary>
        /// 查找字符,忽略大小写.如果找到返回true.
        /// (找到后,提升位置)
        /// </summary>
        /// <param name="strText">要查找的字符</param>
        /// <returns></returns>
        public bool SkipToEndOfNoCase(string strText)
        {
            return this.SeekTo(strText, true, true);
        }
        /// <summary>
        /// 查找字符,如果找到返回true.(找到后,不提升位置)
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
        public bool SkipToStartOf(string strString)
        {
            return this.SeekTo(strString, false, false);
        }
        /// <summary>
        /// 查找字符,忽略大小写,如果找到返回true.(找到后,不提升位置)
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

        /// <summary>
        /// 移除Hmtl注释标记.
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 移除Html的 a标记.
        /// </summary>
        /// <param name="strString"></param>
        /// <returns></returns>
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
                return this._Content;
            }
            set
            {
                this._Content = value;
                this._ContentLower = this._Content.ToLower();
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
                return this._CurrentIndex;
            }
        }

        #endregion
    }


    /// <summary>
    /// Json 格式刷.
    /// </summary>
    public class JsonFormatBrush
    {
        StringParser _Parser;

        public JsonFormatBrush()
        {
            _Parser = new StringParser();
        }

        public string Done(string text)
        {
            _Parser.Content = text;



            return null;
        }

        private string Replace(string text,char[] array)
        {
            Stack<int> stack = new Stack<int>();

            StringBuilder sb = new StringBuilder(text);
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (array.Contains(c))
                {

                }
            }
            return sb.ToString();

        }
        




    }



}
