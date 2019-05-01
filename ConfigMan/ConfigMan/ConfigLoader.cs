using System.Xml;
using System.IO;

namespace ConfigMan
{
	public class ConfigLoader
	{
		public static ConfigModule loadConfig(string configFilePath)
		{
			try
			{
				if (string.IsNullOrEmpty(configFilePath) || !File.Exists(configFilePath)) return null;
				XmlDocument configDoc = new XmlDocument();
				configDoc.Load(configFilePath);
				XmlNode documentRootNode = configDoc.DocumentElement;
				return new ConfigModule(documentRootNode);
			} catch (XmlException)
			{
				return null;
			}
		}
	}
}
