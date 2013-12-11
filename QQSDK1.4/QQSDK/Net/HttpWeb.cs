using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;
using QQSDK.Systems;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
namespace QQSDK.Net
{

    /// <summary>
    /// 异步Http请求操作.
    /// </summary>
    public class HttpWeb
    {

        #region 事件与委托

        /// <summary>
        /// 当请求
        /// </summary>
        public event EventHandler<HttpWebEventArgs> ReciveMessage;

        protected void OnReciveMessage(HttpWebEventArgs e)
        {
            if (ReciveMessage != null)
            {
                ReciveMessage(this, e);
            }
        }

        #endregion

        /// <summary>
        /// 允许连接的并发数.
        /// </summary>
        private static bool _OpenLimit = false;

        

        #region  构造函数
        public HttpWeb()
        {
            _Cookie = new CookieContainer();
            if (!_OpenLimit)
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 1000;
                _OpenLimit = true;
            }

        }

        #endregion

        #region 属性
        private CookieContainer _Cookie;
        /// <summary>
        /// 提供Cookie的容器.
        /// </summary>	
        public CookieContainer Cookie
        {
            get { return _Cookie; }
            set { _Cookie = value; }
        }

        private string _Referer;
        /// <summary>
        /// 
        /// </summary>	
        public string Referer
        {
            get { return _Referer; }
            set { _Referer = value; }
        }

        private string  _UserAgent="";
        /// <summary>
        /// 
        /// </summary>	
        public string  UserAgent
        {
            get { return _UserAgent; }
            set { _UserAgent = value; }
        }

        private WebHeaderCollection _Headers;
        /// <summary>
        /// 
        /// </summary>
        public WebHeaderCollection Headers
        {
            get  
            {
                return _Headers;
            }
        }
        /// <summary>
        /// 上次访问的URL.
        /// </summary>
        private string _URL;

        #endregion


        #region  公共函数
        /// <summary>
        /// 获取Cookie的文本.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetCookieText(string url =null)
        {
            StringBuilder text = new StringBuilder();
            if(string.IsNullOrEmpty (url))
            {
                //TODO:这里设置上次访问的URL.
                url = _URL;
            }
            
            foreach (Cookie item in this.Cookie .GetCookies (new Uri (url)))
            {
                text.Append(item.ToString());
                text.AppendLine();
            }
	        
            return text.ToString();
        }

       

        /// <summary>
        /// 获取指定的图片.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Image GetImage(HttpWebRequest request)
        {
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
              
                _Cookie.Add(response.Cookies);
                Stream rs = response.GetResponseStream();
                return Image.FromStream(rs);

            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                //throw;
            }
            return null;
          
        }


        /// <summary>
        /// 创建一个HttpWebRequest
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public HttpWebRequest CreateHttpWebRequest(string url)
        {
            _URL = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 5.01; Windows NT 5.0)";
            //request.Accept = "*/*";
            //request.ContentType = "";
            //request.Method = "";
            request.Referer = _Referer;
            request.CookieContainer = _Cookie;
            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            return request;
        }

        
        /// <summary>
        /// 采用Post方式获取数据.
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public string PostWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                //Debug.WriteLine(GetCookieText());
                //paramData = Encode.ToUTF8(paramData);
                byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)CreateHttpWebRequest(postUrl);
                webReq.Method = "POST";
                webReq.Accept = "*/*";
                
                webReq.ContentType = "application/x-www-form-urlencoded; charSet=utf-8";
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                try
                {
                    HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    ret = sr.ReadToEnd();

                    this._Cookie.Add(response.Cookies);
                    this._Headers = response.Headers;

                    sr.Close();
                    webReq.Abort();
                    response.Close();
                }
                catch (WebException e)
                {
                    Loger.WriteLog(e);
                   
                }
                //Debug.WriteLine("POST COOKIE");
                //Debug.WriteLine(GetCookieText());
                newStream.Close();
            }
      
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                return ex.Message;
            }
            return ret;
        }
        /// <summary>
        /// 采用Post方式获取数据.
        /// </summary>
        /// <param name="postUrl"></param>
        /// <param name="paramData"></param>
        /// <param name="dataEncode"></param>
        /// <returns></returns>
        public string PollWebRequest(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            try
            {
                //Debug.WriteLine(GetCookieText());

                byte[] byteArray = Encoding.UTF8.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)CreateHttpWebRequest(postUrl);
                webReq.Method = "POST";
                webReq.Accept = "*/*";
                webReq.Timeout = int.MaxValue;
                webReq.ContentType = "application/x-www-form-urlencoded; charSet=utf-8";
                //webReq.ServicePoint.Expect100Continue = true;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                try
                {
                    HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    ret = sr.ReadToEnd();
                    this._Cookie.Add(response.Cookies);
                    this._Headers = response.Headers;
                    sr.Close();
                    webReq.Abort();
                    response.Close();
                }
                catch (WebException e)
                {
                    Loger.WriteLog(e);
                    return e.Message;
                }
                
                webReq.KeepAlive = true;
                //Debug.WriteLine("POST COOKIE");
                //Debug.WriteLine(GetCookieText());
                //newStream.Close();
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                return ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// 使用Get方式请求数据.
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string SendToText(string url)
        {
            //Debug.WriteLine("SendText");
            //Debug.WriteLine(GetCookieText());
            try
            {
                HttpWebRequest request = CreateHttpWebRequest(url);
                request.MaximumAutomaticRedirections = 5;
                request.AllowAutoRedirect = true;
                request.Timeout = 10000;
                request.Method = "Get";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "*/*";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //Thread.Sleep(100);
                Stream stream = response.GetResponseStream();

                StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                string text = streamReader.ReadToEnd();
                _Headers = response.Headers;
                _URL = response.ResponseUri.ToString();
                _Cookie.Add(response.Cookies);

                request.Abort();
                streamReader.Close();
                response.Close();

                return text;
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                return ex.Message;
            }
            
        }
        #endregion

        #region  异步
        public void SendAsynToText(string url, ReciveData recive)
        {
            SendAsynToText(CreateHttpWebRequest(url), recive);
        }

        public void SendAsynToText(HttpWebRequest request,ReciveData recive)
        {
            RequestData data = new RequestData(request, RequestDataType.Text ,recive);
            request.BeginGetRequestStream(new AsyncCallback(RequestCallback), data);
            
        }

        public void SendAsynToStream(HttpWebRequest request, ReciveData recive)
        {
            RequestData data = new RequestData(request, RequestDataType.Stream, recive);
            request.BeginGetRequestStream(new AsyncCallback(RequestCallback), data);

        }

        private void RequestCallback(IAsyncResult ar)
        {
            try
            {
                RequestData data = (RequestData)ar.AsyncState;

                HttpWebRequest req = data.Request;
                HttpWebResponse resp = (HttpWebResponse)req.EndGetResponse(ar);

                switch (data.DataType)
                {
                    case RequestDataType.Text:
                        Stream r = resp.GetResponseStream();
                        StreamReader streamReader = new StreamReader(r, Encoding.UTF8);
                        data.Text = streamReader.ReadToEnd();
                        streamReader.Close();
                        data.ReciveData(data);
                        break;
                    case RequestDataType.Stream:
                        data.Stream = resp.GetResponseStream();
                        data.ReciveData(data);
                        break;
                    default:
                        break;
                }
                resp.Close();
            }
            catch (Exception ex)
            {
                Loger.WriteLog(ex);
                throw;
            }
        


        }

        /// <summary>
        /// 读取文本回调函数.
        /// </summary>
        /// <param name="ar"></param>
        private void ReadTextCallback(IAsyncResult ar)
        {
            RequestData data = (RequestData)ar.AsyncState;
            Stream stream = data.Stream;
            int read = stream.EndRead(ar);
            if (read > 0)
            {
                //对字节进行解码.
                

               
                //继续进行读取.知道read<0为止.
                stream.BeginRead(data.BufferRead, 0, RequestData.BuffSize, new AsyncCallback(ReadTextCallback), data);
            }
            else
            {
                if (data.Text.Length > 1)
                {

                    //通知委托,处理数据.
                    data.ReciveData(data);
                }
                stream.Close();
            }
        }


        /// <summary>
        /// 发送异步请求 数据.
        /// </summary>
        /// <param name="url"></param>
        public void SendGetAsync(string url)
        {
            // 从命令行获取 URI
            Uri HttpSite = new Uri(url);

            // 创建请求对象
            HttpWebRequest wreq = WebRequest.Create(HttpSite) as HttpWebRequest;
            // 创建状态对象
            RequestState rs = new RequestState(wreq);

            IAsyncResult ar = wreq.BeginGetResponse(new AsyncCallback(RespCallback), rs);
        }

        public void RespCallback(IAsyncResult ar)
        {
            // 从异步结果获取 RequestState 对象
            RequestState rs = (RequestState)ar.AsyncState;

            // 从 RequestState 获取 HttpWebRequest
            HttpWebRequest req = rs.Request;

            // 调用 EndGetResponse 生成 HttpWebResponse 对象
            // 该对象来自上面发出的请求
            HttpWebResponse resp = (HttpWebResponse)req.EndGetResponse(ar);

            // 既然我们拥有了响应，就该从
            // 响应流开始读取数据了
            Stream ResponseStream = resp.GetResponseStream();

            // 该读取操作也使用异步完成，所以我们
            // 将要以 RequestState 存储流
            rs.ResponseStream = ResponseStream;
            Thread.Sleep(10000);
            // 请注意，rs.BufferRead 被传入到 BeginRead。
            // 这是数据将被读入的位置。
            IAsyncResult iarRead = ResponseStream.BeginRead(rs.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), rs);
        }


        /// <summary>
        /// 读取数据回调函数.
        /// </summary>
        /// <param name="asyncResult"></param>
        public void ReadCallBack(IAsyncResult asyncResult)
        {
            // 从 asyncresult 获取 RequestState 对象
            RequestState rs = (RequestState)asyncResult.AsyncState;

            // 取出在 RespCallback 中设置的 ResponseStream
            Stream responseStream = rs.ResponseStream;

            // 此时 rs.BufferRead 中应该有一些数据。
            // 读取操作将告诉我们那里是否有数据
            int read = responseStream.EndRead(asyncResult);

            if (read > 0)
            {
                // 准备 Char 数组缓冲区，用于向 Unicode 转换
                Char[] charBuffer = new Char[BUFFER_SIZE];

                // 将字节流转换为 Char 数组，然后转换为字符串
                // len 显示多少字符被转换为 Unicode
                int len = rs.StreamDecoder.GetChars(rs.BufferRead, 0, read, charBuffer, 0);
                String str = new String(charBuffer, 0, len);

                // 将最近读取的数据追加到 RequestData stringbuilder 对象中，
                // 该对象包含在 RequestState 中
                rs.RequestData.Append(str);


                // 现在发出另一个异步调用，读取更多的数据
                // 请注意，将不断调用此过程，直到
                // responseStream.EndRead 返回 -1
                IAsyncResult ar = responseStream.BeginRead(rs.BufferRead, 0, BUFFER_SIZE, new AsyncCallback(ReadCallBack), rs);
            }
            else
            {
                if (rs.RequestData.Length > 1)
                {
                    // 所有数据都已被读取，因此将其显示到控制台
                    string strContent;
                    strContent = rs.RequestData.ToString();
                    HttpWebEventArgs e = new HttpWebEventArgs(strContent);
                    OnReciveMessage(e);
                }

                // 关闭响应流
                responseStream.Close();

            }
            return;
        }
        #endregion 

        public static int BUFFER_SIZE = 1024;

    }


    /// <para>Http的请求状态.</para>
    /// Summary description for RequestState
    /// </summary>
    public class RequestState
    {
        const int BUFFER_SIZE = 1024;

        #region 属性

        private HttpWebRequest _Request;
        /// <summary>
        /// 
        /// </summary>
        public HttpWebRequest Request
        {
            get
            {
                return _Request;
            }
            set
            {
                _Request = value;
            }
        }

        private Stream _ResponseStream;
        /// <summary>
        /// 
        /// </summary>
        public Stream ResponseStream
        {
            get
            {
                return _ResponseStream;
            }
            set
            {
                _ResponseStream = value;
            }
        }

        private Decoder _StreamDecoder = Encoding.UTF8.GetDecoder();
        /// <summary>
        /// 适当编码类型的解码器
        /// </summary>
        public Decoder StreamDecoder
        {
            get
            {
                return _StreamDecoder;
            }
            set
            {
                _StreamDecoder = value;
            }
        }

        private StringBuilder _RequestData;
        public StringBuilder RequestData
        {
            get
            {
                return _RequestData;
            }
            set
            {
                _RequestData = value;
            }
        }

        private byte[] _BufferRead;
        /// <summary>
        /// 
        /// </summary>
        public byte[] BufferRead
        {
            get
            {
                return _BufferRead;
            }
            set
            {
                _BufferRead = value;
            }
        }


        #endregion


        public RequestState(HttpWebRequest request)
        {
            BufferRead = new byte[BUFFER_SIZE];
            RequestData = new StringBuilder("");
            ResponseStream = null;
            Request = request;
        }




        public RequestState()
            : this(null)
        {


        }
    }
}
