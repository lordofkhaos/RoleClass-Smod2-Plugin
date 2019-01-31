using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
	internal class ListCommand : ICommandHandler
	{
		private readonly RoleClass _plugin;

		public ListCommand(RoleClass plugin)
		{
			_plugin = plugin;
		}

		public string GetCommandDescription()
		{
			// This prints when someone types HELP SAVE
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
			// If the file doesn't exist, return an error message
			if (!File.Exists(Ancillary.LegacyBinCfgPath)) return new[] { "<color=#ff0000>File not found!</color>" };
			Dictionary<string, string> deserialized;
			// Open the file
			using (FileStream fileStream = File.Open(Ancillary.LegacyBinCfgPath, FileMode.Open))
			{
				IFormatter formatter = new BinaryFormatter();
				// Deserialize
				deserialized = (Dictionary<string, string>) formatter.Deserialize(fileStream);
			}
			// Null check
			if (deserialized == null) return new[] { GetUsage() };
			
			// Initialize the end variable
			string[] result = new string[deserialized.Count];
			int place = 0;
			// Add each to the end variable
			foreach (KeyValuePair<string, string> t in deserialized)
			{
				result[place] = $"{t.Key}::{t.Value}";
				place++;
			}
			// Return
			return result;
		}
	}
}