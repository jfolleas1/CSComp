using System;

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime
{
    public enum VariableType
    {
        INTEGER,
        FLOAT,
        STRING,
        BOOL,
        ID,
    };


    public abstract class Variable : AbstractExpression
    {

        public VariableType type { get; }

        public Variable(VariableType type, ExpressionType dataType)
        {
            this.type = type;
            base.dataType = dataType;
        }

        public override void Print(int level)
        {
            throw new NotImplementedException();
        }

        public override string Write()
        {
            throw new NotImplementedException();
        }
    }

    public class VariableInteger : Variable
    {
        public long value { get; }

        public VariableInteger(long value) : base(VariableType.INTEGER, ExpressionType.NUMBER)
        {
            this.value = value;
        }

        public override void Print(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.Write("Var "+ base.type.ToString()+" "+ value +"\n");
        }

        public override string Write()
        {
            return value.ToString();
        }
    }

    public class VariableFloat : Variable
    {
        public double value { get; }

        public VariableFloat(double value) : base(VariableType.FLOAT, ExpressionType.NUMBER)
        {
            this.value = value;
        }

        public override void Print(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.Write("Var " + base.type.ToString() + " " + value + "\n");
        }

        public override string Write()
        {
            return value.ToString("G", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }


    public class VariableString : Variable
    {
        public string value { get; }

        public VariableString(string value) : base(VariableType.STRING, ExpressionType.STRING)
        {
            this.value = value;
        }

        public override void Print(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.Write("Var " + base.type.ToString() + " " + value + "\n");
        }

        public override string Write()
        {
            return value;
        }
    }

    public class VariableId : Variable
    {
        public string name { get; }
        public bool local { get; }

        public static ExpressionType ComputeDataType(string dataType)
        {
            ExpressionType type = ExpressionType.INVALIDE;
            switch (dataType)
            {
                case "STRING" :
                case "LSTRING":
                    {
                        type = ExpressionType.STRING;
                        break;
                    }
                case "NUMBER":
                case "LNUMBER":
                    {
                        type = ExpressionType.NUMBER;
                        break;
                    }
                case "BOOL":
                case "LBOOL":
                    {
                        type = ExpressionType.BOOL;
                        break;
                    }
                case "NULL":
                    {
                        // Si la variable tend à être remplacé alors ce sera par une string pour les choix 
                        type = ExpressionType.UNKNOWVAR;
                        break;
                    }
                default:
                    {
                        type = ExpressionType.INVALIDE;
                        break;
                    }
            };
            return type;
        }

        public VariableId(string name, string varType) : base(VariableType.ID, ComputeDataType(varType))
        {
            this.name = name;
            local = ((varType == "LSTRING") || (varType == "LNUMBER") || (varType == "LBOOL"));

        }

        public override void Print(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.Write("Var " + base.type.ToString() + " " + name + "\n");
        }

        public override string Write()
        {
            string text = "$scope."+name;
            if (local)
            {
                text += "()";
            }
            return text;
        }
    }


}
