using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CRC.Extension;
namespace TestCRCLibrary
{


    [TestClass()]
    public class EncodeUtilTest
    {


        private TestContext _TestContextInstance;

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
        /// 
        /// </summary>
        [TestMethod()]
        public void DeBase64Test()
        {
            string str = "验证此测试方法的正确性。";
            string actual;
            string expected;
            actual = EncodeUtil.ToBase64(str);

            expected = EncodeUtil.DeBase64(actual);
            Assert.AreEqual(expected,str);

        }

        [TestMethod()]
        public void DecodeUnicodeTest()
        {
            string expected = "验证此测试方法的正确性。";
            string str = EncodeUtil.EncodeUnicode(expected);
            string actual;
            actual = EncodeUtil.DecodeUnicode(str);
            Assert.AreEqual(expected, actual);
           
        }

        [TestMethod()]
        public void EncodeUnicodeTest()
        {
            string str = "abcde验证此测试方法的正确性。ssss"; 
            bool upperCase = false; 
            string actual;
            actual = EncodeUtil.EncodeUnicode(str, upperCase);
            string expected = EncodeUtil.DecodeUnicode(actual);
            Assert.AreEqual(expected, str);
          
        }

        [TestMethod()]
        public void EncodeUnicodeTest1()
        {
            string str = "abcde验证此测试方法的正确性。ssss";
            string actual;
            actual = EncodeUtil.EncodeUnicode(str);
            string expected = EncodeUtil.DecodeUnicode(actual);

            Assert.AreEqual(expected, str);
        }

        [TestMethod()]
        public void GetHexLengthTest()
        {
            string str = string.Empty; 
            int index = 0; 
            int maxLength = 0; 
            int expected = 0; 
            int actual;
            actual = EncodeUtil.GetHexLength(str, index, maxLength);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsHexTest()
        {
            char ch = '1'; 
            bool expected = true; 
            bool actual;
            actual = EncodeUtil.IsHex(ch);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void IsHexTest1()
        {
            string str = "a"; 
            int index = 0; 
            bool expected = true; 
            bool actual;
            actual = EncodeUtil.IsHex(str, index);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MD5EncodeTest()
        {
            string text = "string.Empty"; 
            int len = 16; 
            string expected = "83D948B3F6D02132"; 
            string actual;
            actual = EncodeUtil.Md5Encode(text, len);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void MD5EncodeTest1()
        {

            byte[] data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            int len = 16; 
            string expected = "067972C3F34F094B"; 
            string actual;
            actual = EncodeUtil.Md5Encode(data, len);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Sha1EncodeTest()
        {
            string text = "验证此测试方法的正确性"; 
            string expected = "B9-B3-B1-18-B6-DC-E1-E9-14-E4-97-97-A6-E1-CC-B5-8E-26-F9-9A"; 
            string actual;
            actual = EncodeUtil.Sha1Encode(text);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod()]
        public void ToBase64Test()
        {
            string str = "验证此测试方法的正确性。"; 
           
            string actual;
            actual = EncodeUtil.ToBase64(str);
            string expected = EncodeUtil.DeBase64(actual);
            Assert.AreEqual(expected, str);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void HttpUrlEncodeTest()
        {
            string text = "!*'();:@&=+$,/?#[]|\\";



        }

    }
}
