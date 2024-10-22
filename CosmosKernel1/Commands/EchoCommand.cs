﻿using CosmosKernel1.Commands.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1.Commands
{
    class EchoCommand : CommandBase
    {
        public override CommandIdentifier Identifier => new CommandIdentifier("echo");

        public override string Usage => "echo <message> - Returns a message to the console.";

        public override void Init()
        {
            //throw new NotImplementedException();
        }

        public override void Run()
        {
            //throw new NotImplementedException();
        }

        public override void Run(string[] args)
        {
            if(args == null || args.Length == 0) {
                Console.WriteLine(Usage);
                return; 
            }

            Console.WriteLine(String.Join(" ", args));
        }
    }
}