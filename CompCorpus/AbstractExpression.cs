using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTime
{

    enum ExpressionType
    {
        INTEGER,
        FLOAT,
        STRING,
        BOOL,
    };

    public abstract class AbstractExpression
    {
        public void Print()
        {
            Print(0);
        }
        public abstract void Print(int level);
        public abstract string Write(); 

    }
}
