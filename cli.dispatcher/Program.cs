using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using cli.dispatcher.configuration;

namespace cli.dispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program(new List<string>(args));
            program.run();
        }

        private readonly IList<string> arguments;

        Program(IList<string> arguments)
        {
            this.arguments = arguments;
        }
        
        void run()
        {
        }

    }
}
