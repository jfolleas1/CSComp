using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Condition : Brick
    {
        public AbstractExpression conditionExpression { get; }
        public List<Brick> brickList { get; }

        public Condition(AbstractExpression conditionExpression, List<Brick> brickList)
        {
            this.conditionExpression = conditionExpression;
            this.brickList = brickList;
        }

        public void Print()
        {
            Console.WriteLine("Condition here");
            Console.WriteLine(conditionExpression.WriteForCondition());
            int i = 1;
            foreach (Brick bk in brickList)
            {
                Console.WriteLine(i++ + " : " + bk.Write());
            }
        }

        public override string Write()
        {
            string htmlText = "";
            htmlText += "<span ng-show=\"" + conditionExpression.WriteForCondition() + "\">";
            foreach (Brick bk in brickList)
            {
                htmlText += bk.Write() + "\n";
            }
            htmlText += "</span>";
            return htmlText;
        }
    }
  
}
