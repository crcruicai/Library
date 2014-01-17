using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;

namespace TestCRCLibrary
{


    [TestClass()]
    public class StringParserTest
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


        [TestMethod()]
        public void AtTest()
        {
            
            string strString = "font is songti";
            StringParser target = new StringParser(strString);
            target.At("font").IsTrue();

            target.At("is").IsFalse();
        }

        [TestMethod()]
        public void AtNoCaseTest()
        {
            string strString = "font is songti";
            StringParser target = new StringParser(strString);
            target.AtNoCase("Font").IsTrue();

            target.AtNoCase("font").IsTrue();
            target.AtNoCase("is").IsFalse();
        }

        [TestMethod()]
        public void ExtractToTest()
        {
            StringParser target = new StringParser("Font is song ti"); 
            string temp="";
            target.ExtractTo("song", ref temp).IsTrue ();

            temp.AreEqualWith("Font is ");
            
        }

        [TestMethod()]
        public void ExtractToEndTest()
        {
            StringParser target = new StringParser("Font is song ti");
            string temp = "";

            target.ExtractToEnd(ref temp);
            temp.AreEqualWith("Font is song ti");


        }

        [TestMethod()]
        public void ExtractToNoCaseTest()
        {
            StringParser target = new StringParser("Font is song ti");
            string temp = "";

            target.ExtractToNoCase("Song", ref temp).IsTrue ();
            temp.AreEqualWith("Font is ");

            target.ExtractToNoCase("song", ref temp).IsFalse ();
            

        }

        [TestMethod()]
        public void ExtractUntilTest()
        {
            StringParser target = new StringParser("Font is song ti");
            string temp = "";

            target.ExtractUntil("song", ref temp).IsTrue();
            temp.AreEqualWith("Font is ");


        }

        [TestMethod()]
        public void ExtractUntilNoCaseTest()
        {
            StringParser target = new StringParser("Font is song ti");
            string temp = "";

            target.ExtractUntilNoCase("Song", ref temp).IsTrue();
            temp.AreEqualWith("Font is ");

        }

        [TestMethod()]
        public void GetLinksTest()
        {
            
        }

        [TestMethod()]
        public void RemoveCommentsTest()
        {
            string strString = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringParser.RemoveComments(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveEnclosingAnchorTagTest()
        {
            string strString = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringParser.RemoveEnclosingAnchorTag(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveEnclosingQuotesTest()
        {
            string strString = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringParser.RemoveEnclosingQuotes(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveHtmlTest()
        {
            string strString = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringParser.RemoveHtml(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void RemoveScriptsTest()
        {
            string strString = string.Empty; 
            string expected = string.Empty; 
            string actual;
            actual = StringParser.RemoveScripts(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ReplaceEveryTest()
        {
            StringParser target = new StringParser(); 
            string strOccurrence = string.Empty; 
            string strReplacement = string.Empty; 
            int expected = 0; 
            int actual;
            actual = target.ReplaceEvery(strOccurrence, strReplacement);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ReplaceEveryExactTest()
        {
            StringParser target = new StringParser(); 
            string strOccurrence = string.Empty; 
            string strReplacement = string.Empty; 
            int expected = 0; 
            int actual;
            actual = target.ReplaceEveryExact(strOccurrence, strReplacement);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ResetPositionTest()
        {
            StringParser target = new StringParser(); 
            target.ResetPosition();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void SeekToTest()
        {
            StringParser_Accessor target = new StringParser_Accessor(); 
            string strString = string.Empty; 
            bool bNoCase = false; 
            bool bPositionAfter = false; 
            bool expected = false; 
            bool actual;
            actual = target.SeekTo(strString, bNoCase, bPositionAfter);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SkipToEndOfTest()
        {
            StringParser target = new StringParser(); 
            string strString = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.SkipToEndOf(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SkipToEndOfNoCaseTest()
        {
            StringParser target = new StringParser(); 
            string strText = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.SkipToEndOfNoCase(strText);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SkipToStartOfTest()
        {
            StringParser target = new StringParser(); 
            string strString = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.SkipToStartOf(strString);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SkipToStartOfNoCaseTest()
        {
            StringParser target = new StringParser(); 
            string strText = string.Empty; 
            bool expected = false; 
            bool actual;
            actual = target.SkipToStartOfNoCase(strText);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void ContentTest()
        {
            StringParser target = new StringParser(); 
            string expected = string.Empty; 
            string actual;
            target.Content = expected;
            actual = target.Content;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void PositionTest()
        {
            StringParser target = new StringParser(); 
            int actual;
            actual = target.Position;
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
