using CRC.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 SortedSplitListTest 的测试类，旨在
    ///包含所有 SortedSplitListTest 单元测试
    ///</summary>
    [TestClass()]
    public class SortedSplitListTest
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

        private static SortedSplitList<TestObject> GetSortedSplitListSortedById()
        {
            SortedSplitList<TestObject> sortedSplitList = new SortedSplitList<TestObject>(new CompareById());
            var obj1 = new TestObject() { Id = 1 };
            var obj2 = new TestObject() { Id = 2 };
            var obj3 = new TestObject() { Id = 3 };
            var obj4 = new TestObject() { Id = 4 };
            sortedSplitList.Add(obj2);
            sortedSplitList.Add(obj4);
            sortedSplitList.Add(obj1);
            sortedSplitList.Add(obj3);
            return sortedSplitList;
        }

        public void TestAddItem()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            Assert.AreEqual(4, sortedSplitListSortedById.Count);
            int i = 1;
            foreach (var testObject in sortedSplitListSortedById)
            {
                Assert.AreEqual(i++, testObject.Id);
            }
        }


        /// <summary>
        ///Clear 的测试
        ///</summary>
        public void ClearTestHelper<T>()
        {
            IComparer<T> defaultComparer = null; // TODO: 初始化为适当的值
            int deepness = 0; // TODO: 初始化为适当的值
            SortedSplitList<T> target = new SortedSplitList<T>(defaultComparer, deepness); // TODO: 初始化为适当的值
            target.Clear();
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void ClearTest()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            // Act
            sortedSplitListSortedById.Clear();
            //Asset
            Assert.AreEqual(0, sortedSplitListSortedById.Count);  
        }


        [TestMethod()]
        public void TestRetriveExistingItem()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            // Act
            var obj = sortedSplitListSortedById.Retrieve(new TestObject() { Id = 3 });
            //Asset
            Assert.AreEqual(3, obj.Id);
        }


        [TestMethod]
        public void TestRetrieveNotExistingItem()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            // Act
            var obj = sortedSplitListSortedById.Retrieve(new TestObject() { Id = 10 });
            //Asset
            Assert.IsNull(obj);
        }

        [TestMethod]
        public void TestBinarySearchFound()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            int index = sortedSplitListSortedById.BinarySearch(new TestObject() { Id = 2 });
            Assert.AreEqual(1, index);
        }

        [TestMethod]
        public void TestBinarySearchHightNotFound()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            int index = sortedSplitListSortedById.BinarySearch(new TestObject() { Id = 8 });
            Assert.AreEqual(-5, index);
        }

        [TestMethod]
        public void TestBinarySearchLowNotFound()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            int index = sortedSplitListSortedById.BinarySearch(new TestObject() { Id = 0 });
            Assert.AreEqual(-1, index);
        }

        [TestMethod]
        public void TestPartiallyEnumerate()
        {
            var sortedSplitList = new SortedSplitList<TestObject>(new CompareByDateId());

            sortedSplitList.Add(new TestObject() { Id = 1, Date = DateTime.Parse("01/01/2003") });
            sortedSplitList.Add(new TestObject() { Id = 2, Date = DateTime.Parse("01/01/2003") });
            sortedSplitList.Add(new TestObject() { Id = 3, Date = DateTime.Parse("01/01/2003") });
            sortedSplitList.Add(new TestObject() { Id = 4, Date = DateTime.Parse("01/01/2003") });
            sortedSplitList.Add(new TestObject() { Id = 5, Date = DateTime.Parse("01/02/2003") });
            sortedSplitList.Add(new TestObject() { Id = 6, Date = DateTime.Parse("01/02/2003") });
            sortedSplitList.Add(new TestObject() { Id = 7, Date = DateTime.Parse("01/02/2003") });
            sortedSplitList.Add(new TestObject() { Id = 8, Date = DateTime.Parse("01/03/2003") });
            sortedSplitList.Add(new TestObject() { Id = 9, Date = DateTime.Parse("01/03/2003") });

            Assert.AreEqual(4, sortedSplitList.PartiallyEnumerate(new TestObject() { Date = DateTime.Parse("01/01/2003") }, new CompareByDate()).Count());
            Assert.AreEqual(3, sortedSplitList.PartiallyEnumerate(new TestObject() { Date = DateTime.Parse("01/02/2003") }, new CompareByDate()).Count());
            Assert.AreEqual(2, sortedSplitList.PartiallyEnumerate(new TestObject() { Date = DateTime.Parse("01/03/2003") }, new CompareByDate()).Count());


            foreach (var testObject in sortedSplitList.PartiallyEnumerate(new TestObject() { Date = DateTime.Parse("01/01/2003") }, new CompareByDate()))
                Console.WriteLine(testObject.Id);

        }             
       
 
        [TestMethod()]
        public void RemoveTest()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();

            // Act
            sortedSplitListSortedById.Remove(new TestObject() { Id = 3 });
            sortedSplitListSortedById.Remove(new TestObject() { Id = 1 });
            sortedSplitListSortedById.Remove(new TestObject() { Id = 4 });

            // Asert
            Assert.AreEqual(1, sortedSplitListSortedById.Count);
            Assert.AreEqual(2, sortedSplitListSortedById[0].Id);
        }

  
        [TestMethod()]
        public void RemoveAllTest()
        {
            var sortedSplitListSortedById = GetSortedSplitListSortedById();
            // Act
            sortedSplitListSortedById.RemoveAll(a => a.Id % 2 == 0);
            //Asset
            Assert.AreEqual(2, sortedSplitListSortedById.Count);
            Assert.AreEqual(1, sortedSplitListSortedById[0].Id);
            Assert.AreEqual(3, sortedSplitListSortedById[1].Id);
        }


    }
}
