using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using Smod2;
using Smod2.API;
using Smod2.Events;

namespace RoleClass.Assists
{
	/// <summary>
	/// The helper class used primarily in the RoleClass plugin by lordofkhaos
	/// </summary>
	public static class Ancillary
	{
		private static readonly RoleClass Plugin = new RoleClass();

		// # example:
		// krc_items:
		//  - role:class[item.item.item]
		//  - owner:classd[1]
		// # if more than one in this list it's randomized
		// krc_health:
		//  - role:class[hp]
		// 
		/// <summary>
		/// For returning a Dictionary of string keys with values of dictionaries with keys of strings and values of item lists.
		/// </summary>
		public static Dictionary<string, Dictionary<string, List<ItemType>>> ReturnSpecialConfig(string key)
		{
			// Setup the end variable
			Dictionary<string, Dictionary<string, List<ItemType>>> result = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			try
			{
				// Retrieve the raw input from the config
				var rawInput = ConfigFile.ServerConfig.GetStringDictionary(key);
				// Loop over the raw input pairs
				foreach (KeyValuePair<string, string> configPair in rawInput)
				{
					// Assign the key locally
					string localKey = configPair.Key;
					localKey = localKey.Replace(" ", string.Empty);

					// The key to the dictionary value
					string subKey = configPair.Value.Split('[')[0];
					subKey = subKey.Replace(" ", string.Empty);

					// Split the items off
					string[] itemsArray = configPair.Value.Split('[');
					// Place the items into a list
					List<string> itemsButTheyreStrings = new List<string>(itemsArray).Skip(1).ToList();
					// Remove the end bracket (it's just aesthetics)
					itemsButTheyreStrings.Remove("]");
					// Setup the end list of items
					List<ItemType> items = new List<ItemType>();
					
					// Loop through the items and find which one they are
					foreach (string item in itemsButTheyreStrings)
					{
						// Find the type of item,
						KItemType typeOfItem = item.Replace(" ", string.Empty).Replace("ITEM#", string.Empty).TrimStart('0').GetTypeOfItem();
						// Setup which item it is
						ItemType thisItem = ItemType.NULL;
						// Find the item from its alias
						switch (typeOfItem)
						{
							case KItemType.Accessory:
								Aliases.Accessories.TryGetValue(item, out thisItem);
								break;
							case KItemType.Ammo:
								Aliases.Ammo.TryGetValue(item, out thisItem);
								break;
							case KItemType.Keycard:
								Aliases.Accessories.TryGetValue(item, out thisItem);
								break;
							case KItemType.Weapon:
								Aliases.Accessories.TryGetValue(item, out thisItem);
								break;
						}
						// Add the found item to the list of items
						items.Add(thisItem);
					}
					// Put the subkey and items into a dictionary
					Dictionary<string, List<ItemType>> classItems = new Dictionary<string, List<ItemType>> { { subKey, items } };
					// Add to the result
					result.Add(localKey, classItems);
				}
				// Outside the loop - return the final value
				return result;
			}
			// In case it fails somewhere, return an empty Dictionary
			catch (Exception)
			{
				return result;
			}
		}

		/// <summary>
		/// Read the special JSON formated config - same format as the special config
		/// </summary>
		public static Dictionary<string, Dictionary<string, List<ItemType>>> ReadSpecialJsonConfig() => JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<ItemType>>>>(File.ReadAllText($"{GetKhaosFolder()}RoleClass{Path.DirectorySeparatorChar}config.json"));

		/// <summary>
		/// Call this to transfer everything from the old binary file to a json file
		/// </summary>
		public static void BackwardsCompat()
		{
			// Each of these is a possible old config file
			const string	path1 = "rolclass.cfgbin",
							path2 = "rc-config.dat",
							path3 = "dictionary.bin";
			// Find if the old file exists, and if it doesn't, no need to do anything
			bool path1Exists = File.Exists(path1);
			bool path2Exists = File.Exists(path2);
			bool path3Exists = File.Exists(path3);
			bool oldFileExists = path1Exists || path2Exists || path3Exists;
			if (!oldFileExists) return;
			// Setup the binary formatter to read from the old file
			BinaryFormatter bf = new BinaryFormatter();
			// Setup the dictionary that is to be converted to json
			Dictionary<string, Dictionary<string, List<ItemType>>> prematureResult = new Dictionary<string, Dictionary<string, List<ItemType>>>();
			// It's possible for all three to exist
			if (path1Exists)
				AddDeserializedData(path1,
					bf,
					prematureResult);
			if (path2Exists)
				AddDeserializedData(path2,
					bf,
					prematureResult);
			if (path3Exists)
				AddDeserializedData(path3,
					bf,
					prematureResult);
			// Convert to json
			string result = JsonConvert.SerializeObject(prematureResult);
			string roleClassConfigLocation = GetKhaosFolder() + "RoleClass";
			string roleClassConfigFile = roleClassConfigLocation + Path.DirectorySeparatorChar + "config.json";
			if (!Directory.Exists(roleClassConfigLocation)) Directory.CreateDirectory(roleClassConfigLocation);
			if (!File.Exists(roleClassConfigFile)) File.Create(roleClassConfigFile);
			// Open the file and write to it
			using (StreamWriter sw = File.AppendText(roleClassConfigFile))
			{
				sw.Write(result);
			}
			// If legacy mode is enabled, don't delete
			if (Plugin.GetConfigBool("krc_legacy_enable")) return;
			// Delete the file(s)
			if (path1Exists) File.Delete(path1);
			if (path2Exists) File.Delete(path2);
			if (path3Exists) File.Delete(path3);
		}

		private static void AddDeserializedData(string path1, BinaryFormatter bf, Dictionary<string, Dictionary<string, List<ItemType>>> prematureResult)
		{
// Use the method to retrieve the deserialized data
			Dictionary<string, Dictionary<string, List<string>>> deserializedData = DeserializedData(path1,
				bf);
			// Retrieve the list of items
			deserializedData.TryGetValue(deserializedData.First()
					.Key,
				out Dictionary<string, List<string>> classItems);
			List<ItemType> itemTypes = new List<ItemType>();
			if (classItems == null) return;
			classItems.TryGetValue(classItems.First()
					.Key,
				out List<string> items);
			// Find which item the alias refers to, and add to the list
			if (items != null)
				foreach (string item in items)
				{
					if (Aliases.Keycards.TryGetValue(item,
						out ItemType typeK))
						itemTypes.Add(typeK);
					else if (Aliases.Accessories.TryGetValue(item,
						out ItemType typeA))
						itemTypes.Add(typeA);
					else if (Aliases.Weapons.TryGetValue(item,
						out ItemType typeW))
						itemTypes.Add(typeW);
					else if (Aliases.Ammo.TryGetValue(item,
						out ItemType type))
						itemTypes.Add(type);
				}

			prematureResult.Add(key: deserializedData.First()
					.Key,
				value: new Dictionary<string, List<ItemType>>
				{
					{
						classItems.First()
							.Key,
						itemTypes
					}
				});
		}

		// Internal method to read the special binary file - no need to make this public
		internal static Dictionary<string, Dictionary<string, List<string>>> DeserializedData(string path, BinaryFormatter bf)
		{
			// The binary file is in this format
			Dictionary<string, List<string>> deserializedData = new Dictionary<string, List<string>>();
			// Open the file in read then copy it to the dictionary
			using (FileStream fs = File.OpenRead(path))
			{
				deserializedData = (Dictionary<string, List<string>>)bf.Deserialize(fs);
			}
			// Begin setting up the final values
			string endkey = string.Empty;
			string subkey = string.Empty; // Apparently it worked this way? Ngl that's bad
			List<string> myList = new List<string>();
			foreach (KeyValuePair<string, List<string>> kvp in deserializedData)
			{
				endkey = kvp.Key; // Only one key was possible
				subkey = kvp.Value[0]; // Will always be the first value of the list
				myList = (List<string>)kvp.Value.Skip(1); // This list will be comprised of everything besides the first
			}
			// Build the end dictionary
			return new Dictionary<string, Dictionary<string, List<string>>>
			{
				{
					endkey, new Dictionary<string, List<string>>
					{
						{ subkey, myList }
					}
				}
			};
		}

		/// <summary>
		/// Return the specific folder used in lordofkhaos's plugins
		/// </summary>
		/// <returns></returns>
		public static string GetKhaosFolder() => FileManager.GetAppFolder() + "Khaos" + Path.DirectorySeparatorChar;

		/// <summary>
		/// An enum used in the RoleClass ancillary method GetTypeOfItem
		/// </summary>
		public enum KItemType
		{
			/// <summary>
			/// If the item is a Keycard
			/// </summary>
			Keycard,
			/// <summary>
			/// If the item is a Weapon
			/// </summary>
			Weapon,
			/// <summary>
			/// If the item is not a <see cref="Keycard"/>, <see cref="Weapon"/>, or <see cref="Ammo"/>
			/// </summary>
			Accessory,
			/// <summary>
			/// If the provided item is a type of Ammo
			/// </summary>
			Ammo,
			/// <summary>
			/// If the item is not a <see cref="Keycard"/>, <see cref="Weapon"/>, <see cref="Accessory"/>, or <see cref="Ammo"/>
			/// </summary>
			Not
		}

		/// <summary>
		/// Extension method on string to return which type of item is the provided string
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public static KItemType GetTypeOfItem(this string item)
		{
			return Aliases.Keycards.ContainsKey(item)
							? KItemType.Keycard
								: Aliases.Weapons.ContainsKey(item)
							? KItemType.Weapon
								: Aliases.Accessories.ContainsKey(item)
							? KItemType.Accessory
								: Aliases.Ammo.ContainsKey(item)
							? KItemType.Ammo
								: KItemType.Not;
		}

		/// <summary>
		/// Extension method to return the number of items in a player's inventory
		/// </summary>
		/// <param name="pl"></param>
		/// <returns></returns>
		public static int PlayerItemCount(this Player pl) => pl.GetInventory().Count(item => item.ItemType != ItemType.NULL);

		/// <summary>
		/// An enum used in the RoleClass ancillary method GetTypeOfPlayer
		/// </summary>
		public enum KPlayerClass
		{
			/// <summary>
			/// If the provided player is a Human
			/// </summary>
			Human,
			/// <summary>
			/// If the provided player is an SCP
			/// </summary>
			SCP,
			/// <summary>
			/// If the provided player is neither a Human nor an SCP
			/// </summary>
			Other,
			/// <summary>
			/// If the provided player does not belong to any other group
			/// </summary>
			Not

		}

		/// <summary>
		/// Extension method on string to return which type of class the player is from the provided string
		/// </summary>
		/// <param name="player"></param>
		/// <returns></returns>
		public static KPlayerClass GetPlayerClass(this string player)
		{
			return Aliases.Humans.ContainsKey(player)
							? KPlayerClass.Human
								: Aliases.SCPs.ContainsKey(player)
							? KPlayerClass.SCP
								: Aliases.Other.ContainsKey(player)
							? KPlayerClass.Other
								: KPlayerClass.Not;
		}

		/// <summary>
		/// Extension method to give items to a player in the PlayerSetRoleEvent
		/// </summary>
		/// <param name="ev"></param>
		/// <param name="config"></param>
		public static void GivePlayerItems(this Smod2.Events.PlayerSetRoleEvent ev, Dictionary<string, Dictionary<string, List<ItemType>>> config)
		{
			List<ItemType> configItems = new List<ItemType>();
			List<ItemType> localItems1 = new List<ItemType>();
			List<ItemType> localItems2 = new List<ItemType>();
			List<ItemType> localItems3 = new List<ItemType>();
			// Setup the dictionaries needed
			Dictionary<string, List<ItemType>> localDict1 = new Dictionary<string, List<ItemType>>(),
				localDict2 = new Dictionary<string, List<ItemType>>(),
				localDict3 = new Dictionary<string, List<ItemType>>();
			// If the player's rank is not, return in the config
			if ((!config.TryGetValue(ev.Player.GetUserGroup().Name, out localDict1) &&
			     !config.TryGetValue(ev.Player.GetRankName(), out localDict2) &&
			     !config.TryGetValue(ev.Player.SteamId, out localDict3))) return;
			// Check if it's their rank name or display name that is the key
			bool	checkClassIn1 = localDict1.TryGetValue(ev.Player.TeamRole.Role.ToString(), out localItems1),
					checkClassIn2 = localDict2.TryGetValue(ev.Player.TeamRole.Role.ToString(), out localItems2),
					checkSteamId = localDict3.TryGetValue(ev.Player.SteamId, out localItems3);
			if (checkClassIn1) configItems.AddRange(localItems1);
			if (checkClassIn2) configItems.AddRange(localItems2);
			if (checkSteamId) configItems.AddRange(localItems3);
			// Give the player the items - if they aren't the right class, it will add an empty List
			foreach (ItemType it in configItems)
			{
				// Ignore ammo - this is controlled by a different config
				if (GetTypeOfItem(it.ToString()) == KItemType.Ammo) continue;
				// Spawn the items at the player if their inventory exceeds the limit
				if (ev.Player.PlayerItemCount() > 8)
				{
					Vector myPos = ev.Player.GetPosition();
					Vector myRot = ev.Player.GetRotation();
					PluginManager.Manager.Server.Map.SpawnItem(it, myPos, myRot);
				}
				else
				{
					ev.Items.Add(it);
				}
			}
		}
	}
}
