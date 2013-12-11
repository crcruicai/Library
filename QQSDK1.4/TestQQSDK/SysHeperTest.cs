using QQSDK.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestQQSDK
{


    [TestClass()]
    public class SysHeperTest
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
        public void LoginStatusToStringTest()
        {
            LoginStatus status = new LoginStatus(); 
            string expected = string.Empty; 
            string actual;
            actual = SysHeper.LoginStatusToString(status);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SysHeperConstructorTest()
        {
            SysHeper target = new SysHeper();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
