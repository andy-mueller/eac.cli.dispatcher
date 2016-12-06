using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace cli.dispatcher.usecase
{
    public  class CompressionToolProperties : IEnumerable<KeyValuePair<string, string>>
    {
        private readonly IDictionary<string, string> properties;

        CompressionToolProperties(IDictionary<string, string> properties)
        {
            this.properties = properties;
        }

        public static CompressionToolProperties of(params string[] properties)
        {
            return of(new List<String>(properties));
        }

        public  static CompressionToolProperties of(IEnumerable<string> listOfProperties)
        {
            IDictionary<string, string> properties = listOfProperties.ToDictionary(s => s.Split('=')[0], s => s.Split('=')[1]);
            return new CompressionToolProperties(properties);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        IEnumerator<KeyValuePair<string, string>> IEnumerable<KeyValuePair<string, string>>.GetEnumerator()
        {
            return properties.GetEnumerator();
        }
        public bool Contains(String key)
        {
            return properties.ContainsKey(key);
        }
    }
}