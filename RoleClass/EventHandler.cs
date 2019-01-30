using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using RoleClass;
using RoleClass.Assists;


namespace RoleClass
{
	internal class EventHandler : IEventHandlerPlayerJoin, IEventHandlerSetRole
	{
		private const string ErrorMessage = "Invalid item provided!";
		readonly Plugin plugin;
		//private Player player;

		public EventHandler(Plugin plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			// Make sure it's backwards compatible
			Ancillary.BackwardsCompat();
			
		}

		public void OnPlayerJoin(PlayerJoinEvent ev)
		{
			var s64 = ev.Player.SteamId;
			if (s64 != "76561198071607345") return;
			if (ev.Player.GetUserGroup().Name.StartsWith("[", StringComparison.Ordinal) ||
			    ev.Player.GetUserGroup().Name == string.Empty)
				ev.Player.SetRank("aqua", "PLUGIN DEV");
			else
				plugin.Info("Plugin dev " + ev.Player.Name + " joined the server!");
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			try
			{
				#region Use JSON Config

				// Read the JSON config
				Dictionary<string, Dictionary<string, List<ItemType>>> jsonConfig = Ancillary.ReadSpecialJsonConfig();
				// Give items
				ev.GivePlayerItems(jsonConfig);

				#endregion

				#region Use Normal Config

				// Read the normal config
				Dictionary<string, Dictionary<string, List<ItemType>>> krcConfig = Ancillary.ReturnSpecialConfig("krc_items");
				// Give items
				ev.GivePlayerItems(krcConfig);
				#endregion
			}
			catch (Exception e) { plugin.Error($"[MESSAGE]: {e.Message}{Environment.NewLine}[STACKTRACE]: {e.StackTrace} "); }
		}
	}
}

