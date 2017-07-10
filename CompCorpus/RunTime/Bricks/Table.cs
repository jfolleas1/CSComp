using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class Table : Brick
    {
        
        public List<List<KeyValuePair<long, List<Brick>>>> listOfRowAndCel { get; }

        public Table(List<List<KeyValuePair<long, List<Brick>>>> listOfRowAndCel)
        {
            this.listOfRowAndCel = listOfRowAndCel;
        }

        public void Print()
        {
            Console.WriteLine("Table");
            foreach (List<KeyValuePair<long, List<Brick>>> llbk in listOfRowAndCel)
            {
                Console.WriteLine("\t Row:");
                foreach (KeyValuePair<long, List<Brick>> lbk in llbk)
                {
                    Console.WriteLine("\t\t Cel:" + lbk.Key + ":");
                    foreach (Brick bk in lbk.Value)
                    {
                        Console.WriteLine("\t\t\t" + bk.Write());
                    }
                }
            }
        }

        public override string Write()
        {
            string res = "";
           res += "<table>\n";
            foreach(List < KeyValuePair<long, List<Brick>> > llbk in listOfRowAndCel)
            {
                res += "\t<tr>\n";
                foreach (KeyValuePair<long, List<Brick>> lbk in llbk)
                {
                    res += "\t\t<td width="+ lbk.Key +"%>\n";
                    foreach (Brick bk in lbk.Value)
                    {
                       res += ("\t\t\t" + bk.Write() + "\n");
                    }
                    res += "\t\t</td>\n";
                }
                res += "\t</tr>\n";
            }
            res += "</table>\n";
            return res;

        }
    }
}
