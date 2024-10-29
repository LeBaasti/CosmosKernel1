using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class WriteCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("write");

        public override string Usage => "write <user> - used to send a message directly to another user logged into the system.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
              string fileName = Path.Combine(currentDirectory, args[1]); // Kombiniere den aktuellen Pfad mit dem Dateinamen
                    Console.WriteLine("Bitte Inhalt eingeben");
                    string content = Console.ReadLine();
                    fileSystemManager.WriteToFile(fileName, content);
                    return true;
        }
    }
}
