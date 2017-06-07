using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTime
{
    enum ExpressionSymbole
    {
        PLUS,
        MUL,
        DIV,
        MINUS,
        AND,
        OR,
        NOT,
        EGALE,
        INF,
        INFEGALE,
        SUP,
        SUPEGALE,
        PARENT,
    };

    class Expression : AbstractExpression
    {
        public ExpressionSymbole symbole { get; }
        public AbstractExpression expression1 { get; }
        public AbstractExpression expression2 { get; }

        public Expression(ExpressionSymbole symb, AbstractExpression exp1, AbstractExpression exp2)
        {
            this.symbole = symb;
            this.expression1 = exp1;
            this.expression2 = exp2;
        }

        public Expression(ExpressionSymbole symb, AbstractExpression exp1)
        {
            Console.WriteLine("Constrction Expression unaire ");
            this.symbole = symb;
            this.expression1 = exp1;
            this.expression2 = null;
        }

        public override void Print(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.Write(symbole.ToString() + "\n");
            this.expression1.Print(level + 1);
            if (this.expression2 != null)
            {
                this.expression2.Print(level + 1);
            }
        }

        public override string Write()
        {
            throw new NotImplementedException();
        }
    }
}
