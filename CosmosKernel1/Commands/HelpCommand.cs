using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class HelpCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("help");

        public override string Usage => "help [commandName] - List all commands or get help for one specific command.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override void Run()
        {
            Console.WriteLine("Help message goes here. (Use CommandHandler)");
        }

        public override void Run(string[] args)
        {
            Run();
        }
    }
}
