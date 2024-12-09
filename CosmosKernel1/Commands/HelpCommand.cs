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

        public override bool Run(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                foreach (var command in CommandHandler.GetAllCommands())
                {
                    command.PrintUsage();
                }
            }
            else if (args.Length == 1)
            {
                if (CommandHandler.GetCommand(args[0]) is CommandBase command)
                {
                    command.PrintUsage();
                }
                else
                {
                    Console.WriteLine($"Command '{args[0]}' not found!");
                }
            }
            return true;
        }
    }
}
