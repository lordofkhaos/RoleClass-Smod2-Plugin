using RoleClass;
using RoleClass.Commands;
using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using Smod2.API;
using System.Linq;
using System;

namespace RoleClass
{
	[PluginDetails(
		author = MetaData.Author,
		name = MetaData.Name,
		description = MetaData.Description,
		id = MetaData.ID,
		version = MetaData.Version,
		SmodMajor = 3,
		SmodMinor = 1,
		SmodRevision = 16
		)]
	class RoleClass : Plugin
	{
		public override void OnDisable()
		{
			this.Info("RoleClass disabled!");
		}

		public override void OnEnable()
		{
			this.Info("RoleClass loaded successfully!");
		}

		private EventHandler events;

		public override void Register()
		{
			events = new EventHandler(this);
			// Register Events
			this.AddEventHandler(typeof(IEventHandlerPlayerJoin), events, Priority.Low);
			this.AddEventHandler(typeof(IEventHandlerSetRole), events, Priority.Highest);
			// Register Commands
			this.AddCommands(new string[] { "save", "add", "nentry" }, new Commands.SaveCommand());
			// Register config settings
			//this.AddConfig(new Smod2.Config.ConfigSetting("k_whitelist", null, Smod2.Config.SettingType.LIST, true, ""));
			this.AddConfig(new Smod2.Config.ConfigSetting("k_enable", true, Smod2.Config.SettingType.BOOL, true, "Enable RoleClass"));
			this.AddConfig(new Smod2.Config.ConfigSetting("k_global_give", new Dictionary<string, string>() { }, true, Smod2.Config.SettingType.DICTIONARY, true, "Roles and items"));
			// experimental
			this.AddConfig(new Smod2.Config.ConfigSetting("k_roleclass", string.Empty, Smod2.Config.SettingType.STRING, true, ""));
			this.AddCommands(new string[] { "del", "rem", "rentry" }, new Commands.DeleteCommand());
			this.AddCommands(new string[] { "list", "entries" }, new Commands.ListCommand());
			// tba

			
		}

		public Dictionary<string, Dictionary<string, List<ItemType>>> GetRoleClassConfig()
		{
			Aliases aliases = new Aliases();
			aliases.AssignAliases();
			var yeet = ConfigFile.GetDict("k_roleclass"); // format= string: list,of,strings
			var endyeet = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			foreach (KeyValuePair<string, string> roleClassPair in yeet)
			{
				string rank = roleClassPair.Key;
				string klasy = roleClassPair.Value.Split('[')[0];
				string[] itemsArray = roleClassPair.Value.Split('[');
				List<string> itemsButTheyreStrings = new List<string>(itemsArray).Skip(1).ToList();
				List<ItemType> items = new List<ItemType>();
				Dictionary<string, List<ItemType>> classItems = new Dictionary<string, List<ItemType>>();

				try { itemsButTheyreStrings.Remove("]"); }
				catch (Exception e) { this.Error($"[MESSAGE]: {e.Message}{Environment.NewLine}[STACKTRACE]: {e.StackTrace} "); }
				foreach (string item in itemsButTheyreStrings)
				{
					int typeOfItem = aliases.Keycards.ContainsKey(item)
								? 0
								: aliases.Weapons.ContainsKey(item)
								? 1
								: aliases.Accessories.ContainsKey(item)
								? 2
								: aliases.Ammo.ContainsKey(item)
								? 3
								: -1;
					ItemType myItem = ItemType.NULL;
					switch (typeOfItem)
					{
						case 0:
							myItem = aliases.Keycards[item];
							break;
						case 1:
							myItem = aliases.Weapons[item];
							break;
						case 2:
							myItem = aliases.Accessories[item];
							break;
						case 3:
							myItem = aliases.Ammo[item];
							break;
						case -1:
							this.Warn($"Item {item} not found!");
							break;
						default:
							this.Warn($"Item {item} unavailable!");
							break;
					}
					items.Add(myItem);
				}

				classItems.Add(klasy, items);
				endyeet.Add(rank, classItems);
			}

			return endyeet;
		}
	}
}
