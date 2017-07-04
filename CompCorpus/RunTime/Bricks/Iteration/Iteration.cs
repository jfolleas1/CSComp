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
        private String listOfParameter { get; set; }

        public Iteration(String iteratorName, VariableId listData, List<Brick> brickList)
        {
            this.iteratorName = iteratorName;
            this.listData = listData;
            this.brickList = brickList;
            this.listOfParameter = IteratorStr.GetListofIndexParametters();

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
            Console.WriteLine("iter : " + listData.name + "-" + listOfParameter);
            string listName = listData.name.Split('.')[listData.name.Split('.').Length - 1];

            string htmlText = "";
            htmlText += "<span ng-repeat=\"(" + listName + "Index," + iteratorName + ") in " + listData.name +"\">";
            foreach (Brick bk in brickList)
            {
                htmlText += bk.Write() + "\n";
            }
            htmlText += "<input type=\"submit\" value=\" - \" ng-click=\"del" + listName + "(" + listOfParameter +
                listName + "Index" + ")\"></span>";
            htmlText += "<input type=\"submit\" class=\"pull-left\" value=\" + \" ng-click=\"add" + listName + "(";
            if (listOfParameter != "")
            {
                //We delete the last comma
                htmlText += listOfParameter.Substring(0, (listOfParameter.Count() - 1));
            }
            htmlText += ")\">";
            return htmlText;
        }
    }
}
