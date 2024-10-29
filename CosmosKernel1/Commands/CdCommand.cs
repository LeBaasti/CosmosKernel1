using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class CdCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("cd");

        public override string Usage => "cd <directory> - used to switch in another directory in a specified location in the file system";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
            if (args.Length > 0)
            {
                string newDir;

                // Wenn der Benutzer ".." eingibt, navigiert er eine Verzeichnisebene zurück
                if (args[0] == "..")
                {
                    // Parent-Directory extrahieren
                    newDir = Directory.GetParent(FileSystemManager.currentDirectory)?.FullName;
                    if (newDir == null)
                    {
                        Console.WriteLine("Sie befinden sich bereits im Root-Verzeichnis.");
                    }
                    else
                    {
                        FileSystemManager.currentDirectory = newDir;
                        Console.WriteLine($"Verzeichnis gewechselt zu '{FileSystemManager.currentDirectory}'.");
                    }
                }
                else
                {
                    // Andernfalls kombiniere den aktuellen Pfad mit dem neuen Unterverzeichnis
                    newDir = Path.Combine(FileSystemManager.currentDirectory, args[0]);
                    if (Directory.Exists(newDir))
                    {
                        FileSystemManager.currentDirectory = newDir;
                        Console.WriteLine($"Verzeichnis gewechselt zu '{FileSystemManager.currentDirectory}'.");
                    }
                    else
                    {
                        Console.WriteLine("Verzeichnis existiert nicht.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Bitte geben Sie ein Verzeichnis an.");
            }
            return true;
        }
    }
}
