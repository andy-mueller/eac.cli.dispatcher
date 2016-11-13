using cli.dispatcher;
using System.Collections.Generic;
using Xunit;

namespace cli.dispatcher.test
{
    public class StringTemplateTest
    {
        [Fact]
        public void SimpleSubstitution()
        {
            var template = StringTemplate.of("lalala %KEY%jfsj sölfkkf");

            var properties = new Dictionary<string, string>()
            {
                ["KEY"] = "VALUE"
            };
            var result = template.Replace(properties);

            Assert.Equal("lalala VALUEjfsj sölfkkf", result);
        }
        [Fact]
        public void MultipleSubstitution()
        {
            var template = StringTemplate.of("lalala %KEY%jfsj söl%KEY2%fkkf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "KEY2","VALUE2" }
            };
            var result = template.Replace(properties);

            Assert.Equal("lalala VALUEjfsj sölVALUE2fkkf", result);
        }

        [Fact]
        public void CutSection()
        {
            var template = StringTemplate.of("lalala %CUT%jfsj söl%CUT%fkkf");

            var result = template.CutOut("CUT");

            Assert.Equal("lalala fkkf", result.asString());
        }

        [Fact]
        public void CutsMultipleSections()
        {
            var template = StringTemplate.of("lalala %CUT%jfsj söl%CUT%fkk%CUT%fsfdasf%CUT% safs sfsf");

            var result = template.CutOut("CUT");

            Assert.Equal("lalala fkk safs sfsf", result.asString());
        }
        [Fact]
        public void Removes()
        {
            var template = StringTemplate.of("lalala %CUT%jfsj söl%CUT%fkk%CUT%fsfdasf%CUT% safs sfsf");

            var result = template.Cut("CUT");

            Assert.Equal("lalala jfsj sölfkkfsfdasf safs sfsf", result.asString());
        }
    }
}
