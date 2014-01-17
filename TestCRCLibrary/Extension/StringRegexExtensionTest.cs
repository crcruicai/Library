using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 StringRegexExtensionTest 的测试类，旨在
    ///包含所有 StringRegexExtensionTest 单元测试
    ///</summary>
    [TestClass()]
    public class StringRegexExtensionTest
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
        ///GetPageTitle 的测试
        ///</summary>
        [TestMethod()]
        public void GetPageTitleTest()
        {
            string s = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringRegexExtension.GetPageTitle(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///GetScript 的测试
        ///</summary>
        [TestMethod()]
        public void GetScriptTest()
        {
            string s = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringRegexExtension.GetScript(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsChineseChar 的测试
        ///</summary>
        [TestMethod()]
        public void IsChineseCharTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsChineseChar(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsEmail 的测试
        ///</summary>
        [TestMethod()]
        public void IsEmailTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsEmail(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsHTML 的测试
        ///</summary>
        [TestMethod()]
        public void IsHTMLTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsHTML(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsIPAddress 的测试
        ///</summary>
        [TestMethod()]
        public void IsIPAddressTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsIPAddress(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsMatch 的测试
        ///</summary>
        [TestMethod()]
        public void IsMatchTest()
        {
            string s = string.Empty; 
            string pattern = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsMatch(s, pattern);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsNumber 的测试
        ///</summary>
        [TestMethod()]
        public void IsNumberTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsNumber(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsPhoneNumber 的测试
        ///</summary>
        [TestMethod()]
        public void IsPhoneNumberTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsPhoneNumber(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsUnicode 的测试
        ///</summary>
        [TestMethod()]
        public void IsUnicodeTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsUnicode(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///IsUrl 的测试
        ///</summary>
        [TestMethod()]
        public void IsUrlTest()
        {
            string s = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = StringRegexExtension.IsUrl(s);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///Match 的测试
        ///</summary>
        [TestMethod()]
        public void MatchTest()
        {
            string s = string.Empty; 
            string pattern = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringRegexExtension.Match(s, pattern);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
