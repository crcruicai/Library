using QQSDK.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using QQSDK.Json;

namespace TestQQSDK
{


    [TestClass()]
    public class QQMessageEventArgsTest
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
        public void ParseGroup()
        {
            string text = ReadTestFile.GetPollKickmessage();
            QQMessageEventArgs e = QQMessageEventArgs.Parse2(text);
            Assert.AreEqual(e.PollMsgType, PollType.Friend);
            Assert.AreEqual(e.Data.GetType(), typeof(FriendMessageData));
        }


        [TestMethod()]
        public void ParseMessage()
        {

            string text = ReadTestFile.GetPollKickmessage();
            QQMessageEventArgs e = QQMessageEventArgs.Parse2(text);
            Assert.AreEqual(e.PollMsgType, PollType.Friend);
            Assert.AreEqual(e.Data.GetType(), typeof(FriendMessageData));
        }


        [TestMethod()]
        public void ParseKickMessage()
        {
            string text = ReadTestFile.GetPollKickmessage();
            QQMessageEventArgs e = QQMessageEventArgs.Parse2(text);
            Assert.AreEqual(e.PollMsgType, PollType.KickMessage);
            Assert.AreEqual(e.Data.GetType(), typeof(KickMessage));

        }

        [TestMethod()]
        public void ParseTips()
        {
            string text = ReadTestFile.GetPollTips ();
            QQMessageEventArgs e = QQMessageEventArgs.Parse2(text);
            Assert.AreEqual(e.PollMsgType, PollType.Tips );
            Assert.AreEqual(e.Data.GetType(), typeof(FriendMessageData));

        }




        [TestMethod()]
        public void QQMessageEventArgsConstructorTest()
        {
            string myqq = string.Empty; 
            int recode = 0; 
            PollType poll = new PollType(); 
            MessageData data = null; 
            string msgtext = string.Empty; 
            QQMessageEventArgs target = new QQMessageEventArgs(myqq, recode, poll, data, msgtext);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void QQMessageEventArgsConstructorTest1()
        {
            string msgtext = string.Empty; 
            QQMessageEventArgs_Accessor target = new QQMessageEventArgs_Accessor(msgtext);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod()]
        [DeploymentItem("QQSDK.exe")]
        public void GetFriendMessageTest()
        {
            string[] array = null; 
            int index = 0; 
            FriendMessageData expected = null; 
            FriendMessageData actual;
            actual = QQMessageEventArgs_Accessor.GetFriendMessage(array, index);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
