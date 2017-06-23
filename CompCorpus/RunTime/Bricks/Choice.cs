using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Choice : Brick
    {
        public string varName { get; }
        public string textOfChoice { get; }
        public List<Proposition> propositionList { get;  }

        public Choice(string varName, string textOfChoice, List<Proposition> propositionList)
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
            htmlText += "<select class=\"pull-left\" ng-model=\"";
            htmlText += varName + "Model";
            htmlText += "\" ng-init=\"" + varName + "Model" + "='" + propositionList.First<Proposition>().textOfChoice;
            htmlText += "'\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"";
            htmlText += textOfChoice.Substring(1, (textOfChoice.Length - 2));
            htmlText += "\" > \n";
            foreach (Proposition pr in propositionList)
            {
                htmlText += pr.WriteHtmlOption() + "\n";
            }
            htmlText += "</select>\n";
            foreach (Proposition pr in propositionList)
            {
                htmlText += pr.Write(varName + "Model") + "\n";
            }
            return htmlText;
        }
    }
}
