using System;
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
        public VariableCall(string text, bool local)
        {
            this.text = text;
            this.local = local;
        }

        public override string Write()
        {
            string htmlText = "{{" + text;
            if (local)
            {
                htmlText += "()";
            }
            htmlText += "}}";
            return htmlText;
        }

        
    }
}
