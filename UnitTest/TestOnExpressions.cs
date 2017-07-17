using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;




namespace UnitTest
{

    [TestClass]
    public class TestOnExpressions
    {
        [TestMethod]
        public void TestMultipleExpressions()
        {
            MainTestOnExpr("TestMultipleExpressions");
        }

        [TestMethod]
        public void TestDBVarInExpressions()
        {
            MainTestOnExpr("TestDBVarInExpressions");
        }


        [TestMethod]
        public void TestVarFromStructInExpressions()
        {
            MainTestOnExpr("TestVarFromStructInExpressions");
        }

        
        public void MainTestOnExpr(string fileName)
        {

            bool sameFiles = true;

            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\src\" + fileName + ".txt";
            string trgHtmlFilePath = @"C:\Users\j.folleas\Desktop\Tests\trg\" + fileName + ".html";
            string trgJSFilePath = @"C:\Users\j.folleas\Desktop\Tests\trg\" + fileName + ".js";
            string resHtmlFilePath = @"C:\Users\j.folleas\Desktop\Tests\res\" + fileName + ".html";
            string resJSFilePath = @"C:\Users\j.folleas\Desktop\Tests\res\" + fileName + ".js";
            string[] args = { srcFilePath, trgHtmlFilePath, trgJSFilePath };

            sameFiles &= MainTest.TestMain(srcFilePath, trgHtmlFilePath, trgJSFilePath);
            try
            {   // Open the text file using a stream reader.
                String linetrgHtml;
                String lineresHtml;
                String linetrgJS;
                String lineresJS;

                using (StreamReader srTrgHtml = new StreamReader(trgHtmlFilePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    linetrgHtml = srTrgHtml.ReadToEnd();
                }
                using (StreamReader srResHtml = new StreamReader(resHtmlFilePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    lineresHtml = srResHtml.ReadToEnd();
                }
                using (StreamReader srTrgJS = new StreamReader(trgJSFilePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    linetrgJS = srTrgJS.ReadToEnd();
                }
                using (StreamReader srResJS = new StreamReader(resJSFilePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    lineresJS = srResJS.ReadToEnd();
                }
                //sameFiles &= (linetrgHtml == lineresHtml);
                sameFiles &= (linetrgJS == lineresJS);
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }


            Assert.AreEqual(true, sameFiles);
        }
    }
}
