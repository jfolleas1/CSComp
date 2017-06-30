using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime
{
    public class Affectation
    {
        public VariableId variableName { get; }
        public AbstractExpression expression { get; }
        public List<Tuple<AbstractExpression, AbstractExpression>> listOfConditionAndExpression{ get; set; }
        public int line { get; }
        public int col { get; }

        public Affectation(VariableId name, AbstractExpression exp, int line = 0, int col = 0)
        {
            this.variableName = name;
            this.expression = exp;
            listOfConditionAndExpression = new List<Tuple<AbstractExpression, AbstractExpression>>();
            this.line = line;
            this.col = col;
        }

        public void Print()
        {
            Console.WriteLine("AFFECTATION");
            Console.WriteLine("Var name : ");
            this.variableName.Print();
            Console.WriteLine("DATA TYPE : " + expression.dataType.ToString());
            Console.WriteLine("Expression : ");
            this.expression.Print();
        }

        public string Write()
        {
            string myAffectationString = "$scope.";
            myAffectationString += this.variableName.name;
            myAffectationString += " = function() \n { \n ";
            foreach (Tuple<AbstractExpression, AbstractExpression> t in listOfConditionAndExpression)
            {
                myAffectationString += "if(" + t.Item1.Write() + ") \n {";
                myAffectationString += "return " + t.Item2.Write() + "; \n } \n";
            }
            myAffectationString += "\t return ";
            myAffectationString += this.expression.Write();
            myAffectationString += "; \n }";
            return myAffectationString;
        }

    }
}
