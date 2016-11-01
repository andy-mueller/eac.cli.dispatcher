using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cli.dispatcher.usecase
{
    public class CliRunResult
    {
        public CliRunResult(string output)
        {
            Output = output;
        }

        public string Output { get; private set; }
    }
}
