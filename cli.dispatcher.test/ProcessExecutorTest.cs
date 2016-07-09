using System;
using System.Diagnostics;
using Xunit;

namespace cli.dispatcher.test
{
    public class ProcessExecutorTest
    {
        [Fact]
        public void runExecutableInTestMode()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(
                fileName: "cli.dispatcher.exe",
                arguments: "--test");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            Process process = Process.Start(startInfo);
            string output = process.StandardOutput.ReadToEnd();
            Assert.Equal("testing...\r\n", output);
            process.WaitForExit();
        }
    }
}
