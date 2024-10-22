using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Sys = Cosmos.System;


namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        public static int memorySize = 16;
        Cosmos.Core.MemoryBlock08[] memory = new Cosmos.Core.MemoryBlock08[memorySize];

        public DateTime systemStart;
        Directory RootDirectory = new Directory("source");
        Directory CurrentDirectory = new Directory("none");

        ProgramClass CurrentProgram = new Output();

        User LocalMachine = new User("root", eUserLevel.kAdministrator);
        User CurrentUser = new User("none", eUserLevel.kNone);

        List<User> Users = new List<User>();

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            systemStart = DateTime.Now;
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

            Console.Write("Input: ");
            var input = Console.ReadLine();

            var args = input.Split(' ');
            switch(args[0])
            {
                case "help":
                    {
                        Console.WriteLine("Usage: [your_program] [options]");
                        Console.WriteLine();
                        Console.WriteLine("Options:");
                        Console.WriteLine("  --help        Display this help message.");
                        Console.WriteLine("  --version     Show the program version.");
                        Console.WriteLine("  --config      Specify the configuration file.");
                        Console.WriteLine("  --verbose     Enable verbose logging.");
                        Console.WriteLine();
                        Console.WriteLine("Example:");
                        Console.WriteLine("  your_program --config=config.json --verbose");
                        break;
                    }
                case "runtime":
                    {
                        Console.WriteLine("Zeit seit Systemstart: " + (DateTime.Now - systemStart));
                        break;
                    }
                case "echo":
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
