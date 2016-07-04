using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace cli.dispatcher.usecase
{
    public class ExecuteMultipleProcessesUseCase
    {
        private readonly ProcessExececutor processExecutor;

        public ExecuteMultipleProcessesUseCase(ProcessExececutor processExecutor)
        {
            this.processExecutor = processExecutor;
        }

        public void execute(IEnumerable<CliTemplate> cliTemplates, Properties properties)
        {
            var executablesToRun = cliTemplates
                                .Select(cli => cli.apply(properties))
                                .Select(inst => new ProcessStartInfo(fileName: inst.Executable, arguments: inst.Parameter));

            foreach(var executableToRun in executablesToRun)
                processExecutor.run(executableToRun);
        }
    }
}
