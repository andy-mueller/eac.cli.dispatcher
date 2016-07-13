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
        
        public CliRunCmd apply(Properties properties)
        {
            return new CliRunCmd(executable: executable.Replace(properties), parameters: parameter.Replace(properties));
        }
    }
}