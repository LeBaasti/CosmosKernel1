using System;
using System.Collections.Generic;
using Sys = Cosmos.System;
using System.IO;
using CosmosKernel1.Commands.API;
using CosmosKernel1.Commands;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        User LocalMachine = new User("root", eUserLevel.kAdministrator);
        User CurrentUser = new User("none", eUserLevel.kNone);

        List<User> Users = new List<User>();

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            UserManagement.InitializeTestUsers();
            UserManagement.LoadUsers();
            registerCommands();
        }

        private void registerCommands()
        {
            CommandHandler.RegisterCommand("help", new HelpCommand());
            CommandHandler.RegisterCommand("echo", new EchoCommand());
        }

        protected override void Run()
        {
            while(CurrentUser.userName == "none")
            {
                if(Users.Count == 0) 
                {
                    Console.Write("Create new user!\nEnter username: ");
                    string uname = Console.ReadLine();
                    if(uname.Length >= SystemSettings.MinCharCountUsername) 
                    {
                        Console.WriteLine("Enter password: ");
                        string pass = Console.ReadLine();

                        if(pass.Length >= SystemSettings.MinCharCountPassword)
                        {
                            Console.WriteLine("Repeat password: ");
                            string pass2 = Console.ReadLine();

                            if(pass == pass2)
                            {
                                Console.WriteLine("Passwort set!");
                                User newUser = new User(uname, eUserLevel.kAdministrator);
                                newUser.SetPassword(pass);
                                Users.Add(newUser);
                            }
                        }
                    }                    
                }
                else
                {
                    Console.Write("Log in with username: ");
                    string name = Console.ReadLine();

                    Console.Write("Password: ");
                    string passw = Console.ReadLine();

                    foreach (User u in Users)
                    {
                        if ((u.userName == name) && (u.password == passw))
                        {
                            Console.WriteLine("Login successfull!");
                            CurrentUser = u;
                        }
                    }
                }              
            }

            string[] arguments = Console.ReadLine().Split(" ");
            string command = arguments[0];
            List<string> list = new List<string>(arguments);
            list.RemoveAt(0);

            CommandHandler.ExecuteCommand(command, list.ToArray());
        }
    }
}
