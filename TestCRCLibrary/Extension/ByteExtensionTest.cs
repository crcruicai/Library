using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 ByteExtensionTest 的测试类，旨在
    ///包含所有 ByteExtensionTest 单元测试
    ///</summary>
    [TestClass()]
    public class ByteExtensionTest
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

        [TestMethod]
        public void ArrayAreEqualTest()
        {
            byte[] oneDimArray = { 1, 2, 3 };

            byte[] oneDimArray1 = { 1, 2, 3 };

            oneDimArray.ArrayAreEqual(oneDimArray1).IsTrue();

            oneDimArray1[2] = 5;
            oneDimArray.ArrayAreEqual(oneDimArray1).IsFalse();

        }

        

        /// <summary>
        ///ClearBit 的测试
        ///</summary>
        [TestMethod()]
        public void ClearBitTest()
        {
            ByteExtension.ClearBit(0xFF, -1)
             .AreEqualWith<byte>(0xFF);

            ByteExtension.ClearBit(0xFF, 0)
              .AreEqualWith<byte>(0xFE);

            ByteExtension.ClearBit(0xFF, 1)
                 .AreEqualWith<byte>(0xFD);

            ByteExtension.ClearBit(0xFF, 2)
               .AreEqualWith<byte>(0xFB);

            ByteExtension.ClearBit(0xFF, 3)
              .AreEqualWith<byte>(0xF7);

            ByteExtension.ClearBit(0xFF, 4)
              .AreEqualWith<byte>(0xEF);

            ByteExtension.ClearBit(0xFF, 5)
              .AreEqualWith<byte>(0xDF);

            ByteExtension.ClearBit(0xFF, 6)
              .AreEqualWith<byte>(0xBF);

            ByteExtension.ClearBit(0xFF, 7)
              .AreEqualWith<byte>(0x7F);


            ByteExtension.ClearBit(0xFF, 8)
              .AreEqualWith<byte>(0xFF);

        }

        /// <summary>
        ///GetBit 的测试
        ///</summary>
        [TestMethod()]
        public void GetBitTest()
        {
            ByteExtension.GetBit(0xFF, 8)
                .IsFalse ();

            ByteExtension.GetBit(0xFF, 7)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 6)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 5)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 4)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 3)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 2)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 1)
               .IsTrue();

            ByteExtension.GetBit(0xFF, 0)
               .IsTrue();

            ByteExtension.GetBit(0xFF, -1)
               .IsFalse();
        }

        /// <summary>
        ///ReverseBit 的测试
        ///</summary>
        [TestMethod()]
        public void ReverseBitTest()
        {
            byte b = 0; 
            int index = 0; 
            byte expected = 0; 
            byte actual;
            actual = ByteExtension.ReverseBit(b, index);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        /// <summary>
        ///SetBit 的测试
        ///</summary>
        [TestMethod()]
        public void SetBitTest()
        {
            ByteExtension.SetBit(0x00, -1)
                .AreEqualWith<byte>(0x00);

            ByteExtension.SetBit(0x00, 0)
               .AreEqualWith<byte>(0x01);

            ByteExtension.SetBit(0x00, 1)
               .AreEqualWith<byte>(0x02);

            ByteExtension.SetBit(0x00, 2)
               .AreEqualWith<byte>(0x04);

            ByteExtension.SetBit(0x00, 3)
               .AreEqualWith<byte>(0x08);

            ByteExtension.SetBit(0x00, 4)
               .AreEqualWith<byte>(0x10);

            ByteExtension.SetBit(0x00, 5)
               .AreEqualWith<byte>(0x20);

            ByteExtension.SetBit(0x00, 6)
               .AreEqualWith<byte>(0x40);

            ByteExtension.SetBit(0x00, 7)
               .AreEqualWith<byte>(0x80);

            ByteExtension.SetBit(0x00, 8)
               .AreEqualWith<byte>(0x00);
        }

        /// <summary>
        ///ToByte 的测试
        ///</summary>
        [TestMethod()]
        public void ToByteTest()
        {

            ByteExtension.ToByte("FF").AreEqualWith<byte>(0xFF);
            ByteExtension.ToByte("0F").AreEqualWith<byte>(0x0F);
            ByteExtension.ToByte("F0").AreEqualWith<byte>(0xF0);
            ByteExtension.ToByte("00").AreEqualWith<byte>(0x00);
            ByteExtension.ToByte("12").AreEqualWith<byte>(0x12);
            ByteExtension.ToByte("F").AreEqualWith<byte>(0x0F);


        }

        /// <summary>
        ///ToBytes 的测试
        ///</summary>
        [TestMethod()]
        public void ToBytesTest()
        {
           
            byte[] data=ByteExtension.ToBytes("00010203");
            data.Length.AreEqualWith(4);

            data = ByteExtension.ToBytes("0x000x010x020x03");
            data.Length.AreEqualWith(4);

            data = ByteExtension.ToBytes("0X000X010X020X03");
            data.Length.AreEqualWith(4);
        }

        /// <summary>
        ///ToBytes 的测试
        ///</summary>
        [TestMethod()]
        public void ToBytesTest1()
        {

            ByteExtension.ToBytes("00 01 02 03", " ").Length.AreEqualWith(4);

            ByteExtension.ToBytes("0x00 0x01 0x02 0x03", " ").Length.AreEqualWith(4);

            ByteExtension.ToBytes("0X00 0X01 0X02 0X03", " ").Length.AreEqualWith(4);

        }

        /// <summary>
        ///ToHex 的测试
        ///</summary>
        [TestMethod()]
        public void ToHexTest()
        {
            ByteExtension.ToHex(0x00).AreEqualWith("00");
            ByteExtension.ToHex(0xFF).AreEqualWith("FF");
            ByteExtension.ToHex(0xAA).AreEqualWith("AA");
          
        }

        /// <summary>
        ///ToHex 的测试
        ///</summary>
        [TestMethod()]
        public void ToHexTest1()
        {
            ByteExtension.ToHex(new byte[] { 1, 2, 3, 4 })
                .AreEqualWith("01020304");
           
        }

        /// <summary>
        ///ToHex 的测试
        ///</summary>
        [TestMethod()]
        public void ToHexTest2()
        {
            IEnumerable<byte> bytes = new byte[] { 1, 2, 3, 4 };
            string Prefix = "0X";
            string split = " ";
            string expected = "0X01 0X02 0X03 0X04";
            string actual;
            actual = ByteExtension.ToHex(bytes, Prefix, split);
            Assert.AreEqual(expected, actual);
           
        }
    }
}
