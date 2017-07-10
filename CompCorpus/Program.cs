using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CompCorpus.Analyzer;
using CompCorpus.RunTime;


namespace CompCorpus
{
   
    public static class Program
    {
        static public string logs;

        static public void Main(string[] args)
        {
            if (args.Length == 5)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];
                string dataStructurePath = args[3];
                string dataBasePath = args[4];

                CompileMain(sourceFileName, targetFilehtmlName, targetFileJSName,
                dataStructurePath, dataBasePath);
                LogManager.DisplayLogs();
            }
            Console.ReadLine();
        }


        static public void CompileMain(string sourceFileName, string targetFilehtmlName, string targetFileJSName,
        string dataStructurePath, string dataBasePath, bool launch = true)
        {
            LogManager.logFilePath = @"C:\Users\j.folleas\Desktop\settings\logs.txt";

            LogManager.EmptyLogs();

            string sourceCopiedFileName = sourceFileName + ".comp";
            try
            {
                File.Copy(sourceFileName, sourceCopiedFileName);
            }
            catch (Exception e)
            {
                LogManager.AddLog("The main src file could not be read:");
                LogManager.AddLog(e.Message);
            }


            FileStream file = null;
            Scanner scn = null;
            Parser parser = null;
            Montage montage = null;

            PreProcessor.dataStructeFile = dataStructurePath;
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
                LogManager.AddLog("The main file comp could not be read:");
                LogManager.AddLog(e.Message);
            }

            try
            {
                file = new FileStream(sourceCopiedFileName, FileMode.Open);
                scn = new Scanner(file);
                parser = new Parser(scn);
                parser.montage.AddSymboleFromFile(dataStructurePath);
                parser.montage.dataBasPath = dataBasePath;
                parser.montage.AddSymboleFromPreCompile(fileForPreCompiling);

                parser.Parse();

                montage = parser.montage;
                Console.WriteLine("FIN DE LECTURE DU CODE ");
                Console.WriteLine();

                if (montage != null && !montage.errorList.Any() && !scn.hasErrors && !PreProcessor.includesHasErros)
                {

                    montage.WriteInFiles(targetFilehtmlName, targetFileJSName);
                    LogManager.AddLog("Compilation principale à réussie");
                    if(launch)
                    {
                        System.Diagnostics.Process.Start(targetFilehtmlName);
                    }
            }
                else
                {
                    LogManager.AddLog(montage.WriteErrors());
                }

            }
            catch (FileNotFoundException exnotfound)
            {
                LogManager.AddLog("Lors de l'analyse du source");
                LogManager.AddLog(exnotfound.Message);
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
                Console.WriteLine("When we try to delete the copied main src file:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
