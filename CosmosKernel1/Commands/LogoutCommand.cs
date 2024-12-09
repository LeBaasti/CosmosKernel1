using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class LogoutCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("logout");

        public override string Usage => "logout - Meldet den aktuellen Benutzer ab.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            if (UserManagement.Logout())
            {
                Console.WriteLine("Abmeldung erfolgreich.");
                return true;
            }

            Console.WriteLine("Kein Benutzer ist derzeit angemeldet.");
            return false;
        }
    }
}
