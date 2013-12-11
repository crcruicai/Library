using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QQSDK.Net
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpWebEventArgs : EventArgs
    {
        private string _Message;
        /// <summary>
        /// 
        /// </summary>	
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public HttpWebEventArgs()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        public HttpWebEventArgs(string text)
        {
            _Message = text;
        }

    }

    

}
