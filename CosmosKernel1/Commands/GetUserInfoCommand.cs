using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class GetUserInfoCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("userinfo");

        public override string Usage => "userinfo <username> - Zeigt die Details eines Benutzers an.";

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
            UserManagement.GetUserInfo(username);
            return true;
        }
    }
}
