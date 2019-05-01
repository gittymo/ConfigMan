using System;
using System.Collections.Generic;
using System.Xml;

namespace ConfigMan
{
	public class ConfigModule
	{
		private void addChildNode(XmlNode childNode)
		{
			string keyAttr = (this.attributes.ContainsKey("keyAttr")) ? this.attributes["keyAttr"] : null;
			if (keyAttr != null)
			{
				string moduleId = childNode.Attributes[keyAttr].Value;
				this.childModules.Add(moduleId, new ConfigModule(childNode));
			} else
			{
				string moduleId = childNode.Name;
				this.childModules.Add(moduleId, new ConfigModule(childNode));
			}
		}
		public ConfigModule(XmlNode moduleNode)
		{
			attributes = new Dictionary<string, string>();
			XmlAttributeCollection xmlAttributes = moduleNode.Attributes;
			foreach (XmlAttribute xmlAttribute in xmlAttributes)
				attributes.Add(xmlAttribute.Name, xmlAttribute.Value);

			if (moduleNode.HasChildNodes)
			{
				childModules = new Dictionary<string, ConfigModule>();
				foreach (XmlNode childNode in moduleNode.ChildNodes)
					addChildNode(childNode);
				this.root = true;
				this.type = moduleNode.Name;
			}
			else
			{
				this.childModules = null;
				this.root = false;
				this.type = moduleNode.ParentNode.Name;
			}
		}

		public Dictionary<string, string> Attributes => this.attributes;
		public Dictionary<string, ConfigModule> SubModules => this.childModules;

		public ConfigModule getSubModule(string moduleName) => (this.childModules.ContainsKey(moduleName) ? this.childModules[moduleName] : null);

		public string getAttribute(string attributeName) => (this.attributes.ContainsKey(attributeName) ? this.attributes[attributeName] : null);

		private string type;    // Defines the type of module this confiruation is for.
		private Dictionary<string, string> attributes;
		private Dictionary<string, ConfigModule> childModules;
		private readonly bool root;
	}
}
