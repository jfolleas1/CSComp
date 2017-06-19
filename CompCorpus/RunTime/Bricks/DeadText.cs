using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class DeadText : Brick
    {
        public string text { get; }
        private bool paragraphOpen { get; set; }
        public DeadText(string text, bool paragraphOpen)
        {
            this.text = text;
            this.paragraphOpen = paragraphOpen;
        }

        public override string Write()
        {
            string htmlText = "";
            if(!paragraphOpen)
            {
                htmlText += " <p>";
            }
            htmlText += GetTextParse();
            return htmlText;
        }

        private string GetTextParse()
        {
            Dictionary<string, string> rules = new Dictionary<string, string>();
            rules.Add(@"\$nouvligne", @"<br/>");
            rules.Add(@"\$nouvparag", @"</p> <p>");
            string parsedText = this.text;
            foreach (var item in rules)
            {
                Regex regexText = new Regex(item.Key);
                parsedText = regexText.Replace(parsedText,item.Value);
            }
            return parsedText;
        }
    }
}
