using System.Collections.Generic;

namespace cli.dispatcher.usecase
{

    public interface CompressionTool
    {
        CliRunResult Run(CliRunCmd request);
    }
}
