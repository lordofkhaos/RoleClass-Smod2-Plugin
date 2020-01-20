using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace RoleClass
{
	internal class EventHandler : IEventHandlerWaitingForPlayers, IEventHandlerSetRole
	{
		private readonly RoleClass _plugin;
		private Aliases _aliases;

		public EventHandler(RoleClass plugin)
		{
			this._plugin = plugin;
		}

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			_aliases = new Aliases();
			_aliases.Assign();
		}

		private int PlayerItemCount(Player pl)
			=> pl.GetInventory().Count(item => item.ItemType != ItemType.NULL);

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			if (!File.Exists(_plugin.ConfigFile))
				return;

			BinaryFormatter formatter = new BinaryFormatter();
			Dictionary<string, Dictionary<string, List<string>>> table = null;
			using (FileStream fs = File.OpenRead(_plugin.ConfigFile))
			{
				table = (Dictionary<string, Dictionary<string, List<string>>>)formatter.Deserialize(fs);
			}

			foreach (KeyValuePair<string, Dictionary<string, List<string>>> rankRolePair in table)
			{
				if (ev.Player.GetUserGroup().Name.ToLower() != rankRolePair.Key || ev.Player.GetRankName().ToLower() == rankRolePair.Key)
					continue;

				// Get role from aliases and items
				int realRole = -1;
				List<int> items = new List<int>();
				foreach (KeyValuePair<string, List<string>> roleItemPair in rankRolePair.Value)
				{
					foreach (KeyValuePair<int, List<string>> roleAliases in _aliases.RoleAliases.Where(r => r.Value.Contains(roleItemPair.Key)))
					{
						realRole = roleAliases.Key;
						break;
					}

					if (realRole == -1) 
						continue;
					
					foreach (KeyValuePair<int, List<string>> itemAliases in roleItemPair.Value.SelectMany(r => _aliases.ItemAliases.Where(i => i.Value.Contains(r))))
					{
						items.Add(itemAliases.Key);
						break;
					}
					
				}

				if ((int) ev.TeamRole.Role != realRole)
					continue;

				foreach (int item in items)
				{
					ev.Player.GiveItem((ItemType) item);
				}
			}

		}
	}
}