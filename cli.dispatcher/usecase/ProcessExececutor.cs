using System.Collections.Generic;
using System.Diagnostics;

namespace cli.dispatcher.usecase
{

    public interface ProcessExececutor
    {
        void run(ProcessStartInfo startInfo);
    }
}
