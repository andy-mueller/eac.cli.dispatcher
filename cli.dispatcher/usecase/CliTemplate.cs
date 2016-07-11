namespace cli.dispatcher.usecase
{
    public class CliTemplate
    {
        private readonly Template executable;
        private readonly Template parameter;

        public CliTemplate(string executable, string parameter)
        {
            this.parameter = Template.of(parameter);
            this.executable = Template.of(executable);
        }
        
        public CliRequest apply(Properties properties)
        {
            return new CliRequest(executable: executable.Replace(properties), parameter: parameter.Replace(properties));
        }
    }
}