using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTime
{
    public class Affectation
    {
        public VariableId variableName { get; }
        public AbstractExpression expression { get; }

        public Affectation(VariableId name, AbstractExpression exp)
        {
            this.variableName = name;
            this.expression = exp;
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
            myAffectationString += " = function() \n { \n \t return ";
            myAffectationString += this.expression.Write();
            myAffectationString += "; \n } \n\n";
            return myAffectationString;
        }

    }
}
