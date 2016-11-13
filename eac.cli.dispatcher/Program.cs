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
            ProcessOperator processOperator = new DiagnosticsProcessOperator();
            Program program = new Program(new List<string>(args), Console.Out, processOperator);
            program.run();
        }

        private readonly List<string> arguments;
        private readonly TextWriter output;
        private readonly ProcessOperator processOperator;

        public Program(List<string> arguments, TextWriter output, ProcessOperator processOperator)
        {
            this.arguments = arguments;
            this.output = output;
            this.processOperator = processOperator;
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
            IEnumerable<string> cutProps = readCutPropertiesFromConfig();
            Properties properties = readPropertiesFromArguments();

            useCase.execute(cliTemplates, cutProps, properties);
        }

        private IEnumerable<string> readCutPropertiesFromConfig()
        {
            return new List<string>();
        }

        private Properties readPropertiesFromArguments()
        {
            return Properties.of(arguments);
        }

        private IEnumerable<CliTemplate> readTemplatedFromAppConfig()
        {
            return CliTemplateConfigurationSection.Instance.Templates.Select((t) => new CliTemplate(t.Executable, t.Parameters));
        }

        private void runTest()
        {
            output.WriteLine("testing...");
            arguments.ForEach(arg => output.Write(arg + " "));
            output.WriteLine();
            CliTemplateConfigurationSection.Instance.Templates.ToList().ForEach(cli => output.WriteLine(cli));
        }
    }
}
