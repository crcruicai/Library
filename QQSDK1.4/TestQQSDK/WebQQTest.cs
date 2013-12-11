using QQSDK.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;
using QQSDK.Json;

namespace TestQQSDK
{


    [TestClass()]
    public class WebQQTest
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
        public void MyQQNumberTest()
        {
            WebQQ target = new WebQQ(); 
            string actual;
            actual = target.MyQQNumber;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ClientIDTest()
        {
            WebQQ target = new WebQQ(); 
            string actual;
            actual = target.ClientID;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            WebQQ target = new WebQQ(); 
            string uin = string.Empty; 
            string content = string.Empty; 
            Font font = null; 
            Color color = new Color(); 
            bool expected = false; 
            bool actual;
            actual = target.SendMessage(uin, content, font, color);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ReLoginTest()
        {
            WebQQ target = new WebQQ(); 
            string qqnumber = string.Empty; 
            string pp = string.Empty; 
            LoginResult expected = new LoginResult(); 
            LoginResult actual;
            actual = target.ReLogin(qqnumber, pp);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void OnReciveMessageTest()
        {
            WebQQ_Accessor target = new WebQQ_Accessor(); 
            QQMessageEventArgs e = null; 
            target.OnReciveMessage(e);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void LoginTest()
        {
            WebQQ target = new WebQQ(); 
            string qqNumber = string.Empty; 
            string password = string.Empty; 
            string verifyCode = string.Empty; 
            LoginResult expected = new LoginResult(); 
            LoginResult actual;
            actual = target.Login(qqNumber, password, verifyCode);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ListenMessageTest()
        {
            WebQQ target = new WebQQ(); 
            
        }

        [TestMethod()]
        public void GetUserFriendsTest()
        {
            WebQQ target = new WebQQ(); 
            UserFriend expected = null; 
            UserFriend actual;
            actual = target.GetUserFriends();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetQQNumberTest()
        {
            WebQQ target = new WebQQ(); 
            string uin = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = target.GetQQNumber(uin);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetOnlineStatusTest()
        {
            //WebQQ target = new WebQQ(); 
            //string expected = string.Empty; 
            //string actual;
            //actual = target.GetOnlineStatus();
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetLoginVCImageTest()
        {
            WebQQ target = new WebQQ(); 
            string qqnumber = string.Empty; 
            Image expected = null; 
            Image actual;
            actual = target.GetLoginVCImage(qqnumber);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetLoginVCTest()
        {
            WebQQ target = new WebQQ(); 
            string qqNumber = string.Empty; 
            string result = string.Empty; 
            string resultExpected = string.Empty; 
            LoginResult expected = new LoginResult(); 
            LoginResult actual;
            actual = target.GetLoginVC(qqNumber, out result);
            Assert.AreEqual(resultExpected, result);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void GetLoginTokenTest()
        {
            WebQQ_Accessor target = new WebQQ_Accessor(); 
            string text = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.GetLoginToken(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");

            
            
        }


        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void GetKeyTest()
        {
            WebQQ_Accessor target = new WebQQ_Accessor(); 
            string expected = string.Empty; 
            string actual;
            actual = target.GetKey();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

       

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void GetDirecteUrlTest()
        {
            WebQQ_Accessor target = new WebQQ_Accessor(); 
            string text = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = target.GetDirectionUrl(text);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CheckLoginImageTest()
        {
            WebQQ target = new WebQQ(); 
            string qqnumber = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.CheckLoginImage(qqnumber);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void WebQQConstructorTest()
        {
            WebQQ target = new WebQQ();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
