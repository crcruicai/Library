using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 ConvertRmbTest 的测试类，旨在
    ///包含所有 ConvertRmbTest 单元测试
    ///</summary>
    [TestClass()]
    public class ConvertRmbTest
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
        ///Convert 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            decimal number = 152142;
            string expected = "壹拾伍万贰仟壹佰肆拾贰元整"; 
            string actual = CRC.Util.ConvertRmb.Convert(number);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///Convert 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertTest1()
        {
            double number = 152142.01;
            string expected = "壹拾伍万贰仟壹佰肆拾贰元壹分";
            string actual = CRC.Util.ConvertRmb.Convert(number);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// 验证 RmbException 异常
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(RmbException))]
        [DeploymentItem("CRCLibrary.dll")]
        public void CheckNumber_Max_RmbExceptionTest()
        {
            double  number = 99999999999999999999999991.99;
            CRC.Util.ConvertRmb_Accessor.CheckNumberLimit(number);
        }
        [TestMethod()]
        [ExpectedException(typeof(RmbException))]
        [DeploymentItem("CRCLibrary.dll")]
        public void CheckNumber_Min_RmbExceptionTest()
        {
            double  number = -99999999999999999999999991.99;
            CRC.Util.ConvertRmb_Accessor.CheckNumberLimit(number);
        }

        /// <summary>
        ///CombinUnit 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void CombinUnitTest()
        {
            string rmb = "11111兆亿万";
            string expected = "11111兆";
            string actual = CRC.Util.ConvertRmb_Accessor.CombinUnit(rmb);
            Assert.AreEqual(expected, actual);


            rmb = "亿万152452";
            expected = "亿152452";
            actual = ConvertRmb_Accessor.CombinUnit(rmb);
            Assert.AreEqual(expected, actual);

            rmb = "兆亿152452";
            expected = "兆152452";
            actual = ConvertRmb_Accessor.CombinUnit(rmb);
            Assert.AreEqual(expected, actual);

         
        }

   

        /// <summary>
        ///DigToCC 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void DigToCcTest()
        {
            char c = '1';
            string expected = "壹";
            string actual = CRC.Util.ConvertRmb_Accessor.DigToCC(c);
            Assert.AreEqual(expected, actual);

            c = '0';
            expected = "零";
            actual = CRC.Util.ConvertRmb_Accessor.DigToCC(c);
            Assert.AreEqual(expected, actual);

            c = '9';
            expected = "玖";
            actual = CRC.Util.ConvertRmb_Accessor.DigToCC(c);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ConvertInt 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void ConvertIntTest()
        {
            decimal number = 152142;
            string expected = "壹拾伍万贰仟壹佰肆拾贰元";
            string actual = ConvertRmb_Accessor.ConvertInt(number.ToString ());
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///ConvertDecimal 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void ConvertDecimalTest()
        {
            string decPart = "22";
            string expected = "贰角贰分";
            string actual = CRC.Util.ConvertRmb_Accessor.ConvertDecimal(decPart);
            Assert.AreEqual(expected, actual);
        }
    }
}
