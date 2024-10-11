using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        public static int memorySize = 16;
        Cosmos.Core.MemoryBlock08[] memory = new Cosmos.Core.MemoryBlock08[memorySize];

        public DateTime systemStart;

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
            systemStart = DateTime.Now;
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();

            var args = input.Split(' ');
            switch(args[0])
            {
                case "help":
                    {
                        Console.WriteLine("Usage: [your_program] [options]");
                        Console.WriteLine();
                        Console.WriteLine("Options:");
                        Console.WriteLine("  --help        Display this help message.");
                        Console.WriteLine("  --version     Show the program version.");
                        Console.WriteLine("  --config      Specify the configuration file.");
                        Console.WriteLine("  --verbose     Enable verbose logging.");
                        Console.WriteLine();
                        Console.WriteLine("Example:");
                        Console.WriteLine("  your_program --config=config.json --verbose");
                        break;
                    }
                case "runtime":
                    {
                        Console.WriteLine("Zeit seit Systemstart: " + (DateTime.Now - systemStart));
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
    }
}
