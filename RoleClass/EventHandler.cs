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


namespace RoleClass
{
	class EventHandler : IEventHandlerPlayerJoin, IEventHandlerSetRole
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
			switch (s64)
			{
				case "76561198071607345":
					if (ev.Player.GetUserGroup().Name.StartsWith("[", StringComparison.Ordinal) || ev.Player.GetUserGroup().Name == string.Empty)
						ev.Player.SetRank("aqua", "PLUGIN DEV");
					else
						plugin.Info("Plugin dev " + ev.Player.Name + " joined the server!");
					break;
				default:
					break;
			}
		}

		public void OnSetRole(PlayerSetRoleEvent ev)
		{
			try
			{
				#region dare player nominibus res
				string player = ev.Player.Name;
				string rank = ev.Player.GetRankName();
				var team = ev.Player.TeamRole.Role;
				#endregion
				List<string> rankNames = new List<string>();
				List<string> classItems = new List<string>();
				List<Smod2.API.Item> userItems = new List<Smod2.API.Item>();
				//List<string> clitems = new List<string>();
				IEnumerable<string> items = new List<string>();

				var deseralizedData = new Dictionary<string, List<string>>();
				var configData = new Dictionary<string, List<string>>();

				var yeet = Ancillary.ReturnSpecialConfig("krc_items");
				#region debug stuff
				plugin.Debug(ev.Player.TeamRole.Role.ToString());
				plugin.Debug("YEET COUNT: " + yeet.Count.ToString());
				foreach (var y in yeet.Keys)
					plugin.Debug("YEET KEY: " + y);
				foreach (var x in yeet.Values)
				{
					foreach (var y in x.Keys)
						plugin.Debug("YEET VALUE KEY: " + y);
					foreach (var y in x.Values)
						foreach (var z in y)
							plugin.Debug("YEET VALUE VALUES: " + z.ToString());
				}
				#endregion
				#region Read config entry
				//configData.Add(key: yeet[0], value: yeet.Skip(1).ToList<string>());
				foreach (var savedPair in yeet)
				{
					rankNames.Add(savedPair.Key);
					string assignedClass = yeet.Keys.First().ToString();
					assignedClass.Trim('-', '_');
					assignedClass.Replace("scp", "");


					Role myHuman = Role.UNASSIGNED;
					Role mySCP = Role.UNASSIGNED;
					Role myRole = Role.UNASSIGNED;
					ItemType myItem = ItemType.NULL;
					//ItemType myKeycard = ItemType.NULL;
					//ItemType myWeapon = ItemType.NULL;
					//ItemType myAccessory = ItemType.NULL;

					Ancillary.KPlayerClass typeOfClass = Ancillary.GetPlayerClass(assignedClass);
					switch (typeOfClass)
					{
						case Ancillary.KPlayerClass.Human:
							myHuman = Aliases.Humans[assignedClass];
							break;
						case Ancillary.KPlayerClass.SCP:
							mySCP = Aliases.SCPs[assignedClass];
							break;
						case Ancillary.KPlayerClass.Other:
							myRole = Aliases.Other[assignedClass];
							break;
						case Ancillary.KPlayerClass.NOT:
						default:
							plugin.Warn("Invalid class name!");
							break;
					}

					foreach (string myRank in rankNames)
					{
						foreach (string item in items)
						{
							Ancillary.KItemType typeOfItem = Ancillary.GetTypeOfItem(item);
							bool isEightItems = Ancillary.PlayerItemCount(ev.Player) == 8;

							bool isMyRank = ev.Player.GetUserGroup().Name == myRank || ev.Player.GetRankName() == myRank;
							if (isEightItems == false && isMyRank == true && ev.Player.TeamRole.Role == myHuman)
							{
								switch (typeOfItem)
								{
									case Ancillary.KItemType.Keycard:
										myItem = Aliases.Keycards[item];
										plugin.Debug(myItem.ToString());
										foreach (Smod2.API.Item playerItem in ev.Player.GetInventory())
										{
											userItems.Add(playerItem);
										}
										try { ev.Items.Add(myItem); }
										catch (Exception e)
										{
											plugin.Error("Encountered unknown error: " + e);
										}

										//ev.Player.GiveItem(myItem);
										break;
									case Ancillary.KItemType.Weapon:
										myItem = Aliases.Weapons[item];
										plugin.Debug(myItem.ToString());
										try { ev.Items.Add(myItem); }
										catch (Exception e)
										{
											plugin.Error("Encountered exception: " + e);
										}
										//ev.Player.GiveItem(myItem);
										break;
									case Ancillary.KItemType.Accessory:
										myItem = Aliases.Accessories[item];
										plugin.Debug(myItem.ToString());
										try { ev.Items.Add(myItem); }
										catch (Exception e)
										{
											plugin.Error("Encountered exception: " + e);
										}
										//ev.Player.GiveItem(myItem);
										break;
									case Ancillary.KItemType.Ammo:
									case Ancillary.KItemType.NOT:
									default:
										plugin.Warn(ErrorMessage);
										break;
								}
							}
							if (isEightItems == true && isMyRank == true && ev.Player.TeamRole.Role == myHuman)
							{
								switch (typeOfItem)
								{
									case Ancillary.KItemType.Keycard:
										myItem = Aliases.Keycards[item];
										break;
									case Ancillary.KItemType.Weapon:
										myItem = Aliases.Weapons[item];
										break;
									case Ancillary.KItemType.Accessory:
										myItem = Aliases.Accessories[item];
										break;
									case Ancillary.KItemType.Ammo:
									case Ancillary.KItemType.NOT:
									default:
										plugin.Warn(ErrorMessage);
										break;
								}
								Vector myPos = ev.Player.GetPosition();
								Vector myRot = ev.Player.GetRotation();
								PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
							}
							if (isMyRank == true && ev.Player.TeamRole.Role == mySCP)
							{
								switch (typeOfItem)
								{
									case Ancillary.KItemType.Keycard:
										myItem = Aliases.Keycards[item];
										break;
									case Ancillary.KItemType.Weapon:
										myItem = Aliases.Weapons[item];
										break;
									case Ancillary.KItemType.Accessory:
										myItem = Aliases.Accessories[item];
										break;
									case Ancillary.KItemType.Ammo:
									case Ancillary.KItemType.NOT:
									default:
										plugin.Warn(ErrorMessage);
										break;
								}
								Vector myPos = ev.Player.GetPosition();
								Vector myRot = ev.Player.GetRotation();
								PluginManager.Manager.Server.Map.SpawnItem(myItem, myPos, myRot);
							}
							if (isMyRank == true && ev.Player.TeamRole.Role == myRole)
								plugin.Warn("Trying to give items to spectators is weird");
						}
					}
					// TODO: clear lists
				}
				#endregion

				#region Binary file config stuff
				#endregion
			}
			catch (Exception e) { plugin.Error($"[MESSAGE]: {e.Message}{Environment.NewLine}[STACKTRACE]: {e.StackTrace} "); }
		}
	}
}

