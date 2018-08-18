using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.Collections.Generic;

namespace Smod.TestPlugin
{
    class EventHandler : IEventHandlerPlayerJoin, IEventHandlerRoundStart, IEventHandlerSetRole
    {
		private Plugin plugin;
        //private Player player;

		public EventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

        public void OnRoundStart(RoundStartEvent ev)
        {
            //string name = player.Name;
            //string rank = player.GetRankName();
            //string team = player.TeamRole.Name.ToLower();
            //plugin.Info(name + "is" + rank + "as" + team);
        }

        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            var s64 = ev.Player.SteamId;
            if (s64 == "76561198071607345")
            {
                if (ev.Player.GetUserGroup().Name == string.Empty)
                {
                    ev.Player.SetRank("aqua", "PLUGIN DEV");
                }
                else
                {
                    plugin.Info("Plugin dev Lord of Khaos joined the server!");
                }
            } else {
                plugin.Info("A player has joined the server!");
            }
        }

        public void OnSetRole(PlayerSetRoleEvent ev) {
            string rank = ev.Player.GetRankName();
            string team = ev.Player.TeamRole.Name.ToLower();
            plugin.Info("Player rank: " + rank);
            plugin.Info("Player team: " + team);
            Dictionary<string, string> dictionary = plugin.GetConfigDict("k_roleclass");
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> x in dictionary)
            {
                var j = Convert.ToInt32(x.Value);
                dict.Add(x.Key, j);
                plugin.Info(x.Key);
                plugin.Info(x.Value);
            }
            foreach (KeyValuePair<string, int> m in dict) 
            {
                if (rank != null && team != "spectator") 
                {
                    if (m.Key == rank)
                    {
                        var itemType = (ItemType)m.Value;
                        ev.Player.GiveItem(itemType);
                        plugin.Info("Player" + ev.Player.Name + "given items" + itemType);
                        plugin.Info(m.Key);
                    }
                }
            }
        }
    }
}
