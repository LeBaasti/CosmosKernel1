using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class ExitCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("exit");

        public override string Usage => "exit - Schaltet das System ab.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            Cosmos.System.Power.Shutdown();
            return true;
        }
    }
}
