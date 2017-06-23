using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Option : Brick

    {
        public string varName { get; }
        public string textOfOption { get; }
        public List<Brick> brickList { get; }

        public Option(string varName, string textOfOption, List<Brick> brickList)
        {
            this.varName = varName;
            this.textOfOption = textOfOption;
            this.brickList = brickList;
        }

        public void Print()
        {
            Console.WriteLine("Option");
            Console.WriteLine(varName);
            Console.WriteLine(textOfOption);
            int i = 1;
            foreach (Brick bk in brickList)
            { 
                Console.WriteLine(i++ + " : " + bk.Write());
            }
        }

        public override string Write()
        {
            throw new NotImplementedException("comming soon");
        }
    }
}