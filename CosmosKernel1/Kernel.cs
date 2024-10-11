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
            }
            else
            {
                Console.Write("Text typed: ");
                Console.WriteLine(input);
            }
        }
    }
}
