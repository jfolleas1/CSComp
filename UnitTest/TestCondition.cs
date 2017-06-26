using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Description résumée pour TestCondition
    /// </summary>
    [TestClass]
    public class TestCondition
    {
        [TestMethod]
        public void TestNegCondition()
        {
            TestHtml.mainTestHtml("TestNegCondition");
        }

        [TestMethod]
        public void TestPosCondition()
        {
            TestHtml.mainTestHtml("TestPosCondition");
        }
    }
}
