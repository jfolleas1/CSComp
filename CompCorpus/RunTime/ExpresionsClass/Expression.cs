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
            base.dataType = ComputeExpressionType();
        }

        public Expression(ExpressionSymbole symb, AbstractExpression exp1)
        {
            this.symbole = symb;
            this.expression1 = exp1;
            this.expression2 = null;
            base.dataType = ComputeExpressionType();
        }

        

        public ExpressionType ComputeExpressionType()
        {
            ExpressionType exp1Type = expression1.dataType;
            ExpressionType exp2Type = ExpressionType.INVALIDE;
            if ( expression2 != null)
            {
                exp2Type = expression2.dataType;
            }


            ExpressionType myType = ExpressionType.INVALIDE;
            switch (symbole)
            {
                case ExpressionSymbole.PLUS:
                    if (exp1Type == exp2Type && exp1Type != ExpressionType.BOOL)
                    {
                        myType = exp1Type;
                    }
                    break;
                case ExpressionSymbole.MUL:
                case ExpressionSymbole.MINUS:
                case ExpressionSymbole.DIV:
                    if (exp2Type == ExpressionType.NUMERICALE && exp1Type == ExpressionType.NUMERICALE)
                    {
                        myType = exp1Type;
                    }
                    break;
                case ExpressionSymbole.AND:
                case ExpressionSymbole.OR:
                    if (exp2Type == ExpressionType.BOOL && exp1Type == ExpressionType.BOOL)
                    {
                        myType = ExpressionType.BOOL;
                    }
                    break;
                case ExpressionSymbole.NOT:
                    if (exp1Type == ExpressionType.BOOL)
                    {
                        myType = ExpressionType.BOOL;
                    }
                    break;
                case ExpressionSymbole.EGALE:
                    if (exp1Type == exp2Type)
                    {
                        myType = ExpressionType.BOOL;
                    }
                    break;
                case ExpressionSymbole.INF:
                case ExpressionSymbole.INFEGALE:
                case ExpressionSymbole.SUP:
                case ExpressionSymbole.SUPEGALE:
                    if (exp2Type == ExpressionType.NUMERICALE && exp1Type == ExpressionType.NUMERICALE)
                    {
                        myType = ExpressionType.BOOL;
                    }
                    break;
                case ExpressionSymbole.PARENT:
                    myType = exp1Type;
                    break;
                default:
                    myType = ExpressionType.INVALIDE;
                    break;
            }
            return myType;
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
            string myExpressionInString;
            switch (symbole)
            {
               
                case ExpressionSymbole.PLUS:
                    myExpressionInString = expression1.Write() + "+" + expression2.Write();
                    break;
                case ExpressionSymbole.MUL:
                    myExpressionInString = expression1.Write() + "*" + expression2.Write();
                    break;
                case ExpressionSymbole.DIV:
                    myExpressionInString = expression1.Write() + "/" + expression2.Write();
                    break;
                case ExpressionSymbole.MINUS:
                    myExpressionInString = expression1.Write() + "-" + expression2.Write();
                    break;
                case ExpressionSymbole.AND:
                    myExpressionInString = expression1.Write() + "&&" + expression2.Write();
                    break;
                case ExpressionSymbole.OR:
                    myExpressionInString = expression1.Write() + "||" + expression2.Write();
                    break;
                case ExpressionSymbole.NOT:
                    myExpressionInString = "!" +expression1.Write();
                    break;
                case ExpressionSymbole.EGALE:
                    myExpressionInString = expression1.Write() + "==" + expression2.Write();
                    break;
                case ExpressionSymbole.INF:
                    myExpressionInString = expression1.Write() + "<" + expression2.Write();
                    break;
                case ExpressionSymbole.INFEGALE:
                    myExpressionInString = expression1.Write() + "<=" + expression2.Write();
                    break;
                case ExpressionSymbole.SUP:
                    myExpressionInString = expression1.Write() + ">" + expression2.Write();
                    break;
                case ExpressionSymbole.SUPEGALE:
                    myExpressionInString = expression1.Write() + ">=" + expression2.Write();
                    break;
                case ExpressionSymbole.PARENT:
                    myExpressionInString = "(" + expression1.Write() + ")";
                    break;
                default:
                    Console.WriteLine("Symbole de calcul non reconu");
                    myExpressionInString = "";
                    break;
            }
            return myExpressionInString;
        }
    }
}
