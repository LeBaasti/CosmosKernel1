
using System;
using System.Collections.Generic;
using Sys = Cosmos.System;

namespace KernelProject_One
{
    public class Kernel : Sys.Kernel
    {
        // Custom Directory class definition
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
            RootDirectory.AddDirectory(new Directory("etc", LocalMachine));  // Example of adding a directory

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
                                Console.WriteLine("Password set!");
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
                            Console.WriteLine("Login successful!");
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

    // Custom Directory class implementation
    public class Directory
{
    public string DirectoryName { get; set; }
    public List<Directory> SubDirectories { get; set; }
    public List<File> Files { get; set; }
    public User Owner { get; set; }  // Optional: Benutzer, der das Verzeichnis besitzt

    // Konstruktor mit einem Parameter (nur Verzeichnisname)
    public Directory(string name)
    {
        DirectoryName = name;
        SubDirectories = new List<Directory>();
        Files = new List<File>();
    }

    // Konstruktor mit zwei Parametern (Verzeichnisname und Benutzer)
    public Directory(string name, User owner)
    {
        DirectoryName = name;
        Owner = owner;  // Speichere den Benutzer als Eigentümer
        SubDirectories = new List<Directory>();
        Files = new List<File>();
    }

    public void AddDirectory(Directory dir)
    {
        SubDirectories.Add(dir);
    }

    // Weitere Methoden für die Verwaltung von Dateien, etc.
}


    // Example File class if needed
    public class File
    {
        public string FileName { get; set; }

        public File(string name)
        {
            FileName = name;
        }
    }
}
