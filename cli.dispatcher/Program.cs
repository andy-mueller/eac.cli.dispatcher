using System;
using System.Collections.Generic;
using System.IO;

namespace cli.dispatcher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program(new List<string>(args), Console.Out);
            program.run();
        }

        private readonly IList<string> arguments;
        private readonly TextWriter output;

        public Program(IList<string> arguments, TextWriter output)
        {
            this.arguments = arguments;
            this.output = output;
        }
        
        public void run()
        {
            if (arguments.Contains("--test"))
            {
                runTest();
            }
            else
            {
                dispatchCommandLine();
            }
        }

        private void dispatchCommandLine()
        {
        }

        private void runTest()
        {
            output.WriteLine("testing...");
        }
    }
}
