using Smod.TestPlugin;
using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;

namespace ExamplePlugin
{
	[PluginDetails(
		author = "lordofkhaos",
		name = "RoleClass",
		description = "Give certain items to roles",
		id = "lordofkhaos.roleclass",
		version = "3.0",
		SmodMajor = 3,
		SmodMinor = 1,
		SmodRevision = 12
		)]
	class ExamplePlugin : Plugin
	{
		public override void OnDisable()
        {
            this.Info("Plugin disabled!");
        }

		public override void OnEnable()
		{
			this.Info("Test Plugin has loaded :)");
			this.Info("Config value: " + this.GetConfigString("test"));
		}

		public override void Register()
		{
			// Register Events
			this.AddEventHandlers(new EventHandler(this));
			// Register Commands
			//this.AddCommand("hello", new Commands(this));
			// Register config settings
			this.AddConfig(new Smod2.Config.ConfigSetting("test", "yes", Smod2.Config.SettingType.STRING, true, "test"));
            this.AddConfig(new Smod2.Config.ConfigSetting("k_roleclass", new Dictionary<string, string>(), true, Smod2.Config.SettingType.DICTIONARY, true, "Roles and items"));
		}
	}
}
