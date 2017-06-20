using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Choise : Brick
    {
        public string varName { get; }
        public string textOfChoice { get; }
        public List<Proposition> propositionList { get;  }

        public Choise(string varName, string textOfChoice, List<Proposition> propositionList)
        {
            this.varName = varName;
            this.textOfChoice = textOfChoice;
            this.propositionList = propositionList;
        }

        public void Print()
        {
            Console.WriteLine("CHOIX");
            Console.WriteLine(varName);
            Console.WriteLine(textOfChoice);
            int i = 1;
            foreach (Proposition prop in propositionList)
            {
                Console.WriteLine("PROPOSITION"+(i++));
                prop.Print();
            }
        }

        public override string Write()
        {
            string htmlText = "";
            return htmlText;
        }
    }
}
