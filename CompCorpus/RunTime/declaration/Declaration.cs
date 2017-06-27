using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompCorpus.RunTime;

namespace CompCorpus.RunTime.declaration
{

    public class Declaration
    {
        public string name { get; }
        public ExpressionType type { get; }

        public Declaration(string name, string typeString)
        {
            this.name = name;
            type = GetTypeFromString(typeString);
        }

        public Declaration(string name, ExpressionType type)
        {
            this.name = name;
            this.type = type;
        }

        private ExpressionType GetTypeFromString(string typeString)
        {
            ExpressionType myType = ExpressionType.INVALIDE;
            if (!Enum.TryParse(typeString, out myType) || (typeString == "STRUCT") || (typeString == "LISTSTRUCT"))
                myType = ExpressionType.INVALIDE;
            return myType;
        }

        public virtual string Write(bool notInStruct = true, int nbTab = 0)
        {
            string text = "";
            if (notInStruct)
            {
                text = "$scope." + name + "; // de type : " + type.ToString();
            }
            else
            {
                for (int i = 0; i < nbTab; i++)
                {
                    text += "\t";
                }
                text += name + ":" + GetDefaultValueForType() + ",";
            }
            return text;
        }

        private string GetDefaultValueForType()
        {
            //miss exeption 
            string defaultValue = "";
            switch (this.type)
            {
                case ExpressionType.INVALIDE:
                    break;
                case ExpressionType.NUMBER:
                    defaultValue = "0";
                    break;
                case ExpressionType.STRING:
                    defaultValue = "\"\"";
                    break;
                case ExpressionType.BOOL:
                    defaultValue = "true";
                    break;
                default:
                    break;
            }
            return defaultValue;
        }

        public virtual List<Tuple<string, string>> GetSymboles(string baseSymbole = "")
        {
            baseSymbole += "." + name;
            List<Tuple<string, string>> ls = new List<Tuple<string, string>>();
            ls.Add(new Tuple<string, string>(baseSymbole, type.ToString()));
            return ls;
        }

        public virtual string GetAddNRemoveFunction()
        {
            return "";
        }
    }
}
