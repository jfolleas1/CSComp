using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTime
{

    public enum ExpressionType
    {
        NUMERICALE,
        STRING,
        BOOL,
        INVALIDE,
    };

    public abstract class AbstractExpression
    {
        public ExpressionType dataType { get; set; }
        public void Print()
        {
            Print(0);
        }
        public abstract void Print(int level);
        public abstract string Write();

        public void CheckValidity(int line)
        {
            if (this.dataType == ExpressionType.INVALIDE)
            {
                Console.WriteLine("ERREUR: l'expression ligne : " + line + " est invalide. L'une des opération n'est pas autorisée");
            }
        }


    }
}
