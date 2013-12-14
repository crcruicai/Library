using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 RandomExtensionTest 的测试类，旨在
    ///包含所有 RandomExtensionTest 单元测试
    ///</summary>
    [TestClass()]
    public class RandomExtensionTest
    {

        private static Random _Rnd;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            _Rnd = new Random();

        }
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
        ///NextBool 的测试
        ///</summary>
        [TestMethod()]
        public void NextBoolTest()
        {
            Random random = _Rnd; 
            bool expected = false; 
            bool actual;
            actual = RandomExtension.NextBool(random);
            if (actual)
            {
                actual.IsTrue();
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
        }

        /// <summary>
        ///NextBytes 的测试
        ///</summary>
        [TestMethod()]
        public void NextBytesTest()
        {
            Random random = _Rnd; 
            int length = 8; 
           
            byte[] actual;
            actual = RandomExtension.NextBytes(random, length);

            actual.Length.AreEqualWith(length);
        }

        /// <summary>
        ///NextEnum 的测试
        ///</summary>
        public void NextEnumTestHelper<T>()
            where T : struct
        {
            Random random = null; 
            T expected = new T(); 
            T actual;
            actual = RandomExtension.NextEnum<T>(random);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void NextEnumTest()
        {
            Assert.Inconclusive("没有找到能够满足 T 的类型约束的相应类型参数。请以适当的类型参数来调用 NextEnumTestHelper<T>()。");
        }

        /// <summary>
        ///NextItem 的测试
        ///</summary>
        public void NextItemTestHelper<T>()
        {
            Random random = null; 
            IList<T> etor = null; 
            T expected = default(T); 
            T actual;
            actual = RandomExtension.NextItem<T>(random, etor);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void NextItemTest()
        {
            NextItemTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///NextItem 的测试
        ///</summary>
        public void NextItemTest1Helper<T>()
        {
            Random random = null; 
            T[] array = null; 
            T expected = default(T); 
            T actual;
            actual = RandomExtension.NextItem<T>(random, array);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void NextItemTest1()
        {
            NextItemTest1Helper<GenericParameterHelper>();
        }

        /// <summary>
        ///NextNumber 的测试
        ///</summary>
        [TestMethod()]
        public void NextNumberTest()
        {
            Random random = new Random (); 
            int length = 5; 
            string actual;
            actual = RandomExtension.NextNumber(random, length);
            Assert.AreEqual(actual.Length, length);

        }

        /// <summary>
        ///NextString 的测试
        ///</summary>
        [TestMethod()]
        public void NextStringTest()
        {
            Random random = null; 
            int length = 0; 
            string expected = string.Empty; 
            string actual;
            actual = RandomExtension.NextString(random, length);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
