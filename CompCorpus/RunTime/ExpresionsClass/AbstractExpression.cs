using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime
{

    public enum ExpressionType
    {
        INVALIDE,
        NUMBER,
        TEXTE,
        BOOL,
        STRUCT,
        LISTSTRUCT,
        UNKNOWVAR,
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
        public abstract string WriteForCondition();
    }
}
