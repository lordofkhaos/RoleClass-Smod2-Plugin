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
	internal class RoleClass : Plugin
	{
		public override void OnDisable()
		{
			Info("RoleClass disabled!");
		}

		public override void OnEnable()
		{
			Info("RoleClass loaded successfully!");
		}

		private EventHandler _events;

		public override void Register()
		{
			_events = new EventHandler(this);
			// Register Events
			AddEventHandler(typeof(IEventHandlerPlayerJoin), _events, Priority.Low);
			AddEventHandler(typeof(IEventHandlerSetRole), _events);
			// Register config settings
			AddConfig(new Smod2.Config.ConfigSetting("krc_enable", true, Smod2.Config.SettingType.BOOL, true, "Enable RoleClass"));
			AddConfig(new Smod2.Config.ConfigSetting("krc_enable_legacy", true, Smod2.Config.SettingType.BOOL, true, "Enable the legacy features of RoleClass")); // This is true for now
			// legacy
			AddCommands(new[] { "save", "add", "nentry" }, new Commands.SaveCommand(this));
			AddCommands(new[] { "del", "rem", "rentry" }, new Commands.DeleteCommand(this));
			AddCommands(new[] { "list", "entries" }, new Commands.ListCommand(this));
			// tba
		}
	}
}
