using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 DateTimeUtilTest 的测试类，旨在
    ///包含所有 DateTimeUtilTest 单元测试
    ///</summary>
    [TestClass()]
    public class DateTimeUtilTest
    {


        private TestContext __TestContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return __TestContextInstance;
            }
            set
            {
                __TestContextInstance = value;
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


  

        /// <summary>
        ///ConvertToDateTime 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertToDateTimeTest()
        {
            long timestamp = 0; 
            System.DateTime expected = new System.DateTime(); 
            System.DateTime actual;
            actual = CRC.Util.DateTimeUtil.ConvertToDateTime(timestamp);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ConvertToDateTime 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertToDateTimeTest1()
        {
            string timestampString = string.Empty; 
            System.DateTime expected = new System.DateTime(); 
            System.DateTime actual;
            actual = CRC.Util.DateTimeUtil.ConvertToDateTime(timestampString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ConvertToUnix 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertToUnixTest()
        {
            string expected = string.Empty; 
            string actual;
            actual = CRC.Util.DateTimeUtil.ConvertToUnix();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ConvertToUnix 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertToUnixTest1()
        {
            System.DateTime datetime = new System.DateTime(); 
            string expected = string.Empty; 
            string actual;
            actual = CRC.Util.DateTimeUtil.ConvertToUnix(datetime);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ConvertToUnixofLong 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertToUnixofLongTest()
        {
            long expected = 0; 
            long actual;
            actual = CRC.Util.DateTimeUtil.ConvertToUnixofLong();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///ConvertToWin 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertToWinTest()
        {
            string timestampString = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = CRC.Util.DateTimeUtil.ConvertToWin(timestampString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetDayOfWeek 的测试
        ///</summary>
        [TestMethod()]
        public void GetDayOfWeekTest()
        {
            System.DateTime now = DateTime.Parse("2014/1/11"); 
            string expected ="星期六";
            string actual = CRC.Util.DateTimeUtil.GetDayOfWeek(now, true);
            Assert.AreEqual(expected, actual);
            actual = DateTimeUtil.GetDayOfWeek(now, false);
            Assert.AreEqual("周六", actual);
        }

        /// <summary>
        ///GetValidityNum 的测试
        ///</summary>
        [TestMethod()]
        public void GetValidityNumTest()
        {
            long expected = 0; 
            long actual;
            actual = CRC.Util.DateTimeUtil.GetValidityNum();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetValidityNum 的测试
        ///</summary>
        [TestMethod()]
        public void GetValidityNumTest1()
        {
            System.DateTime now = new System.DateTime(); 
            long expected = 0; 
            long actual;
            actual = CRC.Util.DateTimeUtil.GetValidityNum(now);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
