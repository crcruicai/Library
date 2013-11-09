using CRC.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 IPUtilTest 的测试类，旨在
    ///包含所有 IPUtilTest 单元测试
    ///</summary>
    [TestClass()]
    public class IPUtilTest
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
        ///GetIPText 的测试
        ///</summary>
        [TestMethod()]
        public void GetIPTextTest()
        {
            byte[] data = new byte[] { 127, 0, 0, 1 };
            string expected = "127.0.0.1";
            string actual;
            actual = IPUtil.GetIPText(data);
            Assert.AreEqual(expected, actual);

            //验证抛出异常的情况
            try
            {
                data = new byte[] { 127, 0, 0 };
                actual = IPUtil.GetIPText(data);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }
        }

        /// <summary>
        ///GetIPAddress 的测试
        ///</summary>
        [TestMethod()]
        public void GetIPAddressTest()
        {
            byte[] data = new byte[] { 127, 0, 0, 1 };
            IPAddress expected = IPAddress.Parse("127.0.0.1");
            IPAddress actual;
            actual = IPUtil.GetIPAddress(data);
            Assert.AreEqual(expected, actual);

            //验证抛出异常的情况
            try
            {
                data = new byte[] { 127, 0, 0 };
                actual = IPUtil.GetIPAddress(data);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }
        }

        /// <summary>
        ///GetIPAddress 的测试
        ///</summary>
        [TestMethod()]
        public void GetIPAddressTest1()
        {
            byte[] data = new byte[] { 127, 0, 0, 1 };
            IPAddress expected = IPAddress.Parse("127.0.0.1");
            IPAddress actual;
            actual = IPUtil.GetIPAddress(data,0);
            Assert.AreEqual(expected, actual);

            //验证抛出异常的情况
            try
            {
                data = new byte[] { 127, 0, 0 };
                actual = IPUtil.GetIPAddress(data,5);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }
        }



        /// <summary>
        ///GetIPText 的测试
        ///</summary>
        [TestMethod()]
        public void GetIPTextTest1()
        {
            byte[] data = new byte[] { 127, 0, 0, 1 };
            int start = 0;
            string expected = "127.0.0.1";
            string actual;
            actual = IPUtil.GetIPText(data, start);
            Assert.AreEqual(expected, actual);
            //验证抛出异常的情况
            try
            {
                start = 1;
                actual = IPUtil.GetIPText(data, start);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }

        }

        /// <summary>
        ///GetPort 的测试
        ///</summary>
        [TestMethod()]
        public void GetPortTest()
        {
            byte[] data = new byte[] { 127, 0, 0, 1, 10, 0 };
            int index = 4;
            int expected = 2560;
            int actual;
            actual = IPUtil.GetPort(data, index);
            Assert.AreEqual(expected, actual);
            //验证抛出异常的情况
            try
            {
                index = 5;
                actual = IPUtil.GetPort(data, index);
                Assert.AreEqual(expected, actual);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);
            }

        }

        /// <summary>
        ///PortToBytes 的测试
        ///</summary>
        [TestMethod()]
        public void PortToBytesTest()
        {
            int port = 8001;

            byte[] actual = IPUtil.PortToBytes(port);
            port = IPUtil.GetPort(actual, 0);
            Assert.AreEqual(port, 8001);

            //验证抛出异常的情况.
            try
            {
                port = 65536;
                actual = IPUtil.PortToBytes(port);//此处抛出异常.
                port = IPUtil.GetPort(actual, 0);
                Assert.AreEqual(port, 8001);
            }
            catch (Exception)
            {
                Assert.IsFalse(false);

            }

        }

        /// <summary>
        ///ParseIPEndPoint 的测试
        ///</summary>
        [TestMethod()]
        public void ParseIPEndPointTest()
        {
            string ipString = "192.168.0.1:8001";
            IPEndPoint expected = new IPEndPoint(IPAddress.Parse("192.168.0.1"), 8001);
            IPEndPoint actual;
            actual = IPUtil.ParseIPEndPoint(ipString);
            Assert.AreEqual(expected, actual);
        

        
        
        
        }


    }
}
