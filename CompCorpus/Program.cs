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
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 2)
            {
                string sourceFileName = args[0];
                string targetFileName = args[1];

                FileStream file = null;
                Scanner scn = null;
                Parser parser = null;
                List<Affectation> program = null;

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
                        foreach (Affectation aff in program)
                        {
                            aff.Print();
                        }
                        Console.WriteLine();
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
