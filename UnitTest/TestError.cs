using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using CompCorpus.RunTime.error;
using CompCorpus.Analyzer;
using CompCorpus.RunTime;

namespace UnitTest
{
    [TestClass]
    public class TestError
    {
        public List<Error> TestErrorMain(string[] args)
        {
            if (args.Length == 3)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];
                List<Error> myErrorList = new List<Error>();

                FileStream file = null;
                Scanner scn = null;
                Parser parser = null;
                Montage montage = null;

                // Read the document to do the precompiling phase
                string fileForPreCompiling = "";
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(sourceFileName))
                    {
                        // Read the stream to a string, and write the string to the console.
                        fileForPreCompiling = sr.ReadToEnd();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }

                try
                {
                    file = new FileStream(sourceFileName, FileMode.Open);
                    scn = new Scanner(file);
                    parser = new Parser(scn);
                    parser.montage.AddSymboleFromFile(@"C:\Users\j.folleas\Desktop\settings\DataStructur.txt");
                    parser.montage.AddSymboleFromPreCompile(fileForPreCompiling);

                    parser.Parse();

                    montage = parser.montage;
                    montage.PrintErrors();
                    myErrorList = montage.errorList;
                }
                catch (FileNotFoundException exnotfound)
                {
                    Console.WriteLine(exnotfound.Message);
                }
                finally
                {
                    file.Close();
                }
                return myErrorList;
            }
            return new List<Error>();
        }


        [TestMethod]
        public void TestErrorInvalideOperation()
        {
            string fileName = "TestErrorInvalideOperation";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.INVALIDE_OPERATION, "toto", 3, 0));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorInvalideExpressionForCondition()
        {
            string fileName = "TestErrorInvalideExpressionForCondition";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.INVALID_CONDITION_EXPR, "", 5,4));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorNotListUseInIteration()
        {
            string fileName = "TestErrorNotListUseInIteration";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.NOTLIST_USE_FOR_ITERATION, "toto", 8, 18));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorDoubleDeclaration()
        {
            string fileName = "TestErrorDoubleDeclaration";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.DOUBLE_DECLARATION, "toto", 0, 0));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorInvalideType()
        {
            string fileName = "TestErrorInvalideType";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.INVALIDE_TYPE, "STRINGG", 2, 6));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorunknowVar()
        {
            string fileName = "TestErrorunknowVar";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.UNKNOW_VARIABLE,"titi", 3, 12));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorunknowVarInDataStruct()
        {
            string fileName = "TestErrorunknowVarInDataStruct";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.UNKNOW_VARIABLE, "last", 10, 11));
            resListError.Add(new Error(ErrorType.UNKNOW_VARIABLE, "titi", 12, 19));
            bool test = (myListError.Count == 2);
            test &= (resListError[0].Equals(myListError[0]));
            test &= (resListError[1].Equals(myListError[1]));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorIncompatibleAffectation()
        {
            string fileName = "TestErrorIncompatibleAffectation";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.INCOMPATIBLE_AFFECTATION, "titi (attendue : TEXTE, retourné : NOMBRE)", 3));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestErrorIncompatibleAffectationWithCond()
        {
            string fileName = "TestErrorIncompatibleAffectationWithCond";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.INCOMPATIBLE_AFFECTATION, "maVar (attendue : NOMBRE, retourné : TEXTE)", 9, 2));
            bool test = (myListError.Count == 1);
            test &= (resListError.First().Equals(myListError.First()));
            Assert.AreEqual(true, test);
        }

        [TestMethod]
        public void TestMultipleError()
        {
            string fileName = "TestMultipleError";
            string srcFilePath = @"C:\Users\j.folleas\Desktop\Tests\srcWithError\" + fileName + ".txt";
            string[] args = { srcFilePath, "", "" };
            List<Error> myListError = TestErrorMain(args);
            List<Error> resListError = new List<Error>();
            resListError.Add(new Error(ErrorType.INVALIDE_OPERATION, "toto", 3, 0));
            resListError.Add(new Error(ErrorType.INVALIDE_OPERATION, "titi", 4, 0));
            bool test = (myListError.Count == 2);
            test &= (resListError[0].Equals(myListError[0]));
            test &= (resListError[1].Equals(myListError[1]));
            Assert.AreEqual(true, test);
        }
    }
}
