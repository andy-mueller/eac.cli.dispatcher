using cli.dispatcher;
using System.Collections.Generic;
using Xunit;

namespace cli.dispatcher.test
{
    public class StringTemplateTest
    {
        [Fact]
        public void simpleSubstitution()
        {
            var template = Template.of("lalala %KEY%jfsj sölfkkf");

            var properties = new Dictionary<string, string>()
            {
                ["KEY"] = "VALUE"
            };
            var result = template.Replace(properties);

            Assert.Equal("lalala VALUEjfsj sölfkkf", result);
        }
        [Fact]
        public void multipleSubstitution()
        {
            var template = Template.of("lalala %KEY%jfsj söl%KEY2%fkkf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "KEY2","VALUE2" }
            };
            var result = template.Replace(properties);

            Assert.Equal("lalala VALUEjfsj sölVALUE2fkkf", result);
        }

    }
}
