﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Proposition
    {
        public string textOfChoice { get; }
        public string varName { get; }
        public List<Brick> brickList { get; }
        public List<Affectation> listConditionnalAffectation { get; }

        public Proposition(string varName, string textOfChoice, List<Brick> brickList)
        {
            this.textOfChoice = textOfChoice.Substring(1, (textOfChoice.Length - 2));
            this.varName = varName;
            this.brickList = brickList;
            this.listConditionnalAffectation = new List<Affectation>();
        }

        public Proposition(string varName, string textOfChoice, List<Brick> brickList, List<Affectation> listConditionnalAffectation)
        {
            this.textOfChoice = textOfChoice.Substring(1, (textOfChoice.Length - 2));
            this.varName = varName;
            this.brickList = brickList;
            this.listConditionnalAffectation = listConditionnalAffectation;
        }

            public string textOfChoiceWithoutCote()
        {
            return textOfChoice;
        }

        public void Print()
        {
            Console.WriteLine(textOfChoice);
            foreach (Brick bk in brickList)
            {
                Console.WriteLine(bk.Write());
            }
        }

        public string WriteHtmlOption()
        {
            string htmlText = "<option>";
            htmlText += textOfChoice;
            htmlText += "</option>";
            return htmlText;
        }

        public string Write(string parentName)
        {
            string htmlText = "<span ng-show=\"" + parentName + "=='" + textOfChoice + "'\">";
            foreach (Brick bk in brickList)
            {
                htmlText += bk.Write() + "\n";
            }
            htmlText += "</span>";
            return htmlText;

        }
    }
}
