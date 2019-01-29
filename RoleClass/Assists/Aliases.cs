using System.Collections.Generic;
using Smod2.API;

namespace RoleClass.Assists
{
	/// <summary>
	/// A collection of <see cref="Aliases"/> designed to account for any user input
	/// </summary>
	public static class Aliases
	{
		#region Roles
		/// <summary>
		/// The static dictionary of SCP aliases
		/// </summary>
		public static Dictionary<string, Role> SCPs = new Dictionary<string, Role>()
		{
			{ Role.SCP_049.ToString().ToLower(), Role.SCP_049 },
			{ "plaguedaddy", Role.SCP_049 },
			{ "doctor", Role.SCP_049 },
			{ "049", Role.SCP_049 },
			{ "5", Role.SCP_049 },
			{ Role.SCP_049_2.ToString().ToLower(), Role.SCP_049_2 },
			{ "zombie", Role.SCP_049_2 },
			{ "0492", Role.SCP_049_2 },
			{ "helicopter", Role.SCP_049_2 },
			{ "plane", Role.SCP_049_2 },
			{ "10", Role.SCP_049_2 },
			{ Role.SCP_106.ToString().ToString(), Role.SCP_106 },
			{ "larry", Role.SCP_106 },
			{ "106", Role.SCP_106 },
			{ "3", Role.SCP_106 },
			{ Role.SCP_096.ToString().ToLower(), Role.SCP_096 },
			{ "shyguy", Role.SCP_096 },
			{ "096", Role.SCP_096 },
			{ "9", Role.SCP_096 },
			{ Role.SCP_079.ToString().ToLower(), Role.SCP_079 },
			{ "079", Role.SCP_079 },
			{ "comp", Role.SCP_079 },
			{ "computer", Role.SCP_079 },
			{ "7", Role.SCP_079 },
			{ Role.SCP_173.ToString().ToLower(), Role.SCP_173 },
			{ "peanut", Role.SCP_173 },
			{ "173", Role.SCP_173 },
			{ "0", Role.SCP_173 },
			{ Role.SCP_939_53.ToString().ToLower(), Role.SCP_939_53 },
			{ "doggo1", Role.SCP_939_53 },
			{ "93953", Role.SCP_939_53 },
			{ "16", Role.SCP_939_53 },
			{ Role.SCP_939_89.ToString().ToLower(), Role.SCP_939_89 },
			{ "doggo2", Role.SCP_939_89 },
			{ "93989", Role.SCP_939_89 },
			{ "17", Role.SCP_939_89 }
		};

		/// <summary>
		/// The static dictionary of Human aliases
		/// </summary>
		public static Dictionary<string, Role> Humans = new Dictionary<string, Role>()
		{
			{ Role.NTF_COMMANDER.ToString().ToLower(), Role.NTF_COMMANDER },
			{ "mtfc", Role.NTF_COMMANDER },
			{ "ntfc", Role.NTF_COMMANDER },
			{ "commander", Role.NTF_COMMANDER },
			{ "12", Role.NTF_COMMANDER },
			{ Role.NTF_LIEUTENANT.ToString().ToLower(), Role.NTF_LIEUTENANT },
			{ "mtfl", Role.NTF_LIEUTENANT },
			{ "ntfl", Role.NTF_LIEUTENANT },
			{ "lieutenant", Role.NTF_LIEUTENANT },
			{ "11", Role.NTF_LIEUTENANT },
			{ Role.NTF_CADET.ToString().ToLower(), Role.NTF_CADET },
			{ "cadet", Role.NTF_CADET },
			{ "mtf", Role.NTF_CADET },
			{ "ntf", Role.NTF_CADET },
			{ "13", Role.NTF_CADET },
			{ Role.NTF_SCIENTIST.ToString().ToLower(), Role.NTF_SCIENTIST },
			{ "mtfs", Role.NTF_SCIENTIST },
			{ "mtfsci", Role.NTF_SCIENTIST },
			{ "mtfscientist", Role.NTF_SCIENTIST },
			{ "ntfs", Role.NTF_SCIENTIST },
			{ "ntfsci", Role.NTF_SCIENTIST },
			{ "ntfscientist", Role.NTF_SCIENTIST },
			{ "4", Role.NTF_SCIENTIST },
			{ Role.CHAOS_INSURGENCY.ToString().ToLower(), Role.CHAOS_INSURGENCY },
			{ "ci", Role.CHAOS_INSURGENCY },
			{ "chaos", Role.CHAOS_INSURGENCY },
			{ "insurgent", Role.CHAOS_INSURGENCY },
			{ "insurgency", Role.CHAOS_INSURGENCY },
			{ "chaosagent", Role.CHAOS_INSURGENCY },
			{ "chaosinsurgent", Role.CHAOS_INSURGENCY },
			{ "chaosinsurgency", Role.CHAOS_INSURGENCY },
			{ "8", Role.CHAOS_INSURGENCY },
			{ Role.CLASSD.ToString().ToLower(), Role.CLASSD },
			{ "cd", Role.CLASSD },
			{ "dc", Role.CLASSD },
			{ "dboi", Role.CLASSD },
			{ "dbois", Role.CLASSD },
			{ "classd", Role.CLASSD },
			{ "dclass", Role.CLASSD },
			{ "1", Role.CLASSD },
			{ Role.FACILITY_GUARD.ToString().ToLower(), Role.FACILITY_GUARD },
			{ "fg", Role.FACILITY_GUARD },
			{ "guard", Role.FACILITY_GUARD },
			{ "facilityguard", Role.FACILITY_GUARD },
			{ "15", Role.FACILITY_GUARD },
			{ Role.SCIENTIST.ToString().ToLower(), Role.SCIENTIST },
			{ "sci", Role.SCIENTIST },
			{ "nerd", Role.SCIENTIST },
			{ "scientist", Role.SCIENTIST },
			{ "scienceboi", Role.SCIENTIST },
			{ "6", Role.SCIENTIST },
			{ Role.TUTORIAL.ToString().ToLower(), Role.TUTORIAL },
			{ "tut", Role.TUTORIAL },
			{ "tutor", Role.TUTORIAL },
			{ "tutorial", Role.TUTORIAL },
			{ "14", Role.TUTORIAL }
		};

		/// <summary>
		/// The static dictionary of playable classes that are neither Human nor SCP
		/// </summary>
		public static Dictionary<string, Role> Other = new Dictionary<string, Role>()
		{
			{ Role.SPECTATOR.ToString().ToLower(), Role.SPECTATOR },
			{ "spec", Role.SPECTATOR },
			{ "specboi", Role.SPECTATOR },
			{ "ghost" ,Role.SPECTATOR },
			{ "2", Role.SPECTATOR }
		};

		#endregion

		#region Items

		/// <summary>
		/// The static dictionary of aliases for Keycards
		/// </summary>
		public static Dictionary<string, ItemType> Keycards = new Dictionary<string, ItemType>()
		{
			{ ItemType.CHAOS_INSURGENCY_DEVICE.ToString().ToLower(), ItemType.CHAOS_INSURGENCY_DEVICE },
			{ "cidevice", ItemType.CHAOS_INSURGENCY_DEVICE },
			{ "kcci", ItemType.CHAOS_INSURGENCY_DEVICE },
			{ "ci", ItemType.CHAOS_INSURGENCY_DEVICE },
			{ ItemType.CONTAINMENT_ENGINEER_KEYCARD.ToString().ToLower(), ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ "ce", ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ "containmentengineer", ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ "pinkcard", ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ "pink", ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ "kcce", ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ "6", ItemType.CONTAINMENT_ENGINEER_KEYCARD },
			{ ItemType.FACILITY_MANAGER_KEYCARD.ToString().ToLower(), ItemType.FACILITY_MANAGER_KEYCARD },
			{ "red", ItemType.FACILITY_MANAGER_KEYCARD },
			{ "9", ItemType.FACILITY_MANAGER_KEYCARD },
			{ "kcfm", ItemType.FACILITY_MANAGER_KEYCARD },
			{ "redcard", ItemType.FACILITY_MANAGER_KEYCARD },
			{ ItemType.GUARD_KEYCARD.ToString().ToLower(), ItemType.GUARD_KEYCARD },
			{ "kcguard", ItemType.GUARD_KEYCARD },
			{ "guardkey", ItemType.GUARD_KEYCARD },
			{ "4", ItemType.GUARD_KEYCARD },
			{ ItemType.JANITOR_KEYCARD.ToString().ToLower(), ItemType.JANITOR_KEYCARD },
			{ "kcjan", ItemType.JANITOR_KEYCARD },
			{ "janitor", ItemType.JANITOR_KEYCARD},
			{ "0", ItemType.JANITOR_KEYCARD },
			{ ItemType.MTF_COMMANDER_KEYCARD.ToString().ToLower(), ItemType.MTF_COMMANDER_KEYCARD },
			{ "kcmtfc", ItemType.MTF_COMMANDER_KEYCARD },
			{ "kcntfc", ItemType.MTF_COMMANDER_KEYCARD },
			{ "commanderkey", ItemType.MTF_COMMANDER_KEYCARD },
			{ "8", ItemType.MTF_COMMANDER_KEYCARD },
			{ "kcmtfl", ItemType.MTF_LIEUTENANT_KEYCARD },
			{ "kcntfl", ItemType.MTF_LIEUTENANT_KEYCARD },
			{ "lieutenantkey", ItemType.MTF_LIEUTENANT_KEYCARD },
			{ "7", ItemType.MTF_LIEUTENANT_KEYCARD },
			{ ItemType.MTF_LIEUTENANT_KEYCARD.ToString().ToLower(), ItemType.MTF_LIEUTENANT_KEYCARD },
			{ "o5", ItemType.O5_LEVEL_KEYCARD },
			{ "kco5", ItemType.O5_LEVEL_KEYCARD },
			{ "black", ItemType.O5_LEVEL_KEYCARD },
			{ "blackcard", ItemType.O5_LEVEL_KEYCARD },
			{ "11", ItemType.O5_LEVEL_KEYCARD },
			{ ItemType.O5_LEVEL_KEYCARD.ToString().ToLower(), ItemType.O5_LEVEL_KEYCARD },
			{ "sci", ItemType.SCIENTIST_KEYCARD },
			{ "kcsci", ItemType.SCIENTIST_KEYCARD },
			{ "1", ItemType.SCIENTIST_KEYCARD },
			{ ItemType.SCIENTIST_KEYCARD.ToString().ToLower().ToLower(), ItemType.SCIENTIST_KEYCARD },
			{ "kcmg", ItemType.SENIOR_GUARD_KEYCARD },
			{ "senior", ItemType.SENIOR_GUARD_KEYCARD },
			{ "seniorguard", ItemType.SENIOR_GUARD_KEYCARD },
			{ "sgk", ItemType.SENIOR_GUARD_KEYCARD },
			{ "5", ItemType.SENIOR_GUARD_KEYCARD },
			{ ItemType.SENIOR_GUARD_KEYCARD.ToString().ToLower(), ItemType.SENIOR_GUARD_KEYCARD },
			{ "wmt", ItemType.WEAPON_MANAGER_TABLET },
			{ "tablet", ItemType.WEAPON_MANAGER_TABLET },
			{ "19", ItemType.WEAPON_MANAGER_TABLET },
			{ ItemType.WEAPON_MANAGER_TABLET.ToString().ToLower(), ItemType.WEAPON_MANAGER_TABLET },
			{ "blue", ItemType.ZONE_MANAGER_KEYCARD },
			{ "zone", ItemType.ZONE_MANAGER_KEYCARD },
			{ "kczm", ItemType.ZONE_MANAGER_KEYCARD },
			{ "3", ItemType.ZONE_MANAGER_KEYCARD },
			{ ItemType.ZONE_MANAGER_KEYCARD.ToString().ToLower(), ItemType.ZONE_MANAGER_KEYCARD },
			{ ItemType.MAJOR_SCIENTIST_KEYCARD.ToString().ToLower(), ItemType.MAJOR_SCIENTIST_KEYCARD },
			{ "majscikey", ItemType.MAJOR_SCIENTIST_KEYCARD },
			{ "majorsci", ItemType.MAJOR_SCIENTIST_KEYCARD },
			{ "majorscientistkeycard", ItemType.MAJOR_SCIENTIST_KEYCARD },
			{ "2", ItemType.MAJOR_SCIENTIST_KEYCARD },
		};

		/// <summary>
		/// The static dictionary of aliases for Weapons
		/// </summary>
		public static Dictionary<string, ItemType> Weapons = new Dictionary<string, ItemType>()
		{
			{ "pistol", ItemType.COM15 },
			{ "com15", ItemType.COM15 },
			{ "handgun", ItemType.COM15 },
			{ "13", ItemType.COM15 },
			{ ItemType.COM15.ToString().ToLower(), ItemType.COM15 },
			{ "mtfgun", ItemType.E11_STANDARD_RIFLE },
			{ "ntfgun", ItemType.E11_STANDARD_RIFLE },
			{ "epsilon", ItemType.E11_STANDARD_RIFLE },
			{ "epsilonstandardrifle", ItemType.E11_STANDARD_RIFLE },
			{ "epsilon11", ItemType.E11_STANDARD_RIFLE },
			{ "epsilon11standardrifle", ItemType.E11_STANDARD_RIFLE },
			{ "e11", ItemType.E11_STANDARD_RIFLE },
			{ "esr", ItemType.E11_STANDARD_RIFLE },
			{ "20", ItemType.E11_STANDARD_RIFLE },
			{ ItemType.E11_STANDARD_RIFLE.ToString().ToLower(), ItemType.E11_STANDARD_RIFLE },
			{ "fb", ItemType.FLASHBANG },
			{ "stun", ItemType.FLASHBANG },
			{ "flashbang", ItemType.FLASHBANG },
			{ "stungrenade", ItemType.FLASHBANG },
			{ "26", ItemType.FLASHBANG },
			{ ItemType.FLASHBANG.ToString().ToLower(), ItemType.FLASHBANG },
			{ "frag", ItemType.FRAG_GRENADE },
			{ "grenade", ItemType.FRAG_GRENADE },
			{ "boom", ItemType.FRAG_GRENADE },
			{ "25", ItemType.FRAG_GRENADE },
			{ ItemType.FRAG_GRENADE.ToString().ToLower(), ItemType.FRAG_GRENADE },
			{ "chaosgun", ItemType.LOGICER },
			{ "logicer", ItemType.LOGICER },
			{ "cigun", ItemType.LOGICER },
			{ "lmg", ItemType.LOGICER },
			{ "24", ItemType.LOGICER },
			{ ItemType.LOGICER.ToString().ToLower(), ItemType.LOGICER },
			{ "micro", ItemType.MICROHID },
			{ "microwave", ItemType.MICROHID },
			{ "supersoaker", ItemType.MICROHID },
			{ "microhid", ItemType.MICROHID },
			{ "16", ItemType.MICROHID },
			{ ItemType.MICROHID.ToString().ToLower(), ItemType.MICROHID },
			{ "mp7", ItemType.MP4 },
			{ "mp4", ItemType.MP4 },
			{ "smg", ItemType.MP4 },
			{ "scorpion", ItemType.MP4 },
			{ "23", ItemType.MP4 },
			{ ItemType.MP4.ToString().ToLower(), ItemType.MP4 },
			{ "p90", ItemType.P90 },
			{ "russia", ItemType.P90 },
			{ "russian", ItemType.P90 },
			{ "21", ItemType.P90 },
			{ ItemType.P90.ToString().ToLower(), ItemType.P90 },
		};

		/// <summary>
		/// The static dictionary of aliases for Ammo
		/// </summary>
		public static Dictionary<string, ItemType> Ammo = new Dictionary<string, ItemType>()
		{
			{ "fusion", ItemType.DROPPED_5 },
			{ "mtfammo", ItemType.DROPPED_5 },
			{ "ntfammo", ItemType.DROPPED_5 },
			{ "e11ammo", ItemType.DROPPED_5 },
			{ "ammo5", ItemType.DROPPED_5 },
			{ "556mm", ItemType.DROPPED_5 },
			{ "22", ItemType.DROPPED_5 },
			{ ItemType.DROPPED_5.ToString().ToLower(), ItemType.DROPPED_5 },
			{ "9mm", ItemType.DROPPED_7 },
			{ "pat", ItemType.DROPPED_7 },
			{ "ammo7", ItemType.DROPPED_7 },
			{ "29", ItemType.DROPPED_7 },
			{ ItemType.DROPPED_7.ToString().ToLower(), ItemType.DROPPED_7 },
			{ "762mm", ItemType.DROPPED_9 },
			{ "rat", ItemType.DROPPED_9 },
			{ "ammo9", ItemType.DROPPED_9 },
			{ "28", ItemType.DROPPED_9 },
			{ ItemType.DROPPED_9.ToString().ToLower(), ItemType.DROPPED_9 },
		};

		/// <summary>
		/// The static dictionary of aliases for items that do not belong to the <see cref="Keycards"/> group, the <see cref="Weapons"/> group, or the <see cref="Ammo"/> group
		/// </summary>
		public static Dictionary<string, ItemType> Accessories = new Dictionary<string, ItemType>()
		{
			{ "coin", ItemType.COIN },
			{ "quarter", ItemType.COIN },
			{ "25c", ItemType.COIN },
			{ "50c", ItemType.COIN },
			{ "17", ItemType.COIN },
			{ ItemType.COIN.ToString().ToLower(), ItemType.COIN },
			{ "cup", ItemType.CUP },
			{ "18", ItemType.CUP },
			{ ItemType.CUP.ToString().ToLower(), ItemType.CUP },
			{ "disarm", ItemType.DISARMER },
			{ "disarmer", ItemType.DISARMER },
			{ "detain", ItemType.DISARMER },
			{ "detainer", ItemType.DISARMER },
			{ "handcuffs", ItemType.DISARMER },
			{ ItemType.DISARMER.ToString().ToLower(), ItemType.DISARMER },
			{ "flashlight", ItemType.FLASHLIGHT },
			{ "fl", ItemType.FLASHLIGHT },
			{ "torch", ItemType.FLASHLIGHT },
			{ "lamp", ItemType.FLASHLIGHT },
			{ "15", ItemType.FLASHLIGHT },
			{ ItemType.FLASHLIGHT.ToString().ToLower(), ItemType.FLASHLIGHT },
			{ "medkit", ItemType.MEDKIT },
			{ "med", ItemType.MEDKIT },
			{ "14", ItemType.MEDKIT },
			{ ItemType.MEDKIT.ToString().ToLower(), ItemType.MEDKIT },
			{ "rad", ItemType.RADIO },
			{ "radio", ItemType.RADIO },
			{ "walkietalkie", ItemType.RADIO },
			{ "12", ItemType.RADIO },
			{ ItemType.RADIO.ToString().ToLower(), ItemType.RADIO },
		};

		#endregion
	}
}