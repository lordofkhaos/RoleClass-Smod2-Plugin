using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.Collections.Generic;

namespace Smod.TestPlugin
{
    class RoundEventHandler : IEventHandlerPlayerJoin
    {
		private Plugin plugin;

		public RoundEventHandler(Plugin plugin)
		{
			this.plugin = plugin;
		}

        public void OnPlayerJoin(PlayerJoinEvent ev) {
            if (ev.Player.SteamId == "76561198071607345" && ev.Player.GetUserGroup().Name == string.Empty)
                ev.Player.SetRank("aqua", "PLUGIN DEV");
        }

        public void OnPlayerSpawn(PlayerSpawnEvent ev) {
            string rank = ev.Player.GetRankName();
            Dictionary<string, string> dictionary = plugin.GetConfigDict("k_roleclass");
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> x in dictionary)
            {
                var j = Convert.ToInt32(x.Value);
                dict.Add(x.Key, j);
            }
            foreach (KeyValuePair<string, int> m in dict) 
            {
                if (rank != null) 
                {
                    if (m.Key == rank)
                    {
                        var itemType = (ItemType)m.Value;
                        ev.Player.GiveItem(itemType);
                    }
                }
            }
        }
    }
}
