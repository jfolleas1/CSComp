using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class IteratorStr
    {
        public String iteratorName { get; }
        public VariableId listData { get; }
        static List<KeyValuePair<string, string>> contextConcatenation { get; } = new List<KeyValuePair<string, string>>();

        public IteratorStr(String iteratorName, VariableId listData)
        {
            this.iteratorName = iteratorName;
            this.listData = listData;
        }

        static public void PushContexte(string Listname)
        {
            contextConcatenation.Add(new KeyValuePair<string, string>(Listname, Listname.Split('.')[Listname.Split('.').Length -1] + "Index"));
            foreach (KeyValuePair<string, string> item in contextConcatenation)
            {
                Console.Write("[" + item.Key + "->" + item.Value + "]");
            }
            Console.Write("\n");
        }

        static public void PopContexte()
        {
            contextConcatenation.RemoveAt(contextConcatenation.Count - 1);
        }

        static public string GetListofIndexParametters()
        {
            string indexparameter = "";
            int i = 0;
            int indexOfLast = contextConcatenation.Count - 1; // we dont need the last 
                                                              //context beacause its the curent 
                                                              //declaration 
            foreach (KeyValuePair<string, string> item in contextConcatenation)
            {
                if (i != indexOfLast)
                {
                    indexparameter += item.Value + ",";
                    i++;
                }
            }
            return indexparameter;
        }

        public List<Tuple<string, string>> GetListVariableOfIterator(Montage mtg)
        {
            List<Tuple<string, string>> ls = new List<Tuple<string, string>>();
            ls.Add(new Tuple<string, string>(iteratorName, ExpressionType.STRUCT.ToString()));
            foreach (var item in mtg.symboleTabe)
            {
                if (item.Key.StartsWith(listData.name) && item.Key != listData.name)
                {
                   
                    string pattern = listData.name;
                    Regex rgx = new Regex(pattern);
                    string varK = rgx.Replace(item.Key, iteratorName, 1);
                    ls.Add(new Tuple<string, string>(varK, item.Value));
                }
            }
            return ls;
        }
    }
}
