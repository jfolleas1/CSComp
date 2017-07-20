using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime
{
    public enum ExpressionSymbole
    {
        PLUS,
        MUL,
        DIV,
        MINUS,
        AND,
        OR,
        NOT,
        EGALE,
        NOTEGALE,
        INF,
        INFEGALE,
        SUP,
        SUPEGALE,
        PARENT,
    };

    public class Expression : AbstractExpression
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
                    if (exp2Type == ExpressionType.NOMBRE && exp1Type == ExpressionType.NOMBRE)
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
                case ExpressionSymbole.NOTEGALE:
                    if (exp1Type == exp2Type)
                    {
                        myType = ExpressionType.BOOL;
                    }
                    break;
                case ExpressionSymbole.INF:
                case ExpressionSymbole.INFEGALE:
                case ExpressionSymbole.SUP:
                case ExpressionSymbole.SUPEGALE:
                    if (exp2Type == ExpressionType.NOMBRE && exp1Type == ExpressionType.NOMBRE)
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

            // If one of the two expression has the unknowvar type  
            // than we concider the operation correct
            if (exp1Type == ExpressionType.UNKNOWVAR)
            {
                myType = exp2Type;
            }
            if (exp2Type == ExpressionType.UNKNOWVAR)
            {
                myType = exp1Type;
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
                case ExpressionSymbole.MUL:
                case ExpressionSymbole.DIV:
                case ExpressionSymbole.MINUS:
                case ExpressionSymbole.AND:
                case ExpressionSymbole.OR:          
                case ExpressionSymbole.EGALE:
                case ExpressionSymbole.NOTEGALE:
                case ExpressionSymbole.INF:
                case ExpressionSymbole.INFEGALE:
                case ExpressionSymbole.SUP:
                case ExpressionSymbole.SUPEGALE:
                    myExpressionInString = expression1.Write() + SymboleToString() + expression2.Write();
                    break;
                case ExpressionSymbole.PARENT:
                    myExpressionInString = "(" + expression1.Write() + ")";
                    break;
                case ExpressionSymbole.NOT:
                    myExpressionInString = "!" + expression1.Write();
                    break;
                default:
                    Console.WriteLine("Symbole de calcul non reconu");
                    myExpressionInString = "";
                    break;
            }
            return myExpressionInString;
        }

        public override string WriteForCondition()
        {
            string myExpressionInString;
            switch (symbole)
            {

                case ExpressionSymbole.PLUS:
                case ExpressionSymbole.MUL:
                case ExpressionSymbole.DIV:
                case ExpressionSymbole.MINUS:
                case ExpressionSymbole.AND:
                case ExpressionSymbole.OR:
                case ExpressionSymbole.EGALE:
                case ExpressionSymbole.NOTEGALE:
                case ExpressionSymbole.INF:
                case ExpressionSymbole.INFEGALE:
                case ExpressionSymbole.SUP:
                case ExpressionSymbole.SUPEGALE:
                    myExpressionInString = expression1.WriteForCondition() + SymboleToString() + expression2.WriteForCondition();
                    break;
                case ExpressionSymbole.NOT:
                    myExpressionInString = "!" + expression1.WriteForCondition();
                    break;
                case ExpressionSymbole.PARENT:
                    myExpressionInString = "(" + expression1.WriteForCondition() + ")";
                    break;
                default:
                    Console.WriteLine("Symbole de calcul non reconu");
                    myExpressionInString = "";
                    break;
            }
            return myExpressionInString;
        }

        public string SymboleToString()
        {
            switch (symbole)
            {
                case ExpressionSymbole.PLUS:
                   return "+";
                case ExpressionSymbole.MUL:
                    return "*";
                case ExpressionSymbole.DIV:
                    return "/";
                case ExpressionSymbole.MINUS:
                   return "-";
                case ExpressionSymbole.AND:
                    return "&&";
                case ExpressionSymbole.OR:
                    return "||";
                case ExpressionSymbole.NOT:
                    return "!";
                case ExpressionSymbole.EGALE:
                    return "==";
                case ExpressionSymbole.NOTEGALE:
                    return "!=";
                case ExpressionSymbole.INF:
                    return "<";
                case ExpressionSymbole.INFEGALE:
                    return "<=";
                case ExpressionSymbole.SUP:
                    return ">";
                case ExpressionSymbole.SUPEGALE:
                    return ">=";
                default:
                    Console.WriteLine("Symbole de calcul non reconu");
                    return "";
            }
        }
    }
}
