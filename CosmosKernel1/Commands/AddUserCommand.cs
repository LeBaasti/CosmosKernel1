using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmosKernel1;

namespace CosmosKernel1.Commands
{
    class AddUserCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("adduser");

        public override string Usage => "adduser <username> <password> <role> - Fügt einen neuen Benutzer hinzu.";

        public override void Init()
        {
            // Keine spezielle Initialisierung erforderlich
        }

        public override bool Run(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Fehlerhafte Syntax. Benutzung: adduser <username> <password> <role>");
                return false;
            }

            string username = args[0];
            string password = args[1];
            string roleName = args[2];

            Role role = RoleMangement.GetRoleByName(roleName);
            if (role == null)
            {
                Console.WriteLine($"Ungültige Rolle: {roleName}. Verfügbare Rollen: {string.Join(", ", RoleMangement.GetAllRoles())}");
                return false;
            }

            UserManagement.AddUser(username, password, role);
            return true;
        }
    }
}
