using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace TestQQSDK
{
   
    /// <summary>
    /// 单元测试 断言扩展方法.
    /// </summary>
    public static class AssertExtension
    {

        /// <summary>
        /// 验证指定的两个泛型类型数据是否相等。如果它们不相等，则断言失败。
        /// <para>采用了Assert.AreEqual方法.</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1">要比较的第一个泛型类型数据。单元测试要求泛型类型数据与 actual 不匹配。</param>
        /// <param name="obj2">要比较的第二个泛型类型数据。这是单元测试生成的泛型类型数据。</param>
        public static void AreEqualWith<T>(this T obj1, T obj2)
        {
            Assert.AreEqual<T>(obj1, obj2);
        }


        /// <summary>
        ///  验证指定的两个泛型类型数据是否不相等。如果它们相等，则断言失败。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj1">要比较的第一个泛型类型数据。单元测试要求泛型类型数据与 actual 不匹配。</param>
        /// <param name="obj2"> 要比较的第二个泛型类型数据。这是单元测试生成的泛型类型数据。</param>
        public static void AreNotEqual<T>(this T obj1, T obj2)
        {
            Assert.AreNotEqual(obj1, obj2);
        }
        /// <summary>
        /// 验证指定的对象是否为 null。如果该对象为 null，则断言失败。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public static void IsNotNull<T>(this T obj) where T : class
        {
            Assert.IsNotNull(obj);
        }
        /// <summary>
        /// 验证指定的对象是否为 null。如果该对象为 null，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="message"></param>
        public static void IsNotNull<T>(this T obj, string message) where T : class
        {
            Assert.IsNotNull(obj, message);
        }
        /// <summary>
        /// 验证指定的对象是否为 null。如果该对象为 null，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要验证的对象不为 null。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
        public static void IsNotNull<T>(this T obj, string message, object[] parameters) where T : class
        {
            Assert.IsNotNull(obj, message, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要验证的对象为 null。</param>
        public static void IsNull<T>(this T obj) where T : class
        {
            Assert.IsNotNull(obj);
        }




        /// <summary>
        /// 验证指定的对象是否为 null。如果该对象不为 null，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要验证的对象为 null。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        public static void IsNull<T>(this T obj, string message) where T : class
        {
            Assert.IsNotNull(obj, message);
        }
        /// <summary>
        /// 验证指定的对象是否为 null。如果该对象不为 null，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">要验证的对象为 null。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
        public static void IsNull<T>(this T obj, string message, object[] parameters) where T : class
        {
            Assert.IsNotNull(obj, message, parameters);
        }
        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。
        /// </summary>
        /// <param name="obj">要验证的条件为 true。</param>
        public static void IsTrue(this bool obj)
        {
            Assert.IsTrue(obj);
        }
        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。如果断言失败，将显示一则消息。
        /// </summary>
        /// <param name="obj">要验证的条件为 true。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        public static void IsTrue(this bool obj, string message)
        {
            Assert.IsTrue(obj, message);
        }
        /// <summary>
        /// 验证指定的条件是否为 true。如果该条件为 false，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <param name="obj">要验证的条件为 true。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
        public static void IsTrue(this bool obj, string message, object[] parameters)
        {
            Assert.IsTrue(obj, message, parameters);
        }






        /// <summary>
        /// 验证指定的条件是否为 false。如果该条件为 true，则断言失败。
        /// </summary>
        /// <param name="obj">要验证的条件为 false。</param>
        public static void IsFalse(this bool obj)
        {
            Assert.IsFalse(obj);
        }
        /// <summary>
        ///  验证指定的条件是否为 false。如果该条件为 true，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <param name="obj">要验证的条件为 false。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        public static void IsFalse(this bool obj, string message)
        {
            Assert.IsFalse(obj, message);
        }
        /// <summary>
        ///  验证指定的条件是否为 false。如果该条件为 true，则断言失败。断言失败时将显示一则消息，并向该消息应用指定的格式。
        /// </summary>
        /// <param name="obj">要验证的条件为 false。</param>
        /// <param name="message">断言失败时显示的消息。在单元测试结果中可以看到此消息。</param>
        /// <param name="parameters">设置 message 格式时使用的参数的数组。</param>
        public static void IsFalse(this bool obj, string message, object[] parameters)
        {
            Assert.IsFalse(obj, message, parameters);
        }




    }
}
