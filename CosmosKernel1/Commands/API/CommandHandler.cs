using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands.API
{
    class CommandHandler
    {
        // Dictionary zur Speicherung der Commands, wobei der Name als Schlüssel dient
        private Dictionary<string, CommandBase> commands;

        public CommandHandler() {
            commands = new Dictionary<string, CommandBase>();
        }

        // Methode zum Registrieren eines Commands
        public void RegisterCommand(string name, CommandBase commandClass)
        {
            // Command unter dem Namen registrieren
            if (!commands.ContainsKey(name))
            {
                commands[name] = commandClass;
            }
        }

        // Methode zum Abrufen eines Commands
        public void ExecuteCommand(string name)
        {
            ExecuteCommand(name, null);
        }

        public void ExecuteCommand(string name, string[] args)
        {
            // Command anhand des Namens abrufen
            if (commands.TryGetValue(name, out var commandClass))
            {
                commandClass.Init();
                commandClass.Run(args);
            }
            else
            {
                Console.WriteLine($"Command '{name}' not found!");
            }
        }
    }
}
