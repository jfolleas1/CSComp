using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class IteratorStr
    {
        public String iteratorName { get; }
        public VariableId listData { get; }

        public IteratorStr(String iteratorName, VariableId listData)
        {
            this.iteratorName = iteratorName;
            this.listData = listData;
        }

        public List<Tuple<string, string>> GetListVariableOfIterator(Montage mtg)
        {
            List<Tuple<string, string>> ls = new List<Tuple<string, string>>();
            ls.Add(new Tuple<string, string>(iteratorName, ExpressionType.STRUCT.ToString()));
            foreach (var item in mtg.symboleTabe)
            {
                if (item.Key.Split('.')[0] == listData.name && item.Key != listData.name)
                {
                    string varK = iteratorName;
                    for (int i = 1; i < item.Key.Split('.').Length; i++)
                        varK += "." + item.Key.Split('.')[i];
                    ls.Add(new Tuple<string, string>(varK, item.Value));
                    Console.WriteLine(varK);
                }
            }
            return ls;
        }
    }
}
