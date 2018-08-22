using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
//using System.Xml;
//using System.Xml.Serialization;

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

        public class Ranks
        {
            public string RankName { get; set; }
            public string Class { get; set; }
            public List<string> Items { get; set; }
            public string ItemNo { get; set; }
            public string Item { get; set; }
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            #region assign player info
            string player = ev.Player.Name;
            string rank = ev.Player.GetRankName();
            var team = ev.Player.TeamRole.Role;
            #endregion
            #region assign vars to roles
            var ntfc = Role.NTF_COMMANDER.ToString();
            var ntfl = Role.NTF_LIEUTENANT.ToString();
            var ntf = Role.NTF_CADET.ToString();
            var ntfs = Role.NTF_SCIENTIST.ToString();
            var ci = Role.CHAOS_INSUGENCY.ToString();
            var cd = Role.CLASSD.ToString();
            var fg = Role.FACILITY_GUARD.ToString();
            var sci = Role.SCIENTIST.ToString();
            var plaguedaddy = Role.SCP_049.ToString();
            var zombie = Role.SCP_049_2.ToString();
            var larry = Role.SCP_106.ToString();
            var shyguy = Role.SCP_096.ToString();
            var comp = Role.SCP_079.ToString();
            var doggo1 = Role.SCP_939_53.ToString();
            var doggo2 = Role.SCP_939_89.ToString();
            var tut = Role.TUTORIAL.ToString();
            var spec = Role.SPECTATOR.ToString();
            var un = Role.UNASSIGNED.ToString();
            List<string> scps = new List<string>();
            scps.AddRange(new string[] { plaguedaddy, zombie, larry, shyguy, comp, doggo1, doggo2 });
            List<string> humans = new List<string>();
            humans.AddRange(new string[] { ntfc, ntfl, ntfs, ntf, ci, cd, fg, sci, tut });
            List<string> other = new List<string>();
            other.AddRange(new string[] { spec, un });
            List<string> cls = new List<string>();
            cls.AddRange(scps);
            cls.AddRange(humans);
            cls.AddRange(other);
            #endregion
            #region items and stuff
            var kc_ci = ItemType.CHAOS_INSURGENCY_DEVICE.ToString();
            Dictionary<string, ItemType> cidev = new Dictionary<string, ItemType>();
            cidev.Add("CI_DEVICE", ItemType.CHAOS_INSURGENCY_DEVICE);
            cidev.Add("10", ItemType.CHAOS_INSURGENCY_DEVICE);
            var coin = ItemType.COIN.ToString();
            var pew = ItemType.COM15.ToString();
            var kc_ce = ItemType.CONTAINMENT_ENGINEER_KEYCARD.ToString();
            var cup = ItemType.CUP.ToString();
            var disarm = ItemType.DISARMER.ToString();
            var mtfammo = ItemType.DROPPED_5.ToString();
            var pat = ItemType.DROPPED_7.ToString();
            var rat = ItemType.DROPPED_9.ToString();
            var e11 = ItemType.E11_STANDARD_RIFLE.ToString();
            var kc_fm = ItemType.FACILITY_MANAGER_KEYCARD.ToString();
            var fb = ItemType.FLASHBANG.ToString();
            var fl = ItemType.FLASHLIGHT.ToString();
            var frag = ItemType.FRAG_GRENADE.ToString();
            var kc_gu = ItemType.GUARD_KEYCARD.ToString();
            var kc_jan = ItemType.JANITOR_KEYCARD.ToString();
            var cigun = ItemType.LOGICER.ToString();
            var kc_msci = ItemType.MAJOR_SCIENTIST_KEYCARD.ToString();
            var med = ItemType.MEDKIT.ToString();
            var micro = ItemType.MICROHID.ToString();
            var mp7 = ItemType.MP4.ToString();
            var kc_ntfc = ItemType.MTF_COMMANDER_KEYCARD.ToString();
            var kc_ntfl = ItemType.MTF_LIEUTENANT_KEYCARD.ToString();
            var none = ItemType.NULL.ToString();
            var kc_o5 = ItemType.O5_LEVEL_KEYCARD.ToString();
            var p90 = ItemType.P90.ToString();
            var kc_sci = ItemType.SCIENTIST_KEYCARD.ToString();
            var kc_mg = ItemType.SENIOR_GUARD_KEYCARD.ToString();
            var wmt = ItemType.WEAPON_MANAGER_TABLET.ToString();
            var kc_zm = ItemType.ZONE_MANAGER_KEYCARD.ToString();
            List<string> keycards = new List<string>();
            keycards.AddRange(new string[] { kc_ce, kc_ci, kc_fm, kc_gu, kc_mg, kc_o5, kc_zm, kc_jan, kc_sci, kc_msci, kc_ntfc, kc_ntfl });
            List<string> weapons = new List<string>();
            weapons.AddRange(new string[] { pew, micro, cigun, e11, mp7, p90, frag, fb });
            List<string> ammo = new List<string>();
            ammo.AddRange(new string[] { mtfammo, pat, rat });
            List<string> accessories = new List<string>();
            accessories.AddRange(new string[] { fl, coin, cup, wmt });
            List<string> masteritems = new List<string>();
            masteritems.AddRange(keycards);
            masteritems.AddRange(weapons);
            masteritems.AddRange(ammo);
            masteritems.AddRange(accessories);
            #endregion
            string[] clss = cls.ToArray();
            foreach (string line in clss) { plugin.Debug(line); }
            string path = @"rc-config.dat";
            plugin.Debug("Player " + player + " rank: " + rank);
            plugin.Debug("Player " + player + "team: " + team);
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
                if (rank != null && team != Role.SPECTATOR) 
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

            string rankName = null;
            string[] classitems = null;
            List<string> clitems = new List<string>();
            string cl = null;
            string[] items = null;

            Hashtable table = null;

            FileStream fs = new FileStream(path, FileMode.Open);
            try 
            {
                BinaryFormatter formatter = new BinaryFormatter();
                table = (Hashtable)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                plugin.Error("Failed to load file: " + e);
                throw;
            }
            finally
            {
                fs.Close();
            }

            foreach (DictionaryEntry x in table)
            {
                rankName = x.Key.ToString();
                clitems.Add(x.Value.ToString());
            }

            classitems = clitems.ToArray();
            cl = classitems[0];

            items = classitems.Skip(1).ToArray();

            if (rank == rankName && cls.Contains(cl))
            {
                try 
                {
                    if (scps.Contains(cl))
                    {
                        plugin.Warn("Trying to give items to SCPs is inadvisable");
                        foreach (string item in items)
                        {
                            if (masteritems.Contains(item))
                            {
                                //ev.Player.GiveItem(item);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    plugin.Error("Cannot exeute code. Error: " + e);
                    throw;
                }
            }

        }
    }
}

