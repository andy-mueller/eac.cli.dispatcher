
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
        public void canLoadConfiguration()
        {
            Assert.NotNull(CliTemplateConfigurationSection.Instance);
        }
        [Fact]
        public void hasCorrecAmmountOfTemplates()
        {
            IEnumerable<CliTemplateConfigurationElement> expectedElements = new List<CliTemplateConfigurationElement>
            {
                new CliTemplateConfigurationElement() { Executable="program 1", Parameters="a lot of parameters"},
                new CliTemplateConfigurationElement() { Executable="program 2", Parameters="a lot of other parameters"}
            };
            IEnumerable<CliTemplateConfigurationElement> configuredElements = generify< CliTemplateConfigurationElement>(CliTemplateConfigurationSection.Instance.Templates);
            Assert.Equal(expectedElements, configuredElements);
        }
        static IEnumerable<T> generify<T>(IEnumerable raw)
        {
            foreach (T item in raw)
                yield return item;
        }
    }
}
