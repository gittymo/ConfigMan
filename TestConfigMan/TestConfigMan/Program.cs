using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigMan;

namespace TestConfigMan
{
	class Program
	{
		static void Main(string[] args)
		{
			ConfigModule conf = ConfigLoader.loadConfig("test_config.xml");
			Dictionary<string, ConfigModule> parts = conf.SubModules;
			foreach (KeyValuePair<string, ConfigModule> configModule in parts)
			{
				Console.WriteLine(configModule.Key);
				if (configModule.Value.SubModules != null)
				{
					foreach (KeyValuePair<string, ConfigModule> subModule in configModule.Value.SubModules)
					{
						Console.WriteLine(subModule.Key);
					}
				}
			}

			ConfigModule dbConf = conf.getSubModule("DatabaseConnection");
			foreach (KeyValuePair<string, ConfigModule> connection in dbConf.SubModules) {
				Console.WriteLine("Database connection: " + connection.Key);
				foreach (KeyValuePair<string, string> attribute in connection.Value.Attributes)
				{
					Console.WriteLine("\t" + attribute.Key + " = " + attribute.Value);
				}
			}

			ConfigModule onBaseConn = dbConf.getSubModule("onbase");
			Console.WriteLine(onBaseConn.getAttribute("server"));
		}
	}
}