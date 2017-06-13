using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTime
{
    
    class Declaration
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
            if (!Enum.TryParse(typeString, out myType))
            {
                Console.WriteLine("message d'erreru pour invalide type a le declaration");
            }
   
            return myType;
        }
    }
}
