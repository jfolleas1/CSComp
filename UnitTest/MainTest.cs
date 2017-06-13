using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Analyser;
using RunTime;

namespace UnitTest
{
    class MainTest
    {
        static public void TestMain(string[] args)
        {
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
                    if (montage != null)
                    {
                        montage.WriteInFiles(targetFilehtmlName, targetFileJSName);
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
        }
    }
}
