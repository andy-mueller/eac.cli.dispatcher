using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using cli.dispatcher.usecase;
using cli.dispatcher.configuration;

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
        private ProcessOperator processOperator;

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
            ExecuteMultipleProcessesUseCase useCase = new ExecuteMultipleProcessesUseCase(processOperator);
            IEnumerable<CliTemplate> cliTemplates = readTemplatedFromAppConfig();
            Properties properties = readPropertiesFromArguments();

            useCase.execute(cliTemplates, properties);
        }

        private Properties readPropertiesFromArguments()
        {
            return Properties.of(arguments);
        }

        private IEnumerable<CliTemplate> readTemplatedFromAppConfig()
        {
            return CliTemplateConfigurationSection.Instance.Templates.Select((t) => new CliTemplate(t.Executable, t.Parameters));
        }

        public void overrideProcessOperator(ProcessOperator processOperator)
        {
            this.processOperator = processOperator;
        }

        private void runTest()
        {
            output.WriteLine("testing...");
        }
    }
}
