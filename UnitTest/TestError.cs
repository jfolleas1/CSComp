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

                try
                {
                    file = new FileStream(sourceFileName, FileMode.Open);
                    scn = new Scanner(file);
                    parser = new Parser(scn);
                    parser.montage.AddSymboleFromFile(@"C:\Users\j.folleas\Desktop\settings\DataStructur.txt");

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
            resListError.Add(new Error(ErrorType.UNKNOW_VARIABLE,"titi", 3, 9));
            bool test = (myListError.Count == 2);
            test &= (resListError.First().Equals(myListError.First()));
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
            resListError.Add(new Error(ErrorType.INCOMPATIBLE_AFFECTATION, "titi ( attendue : STRING, retourner : NUMERICALE )", 3));
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
