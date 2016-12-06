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
            CompressionTool processOperator = new CliCompressionTool();
            Program program = new Program(new List<string>(args), Console.Out, processOperator);
            program.run();
        }

        private readonly List<string> arguments;
        private readonly TextWriter output;
        private readonly CompressionTool processOperator;

        public Program(List<string> arguments, TextWriter output, CompressionTool processOperator)
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
            RunMultipleCompressionToolsUseCase useCase = new RunMultipleCompressionToolsUseCase(processOperator);
            IEnumerable<CliTemplate> cliTemplates = readTemplatedFromAppConfig();
            IEnumerable<string> cutProps = readCutPropertiesFromConfig();
            CompressionToolProperties properties = readPropertiesFromArguments();

            useCase.execute(cliTemplates, cutProps, properties);
        }

        private IEnumerable<string> readCutPropertiesFromConfig()
        {
            return new List<string>();
        }

        private CompressionToolProperties readPropertiesFromArguments()
        {
            return CompressionToolProperties.of(arguments);
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
