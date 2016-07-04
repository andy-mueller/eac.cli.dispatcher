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
        
        public CliInstance apply(Properties properties)
        {
            CliInstance instance = new CliInstance();
            instance.Parameter = parameter.Replace(properties);
            instance.Executable = executable.Replace(properties);
            return instance;
        }
    }
}