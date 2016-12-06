using cli.dispatcher.usecase;
using System;
using System.Diagnostics;
using Xunit;

namespace cli.dispatcher.test
{
    public class CliCompressionToolTest
    {
        [Fact]
        public void CanExecuteExecutableWithParamaters()
        {
            CompressionTool processOperator = new CliCompressionTool();

            CliRunCmd cmd = new CliRunCmd(executable: "eac.cli.dispatcher.exe", parameters: "--test");
            CliRunResult result = processOperator.Run(cmd);

            Assert.StartsWith("testing...\r\n--test", result.Output);
        }
    }
}
