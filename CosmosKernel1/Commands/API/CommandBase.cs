using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands.API
{
    public abstract class CommandBase
    {
        public abstract CommandIdentifier Identifier { get; }

        public abstract string Usage { get; }

        public abstract void Init();
        public abstract void Run();
        public abstract void Run(string[] args);
    }

    public struct CommandIdentifier
    {
        public string ProgramName { get; private set; }
        public string Identifier { get; private set; }
        public string RequiredPermission { get; private set; }

        public CommandIdentifier(string commandName)
        {
            ProgramName = commandName;
            Identifier = commandName + "_id";
            RequiredPermission = "command."+commandName;
        }
    }
}
