using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.declaration
{
    public class DeclarationStruct : Declaration
    {
        static string contextConcatenation { get; set; }
        List<Declaration> declarationList { get; }
        string itemGetterPath { get; }
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
            if (type == ExpressionType.LISTSTRUCT)
            {
                ls.Add(new Tuple<string, string>("nombreDe_" + baseSymbole, "L"+ExpressionType.NOMBRE.ToString()));
            }
            foreach (Declaration dec in declarationList)
            {
                ls.AddRange(dec.GetSymboles(baseSymbole));
            }
            return ls;
        }

        public override string GetAddNDelFunction()
        {
            string functionAdd = "$scope.add" + this.name + " = function() {\n";
            functionAdd += "$scope." + this.name + ".push({";
            foreach (Declaration dec in declarationList)
            {
                functionAdd += (dec.Write(false, 1) + "\n");
            }
            functionAdd += "\n});\n}";
            string functionDel = "$scope.del" + this.name + " = function(index) {\n";
            functionDel += "$scope." + this.name + ".splice(index, 1); \n}\n";
            string functionCount = "$scope.nombreDe_" + this.name + " = function() {\n";
            functionCount += " return $scope." + this.name + ".length; \n}\n";

            return functionAdd + "\n"+ functionDel +"\n" + functionCount;
        }
    }
}
