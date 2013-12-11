using QQSDK.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Text;
using System.Drawing;

namespace TestQQSDK
{


    [TestClass()]
    public class HttpWebTest
    {
       

        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        [TestMethod()]
        public void UserAgentTest()
        {
            HttpWeb target = new HttpWeb(); 
            string expected = string.Empty; 
            string actual;
            target.UserAgent = expected;
            actual = target.UserAgent;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
            
        }

        [TestMethod()]
        public void RefererTest()
        {
            HttpWeb target = new HttpWeb(); 
            string expected = string.Empty; 
            string actual;
            target.Referer = expected;
            actual = target.Referer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void HeadersTest()
        {
            HttpWeb target = new HttpWeb(); 
            WebHeaderCollection actual;
            actual = target.Headers;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CookieTest()
        {
            HttpWeb target = new HttpWeb(); 
            CookieContainer expected = null; 
            CookieContainer actual;
            target.Cookie = expected;
            actual = target.Cookie;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SendToTextTest()
        {
            HttpWeb target = new HttpWeb(); 
            string url = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = target.SendToText(url);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SendGetAsyncTest()
        {
            HttpWeb target = new HttpWeb(); 
            string url = string.Empty; 
            target.SendGetAsync(url);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void SendAsynToTextTest()
        {
            HttpWeb target = new HttpWeb(); 
            HttpWebRequest request = null; 
            ReciveData recive = null; 
            target.SendAsynToText(request, recive);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void SendAsynToTextTest1()
        {
            HttpWeb target = new HttpWeb(); 
            string url = string.Empty; 
            ReciveData recive = null; 
            target.SendAsynToText(url, recive);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void SendAsynToStreamTest()
        {
            HttpWeb target = new HttpWeb(); 
            HttpWebRequest request = null; 
            ReciveData recive = null; 
            target.SendAsynToStream(request, recive);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void RespCallbackTest()
        {
            HttpWeb target = new HttpWeb(); 
            IAsyncResult ar = null; 
            target.RespCallback(ar);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void RequestCallbackTest()
        {
            HttpWeb_Accessor target = new HttpWeb_Accessor(); 
            IAsyncResult ar = null; 
            target.RequestCallback(ar);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void ReadTextCallbackTest()
        {
            HttpWeb_Accessor target = new HttpWeb_Accessor(); 
            IAsyncResult ar = null; 
            target.ReadTextCallback(ar);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void ReadCallBackTest()
        {
            HttpWeb target = new HttpWeb(); 
            IAsyncResult asyncResult = null; 
            target.ReadCallBack(asyncResult);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void PostWebRequestTest()
        {
            HttpWeb target = new HttpWeb(); 
            string postUrl = string.Empty; 
            string paramData = string.Empty; 
            Encoding dataEncode = null; 
            string expected = string.Empty; 
            string actual;
            actual = target.PostWebRequest(postUrl, paramData, dataEncode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void PollWebRequestTest()
        {
            HttpWeb target = new HttpWeb(); 
            string postUrl = string.Empty; 
            string paramData = string.Empty; 
            Encoding dataEncode = null; 
            string expected = string.Empty; 
            string actual;
            actual = target.PollWebRequest(postUrl, paramData, dataEncode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void OnReciveMessageTest()
        {
            HttpWeb_Accessor target = new HttpWeb_Accessor(); 
            HttpWebEventArgs e = null; 
            target.OnReciveMessage(e);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void GetImageTest()
        {
            HttpWeb target = new HttpWeb(); 
            HttpWebRequest request = null; 
            Image expected = null; 
            Image actual;
            actual = target.GetImage(request);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetCookieTextTest()
        {
            HttpWeb target = new HttpWeb(); 
            string url = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = target.GetCookieText(url);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CreateHttpWebRequestTest()
        {
            HttpWeb target = new HttpWeb(); 
            string url = string.Empty; 
            HttpWebRequest expected = null; 
            HttpWebRequest actual;
            actual = target.CreateHttpWebRequest(url);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void HttpWebConstructorTest()
        {
            HttpWeb target = new HttpWeb();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
