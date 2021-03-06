﻿using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CRC.Security;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 EncryptUtilTest 的测试类，旨在
    ///包含所有 EncryptUtilTest 单元测试
    ///</summary>
    [TestClass()]
    public class EncryptUtilTest
    {


        private TestContext _TestContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return _TestContextInstance;
            }
            set
            {
                _TestContextInstance = value;
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
        ///DecryptWithKey 的测试
        ///</summary>
        [TestMethod()]
        public void DecryptWithKeyTest()
        {
            string str = string.Empty; 
            string p_key = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = EncryptUtil.DecryptWithKey(str, p_key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///EncryptWithKey 的测试
        ///</summary>
        [TestMethod()]
        public void EncryptWithKeyTest()
        {
            string pass = string.Empty; 
            string p_key = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = EncryptUtil.EncryptWithKey(pass, p_key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///TripleDESDecrypt 的测试
        ///</summary>
        [TestMethod()]
        public void TripleDESDecryptTest()
        {
            string str = string.Empty; 
            string pass = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = EncryptUtil.TripleDESDecrypt(str, pass);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///TripleDESEncrypt 的测试
        ///</summary>
        [TestMethod()]
        public void TripleDESEncryptTest()
        {
            string pass = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = EncryptUtil.TripleDESEncrypt(pass);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
