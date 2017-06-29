using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    /// <summary>
    /// Description résumée pour TestAffectationWithCondition
    /// </summary>
    [TestClass]
    public class TestAffectationWithCondition
    {
        
        [TestMethod]
        public void TestAffectationWithConditionOnChoice()
        {
            TestHtml.mainTestHtml("TestAffectationWithConditionOnChoice");
        }
        [TestMethod]
        public void TestAffectationWithConditionOnOption()
        {
            TestHtml.mainTestHtml("TestAffectationWithConditionOnOption");
        }
        [TestMethod]
        public void TestAffectationWithConditionOnCondition()
        {
            TestHtml.mainTestHtml("TestAffectationWithConditionOnCondition");
        }
    }
}
