using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
//using System.Xml;
//using System.Xml.Serialization;
using System.Threading;
using RoleClass;
using RoleClass.Assists;
using Smod2;
using Smod2.API;
using Smod2.Commands;

namespace RoleClass.Commands
{
	class SaveCommand : ICommandHandler
	{
		private readonly RoleClass _plugin;
		public SaveCommand(RoleClass plugin)
		{
			_plugin = plugin;
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
			if (!_plugin.GetConfigBool("krc_legacy_enable")) return new string[] { };
			// Check args
			if (args == null || args.Length < 3) return new[] { GetUsage() };
			// Parse the rank from the args
			string trueRankName = args[0].ToLower();
			string className = args[1].ToLower();
			// parse items
			string[] classItemList = args.Skip(2).Select(a => a.ToLowerInvariant()).ToArray();

			Dictionary<string, string> serializingData = new Dictionary<string, string>()
			{
				[trueRankName] = string.Join(",", classItemList)
			};

			IFormatter formatter = new BinaryFormatter();
			if (!File.Exists(Ancillary.LegacyBinCfgPath))
			{
				using (FileStream fileSteam = File.Open(Ancillary.LegacyBinCfgPath, FileMode.OpenOrCreate))
				{
					formatter.Serialize(fileSteam, serializingData);
					return new[] { "Saved configuration for " + trueRankName + ":" + className };
				}
			}
			using (FileStream fileStream = File.Open(Ancillary.LegacyBinCfgPath, FileMode.Append))
			{
				formatter.Serialize(fileStream, serializingData);
				return new[] { "Saved configuration for " + trueRankName + ":" + className };
			}

		}
	}
}