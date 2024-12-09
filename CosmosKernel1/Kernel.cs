using System;
using System.Collections.Generic;
using Sys = Cosmos.System;
using System.IO;
using CosmosKernel1.Commands.API;
using CosmosKernel1.Commands;
using Cosmos.System.ScanMaps;
using Cosmos.System.Graphics;
using System.Drawing;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {
        Canvas canvas;

        protected override void BeforeRun()
        {
            //change to german keyboard layout
            SetKeyboardScanMap(new DE_Standard());

            FileSystemManager.Initialize();

            RoleMangement.InitializeTestRoles();
            RoleMangement.LoadRoles();

            UserManagement.InitializeTestUsers();
            UserManagement.LoadUsers();

            registerCommands();

            Console.WriteLine("Cosmos booted successfully.");

            login();

            /*
            You don't have to specify the Mode, but here we do to show that you can.
            To not specify the Mode and pick the best one, use:
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            */
            //canvas = FullScreenCanvas.GetFullScreenCanvas(new Mode(1920, 1080, ColorDepth.ColorDepth32));

            // This will clear the canvas with the specified color.
            //canvas.Clear(Color.Blue);
        }

        private void registerCommands()
        {
            CommandHandler.RegisterCommand("cat",   new CatCommand());
            CommandHandler.RegisterCommand("cd",    new CdCommand());
            CommandHandler.RegisterCommand("echo",  new EchoCommand());
            CommandHandler.RegisterCommand("help",  new HelpCommand());
            CommandHandler.RegisterCommand("ls",    new LsCommand());
            CommandHandler.RegisterCommand("mkdir", new MkdirCommand());
            CommandHandler.RegisterCommand("rm",    new RmCommand());
            CommandHandler.RegisterCommand("rmdir", new RmdirCommand());
            CommandHandler.RegisterCommand("touch", new TouchCommand());
            CommandHandler.RegisterCommand("write", new WriteCommand());
        }

        public void login()
        {
            // Login-Auforderung
            Console.WriteLine("Bitte melden Sie sich an.");
            string username;
            string password;

            // Überprüfen, ob der Benutzer existiert
            while (true)
            {
                Console.Write("Benutzername: ");
                username = Console.ReadLine();

                if (!string.IsNullOrEmpty(username) && UserManagement.UserExists(username))
                {
                    break; // Abbrechen, wenn der Benutzer existiert
                }
                else
                {
                    Console.WriteLine("Benutzername falsch, bitte versuchen Sie es erneut.");
                }
            }

            // Endlosschleife zur Passwortabfrage
            while (true) // Unendliche Schleife für die Passwortabfrage
            {
                Console.Write("Passwort: ");
                password = GetPassword(); // Ruft das Passwort mit Sternchen-Eingabe ab

                // Einloggen des Benutzers
                if (UserManagement.Login(username, password)) break;
            }
        }

        public static string GetPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true); // true = Zeichen wird nicht angezeigt

                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    // Ein Zeichen aus der Passworteingabe löschen
                    password = password.Substring(0, password.Length - 1);

                    // Cursor eine Stelle zurücksetzen, das Sternchen überschreiben und Cursor wieder zurücksetzen
                    int cursorPos = Console.CursorLeft;
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                }
                else if (key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace)
                {
                    // Zeichen zum Passwort hinzufügen und Sternchen anzeigen
                    password += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter); // Schleife läuft, bis Enter gedrückt wird

            Console.WriteLine(); // Zeilenumbruch nach der Eingabe
            return password;
        }

        protected override void Run()
        {
            //canvas.Display(); // Required for something to be displayed when using a double buffered driver

            Console.Write($"{UserManagement.loggedInUser.Name}@cosmosos-desktop:{FileSystemManager.currentDirectory}$ ");
            string[] arguments = Console.ReadLine().Split(" ");
            string command = arguments[0];
            List<string> list = new List<string>(arguments);
            list.RemoveAt(0);

            CommandHandler.ExecuteCommand(command, list.ToArray());
        }
    }
}
