using System;
using System.Collections.Generic;
using System.Linq;

namespace cli.dispatcher
{
    public class Template
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

        public string Cut(Dictionary<string, string> properties, IEnumerable<string> cuts)
        {
            String reducedTemplate = template;
            foreach(string cut in cuts) { 
                string cutProp = string.Format("%{0}%", cut);
                int first = reducedTemplate.IndexOf(cutProp);
                while (first > 0)
                {
                    int last = reducedTemplate.IndexOf(cutProp, first + 1) + cutProp.Length;
                    reducedTemplate = reducedTemplate.Remove(first, last - first);
                    first = reducedTemplate.IndexOf(cutProp);
                }
            }
            return reducedTemplate;
        }
    }
}