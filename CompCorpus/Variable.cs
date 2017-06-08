using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTime
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

        public Variable(VariableType type)
        {
            this.type = type;
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
        long value;

        public VariableInteger(long value) : base(VariableType.INTEGER)
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
        double value;

        public VariableFloat(double value) : base(VariableType.FLOAT)
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
            return value.ToString();
        }
    }


    public class VariableString : Variable
    {
        string value;

        public VariableString(string value) : base(VariableType.STRING)
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
        string value;

        public VariableId(string value) : base(VariableType.ID)
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


}
