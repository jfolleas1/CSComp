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
            Console.Write("Var Integer" + value +"\n");
        }

        public override string Write()
        {
            throw new NotImplementedException();
        }
    }

    public class VariableFloat : Variable
    {
        float value;

        public VariableFloat(float value) : base(VariableType.FLOAT)
        {
            this.value = value;
        }

        public override void Print(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("\t");
            Console.Write("Var Float" + value + "\n");
        }

        public override string Write()
        {
            throw new NotImplementedException();
        }
    }
}
