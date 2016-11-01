using cli.dispatcher.usecase;
using System;
using System.Diagnostics;
using Xunit;

namespace cli.dispatcher.test
{
    public class ProcessOperatorTest
    {
        [Fact]
        public void CanExecuteExecutableWithParamaters()
        {
            ProcessOperator processOperator = new DiagnosticsProcessOperator();

            CliRunCmd cmd = new CliRunCmd(executable: "cli.dispatcher.exe", parameters: "--test");
            CliRunResult result = processOperator.Run(cmd);

            Assert.StartsWith("testing...\r\n--test", result.Output);
        }
    }
}
