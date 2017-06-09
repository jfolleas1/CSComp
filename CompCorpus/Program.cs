using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Analyser;
using RunTime;


namespace CompCorpus
{
   
    public static class Program
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
                Montage program = null;

                try
                {
                    file = new FileStream(sourceFileName, FileMode.Open);
                    scn = new Scanner(file);
                    parser = new Parser(scn);
                    parser.Parse();

                    program = parser.program;
                    Console.WriteLine("FIN DE LECTURE DU CODE ");
                    Console.WriteLine();

                    if (program != null)
                    {
                        program.PrintFutureFiles();
                        program.WriteInFiles(targetFilehtmlName, targetFileJSName);
                        //Console.WriteLine();
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

        static public void Main(string[] args)
        {
            if (args.Length == 3)
            {
                string sourceFileName = args[0];
                string targetFilehtmlName = args[1];
                string targetFileJSName = args[2];

                FileStream file = null;
                Scanner scn = null;
                Parser parser = null;
                Montage program = null;

                try
                {
                    file = new FileStream(sourceFileName, FileMode.Open);
                    Console.WriteLine(file.Name);
                    scn = new Scanner(file);
                    parser = new Parser(scn);
                    parser.Parse();

                    program = parser.program;
                    Console.WriteLine("FIN DE LECTURE DU CODE ");
                    Console.WriteLine();

                    if (program != null)
                    {
                        program.PrintFutureFiles();
                        program.WriteInFiles(targetFilehtmlName, targetFileJSName);
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
