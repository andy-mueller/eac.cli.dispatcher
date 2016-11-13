using System;
using System.Collections.Generic;
using System.Linq;

namespace cli.dispatcher
{
    public class StringTemplate
    {
        private readonly string template;

        StringTemplate(string template)
        {
            this.template = template;
        }

        public static StringTemplate of(string template)
        {
            return new StringTemplate(template);
        }

        public string Replace(IEnumerable<KeyValuePair<string, string>> properties)
        {
            return properties.Aggregate(this.template, Replace);
        }

        private static string Replace(string template, KeyValuePair<string, string> property)
        {
            return template.Replace(format(property.Key), property.Value);
        }

        private static string format(string prop)
        {
            return string.Format("%{0}%",prop);
        }

        public StringTemplate CutOut(string cutmark)
        {
            String reducedTemplate = template;
            string cutProp = format(cutmark);
            reducedTemplate = CutFromString(reducedTemplate, cutProp);
            return StringTemplate.of(reducedTemplate);
        }

        public StringTemplate Cut(string cutmark)
        {
            return new StringTemplate(template.Replace(format(cutmark), string.Empty));
        }

        private static string CutFromString(string str, string cutout)
        {
            int first = str.IndexOf(cutout);
            if (first < 0) return str;
            int last = str.IndexOf(cutout, first + 1) + cutout.Length;
            str = str.Remove(first, last - first);
            return CutFromString(str, cutout);
        }

        public string asString()
        {
            return template;
        }
    }
}