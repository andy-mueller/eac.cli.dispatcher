using cli.dispatcher.usecase;
using System.Collections.Generic;
using Xunit;

namespace cli.dispatcher.test
{
    public class CompressionToolPropertiesTest
    {
        [Fact]
        public void ExtractProperty()
        {
            IEnumerable<string> arguments = new List<string> { "key=value"};
        
            CompressionToolProperties properties = CompressionToolProperties.of(arguments);

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

            CompressionToolProperties properties = CompressionToolProperties.of(arguments);

            var expected = new Dictionary<string, string>
            {
                {"key", "value" },
                {"key2", "value2" }
            };

            Assert.Equal(expected, properties);
        }
    }
}
