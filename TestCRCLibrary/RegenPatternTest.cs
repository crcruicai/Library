using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 RegenPatternTest 的测试类，旨在
    ///包含所有 RegenPatternTest 单元测试
    ///</summary>
    [TestClass()]
    public class RegenPatternTest
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
        ///IsMatch 的测试
        ///</summary>
        [TestMethod()]
        public void IsMatchTest()
        {
            string input = "23562564652";


            bool expected = true;
            bool actual;
            actual = RegenPattern.IsMatch(input, "^[0-9]*$");
            Assert.AreEqual(expected, actual);

            RegenPattern.IsMatch("蔡锐财", RegenPattern.ChinaName).AreEqualWith(true);
            RegenPattern.IsMatch("ckdk", RegenPattern.ChinaName).AreEqualWith(false);
            RegenPattern.IsMatch("123陈", RegenPattern.ChinaName).AreEqualWith(false);


            RegenPattern.IsMatch("crcruicai@163.com", RegenPattern.Email).AreEqualWith(true);
            RegenPattern.IsMatch("crcruicai@-163.com", RegenPattern.Email).AreEqualWith(false);


            RegenPattern.IsMatch("445202198803252758", RegenPattern.IdentityCard).AreEqualWith(true);

            RegenPattern.IsMatch("192.168.0.1", RegenPattern.IPAddress).AreEqualWith(true);

            RegenPattern.IsMatch("0663-8887586", RegenPattern.PhoneNumber).AreEqualWith(true);

            RegenPattern.IsMatch("0663-8887586", RegenPattern.UserName).AreEqualWith(false);


            RegenPattern.IsMatch("39", RegenPattern.Age).AreEqualWith(true);
            RegenPattern.IsMatch("0", RegenPattern.Age).AreEqualWith(false);
            RegenPattern.IsMatch("129", RegenPattern.Age).AreEqualWith(true);
            RegenPattern.IsMatch("1", RegenPattern.Age).AreEqualWith(true);
            RegenPattern.IsMatch("130", RegenPattern.Age).AreEqualWith(false);
            RegenPattern.IsMatch("1291", RegenPattern.Age).AreEqualWith(false);

            RegenPattern.IsMatch("1291", RegenPattern.NaturalNumber).AreEqualWith(true);
            RegenPattern.IsMatch("1", RegenPattern.NaturalNumber).AreEqualWith(true);
            RegenPattern.IsMatch("0", RegenPattern.NaturalNumber).AreEqualWith(true);
            RegenPattern.IsMatch("0291", RegenPattern.NaturalNumber).AreEqualWith(false);
            RegenPattern.IsMatch("-191", RegenPattern.NaturalNumber).AreEqualWith(false);
            RegenPattern.IsMatch("0291", RegenPattern.NaturalNumber).AreEqualWith(false);

        }

        /// <summary>
        ///IsNumber 的测试
        ///</summary>
        [TestMethod()]
        public void IsNumberTest()
        {
            RegenPattern.IsNumber("12345", 5).IsTrue();
            RegenPattern.IsNumber("12345", 4).IsFalse();
            RegenPattern.IsNumber("123466", 4).IsFalse();

        }

        /// <summary>
        ///IsNumberMore 的测试
        ///</summary>
        [TestMethod()]
        public void IsNumberMoreTest()
        {
          
            RegenPattern.IsNumberMore("12345", 5).IsTrue();
            RegenPattern.IsNumberMore("123454444", 5).IsTrue();
        }

        /// <summary>
        ///IsNumberRange 的测试
        ///</summary>
        [TestMethod()]
        public void IsNumberRangeTest()
        {
            RegenPattern.IsNumberRange("123456", 4, 6).IsTrue();
            RegenPattern.IsNumberRange("1234", 4, 6).IsTrue();

            RegenPattern.IsNumberRange("123", 4, 6).IsFalse();

            RegenPattern.IsNumberRange("1234564", 4, 6).IsFalse();
            RegenPattern.IsNumberRange("1231p", 4, 6).IsFalse();
        }
    }
}
