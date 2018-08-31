﻿using RoleClass;
using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

namespace RoleClass
{
	[PluginDetails(
		author = "lordofkhaos",
		name = "RoleClass",
		description = "Give certain items to roles",
		id = "com.lordofkhaos.roleclass",
		version = "1.0",
		SmodMajor = 3,
		SmodMinor = 1,
		SmodRevision = 12
		)]
	class RoleClass : Plugin
	{
		public override void OnDisable()
        {
            this.Info("Plugin disabled!");
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
            this.AddCommand("save", new Commands());
            // Register config settings
            this.AddConfig(new Smod2.Config.ConfigSetting("k_enable", true, Smod2.Config.SettingType.BOOL, true, "Enable RoleClass"));
            this.AddConfig(new Smod2.Config.ConfigSetting("k_global_give", new Dictionary<string, string>() {}, true, Smod2.Config.SettingType.DICTIONARY, true, "Roles and items"));
            // tba
		}
	}
}
