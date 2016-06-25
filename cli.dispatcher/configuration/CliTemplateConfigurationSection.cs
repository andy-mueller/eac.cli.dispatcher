using System.Configuration;

namespace cli.dispatcher.configuration
{
    public class CliTemplateConfigurationSection : ConfigurationSection
    {
        public static CliTemplateConfigurationSection Instance
        {
            get { return (CliTemplateConfigurationSection)ConfigurationManager.GetSection("cli-templates-section"); }
        }

        [ConfigurationProperty("cli-templates")]
        [ConfigurationCollection(typeof(CliTemplateConfigurationElementsCollection), AddItemName = "add")]
        public CliTemplateConfigurationElementsCollection Templates
        {
            get
            {
                return (CliTemplateConfigurationElementsCollection)base["cli-templates"];
            }
        }
    }
}
