using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.IO;

namespace RoleClass
{
	[PluginDetails(
		author = "lordofkhaos",
		name = "RoleClass",
		description = "Give certain items to roles",
		configPrefix = "krc",
		id = "com.lordofkhaos.roleclass",
		version = "2.0b", // true v2.0 will come with Smod-Atlas
		SmodMajor = 3,
		SmodMinor = 6,
		SmodRevision = 0
		)]
	internal class RoleClass : Plugin
	{
		public readonly string ConfigFile = "roleclass.dat";

		public override void OnDisable()
		{
			this.Info("RoleClass disabled!");
		}

		public override void OnEnable()
		{
			this.Info("RoleClass loaded successfully!");

			// Remove all old files
			if (File.Exists("rc-config.dat"))
				File.Delete("rc-config.dat");
			if (File.Exists("dictionary.bin"))
				File.Delete("dictionary.bin");
			if (File.Exists("roleclass.cfgbin"))
				File.Delete("roleclass.cfgbin");
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
			this.AddConfig(new Smod2.Config.ConfigSetting("enable", true, true, "Enable RoleClass"));
			this.AddCommands(new string[] { "del", "rem", "rentry" }, new Commands.DeleteCommand());
		}
	}
}