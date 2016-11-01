using System.Collections.Generic;

namespace cli.dispatcher.usecase
{

    public interface ProcessOperator
    {
        CliRunResult Run(CliRunCmd request);
    }
}
