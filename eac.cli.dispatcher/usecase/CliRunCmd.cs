namespace cli.dispatcher.usecase
{
    public class CliRunCmd
    {
        public CliRunCmd(string executable, string parameters)
        {
            Executable = executable;
            Parameter = parameters;
        }

        public string Executable { get; internal set; }
        public string Parameter { get; internal set; }
    }
}