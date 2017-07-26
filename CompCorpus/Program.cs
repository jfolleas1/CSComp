using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CompCorpus.Analyzer;
using CompCorpus.RunTime;
using CompCorpus.RunTime.Bricks;


namespace CompCorpus
{
   
    public static class Program
    {
        public static Montage mainMontage; 

        static public void Main(string[] args)
        {
            if (args.Length == 3)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];

              


                CompileMain(sourceFileName, targetFilehtmlName, targetFileJSName);
                LogManager.DisplayLogs();
            }
            else
            {
                Console.WriteLine("Le programme requière trois parmamètres: sourceFileName, targetFilehtmlName, targetFileJSName");
            }
            Console.ReadLine();
        }


        static public Montage CompileMain(string sourceFileName, string targetFilehtmlName, string targetFileJSName, bool launch = true)
        {
            Montage resultMontage = new Montage();
            LogManager.logFilePath = @"C:\Users\j.folleas\Desktop\settings\logs.txt";
            LogManager.EmptyLogs();

            String directoryPath = "";
            for (int i = 0; i < sourceFileName.Split('\\').Length - 1; i++)
            {
                directoryPath += sourceFileName.Split('\\')[i] + '\\';
            }
            Include.directoryPath = directoryPath;

            // We make a copy of the source file in order to return it 
            // with all include into a single file 
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

            PreProcessor.BDSIPath = @"C:\Users\j.folleas\Desktop\settings\SIDB.txt";
            mainMontage = PreProcessor.GetIncludeSIDB();
            PreProcessor.AddIncludes(sourceCopiedFileName);
            // And of mae all include 

            // Read the document to do the precompiling phase
            string fileForPreCompiling = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(sourceFileName,true))
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
                file = new FileStream(sourceFileName, FileMode.Open);
                scn = new Scanner(file,"GUESS");
                parser = new Parser(scn);
                //Empty the core of the montage, now it only contain the declaration and 
                // the affectations
                mainMontage.SetCoreFromOther(new Montage());
                parser.montage = mainMontage;
                parser.montage.AddSymboleFromPreCompile(fileForPreCompiling);

                parser.Parse();

                montage = parser.montage;
                Console.WriteLine("FIN DE LECTURE DU CODE ");
                Console.WriteLine();

                if (montage != null && !montage.errorList.Any() && !scn.hasErrors && !PreProcessor.includesHasErros)
                {

                    LogManager.AddLog("Compilation principale à réussie");
                    if(launch)
                    {
                        montage.WriteInFiles(targetFilehtmlName, targetFileJSName);
                        System.Diagnostics.Process.Start(targetFilehtmlName);
                    }
                    resultMontage = montage;
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
            return resultMontage;
        }
    }
}
