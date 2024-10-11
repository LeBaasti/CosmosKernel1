using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace CosmosKernel1
{
    public class Kernel : Sys.Kernel
    {

        public static int memorySize = 16;
        Cosmos.Core.MemoryBlock08[] memory = new Cosmos.Core.MemoryBlock08[memorySize];
        int uptimeInMilliseconds = Environment.TickCount;
        TimeSpan uptime = TimeSpan.FromMilliseconds(uptimeInMilliseconds);

        protected override void BeforeRun()
        {
            Console.WriteLine("Cosmos booted successfully. Type a line of text to get it echoed back.");
        }

        protected override void Run()
        {
            Console.Write("Input: ");
            var input = Console.ReadLine();
            if (input.Equals("tim", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Tim ist der Schreiber.");
                Console.WriteLine("Oliver auch");
            }
            if (input.Equals("--help", StringComparison.OrdinalIgnoreCase))
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
}
if (input.Equals("runtime", StringComparison.OrdinalIgnoreCase))
Console.WriteLine("Systemlaufzeit: {0} Tage, {1} Stunden, {2} Minuten, {3} Sekunden",
            uptime.Days, uptime.Hours, uptime.Minutes, uptime.Seconds);

            else
            {
                Console.Write("Text typed: ");
                Console.WriteLine(input);
            }
        }
    }
}
