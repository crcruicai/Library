using CRC.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{


    [TestClass()]
    public class Md5SecurityTest
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
        public void AddMd5Test()
        {
            string path = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = Md5Security.AddMd5(path);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void AddMd5ProfixTest()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.AddMd5Profix(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CheckMd5Test()
        {
            string path = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = Md5Security.CheckMd5(path);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DecryptStringTest()
        {
            string input = string.Empty; 
            bool throwException = false; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.DecryptString(input, throwException);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void EncryptStringTest()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.EncryptString(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetMD5_16Test()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.GetMD5_16(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetMD5_32Test()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.GetMD5_32(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetMD5_4Test()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.GetMD5_4(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void GetMD5_8Test()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.GetMD5_8(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void Md5Test()
        {
            string soucesText = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.Md5(soucesText);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void Md5BufferTest()
        {
            byte[] md5File = null; 
            int index = 0; 
            int count = 0; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security_Accessor.Md5Buffer(md5File, index, count);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void Md5EncryptHashTest()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.Md5EncryptHash(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveMd5ProfixTest()
        {
            string input = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Md5Security.RemoveMd5Profix(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ValidateValueTest()
        {
            string input = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = Md5Security.ValidateValue(input);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
