using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RoleClass.Assists;
using Smod2.Commands;

namespace RoleClass.Commands
{
	internal class DeleteCommand : ICommandHandler
	{
		private readonly RoleClass _plugin;
		public DeleteCommand(RoleClass plugin)
		{
			_plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP DEL
			return "Deletes a set of items for the role/class.";
		}

		public string GetUsage()
		{
			return "DEL" + " RANK " + "CLASS";
		}

		public string[] OnCall(ICommandSender sender, string[] args)
		{
			// Don't do anything if legacy mode is disabled
			if (!_plugin.GetConfigBool("krc_legacy_enable")) return new string[] { };
			// Insufficient args
			if (args == null || args.Length < 1) return new[] { GetUsage() };
			if (args[0].ToLower().Compare(StringComparison.OrdinalIgnoreCase, "all", "every", "*"))
			{
				File.Delete(Ancillary.LegacyBinCfgPath);
				return new[] { "File successfully deleted" };
			}
			// Parse the class and role from arguments
			string trueRankName = args[0].ToLower();
			string trueClassName = args[1].ToLower();
			// If the file doesn't exist, return an error message
			if (!File.Exists(Ancillary.LegacyBinCfgPath)) return new[] { "<color=#ff0000>File not found!</color>" };
			Dictionary<string, string> deserialized;
			// Open the file
			using (FileStream fileStream = File.Open(Ancillary.LegacyBinCfgPath, FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				// Deserialize
				deserialized = (Dictionary<string, string>)formatter.Deserialize(fileStream);
			}
			if (!deserialized.ContainsKey(trueRankName))
				return new[] { $"Configuration for {trueRankName}::{trueClassName} not found" };
			
			// Loop through the object 
			foreach (KeyValuePair<string, string> pair in deserialized)
			{
				if (pair.Key != trueRankName || !pair.Value.StartsWith(trueClassName))
					continue;
				// Remove it
				deserialized.Remove(trueRankName);
				File.Delete(Ancillary.LegacyBinCfgPath);
				// Write the object to a new file
				using (StreamWriter sw = new StreamWriter(Ancillary.LegacyBinCfgPath))
				{
					sw.Write(deserialized);
				}
				return new[] { $"Removed configuration for {trueRankName}::{trueClassName}" };
			}

			return new[] { GetUsage() };
		}
	}
}