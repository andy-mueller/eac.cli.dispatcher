using System.Configuration;

namespace cli.dispatcher.configuration
{
    public class CliTemplateConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("executable", IsRequired = true)]
        public string Executable
        {
            get { return (string)this["executable"]; }
            set { this["executable"] = value; }
        }
        [ConfigurationProperty("parameters", IsRequired = true)]
        public string Parameters
        {
            get { return (string)this["parameters"]; }
            set { this["parameters"] = value; }
        }
        public override string ToString()
        {
            return "[CliConfig, executable=" + Executable + ". parameters=" + Parameters + "]";
        }
    }
}