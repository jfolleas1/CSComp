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
        TEXTE,
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

        public VariableInteger(long value) : base(VariableType.INTEGER, ExpressionType.NOMBRE)
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

        public override string WriteForCondition()
        {
            return Write();
        }
    }

    public class VariableFloat : Variable
    {
        public double value { get; }

        public VariableFloat(double value) : base(VariableType.FLOAT, ExpressionType.NOMBRE)
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

        public override string WriteForCondition()
        {
            return Write();
        }
    }


    public class VariableString : Variable
    {
        public string value { get; }

        public VariableString(string value) : base(VariableType.TEXTE, ExpressionType.TEXTE)
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

        public override string WriteForCondition()
        {
            string orginalText = Write();
            return "'" + orginalText.Substring(1, orginalText.Length -2 ) + "'";
        }
    }

    public class VariableId : Variable
    {
        public string name { get; }
        public bool local { get; }

        public static ExpressionType ComputeDataType(string dataType)
        {
            if (dataType[0] == 'L') // for local var
            {
                dataType = dataType.Substring(1);
            }
            ExpressionType type = ExpressionType.INVALIDE;
            if (!Enum.TryParse<ExpressionType>(dataType, out type))
            {
                type = ExpressionType.INVALIDE;
            }
            return type;
        }

        public VariableId(string name, string varType) : base(VariableType.ID, ComputeDataType(varType))
        {
            this.name = name;
            local = (Enum.TryParse<ExpressionType>(varType.Substring(1), out _) &&( varType[0] == 'L'));// LTEXTE || LNOMBRE || LBOOL
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

        public override string WriteForCondition()
        {
            string text = name;
            if (local)
            {
                text += "()";
            }
            return text;
        }
    }


}
