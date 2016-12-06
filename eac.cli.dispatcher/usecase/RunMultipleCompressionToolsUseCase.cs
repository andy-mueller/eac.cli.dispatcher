using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace cli.dispatcher.usecase
{
    public class RunMultipleCompressionToolsUseCase
    {
        private readonly CompressionTool processOperator;

        public RunMultipleCompressionToolsUseCase(CompressionTool processOperator)
        {
            this.processOperator = processOperator;
        }

        public void execute(IEnumerable<CliTemplate> cliTemplates, IEnumerable<string> cutProps, CompressionToolProperties properties)
        {
            cliTemplates.Select(cli => cli.apply(properties, cutProps)).ToList().ForEach(r=> processOperator.Run(r));
        }
    }
}
