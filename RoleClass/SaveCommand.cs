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
	class SaveCommand : ICommandHandler
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
			if (!plugin.GetConfigBool("krc_legacy_enable")) return new string[] { };
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

			if (args != null && args.Length > 0)
			{
				string trueRankName = args[0].ToLower();
				// cleanup old files
				string path = "rc-config.dat";
				string path2 = "dictionary.bin";
				if (File.Exists(path))
					File.Delete(path);
				if (File.Exists(path2))
					File.Delete(path2);
				//
				if (args.Length > 1)
				{
					// parse class
					string trueClassName = args[1].ToLower();
					if (args != null && args.Length > 2)
					{
						int len = args.Length - 2;
						// parse items
						List<string> itemList = new List<string>();

						int i = 2;
						while (i < args.Length)
						{
							itemList.Add(args[i]);
							i++;
						}

						string[] itemArray = itemList.ToArray();

						List<string> classItems = new List<string>();
						if (trueClassName != null)
						{
							classItems.Add(trueClassName);
						}

						i = 0;
						while (i < itemArray.Length)
						{
							int j = i + 1;
							classItems.Add(itemArray[i]);
							i++;
						}

						Dictionary<string, List<string>> serializingData = new Dictionary<string, List<string>>()
						{
							[trueRankName] = classItems
						};

						IFormatter formatter = new BinaryFormatter();
						if (!File.Exists("roleclass.cfgbin"))
						{
							using (FileStream fileSteam = File.Open("roleclass.cfgbin", FileMode.OpenOrCreate))
							{
								formatter.Serialize(fileSteam, serializingData);
								return new string[] { "Saved configuration for " + trueRankName + ":" + trueClassName };
							}
						}
						using (FileStream fileStream = File.Open("roleclass.cfgbin", FileMode.Append))
						{
							formatter.Serialize(fileStream, serializingData);
							return new string[] { "Saved configuration for " + trueRankName + ":" + trueClassName };
						}
					}
					return new string[] { GetUsage() };
				}
				return new string[] { GetUsage() };
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