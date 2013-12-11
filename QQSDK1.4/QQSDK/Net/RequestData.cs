using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;

namespace QQSDK.Net
{
    /// <summary>
    /// 处理来自Http数据请求.
    /// </summary>
    /// <param name="data"></param>
    public delegate void ReciveData(RequestData data);



    /// <summary>
    /// Http请求接收到的数据.
    /// </summary>
    public class RequestData
    {
        public const int BuffSize = 1024;



        public RequestData(HttpWebRequest request, RequestDataType type, ReciveData re)
        {
            _Request = request;
            _DataType = type;
            _ReciveData = re;
            if (type == RequestDataType.Text)
            {
                _BufferRead = new byte[BuffSize];
            }
            else
            {
                _BufferRead = null;
            }
            _Stream = null;

        }



        private RequestDataType _DataType;
        /// <summary>
        /// 
        /// </summary>	
        public RequestDataType DataType
        {
            get { return _DataType; }

        }

        private ReciveData _ReciveData;
        /// <summary>
        /// 
        /// </summary>	
        public ReciveData ReciveData
        {
            get { return _ReciveData; }
            set { _ReciveData = value; }
        }


        private Stream _Stream;
        /// <summary>
        /// 
        /// </summary>	
        public Stream Stream
        {
            get { return _Stream; }
            set { _Stream = value; }
        }

        private string _Text = string.Empty;
        /// <summary>
        /// 
        /// </summary>	
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
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

    }


    /// <summary>
}
