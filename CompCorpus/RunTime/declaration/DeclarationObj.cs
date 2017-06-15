using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.declaration
{
    public class DeclarationStruct : Declaration
    {
        List<Declaration> declarationList { get; }
        bool isList;
        public DeclarationStruct(string name, List<Declaration> declarationList, bool isList) : base(name, "STRUCT")
        {
            this.declarationList = declarationList;
            this.isList = isList;
        }

        public override string Write(bool notInStruct = true)
        {
            string text = "$scope." + name + " = ";
            if (isList)
            {
                text += "[";
            }
            text += "{ \n";
            foreach (Declaration dec in declarationList)
            {
                text += (dec.Write(false) + "\n");
            }
            text += "}";
            if (isList)
            {
                text += "]";
            }
            text += "\n";
            if (notInStruct)
            {
                text += ";";
            }
            return text;
        }
    }
}
