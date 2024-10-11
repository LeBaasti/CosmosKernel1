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
        //Stopwatch timer = Stopwatch.StartNew();

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
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
                        //Console.WriteLine(timer.Elapsed.TotalSeconds);
                        TimeSpan uptime = TimeSpan.FromMilliseconds(Environment.TickCount);
                        Console.WriteLine("Systemlaufzeit: {0} Tage, {1} Stunden, {2} Minuten, {3} Sekunden",
                                    uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);
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
