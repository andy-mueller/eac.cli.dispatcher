using System;
using System.Collections.Generic;
using System.Linq;

namespace cli.dispatcher.test
{
    internal class Template
    {
        private string template;

        Template(string template)
        {
            this.template = template;
        }

        public static Template of(string template)
        {
            return new Template(template);
        }

        public string Replace(IEnumerable<KeyValuePair<string, string>> properties)
        {
            return properties.Aggregate(this.template, Replace);
        }

        private static string Replace(string template, KeyValuePair<string, string> property)
        {
            return template.Replace(string.Format("%{0}%", property.Key), property.Value);
        }
    }
}