using QQSDK.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Drawing;

namespace TestQQSDK
{


    [TestClass()]
    public class ToolTest
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
        public void HashTest()
        {
            ulong num = 0; 
            string ptwebqq = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Tool.Hash(num, ptwebqq);
            Assert.AreEqual(expected, actual);
           
        }

        [TestMethod()]
        public void GetRandomNumberTest()
        {
            int length = 10; 
            string expected = string.Empty; 
            string actual;
            actual = Tool.GetRandomNumber(length);
            Assert.AreEqual(length, actual.Length);

            length = 20;
            actual = Tool.GetRandomNumber(length);
            Assert.AreEqual(length, actual.Length);
            length = 0;
            actual = Tool.GetRandomNumber(length);
            Assert.AreEqual(length, actual.Length);
        }

        [TestMethod()]
        public void GetHashTest()
        {
            string uin = string.Empty; 
            string ptwebqq = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = Tool.GetHash(uin, ptwebqq);
            Assert.AreEqual(expected, actual);
           
        }

        [TestMethod()]
        public void GetFontStyleTest()
        {
            //测试 字体样式转换为文本.
            FontStyle s=FontStyle.Bold | FontStyle.Italic | FontStyle.Underline;
            Font font = new Font("宋体",10F,s);
            string expected = "1,1,1";
            string actual;
            actual = Tool.GetFontStyle(font);
            Assert.AreEqual(expected, actual);

            Font newfont = new Font("宋体", 10F);
            expected = "0,0,0";
            actual = Tool.GetFontStyle(newfont);
            Assert.AreEqual(expected, actual);

        }
        [TestMethod()]
        public void GetFontStyleTest1()
        {
            //测试 文本转换为字体样式.
            FontStyle s = FontStyle.Bold | FontStyle.Italic | FontStyle.Underline;
            string expected = "1,1,1";
            FontStyle a = Tool.GetFontStyle(expected);
            Assert.AreEqual(a, s);

            s = FontStyle.Bold;
            expected = "1,0,0";
            a = Tool.GetFontStyle(expected);
            Assert.AreEqual(a, s);
        }
        [TestMethod()]
        public void GetColorTest()
        {
            //测试 颜色转换为文本.
            Color color = Color.Red;
            string expected = "FF0000";
            string actual;
            actual = Tool.GetColor(color);
            Assert.AreEqual(expected, actual);
           
        }

        [TestMethod()]
        public void GetColorTest1()
        {
            // 测试 文本转换为颜色.
            string text = "FF0000";
            Color expected = Color.Red;
            Color actual;
            actual = Tool.GetColor(text);
            Assert.AreEqual(expected.A, actual.A);
            Assert.AreEqual(expected.B, actual.B);
            Assert.AreEqual(expected.G, actual.G);
            Assert.AreEqual(expected.R, actual.R);
          
        }

        [TestMethod()]
        public void CheckQQPasswordTest()
        {
            // 验证QQ密码长度 是否符合规范.
            string password = "12315"; 
            bool expected = false; 
            bool actual;
            actual = Tool.CheckQQPassword(password);
            Assert.AreEqual(expected, actual);


            password = "152512";
            actual = Tool.CheckQQPassword(password);
            Assert.AreEqual(true, actual);

            password = "ddlaiojrowierj12";
            actual = Tool.CheckQQPassword(password);
            Assert.AreEqual(true, actual);
            password = "ddlaiojrowierj1255555";
            actual = Tool.CheckQQPassword(password);
            Assert.AreEqual(false, actual);
        }

        [TestMethod()]
        public void CheckQQNumberTest()
        {
            string qqnumber = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = Tool.CheckQQNumber(qqnumber);
            Assert.AreEqual(expected, actual);
           
        }
    }
}
