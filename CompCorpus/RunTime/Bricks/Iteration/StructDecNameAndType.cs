using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompCorpus.RunTime.Bricks
{
    public class StructDecNameAndType
    {
        public string name { get; set; }
        public string type { get; set; }

        public StructDecNameAndType(string name, string type)
        {
            this.name = name;
            this.type = type;
        }

    }
}
