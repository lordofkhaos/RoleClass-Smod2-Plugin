using Smod2;
using Smod2.Attributes;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using RoleClass.Essential;

namespace RoleClass
{
	[PluginDetails(
		author = MetaData.Author,
		name = MetaData.Name,
		description = MetaData.Description,
		id = MetaData.Id,
		version = MetaData.Version,
		SmodMajor = MetaData.SmodMajor,
		SmodMinor = MetaData.SmodMinor,
		SmodRevision = MetaData.SmodBuild
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
			this.AddEventHandler(typeof(IEventHandlerSetRole), events, Priority.Normal);
			// Register config settings
			this.AddConfig(new Smod2.Config.ConfigSetting("krc_enable", true, Smod2.Config.SettingType.BOOL, true, "Enable RoleClass"));
			this.AddConfig(new Smod2.Config.ConfigSetting("krc_enable_legacy", true, Smod2.Config.SettingType.BOOL, true, "Enable the legacy features of RoleClass")); // This is true for now
			// legacy
			this.AddCommands(new string[] { "save", "add", "nentry" }, new Commands.SaveCommand());
			this.AddCommands(new string[] { "del", "rem", "rentry" }, new Commands.DeleteCommand());
			this.AddCommands(new string[] { "list", "entries" }, new Commands.ListCommand());
			// tba
		}
	}
}
