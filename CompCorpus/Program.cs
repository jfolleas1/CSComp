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

        static public void Main(string[] args)
        {
            if (args.Length == 4)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];
                string dataStructurePath = args[3];

                FileStream file = null;
                Scanner scn = null;
                Parser parser = null;
                Montage montage = null;

                try
                {
                    file = new FileStream(sourceFileName, FileMode.Open);
                    Console.WriteLine(file.Name);
                    scn = new Scanner(file);
                    parser = new Parser(scn);
                    parser.montage.AddSymboleFromFile(dataStructurePath);

                    parser.Parse();

                    montage = parser.montage;
                    Console.WriteLine("FIN DE LECTURE DU CODE ");
                    Console.WriteLine();

                    if (montage != null && !montage.errorList.Any() && !scn.hasErrors)
                    {
                        montage.WriteInFiles(targetFilehtmlName, targetFileJSName);
                        System.Diagnostics.Process.Start(targetFilehtmlName);
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
            Console.ReadLine();
        }
    }
}
