using QQSDK.Systems;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestQQSDK
{


    [TestClass()]
    public class LogerTest
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
        public void WriteLogTest()
        {
            string text = string.Empty; 
            Loger.WriteLog(text);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void LogerConstructorTest()
        {
            Loger target = new Loger();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod()]
        public void WriteLogTest1()
        {
            Exception e = null; 
            Loger.WriteLog(e);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }
    }
}
