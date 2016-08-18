using System;
using System.Collections.Generic;
using System.Configuration;

namespace cli.dispatcher.configuration
{
    [ConfigurationCollection(typeof(CliTemplateConfigurationElement), AddItemName = "cli-template", CollectionType =ConfigurationElementCollectionType.BasicMap)]
    public class CliTemplateConfigurationElementsCollection : ConfigurationElementCollection, IEnumerable<CliTemplateConfigurationElement>
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            get { return "cli-template"; }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new  CliTemplateConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CliTemplateConfigurationElement)element).Executable;
        }

        IEnumerator<CliTemplateConfigurationElement> IEnumerable<CliTemplateConfigurationElement>.GetEnumerator()
        {
            foreach(CliTemplateConfigurationElement elem in this)
            {
                yield return elem;
            }
        }
    }
}
