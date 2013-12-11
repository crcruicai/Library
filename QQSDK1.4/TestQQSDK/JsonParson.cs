using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QQSDK.Systems;


namespace TestQQSDK
{
    [TestClass]
    public class JsonParsonTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            JsonParson parson = new JsonParson();
            string text = "{[][][][][][][][]}";
            string result = parson.Parser(text);

        }
    }
}
