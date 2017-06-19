using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CompCorpus.Analyzer;
using CompCorpus.RunTime;
using System.Linq;

namespace UnitTest
{
    class MainTest
    {
        static public bool TestMain(string[] args)
        {
            bool filemodified = false ;
            if (args.Length == 3)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];

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

                    if (montage != null && !montage.errorList.Any() )
                    {
                        montage.WriteInFiles(targetFilehtmlName, targetFileJSName);
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
                
            }
            return filemodified;
        }
    }
}
