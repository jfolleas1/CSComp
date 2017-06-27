using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Iteration : Brick
    {
        public String iteratorName { get; }
        public VariableId listData { get; }
        public List<Brick> brickList { get; }

        public Iteration(String iteratorName, VariableId listData, List<Brick> brickList)
        {
            this.iteratorName = iteratorName;
            this.listData = listData;
            this.brickList = brickList;
        }

        public void Print()
        {
            Console.WriteLine("Iteration");
            Console.WriteLine(iteratorName);
            Console.WriteLine(listData.WriteForCondition());
          
            int i = 1;
            foreach (Brick bk in brickList)
            {
                Console.WriteLine(i++ + " : " + bk.Write());
            }
        }

        public override string Write()
        {

            string htmlText = "";
            htmlText += "<span ng-repeat=\"" + iteratorName + " in " + listData.name +"\">";
            foreach (Brick bk in brickList)
            {
                htmlText += bk.Write() + "\n";
            }
            htmlText += "</span>";
            return htmlText;
        }
    }
}
