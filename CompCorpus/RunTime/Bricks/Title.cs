using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Title : Brick 
    {
        public int level { get; }
        public string text { get; }

        public Title(string codeKW, string text)
        {
            this.text = text;
            this.level = GetLevel(codeKW);
        }

        private int GetLevel(string codeKW)
        {
            int i = 1;
            Int32.TryParse(codeKW.Substring(6), out i);
            return i;
        }

        public override string Write()
        {
            string htmlText = "";
            htmlText += "<h" + level + "> " +
                text + "</h" + level + ">";
            return htmlText;
        }
    }
}
