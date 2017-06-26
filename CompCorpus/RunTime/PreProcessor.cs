using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompCorpus.RunTime
{
    static public class PreProcessor
    {
        static public void AddIncludes(string fileName)
        {
            Console.WriteLine("Pre processor add includes");
            //Wee get the directpry path of the source file. The file included are in the same file
            String directoryPath = "";
            for(int i = 0; i<fileName.Split('\\').Length-1; i++)
            {
                directoryPath += fileName.Split('\\')[i] + '\\';
            }

            // We read the copied source file
            string copiedSourceFile = ReadTheTmpSrcFile(fileName);
            

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
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                using (StreamWriter file =
                new StreamWriter(fs, Encoding.UTF8))
                {
                    file.Write(copiedSourceFile);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        private static string ReadTheTmpSrcFile(string fileName)
        {
            string copiedSourceFile = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // Read the stream to a string, and write the string to the console.
                    copiedSourceFile = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return copiedSourceFile;
        }

        static private string  MakeOneInclude(string specificInclude ,string fileToIncludeName, string copiedSourceFile)
        {
           
            //We read the file to include
            string ToIincludeFile = "";
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(fileToIncludeName))
                {
                    // Read the stream to a string, and write the string to the console.
                    ToIincludeFile = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            //And make the changes in the copied source file

            string pattern = '\\'+specificInclude+ " *;";
            Regex rgx = new Regex(pattern);
            string result =  rgx.Replace(copiedSourceFile, ToIincludeFile);

            return result;
        }
    }
}
