using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Iteration : Brick
    {
        public VariableId iterator { get; }
        public List<Brick> brickList { get; }

        public Iteration(VariableId iterator, List<Brick> brickList)
        {
            this.iterator = iterator;
            this.brickList = brickList;
        }

        public void Print()
        {
            Console.WriteLine("Iteration");
            Console.WriteLine(iterator.WriteForCondition());
          
            int i = 1;
            foreach (Brick bk in brickList)
            {
                Console.WriteLine(i++ + " : " + bk.Write());
            }
        }

        public override string Write()
        {
            throw new NotImplementedException("Comming soon");
            //string htmlText = "";
            //htmlText += "<span ng-show=\"" + conditionExpression.WriteForCondition() + "\">";
            //foreach (Brick bk in brickList)
            //{
            //    htmlText += bk.Write() + "\n";
            //}
            //htmlText += "</span>";
            //return htmlText;
        }
    }
}
