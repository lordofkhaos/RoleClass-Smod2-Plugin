using RoleClass;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
//using System.Xml;
//using System.Xml.Serialization;

namespace RoleClass
{
    public class Info
    {
        public Dictionary<string, Role> SCPs { get; set; }
        public Dictionary<string, Role> Humans { get; set; }
        public Dictionary<string, Role> Other { get; set; }
        public Dictionary<string, ItemType> Keycards { get; set; }
        public Dictionary<string, ItemType> Weapons { get; set; }
        public Dictionary<string, ItemType> Ammo { get; set; }
        public Dictionary<string, ItemType> Accessories { get; set; }
    }

    class EventHandler : IEventHandlerPlayerJoin, IEventHandlerSetRole
    {
        readonly Plugin plugin;
        //private Player player;

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            float time = ev.Server.Round.Duration;
            Info info = new Info();
            #region dare ordini agnomes
            // prae perficio
            info.SCPs = new Dictionary<string, Role>();
            info.Humans = new Dictionary<string, Role>();
            info.Other = new Dictionary<string, Role>();
            // perficio

            // cmdr
            var ntfc = Role.NTF_COMMANDER.ToString().ToLower();
            info.Humans["mtfc"] = Role.NTF_COMMANDER;
            info.Humans["ntfc"] = Role.NTF_COMMANDER;
            info.Humans["commander"] = Role.NTF_COMMANDER;
            info.Humans["12"] = Role.NTF_COMMANDER;
            info.Humans[ntfc] = Role.NTF_COMMANDER;

            // lt
            var ntfl = Role.NTF_LIEUTENANT.ToString().ToLower();
            info.Humans["mtfl"] = Role.NTF_LIEUTENANT;
            info.Humans["ntfl"] = Role.NTF_LIEUTENANT;
            info.Humans["lieutenant"] = Role.NTF_LIEUTENANT;
            info.Humans["11"] = Role.NTF_LIEUTENANT;
            info.Humans[ntfl] = Role.NTF_LIEUTENANT;

            // cadet
            var ntf = Role.NTF_CADET.ToString().ToLower();
            info.Humans["cadet"] = Role.NTF_CADET;
            info.Humans["mtf"] = Role.NTF_CADET;
            info.Humans["ntf"] = Role.NTF_CADET;
            info.Humans["13"] = Role.NTF_CADET;
            info.Humans[ntf] = Role.NTF_CADET;

            // msci
            var ntfs = Role.NTF_SCIENTIST.ToString().ToLower();
            info.Humans["mtfs"] = Role.NTF_SCIENTIST;
            info.Humans["ntfs"] = Role.NTF_SCIENTIST;
            info.Humans["mtfsci"] = Role.NTF_SCIENTIST;
            info.Humans["ntfsci"] = Role.NTF_SCIENTIST;
            info.Humans["4"] = Role.NTF_SCIENTIST;
            info.Humans[ntfs] = Role.NTF_SCIENTIST;

            var ci = Role.CHAOS_INSUGENCY.ToString().ToLower();
            info.Humans["ci"] = Role.CHAOS_INSUGENCY;
            info.Humans["chaos"] = Role.CHAOS_INSUGENCY;
            info.Humans["insurgency"] = Role.CHAOS_INSUGENCY;
            info.Humans["insurgent"] = Role.CHAOS_INSUGENCY;
            info.Humans["8"] = Role.CHAOS_INSUGENCY;
            info.Humans[ci] = Role.CHAOS_INSUGENCY;

            var cd = Role.CLASSD.ToString().ToLower();
            info.Humans["classd"] = Role.CLASSD;
            info.Humans["dclass"] = Role.CLASSD;
            info.Humans["cd"] = Role.CLASSD;
            info.Humans["dc"] = Role.CLASSD;
            info.Humans["1"] = Role.CLASSD;
            info.Humans[cd] = Role.CLASSD;

            var fg = Role.FACILITY_GUARD.ToString().ToLower();
            info.Humans["guard"] = Role.FACILITY_GUARD;
            info.Humans["fg"] = Role.FACILITY_GUARD;
            info.Humans["15"] = Role.FACILITY_GUARD;
            info.Humans[fg] = Role.FACILITY_GUARD;

            var sci = Role.SCIENTIST.ToString().ToLower();
            info.Humans["sci"] = Role.SCIENTIST;
            info.Humans["nerd"] = Role.SCIENTIST;
            info.Humans["scientist"] = Role.SCIENTIST;
            info.Humans["science"] = Role.SCIENTIST;
            info.Humans["6"] = Role.SCIENTIST;
            info.Humans[sci] = Role.SCIENTIST;

            var plaguedaddy = Role.SCP_049.ToString().ToLower();
            info.SCPs["plaguedaddy"] = Role.SCP_049;
            info.SCPs["doctor"] = Role.SCP_049;
            info.SCPs["049"] = Role.SCP_049;
            info.SCPs["5"] = Role.SCP_049;
            info.SCPs[plaguedaddy] = Role.SCP_049;

            var zombie = Role.SCP_049_2.ToString().ToLower();
            info.SCPs["zombie"] = Role.SCP_049_2;
            info.SCPs["0492"] = Role.SCP_049_2;
            info.SCPs["helicopter"] = Role.SCP_049_2;
            info.SCPs["plane"] = Role.SCP_049_2;
            info.SCPs["10"] = Role.SCP_049_2;
            info.SCPs[zombie] = Role.SCP_049_2;

            var larry = Role.SCP_106.ToString().ToLower();
            info.SCPs["larry"] = Role.SCP_106;
            info.SCPs["106"] = Role.SCP_106;
            info.SCPs["3"] = Role.SCP_106;
            info.SCPs[larry] = Role.SCP_106;

            var shyguy = Role.SCP_096.ToString().ToLower();
            info.SCPs["shyguy"] = Role.SCP_096;
            info.SCPs["096"] = Role.SCP_096;
            info.SCPs["9"] = Role.SCP_096;
            info.SCPs[shyguy] = Role.SCP_096;

            var comp = Role.SCP_079.ToString().ToLower();
            info.SCPs["079"] = Role.SCP_079;
            info.SCPs["comp"] = Role.SCP_079;
            info.SCPs["computer"] = Role.SCP_079;
            info.SCPs["7"] = Role.SCP_079;
            info.SCPs[comp] = Role.SCP_079;

            var peanut = Role.SCP_173.ToString().ToLower();
            info.SCPs["peanut"] = Role.SCP_173;
            info.SCPs["173"] = Role.SCP_173;
            info.SCPs["0"] = Role.SCP_173;
            info.SCPs[peanut] = Role.SCP_173;

            var doggo1 = Role.SCP_939_53.ToString().ToLower();
            var doggo2 = Role.SCP_939_89.ToString().ToLower();
            info.SCPs["93953"] = Role.SCP_939_53;
            info.SCPs["93989"] = Role.SCP_939_89;
            info.SCPs["16"] = Role.SCP_939_53;
            info.SCPs["17"] = Role.SCP_939_89;
            info.SCPs[doggo1] = Role.SCP_939_53;
            info.SCPs[doggo2] = Role.SCP_939_89;

            var tut = Role.TUTORIAL.ToString().ToLower();
            info.Humans["tut"] = Role.TUTORIAL;
            info.Humans["tutor"] = Role.TUTORIAL;
            info.Humans["tutorial"] = Role.TUTORIAL;
            info.Humans["14"] = Role.TUTORIAL;
            info.Humans[tut] = Role.TUTORIAL;

            var spec = Role.SPECTATOR.ToString().ToLower();
            info.Other["spec"] = Role.SPECTATOR;
            info.Other["specboi"] = Role.SPECTATOR;
            info.Other["ghost"] = Role.SPECTATOR;
            info.Other["2"] = Role.SPECTATOR;
            info.Other[spec] = Role.SPECTATOR;
            #endregion
            #region dare rei agnomes
            // prae perficio
            info.Keycards = new Dictionary<string, ItemType>();
            info.Weapons = new Dictionary<string, ItemType>();
            info.Ammo = new Dictionary<string, ItemType>();
            info.Accessories = new Dictionary<string, ItemType>();
            // perficio
            var kc_ci = ItemType.CHAOS_INSURGENCY_DEVICE.ToString().ToLower();
            info.Keycards["cidevice"] = ItemType.CHAOS_INSURGENCY_DEVICE;
            info.Keycards["kcci"] = ItemType.CHAOS_INSURGENCY_DEVICE;
            info.Keycards["ci"] = ItemType.CHAOS_INSURGENCY_DEVICE;
            info.Keycards[kc_ci] = ItemType.CHAOS_INSURGENCY_DEVICE;

            var coin = ItemType.COIN.ToString().ToLower();
            info.Accessories["coin"] = ItemType.COIN;
            info.Accessories["quarter"] = ItemType.COIN;
            info.Accessories["25c"] = ItemType.COIN;
            info.Accessories["50c"] = ItemType.COIN;
            info.Accessories["17"] = ItemType.COIN;
            info.Accessories[coin] = ItemType.COIN;

            var pew = ItemType.COM15.ToString().ToLower();
            info.Weapons["pistol"] = ItemType.COM15;
            info.Weapons["com15"] = ItemType.COM15;
            info.Weapons["handgun"] = ItemType.COM15;
            info.Weapons["13"] = ItemType.COM15;
            info.Weapons[pew] = ItemType.COM15;

            var kc_ce = ItemType.CONTAINMENT_ENGINEER_KEYCARD.ToString().ToLower();
            info.Keycards["ce"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            info.Keycards["containmentengineer"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            info.Keycards["pinkcard"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            info.Keycards["pink"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            info.Keycards["kcce"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            info.Keycards["6"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            info.Keycards[kc_ce] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;

            var cup = ItemType.CUP.ToString().ToLower();
            info.Accessories["cup"] = ItemType.CUP;
            info.Accessories["18"] = ItemType.CUP;
            info.Accessories[cup] = ItemType.CUP;

            var disarm = ItemType.DISARMER.ToString().ToLower();
            info.Accessories["disarm"] = ItemType.DISARMER;
            info.Accessories["disarmer"] = ItemType.DISARMER;
            info.Accessories["detain"] = ItemType.DISARMER;
            info.Accessories["detainer"] = ItemType.DISARMER;
            info.Accessories["handcuffs"] = ItemType.DISARMER;
            info.Accessories[disarm] = ItemType.DISARMER;

            var fusion = ItemType.DROPPED_5.ToString().ToLower();
            info.Ammo["fusion"] = ItemType.DROPPED_5;
            info.Ammo["mtfammo"] = ItemType.DROPPED_5;
            info.Ammo["ntfammo"] = ItemType.DROPPED_5;
            info.Ammo["e11ammo"] = ItemType.DROPPED_5;
            info.Ammo["ammo5"] = ItemType.DROPPED_5;
            info.Ammo["556mm"] = ItemType.DROPPED_5;
            info.Ammo["22"] = ItemType.DROPPED_5;
            info.Ammo[fusion] = ItemType.DROPPED_5;

            var pat = ItemType.DROPPED_7.ToString().ToLower();
            info.Ammo["9mm"] = ItemType.DROPPED_7;
            info.Ammo["pat"] = ItemType.DROPPED_7;
            info.Ammo["ammo7"] = ItemType.DROPPED_7;
            info.Ammo["29"] = ItemType.DROPPED_7;
            info.Ammo[pat] = ItemType.DROPPED_7;

            var rat = ItemType.DROPPED_9.ToString().ToLower();
            info.Ammo["762mm"] = ItemType.DROPPED_9;
            info.Ammo["rat"] = ItemType.DROPPED_9;
            info.Ammo["ammo9"] = ItemType.DROPPED_9;
            info.Ammo["28"] = ItemType.DROPPED_9;
            info.Ammo[rat] = ItemType.DROPPED_9;

            var e11 = ItemType.E11_STANDARD_RIFLE.ToString().ToLower();
            info.Weapons["mtfgun"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["ntfgun"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["epsilon"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["epsilonstandardrifle"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["epsilon11"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["epsilon11standardrifle"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["e11"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["esr"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons["20"] = ItemType.E11_STANDARD_RIFLE;
            info.Weapons[e11] = ItemType.E11_STANDARD_RIFLE;

            var kc_fm = ItemType.FACILITY_MANAGER_KEYCARD.ToString().ToLower();
            info.Keycards["red"] = ItemType.FACILITY_MANAGER_KEYCARD;
            info.Keycards["9"] = ItemType.FACILITY_MANAGER_KEYCARD;
            info.Keycards["kcfm"] = ItemType.FACILITY_MANAGER_KEYCARD;
            info.Keycards["redcard"] = ItemType.FACILITY_MANAGER_KEYCARD;
            info.Keycards[kc_fm] = ItemType.FACILITY_MANAGER_KEYCARD;

            var fb = ItemType.FLASHBANG.ToString().ToLower();
            info.Weapons["fb"] = ItemType.FLASHBANG;
            info.Weapons["stun"] = ItemType.FLASHBANG;
            info.Weapons["flashbang"] = ItemType.FLASHBANG;
            info.Weapons["stungrenade"] = ItemType.FLASHBANG;
            info.Weapons["26"] = ItemType.FLASHBANG;
            info.Weapons[fb] = ItemType.FLASHBANG;

            var fl = ItemType.FLASHLIGHT.ToString().ToLower();
            info.Accessories["flashlight"] = ItemType.FLASHLIGHT;
            info.Accessories["fl"] = ItemType.FLASHLIGHT;
            info.Accessories["torch"] = ItemType.FLASHLIGHT;
            info.Accessories["lamp"] = ItemType.FLASHLIGHT;
            info.Accessories["15"] = ItemType.FLASHLIGHT;
            info.Accessories[fl] = ItemType.FLASHLIGHT;

            var frag = ItemType.FRAG_GRENADE.ToString().ToLower();
            info.Weapons["frag"] = ItemType.FRAG_GRENADE;
            info.Weapons["grenade"] = ItemType.FRAG_GRENADE;
            info.Weapons["boom"] = ItemType.FRAG_GRENADE;
            info.Weapons["25"] = ItemType.FRAG_GRENADE;
            info.Weapons[frag] = ItemType.FRAG_GRENADE;

            var kc_gu = ItemType.GUARD_KEYCARD.ToString().ToLower();
            info.Keycards["kcguard"] = ItemType.GUARD_KEYCARD;
            info.Keycards["guardkey"] = ItemType.GUARD_KEYCARD;
            info.Keycards["4"] = ItemType.GUARD_KEYCARD;
            info.Keycards[kc_gu] = ItemType.GUARD_KEYCARD;

            var kc_jan = ItemType.JANITOR_KEYCARD.ToString().ToLower();
            info.Keycards["kcjan"] = ItemType.JANITOR_KEYCARD;
            info.Keycards["janitor"] = ItemType.JANITOR_KEYCARD;
            info.Keycards["0"] = ItemType.JANITOR_KEYCARD;
            info.Keycards[kc_jan] = ItemType.JANITOR_KEYCARD;

            var cigun = ItemType.LOGICER.ToString().ToLower();
            info.Weapons["chaosgun"] = ItemType.LOGICER;
            info.Weapons["logicer"] = ItemType.LOGICER;
            info.Weapons["cigun"] = ItemType.LOGICER;
            info.Weapons["lmg"] = ItemType.LOGICER;
            info.Weapons["24"] = ItemType.LOGICER;
            info.Weapons[cigun] = ItemType.LOGICER;

            var kc_msci = ItemType.MAJOR_SCIENTIST_KEYCARD.ToString().ToLower();
            info.Keycards["majscikey"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            info.Keycards["majorsci"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            info.Keycards["majorscientistkeycard"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            info.Keycards["2"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            info.Keycards[kc_msci] = ItemType.MAJOR_SCIENTIST_KEYCARD;

            var med = ItemType.MEDKIT.ToString().ToLower();
            info.Accessories["medkit"] = ItemType.MEDKIT;
            info.Accessories["med"] = ItemType.MEDKIT;
            info.Accessories["14"] = ItemType.MEDKIT;
            info.Accessories[med] = ItemType.MEDKIT;

            var micro = ItemType.MICROHID.ToString().ToLower();
            info.Weapons["micro"] = ItemType.MICROHID;
            info.Weapons["microwave"] = ItemType.MICROHID;
            info.Weapons["supersoaker"] = ItemType.MICROHID;
            info.Weapons["microhid"] = ItemType.MICROHID;
            info.Weapons["16"] = ItemType.MICROHID;
            info.Weapons[micro] = ItemType.MICROHID;

            var mp7 = ItemType.MP4.ToString().ToLower();
            info.Weapons["mp7"] = ItemType.MP4;
            info.Weapons["mp4"] = ItemType.MP4;
            info.Weapons["smg"] = ItemType.MP4;
            info.Weapons["scorpion"] = ItemType.MP4;
            info.Weapons["23"] = ItemType.MP4;
            info.Weapons[mp7] = ItemType.MP4;

            var kc_ntfc = ItemType.MTF_COMMANDER_KEYCARD.ToString().ToLower();
            info.Keycards["kcmtfc"] = ItemType.MTF_COMMANDER_KEYCARD;
            info.Keycards["kcntfc"] = ItemType.MTF_COMMANDER_KEYCARD;
            info.Keycards["commanderkey"] = ItemType.MTF_COMMANDER_KEYCARD;
            info.Keycards["8"] = ItemType.MTF_COMMANDER_KEYCARD;
            info.Keycards[kc_ntfc] = ItemType.MTF_COMMANDER_KEYCARD;

            var kc_ntfl = ItemType.MTF_LIEUTENANT_KEYCARD.ToString().ToLower();
            info.Keycards["kcmtfl"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            info.Keycards["kcntfl"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            info.Keycards["lieutenantkey"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            info.Keycards["7"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            info.Keycards[kc_ntfl] = ItemType.MTF_LIEUTENANT_KEYCARD;

            var kc_o5 = ItemType.O5_LEVEL_KEYCARD.ToString().ToLower();
            info.Keycards["o5"] = ItemType.O5_LEVEL_KEYCARD;
            info.Keycards["kco5"] = ItemType.O5_LEVEL_KEYCARD;
            info.Keycards["black"] = ItemType.O5_LEVEL_KEYCARD;
            info.Keycards["blackcard"] = ItemType.O5_LEVEL_KEYCARD;
            info.Keycards["11"] = ItemType.O5_LEVEL_KEYCARD;
            info.Keycards[kc_o5] = ItemType.O5_LEVEL_KEYCARD;

            var p90 = ItemType.P90.ToString().ToLower();
            info.Weapons["p90"] = ItemType.P90;
            info.Weapons["russia"] = ItemType.P90;
            info.Weapons["russian"] = ItemType.P90;
            info.Weapons["21"] = ItemType.P90;
            info.Weapons[p90] = ItemType.P90;

            var kc_sci = ItemType.SCIENTIST_KEYCARD.ToString().ToLower().ToLower();
            info.Keycards["sci"] = ItemType.SCIENTIST_KEYCARD;
            info.Keycards["kcsci"] = ItemType.SCIENTIST_KEYCARD;
            info.Keycards["1"] = ItemType.SCIENTIST_KEYCARD;
            info.Keycards[kc_sci] = ItemType.SCIENTIST_KEYCARD;

            var kc_mg = ItemType.SENIOR_GUARD_KEYCARD.ToString().ToLower();
            info.Keycards["kcmg"] = ItemType.SENIOR_GUARD_KEYCARD;
            info.Keycards["senior"] = ItemType.SENIOR_GUARD_KEYCARD;
            info.Keycards["seniorguard"] = ItemType.SENIOR_GUARD_KEYCARD;
            info.Keycards["sgk"] = ItemType.SENIOR_GUARD_KEYCARD;
            info.Keycards["5"] = ItemType.SENIOR_GUARD_KEYCARD;
            info.Keycards[kc_mg] = ItemType.SENIOR_GUARD_KEYCARD;

            var wmt = ItemType.WEAPON_MANAGER_TABLET.ToString().ToLower();
            info.Keycards["wmt"] = ItemType.WEAPON_MANAGER_TABLET;
            info.Keycards["tablet"] = ItemType.WEAPON_MANAGER_TABLET;
            info.Keycards["19"] = ItemType.WEAPON_MANAGER_TABLET;
            info.Keycards[wmt] = ItemType.WEAPON_MANAGER_TABLET;

            var kc_zm = ItemType.ZONE_MANAGER_KEYCARD.ToString().ToLower();
            info.Keycards["blue"] = ItemType.ZONE_MANAGER_KEYCARD;
            info.Keycards["zone"] = ItemType.ZONE_MANAGER_KEYCARD;
            info.Keycards["kczm"] = ItemType.ZONE_MANAGER_KEYCARD;
            info.Keycards["3"] = ItemType.ZONE_MANAGER_KEYCARD;
            info.Keycards[kc_zm] = ItemType.ZONE_MANAGER_KEYCARD;

            var rad = ItemType.RADIO.ToString().ToLower();
            info.Accessories["rad"] = ItemType.RADIO;
            info.Accessories["radio"] = ItemType.RADIO;
            info.Accessories["walkietalkie"] = ItemType.RADIO;
            info.Accessories["12"] = ItemType.RADIO;
            info.Accessories[rad] = ItemType.RADIO;
            #endregion
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
                    plugin.Info("Plugin dev " + ev.Player.Name + " joined the server!");
                }
            }
            else
            {
                plugin.Debug("A player has joined the server!");
            }
        }

        public void OnSetRole(PlayerSetRoleEvent ev)
        {
            // this stuff is fine
            #region dare player nominibus res
            string player = ev.Player.Name;
            string rank = ev.Player.GetRankName();
            var team = ev.Player.TeamRole.Role;
            #endregion
            #region primus - config
            string path = @"rc-config.dat";
            plugin.Debug("Player " + player + " rank: " + rank);
            plugin.Debug("Player " + player + " team: " + team);
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
            #endregion

            //this stuff is broken somewhere
            plugin.Debug("1");
            List<string> rankNames = new List<string>();
            plugin.Debug("2");
            List<string> classitems = new List<string>();
            //List<string> clitems = new List<string>();
            plugin.Debug("3");
            IEnumerable<string> items = new List<string>();
            plugin.Debug("4");

            var table = new Dictionary<string, List<string>>();
            plugin.Debug("5");

            int PlayerItemCount(Player pl)
            {
                int itemInt = 0;
                foreach (Item item in pl.GetInventory())
                    if (item.ItemType != ItemType.NULL)
                        itemInt++;
                return itemInt;
            }

            plugin.Debug("6");

            BinaryFormatter formatter = new BinaryFormatter();
            plugin.Debug("7");
            if (File.Exists(path))
            {
                plugin.Debug("8");

                using (FileStream s = File.OpenRead("dictionary.bin"))
                {
                    table = (Dictionary<string, List<string>>)formatter.Deserialize(s);
                }
                //check if it wokred
                foreach (string key in table.Keys)
                {
                    plugin.Debug("Key = " + key + " Value = " + table[key]);
                    List<string> m = table[key];
                    foreach (string b in m)
                        plugin.Debug(b);

                }

                foreach (KeyValuePair<string, List<string>> x in table)
                {
                    plugin.Debug(x.Key);
                    plugin.Debug("11");
                    rankNames.Add(x.Key);
                    foreach (string y in x.Value)
                        classitems.Add(y);

                    string cl = classitems[0].ToString();
                    plugin.Debug(cl);
                    items = classitems.Skip(1).ToList<string>();

                    plugin.Debug("12");
                    Info info = new Info();

                    plugin.Debug("13");
                    Role myHuman = Role.UNASSIGNED;
                    Role mySCP = Role.UNASSIGNED;
                    Role myRole = Role.UNASSIGNED;
                    ItemType myItem = ItemType.NULL;
                    //ItemType myKeycard = ItemType.NULL;
                    //ItemType myWeapon = ItemType.NULL;
                    //ItemType myAccessory = ItemType.NULL;

                    plugin.Debug(cl);
                    plugin.Debug("14");
                    if (info.Humans.ContainsKey(cl))
                        myHuman = info.Humans[cl];
                    else if (info.SCPs.ContainsKey(cl))
                        mySCP = info.SCPs[cl];
                    else if (info.Other.ContainsKey(cl))
                        myRole = info.Other[cl];
                    else
                        plugin.Warn("Class name not found!");

                    plugin.Debug("15");
                    foreach (string myRank in rankNames)
                    {
                        foreach (string item in items)
                        {
                            if (info.Keycards.ContainsKey(item))
                                myItem = info.Keycards[item];
                            else if (info.Weapons.ContainsKey(item))
                                myItem = info.Weapons[item];
                            else if (info.Accessories.ContainsKey(item))
                                myItem = info.Accessories[item];

                            plugin.Debug("16");
                            if (PlayerItemCount(ev.Player) != 8 && ev.Player.GetUserGroup().Name == myRank && ev.Player.TeamRole.Role == myHuman)
                                ev.Player.GiveItem(myItem);
                            if (PlayerItemCount(ev.Player) == 8 && ev.Player.GetUserGroup().Name == myRank && ev.Player.TeamRole.Role == myHuman)
                            {
                                Vector myPos = ev.Player.GetPosition();
                                Vector myRot = ev.Player.GetRotation();
                                PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
                            }
                            if (ev.Player.GetUserGroup().Name == myRank && ev.Player.TeamRole.Role == mySCP)
                            {
                                Vector myPos = ev.Player.GetPosition();
                                Vector myRot = ev.Player.GetRotation();
                                PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
                            }
                            if (ev.Player.GetUserGroup().Name == myRank && ev.Player.TeamRole.Role == myRole)
                                plugin.Warn("Trying to give items to spectators is weird");
                        }
                    }
                }
            }

        }
    }
}

