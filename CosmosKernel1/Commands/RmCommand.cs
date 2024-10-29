using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class RmCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("rm");

        public override string Usage => "rm [options] <file> - used to remove (delete) specified files or directories.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
            if (args.Length > 0)
            {
                string filePath = Path.Combine(FileSystemManager.currentDirectory, args[0]);
                FileSystemManager.DeleteFile(filePath);
            }
            else
            {
                Console.WriteLine("Bitte geben Sie eine Datei an.");
            }
            return true;
        }
    }
}
