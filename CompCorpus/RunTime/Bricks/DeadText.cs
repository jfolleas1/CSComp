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
        public DeadText(string text)
        {
            this.text = text;
        }

        public override string Write()
        {
            string htmlText = "";
            htmlText += " <p>";
            htmlText += GetTextParse();
            htmlText += " </p> \n";
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
