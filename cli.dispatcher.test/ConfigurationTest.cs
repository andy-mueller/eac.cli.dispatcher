
using Xunit;
using cli.dispatcher.configuration;
using System.Collections.Generic;
using System.Collections;

namespace cli.dispatcher.test
{
    public class ConfigurationTest
    {
        [Fact]
        public void CliTemplateConfigValues()
        {
            CliTemplateConfigurationElement configElement = new CliTemplateConfigurationElement();
            configElement.Executable = "Path to executable";
            configElement.Parameters = "a lot of cli parameters";

            Assert.Equal("Path to executable", configElement.Executable); 
            Assert.Equal("a lot of cli parameters", configElement.Parameters);
        }

        [Fact]
        public void CanLoadConfiguration()
        {
            Assert.NotNull(CliTemplateConfigurationSection.Instance);
        }
        [Fact]
        public void LoadsAllConfiguredElements()
        {
            IEnumerable<CliTemplateConfigurationElement> expectedElements = 
                new List<CliTemplateConfigurationElement>
            {
                new CliTemplateConfigurationElement() { Executable="program 1", Parameters="%key% parameters"},
                new CliTemplateConfigurationElement() { Executable="program 2", Parameters="%key2% parameters"}
            };
            IEnumerable<CliTemplateConfigurationElement> configuredElements = 
                Generify<CliTemplateConfigurationElement>(CliTemplateConfigurationSection.Instance.Templates);
            Assert.Equal(expectedElements, configuredElements);
        }
        static IEnumerable<T> Generify<T>(IEnumerable raw)
        {
            foreach (T item in raw)
                yield return item;
        }
    }
}
