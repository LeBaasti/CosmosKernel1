using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class LsCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("ls");

        public override string Usage => "ls [options] [directory] - used to list the contents of a specified directory.";
;

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override bool Run(string[] args)
        {
            try
                    {
                        var directories = Sys.FileSystem.VFS.VFSManager.GetDirectoryListing(currentDirectory);
                        foreach (var dir in directories)
                        {
                            Console.WriteLine($"{(dir.mEntryType == Sys.FileSystem.Listing.DirectoryEntryTypeEnum.Directory ? "[DIR]" : "[FILE]")} {dir.mName}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Fehler bei der Verzeichnisauflistung: {ex.Message}");
                    }
                    return true;
        }
    }
}
