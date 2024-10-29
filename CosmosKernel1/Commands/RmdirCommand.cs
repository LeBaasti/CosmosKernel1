using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class RmdirCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("rmdir");

        public override string Usage => "rmdir <directory> - used to remove an empty directory.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
             if (args.Length > 1)
                    {
                        string dirPath = Path.Combine(currentDirectory, args[1]);
                        fileSystemManager.DeleteDirectory(dirPath);
                    }
                    else
                    {
                        Console.WriteLine("Bitte geben Sie ein Verzeichnis an.");
                    }
                    return true;
        }
    }
}
