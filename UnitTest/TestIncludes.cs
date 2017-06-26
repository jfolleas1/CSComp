using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class TestIncludes
    {
        [TestMethod]
        public void TestTowIncludes()
        {
            TestHtml.mainTestHtml("TestIncludes");
        }
    }
}
