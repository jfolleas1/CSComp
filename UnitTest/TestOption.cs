using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Description résumée pour TestOption
    /// </summary>
    [TestClass]
    public class TestOption
    {
        [TestMethod]
        public void TestOptionSimple()
        {
            TestHtml.mainTestHtml("TestOptionSimple");
        }

        [TestMethod]
        public void TestOptionRec()
        {
            TestHtml.mainTestHtml("TestOptionRec");
        }

        [TestMethod]
        public void TestCallOptionVar()
        {
            TestHtml.mainTestHtml("TestCallOptionVar");
        }
    }
}
