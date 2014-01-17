using CRC.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Forms;

namespace TestCRCLibrary
{
    
    
    /// <summary>
    ///这是 InvokeExtensionTest 的测试类，旨在
    ///包含所有 InvokeExtensionTest 单元测试
    ///</summary>
    [TestClass()]
    public class InvokeExtensionTest
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


        /// <summary>
        ///InvokeIfNeeded 的测试
        ///</summary>
        public void InvokeIfNeededTestHelper<T>()
        {
            Control ctl = new Control();
            Action<string> doit = new Action<string>((item) => ctl.Text = item);
            string args = "Control"; 
            InvokeExtension.InvokeIfNeeded<string>(ctl, doit, args);
          
        }

        [TestMethod()]
        public void InvokeIfNeededTest()
        {
            InvokeIfNeededTestHelper<GenericParameterHelper>();
        }

        /// <summary>
        ///InvokeIfNeeded 的测试
        ///</summary>
        [TestMethod()]
        public void InvokeIfNeededTest1()
        {
            Control ctl = new Control();
            Action doit = new Action(() => ctl.Text = "Contorl");
            InvokeExtension.InvokeIfNeeded(ctl, doit);
        }

        /// <summary>
        ///InvokeIfNeeded 的测试
        ///</summary>
        public void InvokeIfNeededTest2Helper<T, S>()
        {
            Control ctl = new Control();
            Action<string, string> doit = new Action<string, string>((a, b) =>
                {
                    ctl.Text = a;
                    ctl.Tag = b;
                });
            string  arg1 = "";
            string  arg2 = "";
            InvokeExtension.InvokeIfNeeded<string, string>(ctl, doit, arg1, arg2);
        }

        [TestMethod()]
        public void InvokeIfNeededTest2()
        {
            InvokeIfNeededTest2Helper<GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///QueueInvoke 的测试
        ///</summary>
        [TestMethod()]
        public void QueueInvokeTest()
        {
            //Control ctl = new Control();
            //Action doit = new Action(() => ctl.Text = "Contorl");
            //InvokeExtension.QueueInvoke(ctl, doit);
        }

        /// <summary>
        ///QueueInvoke 的测试
        ///</summary>
        public void QueueInvokeTest1Helper<T, S>()
        {
            //Control ctl = new Control();
            //Action<string, string> doit = new Action<string, string>((a, b) =>
            //{
            //    ctl.Text = a;
            //    ctl.Tag = b;
            //});
            //string arg1 = "11";
            //string arg2 = "222";
            //InvokeExtension.QueueInvoke<string, string>(ctl, doit, arg1, arg2);
        }

        [TestMethod()]
        public void QueueInvokeTest1()
        {
            QueueInvokeTest1Helper<GenericParameterHelper, GenericParameterHelper>();
        }

        /// <summary>
        ///QueueInvoke 的测试
        ///</summary>
        public void QueueInvokeTest2Helper<T>()
        {
            //Control ctl = new Control();
            //Action<string> doit = new Action<string>((item) => ctl.Text = item);
            //string args = "Control"; 
            //InvokeExtension.QueueInvoke<string>(ctl, doit, args);
        }

        [TestMethod()]
        public void QueueInvokeTest2()
        {
            QueueInvokeTest2Helper<GenericParameterHelper>();
        }
    }
}
