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
using Smod2;
using Smod2.API;
using Smod2.Commands;

namespace RoleClass.Commands
{
	class ListCommand : ICommandHandler
	{
		private RoleClass plugin;
		public void ListCmd(RoleClass plugin)
		{
			this.plugin = plugin;
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
			//if (sender is Player pl)
			//{
			//    //Player a = (Player)sender;
			//    var s64 = pl.SteamId;
			//    string inf = "Player: " + pl;
			//    string inf2 = "Steamid: " + s64;
			//    if (!string.IsNullOrEmpty(pl.SteamId))
			//    {
			//        return new string[] { inf + inf2 };
			//        //plugin.Debug(inf2);
			//    }
			//}
			Dictionary<string, string> myDeserializedData = new Dictionary<string, string>();
			if (args != null && args.Length > 0)
			{
				string trueRankName = args[0].ToLower();
				// cleanup old files
				string path = @"rc-config.dat";
				string path2 = @"dictionary.bin";
				if (File.Exists(path))
					File.Delete(path);
				if (File.Exists(path2))
					File.Delete(path2);
				//
				// parse class
				string trueClassName = args[1].ToLower();
				IFormatter formatter = new BinaryFormatter();
				List<string> configurations = new List<string>();
				if (File.Exists("roleclass.cfgbin"))
				{
					using (FileStream fileStream = File.Open("roleclass.cfgbin", FileMode.OpenOrCreate))
					{
						myDeserializedData = (Dictionary<string, string>)formatter.Deserialize(fileStream);
					}
					foreach (KeyValuePair<string, string> pair in myDeserializedData)
					{
						string myClassName = pair.Value.Substring(trueClassName.Length);
						if (pair.Key == trueRankName && pair.Value.StartsWith(myClassName, StringComparison.Ordinal))
						{
							configurations.Add(pair.Value);
						}
					}
					string confClassName = string.Empty;
					string confItems = string.Empty;
					foreach (string configuration in configurations)
					{
						int i = configuration.Length;
						while (!Aliases.Humans.ContainsKey(configuration) && !Aliases.SCPs.ContainsKey(configuration) && Aliases.Other.ContainsKey(configuration))
						{
							configuration.Remove(i);
							i--;
							if (i == 0)
								break;
						}
						confClassName = configuration;
					}
					foreach (string configuration in configurations)
					{
						if (configuration.StartsWith(confClassName, StringComparison.Ordinal) && !string.IsNullOrEmpty(confClassName))
							confItems = configuration.Replace(confClassName, "");
					}
					return new string[] { };
				}
				else
				{
					return new string[] { "No configuration file found!" };
				}
			}
			return new string[] { GetUsage() };

		}
	}
}

//***** Welcome to the bottom of the file *****
//**** Here are several things I wrote out to help me code: ****
//
//** Example Commands: **
//save something something 15,2,26,1
//save owner something 1,2,3
//save laneklfhak sakfneoiqoia 928,129u48,127487
//
//** Example Xml: **
//* Note: XML is to-be-added *
//<ranks>
//  <owner class="scientist">
//      <items>
//          <item1>SomeItem</item1>
//          <item2>SomeOtherItem</item2>
//      </items>
//  </owner>
//  <admin class="class-d">
//      <items>
//          <item1>SomeItem</item1>
//          <item2>CouldBeAnyItem</item2>
//          <item3>CanHaveAsManyAsYouWant</item3>
//      </items>
//  </admin>
//</ranks>