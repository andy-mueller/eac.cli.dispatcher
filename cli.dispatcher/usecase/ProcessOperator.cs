using System.Collections.Generic;
using System.Diagnostics;

namespace cli.dispatcher.usecase
{

    public interface ProcessOperator
    {
        CliRunResult run(CliRunCmd request);
    }
}
