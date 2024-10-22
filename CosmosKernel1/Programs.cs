using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KernelProject_One.Programs
{
    public class Output : ProgramClass
    {
        public override void Init()
        {
            ProgramIdentifier newIdentifier = new ProgramIdentifier("Output", eUserLevel.kUser);
        }

        public override void Run(string[] args)
        {
            Console.WriteLine("Output program started with arguments!");
            Console.WriteLine("Argument: " + args[0]);
            Console.Write("Input a value:");
            string val = Console.ReadLine();
            Console.WriteLine("Given input: " + val);
        }

        public override void Run()
        {
            Console.WriteLine("Output program started!");
            Console.Write("Input a value:");
            string val = Console.ReadLine();
            Console.WriteLine("Given input: " + val);
           
        }
    }
}
