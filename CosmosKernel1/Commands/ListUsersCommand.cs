using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class ListUsersCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("listusers");

        public override string Usage => "listusers - Listet alle Benutzer und deren Rollen auf.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            UserManagement.DisplayAllUsers();
            return true;
        }
    }
}
