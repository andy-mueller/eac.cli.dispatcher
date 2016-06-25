using cli.dispatcher;
using System.Collections.Generic;
using Xunit;

namespace cli.dispatcher.test
{
    public class TemplateTest
    {
        [Fact]
        public void simpleSubstitution()
        {
            var substitutor = Template.of("lalala %KEY%jfsj sölfkkf");

            var properties = new Dictionary<string, string>()
            {
                ["KEY"] = "VALUE",
            };
            var result = substitutor.Replace(properties);

            Assert.Equal("lalala VALUEjfsj sölfkkf", result);
        }
        [Fact]
        public void multipleSubstitution()
        {
            var substitutor = Template.of("lalala %KEY%jfsj söl%KEY2%fkkf");

            var properties = new Dictionary<string, string>()
            {
                ["KEY"] = "VALUE",
                ["KEY2"] = "VALUE2",
            };
            var result = substitutor.Replace(properties);

            Assert.Equal("lalala VALUEjfsj sölVALUE2fkkf", result);
        }

    }
}
