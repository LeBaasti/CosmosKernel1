using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    public class Role
    {
        public string Name { get; set; }
        public string[] Permissions { get; set; }

        public Role(string name, params string[] permissions) 
        {
            this.Name = name;
            this.Permissions = permissions;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public bool HasPermission(string permission)
        {
            if (Permissions.Contains("*")) return true;
            permission = permission.ToLower();
            if (Permissions.Contains(permission)) return true;
            var permissionRoot = permission.Split('.');
            if (permissionRoot.Length > 1)
            {
                for (int i = permissionRoot.Length - 1; i > 0; --i)
                {
                    if (Permissions.Contains($"{String.Join(".", permissionRoot.Take(i))}.*")) return true;
                }
            }
            return false;
        }
    }

    public static class RoleMangement
    {
        private const string FilePath = @"0:\roles.txt"; // File to save the roles in
        // Dictionary to store all role objects
        private static Dictionary<string, Role> roles = new Dictionary<string, Role>();

        public static bool RoleExists(string roleName)
        {
            return roles.ContainsKey(roleName.ToLower());
        }

        public static Role GetRoleByName(string name)
        {
            return RoleExists(name.ToLower()) ? roles[name.ToLower()] : null;
        }

        public static Dictionary<string, Role> GetRoles() { return roles; }


        private static void SaveRoles()
        {
            try
            {
                // Überprüfe, ob die Datei bereits existiert und lösche sie ggf.
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }

                // Erstellen und Schreiben in die Datei
                using (var stream = File.Create(FilePath))
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var role in roles)
                    {
                        var roleInfo = role.Value;
                        writer.WriteLine($"{role.Key.ToLower()}:{String.Join(",", roleInfo.Permissions)}");
                    }
                }
                Console.WriteLine("Rolen erfolgreich gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Benutzer: {ex.Message}");
            }
        }

        //fileio
        public static void LoadRoles()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    using (var stream = File.OpenRead(FilePath))
                    using (var reader = new StreamReader(stream))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            var roleInfoString = line.Split(':');
                            if (roleInfoString == null || roleInfoString.Length != 2)
                            {
                                Console.WriteLine("Error with roles.txt content format!");
                                return;
                            }
                            string roleName = roleInfoString[0].ToLower();
                            string[] permissions = roleInfoString[1].Split(",").ToArray();
                            roles[roleName] = new Role(roleName, permissions);
                        }
                    }
                    Console.WriteLine("Rolen erfolgreich geladen.");
                }
                else
                {
                    Console.WriteLine("Rolen-Datei existiert nicht.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Rolen: {ex.Message}");
            }
        }

        public static void InitializeTestRoles()
        {
            if (!File.Exists(FilePath))  // Prüfen, ob die Datei existiert
            {
                Console.WriteLine("Erstelle Testrollen...");

                // Testbenutzer hinzufügen
                roles.Add("admin", new Role("admin", "*"));
                roles.Add("user", new Role("user", "command.logut", "command.ls", "command.cd"));
                roles.Add("guest", new Role("guest", "command.logout", "command.echo"));

                // Speichern der Benutzer in die Datei
                SaveRoles();
            }
            else
            {
                Console.WriteLine("Rolendaten-Datei existiert bereits. Überspringe Testrollen-Erstellung.");
            }
        }
    }
}
