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
        public DeclarationStruct(string name, string typeString, List<Declaration> declarationList ) : base(name, GetTypeFromString(typeString))
        {
            this.declarationList = declarationList;
        }

        static private ExpressionType GetTypeFromString(string typeString)
        {
            ExpressionType myType = ExpressionType.INVALIDE;
            if (typeString == "STRUCT")
            {
                myType = ExpressionType.STRUCT;
            }
            if (typeString == "LISTSTRUCT")
            {
                myType = ExpressionType.LISTSTRUCT;
            }
            return myType;
        }

        public override string Write(bool notInStruct = true, int nbTab = 0)
        {
            string text = "";
            if (notInStruct)
            {
                text += "$scope.";
            }
            for (int i = 0; i < nbTab; i++)
            {
                text += "\t";
            }
            text += name;
            if (notInStruct)
            {
                text += " = ";
            }
            else
            {
                text += " : ";
            }
            if (base.type == ExpressionType.LISTSTRUCT)
            {
                text += "[";
            }
            text += "{ \n";
            foreach (Declaration dec in declarationList)
            {
                text += (dec.Write(false, nbTab+1) + "\n");
            }
            for (int i = 0; i < nbTab; i++)
            {
                text += "\t";
            }
            text += "}";
            if (base.type == ExpressionType.LISTSTRUCT)
            {
                text += "]";
            }
            if (notInStruct)
            {
                text += ";";
            }
            return text;
        }

        public override List<Tuple<string, string>> GetSymboles(string baseSymbole = "")
        {
            bool firstLevel = (baseSymbole == "");
            if(!firstLevel)
            {
                baseSymbole += ".";
            }
            baseSymbole += name;
            List<Tuple<string, string>> ls = new List<Tuple<string, string>>();
            ls.Add(new Tuple<string, string>(baseSymbole, type.ToString()));
            foreach (Declaration dec in declarationList)
            {
                ls.AddRange(dec.GetSymboles(baseSymbole));
            }
            return ls;
        }
    }
}
