using Smod2.API;
using System.Collections.Generic;
namespace RoleClass
{
    public struct Aliases
    {
        public Dictionary<string, Role> SCPs
        {
            get;
            set;
        }
        public Dictionary<string, Role> Humans
        {
            get;
            set;
        }
        public Dictionary<string, Role> Other
        {
            get;
            set;
        }
        public Dictionary<string, ItemType> Keycards
        {
            get;
            set;
        }
        public Dictionary<string, ItemType> Weapons
        {
            get;
            set;
        }
        public Dictionary<string, ItemType> Ammo
        {
            get;
            set;
        }
        public Dictionary<string, ItemType> Accessories
        {
            get;
            set;
        }

        public void AssignAliases()
        {
            #region dare ordini agnomes
            // prae perficio
            SCPs = new Dictionary<string, Role>();
            Humans = new Dictionary<string, Role>();
            Other = new Dictionary<string, Role>();
            // perficio

            // cmdr
            var ntfc = Role.NTF_COMMANDER.ToString().ToLower();
            Humans["mtfc"] = Role.NTF_COMMANDER;
            Humans["ntfc"] = Role.NTF_COMMANDER;
            Humans["commander"] = Role.NTF_COMMANDER;
            Humans["12"] = Role.NTF_COMMANDER;
            Humans[ntfc] = Role.NTF_COMMANDER;

            // lt
            var ntfl = Role.NTF_LIEUTENANT.ToString().ToLower();
            Humans["mtfl"] = Role.NTF_LIEUTENANT;
            Humans["ntfl"] = Role.NTF_LIEUTENANT;
            Humans["lieutenant"] = Role.NTF_LIEUTENANT;
            Humans["11"] = Role.NTF_LIEUTENANT;
            Humans[ntfl] = Role.NTF_LIEUTENANT;

            // cadet
            var ntf = Role.NTF_CADET.ToString().ToLower();
            Humans["cadet"] = Role.NTF_CADET;
            Humans["mtf"] = Role.NTF_CADET;
            Humans["ntf"] = Role.NTF_CADET;
            Humans["13"] = Role.NTF_CADET;
            Humans[ntf] = Role.NTF_CADET;

            // msci
            var ntfs = Role.NTF_SCIENTIST.ToString().ToLower();
            Humans["mtfs"] = Role.NTF_SCIENTIST;
            Humans["ntfs"] = Role.NTF_SCIENTIST;
            Humans["mtfsci"] = Role.NTF_SCIENTIST;
            Humans["ntfsci"] = Role.NTF_SCIENTIST;
            Humans["4"] = Role.NTF_SCIENTIST;
            Humans[ntfs] = Role.NTF_SCIENTIST;

            var ci = Role.CHAOS_INSUGENCY.ToString().ToLower();
            Humans["ci"] = Role.CHAOS_INSUGENCY;
            Humans["chaos"] = Role.CHAOS_INSUGENCY;
            Humans["insurgency"] = Role.CHAOS_INSUGENCY;
            Humans["insurgent"] = Role.CHAOS_INSUGENCY;
            Humans["8"] = Role.CHAOS_INSUGENCY;
            Humans[ci] = Role.CHAOS_INSUGENCY;

            var cd = Role.CLASSD.ToString().ToLower();
            Humans["classd"] = Role.CLASSD;
            Humans["dclass"] = Role.CLASSD;
            Humans["cd"] = Role.CLASSD;
            Humans["dc"] = Role.CLASSD;
            Humans["1"] = Role.CLASSD;
            Humans[cd] = Role.CLASSD;

            var fg = Role.FACILITY_GUARD.ToString().ToLower();
            Humans["guard"] = Role.FACILITY_GUARD;
            Humans["fg"] = Role.FACILITY_GUARD;
            Humans["15"] = Role.FACILITY_GUARD;
            Humans[fg] = Role.FACILITY_GUARD;

            var sci = Role.SCIENTIST.ToString().ToLower();
            Humans["sci"] = Role.SCIENTIST;
            Humans["nerd"] = Role.SCIENTIST;
            Humans["scientist"] = Role.SCIENTIST;
            Humans["science"] = Role.SCIENTIST;
            Humans["6"] = Role.SCIENTIST;
            Humans[sci] = Role.SCIENTIST;

            var plaguedaddy = Role.SCP_049.ToString().ToLower();
            SCPs["plaguedaddy"] = Role.SCP_049;
            SCPs["doctor"] = Role.SCP_049;
            SCPs["049"] = Role.SCP_049;
            SCPs["5"] = Role.SCP_049;
            SCPs[plaguedaddy] = Role.SCP_049;

            var zombie = Role.SCP_049_2.ToString().ToLower();
            SCPs["zombie"] = Role.SCP_049_2;
            SCPs["0492"] = Role.SCP_049_2;
            SCPs["helicopter"] = Role.SCP_049_2;
            SCPs["plane"] = Role.SCP_049_2;
            SCPs["10"] = Role.SCP_049_2;
            SCPs[zombie] = Role.SCP_049_2;

            var larry = Role.SCP_106.ToString().ToLower();
            SCPs["larry"] = Role.SCP_106;
            SCPs["106"] = Role.SCP_106;
            SCPs["3"] = Role.SCP_106;
            SCPs[larry] = Role.SCP_106;

            var shyguy = Role.SCP_096.ToString().ToLower();
            SCPs["shyguy"] = Role.SCP_096;
            SCPs["096"] = Role.SCP_096;
            SCPs["9"] = Role.SCP_096;
            SCPs[shyguy] = Role.SCP_096;

            var comp = Role.SCP_079.ToString().ToLower();
            SCPs["079"] = Role.SCP_079;
            SCPs["comp"] = Role.SCP_079;
            SCPs["computer"] = Role.SCP_079;
            SCPs["7"] = Role.SCP_079;
            SCPs[comp] = Role.SCP_079;

            var peanut = Role.SCP_173.ToString().ToLower();
            SCPs["peanut"] = Role.SCP_173;
            SCPs["173"] = Role.SCP_173;
            SCPs["0"] = Role.SCP_173;
            SCPs[peanut] = Role.SCP_173;

            var doggo1 = Role.SCP_939_53.ToString().ToLower();
            var doggo2 = Role.SCP_939_89.ToString().ToLower();
            SCPs["93953"] = Role.SCP_939_53;
            SCPs["93989"] = Role.SCP_939_89;
            SCPs["16"] = Role.SCP_939_53;
            SCPs["17"] = Role.SCP_939_89;
            SCPs[doggo1] = Role.SCP_939_53;
            SCPs[doggo2] = Role.SCP_939_89;

            var tut = Role.TUTORIAL.ToString().ToLower();
            Humans["tut"] = Role.TUTORIAL;
            Humans["tutor"] = Role.TUTORIAL;
            Humans["tutorial"] = Role.TUTORIAL;
            Humans["14"] = Role.TUTORIAL;
            Humans[tut] = Role.TUTORIAL;

            var spec = Role.SPECTATOR.ToString().ToLower();
            Other["spec"] = Role.SPECTATOR;
            Other["specboi"] = Role.SPECTATOR;
            Other["ghost"] = Role.SPECTATOR;
            Other["2"] = Role.SPECTATOR;
            Other[spec] = Role.SPECTATOR;
            #endregion
            #region dare rei agnomes
            // prae perficio
            Keycards = new Dictionary<string, ItemType>();
            Weapons = new Dictionary<string, ItemType>();
            Ammo = new Dictionary<string, ItemType>();
            Accessories = new Dictionary<string, ItemType>();
            // perficio
            var kc_ci = ItemType.CHAOS_INSURGENCY_DEVICE.ToString().ToLower();
            Keycards["cidevice"] = ItemType.CHAOS_INSURGENCY_DEVICE;
            Keycards["kcci"] = ItemType.CHAOS_INSURGENCY_DEVICE;
            Keycards["ci"] = ItemType.CHAOS_INSURGENCY_DEVICE;
            Keycards[kc_ci] = ItemType.CHAOS_INSURGENCY_DEVICE;

            var coin = ItemType.COIN.ToString().ToLower();
            Accessories["coin"] = ItemType.COIN;
            Accessories["quarter"] = ItemType.COIN;
            Accessories["25c"] = ItemType.COIN;
            Accessories["50c"] = ItemType.COIN;
            Accessories["17"] = ItemType.COIN;
            Accessories[coin] = ItemType.COIN;

            var pew = ItemType.COM15.ToString().ToLower();
            Weapons["pistol"] = ItemType.COM15;
            Weapons["com15"] = ItemType.COM15;
            Weapons["handgun"] = ItemType.COM15;
            Weapons["13"] = ItemType.COM15;
            Weapons[pew] = ItemType.COM15;

            var kc_ce = ItemType.CONTAINMENT_ENGINEER_KEYCARD.ToString().ToLower();
            Keycards["ce"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            Keycards["containmentengineer"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            Keycards["pinkcard"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            Keycards["pink"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            Keycards["kcce"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            Keycards["6"] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;
            Keycards[kc_ce] = ItemType.CONTAINMENT_ENGINEER_KEYCARD;

            var cup = ItemType.CUP.ToString().ToLower();
            Accessories["cup"] = ItemType.CUP;
            Accessories["18"] = ItemType.CUP;
            Accessories[cup] = ItemType.CUP;

            var disarm = ItemType.DISARMER.ToString().ToLower();
            Accessories["disarm"] = ItemType.DISARMER;
            Accessories["disarmer"] = ItemType.DISARMER;
            Accessories["detain"] = ItemType.DISARMER;
            Accessories["detainer"] = ItemType.DISARMER;
            Accessories["handcuffs"] = ItemType.DISARMER;
            Accessories[disarm] = ItemType.DISARMER;

            var fusion = ItemType.DROPPED_5.ToString().ToLower();
            Ammo["fusion"] = ItemType.DROPPED_5;
            Ammo["mtfammo"] = ItemType.DROPPED_5;
            Ammo["ntfammo"] = ItemType.DROPPED_5;
            Ammo["e11ammo"] = ItemType.DROPPED_5;
            Ammo["ammo5"] = ItemType.DROPPED_5;
            Ammo["556mm"] = ItemType.DROPPED_5;
            Ammo["22"] = ItemType.DROPPED_5;
            Ammo[fusion] = ItemType.DROPPED_5;

            var pat = ItemType.DROPPED_7.ToString().ToLower();
            Ammo["9mm"] = ItemType.DROPPED_7;
            Ammo["pat"] = ItemType.DROPPED_7;
            Ammo["ammo7"] = ItemType.DROPPED_7;
            Ammo["29"] = ItemType.DROPPED_7;
            Ammo[pat] = ItemType.DROPPED_7;

            var rat = ItemType.DROPPED_9.ToString().ToLower();
            Ammo["762mm"] = ItemType.DROPPED_9;
            Ammo["rat"] = ItemType.DROPPED_9;
            Ammo["ammo9"] = ItemType.DROPPED_9;
            Ammo["28"] = ItemType.DROPPED_9;
            Ammo[rat] = ItemType.DROPPED_9;

            var e11 = ItemType.E11_STANDARD_RIFLE.ToString().ToLower();
            Weapons["mtfgun"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["ntfgun"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["epsilon"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["epsilonstandardrifle"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["epsilon11"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["epsilon11standardrifle"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["e11"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["esr"] = ItemType.E11_STANDARD_RIFLE;
            Weapons["20"] = ItemType.E11_STANDARD_RIFLE;
            Weapons[e11] = ItemType.E11_STANDARD_RIFLE;

            var kc_fm = ItemType.FACILITY_MANAGER_KEYCARD.ToString().ToLower();
            Keycards["red"] = ItemType.FACILITY_MANAGER_KEYCARD;
            Keycards["9"] = ItemType.FACILITY_MANAGER_KEYCARD;
            Keycards["kcfm"] = ItemType.FACILITY_MANAGER_KEYCARD;
            Keycards["redcard"] = ItemType.FACILITY_MANAGER_KEYCARD;
            Keycards[kc_fm] = ItemType.FACILITY_MANAGER_KEYCARD;

            var fb = ItemType.FLASHBANG.ToString().ToLower();
            Weapons["fb"] = ItemType.FLASHBANG;
            Weapons["stun"] = ItemType.FLASHBANG;
            Weapons["flashbang"] = ItemType.FLASHBANG;
            Weapons["stungrenade"] = ItemType.FLASHBANG;
            Weapons["26"] = ItemType.FLASHBANG;
            Weapons[fb] = ItemType.FLASHBANG;

            var fl = ItemType.FLASHLIGHT.ToString().ToLower();
            Accessories["flashlight"] = ItemType.FLASHLIGHT;
            Accessories["fl"] = ItemType.FLASHLIGHT;
            Accessories["torch"] = ItemType.FLASHLIGHT;
            Accessories["lamp"] = ItemType.FLASHLIGHT;
            Accessories["15"] = ItemType.FLASHLIGHT;
            Accessories[fl] = ItemType.FLASHLIGHT;

            var frag = ItemType.FRAG_GRENADE.ToString().ToLower();
            Weapons["frag"] = ItemType.FRAG_GRENADE;
            Weapons["grenade"] = ItemType.FRAG_GRENADE;
            Weapons["boom"] = ItemType.FRAG_GRENADE;
            Weapons["25"] = ItemType.FRAG_GRENADE;
            Weapons[frag] = ItemType.FRAG_GRENADE;

            var kc_gu = ItemType.GUARD_KEYCARD.ToString().ToLower();
            Keycards["kcguard"] = ItemType.GUARD_KEYCARD;
            Keycards["guardkey"] = ItemType.GUARD_KEYCARD;
            Keycards["4"] = ItemType.GUARD_KEYCARD;
            Keycards[kc_gu] = ItemType.GUARD_KEYCARD;

            var kc_jan = ItemType.JANITOR_KEYCARD.ToString().ToLower();
            Keycards["kcjan"] = ItemType.JANITOR_KEYCARD;
            Keycards["janitor"] = ItemType.JANITOR_KEYCARD;
            Keycards["0"] = ItemType.JANITOR_KEYCARD;
            Keycards[kc_jan] = ItemType.JANITOR_KEYCARD;

            var cigun = ItemType.LOGICER.ToString().ToLower();
            Weapons["chaosgun"] = ItemType.LOGICER;
            Weapons["logicer"] = ItemType.LOGICER;
            Weapons["cigun"] = ItemType.LOGICER;
            Weapons["lmg"] = ItemType.LOGICER;
            Weapons["24"] = ItemType.LOGICER;
            Weapons[cigun] = ItemType.LOGICER;

            var kc_msci = ItemType.MAJOR_SCIENTIST_KEYCARD.ToString().ToLower();
            Keycards["majscikey"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            Keycards["majorsci"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            Keycards["majorscientistkeycard"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            Keycards["2"] = ItemType.MAJOR_SCIENTIST_KEYCARD;
            Keycards[kc_msci] = ItemType.MAJOR_SCIENTIST_KEYCARD;

            var med = ItemType.MEDKIT.ToString().ToLower();
            Accessories["medkit"] = ItemType.MEDKIT;
            Accessories["med"] = ItemType.MEDKIT;
            Accessories["14"] = ItemType.MEDKIT;
            Accessories[med] = ItemType.MEDKIT;

            var micro = ItemType.MICROHID.ToString().ToLower();
            Weapons["micro"] = ItemType.MICROHID;
            Weapons["microwave"] = ItemType.MICROHID;
            Weapons["supersoaker"] = ItemType.MICROHID;
            Weapons["microhid"] = ItemType.MICROHID;
            Weapons["16"] = ItemType.MICROHID;
            Weapons[micro] = ItemType.MICROHID;

            var mp7 = ItemType.MP4.ToString().ToLower();
            Weapons["mp7"] = ItemType.MP4;
            Weapons["mp4"] = ItemType.MP4;
            Weapons["smg"] = ItemType.MP4;
            Weapons["scorpion"] = ItemType.MP4;
            Weapons["23"] = ItemType.MP4;
            Weapons[mp7] = ItemType.MP4;

            var kc_ntfc = ItemType.MTF_COMMANDER_KEYCARD.ToString().ToLower();
            Keycards["kcmtfc"] = ItemType.MTF_COMMANDER_KEYCARD;
            Keycards["kcntfc"] = ItemType.MTF_COMMANDER_KEYCARD;
            Keycards["commanderkey"] = ItemType.MTF_COMMANDER_KEYCARD;
            Keycards["8"] = ItemType.MTF_COMMANDER_KEYCARD;
            Keycards[kc_ntfc] = ItemType.MTF_COMMANDER_KEYCARD;

            var kc_ntfl = ItemType.MTF_LIEUTENANT_KEYCARD.ToString().ToLower();
            Keycards["kcmtfl"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            Keycards["kcntfl"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            Keycards["lieutenantkey"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            Keycards["7"] = ItemType.MTF_LIEUTENANT_KEYCARD;
            Keycards[kc_ntfl] = ItemType.MTF_LIEUTENANT_KEYCARD;

            var kc_o5 = ItemType.O5_LEVEL_KEYCARD.ToString().ToLower();
            Keycards["o5"] = ItemType.O5_LEVEL_KEYCARD;
            Keycards["kco5"] = ItemType.O5_LEVEL_KEYCARD;
            Keycards["black"] = ItemType.O5_LEVEL_KEYCARD;
            Keycards["blackcard"] = ItemType.O5_LEVEL_KEYCARD;
            Keycards["11"] = ItemType.O5_LEVEL_KEYCARD;
            Keycards[kc_o5] = ItemType.O5_LEVEL_KEYCARD;

            var p90 = ItemType.P90.ToString().ToLower();
            Weapons["p90"] = ItemType.P90;
            Weapons["russia"] = ItemType.P90;
            Weapons["russian"] = ItemType.P90;
            Weapons["21"] = ItemType.P90;
            Weapons[p90] = ItemType.P90;

            var kc_sci = ItemType.SCIENTIST_KEYCARD.ToString().ToLower().ToLower();
            Keycards["sci"] = ItemType.SCIENTIST_KEYCARD;
            Keycards["kcsci"] = ItemType.SCIENTIST_KEYCARD;
            Keycards["1"] = ItemType.SCIENTIST_KEYCARD;
            Keycards[kc_sci] = ItemType.SCIENTIST_KEYCARD;

            var kc_mg = ItemType.SENIOR_GUARD_KEYCARD.ToString().ToLower();
            Keycards["kcmg"] = ItemType.SENIOR_GUARD_KEYCARD;
            Keycards["senior"] = ItemType.SENIOR_GUARD_KEYCARD;
            Keycards["seniorguard"] = ItemType.SENIOR_GUARD_KEYCARD;
            Keycards["sgk"] = ItemType.SENIOR_GUARD_KEYCARD;
            Keycards["5"] = ItemType.SENIOR_GUARD_KEYCARD;
            Keycards[kc_mg] = ItemType.SENIOR_GUARD_KEYCARD;

            var wmt = ItemType.WEAPON_MANAGER_TABLET.ToString().ToLower();
            Keycards["wmt"] = ItemType.WEAPON_MANAGER_TABLET;
            Keycards["tablet"] = ItemType.WEAPON_MANAGER_TABLET;
            Keycards["19"] = ItemType.WEAPON_MANAGER_TABLET;
            Keycards[wmt] = ItemType.WEAPON_MANAGER_TABLET;

            var kc_zm = ItemType.ZONE_MANAGER_KEYCARD.ToString().ToLower();
            Keycards["blue"] = ItemType.ZONE_MANAGER_KEYCARD;
            Keycards["zone"] = ItemType.ZONE_MANAGER_KEYCARD;
            Keycards["kczm"] = ItemType.ZONE_MANAGER_KEYCARD;
            Keycards["3"] = ItemType.ZONE_MANAGER_KEYCARD;
            Keycards[kc_zm] = ItemType.ZONE_MANAGER_KEYCARD;

            var rad = ItemType.RADIO.ToString().ToLower();
            Accessories["rad"] = ItemType.RADIO;
            Accessories["radio"] = ItemType.RADIO;
            Accessories["walkietalkie"] = ItemType.RADIO;
            Accessories["12"] = ItemType.RADIO;
            Accessories[rad] = ItemType.RADIO;
            #endregion
        }
    }
}