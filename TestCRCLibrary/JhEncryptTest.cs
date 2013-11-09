using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 JhEncryptTest 的测试类，旨在
    ///包含所有 JhEncryptTest 单元测试
    ///</summary>
    [TestClass()]
    public class JhEncryptTest
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
        ///JhEncrypt 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void JhEncryptConstructorTest()
        {
            JhEncrypt target = new JhEncrypt();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        /// <summary>
        ///Decrypt 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptTest()
        {
            byte[] encrypted = null; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = JhEncrypt.Decrypt(encrypted);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Decrypt 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptTest1()
        {
            string encrypted = string.Empty; // TODO: 初始化为适当的值
            string key = string.Empty; // TODO: 初始化为适当的值
            Encoding encoding = null; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = JhEncrypt.Decrypt(encrypted, key, encoding);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Decrypt 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptTest2()
        {
            byte[] encrypted = null; // TODO: 初始化为适当的值
            byte[] key = null; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = JhEncrypt.Decrypt(encrypted, key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Decrypt 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptTest3()
        {
            string original = string.Empty; // TODO: 初始化为适当的值
            Encoding encoding = null; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = JhEncrypt.Decrypt(original, encoding);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Decrypt 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptTest4()
        {
            string original = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = JhEncrypt.Decrypt(original);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Decrypt 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptTest5()
        {
            string original = string.Empty; // TODO: 初始化为适当的值
            string key = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = JhEncrypt.Decrypt(original, key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Encrypt 的测试
        ///</summary>
        [TestMethod()]
        public void EncryptTest()
        {
            byte[] original = null; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = JhEncrypt.Encrypt(original);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Encrypt 的测试
        ///</summary>
        [TestMethod()]
        public void EncryptTest1()
        {
            string original = string.Empty; // TODO: 初始化为适当的值
            string key = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = JhEncrypt.Encrypt(original, key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Encrypt 的测试
        ///</summary>
        [TestMethod()]
        public void EncryptTest2()
        {
            string original = string.Empty; // TODO: 初始化为适当的值
            string expected = string.Empty; // TODO: 初始化为适当的值
            string actual;
            actual = JhEncrypt.Encrypt(original);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Encrypt 的测试
        ///</summary>
        [TestMethod()]
        public void EncryptTest3()
        {
            byte[] original = null; // TODO: 初始化为适当的值
            byte[] key = null; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = JhEncrypt.Encrypt(original, key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///MakeMD5 的测试
        ///</summary>
        [TestMethod()]
        public void MakeMD5Test()
        {
            byte[] original = null; // TODO: 初始化为适当的值
            byte[] expected = null; // TODO: 初始化为适当的值
            byte[] actual;
            actual = JhEncrypt.MakeMD5(original);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
