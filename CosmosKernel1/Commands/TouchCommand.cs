using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class TouchCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("touch");

        public override string Usage => "touch <file> - used to create an empty file or update the timestamp of an existing file.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
            if (args.Length > 0)
            {
                string filePath = Path.Combine(FileSystemManager.currentDirectory, args[0]);
                FileSystemManager.CreateFile(filePath, "");
            }
            else
            {
                Console.WriteLine("Bitte geben Sie einen Dateinamen an.");
            }
            return true;
        }
    }
}
