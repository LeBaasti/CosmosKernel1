using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class WriteCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("write");

        public override string Usage => "write <file> - used to write content into a file.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
            string fileName = Path.Combine(FileSystemManager.currentDirectory, args[0]); // Kombiniere den aktuellen Pfad mit dem Dateinamen
            Console.WriteLine("Bitte Inhalt eingeben");
            string content = Console.ReadLine();
            FileSystemManager.WriteToFile(fileName, content);
            return true;
        }
    }
}
