using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 SwithCaseExtensionTest 的测试类，旨在
    ///包含所有 SwithCaseExtensionTest 单元测试
    ///</summary>
    [TestClass()]
    public class SwithCaseExtensionTest
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
        ///Case 的测试
        ///</summary>
        public void CaseTestHelper<TCase, TOther>()
            where TCase : IEquatable<T>
        {
            SwithCaseExtension.SwithCase<TCase, TOther> sc = null; 
            TCase option = default(TCase); 
            TOther other = default(TOther); 
            SwithCaseExtension.SwithCase<TCase, TOther> expected = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> actual;
            actual = SwithCaseExtension.Case<TCase, TOther>(sc, option, other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CaseTest()
        {
            Assert.Inconclusive("没有找到能够满足 TCase 的类型约束的相应类型参数。请以适当的类型参数来调用 CaseTestHelper<TCase, TOther>()。");
        }

        /// <summary>
        ///Case 的测试
        ///</summary>
        public void CaseTest1Helper<TCase, TOther>()
            where TCase : IEquatable<T>
        {
            SwithCaseExtension.SwithCase<TCase, TOther> sc = null; 
            Predicate<TCase> predict = null; 
            TOther other = default(TOther); 
            SwithCaseExtension.SwithCase<TCase, TOther> expected = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> actual;
            actual = SwithCaseExtension.Case<TCase, TOther>(sc, predict, other);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CaseTest1()
        {
            Assert.Inconclusive("没有找到能够满足 TCase 的类型约束的相应类型参数。请以适当的类型参数来调用 CaseTest1Helper<TCase, TOther>()。");
        }

        /// <summary>
        ///Case 的测试
        ///</summary>
        public void CaseTest2Helper<TCase, TOther>()
            where TCase : IEquatable<T>
        {
            SwithCaseExtension.SwithCase<TCase, TOther> sc = null; 
            TCase option = default(TCase); 
            TOther other = default(TOther); 
            bool bBreak = false; 
            SwithCaseExtension.SwithCase<TCase, TOther> expected = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> actual;
            actual = SwithCaseExtension.Case<TCase, TOther>(sc, option, other, bBreak);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CaseTest2()
        {
            Assert.Inconclusive("没有找到能够满足 TCase 的类型约束的相应类型参数。请以适当的类型参数来调用 CaseTest2Helper<TCase, TOther>()。");
        }

        /// <summary>
        ///Case 的测试
        ///</summary>
        public void CaseTest3Helper<TCase, TOther>()
            where TCase : IEquatable<T>
        {
            SwithCaseExtension.SwithCase<TCase, TOther> sc = null; 
            Predicate<TCase> predict = null; 
            TOther other = default(TOther); 
            bool bBreak = false; 
            SwithCaseExtension.SwithCase<TCase, TOther> expected = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> actual;
            actual = SwithCaseExtension.Case<TCase, TOther>(sc, predict, other, bBreak);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CaseTest3()
        {
            Assert.Inconclusive("没有找到能够满足 TCase 的类型约束的相应类型参数。请以适当的类型参数来调用 CaseTest3Helper<TCase, TOther>()。");
        }

        /// <summary>
        ///Default 的测试
        ///</summary>
        public void DefaultTestHelper<TCase, TOther>()
        {
            SwithCaseExtension.SwithCase<TCase, TOther> sc = null; 
            TOther other = default(TOther); 
            SwithCaseExtension.Default<TCase, TOther>(sc, other);
            Assert.Inconclusive("无法验证不返回值的方法。");
        }

        [TestMethod()]
        public void DefaultTest()
        {
            DefaultTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///Switch 的测试
        ///</summary>
        public void SwitchTestHelper<TCase, TOther>()
            where TCase : IEquatable<T>
        {
            TCase t = default(TCase); 
            Action<TOther> action = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> expected = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> actual;
            actual = SwithCaseExtension.Switch<TCase, TOther>(t, action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SwitchTest()
        {
            Assert.Inconclusive("没有找到能够满足 TCase 的类型约束的相应类型参数。请以适当的类型参数来调用 SwitchTestHelper<TCase, TOther>()。");
        }

        /// <summary>
        ///Switch 的测试
        ///</summary>
        public void SwitchTest1Helper<TInput, TCase, TOther>()

            where TCase : IEquatable<T>
        {
            TInput t = default(TInput); 
            Func<TInput, TCase> selector = null; 
            Action<TOther> action = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> expected = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> actual;
            actual = SwithCaseExtension.Switch<TInput, TCase, TOther>(t, selector, action);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void SwitchTest1()
        {
            Assert.Inconclusive("没有找到能够满足 TCase 的类型约束的相应类型参数。请以适当的类型参数来调用 SwitchTest1Helper<TInput, TCase, TOther>" +
                    "()。");
        }
    }
}
