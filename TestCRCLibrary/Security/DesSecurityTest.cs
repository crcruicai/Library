using CRC.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace TestCRCLibrary
{


    [TestClass()]
    public class DesSecurityTest
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
        public void DecoderBytesTest()
        {
            byte[] value = null; 
            byte[] key = null; 
            byte[] vector = null; 
            byte[] expected = null; 
            byte[] actual;
            actual = DesSecurity.DecoderBytes(value, key, vector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DecoderStringByKeyTest()
        {
            string textValue = string.Empty; 
            Encoding textEncoding = null; 
            byte[] key = null; 
            byte[] vector = null; 
            string expected = string.Empty; 
            string actual;
            actual = DesSecurity.DecoderStringByKey(textValue, textEncoding, key, vector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DecryptFileTest()
        {
            string inFilePath = string.Empty; 
            string outFilePath = string.Empty; 
            string sDecrKey = string.Empty; 
            DesSecurity.DecryptFile(inFilePath, outFilePath, sDecrKey);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void DecryptStringTest()
        {
            string decryptString = string.Empty; 
            string decryptKey = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = DesSecurity.DecryptString(decryptString, decryptKey);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void EncoderBytesTest()
        {
            byte[] pValue = null; 
            byte[] pKey = null; 
            byte[] vector = null; 
            byte[] expected = null; 
            byte[] actual;
            actual = DesSecurity.EncoderBytes(pValue, pKey, vector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void EncoderStringByKeyTest()
        {
            string textValue = string.Empty; 
            Encoding textEncoding = null; 
            byte[] key = null; 
            byte[] vector = null; 
            string expected = string.Empty; 
            string actual;
            actual = DesSecurity.EncoderStringByKey(textValue, textEncoding, key, vector);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void EncryptFileTest()
        {
            string inFilePath = string.Empty; 
            string outFilePath = string.Empty; 
            string strEncrKey = string.Empty; 
            DesSecurity.EncryptFile(inFilePath, outFilePath, strEncrKey);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void EncryptStringTest()
        {
            string encryptString = string.Empty; 
            string encryptKey = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = DesSecurity.EncryptString(encryptString, encryptKey);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
