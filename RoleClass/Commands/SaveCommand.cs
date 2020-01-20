using Smod2.Commands;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RoleClass.Commands
{
	internal class SaveCommand : ICommandHandler
	{
		private RoleClass plugin;

		public void SaveCmd(RoleClass plugin)
		{
			this.plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP SAVE
			return "Saves a set of items for the role/class.";
		}

		public string GetUsage()
		{
			return "SAVE" + " RANK " + "CLASS " + "ITEMS LIST";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			if (args == null || args.Length < 2)
				return new[] { "Bad format of command args." };

			// Assign variables
			string rank = null, role = null;
			List<string> items = new List<string>();
			for (int i = 0; i < args.Length; i++)
			{
				switch (i)
				{
					case 0:
						rank = args[i].ToLower();
						break;

					case 1:
						role = args[i].ToLower();
						break;

					default:
						items.Add(args[i]);
						break;
				}
			}
			Dictionary<string, Dictionary<string, List<string>>> data = new Dictionary<string, Dictionary<string, List<string>>>
			{
				{ rank, new Dictionary<string, List<string>> { { role, items } } }
			};

			IFormatter formatter = new BinaryFormatter();
			// Read existing data
			Dictionary<string, Dictionary<string, List<string>>> table = null;
			if (File.Exists(plugin.ConfigFile))
			{
				using (FileStream fs = File.OpenRead(plugin.ConfigFile))
				{
					table = (Dictionary<string, Dictionary<string, List<string>>>)formatter.Deserialize(fs);
				}
			}

			// Add new data to file
			if (table == null)
				table = new Dictionary<string, Dictionary<string, List<string>>>();

			foreach (KeyValuePair<string, Dictionary<string, List<string>>> range in data)
			{
				table.Add(range.Key, range.Value);
			}

			using (FileStream fs = File.Open(plugin.ConfigFile, FileMode.OpenOrCreate))
			{
				formatter.Serialize(fs, table);
				return new string[] { "Saved configuration for " + rank + ":" + role };
			}
		}
	}
}