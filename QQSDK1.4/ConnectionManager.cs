using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Lingchen.Common.Utils;

namespace Lingchen.Net.QQCore
{
    /// <summary>
    /// 连接管理类
    /// </summary>
   public class ConnectionManager
    {
       public bool IsConnected
       {
           get
           {
               if (this.m_udpCLient != null) return this.m_udpCLient.Connected;
               else return false;
           }
       }
       /// <summary>
       /// 接收缓冲区大小
       /// </summary>
       internal const int RECEIVE_SIZE = 1024 * 24;
       /// <summary>
       /// 发送超时设置
       /// </summary>
       internal const int SENDRECEIVE_TIMEOUT = 3;
       /// <summary>
       /// 服务器的IP终结点
       /// </summary>
       public IPEndPoint ServerPoint { get; set; }
       /// <summary>
       /// 客户端IP地址
       /// </summary>
       public IPAddress ClientIP { get; set; }
       /// <summary>
       /// 服务器时间
       /// </summary>
       public DateTime ServerTime { get; set; }
       /// <summary>
       /// 是否需要重定向服务器
       /// </summary>
       public bool NeedLocation { get; set; }
       /// <summary>
       /// Udp封装访问对象
       /// </summary>
       private Socket m_udpCLient;
       /// <summary>
       /// 所有事务管理都经过此对象
       /// </summary>
       private QQClient m_qqClient;
       /// <summary>
       /// 接收字节缓冲区
       /// </summary>
      private byte[] m_receiveBytes =new byte[RECEIVE_SIZE];  
       /// <summary>
       /// 管理事件
       /// </summary>
      private EventStrategy m_eventStrategy;
       public ConnectionManager(QQClient client,EventStrategy eventStrategy)
       {
           this.m_qqClient = client;
           this.m_eventStrategy = eventStrategy;
           this.NeedLocation = false;
           this.ServerPoint = new System.Net.IPEndPoint(IPAddress.Parse("112.95.242.111"), 8000);
       }
       /// <summary>
       /// 初始化UDP连接
       /// </summary>
        public void InitConnection()
       {
           if (this.m_udpCLient != null)
           {
               try
               {
                   this.m_udpCLient.Shutdown(SocketShutdown.Both);
               }
               catch
               {
               }
             
           } 
            this.m_udpCLient = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
           #region 绑定本地端口,这一步可有可无
           if (!this.m_udpCLient.IsBound)
           {
               int port = 4000;
               while(true)
               try
               {

                   this.m_udpCLient.Bind(new IPEndPoint(IPAddress.Any, port++));
                   break;
               }
               catch {  }
           }
           #endregion
           //this.m_udpCLient.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.ReceiveTimeout , SENDRECEIVE_TIMEOUT);
      }
       /// <summary>
       /// 启动连接
       /// </summary>
       /// <returns></returns>
        public void Start(bool needConnect)
        {
            if (needConnect)
            {
                try
                {
                    this.m_udpCLient.Connect(this.ServerPoint);
                }
                catch
                {
                }
            }
           if (!this.IsConnected) this.m_qqClient.EventStrategy.TriggerLog(this.m_qqClient, new SocketErrorEventArgs() { Message = "服务器连接失败，请检查网络连接是否正常", LogType = LogType.Exception });
           else
           {
               this.m_udpCLient.BeginReceive(this.m_receiveBytes, 0, this.m_receiveBytes.Length, SocketFlags.None, (p) =>
              {
                  int length = this.m_udpCLient.EndReceive(p);
                  if (length > 0)
                  {
                      //过滤
                      ByteBuffer buf = new ByteBuffer(this.m_receiveBytes, 0, length);
                      this.m_qqClient.PacketStategy.ParseInPacket(buf);
                  }
                  this.Start(false);
              }, null);
           }
        }
        public void SendAsync(Lingchen.Net.QQCore.Packets.OutPacket outPacket)
        {
            if (this.m_udpCLient.Connected)
            { 
                byte[] buffer=outPacket.ToArray();
#if DEBUG
                string l = Util.TileHexString(buffer);
#endif
                this.m_udpCLient.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, p =>
                {
                   int length= this.m_udpCLient.EndSend(p);
                   if (length > 0)
                   {

                   }

                }, null);
            }
        }



   
    }
}
