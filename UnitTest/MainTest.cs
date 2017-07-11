using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CompCorpus.Analyzer;
using CompCorpus.RunTime;
using CompCorpus;
using System.Linq;

namespace UnitTest
{
    class MainTest
    {
        static public bool TestMain(string[] args)
        {
            LogManager.logFilePath = @"C:\Users\j.folleas\Desktop\settings\logs.txt";
            LogManager.EmptyLogs();

            bool filemodified = false ;
            if (args.Length == 3)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];

                string sourceCopiedFileName = sourceFileName + ".comp";
                try
                {
                    File.Copy(sourceFileName, sourceCopiedFileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }

                FileStream file = null;
                Scanner scn = null;
                Parser parser = null;
                Montage montage = null;

                PreProcessor.AddIncludes(sourceCopiedFileName);


                // Read the document to do the precompiling phase
                string fileForPreCompiling = "";
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader(sourceCopiedFileName))
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
                    file = new FileStream(sourceCopiedFileName, FileMode.Open);
                    scn = new Scanner(file);
                    parser = new Parser(scn);
                    parser.montage.AddSymboleFromFile(@"C:\Users\j.folleas\Desktop\settings\DataStruct.txt");
                    parser.montage.dataBasPath = @"C:\Users\j.folleas\Desktop\settings\DataBase.txt";
                    parser.montage.AddSymboleFromPreCompile(fileForPreCompiling);


                    parser.Parse();

                    montage = parser.montage;

                    if (montage != null && !montage.errorList.Any() && !scn.hasErrors)
                    {
                        montage.WriteInFiles(targetFilehtmlName, targetFileJSName);
                        Console.WriteLine("No Error!");
                        filemodified = true;
                    }
                    else
                    {
                        montage.PrintErrors();
                    }

                }
                catch (FileNotFoundException exnotfound)
                {
                    Console.WriteLine(exnotfound.Message);
                }
                finally
                {
                    file.Close();
                }

                try
                {
                    File.Delete(sourceCopiedFileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("The file could not be read:");
                    Console.WriteLine(e.Message);
                }
            }
            LogManager.DisplayLogs();
            return filemodified;
        }
    }
}
