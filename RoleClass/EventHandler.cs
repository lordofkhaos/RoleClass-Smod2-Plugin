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

		private Dictionary<string, string> myGlobalGive = new Dictionary<string, string>();
		private string myRoleClass = string.Empty;
		private List<string> fullRoleClass = new List<string>();
		private Dictionary<string, int> globalGiveSettings = new Dictionary<string, int>();

		public EventHandler(Plugin plugin) => this.plugin = plugin;

		public void OnWaitingForPlayers(WaitingForPlayersEvent ev)
		{
			float time = ev.Server.Round.Duration;

			myGlobalGive = plugin.GetConfigDict("k_global_give");
			globalGiveSettings = new Dictionary<string, int>();
			foreach (KeyValuePair<string, string> globalGivePair in myGlobalGive)
			{
				if (int.TryParse(globalGivePair.Value, out int intifiedValue))
					globalGiveSettings.Add(globalGivePair.Key, intifiedValue);
				else
					plugin.Error(intifiedValue + " is not a number!");
			}

			//myRoleClass = plugin.GetConfigString("k_roleclass");
			//string[] rcArr = myRoleClass.Split(':'); // foreach entry
			//for (int i = 0; i < rcArr.Length; i++)
			//{
			//	string[] entry = rcArr[i].Split(','); // split into role,class,[items]
			//	IEnumerable<string> items = new List<string>();
			//	string role = entry.First();
			//	fullRoleClass.Add(role); // role is first
			//	entry = entry.Where(r => r != entry[0]).ToArray();
			//	string klasy = entry.First();
			//	fullRoleClass.Add(klasy); // class is next
			//	entry = entry.Where(r => r != entry[0]).ToArray();
			//	string entryItems = entry.First(); // items yay
			//	entryItems = string.Concat(entryItems.Split('['));
			//	entryItems = entryItems.Trim('[').Trim(']');
			//	string[] itemArr = entryItems.Split('.');
			//	items = itemArr.Where(r => r != entry[0]);

			//	items.ToList().ForEach(x => fullRoleClass.Add(x));
			//}
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
					plugin.Debug("Super secret message 0.o");
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
				string path = @"roleclass.cfgbin";
				foreach (KeyValuePair<string, int> globalGivePair in globalGiveSettings)
				{
					if (rank == null || team == Role.SPECTATOR || globalGivePair.Key != rank)
						continue;
					var itemType = (ItemType)globalGivePair.Value;
					ev.Items.Add(itemType);
					//ev.Player.GiveItem(itemType);
					plugin.Debug($"Player {player} given item {itemType}");
				}

				List<string> rankNames = new List<string>();
				List<string> classItems = new List<string>();
				List<Smod2.API.Item> userItems = new List<Smod2.API.Item>();
				//List<string> clitems = new List<string>();
				IEnumerable<string> items = new List<string>();

				var deseralizedData = new Dictionary<string, List<string>>();
				var configData = new Dictionary<string, List<string>>();

				var yeet = GLaDOS.GetRoleClassConfig();
				#region Read config entry
				configData.Add(key: yeet[0], value: yeet.Skip(1).ToList<string>());
				foreach (KeyValuePair<string, List<string>> savedPair in configData)
				{
					rankNames.Add(savedPair.Key);
					for (int i = 0, savedPairValueCount = savedPair.Value.Count; i < savedPairValueCount; i++)
					{
						string y = savedPair.Value[i];
						classItems.Add(y);
					}

					string assignedClass = classItems[0].ToString();
					assignedClass.Trim('-', '_');
					assignedClass.Replace("scp", "");
					items = classItems.Skip(1).ToList<string>();

					Aliases aliases = new Aliases();
					aliases.AssignAliases();

					Role myHuman = Role.UNASSIGNED;
					Role mySCP = Role.UNASSIGNED;
					Role myRole = Role.UNASSIGNED;
					ItemType myItem = ItemType.NULL;
					//ItemType myKeycard = ItemType.NULL;
					//ItemType myWeapon = ItemType.NULL;
					//ItemType myAccessory = ItemType.NULL;

					int typeOfClass = GLaDOS.TypeOfPlayerClass(assignedClass);
					switch (typeOfClass)
					{
						case 0:
							myHuman = aliases.Humans[assignedClass];
							break;
						case 1:
							mySCP = aliases.SCPs[assignedClass];
							break;
						case 2:
							myRole = aliases.Other[assignedClass];
							break;
						default:
							plugin.Warn("Invalid class name!");
							break;
					}

					foreach (string myRank in rankNames)
					{
						foreach (string item in items)
						{
							int typeOfItem = GLaDOS.TypeOfItem(item);
							bool isEightItems = GLaDOS.PlayerItemCount(ev.Player) == 8;

							bool isMyRank = ev.Player.GetUserGroup().Name == myRank || ev.Player.GetRankName() == myRank;
							if (isEightItems == false && isMyRank == true && ev.Player.TeamRole.Role == myHuman)
							{
								switch (typeOfItem)
								{
									case 0:
										myItem = aliases.Keycards[item];
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
							if (isEightItems == true && isMyRank == true && ev.Player.TeamRole.Role == myHuman)
							{
								switch (typeOfItem)
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
							if (isMyRank == true && ev.Player.TeamRole.Role == mySCP)
							{
								switch (typeOfItem)
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
							if (isMyRank == true && ev.Player.TeamRole.Role == myRole)
								plugin.Warn("Trying to give items to spectators is weird");
						}
					}
					// TODO: clear lists
				}
				#endregion

				#region Binary file config stuff
				BinaryFormatter formatter = new BinaryFormatter();
				if (File.Exists(path))
				{

					using (FileStream s = File.OpenRead("roleclass.cfgbin"))
					{
						deseralizedData = (Dictionary<string, List<string>>)formatter.Deserialize(s);
					}

					foreach (KeyValuePair<string, List<string>> savedPair in deseralizedData)
					{
						rankNames.Add(savedPair.Key);
						for (int i = 0, savedPairValueCount = savedPair.Value.Count; i < savedPairValueCount; i++)
						{
							string y = savedPair.Value[i];
							classItems.Add(y);
						}

						string assignedClass = classItems[0].ToString();
						assignedClass.Trim('-', '_');
						assignedClass.Replace("scp", "");
						items = classItems.Skip(1).ToList<string>();

						Aliases aliases = new Aliases();
						aliases.AssignAliases();

						Role myHuman = Role.UNASSIGNED;
						Role mySCP = Role.UNASSIGNED;
						Role myRole = Role.UNASSIGNED;
						ItemType myItem = ItemType.NULL;
						//ItemType myKeycard = ItemType.NULL;
						//ItemType myWeapon = ItemType.NULL;
						//ItemType myAccessory = ItemType.NULL;

						if (aliases.Humans.ContainsKey(assignedClass))
						{
							myHuman = aliases.Humans[assignedClass];
						}
						else if (aliases.SCPs.ContainsKey(assignedClass))
						{
							mySCP = aliases.SCPs[assignedClass];
						}
						else if (aliases.Other.ContainsKey(assignedClass))
						{
							myRole = aliases.Other[assignedClass];
						}
						else
							plugin.Warn("Invalid class name!");

						foreach (string myRank in rankNames)
						{
							foreach (string item in items)
							{
								int typeOfItem = GLaDOS.TypeOfItem(item);
								bool isEightItems = true;
								switch (GLaDOS.PlayerItemCount(ev.Player))
								{
									case 8:
										isEightItems = true;
										break;
									default:
										isEightItems = false;
										break;
								}
								bool isMyRank = false;
								isMyRank = ev.Player.GetUserGroup().Name == myRank || ev.Player.GetRankName() == myRank;
								if (isEightItems == false && isMyRank == true && ev.Player.TeamRole.Role == myHuman)
								{
									switch (typeOfItem)
									{
										case 0:
											myItem = aliases.Keycards[item];
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
								if (isEightItems == true && isMyRank == true && ev.Player.TeamRole.Role == myHuman)
								{
									switch (typeOfItem)
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
								if (isMyRank == true && ev.Player.TeamRole.Role == mySCP)
								{
									switch (typeOfItem)
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
								if (isMyRank == true && ev.Player.TeamRole.Role == myRole)
									plugin.Warn("Trying to give items to spectators is weird");
							}
						}
					}
				}
				#endregion
			}
			catch (Exception e) { plugin.Error($"[MESSAGE]: {e.Message}{Environment.NewLine}[STACKTRACE]: {e.StackTrace} "); }
		}
	}
}

