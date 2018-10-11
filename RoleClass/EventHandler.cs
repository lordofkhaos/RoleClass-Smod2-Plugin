using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using RoleClass;
using Smod2;
using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using Smod2.EventSystem.Events;
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

        Dictionary<string, string> myGlobalGive { get; set;}
        Dictionary<string, int> dict { get; set; }

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
        {
            float time = ev.Server.Round.Duration;
            myGlobalGive = plugin.GetConfigDict("k_global_give");
            dict = new Dictionary<string, int>();
            foreach (KeyValuePair<string, string> x in myGlobalGive)
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
            #region dare player nominibus res
            string player = ev.Player.Name;
            string rank = ev.Player.GetRankName();
            var team = ev.Player.TeamRole.Role;
            #endregion
            string path = @"roleclass.cfgbin";
            foreach (KeyValuePair<string, int> m in dict)
            {
                if (rank != null && team != Role.SPECTATOR)
                {
                    if (m.Key == rank)
                    {
                        var itemType = (ItemType)m.Value;
                        ev.Items.Add(itemType);
                        //ev.Player.GiveItem(itemType);
                        plugin.Debug("Player " + player + " given item " + itemType);
                    }
                }
            }

            List<string> rankNames = new List<string>();
            List<string> classitems = new List<string>();
            List<Item> useritems = new List<Item>();
            //List<string> clitems = new List<string>();
            IEnumerable<string> items = new List<string>();

            var table = new Dictionary<string, List<string>>();

            int PlayerItemCount(Player pl)
            {
                int itemInt = 0;
                foreach (Item item in pl.GetInventory())
                    if (item.ItemType != ItemType.NULL)
                        itemInt++;
                return itemInt;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            if (File.Exists(path))
            {

                using (FileStream s = File.OpenRead("roleclass.cfgbin"))
                {
                    table = (Dictionary<string, List<string>>)formatter.Deserialize(s);
                }

                foreach (KeyValuePair<string, List<string>> x in table)
                {
                    rankNames.Add(x.Key);
                    foreach (string y in x.Value)
                        classitems.Add(y);

                    string cl = classitems[0].ToString();
                    cl.Trim('-', '_');
                    cl.Replace("scp", "");
                    items = classitems.Skip(1).ToList<string>();

                    Aliases aliases = new Aliases();
                    aliases.AssignAliases();

                    Role myHuman = Role.UNASSIGNED;
                    Role mySCP = Role.UNASSIGNED;
                    Role myRole = Role.UNASSIGNED;
                    ItemType myItem = ItemType.NULL;
                    //ItemType myKeycard = ItemType.NULL;
                    //ItemType myWeapon = ItemType.NULL;
                    //ItemType myAccessory = ItemType.NULL;

                    if (aliases.Humans.ContainsKey(cl))
                    {
                        myHuman = aliases.Humans[cl];
                    }
                    else if (aliases.SCPs.ContainsKey(cl))
                    {
                        mySCP = aliases.SCPs[cl];
                    }
                    else if (aliases.Other.ContainsKey(cl))
                    {
                        myRole = aliases.Other[cl];
                    }
                    else
                        plugin.Warn("Class name not found!");

                    foreach (string myRank in rankNames)
                    {
                        foreach (string item in items)
                        {
                            int h = 0;
                            if (aliases.Keycards.ContainsKey(item)) { h = 0; }
                            else if (aliases.Weapons.ContainsKey(item)) { h = 1; }
                            else if (aliases.Accessories.ContainsKey(item)) { h = 2; }
                            else if (aliases.Ammo.ContainsKey(item)) { h = 3; }
                            else { h = -1; }
                            bool A = true;
                            if (PlayerItemCount(ev.Player) == 8) { A = true; }
                            else { A = false; }
                            bool B = false;
                            if (ev.Player.GetUserGroup().Name == myRank) { B = true; }
                            else if (ev.Player.GetRankName() == myRank) { B = true; }
                            else { B = false; }
                            if (A == false && B == true && ev.Player.TeamRole.Role == myHuman)
                            {
                                switch (h)
                                {
                                    case 0:
                                        myItem = aliases.Keycards[item];
                                        plugin.Debug(myItem.ToString());
                                        foreach (Item t in ev.Player.GetInventory())
                                        {
                                            useritems.Add(t);
                                        }
                                        try { ev.Items.Add(myItem); }
                                        catch (Exception e)
                                        {
                                            plugin.Error("Encountered exception: " + e);
                                        }

                                        //ev.Player.GiveItem(myItem);
                                        break;
                                    case 1:
                                        myItem = aliases.Weapons[item];
                                        plugin.Debug(myItem.ToString());
                                        try { ev.Items.Add(myItem); }
                                        catch (Exception e)
                                        {
                                            plugin.Error("Encountered exception: " + e);
                                        }
                                        //ev.Player.GiveItem(myItem);
                                        break;
                                    case 2:
                                        myItem = aliases.Accessories[item];
                                        plugin.Debug(myItem.ToString());
                                        try { ev.Items.Add(myItem); }
                                        catch (Exception e)
                                        {
                                            plugin.Error("Encountered exception: " + e);
                                        }
                                        //ev.Player.GiveItem(myItem);
                                        break;
                                    case 3:
                                        myItem = aliases.Ammo[item];
                                        plugin.Debug(myItem.ToString());
                                        Vector myPos = ev.Player.GetPosition();
                                        Vector myRot = ev.Player.GetRotation();
                                        PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
                                        break;
                                    case -1:
                                        plugin.Warn("Item not found!");
                                        break;
                                    default:
                                        plugin.Warn("Item unavailable!");
                                        break;
                                }
                            }
                            if (A == true && B == true && ev.Player.TeamRole.Role == myHuman)
                            {
                                switch (h)
                                {
                                    case 0:
                                        myItem = aliases.Keycards[item];
                                        break;
                                    case 1:
                                        myItem = aliases.Weapons[item];
                                        break;
                                    case 2:
                                        myItem = aliases.Accessories[item];
                                        break;
                                    case 3:
                                        myItem = aliases.Ammo[item];
                                        break;
                                    case -1:
                                        plugin.Warn("Item not found!");
                                        break;
                                    default:
                                        plugin.Warn("Item unavailable!");
                                        break;
                                }
                                Vector myPos = ev.Player.GetPosition();
                                Vector myRot = ev.Player.GetRotation();
                                PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
                            }
                            if (B == true && ev.Player.TeamRole.Role == mySCP)
                            {
                                switch (h)
                                {
                                    case 0:
                                        myItem = aliases.Keycards[item];
                                        break;
                                    case 1:
                                        myItem = aliases.Weapons[item];
                                        break;
                                    case 2:
                                        myItem = aliases.Accessories[item];
                                        break;
                                    case 3:
                                        myItem = aliases.Ammo[item];
                                        break;
                                    case -1:
                                        plugin.Warn("Item not found!");
                                        break;
                                    default:
                                        plugin.Warn("Item unavailable!");
                                        break;
                                }
                                Vector myPos = ev.Player.GetPosition();
                                Vector myRot = ev.Player.GetRotation();
                                PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
                            }
                            if (B == true && ev.Player.TeamRole.Role == myRole)
                                plugin.Warn("Trying to give items to spectators is weird");
                        }
                    }
                }
            }

        }
    }
}

