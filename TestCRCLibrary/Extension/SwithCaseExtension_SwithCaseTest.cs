using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 SwithCaseExtension_SwithCaseTest 的测试类，旨在
    ///包含所有 SwithCaseExtension_SwithCaseTest 单元测试
    ///</summary>
    [TestClass()]
    public class SwithCaseExtension_SwithCaseTest
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
        ///SwithCase`2 构造函数 的测试
        ///</summary>
        public void SwithCaseExtension_SwithCaseConstructorTestHelper<TCase, TOther>()
        {
            TCase value = default(TCase); 
            Action<TOther> action = null; 
            SwithCaseExtension.SwithCase<TCase, TOther> target = new SwithCaseExtension.SwithCase<TCase, TOther>(value, action);
            Assert.Inconclusive("TODO: 实现用来验证目标的代码");
        }

        [TestMethod()]
        public void SwithCaseExtension_SwithCaseConstructorTest()
        {
            SwithCaseExtension_SwithCaseConstructorTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///Action 的测试
        ///</summary>
        public void ActionTestHelper<TCase, TOther>()
        {
            PrivateObject param0 = null; 
            SwithCaseExtension_Accessor.SwithCase<TCase, TOther> target = new SwithCaseExtension_Accessor.SwithCase<TCase, TOther>(param0); 
            Action<TOther> expected = null; 
            Action<TOther> actual;
            target.Action = expected;
            actual = target.Action;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void ActionTest()
        {
            ActionTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///Value 的测试
        ///</summary>
        public void ValueTestHelper<TCase, TOther>()
        {
            PrivateObject param0 = null; 
            SwithCaseExtension_Accessor.SwithCase<TCase, TOther> target = new SwithCaseExtension_Accessor.SwithCase<TCase, TOther>(param0); 
            TCase expected = default(TCase); 
            TCase actual;
            target.Value = expected;
            actual = target.Value;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        [DeploymentItem("CRCLibrary.dll")]
        public void ValueTest()
        {
            ValueTestHelper<GenericParameterHelper, GenericParameterHelper>();
        }
    }
}
