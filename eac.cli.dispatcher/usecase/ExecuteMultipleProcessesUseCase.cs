using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace cli.dispatcher.usecase
{
    public class ExecuteMultipleProcessesUseCase
    {
        private readonly ProcessOperator processOperator;

        public ExecuteMultipleProcessesUseCase(ProcessOperator processOperator)
        {
            this.processOperator = processOperator;
        }

        public void execute(IEnumerable<CliTemplate> cliTemplates, Properties properties)
        {
            cliTemplates.Select(cli => cli.apply(properties)).ToList().ForEach(r=> processOperator.Run(r));
        }
    }
}
