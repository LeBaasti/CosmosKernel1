using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            Run(null);
        }

        public override void Run(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                foreach (var command in CommandHandler.GetAllCommands())
                {
                    Console.WriteLine(command.Usage);
                }
            }
            else if (args.Length == 1)
            {
                if (CommandHandler.GetCommand(args[0]) is CommandBase command)
                {
                    Console.WriteLine(args[0] + ": " + command.Usage);
                }
                else
                {
                    Console.WriteLine($"Command '{args[0]}' not found!");
                }
            }
        }
    }
}
