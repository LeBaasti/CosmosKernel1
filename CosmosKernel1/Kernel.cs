using KernelProject_One.Programs;
using System;
using System.Collections.Generic;
using Sys = Cosmos.System;

namespace KernelProject_One
{
    public class Kernel : Sys.Kernel
    {
        Directory RootDirectory = new Directory("source");
        Directory CurrentDirectory = new Directory("none");

        ProgramClass CurrentProgram = new Output();

        User LocalMachine = new User("root", eUserLevel.kAdministrator);
        User CurrentUser = new User("none", eUserLevel.kNone);

        List<User> Users = new List<User>();

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run()
        {
            RootDirectory.AddDirectory(new Directory("etc", LocalMachine));

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
                            CurrentDirectory = RootDirectory;
                            CurrentUser = u;
                        }
                    }
                }              
            }

            Console.Write(String.Format("{0} >:", CurrentDirectory.DirectoryName));
            string command = Console.ReadLine();

            switch(command)
            {
                case "output":
                    {
                        CurrentProgram = new Output();
                        CurrentProgram.Run();
                        break;
                    }
                case "help":
                    {
                        Console.WriteLine("\nhelp-Command");
                        Console.WriteLine("output - start the output program");
                        break;
                    }
            }
        }
    }
}
