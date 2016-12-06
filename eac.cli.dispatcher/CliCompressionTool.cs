using cli.dispatcher.usecase;
using System.Diagnostics;

namespace cli.dispatcher
{
    public class CliCompressionTool : CompressionTool
    {
        public CliRunResult Run(CliRunCmd request)
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

            return new CliRunResult(output: output);
        }
    }
}
