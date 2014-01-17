using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 DictionaryExtensionTest 的测试类，旨在
    ///包含所有 DictionaryExtensionTest 单元测试
    ///</summary>
    [TestClass()]
    public class DictionaryExtensionTest
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

        private Dictionary<int,string> CreateDictionary()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(1, "a");
            dic.Add(2, "b");
            dic.Add(3, "c");
            dic.Add(4, "d");
            dic.Add(5, "e");
            return dic;
        }

        private Dictionary<int, string> CreateDictionary2()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            dic.Add(11, "a");
            dic.Add(21, "b");
            dic.Add(31, "c");
            dic.Add(41, "d");
            dic.Add(5, "e");
            return dic;
        }
       

        [TestMethod()]
        public void AddOrReplaceTest()
        {
            var dic = CreateDictionary();
            dic.AddOrReplace(6, "f");

            dic[6].AreEqualWith("f");

            dic.AddOrReplace(5, "k");
            dic[5].AreEqualWith("k");

                
        }

        

        [TestMethod()]
        public void AddRangeTest()
        {
            var dic = CreateDictionary();
            var dic2=CreateDictionary2 ();

            dic.AddRange(dic2,false);

            dic.Count.AreEqualWith(9);

            dic[5].AreEqualWith("e");
        }

     

        [TestMethod()]
        public void TryAddTest()
        {
            var dic = CreateDictionary();
            
            dic.TryAdd(5, "w");
            dic[5].AreEqualWith("e");
            dic.TryAdd(7, "p");
            dic[7].AreEqualWith("p");

        }
    }
}
