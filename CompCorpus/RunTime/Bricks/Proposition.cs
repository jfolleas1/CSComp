using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Proposition
    {
        public string textOfChoice { get; }
        public List<Brick> brickList { get; }

        public Proposition(string textOfChoice, List<Brick> brickList)
        {
            this.textOfChoice = textOfChoice;
            this.brickList = brickList;
        }

        public void Print()
        {
            Console.WriteLine(textOfChoice);
            foreach (Brick bk in brickList)
            {
                Console.WriteLine(bk.Write());
            }
        }

        public void Write()
        {
            throw new NotImplementedException();
        }
    }
}
