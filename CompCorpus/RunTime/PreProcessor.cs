using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CompCorpus.Analyzer;
using CompCorpus.RunTime.declaration;


namespace CompCorpus.RunTime
{
    static public class PreProcessor
    {
        static public bool includesHasErros { get; set; }
        static public string BDSIPath { get; set; }

        static public Montage GetIncludeSIDB()
        {
            FileStream file = null;
            Scanner scn = null;
            Parser parser = null;
            Montage montageBaseWithSIDB = null;
            try
            {
                file = new FileStream(PreProcessor.BDSIPath, FileMode.Open);
                scn = new Scanner(file);
                parser = new Parser(scn);

                parser.Parse();
                //ici on va rajouter que toute les déclaration du montage vienne de la db 
                foreach (Declaration dec in parser.montage.listOfDeclarations)
                {
                    // Indicate that the declaration come from the dataBase
                    dec.fromDataBase = true;
                }
                //
                //ici on va rajouter que toute les déclaration du montage vienne de la db 
                foreach (Affectation aff in parser.montage.mapOfCalculExpressions.Values)
                {
                    // Indicate that the declaration come from the dataBase
                    aff.fromDataBase = true;
                }
                //
                //
                montageBaseWithSIDB = parser.montage;
            }
            catch (Exception e)
            {
                LogManager.AddLog("Lors de GetIncludeSIDB");
                LogManager.AddLog(e.Message);
            }
            finally
            {
                file.Close();
            }
            return montageBaseWithSIDB;
        }

        static public void AddIncludes(string fileName)
        {
            Console.WriteLine("Pre processor add includes");
            //Wee get the directpry path of the source file. The file included are in the same dir
            String directoryPath = "";
            for(int i = 0; i<fileName.Split('\\').Length-1; i++)
            {
                directoryPath += fileName.Split('\\')[i] + '\\';
            }

            // We read the copied source file
            string copiedSourceFile = ReadFileWithPath(fileName);
            

            //We find the includes instructions
            string includePattern = @"\$include *\w+\.\w+";
            MatchCollection includesMatches;
            Regex includeRegex = new Regex(includePattern);
            includesMatches = includeRegex.Matches(copiedSourceFile);
            // Iterate on includes instructions
            for (int ctr = 0; ctr < includesMatches.Count; ctr++)
            {
                string fileToIncludeName = includesMatches[ctr].Value.Substring("$include".Length, 
                    includesMatches[ctr].Value.Length - "$include".Length).Trim();
                fileToIncludeName = directoryPath + fileToIncludeName;

                copiedSourceFile = MakeOneInclude(includesMatches[ctr].Value, fileToIncludeName, copiedSourceFile);
            }
            WriteTheTmpSrcFile(fileName, copiedSourceFile);
        }

        private static void WriteTheTmpSrcFile(string fileName, string copiedSourceFile)
        {
            try
            {
                File.WriteAllText(fileName, copiedSourceFile, Encoding.UTF8);
            }
            catch (Exception e)
            {
                LogManager.AddLog("In WriteTheTmpSrcFile function :");
                LogManager.AddLog(e.Message);
            }
        }

        private static string ReadFileWithPath(string filePath)
        {
            string fileText = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Read the stream to a string, and write the string to the console.
                    fileText = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                LogManager.AddLog("In ReadFileWithPath function :");
                LogManager.AddLog(e.Message);
            }
            return fileText;
        }

        static private string  MakeOneInclude(string specificInclude ,string fileToIncludePath, string copiedSourceFile)
        {
           
            //We read the file to include
            string toIncludeFile = ReadFileWithPath(fileToIncludePath);

            // We generate the regexs
            string result = "";
            string patternInclude = '\\' + specificInclude + " *;";
            Regex rgxInclude = new Regex(patternInclude);

            string patternSeparator = "(%%)";
            Regex rgxSep = new Regex(patternSeparator);

            //If the include is an inteligente include, it got %%
            if (rgxSep.IsMatch(toIncludeFile))
            {
                // The inteligent include must have 3 parts
                if (rgxSep.Split(toIncludeFile).Length == 5)
                {
                    // We check if the include his well written
                    string sourceFiletoIncludeCopyPath = CompileInclude(fileToIncludePath);
                    string toIncludeFileCopy = ReadFileWithPath(sourceFiletoIncludeCopyPath);
                    string[] substringsIncludeSrcCopy = rgxSep.Split(toIncludeFileCopy);

                    // We integrate it into the main source file
                    string[] substringsRes = rgxSep.Split(copiedSourceFile);

                    string patternTitre = @"\$Titre{[^}]*}";
                    Regex rgxTitre = new Regex(patternTitre);
                    substringsIncludeSrcCopy[0] = rgxTitre.Replace(substringsIncludeSrcCopy[0], "");
                    Match m = Regex.Match(substringsRes[0], patternTitre);
                    substringsRes[0] = rgxTitre.Replace(substringsRes[0], m.Value + "\n" + substringsIncludeSrcCopy[0]);
                    substringsRes[2] = substringsIncludeSrcCopy[2] + substringsRes[2];
                    substringsRes[4] = rgxInclude.Replace(substringsRes[4], substringsIncludeSrcCopy[4]);
                    result = substringsRes[0] + "%%" + substringsRes[2] + "%%" + substringsRes[4];

                    //We delete the copied included source file
                    deleteFile(sourceFiletoIncludeCopyPath);
                }
                else
                {
                    //Display an include error 
                }

            }
            else
            {
                result = rgxInclude.Replace(copiedSourceFile, toIncludeFile);
            }
            return result;
        }

        private static void deleteFile(string FilePath)
        {
            try
            {
                File.Delete(FilePath);
            }
            catch (Exception e)
            {
                LogManager.AddLog(" In deleteFile function:");
                LogManager.AddLog(e.Message);
            }
        }

        static private string CompileInclude(string fileToIncludePath)
        {
            // We got the include file name 
            char[] splitChar = new char[1];
            splitChar[0] = '\\';
            string fileToIncludeName = fileToIncludePath.Split(splitChar)[fileToIncludePath.Split(splitChar).Length-1];
            string sourceCopiedFiletoIncludePath = "";
            LogManager.AddLog("Compilation du fichier inclue : " + fileToIncludeName);

            
            try
            {
                // We make a copie of theincluded file to make recurcive includes
                sourceCopiedFiletoIncludePath = fileToIncludePath + ".comp";
                try
                {
                    File.Copy(fileToIncludePath, sourceCopiedFiletoIncludePath);
                }
                catch (Exception e)
                {
                    LogManager.AddLog("Try to copy files in CompileInclude function :");
                    LogManager.AddLog(e.Message);
                }

                // We recurcivly add include on the included file
                PreProcessor.AddIncludes(sourceCopiedFiletoIncludePath);


                string SourceFiletoIncludeContent = ReadFileWithPath(fileToIncludePath);

                // We check if the included file is valide
                using (Stream toIncludeStream = new FileStream(fileToIncludePath, FileMode.Open))
                {
                    Scanner scn = new Scanner(toIncludeStream);
                    Parser parser = new Parser(scn);
                    parser.montage.SetDeclarationSymboleAffectationFromOther(Program.mainMontage);
                    parser.montage.AddSymboleFromPreCompile(SourceFiletoIncludeContent);
                   
                    parser.Parse();
                    if (parser.montage == null || parser.montage.errorList.Any() || scn.hasErrors)
                    {
                        includesHasErros = true;
                        LogManager.AddLog(parser.montage.WriteErrors());
                    }
                    else
                    {

                        LogManager.AddLog("L'inclusion " + fileToIncludeName + " a été compilée avec succès.");
                    }
                    
                }
            }
            catch (Exception e)
            {
                LogManager.AddLog("In CompileInclude function :");
                LogManager.AddLog(e.Message);
            }

            // We return the path of the file to include, which now do not containt any includes
            return sourceCopiedFiletoIncludePath;
        }
    }
}
