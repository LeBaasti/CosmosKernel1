using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class MkdirCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("mkdir");

        public override string Usage => "mkdir <directory> - used to create a new directory in a specified location in the file system";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
            if (args.Length > 1)
                    {
                        string dirName = Path.Combine(currentDirectory, args[1]);
                        fileSystemManager.CreateDirectory(dirName);
                    }
                    else
                    {
                        Console.WriteLine("Bitte geben Sie einen Verzeichnisnamen an.");
                    }
                    return true;
        }
    }
}
