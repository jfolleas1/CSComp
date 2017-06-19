﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class VariableCall : Brick
    {
        public string text { get; }
        private bool local { get; set; }
        string typeString { get; }
        public VariableCall(string text, bool local, string typeString)
        {
            this.text = text;
            this.local = local;
            this.typeString = typeString;
        }

        public override string Write()
        {
           
            string htmlText = "";
            if (local)
            {
                htmlText += " {{" + text + "()}}";
            }
            else
            {
                htmlText += " <input type=\"";
                htmlText += GetHtmlType();
                htmlText += "\" ng-model=\"" +
                    text + "\" onkeypress = \"this.style.width ="+
                    " Math.max( ((this.value.length) * 8 + 16 ), 32) + 'px';\" > ";

            }
            return htmlText;
        }

        private string GetHtmlType()
        {
            if (typeString == "L" + ExpressionType.NUMBER.ToString() ||
                typeString == ExpressionType.NUMBER.ToString())
            {
                return "number";
            }
            else
            {
                return "text";
            }
        }
    }
}
