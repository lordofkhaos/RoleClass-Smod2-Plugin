using System;
using System.Collections.Generic;
using System.Linq;
using Smod2;
using Smod2.API;

namespace RoleClass
{
	public static class GLaDOS
	{
		private static Aliases aliases = new Aliases();


		// read my pretty config entry
		public static Dictionary<string, Dictionary<string, List<ItemType>>> GetRoleClassConfig()
		{
			var endyeet = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			try
			{
				aliases.AssignAliases();
				var yeet = ConfigFile.GetDict("k_roleclass"); // format= string: list,of,strings
				foreach (KeyValuePair<string, string> roleClassPair in yeet)
				{
					string rank = roleClassPair.Key;
					string klasy = roleClassPair.Value.Split('[')[0];
					string[] itemsArray = roleClassPair.Value.Split('[');
					List<string> itemsButTheyreStrings = new List<string>(itemsArray).Skip(1).ToList();
					List<ItemType> items = new List<ItemType>();
					Dictionary<string, List<ItemType>> classItems = new Dictionary<string, List<ItemType>>();

					itemsButTheyreStrings.Remove("]");
					foreach (string item in itemsButTheyreStrings)
					{
						int typeOfItem = TypeOfItem(item);
						ItemType myItem = ItemType.NULL;
						myItem  = (ItemType)TypeOfItem(item);
						items.Add(myItem);
					}

					classItems.Add(klasy, items);
					endyeet.Add(rank, classItems);
				}
				return endyeet;
			}
			catch (Exception e)
			{
				return endyeet;
			}
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

		public static int PlayerItemCount(Player pl) => pl.GetInventory().Count(item => item.ItemType != ItemType.NULL);

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
