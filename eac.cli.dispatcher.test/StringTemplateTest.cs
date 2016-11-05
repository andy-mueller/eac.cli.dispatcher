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
            var template = Template.of("lalala %KEY%jfsj sölfkkf");

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
            var template = Template.of("lalala %KEY%jfsj söl%KEY2%fkkf");

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
            var template = Template.of("lalala %CUT%jfsj söl%CUT%fkkf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "CUT","any" }
            };

            IList<string> cutProps = new List<string> { "CUT" };
            var result = template.Cut(properties, cutProps);

            Assert.Equal("lalala fkkf", result);
        }
        [Fact]
        public void CutsSectionOnlyForSpecifiedProperties()
        {
            var template = Template.of("la%CUT2%lala%CUT2%aslfjaljf %CUT%jfsj söl%CUT%fkkf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "CUT","any" }
            };

            IList<string> cutProps = new List<string> { "CUT" };
            var result = template.Cut(properties, cutProps);

            Assert.Equal("la%CUT2%lala%CUT2%aslfjaljf fkkf", result);
        }
        [Fact]
        public void CutsMultipleCuts()
        {
            var template = Template.of("lalala %CUT%jfsj söl%CUT%fkk%CUT2%fsfdasf%CUT2% safs sfsf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "CUT","any" },
                { "CUT2","any" }
            };

            IList<string> cutProps = new List<string> { "CUT", "CUT2" };
            var result = template.Cut(properties, cutProps);

            Assert.Equal("lalala fkk safs sfsf", result);
        }
        [Fact]
        public void ignoresUnknownCut()
        {
            var template = Template.of("lalala %CUT%jfsj söl%CUT%fkk fsf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "CUT","any" }
            };

            IList<string> cutProps = new List<string> { "NOT_NOWN", "CUT" };
            var result = template.Cut(properties, cutProps);

            Assert.Equal("lalala fkk fsf", result);
        }
        [Fact]
        public void CutsMultipleSections()
        {
            var template = Template.of("lalala %CUT%jfsj söl%CUT%fkk%CUT%fsfdasf%CUT% safs sfsf");

            var properties = new Dictionary<string, string>()
            {
                { "KEY", "VALUE" },
                { "CUT","any" }
            };

            IList<string> cutProps = new List<string> { "CUT" };
            var result = template.Cut(properties, cutProps);

            Assert.Equal("lalala fkk safs sfsf", result);
        }

    }
}
