using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class LoginCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("login");

        public override string Usage => "login <username> <password> - Meldet einen Benutzer an.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            if (args.Length < 2)
            {
                return false;
            }

            string username = args[0];
            string password = args[1];

            if (UserManagement.Login(username, password))
            {
                return true;
            }

            return true;
        }
    }
}
