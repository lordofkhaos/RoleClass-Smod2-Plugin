﻿using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

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
	}
}
