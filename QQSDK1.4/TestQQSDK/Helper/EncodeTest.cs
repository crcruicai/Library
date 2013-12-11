using QQSDK.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestQQSDK
{


    [TestClass()]
    public class EncodeTest
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
        [DeploymentItem("QQSDK.dll")]
        public void byteToUpperTest()
        {
            byte item = 0; 
            bool flag = false; 
            string expected = string.Empty; 
            string actual;
            actual = Encode_Accessor.byteToUpper(item, flag);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ToUnicodeStringTest()
        {
            string str = string.Empty; 
            bool isAll = false; 
            string expected = string.Empty; 
            string actual;
            actual = Encode.ToUnicodeString(str, isAll);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ToUnicodeTest()
        {
            string text = string.Empty; 
            bool isAll = false; 
            string expected = string.Empty; 
            string actual;
            actual = Encode.ToUnicode(text, isAll);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ToUTF8Test()
        {
            string text = string.Empty; 
            bool isAll = false; 
            bool isUpper = false; 
            string expected = string.Empty; 
            string actual;
            actual = Encode.ToUTF8(text, isAll, isUpper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ToGB2312Test()
        {
            string text = string.Empty; 
            bool isAll = false; 
            bool isUpper = false; 
            string expected = string.Empty; 
            string actual;
            actual = Encode.ToGB2312(text, isAll, isUpper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ToBase64Test()
        {
            Encode target = new Encode(); 
            string str = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = target.ToBase64(str);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void Sha1EncodeTest()
        {
            //Encode target = new Encode(); 
            //string text = string.Empty; 
            //string expected = string.Empty; 
            //string actual;
            //actual = target.Sha1Encode(text);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void MD5EncodeTest()
        {
            //Encode target = new Encode(); 
            //string text = string.Empty; 
            //int len = 0; 
            //string expected = string.Empty; 
            //string actual;
            //actual = target.MD5Encode(text, len);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void HttpUrlEncodeTest()
        {
            string text = string.Empty; 
            bool isAll = false; 
            bool isUpper = false; 
            string expected = string.Empty; 
            string actual;
            actual = Encode.HttpUrlEncode(text, isAll, isUpper);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DeUnicodeTest()
        {
            string str = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Encode.DeUnicode(str);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void DeBase64Test()
        {
            Encode target = new Encode(); 
            string str = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = target.DeBase64(str);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void BytesToMD5StringTest()
        {
            //Encode target = new Encode(); 
            //byte[] data = null; 
            //int len = 0; 
            //string expected = string.Empty; 
            //string actual;
            //actual = target.BytesToMD5String(data, len);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void EncodeConstructorTest()
        {
            Encode target = new Encode();
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }
    }
}
