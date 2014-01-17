using CRC.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 DoubleKeyDictionaryTest 的测试类，旨在
    ///包含所有 DoubleKeyDictionaryTest 单元测试
    ///</summary>
    [TestClass()]
    public class DoubleKeyDictionaryTest
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


        private DoubleKeyDictionary<int, string, string> CreateDictionary()
        {
            DoubleKeyDictionary<int, string, string> map = new DoubleKeyDictionary<int, string, string>();
            map.Add(1, "a", "Z");
            map.Add(2, "b", "X");
            map.Add(3, "c", "S");
            map.Add(4, "d", "C");
            map.Add(5, "e", "R");

            return map;
        }





        [TestMethod()]
        public void DoubleKeyDictionaryConstructorTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.IsNotNullWith();
        }


        [TestMethod()]
        public void AddTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.Add(9, "p", "O");
            map.ContainsKeyOne(9).IsTrue();
            map.ContainsKeyTwo("p").IsTrue();
        }

        [ExpectedException (typeof (ArgumentNullException))]
        public void AddTestArgumentNullException()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.Add(8, "p", null);

            map.Add(0, null, "P");
        }

       

     
        /// <summary>
        ///Clear 的测试
        ///</summary>
        [TestMethod()]
        public void ClearTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.Clear();
            map.Count.AreEqualWith(0);
        }

 
        /// <summary>
        ///ContainsKeyOne 的测试
        ///</summary>
        [TestMethod()]
        public void ContainsKeyOneTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.ContainsKeyOne(1).IsTrue();
            map.ContainsKeyOne(10).IsFalse();
        }

        
        /// <summary>
        ///ContainsKeyTwo 的测试
        ///</summary>
        [TestMethod()]
        public void ContainsKeyTwoTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.ContainsKeyTwo("a").IsTrue();
            map.ContainsKeyTwo("p").IsFalse();
        }

   
        /// <summary>
        ///GetEnumeratorOne 的测试
        ///</summary>
        [TestMethod()]
        public void GetEnumeratorOneTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            IEnumerator<KeyValuePair<int, string>> enumerable = map.GetEnumeratorOne();
        }

      

        [TestMethod()]
        public void GetEnumeratorTwoTest()
        {
            
        }


        [TestMethod()]
        public void GetOneValueTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.GetOneValue(1).AreEqualWith("Z");
            map.GetOneValue(2).AreEqualWith("X");

        }


        [TestMethod()]
        public void GetTwoValueTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.GetTwoValue("a").AreEqualWith("Z");
            map.GetTwoValue("b").AreEqualWith("X");
        }

      

        /// <summary>
        ///RemoveOne 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveOneTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.RemoveOne(1);
            map.ContainsKeyOne(1).IsFalse();

            map.RemoveOne(10);
            map.ContainsKeyOne(10).IsFalse();
        }
       
   
        /// <summary>
        ///RemoveTwo 的测试
        ///</summary>
        [TestMethod()]
        public void RemoveTwoTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.RemoveTwo("a");
            map.ContainsKeyTwo("a").IsFalse();

            map.RemoveTwo("I");
            map.ContainsKeyTwo("I").IsFalse();
        }

        
    
        /// <summary>
        ///SetOneValue 的测试
        ///</summary>
        [TestMethod()]
        public void SetOneValueTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.SetOneValue(1, "P");
            map.GetOneValue(1).AreEqualWith("P");
            map.GetTwoValue("a").AreEqualWith("P");

        }

        /// <summary>
        ///SetTwoValue 的测试
        ///</summary>
        [TestMethod()]
        public void SetTwoValueTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.SetTwoValue("a", "O");
            map.GetTwoValue("a").AreEqualWith("O");
            map.GetOneValue(1).AreEqualWith("O");
        }

      

        /// <summary>
        ///TryGetValue 的测试
        ///</summary>
        [TestMethod()]
        public void TryGetValueTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            string text;
            map.TryGetValue(1, out text).IsTrue();
            text.AreEqualWith("Z");

            map.TryGetValue("a", out text).IsTrue();
            text.AreEqualWith("Z");

            map.TryGetValue(10, out text).IsFalse();
            text.AreEqualWith(null);

            map.TryGetValue("D", out text).IsFalse();
            text.AreEqualWith(null);

        }


   
        /// <summary>
        ///IsReadOnly 的测试
        ///</summary>
        [TestMethod()]
        public void IsReadOnlyTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.IsReadOnly.IsFalse();
        }

        /// <summary>
        ///Count 的测试
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.Count.AreEqualWith(5);

        }

        /// <summary>
        ///KeysOne 的测试
        ///</summary>
        [TestMethod()]
        public void KeysOneTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.KeysOne.Count.AreEqualWith(5);
           
        }

        /// <summary>
        ///KeysTwo 的测试
        ///</summary>
        [TestMethod()]
        public void KeysTwoTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();

            map.KeysTwo.Count.AreEqualWith(5);
        }

        /// <summary>
        ///Values 的测试
        ///</summary>
        [TestMethod()]
        public void ValuesTest()
        {
            DoubleKeyDictionary<int, string, string> map = CreateDictionary();
            map.Values.Count.AreEqualWith(5);
        }
    }
}
