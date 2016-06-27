using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace cli.dispatcher.test
{
    public  class Properties : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> properties;

        Properties(IDictionary<string, string> properties)
        {
            this.properties = properties;
        }

        public  static Properties of(IEnumerable<string> arguments)
        {
            IDictionary<string, string> properties = arguments.ToDictionary(s => s.Split('=')[0], s => s.Split('=')[1]);
            return new Properties(properties);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return properties.GetEnumerator();
        }
    }
}