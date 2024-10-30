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
        //Is getting executed when the command handler registers the command
        public abstract void Init();
        public abstract bool Run(string[] args);

        public void Execute(string[] args)
        {
            if(!UserManagement.loggedInUser.HasPermission(Identifier.RequiredPermission))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Permission denied for command '{Identifier.CommandName}'!");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            if(!Run(args))
            {
                PrintUsage();
            }
        }

        public void PrintUsage()
        {
            Console.WriteLine(Usage);
        }
    }

    public struct CommandIdentifier
    {
        public string CommandName { get; private set; }
        public string Identifier { get; private set; }
        public string RequiredPermission { get; private set; }

        public CommandIdentifier(string commandName)
        {
            CommandName = commandName;
            Identifier = commandName + "_id";
            RequiredPermission = "command."+commandName;
        }
    }
}
