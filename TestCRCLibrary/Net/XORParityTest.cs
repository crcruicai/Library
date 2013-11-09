using CRC.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 XORParityTest 的测试类，旨在
    ///包含所有 XORParityTest 单元测试
    ///</summary>
    [TestClass()]
    public class XORParityTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
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
        /// <summary>
        ///GetParity 的测试
        ///</summary>
        [TestMethod()]
        public void GetParityTest()
        {
            List<byte> data = new List<byte> { 1, 2, 3, 4, 5, 3, 5, 7, 8 };
            int start = 0;
            int length = 9;
            byte expected = 8;
            byte actual;
            actual = XORParity.GetParity(data, start, length);
            Assert.AreEqual(expected, actual);

            //验证 length 异常
            try
            {
                start = 1;
                actual = XORParity.GetParity(data, start, length);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }

            //验证 Null异常
            try
            {
                data = null;
                actual = XORParity.GetParity(data, start, length);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }
        }

        /// <summary>
        ///GetParity 的测试
        ///</summary>
        [TestMethod()]
        public void GetParityTest1()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 3, 5, 7, 8 };
            int start = 0;
            int length = 9;
            byte expected = 8;
            byte actual;
            actual = XORParity.GetParity(data, start, length);
            Assert.AreEqual(expected, actual);
            //验证 length 异常
            try
            {
                start = 1;
                actual = XORParity.GetParity(data, start, length);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }

            //验证 Null异常
            try
            {
                data = null;
                actual = XORParity.GetParity(data, start, length);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }

        }

        /// <summary>
        ///GetParity 的测试
        ///</summary>
        [TestMethod()]
        public void GetParityTest2()
        {
            byte[] data = new byte[] { 1, 2, 3, 4, 5, 3, 5, 7, 8 };
            byte expected = 8;
            byte actual;
            actual = XORParity.GetParity(data);
            Assert.AreEqual(expected, actual);

            //验证异常
            try
            {
                data = null;
                actual = XORParity.GetParity(data);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }
        }
    }
}
