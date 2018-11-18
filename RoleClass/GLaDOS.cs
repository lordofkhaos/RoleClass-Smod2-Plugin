using System;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using Smod2.API;

namespace RoleClass
{
	public static class GLaDOS
	{
		private static readonly Plugin plugin;
		private static Aliases aliases = new Aliases();


		// read my pretty config entry
		public static Dictionary<string, Dictionary<string, List<ItemType>>> GetRoleClassConfig()
		{
			aliases.AssignAliases();
			var yeet = ConfigFile.GetDict("k_roleclass"); // format= string: list,of,strings
			var endyeet = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			foreach (KeyValuePair<string, string> roleClassPair in yeet)
			{
				string rank = roleClassPair.Key;
				string klasy = roleClassPair.Value.Split('[')[0];
				string[] itemsArray = roleClassPair.Value.Split('[');
				List<string> itemsButTheyreStrings = new List<string>(itemsArray).Skip(1).ToList();
				List<ItemType> items = new List<ItemType>();
				Dictionary<string, List<ItemType>> classItems = new Dictionary<string, List<ItemType>>();

				try { itemsButTheyreStrings.Remove("]"); }
				catch (Exception e) { plugin.Error($"[MESSAGE]: {e.Message}{Environment.NewLine}[STACKTRACE]: {e.StackTrace} "); }
				foreach (string item in itemsButTheyreStrings)
				{
					int typeOfItem = TypeOfItem(item);
					ItemType myItem = ItemType.NULL;
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
							plugin.Warn($"Item {item} not found!");
							break;
						default:
							plugin.Warn($"Item {item} unavailable!");
							break;
					}
					items.Add(myItem);
				}

				classItems.Add(klasy, items);
				endyeet.Add(rank, classItems);
			}

			return endyeet;
		}

		public static int TypeOfItem(string item)
		{
			aliases.AssignAliases();
			return aliases.Keycards.ContainsKey(item)
						  ? 0
							  : aliases.Weapons.ContainsKey(item)
						  ? 1
							  : aliases.Accessories.ContainsKey(item)
						  ? 2
							  : aliases.Ammo.ContainsKey(item)
						  ? 3
							  : -1;
		}

		public static int PlayerItemCount(Player pl)
		{
			int itemInt = 0;
			var inv = pl.GetInventory().Where(item => item.ItemType != ItemType.NULL);
			itemInt = inv.Count();
			return itemInt;
		}

		public static int TypeOfPlayerClass(string player)
		{
			aliases.AssignAliases();
			return aliases.Humans.ContainsKey(player)
						  ? 0
							  : aliases.SCPs.ContainsKey(player)
						  ? 1
							  : aliases.Other.ContainsKey(player)
						  ? 2
							  : -1;
		}
	}
}
