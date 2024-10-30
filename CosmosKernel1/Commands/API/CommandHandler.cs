using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands.API
{
    public static class CommandHandler
    {
        // Dictionary zur Speicherung der Commands, wobei der Name als Schlüssel dient
        private static Dictionary<string, CommandBase> commands = new Dictionary<string, CommandBase>();

        // Methode zum Registrieren eines Commands
        public static void RegisterCommand(string name, CommandBase commandClass)
        {
            // Command unter dem Namen registrieren
            if (!commands.ContainsKey(name))
            {
                commands[name] = commandClass;
                commandClass.Init();
            }
        }

        public static List<CommandBase> GetAllCommands() { return commands.Values.ToList(); }

        // Methode zum Abrufen eines Commands
        public static CommandBase GetCommand(string name)
        {
            // Command anhand des Namens abrufen
            if (commands.TryGetValue(name, out var commandClass))
            {
                return commandClass;
            }
            return null;
        }

        public static void ExecuteCommand(string name)
        {
            ExecuteCommand(name, null);
        }

        public static void ExecuteCommand(string name, string[] args)
        {
            if (GetCommand(name) is CommandBase command)
            {
                command.Execute(args);
            }
            else
            {
                Console.WriteLine($"Command '{name}' not found!");
            }
        }
    }
}
