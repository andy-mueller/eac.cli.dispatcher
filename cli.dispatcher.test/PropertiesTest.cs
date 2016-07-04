using cli.dispatcher.usecase;
using System.Collections.Generic;
using Xunit;

namespace cli.dispatcher.test
{
    public class PropertiesTest
    {
        [Fact]
        public void ExtractProperty()
        {
            IEnumerable<string> arguments = new List<string> { "key=value"};
        
            Properties properties = Properties.of(arguments);

            IEnumerable<KeyValuePair<string, string>> expected = new Dictionary<string, string>
            {
                {"key", "value" }
            };

            Assert.Equal(expected, properties);
        }

        [Fact]
        public void ExtractMultipleProperties()
        {
            IEnumerable<string> arguments = new List<string> { "key=value", "key2=value2" };

            Properties properties = Properties.of(arguments);

            var expected = new Dictionary<string, string>
            {
                {"key", "value" },
                {"key2", "value2" }
            };

            Assert.Equal(expected, properties);
        }
    }
}
