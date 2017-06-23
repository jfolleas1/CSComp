using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompCorpus.RunTime
{
    static public class PreProcessor
    {
        static public void AddIncludes(string fileName)
        {
            string includePattern = @"$include *\w+\.\w+;";
            MatchCollection includesMatches;
            Regex includeRegex = new Regex(includePattern);
            includesMatches = propositionRegex.Matches(file);
            // Iterate matches
            for (int ctr = 0; ctr < propositionMatches.Count; ctr++)
            {
            }
    }
}
