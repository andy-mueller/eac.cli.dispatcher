using cli.dispatcher.usecase;
using System;
using System.Diagnostics;
using Xunit;

namespace cli.dispatcher.test
{
    public class ProcessOperatorTest
    {
        [Fact]
        public void runExecutableInTestMode()
        {
            ProcessOperator processOperator = new DiagnosticsProcessOperator();

            CliRunCmd cmd = new CliRunCmd(executable: "cli.dispatcher.exe", parameters: "--test");
            CliRunResult result = processOperator.run(cmd);

            Assert.Equal("testing...\r\n", result.Output);
        }

        class DiagnosticsProcessOperator : ProcessOperator
        {
            public CliRunResult run(CliRunCmd request)
            {

                ProcessStartInfo startInfo = new ProcessStartInfo(
                    fileName: request.Executable,
                    arguments: request.Parameter);
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;

                Process process = Process.Start(startInfo);
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                CliRunResult r = new CliRunResult(output: output);
                return r;
            }
        }
    }
}
