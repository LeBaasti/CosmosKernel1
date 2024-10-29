using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class CatCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("cat");

        public override string Usage => "cat <file> - used to display the contents of a file or concatenate multiple files and display their output.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
               if (args.Length > 1)
                    {
                        string filePath = Path.Combine(currentDirectory, args[1]);
                        fileSystemManager.ReadFile(filePath);
                    }
                    else
                    {
                        Console.WriteLine("Bitte geben Sie eine Datei an.");
                    }
                    return true;
        }
    }
}
