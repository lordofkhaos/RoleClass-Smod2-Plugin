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
			// Setup the end variable
			var result = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			try
			{
				// Retrieve the raw input from the config
				var rawInput = ConfigFile.ServerConfig.GetStringDictionary(key);
				// Loop over the raw input and a
				foreach (KeyValuePair<string, string> roleClassPair in rawInput)
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
						// Find the type of item, 
						int typeOfItem = TypeOfItem(item.Replace(" ", string.Empty).Replace("ITEM#", string.Empty).TrimStart('0'));
						ItemType myItem = ItemType.NULL;
						myItem = (ItemType)typeOfItem;
						items.Add(myItem);
					}

					classItems.Add(klasa, items);
					result.Add(rank, classItems);
				}
				return result;
			}
			catch (Exception)
			{
				return result;
			}
		}
		/// <summary>
		/// Find out which item group the input belongs to.
		/// </summary>
		/// <param name="item"></param>
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

		/// <summary>
		/// Return the number of items in a player's inventory
		/// </summary>
		/// <param name="pl"></param>
		/// <returns></returns>
		public static int PlayerItemCount(Player pl) => pl.GetInventory().Count(item => item.ItemType != ItemType.NULL);

		/// <summary>
		/// Determine which group the inputted player belongs to.
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
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
