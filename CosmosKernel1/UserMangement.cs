using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using Sys = Cosmos.System;
using System.Security.Cryptography;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace CosmosKernel1
{
    // Klasse zur Verwaltung von Benutzerinformationen
    public class UserInfo
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string[] Permissions { get; set; }

        public UserInfo(string name, string password, string role, params string[] permissions)
        {
            this.Name = name;
            this.Password = password;
            this.Role = role;
            this.Permissions = permissions;
        }
    }
    public static class UserManagement
    {
        private const string FilePath = @"0:\users.txt"; // Datei zur Speicherung
        // Dictionary zur Verwaltung von Benutzern mit Benutzername, Rolle und Passwort
        private static Dictionary<string, UserInfo> users = new Dictionary<string, UserInfo>();

        public static UserInfo loggedInUser { get; private set; }

        // Benutzer hinzufügen
        public static void AddUser(string username, string password, string role)
        {
            if (!users.ContainsKey(username))
            {
                users.Add(username, new UserInfo(username, password, role)); // Übergebe den Benutzernamen
                Console.WriteLine($"Benutzer {username} mit der Rolle {role} wurde hinzugefügt.");
                SaveUsers(); // Speichert nach jedem Hinzufügen
            }
            else
            {
                Console.WriteLine($"Benutzer {username} existiert bereits.");
            }
        }
        // Überprüfen, ob ein Benutzer existiert
        public static bool UserExists(string username)
        {
            return users.ContainsKey(username);
        }

        // Rolle eines Benutzers abrufen
        public static string GetUserRole(string username)
        {
            if (users.ContainsKey(username))
            {
                return users[username].Role;
            }
            else
            {
                return "Benutzer existiert nicht.";
            }
        }

        // Benutzername abrufen (jetzt über die UserInfo-Klasse)
        public static string GetUsername(string username)
        {
            if (users.ContainsKey(username))
            {
                return users[username].Name; // Hier verwenden wir die Name-Eigenschaft
            }
            else
            {
                return "Benutzer existiert nicht.";
            }
        }

        // Passwort eines Benutzers überprüfen
        public static bool VerifyPassword(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                return users[username].Password == password;
            }
            else
            {
                return false;
            }
        }

        // Rolle eines Benutzers ändern
        public static void ChangeUserRole(string username, string newRole)
        {
            if (users.ContainsKey(username))
            {
                users[username].Role = newRole;
                Console.WriteLine($"Die Rolle von {username} wurde auf {newRole} geändert.");
                SaveUsers();
            }
            else
            {
                Console.WriteLine("Benutzer existiert nicht.");
            }
        }

        // Benutzer entfernen
        public static void RemoveUser(string username)
        {
            if (users.ContainsKey(username))
            {
                users.Remove(username);
                Console.WriteLine($"Benutzer {username} wurde entfernt.");
                SaveUsers(); // Speichere die Änderungen

            }
            else
            {
                Console.WriteLine("Benutzer existiert nicht.");
            }
        }

        // alle Benutzer entfernen
        public static void RemoveAllUser()
        {
            users.Clear();
            Console.WriteLine("Alle Benutzer wurden entfernt");
            SaveUsers(); // Speichere die Änderungen
        }

        public static void DisplayAllUsers()
        {
            foreach (var kvp in users)
            {
                string username = kvp.Key;
                UserInfo userInfo = kvp.Value;
                Console.WriteLine($"Benutzer: {username}, Role: {userInfo.Role}");
            }
        }

        // Zugriff auf einen Benutzer
        public static void GetUserInfo(string username)
        {
            if (users.ContainsKey(username))
            {
                UserInfo userInfo = users[username];
                Console.WriteLine($"Benutzer: {username}, Passwort: {userInfo.Password}, Role: {userInfo.Role},");
            }
            else
            {
                Console.WriteLine("Benutzer existiert nicht.");
            }
        }

        // Benutzer Login-Anforderung
        public static bool Login(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                if (users[username].Password == password)
                {
                    Console.WriteLine($"Login erfolgreich. Willkommen {username}!");
                    loggedInUser = users[username];
                    return true;
                }
                else
                {
                    Console.WriteLine("Falsches Passwort.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Benutzer existiert nicht.");
                return false;
            }
        }

        // Benutzername ändern(für den aktuell eingeloggten Benutzer)
        // Benutzername ändern
        public static bool ChangeUsername(string oldUsername, string newUsername)
        {
            if (users.ContainsKey(oldUsername))
            {
                if (!users.ContainsKey(newUsername)) // Überprüfen, ob der neue Benutzername bereits existiert
                {
                    UserInfo userInfo = users[oldUsername];
                    users.Remove(oldUsername); // Alten Benutzernamen entfernen
                    users.Add(newUsername, userInfo); // Neuen Benutzernamen hinzufügen
                    Console.WriteLine($"Benutzername wurde von {oldUsername} auf {newUsername} geändert.");
                    SaveUsers();
                    return true;
                }
                else
                {
                    Console.WriteLine("Der neue Benutzername existiert bereits.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Benutzer existiert nicht.");
                return false;
            }
        }

        private static void SaveUsers()
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
                    foreach (var user in users)
                    {
                        var userInfo = user.Value;
                        writer.WriteLine($"{user.Key}:{userInfo.Password},{userInfo.Role},{String.Join(",", userInfo.Permissions)}");
                    }
                }
                Console.WriteLine("Benutzerdaten erfolgreich gespeichert.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Speichern der Benutzer: {ex.Message}");
            }
        }

        //fileio
        public static void LoadUsers()
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
                            var userInfoString = line.Split(':');
                            if (userInfoString == null || userInfoString.Length != 2)
                            {
                                Console.WriteLine("Error with users.txt content format!");
                                return;
                            }
                            string username = userInfoString[0];
                            var parts = userInfoString[1].Split(",");
                            if (parts.Length >= 2)
                            {
                                string password = parts[0];
                                string role = parts[1];
                                string[] permissions = parts.Skip(2).ToArray();
                                users[username] = new UserInfo(username, password, role, permissions);
                            }
                        }
                    }
                    Console.WriteLine("Benutzerdaten erfolgreich geladen.");
                }
                else
                {
                    Console.WriteLine("Benutzerdaten-Datei existiert nicht.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Benutzer: {ex.Message}");
            }
        }

        public static void InitializeTestUsers()
        {
            if (!File.Exists(FilePath))  // Prüfen, ob die Datei existiert
            {
                Console.WriteLine("Erstelle Testbenutzer...");

                // Testbenutzer hinzufügen
                users.Add("User1", new UserInfo("User1", "password123", "User", "command.echo", "command.help"));
                users.Add("User2", new UserInfo("User2", "adminPass", "Admin", "command.help"));
                users.Add("User3", new UserInfo("User3", "password123", "User"));

                // Speichern der Benutzer in die Datei
                SaveUsers();
            }
            else
            {
                Console.WriteLine("Benutzerdaten-Datei existiert bereits. Überspringe Testbenutzer-Erstellung.");
            }
        }

    }
}