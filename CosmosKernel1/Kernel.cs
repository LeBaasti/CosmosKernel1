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

            UserManagement.LoginPrompt();

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
            CommandHandler.RegisterCommand("exit",  new ExitCommand());
            CommandHandler.RegisterCommand("help",  new HelpCommand());
            CommandHandler.RegisterCommand("ls",    new LsCommand());
            CommandHandler.RegisterCommand("mkdir", new MkdirCommand());
            CommandHandler.RegisterCommand("rm",    new RmCommand());
            CommandHandler.RegisterCommand("rmdir", new RmdirCommand());
            CommandHandler.RegisterCommand("touch", new TouchCommand());
            CommandHandler.RegisterCommand("write", new WriteCommand());
            CommandHandler.RegisterCommand("changeuserrole", new ChangeUserRoleCommand());
            CommandHandler.RegisterCommand("adduser", new AddUserCommand());
            CommandHandler.RegisterCommand("userinfo", new GetUserInfoCommand());
            CommandHandler.RegisterCommand("initialize", new InitializeCommand());
            CommandHandler.RegisterCommand("listusers", new ListUsersCommand());
            CommandHandler.RegisterCommand("login", new LoginCommand());
            CommandHandler.RegisterCommand("logout", new LogoutCommand());
            CommandHandler.RegisterCommand("removeuser", new RemoveUserCommand());
            
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
