using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class RemoveUserCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("removeuser");

        public override string Usage => "removeuser <username> - Entfernt einen Benutzer.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            if (args.Length < 1)
            {
                return false;
            }

            string username = args[0];
            if (!UserManagement.UserExists(username))
            {
                Console.WriteLine($"Benutzer {username} existiert nicht.");
                return true;
            }

            UserManagement.RemoveUser(username);
            return true;
        }
    }
}
