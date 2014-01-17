using CRC.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using CRC.Extension;
namespace TestCRCLibrary
{


    /// <summary>
    ///这是 ConvertCodeTest 的测试类，旨在
    ///包含所有 ConvertCodeTest 单元测试
    ///</summary>
    [TestClass()]
    public class ConvertCodeTest
    {


        private TestContext __TestContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
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


        /// <summary>
        ///ConvertCode 构造函数 的测试
        ///</summary>
        [TestMethod()]
        public void ConvertCodeConstructorTest()
        {
            ConvertCode target = new ConvertCode();
        }

        /// <summary>
        ///AddSeparate 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void AddSeparateTest()
        {
            Separate se = Separate.Bank;
            string expected = " ";
            string actual = ConvertCode_Accessor.AddSeparate(se);
            Assert.AreEqual(expected, actual);

            se = Separate.None;
            expected = "";
            actual = ConvertCode_Accessor.AddSeparate(se);
            Assert.AreEqual(expected, actual);

            se = Separate.Ox;
            expected = "0x";
            actual = ConvertCode_Accessor.AddSeparate(se);
            Assert.AreEqual(expected, actual);

            se = Separate.OX;
            expected = "0X";
            actual = ConvertCode_Accessor.AddSeparate(se);
            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        ///ByteToBinary 的测试
        ///</summary>
        [TestMethod()]
        public void ByteToBinaryTest()
        {
            byte data = 0;
            string expected = "00000000";
            string actual = ConvertCode.ByteToBinary(data);
            Assert.AreEqual(expected, actual);

            data = 255;
            expected = "11111111";
            actual = ConvertCode.ByteToBinary(data);
            Assert.AreEqual(expected, actual);

            data = 168;
            expected = "10101000";
            actual = ConvertCode.ByteToBinary(data);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///BytesTo16 的测试
        ///</summary>
        [TestMethod()]
        public void BytesTo16Test()
        {
            byte[] bytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            Separate se = Separate.Ox;
            string expected = "0x01 0x02 0x03 0x04 0x05 0x06 0x07 0x08 ";
            string actual = ConvertCode.BytesTo16(bytes, se);
            Assert.AreEqual(expected, actual);

            se = Separate.OX;
            expected = "0X01 0X02 0X03 0X04 0X05 0X06 0X07 0X08 ";
            actual = ConvertCode.BytesTo16(bytes, se);
            Assert.AreEqual(expected, actual);

            se = Separate.Bank;
            expected = " 01 02 03 04 05 06 07 08";
            actual = ConvertCode.BytesTo16(bytes, se);
            Assert.AreEqual(expected, actual);

            se = Separate.None;
            expected = "0102030405060708";
            actual = ConvertCode.BytesTo16(bytes, se);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///BytesToBinary 的测试
        ///</summary>
        [TestMethod()]
        public void BytesToBinaryTest()
        {
            byte[] bytes = new byte[] { 1, 2, 3 };
            Separate se = Separate.None;
            string expected = "000000010000001000000011";
            string actual = ConvertCode.BytesToBinary(bytes, se);
            Assert.AreEqual(expected, actual);

            se = Separate.Bank;
            expected = "00000001 00000010 00000011 ";
            actual = ConvertCode.BytesToBinary(bytes, se);
            Assert.AreEqual(expected, actual);

            se = Separate.Ox;
            expected = "00000001 00000010 00000011 ";
            actual = ConvertCode.BytesToBinary(bytes, se);
            Assert.AreEqual(expected, actual);

            se = Separate.OX;
            expected = "00000001 00000010 00000011 ";
            actual = ConvertCode.BytesToBinary(bytes, se);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///BytesToString 的测试
        ///</summary>
        [TestMethod()]
        public void BytesToStringTest()
        {
            byte[] bytes = new byte[] { 1, 2, 3 };
            Separate se = new Separate();

            string actual = ConvertCode.BytesToString(bytes, se);
            byte[] expected = ConvertCode.StringToBtyes(actual);
            Assert.AreEqual(expected.ArrayAreEqual(bytes), true);

        }

        /// <summary>
        ///DelSeparate 的测试
        ///</summary>
        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void DelSeparateTest()
        {
            string inString = "0X01 0X02 0X03 0X04 0x05 0X06 0X07 0X08 ";
            string expected = "0102030405060708";
            string actual = ConvertCode_Accessor.DelSeparate(inString);
            Assert.AreEqual(expected, actual);

        }

        /// <summary>
        ///HexToBtyes 的测试
        ///</summary>
        [TestMethod()]
        public void HexToBtyesTest()
        {
            string inSting = "0X01 0X02 0X03 0X04 0x05 0X06 0X07 0X08 "; 
            byte[] expected = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            byte[] actual = ConvertCode.HexToBtyes(inSting);

            Assert.AreEqual(expected.ArrayAreEqual(actual), true);

        }

        /// <summary>
        ///HexToString 的测试
        ///</summary>
        [TestMethod()]
        public void HexToStringTest()
        {
            string inSting = ConvertCode.StringToHex("初始化为适当的值", Separate.OX);
            string expected = "初始化为适当的值";
            string actual = ConvertCode.HexToString(inSting);
            Assert.AreEqual(expected, actual);


        }

        /// <summary>
        ///StringToBtyes 的测试
        ///</summary>
        [TestMethod()]
        public void StringToBtyesTest()
        {
            string inSting = "初始化为适当的值";
            byte[] actual = ConvertCode.StringToBtyes(inSting);
            string text = ConvertCode.BytesToString(actual, Separate.None);
            Assert.AreEqual(text, inSting);

        }

        /// <summary>
        ///StringToBytes 的测试
        ///</summary>
        [TestMethod()]
        public void StringToBytesTest()
        {

        }

        /// <summary>
        ///StringToHex 的测试
        ///</summary>
        [TestMethod()]
        public void StringToHexTest()
        {
            string str = "TODO: 初始化为适当的值";
            Separate se = Separate.Bank;
            string actual = ConvertCode.StringToHex(str, se);
            string expected = ConvertCode.HexToString(actual);

            Assert.AreEqual(expected, str);

        }

        [TestMethod()]
        public void BTest()
        {
            string text = "00001111";
            byte value = ConvertCode.BinaryToByte(text);
            Assert.AreEqual(value, 15);

            text = "1111";
            value = ConvertCode.BinaryToByte(text);
            Assert.AreEqual(value, 15);

            text = "11110000";
            value = ConvertCode.BinaryToByte(text);
            Assert.AreEqual(value, 0xF0);

            text = "1111000";
            value = ConvertCode.BinaryToByte(text);
            Assert.AreEqual(value, 0x78);

            text = "0";
            value = ConvertCode.BinaryToByte(text);
            Assert.AreEqual(value, 0x00);

            text = "11111111";
            value = ConvertCode.BinaryToByte(text);
            Assert.AreEqual(value, 0xFF);
        }
    }
}
