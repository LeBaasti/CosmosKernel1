using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public abstract class ProgramClass
    {
        public string PID { get; private set; }
        public string Identifier { get; private set; }
        public int MemoryStartIndex { get; private set; }

        public ProgramClass(string pID, string identifier, int memoryStartIndex)
        {
            PID = pID;
            Identifier = identifier;
            MemoryStartIndex = memoryStartIndex;
        }
    }

    public class Calculator : ProgramClass
    {
        public Calculator(): base("", "", 5)
        {

        }
    }
}
