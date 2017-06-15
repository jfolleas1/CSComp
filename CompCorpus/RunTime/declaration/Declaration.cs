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

        private ExpressionType GetTypeFromString(string typeString)
        {     
            ExpressionType myType = ExpressionType.INVALIDE;
            if(!Enum.TryParse(typeString, out myType) || (typeString == "STRUCT"))
                myType = ExpressionType.INVALIDE;
            return myType;
        }

        public virtual string Write(bool notInStruct = true)
        {
            if (notInStruct)
            {
                return "$scope." + name + "; // de type : " + type.ToString();
            }
            else
            {
                return "$scope." + name + ":" + GetDefaultValueForType() + ",";
            }
        }

        private string GetDefaultValueForType()
        {
            //miss exeption 
            string defaultValue = "";
            switch (this.type)
            {
                case ExpressionType.INVALIDE:
                    break;
                case ExpressionType.NUMERICALE:
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
    }
}
