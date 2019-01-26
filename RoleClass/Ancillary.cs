using System;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using Smod2.API;

namespace RoleClass
{
	public static class Ancillary
	{
		// example:
		// k_roleclass:
		//  - role:class[item.item.item]
		// 
		// 
		/// <summary>
		/// For returning a Dictionary of string keys with values of dictionaries with keys of strings and values of item lists.
		/// </summary>
		public static Dictionary<string, Dictionary<string, List<ItemType>>> ReturnSpecialConfig(string key)
		{
			var endyeet = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			try
			{
				var yeet = ConfigFile.ServerConfig.GetStringDictionary(key);
				foreach (KeyValuePair<string, string> roleClassPair in yeet)
				{
					string rank = roleClassPair.Key;
					rank = rank.Replace(" ", string.Empty);
					string klasa = roleClassPair.Value.Split('[')[0];
					klasa = klasa.Replace(" ", string.Empty);
					string[] itemsArray = roleClassPair.Value.Split('[');
					List<string> itemsButTheyreStrings = new List<string>(itemsArray).Skip(1).ToList();
					List<ItemType> items = new List<ItemType>();
					Dictionary<string, List<ItemType>> classItems = new Dictionary<string, List<ItemType>>();

					itemsButTheyreStrings.Remove("]");
					foreach (string item in itemsButTheyreStrings)
					{
						int typeOfItem = TypeOfItem(item.Replace(" ", string.Empty).Replace("ITEM#", string.Empty).TrimStart('0'));
						ItemType myItem = ItemType.NULL;
						myItem = (ItemType)typeOfItem;
						items.Add(myItem);
					}

					classItems.Add(klasa, items);
					endyeet.Add(rank, classItems);
				}
				return endyeet;
			}
			catch (Exception)
			{
				return endyeet;
			}
		}

		public static int TypeOfItem(string item)
		{
			return Aliases.Keycards.ContainsKey(item)
							? 0
								: Aliases.Weapons.ContainsKey(item)
							? 1
								: Aliases.Accessories.ContainsKey(item)
							? 2
								: Aliases.Ammo.ContainsKey(item)
							? 3
								: -1;
		}

		// get the items in a player's inventory
		public static int PlayerItemCount(Player pl) => pl.GetInventory().Count(item => item.ItemType != ItemType.NULL);

		// find if the player is human, scp, or other
		public static int TypeOfPlayerClass(string player)
		{
			return Aliases.Humans.ContainsKey(player)
							? 0
								: Aliases.SCPs.ContainsKey(player)
							? 1
								: Aliases.Other.ContainsKey(player)
							? 2
								: -1;
		}
	}
}
