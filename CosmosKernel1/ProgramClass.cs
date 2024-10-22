using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelProject_One
{
    public abstract class ProgramClass
    {
        public ProgramIdentifier Identifier { get; private set; }

        public abstract void Init();
        public abstract void Run();
        public abstract void Run(string[] args);
    }

    public struct ProgramIdentifier
    {
        public string ProgramName { get; private set; }
        public string Identifier { get; private set; }
        public eUserLevel RequiredUserLevel { get; private set; }

        public ProgramIdentifier(string programName, eUserLevel minimumLevel)
        {
            ProgramName = programName;
            Identifier = programName + "_id";
            RequiredUserLevel = minimumLevel;
        }
    }
}
