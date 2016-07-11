namespace cli.dispatcher.usecase
{
    public class CliRequest
    {
        public CliRequest(string executable, string parameter)
        {
            Executable = executable;
            Parameter = parameter;
        }

        public string Executable { get; internal set; }
        public string Parameter { get; internal set; }
    }
}