using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

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
            string[] players = new string[ev.Server.GetPlayers().Count];
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
                plugin.Debug("A player has joined the server!");
            }
        }

        public class Details
        {
            public string RankName { get; set; }
            public string Class { get; set; }
            public List<string> Items { get; set; }
            public string ItemNo { get; set; }
            public string Item { get; set; }
        }

        public void OnSetRole(PlayerSetRoleEvent ev) {
            string rank = ev.Player.GetRankName();
            string team = ev.Player.TeamRole.Name.ToLower();
            //string path = @"rc-config.xml";
            plugin.Debug("Player rank: " + rank);
            plugin.Debug("Player team: " + team);
            Dictionary<string, string> dictionary = plugin.GetConfigDict("k_global_give");
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> x in dictionary)
            {
                if (int.TryParse(x.Value, out int myValue))
                {
                    dict.Add(x.Key, myValue);
                }
                else 
                {
                    plugin.Error(myValue + " is not a number!");
                }
            }
            foreach (KeyValuePair<string, int> m in dict) 
            {
                if (rank != null && team != "spectator" && team != "ghost") 
                {
                    if (m.Key == rank)
                    {
                        var itemType = (ItemType)m.Value;
                        ev.Player.GiveItem(itemType);
                        plugin.Debug("Player " + ev.Player.Name + " given item " + itemType);
                        plugin.Debug(m.Key);
                    }
                }
            }

            //List<string> itemlist = new List<string>();
            //List<string> ranknames = new List<string>();
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(path);
            //XmlNodeList rankNodes = xmlDoc.SelectNodes("//ranks");
            //foreach(XmlNode rankNode in rankNodes) {
            //    var ranks = rankNode.ChildNodes;
            //    foreach (XmlNode rnk in ranks)
            //    {
            //        ranknames.Add(rnk.ToString());
            //        var items = rankNode.ChildNodes;
            //        foreach (XmlNode item in items)
            //        {
            //            itemlist.Add(item.InnerText);
            //        }
            //    }
            //}


        }
    }
}
