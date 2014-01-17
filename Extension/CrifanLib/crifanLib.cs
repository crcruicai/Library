/*
 * [File]
 * crifanLib.cs
 * 
 * [Function]
 * This library file contains many common functions, implemented in C#, created by crifan.
 * 
 * [Note]
 * 1.copy out embed dll into exe related code into your project for use
 * 
 * [Version]
 * v8.5
 * 
 * [update]
 * 2013-09-17
 * 
 * [Author]
 * Crifan Li
 * 
 * [Contact]
 * http://www.crifan.com/contact_me/
 * http://www.crifan.com/crifan_released_all/crifanlib/
 * http://www.crifan.com/crifan_csharp_lib_crifanlib_cs/
 * 
 * [History]
 * [v8.5]
 * 1. check input setCookieStr for 
 * [v8.4]
 * 1. fix extractDomain for input fiverr.com, extracted should not .com, should fivver.com
 * 
 * [v8.3]
 * 1. change update cookie from use dotnet self parsed resp.Cookies, to self parsed cookies
 * 2. for parse cookie, support: when no expires or expires parse fail, use max datetime, let it not expired
 * 3. support year is dd for parseSetCookie
 * 
 * [v8.0]
 * 1. fix typo "accept-language" to "accept language"
 * 2. move some code location
 *
 * [v7.9]
 * 1. change some code location.
 * 
 * [v7.8]
 * 1. update _getUrlResponse support variation of http headers
 * 
 * [v7.7]
 * 1. update getDomainAlexaRank, getDomainPageRank
 * 2. add setProxy
 * 3. update _getUrlResponse to support deflate
 * 
 * [v7.5]
 * 1. update _getUrlResponse: ReadWriteTimeout, gUserAgent
 * 2. update getUrlRespHtml
 * 3. update getUrlRespHtml_multiTry
 * 
 * [v7.1]
 * 1. update getUrlRespHtml_multiTry
 * 2. update getUrlRespHtml
 * 
 * [v7.0]
 * 1. add findRootTreeNode
 * 
 * [v6.9]
 * 1. add openFileDirectly, htmlRemoveTag
 * 2. add formatString
 * 
 * [v6.6]
 * 1. change byte buffer alloc scenario for downloadFile
 * 2. add emptyStringArray
 * 
 * [v6.4]
 * 1. add jsonToDict
 * 
 * [v6.3]
 * 1. add extractSingleStr regex option support
 * 
 * [v6.2]
 * 1. add extractFilenameFromUrl
 * 2. add downloadFile
 * 
 * [v6.0]
 * 1. add getUrlRespHtml_multiTry
 * 2. fix bug of ReadToEnd hangs/dead for getUrlRespHtml
 * 
 * [v5.8]
 * 1. add removeSubHtmlNode
 * 
 * [v5.7]
 * 1. add ounceToKiloGram, kiloGramToOunce, kiloGramToPound, poundToKiloGram
 * 2. add inchToCm, cmToInch
 * 
 * [v5.1]
 * 1. make HtmlAgilityPack html tag option/form has its children
 * 
 * [v5.0]
 * 1. add content-type for getUrlResponse
 * 2. add definition for bw version getUrlResponse
 * 
 * [v4.8]
 * 1. update _quoteParas to support space to %20
 * 2. add getSaveFolder
 * 3. add new method for getDomainAlexaRank
 * 
 * [v4.5]
 * 1. add json related code
 * 2. add more check method for getDomainAlexaRank, getDomainPageRank
 * 3. update for dgvExportToExcel, dgvExportToCsv to support tag value
 * 
 * [v4.2]
 * 1. add htmlToXmlDoc, htmlToHtmlDoc, and related demo code
 * 2. add embeded dll into exe related code: init code and CurrentDomain_AssemblyResolve
 * 3. add dgvDrawHeaderNum, dgvClearContent, dgvExportToExcel, dgvExportToCsv
 * 4. add openFolderAndSelectFile
 * 
 * [v3.4]
 * 1. add getDomainAlexaRank, getDomainPageRank, getDomainUrl
 * 
 * [v3.1]
 * 1.convert getUrlResponse and getUrlRespStreamBytes into BackgroundWorker version
 *   to ehance UI response during these operation
 * 
 * [v2.9]
 * 1. add Application.DoEvents for getUrlRespStreamBytes for up layer UI update
 * 2. add functions: getCurDownloadPercent
 * 
 * [v2.8]
 * 1.add transZhcnToEn, translateString, getCurVerStr
 * 
 * [v2.5]
 * 1. add postDataStr for getUrlResponse
 * 2. add functions: removeInvChrInPath, getCurTaskbarSize, getCurTaskbarLocation, getCornerLocation
 * 
 * [v2.0]
 * 1. add functions: getUrlRespHtml, getUrlResponse, getUrlRespStreamBytes
 * 2. add saveBytesToFile
 * 3. remove the skydrive related functions, for it's not belong to common functions
 */

#region 宏定义
//comment out following macros if not use them
#define USE_GETURLRESPONSE_BW //for getUrlResponse use backgroundworker version
//#define USE_HTML_PARSER_SGML //need SgmlReaderDll.dll
//#define USE_HTML_PARSER_HTMLAGILITYPACK //need HtmlAgilityPack.dll
//#define USE_DATAGRIDVIEW //need add References: Microsoft.Office.Interop.Excel
//#define USE_JSON //need .NET 3.5+ and add References: (System.Web and) System.Web.Extensions
#endregion

#region  Using
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web; // for server
using System.Net; // for client
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;

#if USE_JSON
//!!! need add References: (System.Web and) System.Web.Extensions
using System.Web.Script.Serialization; // json lib, need: .NET 3.5+
#endif

#if USE_HTML_PARSER_SGML
using Sgml;
using System.Xml;
#endif

#if USE_HTML_PARSER_HTMLAGILITYPACK
using HtmlAgilityPack;
#endif

#if USE_DATAGRIDVIEW
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
#endif
#endregion 

/// <summary>
/// http://www.crifan.com/files/doc/docbook/crifanlib_csharp/release/html/crifanlib_csharp.html#calc_spend_time
/// </summary>
public partial  class CrifanLib
{
    #region 结构
    /// <summary>
    /// 
    /// </summary>
    public struct PairItem
    {
        public string Key;
        public string Value;
    };
    #endregion

    #region 字段与变量

    /// <summary>
    /// 
    /// </summary>
    private readonly Dictionary<string, DateTime> _CalcTimeList;

    /// <summary>
    /// 
    /// </summary>
    private const char ConstReplacedChar = '_';

    /// <summary>
    /// 
    /// </summary>
    private const string ConstStrExpires = "expires";

    /// <summary>
    /// 
    /// </summary>
    private readonly string[] _CookieFieldArr = {ConstStrExpires, "domain", "secure", "path", "httponly", "version"};

    #region Brower Agent

    //IE7
    private const string ConstUserAgentIe7X64 = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)";
    //IE8
    private const string ConstUserAgentIe8X64 = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; WOW64; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E";
    //IE9
    private const string ConstUserAgentIe9X64 = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"; // x64
    private const string ConstUserAgentIe9X86 = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)"; // x86
    //Chrome
    private const string ConstUserAgentChrome = "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US) AppleWebKit/533.4 (KHTML, like Gecko) Chrome/5.0.375.99 Safari/533.4";
    //Mozilla Firefox
    private const string ConstUserAgentFirefox = "Mozilla/5.0 (Windows; U; Windows NT 6.1; rv:1.9.2.6) Gecko/20100625 Firefox/3.6.6";

    #endregion

    /// <summary>
    /// 
    /// </summary>
    private readonly string _GUserAgent;

    /// <summary>
    /// 
    /// </summary>
    private WebProxy _GProxy = null;

    //detault values:
    //getUrlResponse
    /// <summary>
    /// 
    /// </summary>
    private const Dictionary<string, string> DefHeaderDict = null;

    /// <summary>
    /// 
    /// </summary>
    private const Dictionary<string, string> DefPostDict = null;

    /// <summary>
    /// 
    /// </summary>
    private const int DefTimeout = 30*1000;

    /// <summary>
    /// 
    /// </summary>
    private const string DefPostDataStr = null;

    /// <summary>
    /// 
    /// </summary>
    private const int DefReadWriteTimeout = 30*1000;

    /// <summary>
    /// getUrlRespHtml 
    /// </summary>
    private const string DefCharset = null;

    /// <summary>
    /// getUrlRespHtml_multiTry
    /// </summary>
    private const int DefMaxTryNum = 5;

    /// <summary>
    /// sleep time in ms when retry fail for getUrlRespHtml
    /// </summary>
    private const int DefRetryFailSleepTime = 100; //

    /// <summary>
    /// 
    /// </summary>
    private readonly List<string> _GCookieFieldList = new List<string>();

    /// <summary>
    /// 
    /// </summary>
    private CookieCollection _GCurCookies = null;

    //private long totalLength = 0;
    //private long currentLength = 0;
#if USE_GETURLRESPONSE_BW
    //
    /// <summary>
    /// indicate background worker complete or not
    /// </summary>
    private bool _BNotCompletedResp = true;

    //
    /// <summary>
    /// store response of http request
    /// </summary>
    private HttpWebResponse _GCurResp = null;
#endif

    /// <summary>
    /// 
    /// </summary>
    private BackgroundWorker _GBgwDownload;

    //indicate download complete or not
    /// <summary>
    /// 
    /// </summary>
    private bool _BNotCompletedDownload = true;

    //store current read out data len
    /// <summary>
    /// 
    /// </summary>
    private int _GRealReadoutLen = 0;

    /// <summary>
    /// 
    /// </summary>
    private Action<int> _GFuncUpdateProgress = null;

    #endregion

    #region 构造函数
    public CrifanLib()
    {
        //!!! for load embedded dll: (1) register resovle handler
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

        //http related
        _GUserAgent = ConstUserAgentIe8X64;
        //set max enough to avoid http request is used out -> avoid dead while get response 
        System.Net.ServicePointManager.DefaultConnectionLimit = 200;

        _GCurCookies = new CookieCollection();
        // init const cookie keys
        foreach (string key in _CookieFieldArr)
        {
            _GCookieFieldList.Add(key);
        }

        //init for calc time
        _CalcTimeList = new Dictionary<string, DateTime>();
#if USE_GETURLRESPONSE_BW
        _GBgwDownload = new BackgroundWorker();
#endif

        //debug
        //gProxy = new WebProxy("127.0.0.1", 8087);
    }
    #endregion

    #region 私有函数

    /*------------------------Private Functions------------------------------*/

    //!!! for load embedded dll: (2) implement this handler
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    /// <returns></returns>
    private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

        dllName = dllName.Replace(".", "_");

        if(dllName.EndsWith("_resources"))
            return null;

        ResourceManager rm = new ResourceManager(GetType().Namespace + ".Properties.Resources", Assembly.GetExecutingAssembly());

        byte[] bytes = (byte[]) rm.GetObject(dllName);

        return Assembly.Load(bytes);
    }

    // replace the constReplacedChar back to original ','
    /// <summary>
    /// 
    /// </summary>
    /// <param name="foundPprocessedExpire"></param>
    /// <returns></returns>
    public static string RecoverExpireField(Match foundPprocessedExpire)
    {
        string recovedStr = "";
        recovedStr = foundPprocessedExpire.Value.Replace(ConstReplacedChar, ',');
        return recovedStr;
    }

    //replace ',' with constReplacedChar
    /// <summary>
    /// 
    /// </summary>
    /// <param name="foundExpire"></param>
    /// <returns></returns>
    private static string ProcessExpireField(Match foundExpire)
    {
        string replacedComma = "";
        replacedComma = foundExpire.Value.ToString().Replace(',', ConstReplacedChar);
        return replacedComma;
    }

    //replace "0A" (in \x0A) into '\n'
    /// <summary>
    /// 
    /// </summary>
    /// <param name="foundEscapeSequence"></param>
    /// <returns></returns>
    private string ReplaceEscapeSequenceToChar(Match foundEscapeSequence)
    {
        char[] hexValues = new char[2];
        //string hexChars = foundEscapeSequence.Value.ToString();
        string matchedEscape = foundEscapeSequence.ToString();
        hexValues[0] = matchedEscape[2];
        hexValues[1] = matchedEscape[3];
        string hexValueString = new string(hexValues);
        int convertedInt = int.Parse(hexValueString, NumberStyles.HexNumber, NumberFormatInfo.InvariantInfo);
        char hexChar = Convert.ToChar(convertedInt);
        string hexStr = hexChar.ToString();
        return hexStr;
    }

    //check whether need add/retain this cookie
    // not add for:
    // ck is null or ck name is null
    // domain is null and curDomain is not set
    // expired and retainExpiredCookie==false
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ck"></param>
    /// <param name="curDomain"></param>
    /// <returns></returns>
    private static bool NeedAddThisCookie(Cookie ck, string curDomain)
    {
        bool needAdd = false;

        if((ck == null) || (ck.Name == ""))
        {
            needAdd = false;
        }
        else
        {
            if(ck.Domain != "")
            {
                needAdd = true;
            }
            else // ck.Domain == ""
            {
                if(curDomain != "")
                {
                    ck.Domain = curDomain;
                    needAdd = true;
                }
                else // curDomain == ""
                {
                    // not set current domain, omit this
                    // should not add empty domain cookie, for this will lead execute CookieContainer.Add() fail !!!
                    needAdd = false;
                }
            }
        }

        return needAdd;
    }

    //quote the input dict values
    //note: the return result for first para no '&'
    /// <summary>
    /// 
    /// </summary>
    /// <param name="paras"></param>
    /// <param name="spaceToPercent20"></param>
    /// <returns></returns>
    private static string _quoteParas(Dictionary<string, string> paras, bool spaceToPercent20 = true)
    {
        string quotedParas = "";
        bool isFirst = true;
        string val = "";
        foreach(string para in paras.Keys)
        {
            if(paras.TryGetValue(para, out val))
            {
                string encodedVal = "";
                if(spaceToPercent20)
                {
                    //encodedVal = HttpUtility.UrlPathEncode(val);
                    //encodedVal = Uri.EscapeDataString(val);
                    //encodedVal = Uri.EscapeUriString(val);
                    encodedVal = HttpUtility.UrlEncode(val).Replace("+", "%20");
                }
                else
                {
                    encodedVal = HttpUtility.UrlEncode(val); //space to +
                }

                if(isFirst)
                {
                    isFirst = false;
                    quotedParas += para + "=" + encodedVal;
                }
                else
                {
                    quotedParas += "&" + para + "=" + encodedVal;
                }
            }
            else
            {
                break;
            }
        }

        return quotedParas;
    }

    /* get url's response
     * */

    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headerDict"></param>
    /// <param name="postDict"></param>
    /// <param name="timeout"></param>
    /// <param name="postDataStr"></param>
    /// <param name="readWriteTimeout"></param>
    /// <returns></returns>
    private HttpWebResponse _getUrlResponse(string url, Dictionary<string, string> headerDict = DefHeaderDict, Dictionary<string, string> postDict = DefPostDict, int timeout = DefTimeout, string postDataStr = DefPostDataStr, int readWriteTimeout = DefReadWriteTimeout)
    {
        //CookieCollection parsedCookies;

        HttpWebResponse resp = null;

        HttpWebRequest req = (HttpWebRequest) WebRequest.Create(url);

        req.AllowAutoRedirect = true;
        req.Accept = "*/*";

        //req.ContentType = "text/plain";

        //const string gAcceptLanguage = "en-US"; // zh-CN/en-US
        //req.Headers["Accept-Language"] = gAcceptLanguage;

        req.KeepAlive = true;

        req.UserAgent = _GUserAgent;

        req.Headers["Accept-Encoding"] = "gzip, deflate";
        //req.AutomaticDecompression = DecompressionMethods.GZip;
        req.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

        req.Proxy = _GProxy;

        if(timeout > 0)
        {
            req.Timeout = timeout;
        }

        if(readWriteTimeout > 0)
        {
            //default ReadWriteTimeout is 300000=300 seconds = 5 minutes !!!
            //too long, so here change to 300000 = 30 seconds
            //for support TimeOut for later StreamReader's ReadToEnd
            req.ReadWriteTimeout = readWriteTimeout;
        }

        if(_GCurCookies != null)
        {
            req.CookieContainer = new CookieContainer();
            req.CookieContainer.PerDomainCapacity = 40; // following will exceed max default 20 cookie per domain
            req.CookieContainer.Add(_GCurCookies);
        }

        if((headerDict != null) && (headerDict.Count > 0))
        {
            foreach(string header in headerDict.Keys)
            {
                string headerValue = "";
                if(headerDict.TryGetValue(header, out headerValue))
                {
                    string lowecaseHeader = header.ToLower();
                    // following are allow the caller overwrite the default header setting
                    if(lowecaseHeader == "referer")
                    {
                        req.Referer = headerValue;
                    }
                    else if((lowecaseHeader == "allow-autoredirect") || (lowecaseHeader == "allowautoredirect") || (lowecaseHeader == "allow autoredirect"))
                    {
                        bool isAllow = false;
                        if(bool.TryParse(headerValue, out isAllow))
                        {
                            req.AllowAutoRedirect = isAllow;
                        }
                    }
                    else if(lowecaseHeader == "accept")
                    {
                        req.Accept = headerValue;
                    }
                    else if((lowecaseHeader == "keep-alive") || (lowecaseHeader == "keepalive") || (lowecaseHeader == "keep alive"))
                    {
                        bool isKeepAlive = false;
                        if(bool.TryParse(headerValue, out isKeepAlive))
                        {
                            req.KeepAlive = isKeepAlive;
                        }
                    }
                    else if((lowecaseHeader == "accept-language") || (lowecaseHeader == "acceptlanguage") || (lowecaseHeader == "accept language"))

                    {
                        req.Headers["Accept-Language"] = headerValue;
                    }
                    else if((lowecaseHeader == "user-agent") || (lowecaseHeader == "useragent") || (lowecaseHeader == "user agent"))
                    {
                        req.UserAgent = headerValue;
                    }
                    else if((lowecaseHeader == "content-type") || (lowecaseHeader == "contenttype") || (lowecaseHeader == "content type"))
                    {
                        req.ContentType = headerValue;
                    }
                    else
                    {
                        req.Headers[header] = headerValue;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if(((postDict != null) && (postDict.Count > 0)) || (!string.IsNullOrEmpty(postDataStr)))
        {
            req.Method = "POST";
            if(req.ContentType == null)
            {
                req.ContentType = "application/x-www-form-urlencoded";
            }

            if((postDict != null) && (postDict.Count > 0))
            {
                postDataStr = _quoteParas(postDict);
            }

            //byte[] postBytes = Encoding.GetEncoding("utf-8").GetBytes(postData);
            byte[] postBytes = Encoding.UTF8.GetBytes(postDataStr);
            req.ContentLength = postBytes.Length;

            try
            {
                Stream postDataStream = req.GetRequestStream();
                postDataStream.Write(postBytes, 0, postBytes.Length);
                postDataStream.Close();
            }
            catch(WebException webEx)
            {
                //for prev has set ReadWriteTimeout
                //so here also may timeout
                if(webEx.Status == WebExceptionStatus.Timeout)
                {
                    req = null;
                }
            }
        }
        else
        {
            req.Method = "GET";
        }

        if(req != null)
        {
            //may timeout, has fixed in:
            //http://www.crifan.com/fixed_problem_sometime_httpwebrequest_getresponse_timeout/
            try
            {
                resp = (HttpWebResponse) req.GetResponse();

                //type1: use dotnet parse cookies
                //updateLocalCookies(resp.Cookies, ref gCurCookies);

                //type2: use crifanLib.cs functions to parse cookie
                //update latest cookies
                string curDomain = ExtractDomain(url);
                CookieCollection parsedCookies = ParseSetCookie(resp.Headers["Set-Cookie"], curDomain);
                UpdateLocalCookies(parsedCookies, ref _GCurCookies);
            }
            catch(WebException webEx)
            {
                if(webEx.Status == WebExceptionStatus.Timeout)
                {
                    resp = null;
                }
            }
        }

        return resp;
    }

    #region user

#if USE_GETURLRESPONSE_BW
    private void getUrlResponse_bw(string url, Dictionary<string, string> headerDict = DefHeaderDict, Dictionary<string, string> postDict = DefPostDict, int timeout = DefTimeout, string postDataStr = DefPostDataStr, int readWriteTimeout = DefReadWriteTimeout)
    {
        // Create a background thread
        BackgroundWorker bgwGetUrlResp = new BackgroundWorker();
        bgwGetUrlResp.DoWork += new DoWorkEventHandler(bgwGetUrlResp_DoWork);
        bgwGetUrlResp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwGetUrlResp_RunWorkerCompleted);

        //init
        _BNotCompletedResp = true;

        // run in another thread
        object paraObj = new object[] {url, headerDict, postDict, timeout, postDataStr, readWriteTimeout};
        bgwGetUrlResp.RunWorkerAsync(paraObj);
    }

    private void bgwGetUrlResp_DoWork(object sender, DoWorkEventArgs e)
    {
        object[] paraObj = (object[]) e.Argument;
        string url = (string) paraObj[0];
        Dictionary<string, string> headerDict = (Dictionary<string, string>) paraObj[1];
        Dictionary<string, string> postDict = (Dictionary<string, string>) paraObj[2];
        int timeout = (int) paraObj[3];
        string postDataStr = (string) paraObj[4];
        int readWriteTimeout = (int) paraObj[5];

        e.Result = _getUrlResponse(url, headerDict, postDict, timeout, postDataStr, readWriteTimeout);
    }

    //void m_bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //{
    //    bRespNotCompleted = true;
    //}

    private void bgwGetUrlResp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // The background process is complete. We need to inspect
        // our response to see if an error occurred, a cancel was
        // requested or if we completed successfully.

        // Check to see if an error occurred in the
        // background process.
        if(e.Error != null)
        {
            //MessageBox.Show(e.Error.Message);
            return;
        }

        // Check to see if the background process was cancelled.
        if(e.Cancelled)
        {
            //MessageBox.Show("Cancelled ...");
        }
        else
        {
            _BNotCompletedResp = false;

            // Everything completed normally.
            // process the response using e.Result
            //MessageBox.Show("Completed...");
            _GCurResp = (HttpWebResponse) e.Result;
        }
    }
#endif

    #endregion


    private void getUrlRespStreamBytes_bw(ref Byte[] respBytesBuf, string url, Dictionary<string, string> headerDict, Dictionary<string, string> postDict, int timeout, Action<int> funcUpdateProgress)
    {
        // Create a background thread
        _GBgwDownload = new BackgroundWorker();
        _GBgwDownload.DoWork += bgwDownload_DoWork;
        _GBgwDownload.RunWorkerCompleted += bgwDownload_RunWorkerCompleted;
        _GBgwDownload.WorkerReportsProgress = true;
        _GBgwDownload.ProgressChanged += bgwDownload_ProgressChanged;

        //init
        _BNotCompletedDownload = true;
        _GFuncUpdateProgress = funcUpdateProgress;

        // run in another thread
        object paraObj = new object[] {respBytesBuf, url, headerDict, postDict, timeout};
        _GBgwDownload.RunWorkerAsync(paraObj);
    }

    private void bgwDownload_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        if(_GFuncUpdateProgress != null)
        {
            // This function fires on the UI thread so it's safe to edit
            // the UI control directly, no funny business with Control.Invoke.
            // Update the progressBar with the integer supplied to us from the
            // ReportProgress() function.  Note, e.UserState is a "tag" property
            // that can be used to send other information from the
            // BackgroundThread to the UI thread.

            _GFuncUpdateProgress(e.ProgressPercentage);
        }
    }

    private void bgwDownload_DoWork(object sender, DoWorkEventArgs e)
    {
        //    // The sender is the BackgroundWorker object we need it to
        //    // report progress and check for cancellation.
        //    BackgroundWorker gBgwDownload = sender as BackgroundWorker;

        object[] paraObj = (object[]) e.Argument;
        Byte[] respBytesBuf = (Byte[]) paraObj[0];
        string url = (string) paraObj[1];
        Dictionary<string, string> headerDict = (Dictionary<string, string>) paraObj[2];
        Dictionary<string, string> postDict = (Dictionary<string, string>) paraObj[3];
        int timeout = (int) paraObj[4];

        //e.Result = _getUrlRespStreamBytes(ref respBytesBuf, url, headerDict, postDict, timeout);


        int realReadoutLen = 0;
        int curBufPos = 0;

        try
        {
            //HttpWebResponse resp = getUrlResponse(url, headerDict, postDict, timeout);
            HttpWebResponse resp = GetUrlResponse(url, headerDict, postDict);
            long expectReadoutLen = resp.ContentLength;

            long totalLength = expectReadoutLen;

            Stream binStream = resp.GetResponseStream();
            //int streamDataLen  = (int)binStream.Length; // erro: not support seek operation

            int curReadoutLen;
            do
            {
                //let up layer update its UI, otherwise up layer UI will no response during this func exec time
                //now has make this function to call by backgroundworker, so not need this to update UI
                //System.Windows.Forms.Application.DoEvents();

                // here download logic is:
                // once request, return some data
                // request multiple time, until no more data
                curReadoutLen = binStream.Read(respBytesBuf, curBufPos, (int) expectReadoutLen);
                if(curReadoutLen > 0)
                {
                    curBufPos += curReadoutLen;

                    long currentLength = curBufPos;

                    expectReadoutLen = expectReadoutLen - curReadoutLen;

                    realReadoutLen += curReadoutLen;

                    int currentPercent = (int) ((currentLength*100)/totalLength);

                    if(currentPercent < 0)
                    {
                        currentPercent = 0;
                    }

                    if(currentPercent > 100)
                    {
                        currentPercent = 100;
                    }

                    _GBgwDownload.ReportProgress(currentPercent);
                }
            } while(curReadoutLen > 0);
        }
        catch(Exception ex)
        {
            string errorMessage = ex.Message;
            realReadoutLen = -1;
        }

        //return realReadoutLen;

        e.Result = realReadoutLen;
        //gBgwDownload.ReportProgress(100);
    }

    private void bgwDownload_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    {
        // The background process is complete. We need to inspect
        // our response to see if an error occurred, a cancel was
        // requested or if we completed successfully.

        // Check to see if an error occurred in the
        // background process.
        if(e.Error != null)
        {
            //MessageBox.Show(e.Error.Message);
            return;
        }

        // Check to see if the background process was cancelled.
        if(e.Cancelled)
        {
            //MessageBox.Show("Cancelled ...");
        }
        else
        {
            _BNotCompletedDownload = false;

            // Everything completed normally.
            // process the response using e.Result
            //MessageBox.Show("Completed...");
            _GRealReadoutLen = (int) e.Result;
        }
    }

    #endregion

    #region 公共函数
  

    #region TreeView


    /*
     * [Function]
     * find root TreeNode of current TreeNode
     * [Input]
     * some TreeNode
     * 
     * [Output]
     * root TreeNode of input TreeNode
     * 
     * [Note]
     */
    /// <summary>
    /// 从当前的TreeNode找出根TreeNode
    /// </summary>
    /// <param name="curTreeNode">当前的TreeNode</param>
    /// <returns></returns>
    public TreeNode FindRootTreeNode(TreeNode curTreeNode)
    {
        TreeNode rootTreeNode = curTreeNode.Parent;

        if(rootTreeNode == null)
        {
            //root parent is null
            rootTreeNode = curTreeNode;
        }
        else
        {
            //child parent is not null
            while(rootTreeNode.Parent != null)
            {
                rootTreeNode = rootTreeNode.Parent;
            }
        }

        return rootTreeNode;
    }

    /*
     * [Function]
     * un highlight tree node
     * [Input]
     * some TreeNode
     * 
     * [Output]
     * restore color to background color
     * 
     * [Note]
     */

    /// <summary>
    /// 取消指定的高亮的TreeNode.
    /// </summary>
    /// <param name="trvValue"></param>
    /// <param name="treeNode"></param>
    /// <returns></returns>
    public Color UnHighlightNode(TreeView trvValue, TreeNode treeNode)
    {
        Color oldColor = trvValue.BackColor;
        if(treeNode != null)
        {
            oldColor = treeNode.BackColor;
            treeNode.BackColor = trvValue.BackColor;
            treeNode.ForeColor = Color.Black;
        }

        return oldColor;
    }

    /*
     * [Function]
     * highlight tree node
     * [Input]
     * some TreeNode
     * 
     * [Output]
     * set color to highlighted color
     * 
     * [Note]
     */

    /// <summary>
    /// 高亮指定的TreeNode
    /// </summary>
    /// <param name="trvValue"></param>
    /// <param name="someNode"></param>
    /// <returns></returns>
    public Color HighlightNode(TreeView trvValue, TreeNode someNode)
    {
        Color oldColor = trvValue.BackColor; //"{Name=Window, ARGB=(255, 255, 255, 255)}"
        if(someNode != null)
        {
            oldColor = someNode.BackColor; //"{Name=0, ARGB=(0, 0, 0, 0)}"

            // HTML #3399FF -> RGB(51,153,255)
            //"{Name=MenuHighlight, ARGB=(255, 51, 153, 255)}"
            someNode.BackColor = SystemColors.MenuHighlight;

            //node.BackColor = nodeHlBackColor;

            //node.ForeColor = Color.FromArgb(255, 255, 255);
            someNode.ForeColor = Color.White;
        }

        return oldColor;
    }


    /*********************************************************************/

    #endregion

    #region 单位转换
    /// <summary>
    ///  盎司转千克
    /// </summary>
    /// <param name="ounce"></param>
    /// <returns></returns>
    public float OunceToKiloGram(float ounce)
    {
        float kiloGram = ounce*0.028349523125F;

        return kiloGram;
    }
    /// <summary>
    /// 千克转盎司
    /// </summary>
    /// <param name="kiloGram"></param>
    /// <returns></returns>
    public float KiloGramToOunce(float kiloGram)
    {
        float ounce = kiloGram*35.27396194958F;

        return ounce;
    }
    /// <summary>
    /// 英镑转千克
    /// </summary>
    /// <param name="pound"></param>
    /// <returns></returns>
    public float PoundToKiloGram(float pound)
    {
        float kiloGram = pound*0.45359237F;

        return kiloGram;
    }
    /// <summary>
    /// 千克转英镑.
    /// </summary>
    /// <param name="kiloGram"></param>
    /// <returns></returns>
    public float KiloGramToPound(float kiloGram)
    {
        float pound = kiloGram*0.45359237F;

        return pound;
    }
    /// <summary>
    /// 英尺转厘米
    /// </summary>
    /// <param name="inch"></param>
    /// <returns></returns>
    public float InchToCm(float inch)
    {
        float cm = inch*2.54F;

        return cm;
    }
    /// <summary>
    /// 厘米转英尺
    /// </summary>
    /// <param name="cm"></param>
    /// <returns></returns>
    public float CmToInch(float cm)
    {
        float inch = cm*0.39370078740157F;

        return inch;
    }

    #endregion

    /*********************************************************************/
    /* Values: int/double/... */
    /*********************************************************************/

    //
    //
    /// <summary>
    /// 和Javascript中Math.Random()等价的函数:mathRandom
    /// equivalent of Math.Random() in Javascript
    /// <para><![CDATA[get a 17 bit double value x, 0 < x < 1, eg:0.68637410117610087]]></para>
    /// </summary>
    /// <returns></returns>
    public double MathRandom()
    {
        Random rdm = new Random();
        double betweenZeroToOne17Bit = rdm.NextDouble();
        return betweenZeroToOne17Bit;
    }

    #region 时间处理

    /// <summary>
    /// init for calculate time span
    /// </summary>
    /// <param name="keyName"></param>
    public void ElapsedTimeSpanInit(string keyName)
    {
        _CalcTimeList.Add(keyName, DateTime.Now);
    }

    // got calculated time span
    /// <summary>
    /// 
    /// </summary>
    /// <param name="keyName"></param>
    /// <returns></returns>
    public double GetElapsedTimeSpan(string keyName)
    {
        double milliSec = 0.0;
        if(_CalcTimeList.ContainsKey(keyName))
        {
            DateTime startTime = _CalcTimeList[keyName];
            DateTime endTime = DateTime.Now;
            milliSec = (endTime - startTime).TotalMilliseconds;
        }
        return milliSec;
    }

    //refer: http://bytes.com/topic/c-sharp/answers/713458-c-function-equivalent-javascript-gettime-function
    //get current time in milli-second-since-epoch(1970/01/01)
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public double GetCurTimeInMillisec()
    {
        DateTime st = new DateTime(1970, 1, 1);
        TimeSpan t = (DateTime.Now - st);
        return t.TotalMilliseconds; // milli seconds since epoch
    }

    // 
    /// <summary>
    /// <para>将毫秒转换为（自1970年1月1日以来的）本地时间:milliSecToDateTime</para>
    /// parse the milli second to local DateTime value
    /// </summary>
    /// <param name="milliSecSinceEpoch"></param>
    /// <returns></returns>
    private static DateTime MilliSecToDateTime(double milliSecSinceEpoch)
    {
        DateTime st = new DateTime(1970, 1, 1, 0, 0, 0);
        st = st.AddMilliseconds(milliSecSinceEpoch);
        return st;
    }


    /// <summary>
    /// 将Javascript中的"new Date(xxx)"转换为C#中的DateTime变量:parseJsNewDate
    /// <para>parse xxx in "new Date(xxx)" of javascript to C# DateTime</para>
    /// <para>input example:</para>
    /// <para>new Date(1329198041411.84) / new Date(1329440307389.9) / new Date(1329440307483)</para>
    /// </summary>
    /// <param name="newDateStr"></param>
    /// <param name="parsedDatetime"></param>
    /// <returns></returns>
    public static bool ParseJsNewDate(string newDateStr, out DateTime parsedDatetime)
    {
        bool parseOk = false;
        parsedDatetime = new DateTime();

        if((newDateStr != "") && (newDateStr.Trim() != ""))
        {
            string dateValue = "";
            if(ExtractSingleStr(@".*new\sDate\((.+?)\).*", newDateStr, out dateValue))
            {
                double doubleVal = 0.0;
                if(Double.TryParse(dateValue, out doubleVal))
                {
                    // try whether is double/int64 milliSecSinceEpoch
                    parsedDatetime = MilliSecToDateTime(doubleVal);
                    parseOk = true;
                }
                else if(DateTime.TryParse(dateValue, out parsedDatetime))
                {
                    // try normal DateTime string
                    //refer: http://www.w3schools.com/js/js_obj_date.asp
                    //October 13, 1975 11:13:00
                    //79,5,24 / 79,5,24,11,33,0
                    //1329198041411.3344 / 1329198041411.84 / 1329198041411
                    parseOk = true;
                }
            }
        }

        return parseOk;
    }

    #endregion

    #region 字符串处理



    //
    //
    /// <summary>
    /// 将字符格式化.用指定的字符对齐.
    /// <para>input: [4] Valid: B0009IQZFM</para>
    /// <para>output: ============================ [4] Valid: B0009IQZFM =============================</para>
    /// </summary>
    /// <param name="strToFormat">进行格式化的字符串</param>
    /// <param name="cPaddingChar">指定的字符.</param>
    /// <param name="iTotalWidth">每行的宽度.</param>
    /// <returns></returns>
    public string FormatString(string strToFormat, char cPaddingChar = '*', int iTotalWidth = 80)
    {
        //auto added space
        strToFormat = " " + strToFormat + " "; //" [4] Valid: B0009IQZFM "

        //1. padding left
        int iPaddingLen = (iTotalWidth - strToFormat.Length)/2;
        int iLefTotalLen = iPaddingLen + strToFormat.Length;
        string strLefPadded = strToFormat.PadLeft(iLefTotalLen, cPaddingChar); //"============================ [4] Valid: B0009IQZFM "
        //2. padding right
        string strFormatted = strLefPadded.PadRight(iTotalWidth, cPaddingChar); //"============================ [4] Valid: B0009IQZFM ============================="

        return strFormatted;
    }

 
    /// <summary>
    /// 将字符串数组中的元素 填充为 empty.
    /// </summary>
    /// <param name="strArr">字符串数组</param>
    /// <returns></returns>
    public string[] EmptyStringArray(string[] strArr)
    {
        if(strArr != null)
        {
            for(int idx = 0; idx < strArr.Length; idx++)
            {
                strArr[idx] = String.Empty;
            }
        }
        return strArr;
    }

    /// <summary>
    /// 将字符串中的"!"字符编码为"%21".
    /// </summary>
    /// <param name="inputStr"></param>
    /// <returns></returns>
    public static string EncodeExclamationMark(string inputStr)
    {
        return inputStr.Replace("!", "%21");
    }

    /// <summary>
    /// 将字符串中的"%21"的字符转换为"!"
    /// </summary>
    /// <param name="inputStr"></param>
    /// <returns></returns>
    public static string DecodeExclamationMark(string inputStr)
    {
        return inputStr.Replace("%21", "!");
    }


    /// <summary>
    ///     //using Regex to extract single string value
    // caller should make sure the string to extract is Groups[1] == include single () !!!
    /// </summary>
    /// <param name="pattern"></param>
    /// <param name="extractFrom"></param>
    /// <param name="extractedStr"></param>
    /// <param name="regexOption"></param>
    /// <returns></returns>
    public static bool ExtractSingleStr(string pattern, string extractFrom, out string extractedStr, RegexOptions regexOption = RegexOptions.None)
    {
        bool extractOk = false;
        Regex rx = new Regex(pattern, regexOption);
        Match found = rx.Match(extractFrom);
        if(found.Success)
        {
            extractOk = true;
            extractedStr = found.Groups[1].ToString();
        }
        else
        {
            extractOk = false;
            extractedStr = "";
        }

        return extractOk;
    }

    /// <summary>
    /// 
    /// remove invalid char in path and filename
    /// </summary>
    /// <param name="origFileOrPathStr"></param>
    /// <returns></returns>
    public string RemoveInvChrInPath(string origFileOrPathStr)
    {
        string validFileOrPathStr = origFileOrPathStr;

        //filter out invalid title and artist char
        //char[] invalidChars = { '\\', '/', ':', '*', '?', '<', '>', '|', '\b' };
        char[] invalidChars = Path.GetInvalidPathChars();
        char[] invalidCharsInName = Path.GetInvalidFileNameChars();

        foreach(char chr in invalidChars)
        {
            validFileOrPathStr = validFileOrPathStr.Replace(chr.ToString(), "");
        }

        foreach(char chr in invalidCharsInName)
        {
            validFileOrPathStr = validFileOrPathStr.Replace(chr.ToString(), "");
        }

        return validFileOrPathStr;
    }

    //convert \xXX into corresponding char
    //eg: \x0A -> '\n'
    public string FilterEscapeSequence(string esacapeSequenceStr)
    {
        string filteredStr = Regex.Replace(esacapeSequenceStr, @"\\x\w{2}", new MatchEvaluator(ReplaceEscapeSequenceToChar));

        return filteredStr;
    }


    /// <summary>
    /// 从带有文件名的Url中提取文件名.
    /// <para>例如:http://g-ecx.images-amazon.com/images/G/01/kindle/dp/2012/KC/KC-slate-01-lg._V401028090_.jpg</para>
    /// <para>结果:KC-slate-01-lg._V401028090_.jpg</para>
    /// <para>又如:file:///C:/Users/CLi/AppData/Local/Temp/WindowsLiveWriter-1737927945/supfilesC19F10/now-the-service-status-is-active_thu%5B1%5D.png</para>
    /// <para>结果:now-the-service-status-is-active_thu%5B1%5D.png</para>
    /// </summary>
    /// <param name="fullUrl"></param>
    /// <returns></returns>
    public string ExtractFilenameFromUrl(string fullUrl)
    {
        string filename = "";
        string[] slashList = fullUrl.Split('/');
        filename = slashList[slashList.Length - 1];
        return filename;
    }

    #endregion

    #region 数组处理


    //given a string array 'origStrArr', get a sub string array from 'startIdx', length is 'len'
    /// <summary>
    /// 
    /// </summary>
    /// <param name="origStrArr"></param>
    /// <param name="startIdx"></param>
    /// <param name="len"></param>
    /// <returns></returns>
    public static string[] GetSubStrArr(string[] origStrArr, int startIdx, int len)
    {
        string[] subStrArr = new string[] {};
        if((origStrArr != null) && (origStrArr.Length > 0) && (len > 0))
        {
            List<string> strList = new List<string>();
            int endPos = startIdx + len;
            if(endPos > origStrArr.Length)
            {
                endPos = origStrArr.Length;
            }

            for(int i = startIdx; i < endPos; i++)
            {
                //refer: http://zhidao.baidu.com/question/296384408.html
                strList.Add(origStrArr[i]);
            }

            subStrArr = new string[len];
            strList.CopyTo(subStrArr);
        }

        return subStrArr;
    }

    #endregion

    #region 序列化

    /*********************************************************************/
    /* Serialize/Deserialize */
    /*********************************************************************/

    // serialize an object to string
    public bool SerializeObjToStr(Object obj, out string serializedStr)
    {
        bool serializeOk = false;
        serializedStr = "";
        try
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            serializedStr = Convert.ToBase64String(memoryStream.ToArray());

            serializeOk = true;
        }
        catch
        {
            serializeOk = false;
        }

        return serializeOk;
    }

    // deserialize the string to an object
    public bool DeserializeStrToObj(string serializedStr, out object deserializedObj)
    {
        bool deserializeOk = false;
        deserializedObj = null;

        try
        {
            byte[] restoredBytes = Convert.FromBase64String(serializedStr);
            MemoryStream restoredMemoryStream = new MemoryStream(restoredBytes);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            deserializedObj = binaryFormatter.Deserialize(restoredMemoryStream);

            deserializeOk = true;
        }
        catch
        {
            deserializeOk = false;
        }

        return deserializeOk;
    }

    #endregion

    #region HTTP

    /*********************************************************************/
    /* HTTP */
    /*********************************************************************/

    /* set proxy
     * Note:
     * 1. current only support http proxy
     * 2. current only support single proxy
     */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="proxyIp"></param>
    /// <param name="proxyPort"></param>
    public void SetProxy(string proxyIp, int proxyPort)
    {
        _GProxy = new WebProxy(proxyIp, proxyPort);
    }

    /*
     * Note: currently support auto handle cookies
     * currently only support single caller -> multiple caller of these functions will cause cookies accumulated
     * you can clear previous cookies to avoid unexpected result by call clearCurCookies
     */
    /// <summary>
    /// 
    /// </summary>
    public void ClearCurCookies()
    {
        if(_GCurCookies != null)
        {
            _GCurCookies = null;
            _GCurCookies = new CookieCollection();
        }
    }

    /* get current cookies */
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public CookieCollection GetCurCookies()
    {
        return _GCurCookies;
    }

    /* set current cookies */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cookies"></param>
    public void SetCurCookies(CookieCollection cookies)
    {
        _GCurCookies = cookies;
    }

    /* get url's response
    * */
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headerDict"></param>
    /// <param name="postDict"></param>
    /// <param name="timeout"></param>
    /// <param name="postDataStr"></param>
    /// <param name="readWriteTimeout"></param>
    /// <returns></returns>
    public HttpWebResponse GetUrlResponse(string url, Dictionary<string, string> headerDict = DefHeaderDict, Dictionary<string, string> postDict = DefPostDict, int timeout = DefTimeout, string postDataStr = DefPostDataStr, int readWriteTimeout = DefReadWriteTimeout)
    {
#if USE_GETURLRESPONSE_BW
        //BackgroundWorker Version getUrlResponse
        HttpWebResponse localCurResp = null;
        getUrlResponse_bw(url, headerDict, postDict, timeout, postDataStr, readWriteTimeout);
        while(_BNotCompletedResp)
        {
            Application.DoEvents();
        }
        localCurResp = _GCurResp;

        //clear
        _GCurResp = null;

        return localCurResp;
#else
    //non-BackgroundWorker Version getUrlResponse
        return _getUrlResponse(url, headerDict, postDict, timeout, postDataStr);;
#endif
    }

    // valid charset:"GB18030"/"UTF-8", invliad:"UTF8"
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headerDict"></param>
    /// <param name="charset"></param>
    /// <param name="postDict"></param>
    /// <param name="timeout"></param>
    /// <param name="postDataStr"></param>
    /// <param name="readWriteTimeout"></param>
    /// <returns></returns>
    public string GetUrlRespHtml(string url, Dictionary<string, string> headerDict = DefHeaderDict, string charset = DefCharset, Dictionary<string, string> postDict = DefPostDict, int timeout = DefTimeout, string postDataStr = DefPostDataStr, int readWriteTimeout = DefReadWriteTimeout)
    {
        string respHtml = "";

        HttpWebResponse resp = GetUrlResponse(url, headerDict, postDict, timeout, postDataStr, readWriteTimeout);

        //long realRespLen = resp.ContentLength;
        if(resp != null)
        {
            StreamReader sr;
            Stream respStream = resp.GetResponseStream();
            if(!string.IsNullOrEmpty(charset))
            {
                Encoding htmlEncoding = Encoding.GetEncoding(charset);
                sr = new StreamReader(respStream, htmlEncoding);
            }
            else
            {
                sr = new StreamReader(respStream);
            }

            try
            {
                respHtml = sr.ReadToEnd();

                //while (!sr.EndOfStream)
                //{
                //    respHtml = respHtml + sr.ReadLine();
                //}

                //string curLine = "";
                //while ((curLine = sr.ReadLine()) != null)
                //{
                //    respHtml = respHtml + curLine;
                //}

                ////http://msdn.microsoft.com/zh-cn/library/system.io.streamreader.peek.aspx
                //while (sr.Peek() > -1) //while not error or not reach end of stream
                //{
                //    respHtml = respHtml + sr.ReadLine();
                //}

                //respStream.Close();
                //sr.Close();
                //resp.Close();
            }
            catch(Exception ex)
            {
                //【未解决】C#中StreamReader中遇到异常：未处理ObjectDisposedException,无法访问已关闭的流
                //http://www.crifan.com/csharp_streamreader_unhandled_exception_objectdisposedexception_cannot_access_closed_stream
                //System.ObjectDisposedException
                respHtml = "";
            } finally
            {
                if(respStream != null)
                {
                    respStream.Close();
                }
                if(sr != null)
                {
                    sr.Close();
                }
                if(resp != null)
                {
                    resp.Close();
                }
            }
        }

        return respHtml;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="url"></param>
    /// <param name="headerDict"></param>
    /// <param name="charset"></param>
    /// <param name="postDict"></param>
    /// <param name="timeout"></param>
    /// <param name="postDataStr"></param>
    /// <param name="readWriteTimeout"></param>
    /// <param name="maxTryNum"></param>
    /// <param name="retryFailSleepTime"></param>
    /// <returns></returns>
    public string getUrlRespHtml_multiTry(string url, Dictionary<string, string> headerDict = DefHeaderDict, string charset = DefCharset, Dictionary<string, string> postDict = DefPostDict, int timeout = DefTimeout, string postDataStr = DefPostDataStr, int readWriteTimeout = DefReadWriteTimeout, int maxTryNum = DefMaxTryNum, int retryFailSleepTime = DefRetryFailSleepTime)
    {
        string respHtml = "";

        for(int tryIdx = 0; tryIdx < maxTryNum; tryIdx++)
        {
            respHtml = GetUrlRespHtml(url, headerDict, charset, postDict, timeout, postDataStr, readWriteTimeout);
            if(!string.IsNullOrEmpty(respHtml))
            {
                break;
            }
            else
            {
                //something wrong
                //maybe network is not stable
                //so wait some time, then re-do it
                Thread.Sleep(retryFailSleepTime);
            }
        }

        return respHtml;
    }

    ////return current download percentage (max=100)
    //public int getCurDownloadPercent()
    //{
    //    int curPercent = 0;
    //    if ((currentLength > 0) && (totalLength > 0))
    //    {
    //        //NOTE: here currentLength * 100 maybe exceed 2^31=2147483648, so here must use long, can NOT use int
    //        //otherwise it will becomd nagative value
    //        curPercent = (int)((currentLength * 100) / totalLength);
    //    }

    //    return (int)curPercent;
    //}

    //public int _getUrlRespStreamBytes(ref Byte[] respBytesBuf,
    //                                string url,
    //                                Dictionary<string, string> headerDict,
    //                                Dictionary<string, string> postDict,
    //                                int timeout)
    //{
    //    int curReadoutLen;
    //    int realReadoutLen = 0;
    //    int curBufPos = 0;

    //    try
    //    {
    //        //HttpWebResponse resp = getUrlResponse(url, headerDict, postDict, timeout);
    //        HttpWebResponse resp = getUrlResponse(url, headerDict, postDict);
    //        long expectReadoutLen = resp.ContentLength;

    //        totalLength = expectReadoutLen;
    //        currentLength = 0;

    //        Stream binStream = resp.GetResponseStream();
    //        //int streamDataLen  = (int)binStream.Length; // erro: not support seek operation

    //        do
    //        {
    //            //let up layer update its UI, otherwise up layer UI will no response during this func exec time
    //            //now has make this function to call by backgroundworker, so not need this to update UI
    //            //System.Windows.Forms.Application.DoEvents();

    //            // here download logic is:
    //            // once request, return some data
    //            // request multiple time, until no more data
    //            curReadoutLen = binStream.Read(respBytesBuf, curBufPos, (int)expectReadoutLen);
    //            if (curReadoutLen > 0)
    //            {
    //                curBufPos += curReadoutLen;

    //                currentLength = curBufPos;

    //                expectReadoutLen = expectReadoutLen - curReadoutLen;

    //                realReadoutLen += curReadoutLen;
    //            }
    //        } while (curReadoutLen > 0);
    //    }
    //    catch(Exception ex)
    //    {
    //        realReadoutLen = -1;
    //    }

    //    return realReadoutLen;
    //}
    /// <summary>
    /// 
    /// </summary>
    /// <param name="respBytesBuf"></param>
    /// <param name="url"></param>
    /// <param name="headerDict"></param>
    /// <param name="postDict"></param>
    /// <param name="timeout"></param>
    /// <param name="funcUpdateProgress"></param>
    /// <returns></returns>
    public int GetUrlRespStreamBytes(ref Byte[] respBytesBuf, string url, Dictionary<string, string> headerDict, Dictionary<string, string> postDict, int timeout, Action<int> funcUpdateProgress)
    {
        int realReadoutLen = 0;
        getUrlRespStreamBytes_bw(ref respBytesBuf, url, headerDict, postDict, timeout, funcUpdateProgress);
        while(_BNotCompletedDownload)
        {
            Application.DoEvents();
        }
        realReadoutLen = _GRealReadoutLen;

        //clear
        _GRealReadoutLen = 0;

        return realReadoutLen;
    }


    //-----------------------------------------------------------------------------
    //translate strToTranslate from fromLanguage to toLanguage
    //return the translated string
    //return empty string if error
    //some frequently used language abbrv:
    //Chinese Simplified:   zh-CN
    //Chinese Traditional:  zh-TW
    //English:              en
    //German:               de
    //Japanese:             ja
    //Korean:               ko
    //French:               fr    
    //more can be found at: 
    //http://code.google.com/intl/ru/apis/language/translate/v2/using_rest.html#language-params
    /// <summary>
    /// 
    /// </summary>
    /// <param name="strToTranslate"></param>
    /// <param name="fromLanguage"></param>
    /// <param name="toLanguage"></param>
    /// <returns></returns>
    public string TranslateString(string strToTranslate, string fromLanguage, string toLanguage)
    {
        string translatedStr = "";

        ////following refer: http://python.u85.us/viewnews-335.html
        //string googleTranslateUrl = "http://translate.google.cn/translate_t";
        //Dictionary<string, string> postDict = new Dictionary<string, string>();
        //postDict.Add("hl", "zh-CN");
        //postDict.Add("ie", "UTF-8");
        //postDict.Add("text", strToTranslate);
        //postDict.Add("langpair", fromLanguage + "|" + toLanguage);
        //const string googleTransHtmlCharset = "UTF-8";
        //string transRetHtml = getUrlRespHtml(googleTranslateUrl, charset:googleTransHtmlCharset, postDict:postDict);


        ////http://translate.google.cn/#zh-CN/en/%E4%BB%96%E4%BB%AC%E6%98%AF%E8%BF%99%E6%A0%B7%E8%AF%B4%E7%9A%84
        //string googleTransBaseUrl = "http://translate.google.cn/#";
        //strToTranslate = "他们是这样说的";
        //string encodedStr = HttpUtility.UrlEncode(strToTranslate);
        //string googleTransUrl = googleTransBaseUrl + fromLanguage + "/" + toLanguage + "/" + encodedStr;
        //string transRetHtml = getUrlRespHtml(googleTransUrl);


        //http://translate.google.cn/translate_a/t?client=t&text=%E4%BB%96%E4%BB%AC%E6%98%AF%E8%BF%99%E6%A0%B7%E8%AF%B4%E7%9A%84&hl=zh-CN&sl=zh-CN&tl=en&ie=UTF-8&oe=UTF-8&multires=1&ssel=0&tsel=0&sc=1
        //strToTranslate = "他们是这样说的";
        string encodedStr = HttpUtility.UrlEncode(strToTranslate);
        string googleTransBaseUrl = "http://translate.google.cn/translate_a/t?";
        string googleTransUrl = googleTransBaseUrl;
        googleTransUrl += "&client=" + "t";
        googleTransUrl += "&text=" + encodedStr;
        googleTransUrl += "&hl=" + "zh-CN";
        googleTransUrl += "&sl=" + fromLanguage; // source   language
        googleTransUrl += "&tl=" + toLanguage; // to       language
        googleTransUrl += "&ie=" + "UTF-8"; // input    encode
        googleTransUrl += "&oe=" + "UTF-8"; // output   encode

        try
        {
            string transRetHtml = getUrlRespHtml_multiTry(googleTransUrl);
            //[[["They say","他们是这样说的","","Tāmen shì zhèyàng shuō de"]],,"zh-CN",,[["They",[5],0,0,1000,0,1,0],["say",[6],1,0,1000,1,2,0]],[["他们 是",5,[["They",1000,0,0],["they are",0,0,0],["they were",0,0,0],["that they are",0,0,0],["they are the",0,0,0]],[[0,3]],"他们是这样说的"],["这样 说",6,[["say",1000,1,0],["said",0,1,0],["say so",0,1,0],["says",0,1,0],["say this",0,1,0]],[[3,6]],""]],,,[["zh-CN"]],1]

            if(ExtractSingleStr(@"\[\[\[""(.+?)"","".+?"",", transRetHtml, out translatedStr))
            {
                //extrac out:They say
            }
        }
        catch
        {
            // if pass some special string, such as "彭德怀", then will occur 500 error
            // here tmp not process the error, just omit it here
        }

        return translatedStr;
    }

    public string TransZhcnToEn(string strToTranslate)
    {
        return TranslateString(strToTranslate, "zh-CN", "en");
    }


    //get page rank for some domain url
    //para: http://answers.yahoo.com
    //return: 7
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domainUrl"></param>
    /// <returns></returns>
    public int GetDomainPageRank(string domainUrl)
    {
        int pageRank = 0;
        string queryUrl = "";
        string respHtml = "";
        Dictionary<string, string> postDict = new Dictionary<string, string>();
        string rankStr = "";
        bool prevMethodFail = true;

        if((pageRank == 0) && prevMethodFail)
        {
            //Method 1: use http://www.pagerankme.com/
            queryUrl = "http://www.pagerankme.com/";
            postDict = new Dictionary<string, string>();
            postDict.Add("url", domainUrl);
            respHtml = getUrlRespHtml_multiTry(queryUrl, postDict: postDict);
            //<a href="http://www.pagerankme.com" target="_blank" style="text-decoration:none;color:#000000;">PageRank 7</a>
            rankStr = "";
            if(ExtractSingleStr(@"<a href=""http://www\.pagerankme\.com"" target=""_blank"" style="".+?"">PageRank (\d+)</a>", respHtml, out rankStr))
            {
                pageRank = Int32.Parse(rankStr);
                prevMethodFail = false;
            }
            else
            {
                prevMethodFail = true;
            }
        }

        if((pageRank == 0) && prevMethodFail)
        {
            //Method 2: use http://moonsy.com/pagerank_checker/
            //(1) http://moonsy.com/pagerank_checker/
            queryUrl = "http://moonsy.com/pagerank_checker/";
            postDict = new Dictionary<string, string>();
            postDict.Add("domain", domainUrl);
            postDict.Add("Submit", "CHECK");

            respHtml = getUrlRespHtml_multiTry(queryUrl, postDict: postDict);

            //<h3>Your Page Rank: 7/10
            rankStr = "";
            if(ExtractSingleStr(@"<h3>Your Page Rank.+?(\d+)/10", respHtml, out rankStr))
            {
                pageRank = Int32.Parse(rankStr);
                prevMethodFail = false;
            }
            else
            {
                prevMethodFail = true;
            }
        }

        if((pageRank == 0) && prevMethodFail)
        {
            //Method 3: use http://pagerank.webmasterhome.cn/
            string noHttpPreDomainUrl = Regex.Replace(domainUrl, "((https)|(http)|(ftp))://", "");

            //http://pagerank.webmasterhome.cn/prLoading.asp?domain=answers.yahoo.com

            string tmpRespHtml = "";
            Dictionary<string, string> headerDict;
            //(1)to get cookies
            string pageRankMainUrl = "http://pagerank.webmasterhome.cn/";
            tmpRespHtml = getUrlRespHtml_multiTry(pageRankMainUrl);
            //(2)ask page rank
            string firstBaseUrl = "http://pagerank.webmasterhome.cn/?domain=";
            //http://pagerank.webmasterhome.cn/?domain=answers.yahoo.com
            string firstWholeUrl = firstBaseUrl + noHttpPreDomainUrl;
            headerDict = new Dictionary<string, string>();
            headerDict.Add("referer", pageRankMainUrl);
            tmpRespHtml = getUrlRespHtml_multiTry(firstWholeUrl, headerDict: headerDict);

            string baseUrl = "http://pagerank.webmasterhome.cn/prLoading.asp?domain=";
            //http://pagerank.webmasterhome.cn/prLoading.asp?domain=answers.yahoo.com
            queryUrl = baseUrl + noHttpPreDomainUrl;
            headerDict = new Dictionary<string, string>();
            headerDict.Add("referer", firstWholeUrl);
            respHtml = getUrlRespHtml_multiTry(queryUrl, headerDict: headerDict);

            //'<img src=\"http://primg.webmasterhome.cn/pr7.gif\" style=\"width:40px;height:5px;border:0px;\" alt=PageRank align=absmiddle> (7/10)'
            rankStr = "";
            if(ExtractSingleStr(@"\((\d+)/10\)", respHtml, out rankStr))
            {
                pageRank = Int32.Parse(rankStr);
                prevMethodFail = false;
            }
            else
            {
                prevMethodFail = true;
            }
        }

        //TODO:
        //Google PR (PageRank) Checker
        //http://www.searchbliss.com/seo-tools/google-pagerank-checker.php
        //tmp is "We're sorry, the Google PR check is currently being repaired."
        //future: if Ok, mayby can use it

        return pageRank;
    }

    //get alexa rank for some domain url
    //para: http://answers.yahoo.com
    //return: 4
    /// <summary>
    /// 
    /// </summary>
    /// <param name="domainUrl"></param>
    /// <returns></returns>
    public int GetDomainAlexaRank(string domainUrl)
    {
        int alexaRank = 0;
        string queryUrl = "";
        string respHtml = "";
        Dictionary<string, string> postDict = new Dictionary<string, string>();
        string alexaRankStr = "";
        bool prevMethodFail = true;

        //string noHttpPreDomainUrl = Regex.Replace(domainUrl, "((https)|(http)|(ftp))://", "");

        if((alexaRank == 0) && prevMethodFail)
        {
            //Method 1: use http://www.searchbliss.com/rank.asp
            string mainUrl = "http://www.searchbliss.com/rank.asp";
            respHtml = getUrlRespHtml_multiTry(mainUrl);
            //<input type="hidden" name="RAC" value="EIS">
            string accessCode = "";
            if(ExtractSingleStr(@"<input\s+type=""hidden""\s+name=""RAC""\s+value=""([A-Z]+)"">", respHtml, out accessCode))
            {
                queryUrl = "http://www.searchbliss.com/rank.asp";
                //AC	EIS
                //RAC	EIS
                //rank	http://hubpages.com
                postDict = new Dictionary<string, string>();
                //postDict.Add("domain", noHttpPreDomainUrl);
                postDict.Add("AC", accessCode);
                postDict.Add("RAC", accessCode);
                postDict.Add("rank", domainUrl);
                respHtml = getUrlRespHtml_multiTry(queryUrl, postDict: postDict);
                //<a href="http://www.alexa.com/data/details/main/http://hubpages.com" target="_blank">444</a>
                if(ExtractSingleStr(@"<a\s+href=""http://www\.alexa\.com/data/details/main/.+?""\s+target=""_blank"">(\d+)</a>", respHtml, out alexaRankStr))
                {
                    //alexaRank = Int32.Parse(alexaRankStr);
                    if(Int32.TryParse(alexaRankStr, out alexaRank))
                    {
                        prevMethodFail = false;
                    }
                    else
                    {
                        prevMethodFail = true;
                    }

                    prevMethodFail = false;
                }
                else
                {
                    prevMethodFail = true;
                }
            }
            else
            {
                prevMethodFail = true;
            }
        }

#if USE_HTML_PARSER_HTMLAGILITYPACK
        if ((alexaRank == 0) && prevMethodFail)
        {
            //Method 2: use http://www.alexa.com/
            string tmpUrl = "http://www.alexa.com";
            //to get cookies
            string tmpRespHtml = getUrlRespHtml_multiTry(tmpUrl);
            //then do work
            queryUrl = "http://www.alexa.com/search";
            //http://www.alexa.com/search?q=crifan.com&r=home_home&p=bigtop
            queryUrl += "?q=" + domainUrl;
            queryUrl += "&r=" + "home_home";
            queryUrl += "&p=" + "bigtop";
            respHtml = getUrlRespHtml_multiTry(queryUrl);

            HtmlAgilityPack.HtmlDocument htmlDoc = htmlToHtmlDoc(respHtml);
            HtmlNode rootHtmlNode = htmlDoc.DocumentNode;

            //<span>
            //<img class="align-top" src="/images/icons/globe-sm.gif" />
            //<span class="traffic-stat-label">Alexa Traffic Rank:</span>
            //<a href="/siteinfo/yahoo.com#trafficstats">
            //4</a>
            //</span>

            //<span class="traffic-stat-label">Alexa Traffic Rank:</span>
            //<a href="/siteinfo/crifan.com#trafficstats">
            //170,557</a>
            //</span>
            //HtmlNode trafficHtmlNode = rootHtmlNode.SelectSingleNode("//span/span[@class='traffic-stat-label']/a[@href]");
            //HtmlNode trafficHtmlNode = rootHtmlNode.SelectSingleNode("//span/span[@class='traffic-stat-label']/a]");
            //HtmlNodeCollection trafficHtmlNodes = rootHtmlNode.SelectNodes("//span/span[@class='traffic-stat-label']");
            HtmlNode trafficHtmlNode = rootHtmlNode.SelectSingleNode("//span/span[@class='traffic-stat-label']");
            if ((trafficHtmlNode != null) && (trafficHtmlNode.InnerText.StartsWith("Alexa Traffic Rank:")))
            {
                HtmlNode parentHtmlNode = trafficHtmlNode.ParentNode;
                HtmlNode aHrefNode = parentHtmlNode.SelectSingleNode(".//a[@href]");
                string tracfficNumberStr = aHrefNode.InnerText;
                alexaRankStr = tracfficNumberStr.Trim().Replace(",", "");
                                
                //speical:
                //"No Data"
                //alexaRank = Int32.Parse(alexaRankStr);
                if(Int32.TryParse(alexaRankStr, out alexaRank))
                {
                    prevMethodFail = false;
                }
                else
                {
                    prevMethodFail = true;
                }
            }
            else
            {
                prevMethodFail = true;
            }
        }
        #endif

        if((alexaRank == 0) && prevMethodFail)
        {
            //Method 3: use http://moonsy.com/alexa_rank/

            //(1) http://moonsy.com/alexa_rank/
            queryUrl = "http://moonsy.com/alexa_rank/";
            postDict = new Dictionary<string, string>();
            //postDict.Add("domain", noHttpPreDomainUrl);
            postDict.Add("domain", domainUrl);
            postDict.Add("Submit", "CHECK");

            respHtml = getUrlRespHtml_multiTry(queryUrl, postDict: postDict);

            //<h2>Alexa Rank of <b>ANSWERS.YAHOO.COM</b> is : <b>4</b></h2>
            alexaRankStr = "";
            if(ExtractSingleStr(@"<h2>Alexa Rank of.+?is.+?(\d+).+?</h2>", respHtml, out alexaRankStr))
            {
                //alexaRank = Int32.Parse(alexaRankStr);
                if(Int32.TryParse(alexaRankStr, out alexaRank))
                {
                    prevMethodFail = false;
                }
                else
                {
                    prevMethodFail = true;
                }

                prevMethodFail = false;
            }
            else
            {
                prevMethodFail = true;
            }
        }

        //TODO:
        //maybe future can use:
        //http://www.dakola.com/tools/alexa/

        return alexaRank;
    }

    #endregion

    #region 文件与文件夹

    /*********************************************************************/
    /* File/Folder */
    /*********************************************************************/

    /// <summary>
    /// 打开显示文件夹浏览器,并获取保存目录,取消则返回Empty
    /// </summary>
    /// <param name="fbdSave">文件夹浏览器</param>
    /// <returns></returns>
    public string GetSaveFolder(FolderBrowserDialog fbdSave)
    {
        string saveFolderPath = "";
        //string saveFolderPath = System.Environment.CurrentDirectory;
        //fbdSaveFolder.SelectedPath = System.Environment.CurrentDirectory;
        DialogResult saveFolderResult = fbdSave.ShowDialog();
        if(saveFolderResult == DialogResult.OK)
        {
            saveFolderPath = fbdSave.SelectedPath;
        }
        else if(saveFolderResult == DialogResult.Cancel)
        {
            saveFolderPath = "";
        }

        return saveFolderPath;
    }

    //save binary bytes into file
    /// <summary>
    /// 将字节流保存为二进制文件.
    /// </summary>
    /// <param name="fileToSave">文件路径</param>
    /// <param name="bytes">字节流</param>
    /// <param name="dataLen">字节长度.</param>
    /// <param name="errStr">错误信息.</param>
    /// <returns>保存成功返回true,失败返回false</returns>
    public static bool SaveBytesToFile(string fileToSave, ref Byte[] bytes, int dataLen, out string errStr)
    {
        bool saveOk = false;
        errStr = "未知错误！";

        try
        {
            int bufStartPos = 0;
            int bytesToWrite = dataLen;

            FileStream fs = File.Create(fileToSave, bytesToWrite);
            fs.Write(bytes, bufStartPos, bytesToWrite);
            fs.Close();

            saveOk = true;
        }
        catch(Exception ex)
        {
            errStr = ex.Message;
        }

        return saveOk;
    }

    //download file from url
    //makesure destination folder exist before call this function
    //input para example:
    //http://g-ecx.images-amazon.com/images/G/01/kindle/dp/2012/KC/KC-slate-01-lg._V401028090_.jpg
    //download\B007OZNZG0\KC-slate-01-lg._V401028090_.jpg
   
    /// <summary>
    /// 根据指定的Url 下载文件
    /// </summary>
    /// <param name="fileUrl">文件的Url</param>
    /// <param name="fullnameToStore">要保存的文件路径.</param>
    /// <param name="errStr">错误信息.</param>
    /// <param name="funcUpdateProgress">文件下载进度委托.</param>
    /// <returns>下载成功返回true,失败返回false</returns>
    public bool DownloadFile(string fileUrl, string fullnameToStore, out string errStr, Action<int> funcUpdateProgress)
    {
        bool downloadOk = false;
        errStr = "未知错误！";

        if(string.IsNullOrEmpty(fileUrl))
        {
            errStr = "URL地址为空！";
            return false;
        }

        if(string.IsNullOrEmpty(fullnameToStore))
        {
            errStr = "文件保存路径为空！";
            return false;
        }

        //const int maxFileLen = 100 * 1024 * 1024; // 100M
        const int maxFileLen = 300*1024*1024; // 300M
        const int lessMaxFileLen = 100*1024*1024; // 100M
        Byte[] binDataBuf;
        try
        {
            binDataBuf = new Byte[maxFileLen];
        }
        catch(Exception ex)
        {
            //if no enough memory, then try alloc less
            binDataBuf = new Byte[lessMaxFileLen];
        }

        int respDataLen = GetUrlRespStreamBytes(ref binDataBuf, fileUrl, null, null, 0, funcUpdateProgress);
        if(respDataLen < 0)
        {
            errStr = "无法下载文件数据！";
            return false;
        }

        if(SaveBytesToFile(fullnameToStore, ref binDataBuf, respDataLen, out errStr))
        {
            downloadOk = true;
        }

        return downloadOk;
    }

    ////*** need in furure, do full test to check following WebClient DownloadFile/DownloadFileAsync is ok or not ***
    //string gLastErrStr = "";
    ////private Action<int> gFuncUpdateProgress = null;
    //private void _downloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    //{
    //    if (e.Error != null)
    //    {
    //        gLastErrStr = e.Error.Message;
    //    }
    //}

    //private void _downloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    //{
    //    if (gFuncUpdateProgress != null)
    //    {
    //        //e.BytesReceived
    //        //e.TotalBytesToReceive
    //        gFuncUpdateProgress(e.ProgressPercentage);
    //    }
    //}

    //public bool downloadFile(string fileUrl, string fullnameToStore, out string errStr, Action<int> funcUpdateProgress)
    //{
    //    bool downloadOk = false;
    //    errStr = "未知错误！";

    //    WebClient wcDownloader = new WebClient();
    //    Uri fileUri = new Uri(fileUrl);
    //    wcDownloader.DownloadProgressChanged += new DownloadProgressChangedEventHandler(_downloadProgressChanged);
    //    wcDownloader.DownloadFileCompleted += new AsyncCompletedEventHandler(_downloadFileCompleted);
    //    wcDownloader.DownloadFileAsync(fileUri, fullnameToStore);

    //    gFuncUpdateProgress = funcUpdateProgress;

    //    if (!string.IsNullOrEmpty(gLastErrStr))
    //    {
    //        errStr = gLastErrStr;
    //        downloadOk = false;
    //    }

    //    return downloadOk;
    //}

    //open folder and select file
    /// <summary>
    /// 打开文件所在文件夹,并选中文件.
    /// </summary>
    /// <param name="fullFilename"></param>
    public void OpenFolderAndSelectFile(string fullFilename)
    {
        Process.Start("Explorer.exe", "/select," + fullFilename);
    }

    //open file/url/...
    /// <summary>
    /// 打开指定的文件.(系统调用默认程序打开文件)
    /// </summary>
    /// <param name="fullFilename"></param>
    public void OpenFileDirectly(string fullFilename)
    {
        Process.Start(fullFilename);
    }

    #endregion

    #region Screen

    /*********************************************************************/
    /* Screen */
    /*********************************************************************/

    // get current taskbar size(width, height), support 4 mode: taskbar bottom/right/up/left
    /// <summary>
    /// 获得当前任务栏的尺寸大小.
    /// </summary>
    /// <returns></returns>
    public Size GetCurTaskbarSize()
    {
        int width = 0, height = 0;

        if((Screen.PrimaryScreen.Bounds.Width == Screen.PrimaryScreen.WorkingArea.Width) && (Screen.PrimaryScreen.WorkingArea.Y == 0))
        {
            //taskbar bottom
            width = Screen.PrimaryScreen.WorkingArea.Width;
            height = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        }
        else if((Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.WorkingArea.Height) && (Screen.PrimaryScreen.WorkingArea.X == 0))
        {
            //taskbar right
            width = Screen.PrimaryScreen.Bounds.Width - Screen.PrimaryScreen.WorkingArea.Width;
            height = Screen.PrimaryScreen.WorkingArea.Height;
        }
        else if((Screen.PrimaryScreen.Bounds.Width == Screen.PrimaryScreen.WorkingArea.Width) && (Screen.PrimaryScreen.WorkingArea.Y > 0))
        {
            //taskbar up
            width = Screen.PrimaryScreen.WorkingArea.Width;
            //height = Screen.PrimaryScreen.WorkingArea.Y;
            height = Screen.PrimaryScreen.Bounds.Height - Screen.PrimaryScreen.WorkingArea.Height;
        }
        else if((Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.WorkingArea.Height) && (Screen.PrimaryScreen.WorkingArea.X > 0))
        {
            //taskbar left
            width = Screen.PrimaryScreen.Bounds.Width - Screen.PrimaryScreen.WorkingArea.Width;
            height = Screen.PrimaryScreen.WorkingArea.Height;
        }

        return new Size(width, height);
    }

    // get current taskbar position(X, Y), support 4 mode: taskbar bottom/right/up/left
    /// <summary>
    /// 获取当前任务栏的坐标位置.
    /// </summary>
    /// <returns></returns>
    public Point GetCurTaskbarLocation()
    {
        int xPos = 0, yPos = 0;

        if((Screen.PrimaryScreen.Bounds.Width == Screen.PrimaryScreen.WorkingArea.Width) && (Screen.PrimaryScreen.WorkingArea.Y == 0))
        {
            //taskbar bottom
            xPos = 0;
            yPos = Screen.PrimaryScreen.WorkingArea.Height;
        }
        else if((Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.WorkingArea.Height) && (Screen.PrimaryScreen.WorkingArea.X == 0))
        {
            //taskbar right
            xPos = Screen.PrimaryScreen.WorkingArea.Width;
            yPos = 0;
        }
        else if((Screen.PrimaryScreen.Bounds.Width == Screen.PrimaryScreen.WorkingArea.Width) && (Screen.PrimaryScreen.WorkingArea.Y > 0))
        {
            //taskbar up
            xPos = 0;
            yPos = 0;
        }
        else if((Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.WorkingArea.Height) && (Screen.PrimaryScreen.WorkingArea.X > 0))
        {
            //taskbar left
            xPos = 0;
            yPos = 0;
        }

        return new Point(xPos, yPos);
    }

    // get current right bottom corner position(X, Y), support 4 mode: taskbar bottom/right/up/left
    /// <summary>
    /// 获取当前屏幕的角落的坐标位置.
    /// </summary>
    /// <param name="windowSize"></param>
    /// <returns></returns>
    public Point GetCornerLocation(Size windowSize)
    {
        int xPos = 0, yPos = 0;

        if((Screen.PrimaryScreen.Bounds.Width == Screen.PrimaryScreen.WorkingArea.Width) && (Screen.PrimaryScreen.WorkingArea.Y == 0))
        {
            //taskbar bottom
            xPos = Screen.PrimaryScreen.WorkingArea.Width - windowSize.Width;
            yPos = Screen.PrimaryScreen.WorkingArea.Height - windowSize.Height;
        }
        else if((Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.WorkingArea.Height) && (Screen.PrimaryScreen.WorkingArea.X == 0))
        {
            //taskbar right
            xPos = Screen.PrimaryScreen.WorkingArea.Width - windowSize.Width;
            yPos = Screen.PrimaryScreen.WorkingArea.Height - windowSize.Height;
        }
        else if((Screen.PrimaryScreen.Bounds.Width == Screen.PrimaryScreen.WorkingArea.Width) && (Screen.PrimaryScreen.WorkingArea.Y > 0))
        {
            //taskbar up
            xPos = Screen.PrimaryScreen.WorkingArea.Width - windowSize.Width;
            yPos = Screen.PrimaryScreen.WorkingArea.Y;
        }
        else if((Screen.PrimaryScreen.Bounds.Height == Screen.PrimaryScreen.WorkingArea.Height) && (Screen.PrimaryScreen.WorkingArea.X > 0))
        {
            //taskbar left
            xPos = Screen.PrimaryScreen.WorkingArea.X;
            yPos = Screen.PrimaryScreen.WorkingArea.Height - windowSize.Height;
        }

        return new Point(xPos, yPos);
    }

    /*********************************************************************/
    /* Runtime */
    /*********************************************************************/
    /// <summary>
    /// 获取当前软件的版本
    /// </summary>
    /// <returns></returns>
    public string GetCurVerStr()
    {
        string curVerStr = "";
        Assembly asm = Assembly.GetExecutingAssembly();
        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(asm.Location);
        curVerStr = String.Format("{0}.{1}", fvi.ProductMajorPart, fvi.ProductMinorPart);
        return curVerStr;
    }

    #endregion

    #region Html Parse

    /*********************************************************************/
    /* HTML Parse */
    /*********************************************************************/

#if USE_HTML_PARSER_SGML
    //convert html to XML document
    public XmlDocument htmlToXmlDoc(string html)
    {
        // setup SgmlReader
        SgmlReader sgmlReader = new SgmlReader();
        sgmlReader.DocType = "HTML";
        sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
        sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;

        string decodedHtml = HttpUtility.HtmlDecode(html);
        sgmlReader.InputStream = new StringReader(decodedHtml);

        // create document
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.PreserveWhitespace = true;
        xmlDoc.XmlResolver = null;
        xmlDoc.Load(sgmlReader);

        return xmlDoc;
    }
    #endif

#if USE_HTML_PARSER_HTMLAGILITYPACK
    public HtmlAgilityPack.HtmlDocument htmlToHtmlDoc(string html)
    {
        HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

        //http://www.crifan.com/htmlagilitypack_html_tag_form_option_no_child_via_sibling_get_innertext/
        //make some html tag: form/option, has child
        HtmlNode.ElementsFlags.Remove("form");
        HtmlNode.ElementsFlags.Remove("option");

        htmlDoc.LoadHtml(html);

        return htmlDoc;
    }

    //remove sub node from current html node
    //eg: 
    //"script"
    //for
    //<script type="text/javascript"> 
    public HtmlNode removeSubHtmlNode(HtmlNode curHtmlNode, string subNodeToRemove)
    {
        HtmlNode afterRemoved = curHtmlNode;
        
        ////method 1: fail
        ////foreach (var subNode in afterRemoved.Descendants(subNodeToRemove))
        //foreach (HtmlNode subNode in afterRemoved.Descendants(subNodeToRemove))
        //{
        //    //An unhandled exception of type 'System.InvalidOperationException' occurred in mscorlib.dll
        //    //Additional information: Collection was modified; enumeration operation may not execute.
            
        //    //afterRemoved.RemoveChild(subNode);
        //    //curHtmlNode.RemoveChild(subNode);
        //    subNode.Remove();
        //}

        //method 2: OK
        HtmlNodeCollection foundAllSub = curHtmlNode.SelectNodes(subNodeToRemove);
        if ((foundAllSub != null) && (foundAllSub.Count > 0))
        {
            foreach (HtmlNode subNode in foundAllSub)
            {
                curHtmlNode.RemoveChild(subNode);
            }
        }

        return afterRemoved;
    }


    /*
     * [Function]
     * remove html tag, retain html content
     * [Input]
     * html, with tag
     * 
     * [Output]
     * pure content, no html tag
     * 
     * [Note]
     */
    public string htmlRemoveTag(string html)
    {
        string filteredHtml = "";

        if (!string.IsNullOrEmpty(html))
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (htmlDoc == null)
            {
                return "";
            }

            // 1. remove all comments
            //(1)get all comment nodes using XPATH
            HtmlNodeCollection commentNodeList = htmlDoc.DocumentNode.SelectNodes("//comment()");
            if (commentNodeList != null)
            {
                foreach (HtmlNode comment in commentNodeList)
                {
                    //(2) remove comment node itself
                    comment.ParentNode.RemoveChild(comment);
                }
            }

            //2. get all content
            foreach (var node in htmlDoc.DocumentNode.ChildNodes)
            {
                filteredHtml += node.InnerText;
            }
        }

        return filteredHtml;
    }

    #endif

    //example code for html parse
    /// <summary>
    /// 
    /// </summary>
    private void _demoHtmlParse()
    {
#if USE_HTML_PARSER_SGML
    //Method 1: use  htmlToXmlDoc
    //(1) with xmlns
        string withXmlnsUrl = "http://fiverr.com/gigs/search?utf8=%E2%9C%93&query=seo&x=15&y=13&page=2";
        string withXmlnsHtml = getUrlRespHtml(withXmlnsUrl);
        XmlDocument xmlDocWithNs = htmlToXmlDoc(withXmlnsHtml);
        //<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
        //<html xmlns:og="http://ogp.me/ns#" xmlns:fb="http://www.facebook.com/2008/fbml" xmlns="http://www.w3.org/1999/xhtml" lang="en" xml:lang="en" >
        //  <head>
        //      ...
        XmlNamespaceManager m = new XmlNamespaceManager(xmlDocWithNs.NameTable);
        m.AddNamespace("w3org", "http://www.w3.org/1999/xhtml");
        XmlNode titleNode = xmlDocWithNs.SelectSingleNode("//w3org:h1[@itemprop='name']", m);
        string title = titleNode.InnerText;

        //(2) without xmlns
        string withoutXmlnsUrl = "http://www.amazon.com/gp/new-releases/appliances/ref=zg_bsnr_nav_0";
        //<!DOCTYPE html>
        //<html>
        //<head>
        //...
        string withoutXmlnsHtml = getUrlRespHtml(withoutXmlnsUrl);
        XmlDocument xmlDocNoNs = htmlToXmlDoc(withoutXmlnsHtml);
        XmlNodeList pageNodeList = xmlDocNoNs.SelectNodes("//ol[@class='zg_pagination']/li[@class]");
        #endif

        //common part
        //how to use Attributes
        //XmlNodeList pageNodeList = xmlDoc.SelectNodes("//ol[@class='zg_pagination']/li[@class]");
        //if (pageNodeList != null)
        //{
        //    for (int pageIdx = 1; pageIdx < pageNodeList.Count; pageIdx++)
        //    {
        //        XmlNode curPageNode = pageNodeList[pageIdx];
        //        //<li class="zg_page " id="zg_page2"><a page="2" ajaxUrl="http://www.amazon.com/gp/new-releases/appliances/ref=zg_bsnr_appliances_pg_2/191-0874592-3518518?ie=UTF8&pg=2&ajax=1" href="http://www.amazon.com/gp/new-releases/appliances/ref=zg_bsnr_appliances_pg_2/191-0874592-3518518?ie=UTF8&pg=2">21-40</a></li>
        //        XmlNode ajaxUrlNode = curPageNode.SelectSingleNode(".//a[@href]");
        //        string pageUrl = ajaxUrlNode.Attributes["href"].Value;
        //    }
        //}


#if USE_HTML_PARSER_HTMLAGILITYPACK
    //Method 2: use htmlToHtmlDoc
        string testUrlWithXmlns = "http://sd.csdn.net/";
        string respHtml = getUrlRespHtml(testUrlWithXmlns);

        //<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
        //<html xmlns="http://www.w3.org/1999/xhtml">
        //<head>
        HtmlAgilityPack.HtmlDocument htmlDoc = htmlToHtmlDoc(respHtml);
        
        //<div class="tabcontent" id="sc1">
        //    <ul>
        //    <li><a href="http://www.csdn.net/article/tag/%E4%BA%A7%E5%93%81" target="_blank">产品</a></li>
        //    ......
        //    <li><a href="http://www.csdn.net/article/tag/%E8%AE%BE%E8%AE%A1" target="_blank">设计</a></li>
        //                        </ul>
        //</div>
        //...
        //<div class="tabcontent" id="sc4">
        //    <ul>
        //          ...
        //    <li><a href="http://www.csdn.net/article/tag/%E6%95%B0%E6%8D%AE%E5%BA%93"  target="_blank">数据库</a></li>
        //                        </ul>
        //</div>
        
        //here, no need to take care the html xmlns
        //is better than SGMLReader
        HtmlNode rootHtmlNode = htmlDoc.DocumentNode;
        HtmlNodeCollection htmlNodes = rootHtmlNode.SelectNodes("//div[@class='tabcontent']");
        foreach (HtmlNode link in htmlNodes)
        {
            HtmlAttribute att = link.Attributes["id"];
            string idHref = att.Value;
        }
#endif
    }


#if USE_EMBED_DLL_TO_EXE
    /*********************************************************************/
    /* Embedded dll into exe related code */
    /*********************************************************************/

    public yourClassname()
    {
        //!!! for load embedded dll: (1) register resovle handler
        AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

        InitializeComponent();

        ...
    }

    //!!! for load embedded dll: (2) implement this handler
    System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        string dllName = args.Name.Contains(",") ? args.Name.Substring(0, args.Name.IndexOf(',')) : args.Name.Replace(".dll", "");

        dllName = dllName.Replace(".", "_");

        if (dllName.EndsWith("_resources")) return null;

        System.Resources.ResourceManager rm = new System.Resources.ResourceManager(GetType().Namespace + ".Properties.Resources", System.Reflection.Assembly.GetExecutingAssembly());

        byte[] bytes = (byte[])rm.GetObject(dllName);

        return System.Reflection.Assembly.Load(bytes);
    }
#endif

#if USE_DATAGRIDVIEW
    /*********************************************************************/
    /* DataGridView */
    /*********************************************************************/

    public void dgvClearContent(DataGridView dgvValue)
    {
        dgvValue.Rows.Clear();
    }

    //draw the row index
    public void dgvDrawHeaderNum(DataGridView dgvValue)
    {
        for (int index = 0; (index <= (dgvValue.Rows.Count - 1)); index++)
        {
            int number = index + 1;
            dgvValue.Rows[index].HeaderCell.Value = String.Format("{0}", number);
        }
    }
    
    //release object
    public void releaseObject(object obj)
    {
        try
        {
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
            obj = null;
        }
        catch (Exception ex)
        {
            obj = null;
            //MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
        }
        finally
        {
            GC.Collect();
        }
    }
    
    public void dgvExportToExcel(  DataGridView dgvValue,
                                            string excelFullFilename,
                                            bool isAutoFit = true,
                                            bool isHeaderBold = true,
                                            List<int> omitRowIdxList = null,
                                            List<int> omitColumnIdxList = null,
                                            List<int> useTagColumnIdxList = null)
    {
        Excel.Application xlApp = new Excel.Application();
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
                
        object misValue = System.Reflection.Missing.Value;
        xlApp = new Excel.ApplicationClass();
        xlWorkBook = xlApp.Workbooks.Add(misValue);
        xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

        int rowIdx = 0, realRowIdx = 0;
        int columnIdx = 0, realColumnIdx = 0;
        const int excelRowHeader = 1;
        const int excelColumnHeader = 1;

        //save header
        for (columnIdx = 0, realColumnIdx = 0; columnIdx <= dgvValue.ColumnCount - 1; columnIdx++)
        {
            
            if ((omitColumnIdxList != null) && omitColumnIdxList.Contains(columnIdx))
            {
                //omit this column
            }
            else
            {
                //excelRowHeader and excelColumnHeader -> jump over the excel buildin row and column
                xlWorkSheet.Cells[0 + excelRowHeader, realColumnIdx + excelColumnHeader] = dgvValue.Columns[columnIdx].HeaderText;

                realColumnIdx++;
            }
        }
        
        const int excelTitleRow = 1;
        //save cells
        for (rowIdx = 0, realRowIdx= 0; rowIdx <= dgvValue.RowCount - 1; rowIdx++)
        {
            if ((omitRowIdxList != null) && omitRowIdxList.Contains(rowIdx))
            {
                //omit this row
            }
            else
            {
                for (columnIdx = 0, realColumnIdx = 0; columnIdx <= dgvValue.ColumnCount - 1; columnIdx++)
                {
                    if ((omitColumnIdxList != null) && omitColumnIdxList.Contains(columnIdx))
                    {
                        //omit this column
                    }
                    else
                    {
                        //note here use [columnIdx, rowIdx], not [rowIdx, columnIdx]
                        DataGridViewCell curCell = dgvValue[columnIdx, rowIdx];
                        if ((useTagColumnIdxList != null) && useTagColumnIdxList.Contains(columnIdx))
                        {
                            xlWorkSheet.Cells[(realRowIdx + excelTitleRow) + excelRowHeader, realColumnIdx + excelColumnHeader] = curCell.Tag;
                        }
                        else
                        {
                            xlWorkSheet.Cells[(realRowIdx + excelTitleRow) + excelRowHeader, realColumnIdx + excelColumnHeader] = curCell.Value;
                        }

                        realColumnIdx++;
                    }
                }

                realRowIdx++;
            }
        }

        //formatting
        //(1) header to bold
        if (isHeaderBold)
        {
            Range headerRow = xlWorkSheet.get_Range("1:1", System.Type.Missing);
            headerRow.Font.Bold = true;
        }
        //(2) auto adjust column width (according to content)
        if (isAutoFit)
        {
            Range allColumn = xlWorkSheet.Columns;
            allColumn.AutoFit();
        }

        //output
        xlWorkBook.SaveAs(  excelFullFilename,
                            XlFileFormat.xlWorkbookNormal,
                            misValue,
                            misValue, 
                            misValue, 
                            misValue, 
                            XlSaveAsAccessMode.xlExclusive,
                            XlSaveConflictResolution.xlLocalSessionChanges,
                            misValue, 
                            misValue, 
                            misValue, 
                            misValue);
        xlWorkBook.Close(true, misValue, misValue);
        xlApp.Quit();

        releaseObject(xlWorkSheet);
        releaseObject(xlWorkBook);
        releaseObject(xlApp);
    }

    public void dgvExportToCsv(DataGridView dgvValue,
                                        string csvFullFilename,
                                        string delimiter = ",",
                                        List<int> omitRowIdxList = null,
                                        List<int> omitColumnIdxList = null,
                                        List<int> useTagColumnIdxList = null)
    {
        StreamWriter csvStreamWriter = new StreamWriter(csvFullFilename, false, System.Text.Encoding.UTF8);

        int rowIdx = 0, realRowIdx = 0;
        int columnIdx = 0, realColumnIdx = 0;

        //output header data
        string headerRowStr = "";
        for (columnIdx = 0, realColumnIdx = 0; columnIdx <= dgvValue.ColumnCount - 1; columnIdx++)
        {
            if ((omitColumnIdxList != null) && omitColumnIdxList.Contains(columnIdx))
            {
                //omit this column
            }
            else
            {
                headerRowStr += dgvValue.Columns[columnIdx].HeaderText + delimiter;

                realColumnIdx++;
            }
        }
        csvStreamWriter.WriteLine(headerRowStr);

        //output rows data
        for (rowIdx = 0, realRowIdx = 0; rowIdx <= dgvValue.RowCount - 1; rowIdx++)
        {
            if ((omitRowIdxList != null) && omitRowIdxList.Contains(rowIdx))
            {
                //omit this row
            }
            else
            {
                string eachRowStr = "";
                for (columnIdx = 0, realColumnIdx = 0; columnIdx <= dgvValue.ColumnCount - 1; columnIdx++)
                {
                    if ((omitColumnIdxList != null) && omitColumnIdxList.Contains(columnIdx))
                    {
                        //omit this column
                    }
                    else
                    {
                        DataGridViewCell curCell = dgvValue[columnIdx, rowIdx];//dgvValue.Rows[rowIdx].Cells[columnIdx]
                        if ((useTagColumnIdxList != null) && useTagColumnIdxList.Contains(columnIdx))
                        {
                            eachRowStr += curCell.Tag + delimiter;
                        }
                        else
                        {
                            eachRowStr += curCell.Value + delimiter;
                        }
                        
                        realColumnIdx++;
                    }
                }
                csvStreamWriter.WriteLine(eachRowStr);

                realRowIdx++;
            }
        }

        csvStreamWriter.Close();        
    }
#endif

    #endregion

    #region Json

    /*********************************************************************/
    /* JSON */
    /*********************************************************************/
#if USE_JSON
    /*
     * [Function]
     * convert json string into dictionary object
     * [Input]
     * json string
     * [Output]
     * object, internally is dictionary
     * [Note]
     * 1.you should know the internal structure of the dictionary
     * then converted to specific type of yours
     */
    public Object jsonToDict(string jsonStr)
    {
        JavaScriptSerializer jsonSerializer = new JavaScriptSerializer() { MaxJsonLength = int.MaxValue };
        Object dictObj = jsonSerializer.DeserializeObject(jsonStr);

        return dictObj;
    }
#endif

    #endregion

    #endregion
}

