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
            string htmlText = "";
            htmlText += "<input type=\"checkbox\" class=\"pull-left\" ng-model=\"";
            htmlText += varName + "Model";
            htmlText += "\" data-toggle=\"tooltip\" data-placement=\"top\" title=\"";
            htmlText += textOfOption.Substring(1, (textOfOption.Length - 2));
            htmlText += "\" >\n";
            htmlText += "<span ng-show=\"" + varName + "Model" + "\">";
            foreach (Brick bk in brickList)
            {
                htmlText += bk.Write() + "\n";
            }
            htmlText += "</span>";
            return htmlText;


        }
    }
}