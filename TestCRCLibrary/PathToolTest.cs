using CRC.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 PathToolTest 的测试类，旨在
    ///包含所有 PathToolTest 单元测试
    ///</summary>
    [TestClass()]
    public class PathToolTest
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
        ///LastDirectoryName 的测试
        ///</summary>
        [TestMethod()]
        public void LastDirectoryNameTest()
        {
            string path ="c:\\abc\\jjkk\\lkjkj"; 
            string expected = "jjkk";
            string actual = CRC.Files.PathTool.LastDirectoryName(path);
            Assert.AreEqual(expected, actual);

            path = "c:\\abc\\jjkk\\lkjkj.exe";
            actual = CRC.Files.PathTool.LastDirectoryName(path);
            Assert.AreEqual(expected, actual);

            path = "c:\\abc//jjkk//lkjkj.exe";
            actual = CRC.Files.PathTool.LastDirectoryName(path);
            Assert.AreEqual(expected, actual);

            path = "c:\\abc";
            expected = "c";
            actual = CRC.Files.PathTool.LastDirectoryName(path);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///FatherLayer 的测试
        ///</summary>
        [TestMethod()]
        public void FatherLayerTest()
        {
            //string path = string.Empty; 
            //string expected = string.Empty; 
            //string actual;
            //actual = CRC.Files.PathTool.FatherLayer(path);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///LastDirectoryPath 的测试
        ///</summary>
        [TestMethod()]
        public void LastDirectoryPathTest()
        {
            string path = "c:\\abc\\jjkk\\lkjkj";
            string expected = "c:\\abc\\jjkk";
            string actual = CRC.Files.PathTool.LastDirectoryPath(path);
            Assert.AreEqual(expected, actual);

            path = "c:\\abc\\jjkk\\lkjkj.exe";
            actual = CRC.Files.PathTool.LastDirectoryPath(path);
            Assert.AreEqual(expected, actual);

            path = "c:\\abc//jjkk//lkjkj.exe";
            actual = CRC.Files.PathTool.LastDirectoryPath(path);
            Assert.AreEqual(expected, actual);

            path = "c:\\abc";
            expected = "c:\\";
            actual = CRC.Files.PathTool.LastDirectoryPath(path);
            Assert.AreEqual(expected, actual);
        }
    }
}
