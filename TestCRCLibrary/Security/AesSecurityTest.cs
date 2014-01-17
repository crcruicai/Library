using CRC.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Security.Cryptography;

namespace TestCRCLibrary
{


    [TestClass()]
    public class AesSecurityTest
    {


        private TestContext __TestContextInstance;

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


        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void CheckStringTest()
        {
            string source ="123456123456"; 
            int length =12;
            bool actual = AesSecurity_Accessor.CheckString(source, length);
            Assert.AreEqual(true, actual);

            length = 13;
            actual = AesSecurity_Accessor.CheckString(source, length);
            Assert.AreEqual(false, actual);

            actual =AesSecurity_Accessor.CheckString(source: null, length: length);
            Assert.AreEqual(false, actual);

        }

        [TestMethod()]
        public void DecryptBase64StringTest()
        {
            string decryptString = "验证此测试方法的正确性。";
            string expected = AesSecurity.EncryptBase64String(decryptString);
            string actual = AesSecurity.DecryptBase64String(expected);
            Assert.AreEqual(decryptString, actual);
        }

        [TestMethod()]
        public void DecryptBase64StringTest1()
        {
            string decryptString = "验证此测试方法的正确性。";
            string decryptKey = "Abcdendk";
            decryptKey = decryptKey.PadLeft(32, 'F');
            string expected = AesSecurity.EncryptBase64String(decryptString, decryptKey);
            string actual= AesSecurity.DecryptBase64String(expected, decryptKey);
            Assert.AreEqual(decryptString, actual);
           
        }
        [TestMethod()]
        public void EncryptBase64StringTest()
        {
            string decryptString = "验证此测试方法的正确性。";
            string expected = AesSecurity.EncryptBase64String(decryptString);
            string actual = AesSecurity.DecryptBase64String(expected);
            Assert.AreEqual(decryptString, actual);
        }

        [TestMethod()]
        public void EncryptBase64StringTest1()
        {
            string decryptString = "验证此测试方法的正确性。";
            string decryptKey = "Abcdendk";
            decryptKey = decryptKey.PadLeft(32, 'F');
            string expected = AesSecurity.EncryptBase64String(decryptString, decryptKey);
            string actual = AesSecurity.DecryptBase64String(expected, decryptKey);
            Assert.AreEqual(decryptString, actual);
        }

        [TestMethod()]
        public void DecryptFileTest()
        {
            string inputFile = string.Empty; 
            string outputFile = string.Empty; 
            string decryptKey = string.Empty; 
            bool expected = false;
            bool actual = AesSecurity.DecryptFile(inputFile, outputFile, decryptKey);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DecryptStreamTest()
        {
            FileStream fs = null;
            string decryptKey = string.Empty; 
            CryptoStream expected = null; 
            CryptoStream actual;
            actual = AesSecurity.DecryptStream(fs, decryptKey);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void EncryptStreamTest()
        {
            FileStream fs = null;
            string decryptKey = string.Empty;
            CryptoStream expected = null;
            CryptoStream actual = AesSecurity.EncryptStream(fs, decryptKey);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DecryptStringTest()
        {
            string decryptString = "验证此测试方法的正确性。"; 
            string decryptKey = "123".PadLeft(32,'A');
            string expected = AesSecurity.EncryptString(decryptString, decryptKey);
            string actual = AesSecurity.DecryptString(expected, decryptKey);
            Assert.AreEqual(decryptString, actual);
        }

        [TestMethod()]
        public void EncryptStringTest()
        {
            string decryptString = "验证此测试方法的正确性。";
            string decryptKey = "123".PadLeft(32, 'A');
            string expected = AesSecurity.EncryptString(decryptString, decryptKey);
            string actual = AesSecurity.DecryptString(expected, decryptKey);
            Assert.AreEqual(decryptString, actual);
        }

        [TestMethod()]
        public void EncryptFileTest()
        {
            string inputFile = string.Empty; 
            string outputFile = string.Empty; 
            string decryptKey = string.Empty; 
            bool expected = false;
            bool actual = AesSecurity.EncryptFile(inputFile, outputFile, decryptKey);
            Assert.AreEqual(expected, actual);
        }

     

    }
}
