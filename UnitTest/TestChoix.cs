using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Description résumée pour TestChoix
    /// </summary>
    [TestClass]
    public class TestChoix
    {

        [TestMethod]
        public void TestChoixRecurecif()
        {
            TestHtml.mainTestHtml("TestChoixRecurecif");
        }

        [TestMethod]
        public void TestChoixSimple()
        {
            TestHtml.mainTestHtml("TestChoixSimple");
        }
    }
}
