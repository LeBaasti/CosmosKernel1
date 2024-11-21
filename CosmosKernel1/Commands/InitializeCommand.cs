using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class InitializeCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("initusers");

        public override string Usage => "initusers - Initialisiert Testbenutzer.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            UserManagement.InitializeTestUsers();
            return true;
        }
    }
}
