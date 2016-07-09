using System;
using System.Collections.Generic;

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
            Console.WriteLine("testing...");
        }
    }
}
