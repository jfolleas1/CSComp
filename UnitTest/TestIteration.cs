using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Description résumée pour TestIteration
    /// </summary>
    [TestClass]
    public class TestIteration
    {
        [TestMethod]
        public void TestMultipleIterationOnDB()
        {
            TestHtml.mainTestHtml("TestMultipleIterationOnDB");
        }

        [TestMethod]
        public void TestMultipleIterationOnLocals()
        {
            TestHtml.mainTestHtml("TestMultipleIterationOnLocals");
        }

        [TestMethod]
        public void TestCountFunction()
        {
            TestHtml.mainTestHtml("TestCountFunction");
        }
    }
}
