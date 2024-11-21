using CosmosKernel1.Commands.API;
using System;

namespace CosmosKernel1.Commands
{
    class ChangeUserRoleCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("changeuserrole");

        public override string Usage => "changeuserrole <username> <role> - Ändert die Rolle eines Benutzers.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Fehlerhafte Syntax. Benutzung: changeuserrole <username> <role>");
                return false;
            }

            string username = args[0];
            string roleName = args[1];

            Role role = RoleMangement.GetRoleByName(roleName);
            if (role == null)
            {
                Console.WriteLine($"Ungültige Rolle: {roleName}. Verfügbare Rollen: {string.Join(", ", RoleMangement.GetAllRoles())}");
                return false;
            }

            if (!UserManagement.UserExists(username))
            {
                Console.WriteLine($"Benutzer {username} existiert nicht.");
                return false;
            }

            UserManagement.ChangeUserRole(username, role);
            return true;
        }
    }
}
