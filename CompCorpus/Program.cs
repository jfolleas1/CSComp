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
            FileStream file = null;
            Scanner scn = null;
            Parser parser = null;
            AbstractExpression program = null;

            try
            {
                file = new FileStream(@"C:\Users\j.folleas\Documents\testGplex\truc\fichierAParser.txt", FileMode.Open);
                scn = new Scanner(file);
                parser = new Parser(scn);
                parser.Parse();

                program = parser.program;

                Console.WriteLine("FIN DE LECTURE DU CODE ");
                Console.WriteLine();

                if (program != null)
                {
                    program.Print();
                    Console.WriteLine();
                }
                
                Console.ReadLine();

            }
            finally
            {
                file.Close();
            }
        }
    }
}
